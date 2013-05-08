using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {
	
	public float attackWaitTime;
	public float coolDown; //This is to be able to adjust this in GUI
	
	public List<Transform> enemies; //Holds enemy transforms as targets for the player to hit
	public GameObject[] enemyObjs;
	
	private Transform myTransform;	//Cache player's transform
		
	// Use this for initialization
	void Start () {
		enemies = new List<Transform>();
		myTransform = transform;
		
		attackWaitTime = 0; //When the player attacks, it sets the coolDown and he/she must wait until the timer is done
		coolDown = 1.0f; //in seconds
		
		enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");
		AddAllEnemies();
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
		if (Input.GetKeyUp(KeyCode.D)) {
			if (attackWaitTime == 0) {
				Attack();
				attackWaitTime = coolDown;
			}
		}
	}
	
	public void AddAllEnemies() {
		foreach(GameObject enemy in enemyObjs) {
			AddTarget(enemy.transform);
		}
	}
	
	public void AddTarget(Transform enemy) {
		enemies.Add(enemy);
	}
	
	private void SortEnemiesByDistance() {
		enemies.Sort(delegate(Transform t1, Transform t2) { //delegate creates a function in the parameters of a called method
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
			});	
	}
	
	private void Attack() {
		SortEnemiesByDistance();
		
		//Get how close the 1st enemy is after enemies have been sorted by distance
		float distance = Vector3.Distance(enemies[0].transform.position, transform.position);
		
		foreach(GameObject enemy in enemyObjs) {
			if (distance < 3f) { //Using decimals with floats, put f at the end
				EnemyHealth eHealth = (EnemyHealth)enemy.GetComponent("EnemyHealth"); //Grab reference to EnemyHealth script
				eHealth.AdjustCurrentHealth(-10); //Take away 10 health each time the enemy is hit
			}
		}
	}
}
