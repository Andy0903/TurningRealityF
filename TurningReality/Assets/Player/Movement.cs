using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;
    [SerializeField]
    float distanceToGroundOffset = 0.1f;
    [SerializeField]
    float jumpForce = 6f;

    float distanceToGround;
    Rigidbody rb;

    bool IsGrounded { get { return Physics.Raycast(transform.position, Vector3.down, distanceToGround + distanceToGroundOffset); } }

    void Awake()
    {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");

        Vector3 cameraDirection = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = vertical * cameraDirection + horizontal * Camera.main.transform.right;
        movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime);

        if (jump && IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}
