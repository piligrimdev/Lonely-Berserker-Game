using UnityEngine;

[CreateAssetMenu(menuName = "Misc/Sound")]
public class Sound : ScriptableObject
{
	[SerializeField]
	private new string name;

	[SerializeField]
	public bool play_immediately;

	[SerializeField]
	public AudioClip clip;

	[SerializeField]
	private bool isLooping;

	[Range(0f, 1f)]
	[SerializeField]
	private float volume;

	[Range(0f, 1f)]
	[SerializeField]
	private float blend;

	[HideInInspector]
	public AudioSource source;

	public void SetSource()
	{
		if (source != null)
		{
			source.clip = clip;
			source.volume = volume;
			source.spatialBlend = blend;
			source.loop = isLooping;
		}
		else
		{
			Debug.LogError("Audio soruce for " + name + " sound isn't setted");
		}
	}
}
