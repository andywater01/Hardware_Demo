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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == false)
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
