using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputBehaviour : MonoBehaviour
{
	protected Vector2 move;
	protected Vector2 fire;
	protected bool submit;

	public Vector2 GetMove() => move;

	public Vector2 GetFire() => fire;

	public bool GetSubmit() => submit;
}
