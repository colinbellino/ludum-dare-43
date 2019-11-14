using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Grayscale : MonoBehaviour
{
	[SerializeField] private float _speed = 0.5f;

	private ColorAdjustments _colorAdjustments;

	private float _startTime;
	private float _saturation;

	private void OnEnable()
	{
		GetComponent<Volume>().profile.TryGet(out _colorAdjustments);

		this.AddObserver(OnEnableColorSacrifice, ColorSacrifice.OnEnableNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnEnableColorSacrifice, ColorSacrifice.OnEnableNotification);
	}

	private void OnEnableColorSacrifice(object sender, object args)
	{
		_startTime = Time.time;
		_saturation = -100f;
	}

	private void Update()
	{
		float distCovered = (Time.time - _startTime) * _speed;
		float fracJourney = distCovered / 1f;
		if (_startTime >= 0)
		{
			_colorAdjustments.saturation.value = Mathf.Lerp(0, _saturation, fracJourney);
		}
	}
}
