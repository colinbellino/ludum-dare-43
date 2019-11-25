using NUnit.Framework;
using Pouet;

namespace Tests
{
	public class ModifiersTests
	{
		private ModifersManager _manager;
		private AddValueModifier _plusOneHealth;
		private MultiplyValueModifier _doubleHealth;

		[SetUp]
		public void SetUp()
		{
			_manager = new ModifersManager();
			_plusOneHealth = new AddValueModifier(1, 1);
			_doubleHealth = new MultiplyValueModifier(0, 2);
		}

		[Test]
		public void GetHealth_WhenNoHealthhModifierAreEquipped_Returns1()
		{
			var health = _manager.GetHealth();

			Assert.AreEqual(health, 1);
		}

		[Test]
		public void GetHealth_When1DoubleHealthIsEquipped_Returns2()
		{
			_manager.Mods.Add(_doubleHealth);

			var health = _manager.GetHealth();

			Assert.AreEqual(health, 2);
		}

		[Test]
		public void GetHealth_When2DoubleHealthAnd1AddHealthAreEquipped_Returns5()
		{
			_manager.Mods.Add(_doubleHealth);
			_manager.Mods.Add(_doubleHealth);
			_manager.Mods.Add(_plusOneHealth);

			var health = _manager.GetHealth();

			Assert.AreEqual(health, 5);
		}
	}
}
