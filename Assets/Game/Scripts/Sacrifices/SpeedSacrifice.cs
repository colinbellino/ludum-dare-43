using UnityEngine;

public class SpeedSacrifice : MonoBehaviour, ISacrifice
{
	private Movement playerMovement;

	public void OnApply()
	{
		var player = GameObject.Find("Player")?.GetComponent<PlayerFacade>();
		if (player)
		{
			playerMovement = player.GetComponent<Movement>();
			playerMovement.Speed.Current *= 0.5f;
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
