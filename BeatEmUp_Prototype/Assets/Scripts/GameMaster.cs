using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public GameObject gameSettings;
	public Camera mainCamera;
	public Vector3 _playerSpawnPosition;		//This is the place were the player will spawn
	
	public float zOffset;
	public float yOffset;
	
	private GameObject _pc;
	private PlayerCharacter _pcScript; 

	
	// Use this for initialization
	void Start () {
		_playerSpawnPosition = new Vector3(156, 6, 166);		//Set default spawn location
		
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
		
		if (go == null){			//If spawn point doesn't exist, create it
			Debug.Log("Can not find Player Spawn Point");
			
			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);		//Create a new GameObject so we can create a gameobj anywhere in world and have the player spawn there
			Debug.Log("Created Player Spawn Point");
			
			go.transform.position = _playerSpawnPosition;
			Debug.Log("Moved Player Spawn Point");
		}
		
		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;	//Instantiate Prefab of character, placed at spawn point in the world and facing straight ahead
		_pc.name = "Player Character";		//Rename to proper name so it can be found by GameSettings
		
		_pcScript = _pc.GetComponent<PlayerCharacter>(); 	//Get reference of PlayerCharacter script
		
		zOffset = -0.342442f;
		yOffset = 0.00761807f;
		
		//Move camera to behind player
		mainCamera.transform.position = new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
	
		LoadCharacter();
	}
	
	public void LoadCharacter() {	//Call GameSettings' load function
		GameObject gs = GameObject.Find("__GameSettings");		
		
		if (gs == null) {	//If prefab (to avoid clicking through the CharacterGenerator screen each time) for game settings doesn't exist, instantiate one
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";	//Also make sure it's named properly
		}
		//Grab a reference to the GameSettings script that is attached to the __GameSettings object
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();			

		gsScript.LoadCharacterData(); //Call the GameSettings' load method			
	}
}
