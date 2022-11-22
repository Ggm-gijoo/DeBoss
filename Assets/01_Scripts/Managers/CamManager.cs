using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private Camera mainCam;

    [Header("카메라 거리")]
    [SerializeField] private Vector3 CameraDistance;
    [Header("플레이어 위치")]
    [SerializeField] private Transform playerTransform;
    [Header("카메라 줌인/줌아웃 감도")]
    [Range(1f, 20f)]
    [SerializeField] private float zoomSpeed = 10f;
    [Header("카메라 회전 감도")]
    [Range(1f, 200f)]
    [SerializeField] private float rotateSpeed = 10f;

    [Header("최소 카메라 거리")]
    [SerializeField] private float nearestDist = 20f;
    [Header("최대 카메라 거리")]
    [SerializeField]private float farestDist = 60f;
    [Header("카메라 줌인 거리")]
    [SerializeField]private float nearDist = 50f;

    private float rotationX = 0.0f;
    private float mouseX = 0.0f;

    private void Awake()
    {
        mainCam = Camera.main;
        mainCam.transform.position = playerTransform.position + CameraDistance;
    }

    private void Update()
    {
        ZoomCamera();
        RotateCamera();
    }


    public void ZoomCamera()
    {
        float scroll = -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        
        switch (mainCam.fieldOfView)
        {
            case var a when a <= nearestDist && scroll < 0:
                mainCam.fieldOfView = nearestDist;
                break;
            case var a when a >= farestDist && scroll > 0:
                mainCam.fieldOfView = farestDist;
                break;
            default:
                mainCam.fieldOfView += scroll;
                break;
        }

    }
    public void RotateCamera()
    {
        mouseX = Input.GetAxis("Mouse X");

        rotationX += mouseX * rotateSpeed;
        mainCam.transform.rotation = Quaternion.Euler(0, rotationX, 0);

        Vector3 reverseDistance = new Vector3(0.0f, CameraDistance.y, CameraDistance.z);
        mainCam.transform.position = playerTransform.position + mainCam.transform.rotation * reverseDistance;

        mainCam.transform.LookAt(playerTransform);
    }



}
