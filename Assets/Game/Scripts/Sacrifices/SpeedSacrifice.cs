using UnityEngine;

public class SpeedSacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		var player = GameObject.Find("Player")?.GetComponent<PlayerFacade>();
		if (player)
		{
			player.Stats.MoveSpeed.Current /= 2;
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
