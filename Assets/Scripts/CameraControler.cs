using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControler : MonoBehaviour
{
    private const float MIN_FOLLOW_YOFFSET = 4f;
    private const float MAX_FOLLOW_YOFFSET = 24f;
    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
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
        float zoomAmmount = 1f;
        targetFollowOffset.y += InputManager.Instance.GetCameraZoomAmmount();
        float zoomSpeed = 5f;
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_YOFFSET, MAX_FOLLOW_YOFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
