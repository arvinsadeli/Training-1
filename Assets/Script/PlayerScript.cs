using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isJumping;
    public MovementClass movement;
    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(movement.velocity);
        Jump(movement.jumpForce);
    }

    public void Movement(float velocity)
    {
        float input = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * velocity, rb.velocity.y);
        if(input < -.01f)
        {
            Debug.Log(input + "Going left");
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (input > .01f)
        {
            Debug.Log(input + "Going Right");
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        anim.SetBool("isRunning", rb.velocity.x != 0);
    }

    public void Jump(float jumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    IEnumerator CheckIfLanded()
    {
        if(rb.velocity.y <= 0)
        {
            isJumping = false;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CheckIfLanded());
        }
        Debug.Log("Velocity = " + rb.velocity.y);
    }

    [System.Serializable]
    public class MovementClass
    {
        public float jumpForce = 3f;
        public float velocity = 5f;
    }
}
