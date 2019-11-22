using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pouet
{
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

	public class InventorySystem
	{
		private readonly List < (FeatureData, IFeatureHandler) > _features = new List < (FeatureData, IFeatureHandler) > ();

		private readonly int _totalSpace;
		private int _availableSpace;

		public InventorySystem(int totalSpace)
		{
			_totalSpace = totalSpace;
			_availableSpace = totalSpace;
		}

		public void AddFeature((FeatureData, IFeatureHandler) feature)
		{
			var(data, handler) = feature;
			if (data.Cost > _availableSpace)
			{
				throw new InsufficientCostException();
			}

			_features.Add((data, handler));
			handler.Enable();
			_availableSpace -= data.Cost;
		}

		public void RemoveFeature((FeatureData, IFeatureHandler) feature)
		{
			var(data, handler) = feature;

			_features.Remove((data, handler));
			handler.Disable();
			_availableSpace += data.Cost;
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
