using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

public class Grayscale : MonoBehaviour
{
	[SerializeField]
	private PostProcessVolume volume;

	[SerializeField]
	private float speed = 1.0f;
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
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / 1f;
		if (startTime > 0)
		{

			volume.weight = Mathf.Lerp(0f, 1f, fracJourney);
		}
	}
}
