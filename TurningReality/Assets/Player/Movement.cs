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

    void RotationInput()
    {
        float yaw = Input.GetAxis("Yaw");
        yaw = Mathf.Clamp(yaw, -1, 1);

        transform.Rotate(0, yaw, 0);
    }

    void TranslationInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(movement * speed * Time.deltaTime);

        if (jump && IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    void ProcessInput()
    {
        RotationInput();
        TranslationInput();
    }
}
