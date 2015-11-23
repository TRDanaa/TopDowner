using UnityEngine;
using System.Collections;

public class CharacterJump : MonoBehaviour {

    Rigidbody rigidBody;
    Animator anim;

    public float jumpForce = 10000.0f;
    bool jump;
    bool jumpAnim;
    bool air;
    bool land;


    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        jump = Input.GetButton("Jump");

        if (air == true)
        {
            jumpAnim = false;
        }   
	}

    void FixedUpdate()
    {
        anim.SetBool("Jump", jumpAnim);
        anim.SetBool("Air", air);
        anim.SetBool("Land", land);

        if (air == true)
        {
            rigidBody.AddForce(0, -300.0f*Time.deltaTime, 0);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            air = false;
            if (jump == true)
            {
                Jump();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            air = true;
            land = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            land = true;
        }
    }

    void Jump()
    {
        land = false;
        jumpAnim = true;
        rigidBody.AddForce(0, jumpForce * Time.deltaTime, 0);
    }
}
