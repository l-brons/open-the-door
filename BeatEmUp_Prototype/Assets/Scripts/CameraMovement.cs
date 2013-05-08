using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	private GameObject _player;
	public float depthOffset = -60;		//z
	public float verticalOffset = 18;	//y
	
	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(_player.transform.position.x, verticalOffset , depthOffset);
	}
}
