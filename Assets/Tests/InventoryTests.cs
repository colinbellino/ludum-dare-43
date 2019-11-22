using NSubstitute;
using NUnit.Framework;
using Pouet;

namespace Tests
{
	public class InventoryTests
	{
		private(FeatureData, HealthHandler) _healthFeature;

		[SetUp]
		public void SetUp()
		{
			_healthFeature = (new FeatureData { Name = "Health", Cost = 1 }, Substitute.For<HealthHandler>());
		}

		[Test]
		public void AddFeature_WhenOverMaxCapacity_ShouldThrow()
		{
			var inventory = new InventorySystem(0);

			Assert.Throws<InsufficientCostException>(() =>
			{
				inventory.AddFeature(_healthFeature);
			});
		}

		[Test]
		public void AddFeature_WhenUnderMaxCapacity_ShouldEnableTheFeature()
		{
			var inventory = new InventorySystem(1);

			inventory.AddFeature(_healthFeature);

			_healthFeature.Item2.Received().Enable();
		}

		[Test]
		public void RemoveFeature_ShouldDisableTheFeature()
		{
			var inventory = new InventorySystem(1);
			inventory.AddFeature(_healthFeature);

			inventory.RemoveFeature(_healthFeature);

			_healthFeature.Item2.Received().Disable();
		}
	}
}
