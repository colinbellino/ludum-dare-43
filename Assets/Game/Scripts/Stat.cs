using UnityEngine;

public class Stat
{
	private int _current;

	public Stats Name { get; }
	public int Current
	{
		get => _current;
		set => SetValue(value, true);
	}

	public Stat(Stats name, int value)
	{
		Name = name;
		_current = value;
	}

	private void SetValue(int value, bool allowExceptions)
	{
		var oldValue = _current;
		var newValue = value;

		if (oldValue == value)
		{
			return;
		}

		if (allowExceptions)
		{
			var exc = new ValueChangeException(oldValue, value);
			this.PostNotification(WillChangeNotification(Name), exc);

			newValue = Mathf.FloorToInt(exc.GetModifiedValue());

			if (!exc.Toggle || newValue == oldValue)
			{
				return;
			}
		}

		_current = newValue;
	}

	public string WillChangeNotification(Stats type) => $"Stat.{type}WillChange";
}

public enum Stats
{
	MoveSpeed,
	FireRate,
	Health,
	MaxHealth,
}
