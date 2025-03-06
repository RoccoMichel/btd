using UnityEngine;
using Cinemachine;

public class CameraControler : MonoBehaviour
{
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
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }
    private void HandleMovement()
    {
        Vector2 inputMoveDir = InputManager.Instance.GetCameraMoveVector();

        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDir.y + transform.right * inputMoveDir.x;

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

        float rotationSpeed = 100f;

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        targetFollowOffset.y += InputManager.Instance.GetCameraZoomAmmount();
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_YOFFSET, MAX_FOLLOW_YOFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
