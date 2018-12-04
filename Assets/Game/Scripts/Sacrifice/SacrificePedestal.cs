using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SacrificePedestal : MonoBehaviour
{
	[SerializeField]
	private Transform icon;

	[SerializeField]
	private TextMeshProUGUI label;

	[SerializeField]
	private SpriteRenderer iconSpriteRenderer;

	private Sacrifice sacrifice;
	private float min = 0f;
	private float max = 0.02f;

	private void OnEnable()
	{
		this.AddObserver(OnChooseSacrifice, SacrificesManager.OnChooseSacrificeNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnChooseSacrifice, SacrificesManager.OnChooseSacrificeNotification);
	}

	private void Update()
	{
		icon.position = new Vector3(
			transform.position.x,
			transform.position.y + Mathf.PingPong(Time.time * 0.02f, max - min) + min,
			transform.position.z
		);
	}

	private void OnChooseSacrifice(object sender, object args)
	{
		GameObject.Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			this.PostNotification(SacrificesManager.OnChooseSacrificeNotification, sacrifice);

			GameObject.Destroy(gameObject);
		}
	}

	public void SetSacrifice(Sacrifice sacrifice)
	{
		this.sacrifice = sacrifice;
		iconSpriteRenderer.sprite = sacrifice.image;
		label.text = sacrifice.label;
	}
}
