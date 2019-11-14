using UnityEngine;

public static class SoundExtensions
{
	// Source: http://answers.unity.com/answers/1037039/view.html
	public static AudioSource PlayAudioClipAtPoint(this AudioSource audioSource, AudioClip clip)
	{
		GameObject instance = new GameObject("TempAudio");
		instance.transform.position = audioSource.transform.position;

		AudioSource tempAudioSource = instance.AddComponent<AudioSource>();
		tempAudioSource.clip = clip;
		tempAudioSource.outputAudioMixerGroup = tempAudioSource.outputAudioMixerGroup;
		tempAudioSource.mute = tempAudioSource.mute;
		tempAudioSource.bypassEffects = tempAudioSource.bypassEffects;
		tempAudioSource.bypassListenerEffects = tempAudioSource.bypassListenerEffects;
		tempAudioSource.bypassReverbZones = tempAudioSource.bypassReverbZones;
		tempAudioSource.playOnAwake = tempAudioSource.playOnAwake;
		tempAudioSource.loop = tempAudioSource.loop;
		tempAudioSource.priority = tempAudioSource.priority;
		tempAudioSource.volume = tempAudioSource.volume;
		tempAudioSource.pitch = tempAudioSource.pitch;
		tempAudioSource.panStereo = tempAudioSource.panStereo;
		tempAudioSource.spatialBlend = tempAudioSource.spatialBlend;
		tempAudioSource.reverbZoneMix = tempAudioSource.reverbZoneMix;
		tempAudioSource.dopplerLevel = tempAudioSource.dopplerLevel;
		tempAudioSource.rolloffMode = tempAudioSource.rolloffMode;
		tempAudioSource.minDistance = tempAudioSource.minDistance;
		tempAudioSource.spread = tempAudioSource.spread;
		tempAudioSource.maxDistance = tempAudioSource.maxDistance;

		tempAudioSource.Play();
		GameObject.Destroy(instance, tempAudioSource.clip.length);

		return tempAudioSource;
	}
}
