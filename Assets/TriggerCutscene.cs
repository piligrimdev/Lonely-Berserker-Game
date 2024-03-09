using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
	[SerializeField]
	private PlayableDirector cutscene;

	private bool isInteractable = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && isInteractable)
		{
			cutscene.Play();
			isInteractable = false;
		}
	}
}
