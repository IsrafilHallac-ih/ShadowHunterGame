using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{

    public Transform player;
    public float mouseSensor = 200f;
    private float xRotation;



    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }







    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseXPos = Input.GetAxis("Mouse X") * mouseSensor * Time.deltaTime;
        float mouseYPos = Input.GetAxis("Mouse Y") * mouseSensor * Time.deltaTime;

        xRotation -= mouseYPos;
        xRotation = Mathf.Clamp(xRotation, -80f, 85f);  // Sýnýrlama
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        player.Rotate(Vector3.up * mouseXPos);





    }
}
