using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Grayscale : MonoBehaviour
{
	[SerializeField] private ColorAdjustments _colorAdjustments;
	[SerializeField] private float _speed = 0.7f;

	private float startTime;

	private void OnEnable()
	{
		this.AddObserver(OnEnableColorSacrifice, ColorSacrifice.OnEnableNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnEnableColorSacrifice, ColorSacrifice.OnEnableNotification);
	}

	private void OnEnableColorSacrifice(object sender, object args)
	{
		startTime = Time.time;
	}

	private void Update()
	{
		float distCovered = (Time.time - startTime) * _speed;
		float fracJourney = distCovered / 1f;
		if (startTime > 0)
		{
			_colorAdjustments.saturation.value = Mathf.Lerp(0f, 1f, fracJourney);
		}
	}
}
