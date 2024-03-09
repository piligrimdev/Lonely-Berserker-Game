using UnityEngine;
using UnityEngine.AI;

public class AudioController : MonoBehaviour
{
	private NavMeshAgent agent;

	private AudioManager audio_mg;

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		agent = GetComponentInChildren<NavMeshAgent>();
		audio_mg = Object.FindObjectOfType<AudioManager>();
	}

	private void Update()
	{
		if (!(agent.desiredVelocity.magnitude > 1f) || !anim.GetBool("OnGround"))
		{
			return;
		}
		int num = Random.Range(1, 3);
		for (int i = 0; i < 3; i++)
		{
			if (audio_mg.isSoundPlaying("Footstep_" + num))
			{
				return;
			}
		}
		audio_mg.Play("Footstep_" + num);
	}
}
