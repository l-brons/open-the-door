using UnityEngine;
using System.Collections;
using System; //For enums

//This class creates a GUI to generater stats for the character; this class also creates a character

public class CharacterGenerator : MonoBehaviour {
	
	//Reference to the new character that will be created
	private PlayerCharacter _player; //This script runs when the player 1st joins the game --> need to make new scene in Unity
	
	//Reference to the player character prefab; a prefab is like a blueprint for other game objs in a scene
	public GameObject playerPrefab;
	
	//Constants for attributes
	private const int STARTING_POINTS = 100;	//Constant for the amount of points the player has to spend on his attributes
	private const int MIN_ATTRIBUTE_VALUE = 10;	//Constant for the minimum value of each attribute
	private const int STARTING_ATTRIBUTE_VALUE = 20; //Constant for the starting value for attributes, this is done as a suggestions, players can go down to the min value from here
	
	private int pointsLeft;
	
	//Constants for ease of understanding the Rects in GUI display functions
	private const int OFFSET = 5; //Offset is the amount of pixels that go around the screen and nothing is displayed here
	private const int LINE_HEIGHT = 20; //How tall each line will be
	private const int STAT_LABEL_WIDTH = 100; 
	private const int BASE_VALUE_LABEL_WIDTH = 30; 	
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HEIGHT = 20;
	private const int STAT_STARTING_POSITION = 40;
	
	//Testing GUI styles, this is for one specific element
	public GUIStyle myStyle; //Make this public so you can tweak it in the Unity inspector
	
	//Testing GUI skin, gives you elements (sliders, buttons, fields, etc) that come with OnGUI and you can customaize as you like; affects all elements of the same kind; collection of GUI styles
	public GUISkin mySkin;
	
	
	// Use this for initialization
	void Start () {
		//Instantiate the playerPrefab, Vector3.zero puts in the dead center of the game world, Quaterion.identity simply makes the obj face the direction that the gameobj is in (or that of its parent)
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject; //Typecast the Instantiate() results as a Gameobject 
		pc.name = "Player Character"; //Give the prefab a name
		
//		_player = new PlayerCharacter(); //Not the best way to do this -- you get warning that you can't use "new" with MonoBehaviour --> will fix later
//		_player.Awake(); //Another way to do this player instantiation will auto call Awake(), for now manual call is needed
		
		_player = pc.GetComponent<PlayerCharacter>(); //_player is now a reference to the PlayerCharacter script attached to the instantiated Prefab, Note: <type>
		
		pointsLeft = STARTING_POINTS; //Initialize points left to use to the allocated starting amount of points, pointsLeft will decrease as we spend points
		
		//Iterate through attributes and set their base values to MIN_STARTING_ATTRIBUTE_VALUE
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			_player.GetPrimaryAttribute(i).BaseValue = STARTING_ATTRIBUTE_VALUE; //Remember to set the base value
			pointsLeft -= (STARTING_ATTRIBUTE_VALUE - MIN_ATTRIBUTE_VALUE); //For each attribute, remove the points added from min values to bump up the starting values
		}
		
		_player.StatUpdate(); //Set initial values calculated for vitals/skills based upon intial values of attributes
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI() { //Makes a GUI in scene
		GUI.skin = mySkin; //Add the skin to elements below
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		
		GUI.skin = null; //No skin for vitals
		DisplayVitals();
		
		GUI.skin = mySkin; //Add the skin skills
		DisplaySkills();
		
		DisplayCreateButton(); //When the create button is clicked, the character data is saved and reloaded into the next scene
	}
	
#region Display functions for OnGUI()
	//Methods called by OnGUI() to make it cleaner when displaying stuff --> helps in making OnGUI() easier to read
	private void DisplayName() {
		GUI.Label(new Rect(OFFSET, OFFSET, 50, LINE_HEIGHT), "Name:"); //Rect(left-position, top-position, length, height); Label is on screen text
		_player.Name = GUI.TextField(new Rect(OFFSET + 50, OFFSET, STAT_LABEL_WIDTH, LINE_HEIGHT), _player.Name); //GUI.TextField returns a string, takes a string

	}
	
