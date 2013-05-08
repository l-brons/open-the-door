using UnityEngine;
using System.Collections;

public class PlayerTargetedAttack : MonoBehaviour {
	
	public GameObject target;
	public float attackWaitTime;
	public float coolDown; //This is to be able to adjust this in GUI

	// Use this for initialization
	void Start () {
		attackWaitTime = 0; //When the player attacks, it sets the coolDown and he/she must wait until the timer is done
		coolDown = 2.0f; //in seconds
	}
	
	// Update is called once per frame
	void Update () {
		if (attackWaitTime > 0) {
			attackWaitTime -= Time.deltaTime;	//Every frame that attackWaitTime > 0, attackWaitTime is subtracted by the render time of the frame 
		}
		
		if (attackWaitTime < 0) {
			attackWaitTime = 0;
		}
		
		//Check for key press - GetKeyUp, when key is released
		if (Input.GetKeyUp(KeyCode.F)) {
			if (attackWaitTime == 0) {
				TargetedAttack();
				attackWaitTime = coolDown;
			}
		}
	}
	
	private void TargetedAttack() {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 directionVec3 = (target.transform.position - transform.position).normalized; //Takes enemy position and player position and returns the vector with magnitude of 1    
		
//		float direction = Vector3.Dot(directionVec3, transform.forward); //directionVec3 and transform.forward should be the same length for the dot product --> why we use forward
		//The dot product returns a number between 1 & -1, if enemy is in front of us, +#, if enemy is behind us, -#, and then if he's on either side, it returns 0
		
//		Debug.Log(direction); //Testing info outputs to console
		
		if (distance < 4.0f) { //Using decimals with floats, put f at the end
//			if (direction > 0) {
				EnemyHealth eHealth = (EnemyHealth)target.GetComponent("EnemyHealth"); //Grab reference to EnemyHealth script
				eHealth.AdjustCurrentHealth(-30); //Take away 10 health each time the enemy is hit
//			}
		}
	}
}
