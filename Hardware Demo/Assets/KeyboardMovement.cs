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
        if (Input.GetKey(KeyCode.V))
        {
            Debug.Log("Lefty");
        }

        string message = serialController.ReadSerialMessage();


        //if (message == null)
        //    return;


        //// Check if the message is plain data or a connect/disconnect event.
        //if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        //    Debug.Log("Connection established");
        //else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        //    Debug.Log("Connection attempt failed or disconnection detected");
        //else
        //{
        //    Debug.Log("Message arrived: " + message);

        //}


        if ((Input.GetKeyDown(KeyCode.Escape) || message == "START") && pauseMenu.activeInHierarchy == false)
        {
            Start_Button.GetComponent<Renderer>().material = red;
            Select_Button.GetComponent<Renderer>().material = red;

            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || message == "START") && pauseMenu.activeInHierarchy == true)
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
        


        //if (Input.GetKeyDown(KeyCode.Space) || message == "X" && isGrounded)
        //{
        //    X_Button.GetComponent<Renderer>().material = red;
        //    isJumping = true;
        //    Debug.Log("REACHED HERE");
        //}
        //if (Input.GetKeyUp(KeyCode.Space) || (message == "X" && isJumping))
        //{

        //    isJumping = false;
        //    X_Button.GetComponent<Renderer>().material = white;
        //}

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

        //if (Input.GetKeyDown(KeyCode.Space) || message == "X" && isGrounded)
        //{
        //    X_Button.GetComponent<Renderer>().material = red;
        //    isJumping = true;
        //    Debug.Log("REACHED HERE");
        //}
        //if (Input.GetKeyUp(KeyCode.Space) || message == "X")
        //{

        //    isJumping = false;
        //    X_Button.GetComponent<Renderer>().material = white;
        //}

        if (Input.GetKeyDown(KeyCode.RightArrow) && dashCount < 1)
        {
            //isDashing = true;
            //Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //Vector3 endPos = new Vector3(transform.position.x + 120, transform.position.y, transform.position.z);
            //timer += Time.deltaTime;


            ////transform.Translate(Vector3.right* dashForce * Time.deltaTime);

            //transform.position = Vector3.Lerp(startPos, endPos, 5 * Time.deltaTime);
            //timer = 0.0f;
            dashCount++;


            speed = 60.0f;
            isDashing = true;
            R_Trigger.GetComponent<Renderer>().material = red;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && dashCount < 1)
        {
            //isDashing = true;
            //Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //Vector3 endPos = new Vector3(transform.position.x - 120, transform.position.y, transform.position.z);
            ////timer += Time.deltaTime;


            ////transform.Translate(Vector3.right* dashForce * Time.deltaTime);

            //transform.position = Vector3.Lerp(startPos, endPos, 5 * Time.deltaTime);
           

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
            rb.velocity = new Vector3(0, jumpForce, 0);
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

        //if (rightDash == true)
        //{
        //    rb.AddForce(Vector3.right * dashForce * Time.deltaTime, ForceMode.Impulse);
        //}
            
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

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
