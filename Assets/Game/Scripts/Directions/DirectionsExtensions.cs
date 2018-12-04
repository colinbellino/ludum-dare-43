using UnityEngine;

public static class DirectionExtensions
{
	public static Directions GetDirection(this Vector2 vector)
	{
		// We need to convert it to a Vector2Int because in some cases we will have
		// really small values in float that unity will confuse with zero.
		var vectorInt = new Vector2Int(
			(int) vector.x,
			(int) vector.y
		);

		if (vectorInt.y > 0f)
		{
			return Directions.North;
		}
		if (vectorInt.x > 0f)
		{
			return Directions.East;
		}
		if (vectorInt.y < 0f)
		{
			return Directions.South;
		}
		return Directions.West;
	}

	public static Directions GetClosestDirection(this Vector2 vector)
	{
		if (vector.y > 0f)
		{
			return Directions.North;
		}
		if (vector.x > 0f)
		{
			return Directions.East;
		}
		if (vector.y < 0f)
		{
			return Directions.South;
		}
		return Directions.West;
	}

	public static Vector3 ToEuler(this Directions direction)
	{
		return new Vector3(0f, (int) direction * 90f, 0f);
	}

	public static Vector2 GetNormal(this Directions direction)
	{
		switch (direction)
		{
			case Directions.North:
				return new Vector2(0f, 1f);
			case Directions.East:
				return new Vector2(1f, 0f);
			case Directions.South:
				return new Vector2(0f, -1f);
			default: // Directions.West:
				return new Vector2(-1f, 0f);
		}
	}
}
