using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;
    [SerializeField]
    float distanceToGroundOffset = 0.1f;
    public float jumpForce = 6f;

    public bool HoldsObject { get; set; }
    public bool StopTranslation { get; set; }

    float distanceToGround;
    Rigidbody rb;

    bool inAir;
    bool isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().isTrigger == false)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    void Awake()
    {
        if (PathManager.Instance != null)
        {
            const int savePositionTime = 1;
            InvokeRepeating("UpdatePathManagerData", savePositionTime, savePositionTime);
        }

        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
    }

    void UpdatePathManagerData()
    {
        PathManager.Instance.UpdateData(transform.localPosition);
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
        if (movement != Vector3.zero && isGrounded)
        {
            AudioManager.Instance.Play("Footstep");
            if (HoldsObject)
            {
                AudioManager.Instance.Play("DragOnFloor");
                AudioManager.Instance.Play("DragOnFloor2");
            }
        }
        else
        {
            AudioManager.Instance.Stop("DragOnFloor");
            AudioManager.Instance.Stop("DragOnFloor2");
        }

        transform.Translate(movement * speed * Time.deltaTime);

        if (jump && isGrounded)
        {
            AudioManager.Instance.Play("Jump");
            if (Input.GetButton("Cheat"))
            {
                rb.AddForce(Vector3.up * jumpForce * 3, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
        }

        if (inAir && isGrounded)
        {
            AudioManager.Instance.Play("Land");
        }
        inAir = !isGrounded;
    }

    void ProcessInput()
    {
        RotationInput();
        if (!StopTranslation)
        {
            TranslationInput();
        }
    }
}
