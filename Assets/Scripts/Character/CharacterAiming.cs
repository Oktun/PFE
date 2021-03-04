using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class CharacterAiming : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform cameraLookAt;
    Camera mainCamera;

    Animator animator;
    int isAimaingParam = Animator.StringToHash("isAiming");
    
    [Header("Cinemachine Settings")]
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    private Cinemachine3rdPersonFollow cinemachine3RdPerson;
    private CinemachineVirtualCamera cinemachineVirtual;

    [Header("Zoom in Settings")]
    public float zoomInCameraDistance= 2f;
    public float zoomInShoulderOffsetX= 0.65f;
    public float zoomInFieldOfView= 30f;

    [Header("Zoom out Settings")]
    public float zoomOutCameraDistance= 3.2f;
    public float zoomOutShoulderOffsetX= 0.55f;
    public float zoomoutFieldOfView= 40f;


    [Header("Aim Settings")]
    public bool isAiming ;

    private void Awake()
    {
        cinemachine3RdPerson = FindObjectOfType<Cinemachine3rdPersonFollow>().GetComponent<Cinemachine3rdPersonFollow>();
        cinemachineVirtual = FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isAiming = Input.GetMouseButton(1);
        if (isAiming)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
        
    }


    private void LateUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void ZoomIn()
    {
        cinemachine3RdPerson.CameraDistance = zoomInCameraDistance;
        cinemachine3RdPerson.ShoulderOffset.x = zoomInShoulderOffsetX;
        cinemachineVirtual.m_Lens.FieldOfView = zoomInFieldOfView;
    }

    private void ZoomOut()
    {
        cinemachine3RdPerson.CameraDistance = zoomOutCameraDistance;
        cinemachine3RdPerson.ShoulderOffset.x = zoomOutShoulderOffsetX;
        cinemachineVirtual.m_Lens.FieldOfView = zoomoutFieldOfView;

    }
}*/
