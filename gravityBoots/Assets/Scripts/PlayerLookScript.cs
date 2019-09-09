using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cam; // camera
    [SerializeField]
    private float turnSpeed = 90; // turning speed (degrees/second)
    [SerializeField]
    private bool lockCursor = true;
    [SerializeField]
    private float maxUp = 180;
    [SerializeField]
    private float maxDown = 0;

    Transform cameraTransform;
    float pitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform temp = cam.transform;
        transform.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, 0);

        pitch -= Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, maxDown, maxUp);
        cam.transform.localEulerAngles = new Vector3(pitch, 0, 0);

        InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            lockCursor = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lockCursor = true;
        }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
