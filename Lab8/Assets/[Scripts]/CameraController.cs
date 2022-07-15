using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10.0f;
    public Transform playerBody;
    public Joystick rightJoystick;

    private float XRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Cursor.lockState = CursorLockMode.None;
            mouseSensitivity = 2.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseSensitivity = 7.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            x = rightJoystick.Horizontal * mouseSensitivity;
            y = rightJoystick.Vertical * mouseSensitivity;
        }
        else
        {
            x = Input.GetAxis("Mouse X") * mouseSensitivity;
            y = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        XRotation -= y;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * x);
    }
}
