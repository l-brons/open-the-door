using UnityEngine;
using System.Collections;

/* This script moves the character controller forward 
 * and sideways based on the arrow keys.
 * It also jumps when pressing space.
 * Make sure to attach a character controller to the same game object.
 * It is recommended that you make only one call to Move or SimpleMove per frame.*/  

public class PlayerMovement : MonoBehaviour {
	public float speed = 6.0f;
    public float jumpSpeed = 8.0f; 
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
	
	private Transform myTransform;
	private Vector3 pos;
	
	void Awake () {
		myTransform = transform;	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		
        if (controller.isGrounded) {	//Check if the controller on the ground
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = myTransform.TransformDirection(moveDirection);
           
			//Multiply it by speed.
            moveDirection *= speed;
            
			//Jumping
            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
			} 
        }
		//Make boundaries for player movement in X and Y direction
		pos = myTransform.position;		//Grab the player's position
		pos.x = Mathf.Clamp(transform.position.x, 0, 100);		
		pos.z = Mathf.Clamp(transform.position.z, -8, 7.5f);	
		myTransform.position = pos;		//Set player's position to those boundaries
		
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
       
		//Making the character move
        controller.Move(moveDirection * Time.deltaTime);
	}
}
 