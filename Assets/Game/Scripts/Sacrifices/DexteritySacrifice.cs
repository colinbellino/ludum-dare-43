using UnityEngine;

public class DexteritySacrifice : MonoBehaviour, ISacrifice
{
	private EntityStats _stats;

	public void OnApply()
	{
		_stats = GameObject.Find("Player")?.GetComponent<IEntity>()?.Stats;
		if (_stats != null)
		{
			_stats.FireRate.Current /= 2;
		}
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
