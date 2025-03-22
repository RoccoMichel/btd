using UnityEngine;
using Cinemachine;
using System;

public class CameraControler : MonoBehaviour
{
    public static bool debug;
    [Space(20)]
    public bool useMaxDistance = true;
    public float xMaxDistance = 25;
    public float zMaxDistance = 25;

    private const float MIN_FOLLOW_YOFFSET = 4;
    private const float MAX_FOLLOW_YOFFSET = 30F;
    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;
    [Space(25)] [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    public float zoomSpeed = 5f;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)) debug = !debug;

        HandleMovement();
        HandleRotation();
        HandleZoom();
    }
    private void HandleMovement()
    {
        Vector2 inputMoveDir = InputManager.Instance.GetCameraMoveVector();

        float moveSpeed = 10f;

        if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = 40f;
        Vector3 moveVector = transform.forward * inputMoveDir.y + transform.right * inputMoveDir.x;

        Vector3 absPosition = new Vector3(Mathf.Abs(transform.position.x), Mathf.Abs(transform.position.y), Mathf.Abs(transform.position.z));
        
        transform.position += moveVector * moveSpeed * Time.deltaTime;

        // Clamp position based on Max Distance
        if (!useMaxDistance) return;
        transform.position = new Vector3
        {
            x = Mathf.Clamp(transform.position.x, -xMaxDistance, xMaxDistance),
            y = transform.position.y,
            z = Mathf.Clamp(transform.position.z, -zMaxDistance, zMaxDistance)
        };
    }
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        rotationVector.y = InputManager.Instance.GetCameraRotateAmmount();

        float rotationSpeed = 100f / Time.timeScale;

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        targetFollowOffset.y += InputManager.Instance.GetCameraZoomAmmount();
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_YOFFSET, MAX_FOLLOW_YOFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }

    /*You are entering the*/
    ////////////////////////////////////////
    ////////////// DEBUG ZONE //////////////
    ////////////////////////////////////////

    GameObject[] allTerrain;
    GameObject[] allUI;

    private void OnGUI()
    {
        if (!debug) return;

        Transform camera = Camera.main.transform;

        GUILayout.Label("CAMERA TRANSFORM:");
        GUILayout.Label(transform.position.ToString());
        GUILayout.Label(transform.eulerAngles.ToString());

        GUILayout.Label("----------------");

        GUILayout.Label(Mathf.Round(1 / Time.deltaTime).ToString() + " FPS");
        GUILayout.Label("Level time passed (sec): " + Time.timeSinceLevelLoad);


        if (GUI.Button(new Rect(10, 300, 120, 50), "Toggle PP"))
        {
            UnityEngine.Rendering.Universal.UniversalAdditionalCameraData uac = Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
            uac.renderPostProcessing = !uac.renderPostProcessing;
        }
        if (GUI.Button(new Rect(10, 370, 120, 50), "Toggle Terrain"))
        {
            try { foreach (GameObject terrain in allTerrain) terrain.SetActive(!terrain.activeSelf); }
            catch
            {
                Terrain[] terrains = FindObjectsByType<Terrain>(FindObjectsSortMode.None);
                allTerrain = Array.ConvertAll(terrains, t => t.gameObject);

                foreach (GameObject terrain in allTerrain) terrain.SetActive(!terrain.activeSelf);
            }
        }
        if (GUI.Button(new Rect(10, 440, 120, 50), "Infinite Wealth"))
        {
            FindAnyObjectByType<Session>().GetComponent<Session>().infiniteWealth = !FindAnyObjectByType<Session>().GetComponent<Session>().infiniteWealth;
        }
        if (GUI.Button(new Rect(10, 510, 120, 50), "Immortality"))
        {
            FindAnyObjectByType<Session>().GetComponent<Session>().immortal = !FindAnyObjectByType<Session>().GetComponent<Session>().immortal;
        }
        if (GUI.Button(new Rect(10, 580, 120, 50), "Toggle UI"))
        {
            try { foreach (GameObject canvas in allUI) canvas.SetActive(!canvas.activeSelf); }
            catch
            {
                Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                allUI = Array.ConvertAll(canvases, t => t.gameObject);

                foreach (GameObject canvas in allUI) canvas.SetActive(!canvas.activeSelf);
            }
        }
        if (GUI.Button(new Rect(10, 650, 120, 50), "Reload Level"))
        {
            LoadLogic.ReloadScene();
        }
        if (GUI.Button(new Rect(10, 720, 120, 50), "Main Menu"))
        {
            LoadLogic.LoadSceneByNumber(0);
        }
    }
}