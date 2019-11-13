using UnityEngine;

public interface IInputState
{
	Vector2 Move { get; set; }
	Vector2 Aim { get; set; }
	bool Act { get; set; }
}

public class InputState : IInputState
{
	public Vector2 Move { get; set; }
	public Vector2 Aim { get; set; }
	public bool Act { get; set; }
}
