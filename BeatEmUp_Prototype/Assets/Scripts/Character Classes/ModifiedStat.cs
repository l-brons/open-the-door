using System.Collections.Generic; //Import this to make our list

//This class is a base for other classes that will allow your character stats (vitals and skills) to be modified by your attributes
public class ModifiedStat : BaseStat {

	private List<ModifyingAttribute> _mods; //A list of Attributes that modify this stat
	private int _modValue;	//The amount added to the baseValue from the modifiers

	public ModifiedStat() {
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	//Adds Attributes able to modify the stat to the list
	public void AddModifier(ModifyingAttribute mod) {
		_mods.Add (mod);
	}
	
	//Calculate the amount to modify stat's base value by
	private void CalculateModValue() {
		_modValue = 0; //Reset _modValue to make sure that it is 0
		
		//Iterate through the list of modifying attributes and calculate how much each attibute will change the modValue based on the ratio
		if (_mods.Count > 0) { //If we have at least one Attribute in our list
			foreach(ModifyingAttribute attr in _mods) {
				_modValue += (int)(attr.attribute.AdjustedBaseValue * attr.ratio);
			}
		}
	}
	
	//Override AdjustedBaseValue function from parent class BaseStat to include _modValue
	public new int AdjustedBaseValue {
		get { return BaseValue + BuffValue + _modValue; }	//Using getters/setters for BaseValue and BuffValue		
	}
	
	public void Update() { //Not the same update function you have when you inherit from MonoBehaviour; this is just for the ModifiedStat class
		CalculateModValue(); 
		//Stick other private methods in here that need to be updated when a stat is raised
	}
	
	//Function that returns a string of all the indexes of modifying attribute so this info can be saved
	public string GetModifiyingAttributesString() {
		string temp = "";
		
		UnityEngine.Debug.Log(_mods.Count);
		
		for (int i = 0; i < _mods.Count; i++) {
			temp += _mods[i].attribute.Name;	//Concatenate name to string
			temp += "_";	//Deliminator in string
			temp += _mods[i].ratio;
			
			if (i < _mods.Count - 1) {		//If we're not the last modifying attribute in th list, add a deliminator
				temp += "|";
			}
			
//			UnityEngine.Debug.Log(_mods[i].attribute.Name);
//			UnityEngine.Debug.Log(_mods[i].ratio);
		}
		UnityEngine.Debug.Log(temp);
		return temp;
	}
}

public struct ModifyingAttribute {	//A struct is like a class with no methods; it's simply to keep a collection of vars
	public Attribute attribute; //Store instances of Attribute class
	public float ratio; //Tells you how much the Attribute will modify the stat (Ex: 1/3 of the Attribute may be added to the modValue)
	
	//Constructor
	public ModifyingAttribute(Attribute attr, float ratio) {
		this.attribute = attr;
		this.ratio = ratio;
	}
}