using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    [Header("OnScreen Controls")] 
    public GameObject onScreenControls;
    public GameObject miniMap;

    [Header("Input")]
    public Joystick leftJoystick;

    [Header("Movement")] 
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection")] 
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        onScreenControls.SetActive((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer));
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal") + leftJoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + leftJoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);

        // jumping
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.M))
        {
            // toggle minimap
            miniMap.SetActive(!miniMap.activeInHierarchy);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnAButtonPressed()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void OnYButtonPressed()
    {
        // toggle minimap
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }
}
