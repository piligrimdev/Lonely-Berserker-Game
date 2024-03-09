using UnityEngine;
using UnityEngine.AI;

public class BridgeFallScript : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer renderer;

	[SerializeField]
	private NavMeshObstacle obstacle;

	[SerializeField]
	private string fall_sound;

	private bool isInteractable = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && isInteractable)
		{
			Object.FindObjectOfType<AudioManager>().Play(fall_sound);
			renderer.enabled = false;
			obstacle.enabled = true;
			isInteractable = false;
		}
	}
}
