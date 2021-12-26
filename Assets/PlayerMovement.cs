using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpSpeed = 50f;
    public float gravity = -9.8f;

    private Vector3 gravityVelocity;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Movement control:
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical; // moving forward direction = current forward direction
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Gravity:
        gravityVelocity.y += gravity * Time.deltaTime; // increase the falling velocity as the in-air time longer
        controller.Move(gravityVelocity * Time.deltaTime); // moving downwards to simulate gravity

        // Ground check:
        if (IsGrounded()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Vector3 jumpDirection = transform.up * jumpSpeed;
                controller.Move(jumpDirection * jumpSpeed * Time.deltaTime);
            }
            gravityVelocity.y = 0; // reset gravity velocity y to 0, otherwise the falling speed keep increasing over time
        }
    }

    bool IsGrounded()
    {
        // a ray from controller collider's center pointing to ground:
        bool hit = Physics.Raycast(controller.bounds.center, Vector3.down, controller.bounds.extents.y + 0.1f); // return true if ray hit another collider
        Color rayColor;
        if (hit) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(controller.bounds.center, Vector3.down * controller.bounds.extents.y, rayColor); // draw green line if ray hit
        return hit;
    }
}
