using System;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeItemUI : MonoBehaviour
{
	[SerializeField] public Image _image;
	[SerializeField] public Text _label;

	public void SetSacrifice(SacrificeData data)
	{
		_image.sprite = data.image;

		if (_label)
		{
			_label.text = data.label;
		}
	}
}
