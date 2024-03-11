using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 10;
    [SerializeField]
    float slowspeed = 10;
    float speed;
    [SerializeField]
    float fastspeed = 10;
    Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = Vector3.zero;
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        speed = (Input.GetKey(KeyCode.LeftShift)) ? fastspeed: slowspeed;

        transform.position = transform.position + (cameraTransform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        transform.position = transform.position + (cameraTransform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
            transform.position = transform.position + (transform.up * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
            transform.position = transform.position + (transform.up * -1 * speed * Time.deltaTime);
        //float lookX = cameraTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        //float lookY = cameraTransform.localEulerAngles.x + Input.GetAxis("Mouse Y") * sensitivity;


        float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        //cameraTransform.localEulerAngles = new Vector3(lookY, lookX, 0f);
    }



}
