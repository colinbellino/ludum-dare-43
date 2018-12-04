using UnityEngine;
using Zenject;

public class RandomAudioClip : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip[] audioClips;

	private GameManager gameManager;

	[Inject]
	public void Construct(GameManager gameManager)
	{
		this.gameManager = gameManager;
	}

	public void PlayRandomSound()
	{
		if (!audioSource)
		{
			Debug.LogWarning("Missing audio source.");
			return;
		}

		var clipToPlay = audioClips[Random.Range(0, audioClips.Length)];
		audioSource.PlayAudioClipAtPoint(clipToPlay);
	}
}
