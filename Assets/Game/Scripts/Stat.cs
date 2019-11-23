using System.Collections.Generic;
using UnityEngine;

public class Stats
{
	private static readonly Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
	private static readonly Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

	public int this [StatTypes s]
	{
		get { return _data[(int) s]; }
		set { SetValue(s, value, true); }
	}

	private readonly int[] _data = new int[(int) StatTypes.Count];

	public void SetValue(StatTypes type, int value, bool allowExceptions)
	{
		int oldValue = this [type];
		if (oldValue == value)
		{
			return;
		}

		if (allowExceptions)
		{
			// Allow exceptions to the rule here
			var exc = new ValueChangeException(oldValue, value);

			// The notification is unique per stat type
			this.PostNotification(WillChangeNotification(type), exc);

			// Did anything modify the value?
			value = Mathf.FloorToInt(exc.GetModifiedValue());

			// Did something nullify the change?
			if (!exc.Toggle || value == oldValue)
			{
				return;
			}
		}

		_data[(int) type] = value;
		this.PostNotification(DidChangeNotification(type), oldValue);
	}

	public static string WillChangeNotification(StatTypes type)
	{
		if (!_willChangeNotifications.ContainsKey(type))
		{
			_willChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type));
		}
		return _willChangeNotifications[type];
	}

	public static string DidChangeNotification(StatTypes type)
	{
		if (!_didChangeNotifications.ContainsKey(type))
		{
			_didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type));
		}
		return _didChangeNotifications[type];
	}
}

public enum StatTypes
{
	MoveSpeed,
	FireRate,
	Health,
	MaxHealth,
	Count,
}
