using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class EnemyAI : MonoBehaviour
{
	[SerializeField]
	private float chase_radius = 1f;

	[SerializeField]
	private float attack_radius = 1f;

	[SerializeField]
	private float projectile_speed = 5f;

	[SerializeField]
	private int projectile_damage = 10;

	[SerializeField]
	private float shoot_cooldown;

	[SerializeField]
	private float attack_success_chance = 100f;

	private Coroutine shoot_coroutine;

	private bool isAttacking;

	[SerializeField]
	private GameObject projectile_prefab;

	[SerializeField]
	private Vector3 projectile_direction_offset;

	private Transform projectile_socket;

	private AICharacterControl ai_chase_control;

	private Transform target;

	private Animator anim;

	private AudioManager audio_mg;

	private float distance_chase;

	private float distance_attack;

	private void Awake()
	{
		audio_mg = Object.FindObjectOfType<AudioManager>();
		anim = GetComponent<Animator>();
		ai_chase_control = GetComponent<AICharacterControl>();
		target = ai_chase_control.target;
		isAttacking = false;
		projectile_socket = base.transform.Find("Projectile Socket");
		distance_chase = 0f;
		distance_attack = 0f;
	}

	private void Update()
	{
		distance_chase = Vector3.Distance(base.transform.position, target.position);
		distance_attack = Vector3.Distance(base.transform.position, target.position);
		bool @bool = anim.GetBool("IsDead");
		if (distance_attack > attack_radius || @bool)
		{
			isAttacking = false;
			anim.SetBool("IsAttacking", isAttacking);
		}
		else
		{
			base.transform.LookAt(target);
			if (!isAttacking)
			{
				isAttacking = true;
			}
		}
		if (distance_chase <= chase_radius && !@bool)
		{
			ai_chase_control.SetTarget(target);
		}
		else
		{
			ai_chase_control.SetTarget(base.transform);
		}
		anim.SetBool("IsAttacking", isAttacking);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.transform.position, chase_radius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, attack_radius);
	}

	private IEnumerator ShootProjectile(float cooldown)
	{
		while (true)
		{
			yield return new WaitForSeconds(cooldown);
			anim.SetBool("IsAttacking", isAttacking);
			GameObject gameObject = Object.Instantiate(projectile_prefab, projectile_socket.position, Quaternion.identity);
			if (Random.Range(0f, 100f) <= attack_success_chance)
			{
				gameObject.GetComponent<Projectile>().damage = projectile_damage;
			}
			else
			{
				gameObject.GetComponent<Projectile>().damage = 0;
				Debug.Log("Enemy missed!");
			}
			Vector3 normalized = (target.position + projectile_direction_offset - gameObject.transform.position).normalized;
			gameObject.GetComponent<Rigidbody>().velocity = normalized * projectile_speed;
		}
	}

	public void Hit()
	{
		if (Random.Range(0f, 100f) <= attack_success_chance && distance_attack <= attack_radius)
		{
			target.gameObject.GetComponent<PlayerHealth>().TakeDamage(projectile_damage);
		}
		else
		{
			Debug.Log("Enemy missed!");
		}
	}
}
