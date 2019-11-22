/*

	Player:
	- Health: 3
	- Damage: 1
	- Speed: 2
	- Knowledge: true
	- Vision: true
	- Brains: [PlayerControl]

	Skull:
	- Health: 1
	- Damage: 1
	- Speed: 0
	- Brains: [ShootTowardsTarget]

	Muncher:
	- Health: 1
	- Damage: 1
	- Speed: 1
	- Brains: [MoveTowardsTarget]

	---------------------

	Entity
	- Brains
	- Health
	- Damage
	- Speed

*/

using System.Collections.Generic;
using UnityEngine;

namespace Pouet
{
	public class Entity : MonoBehaviour
	{
		// Systems
		private readonly IHealthHandler _healthHandler;
		private readonly IMoveHandler _moveHandler;
		private readonly IShootHandler _shootHandler;
		private readonly IBrainHandler _brainHandler;

		// Data
		public IHealthData Health { get; private set; }
		public IDamageData Damage { get; private set; }
		public ISpeedData Speed { get; private set; }

		public void Update()
		{
			_brainHandler.UpdateInputs();
			_moveHandler.Move(speed: 2);
			_shootHandler.Fire(damage: 2);
		}

		private void OnCollision(Entity other)
		{
			if (other.Damage != null)
			{
				_healthHandler.TakeDamage(other.Damage.Current);
			}
		}
	}

	public interface IStatData
	{
		int Max { get; set; }
		int Current { get; set; }
	}
	public interface IHealthData : IStatData { }
	public interface IDamageData : IStatData { }
	public interface ISpeedData : IStatData { }

	public class FeatureManager
	{
		private readonly List<IFeature> _features;

		public void Add(IFeature feature) { }

		public void Remove(IFeature feature) { }
	}

	public interface IFeature
	{
		void AddFeature();
		void RemoveFeature();
	}

	public class HealthManager : IHealthHandler, IFeature
	{
		private readonly IHealthData _health;

		public void Init()
		{
			_health.Max = 3;
			_health.Current = 3;
		}

		public void TakeDamage(int amount)
		{
			_health.Current -= amount;
		}

		public void AddFeature()
		{
			_health.Max += 1;
			_health.Current += 1;
		}

		public void RemoveFeature()
		{
			_health.Max -= 1;
			_health.Current -= 1;
		}
	}

	public interface IShootHandler
	{
		void Fire(int damage);
	}

	public interface IBrainHandler
	{
		void UpdateInputs();
	}

	public interface IHealthHandler
	{
		void TakeDamage(int amount);
	}

	public interface IMoveHandler
	{
		void HandleInputs(Vector2 move);
		void Move(int speed);
	}

	public interface IHealth2
	{
		void TakeDamage(int amount);
	}

	public class PlayerInputState
	{
		public Vector2 Move;
		public Vector2 Aim;
	}
}
