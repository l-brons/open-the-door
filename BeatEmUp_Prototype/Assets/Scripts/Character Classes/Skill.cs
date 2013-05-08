public class Skill : ModifiedStat {
	
	//Allows character to learn skills from skill system in the game
	private bool _known; 	//Tells the game whether or not the player knows this skill
	
	public Skill() {
		_known = false;		//Player will not know skills at start
		ExpToLevel = 25; 	//Skills are cheap to start off in this case
		LevelModifier = 1.1f;
	}
	
	public bool Known {
		get { return this._known; }
		set { this._known = value; }
	}
}

//List of all the skills in the game
public enum SkillName {
	Melee_Offense, //You can break these down further. Ex: weapons like axe, mace, sword, etc.
	Melee_Defense,
	Ranged_Offense,
	Ranged_Defense,
	Magic_Offense,
	Magic_Defense
}