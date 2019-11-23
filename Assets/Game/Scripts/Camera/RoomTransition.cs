using UnityEngine;

public class RoomTransition : MonoBehaviour
{
	[SerializeField] private Vector2Int _direction;

	private CameraRig _rig;

	private void Awake()
	{
		_rig = GetComponentInParent<CameraRig>();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			_rig.MoveCameraInDirection(_direction);
		}
	}
}
