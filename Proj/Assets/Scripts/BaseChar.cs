using UnityEngine;
using System.Collections;

public class BaseChar : MonoBehaviour {
	private int _health;
	private int _mana;
	private Vector3 _velocity;
	private Transform _position;//used to store position, rotation, and scale
	//look up Transform to see if we're going to do it this way
	private Vector3 _acceleration;
	private double _damageTakenModifier;

	// Use this for initialization
	void Start (){
		_health = 150;
		_mana = 150;
		_velocity = new Vector3(0,0,0);
		_position = new Vector3(0,0,0);
		_acceleration = new Vector3(0,0,0);
		_damageTakenModifier = 1;
	
	}
	
	// Update is called once per frame
	void Update (){
	
	}
}
