using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int distanceBuffer; //Max distance enemy will be away from player before moving toward player
	
	private Transform myTransform; //Enemy's transform
	
	void Awake() {
		myTransform = transform; //This is so that the transform of the enemy is easily captured without having to look it up -- cached
	}

	// Use this for initialization
	void Start () {
		//Set target to player's transform
		GameObject gameObj = GameObject.FindGameObjectWithTag("Player");
		target = gameObj.transform;
		
		distanceBuffer = 2; //Enemy attacks at 2.5 so this means that enemy will still attack
	}
	
	// Update is called once per frame
	void Update () {
		//Draw a line from enemy's transform.position (Vector3) to player
		Debug.DrawLine(target.position, myTransform.position, Color.yellow);
		
		//Get enemy to look at target; Quaternion is used to reference rotations and slerp is a spherical interpolation from and to your object 
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, //Angle where you're currently looking at
							   Quaternion.LookRotation(target.position - myTransform.position), //Take the enemy's poistion and then the players & slowly increment until enemy is looking at the right thing
							   rotationSpeed * Time.deltaTime); //How much to rotate by; Time.deltaTime normalizes the framerate on different computer systems (amount of time passed between frames)
													//Turn at the same speed on all computer systems
		
		//Stops enemy from getting too close to the player and spinning around him/her
		if (Vector3.Distance(target.position, myTransform.position) > distanceBuffer) {
			//Move towards target
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime; //myTransform moves the enemy forward in thr enemy's space
		}
	}
}
