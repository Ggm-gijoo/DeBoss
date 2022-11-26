using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private Camera mainCam;

    [Header("카메라 거리")]
    [SerializeField] private Vector3 CameraDistance;
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

    private Transform playerTransform;
    private Transform bossTransform;

    private GameObject targetTransformGameObj;

    private float rotationX = 0.0f;
    private float mouseX = 0.0f;

    public bool isBossAppear = false;

    private void Start()
    {
        mainCam = Camera.main;

        if(MainModule.player != null)
            playerTransform = MainModule.player.transform;
        if(MainModule.boss != null)
            bossTransform = MainModule.boss.transform;

        targetTransformGameObj = new GameObject();

        mainCam.transform.position = playerTransform.position + CameraDistance;
    }

    private void LateUpdate()
    {
        ZoomCamera();
        if (isBossAppear)
            RotateCamera(bossTransform);
        else
            RotateCamera(playerTransform);
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
    public void RotateCamera(Transform targetTransform)
    {
        mouseX = Input.GetAxis("Mouse X");

        Transform test = targetTransformGameObj.transform;
        test.position = (targetTransform.position + playerTransform.position) / 2.0f;

        rotationX += mouseX * rotateSpeed;
        mainCam.transform.rotation = Quaternion.Euler(0, rotationX, 0);

        Vector3 reverseDistance = new Vector3(CameraDistance.x, CameraDistance.y, CameraDistance.z);
        mainCam.transform.position = playerTransform.position + mainCam.transform.rotation * reverseDistance;

        mainCam.transform.LookAt(test);
    }



}
