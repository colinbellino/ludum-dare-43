using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SacrificeBehaviour : MonoBehaviour
{
	protected virtual void Start()
	{
		StartCoroutine(WaitAndDestroy());
	}

	private IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(1f);

		GameObject.Destroy(gameObject);
	}
}
