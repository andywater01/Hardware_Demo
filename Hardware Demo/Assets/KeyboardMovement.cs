using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KeyboardMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float dashForce = 45.0f;
    public Rigidbody rb;

    public bool isGrounded = false;
    public bool isJumping = false;

    public bool isDashing = false;
   

    public int dashCount = 0;

    float timer = 0.0f;

    float dashTimer = 0.0f;

    public GameObject Spawn;


    public Material green;
    public Material red;
    public Material purple;
    public Material white;

    public GameObject GreenGround;
    public GameObject RedGround;
    public GameObject PurpleGround;

    public GameObject FinishText;
    public TextMeshProUGUI text;

    //Buttons
    public GameObject R_Trigger;
    public GameObject L_Trigger;

    public GameObject X_Button;
    public GameObject Square_Button;
    public GameObject Triangle_Button;
    public GameObject O_Button;

    public GameObject JoyStick;

    public SerialController serialController;


    public GameObject pauseMenu;

    public GameObject Start_Button;
    public GameObject Select_Button;



    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= 3.0f;

        GreenGround.GetComponent<MeshCollider>().enabled = true;
        RedGround.GetComponent<MeshCollider>().enabled = false;
        PurpleGround.GetComponent<MeshCollider>().enabled = false;

        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();


        if ((Input.GetKeyDown(KeyCode.Escape) || message == "START" || message == "SELECT") && pauseMenu.activeInHierarchy == false)
        {
            Start_Button.GetComponent<Renderer>().material = red;
            Select_Button.GetComponent<Renderer>().material = red;

            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || message == "START" || message == "SELECT") && pauseMenu.activeInHierarchy == true)
        {
            Start_Button.GetComponent<Renderer>().material = white;
            Select_Button.GetComponent<Renderer>().material = white;

            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }



        if (Input.GetKeyDown(KeyCode.Space) || message == "X" && isGrounded)
        {
            X_Button.GetComponent<Renderer>().material = red;
            isJumping = true;
        }
        


        //Input
        if (Input.GetKey(KeyCode.A) || message == "LEFT")
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            rb.velocity = new Vector3(-1 * speed, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.A) || message == "STOP HORIZONTAL")
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = white;
        }

        if (Input.GetKey(KeyCode.D) || message == "RIGHT")
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            rb.velocity = new Vector3(1 * speed, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.D) || message == "STOP HORIZONTAL")
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = white;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) || message == "RIGHT TRIGGER" && dashCount < 1)
        {
            dashCount++;


            speed = 60.0f;
            isDashing = true;
            R_Trigger.GetComponent<Renderer>().material = red;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || message == "LEFT TRIGGER" && dashCount < 1)
        {
           
            dashCount++;


            speed = 60.0f;
            isDashing = true;
            L_Trigger.GetComponent<Renderer>().material = red;
        }


        if (isDashing == true)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= 0.4f)
            {
                speed = 25.0f;
                dashCount = 0;
                dashTimer = 0.0f;
                R_Trigger.GetComponent<Renderer>().material = white;
                L_Trigger.GetComponent<Renderer>().material = white;
            }
        }


        //Change Material
        if (Input.GetKeyDown(KeyCode.Alpha1) || message == "SQUARE")
        {
            this.gameObject.GetComponent<Renderer>().material = green;
            GreenGround.GetComponent<MeshCollider>().enabled = true;
            RedGround.GetComponent<MeshCollider>().enabled = false;
            PurpleGround.GetComponent<MeshCollider>().enabled = false;

            Square_Button.GetComponent<Renderer>().material = red;
        }
        else if(Input.GetKeyUp(KeyCode.Alpha1) || message == "SQUARE BUFFER")
        {
            Square_Button.GetComponent<Renderer>().material = white;
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha2) || message == "TRIANGLE")
        {
            this.gameObject.GetComponent<Renderer>().material = red;
            GreenGround.GetComponent<MeshCollider>().enabled = false;
            RedGround.GetComponent<MeshCollider>().enabled = true;
            PurpleGround.GetComponent<MeshCollider>().enabled = false;
            Triangle_Button.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) || message == "TRIANGLE BUFFER")
        {
            Triangle_Button.GetComponent<Renderer>().material = white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || message == "CIRCLE")
        {
            this.gameObject.GetComponent<Renderer>().material = purple;
            GreenGround.GetComponent<MeshCollider>().enabled = false;
            RedGround.GetComponent<MeshCollider>().enabled = false;
            PurpleGround.GetComponent<MeshCollider>().enabled = true;
            O_Button.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3) || message == "CIRCLE BUFFER")
        {
            O_Button.GetComponent<Renderer>().material = white;
        }



    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            isJumping = false;
        }
        
        if (rb.velocity.y < 0)
        {
            rb.AddForce(0, 0, 0);
            rb.AddForce(Vector3.down * 200 * Time.deltaTime, ForceMode.Impulse);

            X_Button.GetComponent<Renderer>().material = white;
            //Debug.Log(rb.velocity.y.ToString());
        }
            
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            dashCount = 0;
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            this.gameObject.transform.position = Spawn.transform.position;
            this.gameObject.GetComponent<Renderer>().material = green;
            GreenGround.GetComponent<MeshCollider>().enabled = true;
            RedGround.GetComponent<MeshCollider>().enabled = false;
            PurpleGround.GetComponent<MeshCollider>().enabled = false;
        }

    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            dashCount = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            FinishText.SetActive(true);
            text.text = "You Win!";
            Time.timeScale = 0.0f;
        }
    }
}
