public class Vital : ModifiedStat {

	private int _currentValue;	//Ex: AdjustedBaseValue is your max health, this var stores the heatlh you have as you get hit and lose health

	public Vital() {
		_currentValue = 0;
		ExpToLevel = 40;
		LevelModifier = 1.1f;
	}
	
	public int CurrentValue {
		get {
			if (_currentValue > AdjustedBaseValue) { //If current value is > than max value, set current value to max value
				_currentValue = AdjustedBaseValue;
			}
			return this._currentValue;
		}
		set { this._currentValue = value; }
	}
}

//List of vitals the character will have
public enum VitalName {
	Health,
	Energy,
	Mana
}