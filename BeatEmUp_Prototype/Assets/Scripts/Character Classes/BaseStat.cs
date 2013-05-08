/// <summary>
/// Base stat.cs
/// Stephanie Li
/// March 2013
/// 
/// This is the base class for all stats in game
/// </summary>
public class BaseStat {
	public const int STARTING_EXP_COST = 100;	//Publicly accessible value for all base stats to start at	
	
	//The underscore denotes that the var is a private one with a setter/getter`
	private int _baseValue;						//Base value of this stat, each time we spend experience to raise a skill, the base value will go up by one
	private int _buffValue;						//The amount of buff to this stat, usually increases in stat temporarily (a temporary bonus)
	private int _expToLevel;					//This is the total amount of exp needed to raise/gain this skill/stat
	private float _levelModifier;				//Float from 0-1 to say how much the next stat point will cost us; recalculates how much exp we need to raise the stat to the next point
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat() {
		_baseValue = 0;
		_buffValue = 0;
		_expToLevel = STARTING_EXP_COST; //Base cost to raise to the 1st stat point, the next stat point will be recalculated using the levelModifier
		_levelModifier = 1.1f; //Everything will cose 10% more exp to raise
	}

#region Basic setters and getters using a shorthand .NET syntax
	/// <summary>
	/// Gets or sets the _baseValue.
	/// </summary>
	/// <value>
	/// The _baseValue.
	/// </value>
	public int BaseValue {
		get{ return this._baseValue; }
		set{ this._baseValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _buffValue.
	/// </summary>
	/// <value>
	/// The _buffValue.
	/// </value>
	public int BuffValue {
		get{ return this._buffValue; }
		set{ this._buffValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _expToLevel.
	/// </summary>
	/// <value>
	/// The _expToLevel.
	/// </value>
	public int ExpToLevel {
		get{ return this._expToLevel; }
		set{ this._expToLevel = value; }
	}
	
	/// <summary>
	/// Gets or sets the _levelModifier.
	/// </summary>
	/// <value>
	/// The _levelModifier.
	/// </value>
	public float LevelModifier {
		get{ return this._levelModifier; }
		set{ this._levelModifier = value; }
	}
#endregion
	
	/// <summary>
	/// Calculates how much exp we need to get to the next level
	/// </summary>
	/// <returns>
	/// The exp to level.
	/// </returns>
	private int CalculateExpToLevel() {
		return (int)(_expToLevel * _levelModifier); //Need typecast as an int because multiplying by a float produces a float -- typecast truncates, not rounds
	}
	
	/// <summary>
	/// Levels up. When the player levels up, a new value is assigned to _expToLevel and the baseValue of the stat also goes up by one
	/// </summary>
	public void LevelUp() {
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}

	/// <summary>
	/// Gets the adjusted base value. Returns the complete value of the stat, base + buff (using getter/setter syntax to avoid using "()")
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public int AdjustedBaseValue {
		get { return _baseValue + _buffValue; }	
	}
}
