using UnityEngine;

public class KnowledgeSacrifice : MonoBehaviour, ISacrifice
{
	public const string OnUiDisable = "KnowledgeSacrifice.OnUiDisable";

	public void OnApply()
	{
		this.PostNotification(OnUiDisable, false);
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
