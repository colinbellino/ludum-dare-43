using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Pouet
{
	public class InventorySystem
	{
		private readonly List < (FeatureData, IFeatureHandler) > _features = new List < (FeatureData, IFeatureHandler) > ();
		private readonly int _capacity;

		private int AvailableSpace => _capacity - FeaturesCost;
		private int FeaturesCost => _features
			.Select(tupple => tupple.Item1.Cost)
			.DefaultIfEmpty()
			.Aggregate((a, b) => a + b);

		public InventorySystem(int capacity)
		{
			_capacity = capacity;
		}

		public void AddFeature((FeatureData, IFeatureHandler) feature)
		{
			var(data, handler) = feature;
			if (data.Cost > AvailableSpace)
			{
				throw new InsufficientCostException();
			}

			_features.Add((data, handler));
			handler.Enable();
		}

		public void RemoveFeature((FeatureData, IFeatureHandler) feature)
		{
			var(data, handler) = feature;

			_features.Remove((data, handler));
			handler.Disable();
		}
	}

	public interface IFeatureHandler
	{
		void Enable();
		void Disable();
	}

	public class FeatureData
	{
		public string Name;
		public int Cost;
	}

	public class HealthHandler : IFeatureHandler
	{
		public void Disable() { }
		public void Enable() { }
	}

	public class ModifersManager
	{
		public readonly List<ValueModifier> Mods = new List<ValueModifier>();

		public float GetHealth()
		{
			var fromValue = 1f;
			var value = fromValue;

			var sortedMods = Mods.OrderBy(mod => mod.SortOrder);
			foreach (var mod in sortedMods)
			{
				value = mod.Modify(fromValue, value);
			}

			return value;
		}
	}
}

[Serializable]
public class InsufficientCostException : Exception
{
	public InsufficientCostException() { }
	public InsufficientCostException(string message) : base(message) { }
	public InsufficientCostException(string message, Exception innerException) : base(message, innerException) { }
	protected InsufficientCostException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
