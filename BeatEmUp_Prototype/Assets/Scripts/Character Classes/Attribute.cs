/// <summary>
/// Attribute.cs
/// Stephanie Li
/// March 2013
/// 
/// This is the class for all of the character attributes in-game
/// </summary>

/* An attribute is a piece of data (a “statistic”) that describes to what extent a fictional character in a role-playing game 
 * possesses a specific natural, in-born characteristic common to all characters in the game. That piece of data is usually 
 * an abstract number or, in some cases, a set of dice. Some games use different terms to refer to an attribute, such as 
 * statistic, (primary) characteristic or ability.*/

public class Attribute : BaseStat {					//Inherit from BaseStat class, when a new Attribute is created, the BaseStat constructor is going to be called first
	new public const int STARTING_EXP_COST = 50; 	//The new keyword allows you to override an existing constant
	
	private string _name; 							//Used to tie in the attribute name with its position in the enum list so that it can be saved as a modifying attribute
	
	public Attribute() {
		_name = "";
		ExpToLevel = 50;							//This means that our attributes will only cost 50 exp to raise it to the next level
		LevelModifier = 1.05f; 						//A smaller increase between Attribute points --> the LevelModifier should be saved in character data if each attribute has a different one
		
	}
	
	public string Name {
		get{ return this._name; }
		set{ this._name = value; }
	}
}

public enum AttributeName { //This enum is for all the attributes you have for your characters (enums can't have spaces, use underscores)
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Willpower,
	Charisma //No comma at the end
	
	/* Enums have default associated int values, starting at 0. 
	 * You can also assign values randomly to enums: (Might = 2, Constitution = 4)
	 * If you're not assigning int values, your order of enums should not change*/
}
