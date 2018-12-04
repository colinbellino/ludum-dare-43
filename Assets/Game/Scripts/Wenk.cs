using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Wenk : MonoBehaviour
{
	[SerializeField]
	private InputBehaviour input;

	[SerializeField]
	private float cooldown = 1f;

	[SerializeField]
	private UnityEvent onWenk;

	private float wenkTimestamp;

	private void Update()
	{
		var submitInput = input.GetSubmit();
		if (submitInput)
		{
			if (Time.time > wenkTimestamp)
			{
				onWenk.Invoke();
				wenkTimestamp = Time.time + cooldown;
			}
		}
	}
}
