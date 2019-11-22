using System.Collections.Generic;

public abstract class Modifier
{
	public readonly int SortOrder;

	public Modifier(int sortOrder)
	{
		SortOrder = sortOrder;
	}
}

public abstract class ValueModifier : Modifier
{
	public ValueModifier(int sortOrder) : base(sortOrder) { }

	public abstract float Modify(float fromValue, float toValue);
}

public class AddValueModifier : ValueModifier
{
	public readonly float ToAdd;

	public AddValueModifier(int sortOrder, float toAdd) : base(sortOrder)
	{
		ToAdd = toAdd;
	}

	public override float Modify(float fromValue, float toValue)
	{
		return toValue + ToAdd;
	}
}

public class MultiplyValueModifier : ValueModifier
{
	public readonly float ToMultiply;

	public MultiplyValueModifier(int sortOrder, float toMultiply) : base(sortOrder)
	{
		ToMultiply = toMultiply;
	}

	public override float Modify(float fromValue, float toValue)
	{
		return toValue * ToMultiply;
	}
}

public class BaseException
{
	public bool Toggle { get; private set; }
	public readonly bool DefaultToggle;

	public BaseException(bool defaultToggle)
	{
		DefaultToggle = defaultToggle;
		Toggle = defaultToggle;
	}

	public void FlipToggle()
	{
		Toggle = !DefaultToggle;
	}
}

public class ValueChangeException : BaseException
{
	public readonly float FromValue;
	public readonly float ToValue;
	public float Delta => ToValue - FromValue;
	private List<ValueModifier> _modifiers;

	public ValueChangeException(float fromValue, float toValue) : base(true)
	{
		FromValue = fromValue;
		ToValue = toValue;
	}

	public void AddModifier(ValueModifier modifier)
	{
		if (_modifiers == null)
		{
			_modifiers = new List<ValueModifier>();
		}
		_modifiers.Add(modifier);
	}

	public float GetModifiedValue()
	{
		if (_modifiers == null)
		{
			return ToValue;
		}

		float value = ToValue;
		_modifiers.Sort(Compare);
		for (int i = 0; i < _modifiers.Count; ++i)
		{
			value = _modifiers[i].Modify(FromValue, value);
		}

		return value;
	}

	private int Compare(ValueModifier x, ValueModifier y)
	{
		return x.SortOrder.CompareTo(y.SortOrder);
	}

}
