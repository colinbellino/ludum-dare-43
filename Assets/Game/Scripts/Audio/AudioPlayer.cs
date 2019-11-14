using UnityEngine;

public class AudioPlayer
{
	private readonly AudioSource _audioSource;

	public AudioPlayer(AudioSource audioSource)
	{
		_audioSource = audioSource;
	}

	public void PlayRandom(AudioClip[] clips)
	{
		var clipToPlay = clips[Random.Range(0, clips.Length)];
		_audioSource.PlayAudioClipAtPoint(clipToPlay);
	}
}
