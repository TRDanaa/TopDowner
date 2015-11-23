using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

Rigidbody rigidBody;
public float speed = 1;
Animator anim;
Vector3 lookPos;

Transform cam;
Vector3 camForward;
Vector3 move;
Vector3 moveInput;

float forwardAmount;
float turnAmount;






void Start(){

	rigidBody = GetComponent<Rigidbody>();
	anim = GetComponent<Animator>();
	
	cam= Camera.main.transform;
	
}



    void Update(){
	
		
	
	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	RaycastHit hit;
	if(Physics.Raycast(ray, out hit, 100))
	{
		lookPos = hit.point;	
	}
	Vector3 lookDir = lookPos - transform.position;
	lookDir.y = 0;
	transform.LookAt(transform.position + lookDir, Vector3.up);


    }
	

void FixedUpdate(){

    float horizontal = Input.GetAxis("Horizontal");
	float vertical = Input.GetAxis("Vertical");

	
	if (cam != null)
	{
		camForward = Vector3.Scale(cam.up,new Vector3(1,0,1)).normalized;
		
		move = vertical * camForward + horizontal * cam.right;
		
	}
	else
	{
		move = vertical * Vector3.forward + horizontal *Vector3.right;
	}
	
	if(move.magnitude > 1)
	{
		move.Normalize();
	}
		
	Move(move);	
		
	Vector3 movement = new Vector3(horizontal,0,vertical);
	
	rigidBody.AddForce(movement * speed / Time.deltaTime);
	
	
	

}

    void Move(Vector3 move)
    {
	    if(move.magnitude>1)
	    {
	    move.Normalize();

	    }
	
	    this.moveInput = move;

	    ConvertMoveInput();
	    UpdateAnimator();
    }

    void ConvertMoveInput(){
	
	    Vector3 localMove = transform.InverseTransformDirection(moveInput);
	    turnAmount = localMove.x;
		
	    forwardAmount = localMove.z;
	
    }
    void UpdateAnimator(){
	
	    anim.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
	    anim.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);
    }

    
}
	









