using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;

    [Space]
    public bool canLook = true;

    [Space]
    public Transform playerBody;

    float xRotation = 0f;

    public bool inverted;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Start()
    {
        mouseSensitivity = InfoStorage.Instance.sensitivity;
        inverted = InfoStorage.Instance.inverted;
        //Locks cursor to screen centre. 
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void FixedUpdate()
    {

        if (canLook == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

            switch(inverted)
            {
                case true:
                    xRotation += mouseY;
                    break;
                case false:
                    xRotation -= mouseY;
                    break;
            }

            //Ensures that the player can only look up and down to a maximum of 90 degrees. 
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void OnSliderValueChanged(float value)
    {
        mouseSensitivity = value;
        InfoStorage.Instance.sensitivity = value;
    }

    public void OnValueChanged(bool value)
    {
        inverted = value;
        InfoStorage.Instance.inverted = value;
    }
}
