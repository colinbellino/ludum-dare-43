using UnityEngine;

public abstract class Notifications : MonoBehaviour
{
	// FIXME: Remove the need for this by reading InputState
	public const string OnSetFireInputNotification = "AI.SetFireInputNotification";
	public const string OnSetMoveInputNotification = "AI.SetMoveInputNotification";
}
