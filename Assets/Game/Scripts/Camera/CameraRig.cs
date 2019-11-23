using System;
using System.Collections;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
	[SerializeField] private Transform _transitionsContainer;
	[SerializeField] private Camera _camera;

	public static Vector2Int _roomSize = new Vector2Int(16, 9);
	public static Vector3 GridToWorldSpace(Vector2 value) => new Vector3(value.x, value.y, 0f);

	public Action OnCameraMoveStart = delegate { };
	public Action OnCameraMoveEnd = delegate { };

	private IEnumerator _transition;
	private readonly float _speed = 10f;

	private Vector2Int _gridPosition;

	public void MoveCameraInDirection(Vector2Int direction)
	{
		if (_transition != null)
		{
			StopCoroutine(_transition);
		}

		_transition = MoveInDirection(direction);
		StartCoroutine(_transition);
	}

	private IEnumerator MoveInDirection(Vector2Int direction)
	{
		_gridPosition += direction * _roomSize;

		_transitionsContainer.localPosition = GridToWorldSpace(_gridPosition);
		OnCameraMoveStart();

		yield return LerpCameraTo(GridToWorldSpace(_gridPosition));

		OnCameraMoveEnd();
	}

	private IEnumerator LerpCameraTo(Vector3 destination)
	{
		while (Vector3.Distance(_camera.transform.localPosition, destination) > 0.01f)
		{
			_camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, destination, Time.deltaTime * _speed);
			yield return null;
		}
	}
}
