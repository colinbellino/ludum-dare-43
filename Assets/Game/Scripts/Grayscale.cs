using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Grayscale : MonoBehaviour
{
	[SerializeField] private float _speed = 0.7f;

	private ColorAdjustments _colorAdjustments;

	private float startTime;

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
		startTime = Time.time;
	}

	private void Update()
	{
		float distCovered = (Time.time - startTime) * _speed;
		float fracJourney = distCovered / 1f;
		if (startTime > 0)
		{
			_colorAdjustments.saturation.value = Mathf.Lerp(-100f, 0, fracJourney);
		}
	}
}
