using TMPro;
using UnityEngine;
using Zenject;

public class SacrificePedestal : MonoBehaviour
{
	[SerializeField] private Transform _iconTransform;
	[SerializeField] private TextMeshProUGUI _label;
	[SerializeField] private SpriteRenderer _iconSpriteRenderer;

	private SacrificeData _data;
	private SacrificesManager _manager;
	private readonly float _min = 0f;
	private readonly float _max = 0.02f;

	[Inject]
	private void Construct(SacrificesManager manager)
	{
		_manager = manager;
	}

	private void Update()
	{
		_iconTransform.position = new Vector3(
			transform.position.x,
			transform.position.y + Mathf.PingPong(Time.time * _max, _max - _min) + _min,
			transform.position.z
		);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			_manager.ActivateSacrifice(_data);
		}
	}

	public void SetSacrifice(SacrificeData sacrifice)
	{
		_data = sacrifice;

		_iconSpriteRenderer.sprite = sacrifice.image;
		_label.text = sacrifice.label;
	}
}
