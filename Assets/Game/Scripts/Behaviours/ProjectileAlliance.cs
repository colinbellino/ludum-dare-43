using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileAlliance : IInitializable
{
	private readonly List<Sprite> _sprites;
	private readonly Alliances _alliances;
	private readonly int _damage;
	private readonly SpriteRenderer _spriteRenderer;

	public ProjectileAlliance(
		GameSettings settings,
		ProjectileSettings projectileSettings,
		ProjectileFacade projectileFacade
	)
	{
		_sprites = settings.projectilesSprites;
		_alliances = projectileSettings.Alliances;
		_damage = projectileSettings.Stats[StatTypes.Damage];
		_spriteRenderer = projectileFacade.SpriteRenderer;
	}

	public void Initialize()
	{
		if (_alliances == Alliances.Enemy)
		{
			_spriteRenderer.color = new Color(255, 0, 0);
		}

		switch (_damage)
		{
			case 0:
			case 1:
				_spriteRenderer.sprite = _sprites[0];
				break;
			case 2:
				_spriteRenderer.sprite = _sprites[1];
				break;
			default:
				_spriteRenderer.sprite = _sprites[2];
				break;
		}
	}
}
