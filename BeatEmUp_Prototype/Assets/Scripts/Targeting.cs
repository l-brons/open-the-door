using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targeting : MonoBehaviour {

	public List<Transform> targets; //Holds enemy transforms as targets for the player to hit
	public Transform selectedTarget;
	
	private Transform myTransform;	//Cache our transform
	
	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;
		
		AddAllEnemies();
	}
	
	public void AddAllEnemies() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in objs) {
			AddTarget(enemy.transform);
		}
	}
	
	public void AddTarget(Transform enemy) {
		targets.Add(enemy);
	}
	
	private void SortTargetsByDistance() {
		targets.Sort(delegate(Transform t1, Transform t2) { //delegate creates a function in the parameters of a called method
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
			});	
	}
	
	private void TargetEnemy() {
		if (selectedTarget	== null) { //If nothing is selected, select the closet enemy
			SortTargetsByDistance();
			selectedTarget = targets[0];
		}
		else {
			int index = targets.IndexOf(selectedTarget);
			
			if (index < targets.Count - 1) { //If enemy selected is not the last enemy in the list, select the next one
				index++;	
			}
			else {
				index = 0;	//If enemy is the last one in the list, go back to the 1st/closest enemy
			}
			DeselectTarget(); //Deselect selected target
			selectedTarget = targets[index]; //Select new target
		}
		SelectTarget(); //Show new selected target
	}
	
	private void SelectTarget() { //Change the color of the selected target/enemy
		selectedTarget.renderer.material.color = Color.red;	
		
		PlayerTargetedAttack pAttack = (PlayerTargetedAttack)GetComponent<PlayerTargetedAttack>(); //Get a reference to the PlayerAttack script
		
		pAttack.target = selectedTarget.gameObject; //Set selected target to the target in the PlayerAttack script. selectedTarget is a transform and PlayerAttack wants a GameObject
	}
	
	private void DeselectTarget() { //Change the color of deselected targets {
		selectedTarget.renderer.material.color = Color.blue;	
		selectedTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) { //On Tab key down, select an enemy target
			TargetEnemy();
		}
	}
}
