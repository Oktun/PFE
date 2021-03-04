using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    //This Handle Camera Settings
    [System.Serializable]
    public class CameraSettings
    {
        [Header("Camera Move Settings")]
        public float zoomSpeed = 5;
        public float moveSpeed = 5;
        public float rotationSpeed = 5;
        public float originalFieldOfView = 70;
        public float zoomFieldOfView = 20;
        public float MouseX_Sensitivity = 5;
        public float MouseY_Sensitivity = 5;
        public float MaxClampAngle = 90;
        public float MinClampAngle = -30;

        [Header("Camera Collision")]
        public Transform camPosition;
        public LayerMask camCollisionlayers;
    }
    [SerializeField]
    public CameraSettings cameraSettings;

    [System.Serializable]
    public class CameraInputSettings
    {
        public string MouseXAxis = "Mouse X";
        public string MouseYAxis = "Mouse Y";
        public string AimingInput = "Fire2";
    }
    [SerializeField]
    public CameraInputSettings inputSettings;

    //Camera rotation stuffs
    Transform center;
    Transform target;

    Camera mainCamera;

    float cameraXrotation = 0;
    float cameraYrotation = 0;

    //Camera collision stuffs
    Vector3 cameraInitialPos;
    RaycastHit hit;


    private void Start()
    {
        mainCamera = Camera.main;
        center = transform.GetChild(0);
        FindPlayer();
        cameraInitialPos = mainCamera.transform.localPosition;
    }

    private void Update()
    {
        if (!target)
            return;

        if (!Application.isPlaying)
            return;

        ZoomCamera();
        HandleCollision();
    }

    private void FixedUpdate () => RotateCamera();

    private void LateUpdate()
    {
        if (target)
        {
            FollowPlayer();
        }
        else
        {
            FindPlayer();
        }
    }

    //This's to find the gameobject WithTag Player
    private void FindPlayer() => target = GameObject.FindGameObjectWithTag("Player").transform;

    //Player Follow
    void FollowPlayer()
    {
        Vector3 moveVector = Vector3.Lerp(transform.position, target.transform.position, 
            cameraSettings.moveSpeed * Time.deltaTime);
        transform.position = moveVector;
    }

    //Handle Camera Rotation
    void RotateCamera()
    {
        cameraXrotation += Input.GetAxis(inputSettings.MouseYAxis) * cameraSettings.MouseY_Sensitivity;
        cameraYrotation += Input.GetAxis(inputSettings.MouseXAxis) * cameraSettings.MouseX_Sensitivity;

        cameraXrotation = Mathf.Clamp(cameraXrotation, cameraSettings.MinClampAngle, cameraSettings.MaxClampAngle);

        cameraYrotation = Mathf.Repeat(cameraYrotation, 360);

        Vector3 rotatingAngle = new Vector3(cameraXrotation, cameraYrotation, 0);

        Quaternion rotation = Quaternion.Slerp(center.transform.localRotation, Quaternion.Euler(rotatingAngle), 
            cameraSettings.rotationSpeed * Time.deltaTime);

        center.transform.localRotation = rotation;
    }

    //This  handle the Zoom In and the Zoom Out
    void ZoomCamera()
    {
        if (Input.GetButton(inputSettings.AimingInput))
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, cameraSettings.zoomFieldOfView,
                cameraSettings.zoomSpeed * Time.deltaTime);
        }
        else
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, cameraSettings.originalFieldOfView,
                cameraSettings.zoomSpeed * Time.deltaTime);
        }
    }

    //this handle the collision
    void HandleCollision()
    {
        if (!Application.isPlaying)
            return;

        if (Physics.Linecast(target.transform.position + target.transform.up, cameraSettings.camPosition.position,
            out hit, cameraSettings.camCollisionlayers))
        {
            Vector3 newCamPos = new Vector3(hit.point.x + hit.normal.x * .2f, hit.point.y + hit.normal.y * .8f,
                hit.point.z + hit.normal.z * .2f);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPos,
                Time.deltaTime * cameraSettings.moveSpeed);
        }
        else
        {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, cameraInitialPos,
                Time.deltaTime * cameraSettings.moveSpeed);
        }

        Debug.DrawLine(target.transform.position + target.transform.up, cameraSettings.camPosition.position,
            Color.blue);
    }
}
