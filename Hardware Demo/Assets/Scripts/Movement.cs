using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    PlayerControls controls;

    public float speed = 10.0f;
    public Rigidbody rb;
    public float jumpForce = 100.0f;
    public float dashForce = 100.0f;

    Vector2 move;
    Vector2 dash;

    bool isJumping = false;
    bool isGrounded = false;
    bool DashLeftB = false;
    bool DashRightB = false;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += mvr => Jump();
        controls.Gameplay.DashLeft.performed += mvr => dash = new Vector2(-10, 0);
        controls.Gameplay.DashRight.performed += mvr => DashRight();

        controls.Gameplay.Move.performed += mvr => move = mvr.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += mvr => move = Vector2.zero;
    }

    void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * speed * Time.deltaTime;
        transform.Translate(m.x, 0.0f, 0.0f, Space.World);

        Vector2 d = new Vector2(dash.x, dash.y) * Time.deltaTime;
        transform.Translate(d.x, 0.0f, 0.0f, Space.World);
        d = Vector2.zero;
        rb.velocity = Vector3.zero;
        //Debug.Log(rb.velocity.y.ToString());
    }

    void FixedUpdate()
    {
        if (isJumping && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            Debug.Log("is Jumping");
            isJumping = false;
        }
        if (DashLeftB)
        {
            rb.AddForce(Vector2.left * dashForce * Time.deltaTime, ForceMode.Impulse);
            Debug.Log("is Dashing");
            DashLeftB = false;
            
        }
        if (DashRightB)
        {
            rb.AddForce(Vector2.right * dashForce * Time.deltaTime, ForceMode.Impulse);
            Debug.Log("is Dashing");
            DashRightB = false;

        }
    }

    void DashLeft()
    {
        DashLeftB = true;
    }

    void DashRight()
    {
        //DashRightB = true;
    }

    void Jump()
    {
        isJumping = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }



}
