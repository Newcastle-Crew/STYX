using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    Vector3 moveRef;
    [SerializeField] float smoothingFactor = 10f;

    float mouseX, mouseY;

    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -30, 30);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, playerRef.GetComponent<Rigidbody>().position, ref moveRef, smoothingFactor);        
    }

}
