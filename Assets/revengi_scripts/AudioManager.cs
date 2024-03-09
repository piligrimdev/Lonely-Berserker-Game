using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private List<Sound> sounds;

	private void Awake()
	{
		foreach (Sound sound in sounds)
		{
			sound.source = base.gameObject.AddComponent<AudioSource>();
			sound.SetSource();
			if (sound.play_immediately)
			{
				sound.source.playOnAwake = true;
				sound.source.Play();
			}
			else
			{
				sound.source.playOnAwake = false;
			}
		}
	}

	public void Stop(string name)
	{
		Sound sound2 = sounds.Find((Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			sound2.source.Stop();
		}
		else
		{
			Debug.LogError("Sound with name " + name + " does not exist");
		}
	}

	public void Play(string name)
	{
		Sound sound2 = sounds.Find((Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			if (!sound2.source.isPlaying)
			{
				sound2.source.Play();
			}
		}
		else
		{
			Debug.LogError("Sound with name " + name + " does not exist");
		}
	}

	public void Play(string name, float delay)
	{
		Sound sound2 = sounds.Find((Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			if (!sound2.source.isPlaying)
			{
				sound2.source.PlayDelayed(delay);
			}
		}
		else
		{
			Debug.LogError("Sound with name " + name + " does not exist");
		}
	}

	public void AddSound(Sound sound_added)
	{
		if (sounds.Find((Sound sound) => sound.name == sound_added.name) != null)
		{
			Debug.Log("returning");
			return;
		}
		sound_added.source = base.gameObject.AddComponent<AudioSource>();
		sound_added.SetSource();
		if (sound_added.play_immediately)
		{
			sound_added.source.Play();
		}
		sounds.Add(sound_added);
	}

	public bool isSoundPlaying(string name)
	{
		Sound sound2 = sounds.Find((Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			return sound2.source.isPlaying;
		}
		Debug.LogError("Sound with name " + name + " does not exist");
		return false;
	}

	public void DecreaseVolume(string name, float decrease_amount)
	{
		Sound sound2 = sounds.Find((Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			sound2.source.volume -= decrease_amount;
		}
		else
		{
			Debug.LogError("Sound with name " + name + " does not exist");
		}
	}
}
