using UnityEngine;
using System.Collections;
using System;	//Added to quickly access the enum class (this is so we can assign attributes, vitals, and skills) 

//All players and mobs/enemies will derive from this base class, so this will contain all elements in common 
public class BaseCharacter : MonoBehaviour { //Inheriting from MonoBehaviour allows scripts to be dragged onto objects in the Unity scene
	private string _name;
	private int _level;
	
	//Exp that the character can spend
	private uint _freeExp; //uint = unsigned integer; exp can't be negative and this increases the positive range of the int
	
	//Array setup to hold attributes, vitals, and skills
	private Attribute[] _primaryAttributes;
	private Vital[] _vitals;
	private Skill[] _skills;
	
	public void Awake() { 	//Assign default values
		_name = String.Empty;
		_level = 0;
		_freeExp = 0;
		
		//Set up size of arrays using the number of elements in each enum
		_primaryAttributes = new Attribute[Enum.GetValues(typeof(AttributeName)).Length]; //Get number of attributes in AttributeName enum and set that as the array's size
		_vitals = new Vital[Enum.GetValues(typeof(VitalName)).Length]; 
		_skills = new Skill[Enum.GetValues(typeof(SkillName)).Length]; 
		
		//Call functions that instantiate the arrays
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}
	
#region Basic setters/getters 
	public string Name {
		get { return this._name; }
		set { this._name = value; }
	}
	
	public int Level {
		get { return this._level; }
		set { this._level = value; }
	}
	
	public uint FreeExp {
		get { return this._freeExp; }
		set { this._freeExp = value; }
	}
#endregion
	
	//Add experience to the character 
	public void AddExp(uint exp) {
		_freeExp += exp;
		
		CalculateLevel(); //Do this every time exp is added
	}
	
	/*Calculate level of the character (Traditional way is to level up character after it reaches a certain amount of exp.
	 * Traditional way needs a leveling up function that adds skill points/opens up new skills)
	 * The way that is done here is to take the average of all the character's skills and assign that as the level*/
	public void CalculateLevel() {
	
	}
	
	//Instantiate default attributes, vitals, and skills in arrays
	private void SetupPrimaryAttributes() {
		for (int i = 0; i < _primaryAttributes.Length; i++) {
			_primaryAttributes[i] = new Attribute();
			_primaryAttributes[i].Name = ((AttributeName)i).ToString (); //Set name of attribute
		}
	}
	
	private void SetupVitals() {
		for (int i = 0; i < _vitals.Length; i++) {
			_vitals[i] = new Vital();
		}
		SetupVitalModifiers(); //Call attribute modifier setup on stat
	}
	
	private void SetupSkills() {
		for (int i = 0; i < _skills.Length; i++) {
			_skills[i] = new Skill();
		}
		SetupSkillModifiers();
	}
	
#region Getters for the stat arrays (referred to by its index)
	public Attribute GetPrimaryAttribute(int index) { //All attributes are referred to by its index in the array
		return _primaryAttributes[index];
	}
	
	public Vital GetVital(int index) { 
		return _vitals[index];
	}
	
	public Skill GetSkill(int index) { 
		return _skills[index];
	}	
#endregion
	
	//Allow attributes to modify vital stat -- pass Attribute and ratio in (see ModifiedStat class)
	private void SetupVitalModifiers() {
		//Health 
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 0.5f));
		
		//Energy
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 1));
		
		//Mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 1));
	}
	
	private void SetupSkillModifiers() {
		//Examples of 2 modifiers affecting one skill stat
		//Melee_Offense
		GetSkill((int)SkillName.Melee_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Might), .33f));
		GetSkill((int)SkillName.Melee_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));
		
		//Melee_Defense
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), .33f));
		
		//Magic_Offense
		GetSkill((int)SkillName.Magic_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .33f));
		
		//Magic_Defense
		GetSkill((int)SkillName.Magic_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .33f));
		
		//Ranged_Offense
		GetSkill((int)SkillName.Ranged_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Ranged_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));		

		//Ranged_Defense
		GetSkill((int)SkillName.Ranged_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Ranged_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));		
	}
	
	/* Update function to be able to update all the character's modified stats
	 * Basically iterate through skill and vital arrays and call each of their Update() function inherited from ModifiedStat() */
	public void StatUpdate() {
		for (int i = 0; i < _vitals.Length; i++) {
			_vitals[i].Update();
		}
		
		for (int i = 0; i < _skills.Length; i++) {
			_skills[i].Update();
		}		
	}
}