	private void DisplayAttributes() {
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			GUI.Label(new Rect(OFFSET, 											//x
							   STAT_STARTING_POSITION + (i * LINE_HEIGHT), 		//y
							   STAT_LABEL_WIDTH, 								//width
							   LINE_HEIGHT										//height
				), ((AttributeName)i).ToString()); //Typecast the index of the for loop into the correspnding enum AttributeName and then convert it to a String to display
			
			GUI.Label(new Rect(STAT_LABEL_WIDTH + OFFSET, 
							   STAT_STARTING_POSITION + (i * LINE_HEIGHT), 
							   BASE_VALUE_LABEL_WIDTH, 
							   LINE_HEIGHT
				), _player.GetPrimaryAttribute(i).AdjustedBaseValue.ToString()); 
			
			//Add buttons for increasing/decreasing attribute points
			if (GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASE_VALUE_LABEL_WIDTH, 
									STAT_STARTING_POSITION + (i * BUTTON_HEIGHT), 
									BUTTON_WIDTH, 
									BUTTON_HEIGHT
				), "-")) { //If block will be called when this button is clicked
				if (_player.GetPrimaryAttribute(i).BaseValue > MIN_ATTRIBUTE_VALUE) { //Check to make sure attribute does not go below the MIN_STARTING_ATTRIBUTE_VALUE
					_player.GetPrimaryAttribute(i).BaseValue--;
					pointsLeft++;
					_player.StatUpdate(); //If attribute value changes via button change, call StatUpdate() BaseCharacter class to update changes to vital/skill stats
				}
			}
			if (GUI.Button(new Rect(OFFSET * 2 + STAT_LABEL_WIDTH + BASE_VALUE_LABEL_WIDTH + BUTTON_WIDTH, 
									STAT_STARTING_POSITION + (i * BUTTON_HEIGHT), 
									BUTTON_WIDTH, 
									BUTTON_HEIGHT
				), "+", myStyle)) {
				if (pointsLeft > 0) { //Check to see if player has points left
					_player.GetPrimaryAttribute(i).BaseValue++;
					pointsLeft--;
					_player.StatUpdate(); //If attribute value changes via button change, call StatUpdate() BaseCharacter class to update changes to vital/skill stats
				}
			}
		}	
	}
	
	private void DisplayVitals() {
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			GUI.Label(new Rect(OFFSET, 
							   STAT_STARTING_POSITION + ((i + 7) * LINE_HEIGHT), 
							   STAT_LABEL_WIDTH, 
							   LINE_HEIGHT
				), 
				  			   ((VitalName)i).ToString()); //(i + 7) is for the seven attributes -- this pushes the vital display down so no overlap occurs
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH, 
							   STAT_STARTING_POSITION + ((i + 7) * LINE_HEIGHT), 
							   BASE_VALUE_LABEL_WIDTH, 
							   LINE_HEIGHT
				), _player.GetVital(i).AdjustedBaseValue.ToString()); 
		}	
		
	}
	
	private void DisplaySkills() {
		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASE_VALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 3, 
							   STAT_STARTING_POSITION + (i * LINE_HEIGHT), 
							   STAT_LABEL_WIDTH, 
							   LINE_HEIGHT
				), ((SkillName)i).ToString());
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASE_VALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 3 + STAT_LABEL_WIDTH, 
							   STAT_STARTING_POSITION + (i * LINE_HEIGHT), 
							   BASE_VALUE_LABEL_WIDTH, 
							   LINE_HEIGHT
				), _player.GetSkill(i).AdjustedBaseValue.ToString()); 
		}	
		
	}
	
	private void DisplayPointsLeft() {
		GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASE_VALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 3, 
						   OFFSET, 
						   STAT_LABEL_WIDTH, 
						   LINE_HEIGHT), "Points Left: " + pointsLeft.ToString());	
	}
	
	private void DisplayCreateButton() {
		if (pointsLeft > 0 || _player.Name == "") {		//If the player has not allocated all skill points or named the character, disable the create button
			GUI.Label(new Rect((Screen.width/2) - 100, 
					    	STAT_STARTING_POSITION + (11 * LINE_HEIGHT), 
					    	400, 
					    	LINE_HEIGHT
			), "Please allocate all points to character and enter a name.");		//Display instructional message
			GUI.enabled = false;
		}
		else {
			GUI.enabled = true;		//Once all skill points have been alloted and character is named, enable the create button
		}
		if (GUI.Button(new Rect((Screen.width/2) - 50, 
					    	STAT_STARTING_POSITION + (10 * LINE_HEIGHT), 
					    	STAT_LABEL_WIDTH, 
					    	LINE_HEIGHT
			), "Create")) {
			//Grab a reference to the GameObject __GameSettings
			GameObject gs = GameObject.Find("__GameSettings");
			
			//Grab a reference to the GameSettings script that is attached to the __GameSettings object
			GameSettings gsScript = gs.GetComponent<GameSettings>();
			
			//Change the currentValue of the vitals to the max modified value of that vital (bc deafult is 0 and that means the player dies)
			UpdateCurrentVitalValues();			
			
			gsScript.SaveCharacterData(); //Call the GameSettings' save method
			
			//Load next level by name or by index
			Application.LoadLevel("Level1"); //Don't forget get to add your scenes/levels to the Build Setting of Unity in the right order					
		}
	}
#endregion

	//Change currentValue of vitals to max modified values and not the default 0
	private void UpdateCurrentVitalValues() {
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			_player.GetVital(i).CurrentValue = _player.GetVital(i).AdjustedBaseValue;	
		}
	}

}
