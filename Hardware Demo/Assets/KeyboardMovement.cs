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

    

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= 3.0f;

        GreenGround.GetComponent<MeshCollider>().enabled = true;
        RedGround.GetComponent<MeshCollider>().enabled = false;
        PurpleGround.GetComponent<MeshCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            rb.velocity = new Vector3(-1 * speed, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = white;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            rb.velocity = new Vector3(1 * speed, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            JoyStick.GetComponent<Renderer>().material = white;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            X_Button.GetComponent<Renderer>().material = red;
            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

            isJumping = false;
            X_Button.GetComponent<Renderer>().material = white;
        }

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.gameObject.GetComponent<Renderer>().material = green;
            GreenGround.GetComponent<MeshCollider>().enabled = true;
            RedGround.GetComponent<MeshCollider>().enabled = false;
            PurpleGround.GetComponent<MeshCollider>().enabled = false;

            Square_Button.GetComponent<Renderer>().material = red;
        }
        else if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            Square_Button.GetComponent<Renderer>().material = white;
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.gameObject.GetComponent<Renderer>().material = red;
            GreenGround.GetComponent<MeshCollider>().enabled = false;
            RedGround.GetComponent<MeshCollider>().enabled = true;
            PurpleGround.GetComponent<MeshCollider>().enabled = false;
            Triangle_Button.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Triangle_Button.GetComponent<Renderer>().material = white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.gameObject.GetComponent<Renderer>().material = purple;
            GreenGround.GetComponent<MeshCollider>().enabled = false;
            RedGround.GetComponent<MeshCollider>().enabled = false;
            PurpleGround.GetComponent<MeshCollider>().enabled = true;
            O_Button.GetComponent<Renderer>().material = red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
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
            Debug.Log(rb.velocity.y.ToString());
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
