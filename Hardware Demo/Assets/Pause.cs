using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject Start_Button;
    public GameObject Select_Button;

    public Material white;
    public Material red;

    public SerialController serialController;

    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();

        //if (message == null)
        //    return;

        //// Check if the message is plain data or a connect/disconnect event.
        //if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        //    Debug.Log("Connection established");
        //else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        //    Debug.Log("Connection attempt failed or disconnection detected");
        //else
        //    Debug.Log("Message arrived: " + message);


        if ((Input.GetKeyDown(KeyCode.Escape) || message == "START") && pauseMenu.activeInHierarchy == false)
        {
            Start_Button.GetComponent<Renderer>().material = white;
            Select_Button.GetComponent<Renderer>().material = white;

            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == true)
        {
            Start_Button.GetComponent<Renderer>().material = red;
            Select_Button.GetComponent<Renderer>().material = red;

            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
