using UnityEngine;

public class SpeedSacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		var stats = GameObject.Find("Player")?.GetComponent<PlayerFacade>()?.Stats;
		if (stats != null)
		{
			stats[StatTypes.MoveSpeed] /= 2;
		}
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}

public interface ISacrifice
{
	void OnApply();
	void OnRemove();
}
