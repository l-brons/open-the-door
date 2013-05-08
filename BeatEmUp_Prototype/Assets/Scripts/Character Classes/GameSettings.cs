using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";		//This is the name of the gameObject that the player will spawn at on the the start of the level
	
	void Awake () {
		DontDestroyOnLoad(this); //You don't want to destroy the game object this script is attached to on load; game objs are usually destroyed when changing scenes
	}
	
	public void SaveCharacterData() {
		//Get a reference to player object that was created in scene
		GameObject pc = GameObject.Find("Player Character"); //Finds a game object by name and returns it
		
		//Get a reference to the PlayerCharacter script that was attached to the "Player Character" object
		PlayerCharacter pcScript = pc.GetComponent<PlayerCharacter>();
		
		//Optional: delete all to get rid of all the keys in the registry
//		PlayerPrefs.DeleteAll();
		
		//Save a string -- the name; PrayerPrefs is Unity's class for saving/loading data
		PlayerPrefs.SetString("Player Name", pcScript.Name); //"Player Name" is the key to refer to this string from now on, next is reference to player name
		
		//Save the attributes
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			PlayerPrefs.SetInt(((AttributeName)i).ToString() + " - BaseValue", pcScript.GetPrimaryAttribute(i).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)i).ToString() + " - ExpToLevel", pcScript.GetPrimaryAttribute(i).ExpToLevel);		
		}
		
		//Save the vitals
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			PlayerPrefs.SetInt(((VitalName)i).ToString() + " - BaseValue", pcScript.GetVital(i).BaseValue);
			PlayerPrefs.SetInt(((VitalName)i).ToString() + " - ExpToLevel", pcScript.GetVital(i).ExpToLevel);				
			PlayerPrefs.SetInt(((VitalName)i).ToString() + " - CurrentValue", pcScript.GetVital(i).CurrentValue);						
		
//			PlayerPrefs.SetString(((VitalName)i).ToString() + " - Modifying Attributes", pcScript.GetVital(i).GetModifiyingAttributesString());	//NOT NEEDED							
		}
		
		//Save the skills
		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
			PlayerPrefs.SetInt(((SkillName)i).ToString() + " - BaseValue", pcScript.GetSkill(i).BaseValue);
			PlayerPrefs.SetInt(((SkillName)i).ToString() + " - ExpToLevel", pcScript.GetSkill(i).ExpToLevel);										
		
//			PlayerPrefs.SetString(((SkillName)i).ToString() + " - Modifying Attributes", pcScript.GetSkill(i).GetModifiyingAttributesString());	//NOT NEEDED							
		}
	}
	
	public void LoadCharacterData() {
		//Get a reference to player object that was created in scene
		GameObject pc = GameObject.Find("Player Character"); //Finds a game object by name and returns it
		
		//Get a reference to the PlayerCharacter script that was attached to the "Player Character" object
		PlayerCharacter pcScript = pc.GetComponent<PlayerCharacter>();
		
		//Get a string -- the name; PrayerPrefs is Unity's class for saving/loading data
		pcScript.Name = PlayerPrefs.GetString("Player Name", "Default"); //"Player Name" is the key to refer to this string from now on, next is default name
		
		//Load the attributes
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			pcScript.GetPrimaryAttribute(i).BaseValue = PlayerPrefs.GetInt(((AttributeName)i).ToString() + " - BaseValue", 0);
			pcScript.GetPrimaryAttribute(i).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)i).ToString() + " - ExpToLevel", 0);		
		}
		
		//Load the vitals
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			pcScript.GetVital(i).BaseValue = PlayerPrefs.GetInt(((VitalName)i).ToString() + " - BaseValue", 0);
			pcScript.GetVital(i).ExpToLevel = PlayerPrefs.GetInt(((VitalName)i).ToString() + " - ExpToLevel", 0);				
			
			//Do this call so that the AdjustedBaseValue is updated before you try to call to get the currentValue
			pcScript.GetVital(i).Update();
			
			//Get stored value for the currentValue for each vital
			pcScript.GetVital(i).CurrentValue = PlayerPrefs.GetInt(((VitalName)i).ToString() + " - CurrentValue", 1);	//Use 1 so character doesn't load up dead
		}
		
		//Load the skills
		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
			pcScript.GetSkill(i).BaseValue = PlayerPrefs.GetInt(((SkillName)i).ToString() + " - BaseValue", 0);
			pcScript.GetSkill(i).ExpToLevel = PlayerPrefs.GetInt(((SkillName)i).ToString() + " - ExpToLevel", 0);										
		}		
		//Debug
//		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
//			Debug.Log(((SkillName)i).ToString() + " : " + pcScript.GetSkill(i).ExpToLevel);
//		}
	}
}
