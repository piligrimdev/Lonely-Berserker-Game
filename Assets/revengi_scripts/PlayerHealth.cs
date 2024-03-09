using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
	public delegate void OnHealthChange(float health);

	[SerializeField]
	private float max_helath;

	[SerializeField]
	private float cur_health;

	private Animator anim;

	private AudioManager audio_mg;

	public bool isInvincible;

	public float health_fraction => cur_health / max_helath;

	public event OnHealthChange notifyHealthChangesObservers;

	private void Start()
	{
		anim = GetComponent<Animator>();
		audio_mg = Object.FindObjectOfType<AudioManager>();
		this.notifyHealthChangesObservers(cur_health);
	}

	public void TakeDamage(int damageDealed)
	{
		if (!isInvincible)
		{
			cur_health = Mathf.Clamp(cur_health - (float)damageDealed, 0f, max_helath);
			if (cur_health == 0f)
			{
				anim.SetBool("IsDead", value: true);
			}

			if (audio_mg)
			{
				int num = Random.Range(1, 3);
				for (int i = 0; i < 3; i++)
				{
					if (audio_mg.isSoundPlaying("Player_Hit_" + num))
					{
						return;
					}
				}
				audio_mg.Play("Player_Hit_" + num);
			}

			this.notifyHealthChangesObservers(cur_health);
		}
		else
		{
			Debug.Log("Damage blocked");
		}
	}
}
