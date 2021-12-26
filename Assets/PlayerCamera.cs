using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseXSpeed = 100f;
    public float mouseYSpeed = 100f;
    public Transform playerBody;
    public float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor in game view
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Mouse looking control:
        playerBody.Rotate(Vector3.up * mouseX * mouseXSpeed * Time.deltaTime);   // rotate player body around y axis (turn left/right)
        xRotation += -mouseY * mouseYSpeed * Time.deltaTime; // -mouseY since negative for lookup and positive for lookdown
        xRotation = Mathf.Clamp(xRotation, -90f, 70f); // -90 for max lookup angle, 70 for max lookup angle
        this.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // rotate player camera around x axis (lookup/lookdown)
    }
}
