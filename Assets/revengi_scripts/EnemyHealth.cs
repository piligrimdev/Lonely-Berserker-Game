using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
	[SerializeField]
	private float max_helath;

	[SerializeField]
	private float cur_health;

	[SerializeField]
	private float despawn_delay = 1.5f;

	private Animator anim;

	private AudioManager audio_mg;

	public float health_fraction => cur_health / max_helath;

	private void Start()
	{
		anim = GetComponent<Animator>();
		audio_mg = Object.FindObjectOfType<AudioManager>();
	}

	public void TakeDamage(int damageDealed)
	{
		cur_health = Mathf.Clamp(cur_health - (float)damageDealed, 0f, max_helath);
		if (cur_health == 0f)
		{
			anim.SetBool("IsDead", value: true);
			StartCoroutine(WaitToDespawn());
		}
		int num = Random.Range(1, 2);
		for (int i = 1; i < 6; i++)
		{
			if (audio_mg.isSoundPlaying("Enemy_Hit_" + num))
			{
				return;
			}
		}
		audio_mg.Play("Enemy_Hit_" + num);
	}

	private IEnumerator WaitToDespawn()
	{
		yield return new WaitForSeconds(despawn_delay);
		Object.Destroy(base.gameObject);
	}
}
