using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpHeight = 5;
    public float gravity = -9.8f;
    public CharacterController controller;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>(); // Automatically assign the controller
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Movement control:
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical; // moving forward direction = current forward direction
        if(Input.GetKey(KeyCode.LeftShift)) {
            controller.Move(moveDirection * (moveSpeed + 5f) * Time.deltaTime); // run speed
        } else {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime); // normal speed
        }

        // Simulate gravity:
        velocity.y += gravity * Time.deltaTime; // increase the falling velocity as the in-air time longer
        controller.Move(velocity * Time.deltaTime); // moving downwards to simulate gravity

        // Ground check:
        if (IsGrounded()) {
            velocity.y = 0; // reset gravity velocity y to 0, otherwise the falling speed keep increasing over time
            if (Input.GetButtonDown("Jump")) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // apply jump velocity equation
            }
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
