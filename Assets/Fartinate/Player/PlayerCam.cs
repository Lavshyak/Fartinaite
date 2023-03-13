using System;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform rotateHorizontal;
    public Transform rotateVertical;

    private float xRotation = 0;
    private float yRotation = 0;

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;
        
        yRotation += mouseX;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        rotateVertical.localRotation = Quaternion.Euler(xRotation, 0, 0);
        rotateHorizontal.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
