using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
	public delegate void OnBlockUse();

	[SerializeField]
	private Weapon current_weapon;

	[SerializeField]
	private int current_damage = 10;

	[SerializeField]
	private float attack_success_chance = 100f;

	[SerializeField]
	private float attack_radius;

	[SerializeField]
	private string original_attack_anim_clip = "2Hand-Sword-Attack1";

	private GameObject current_weapon_object;

	[SerializeField]
	private int enemy_layer = 7;

	[SerializeField]
	public float block_cooldown = 0.5f;

	private float last_block_time;

	private GameObject current_target;

	private Animator anim;

	private AudioManager audio_mg;

	private PlayerHealth p_health;

	public event OnBlockUse notifyBlockUseObservers;

	private void Start()
	{
		anim = GetComponent<Animator>();
		p_health = GetComponent<PlayerHealth>();
		audio_mg = Object.FindObjectOfType<AudioManager>();
		Camera.main.GetComponent<CameraRaycaster>().notifyMouseClickObservers += OnEnemyClick;
		ChangeWeapon();
	}



	private void ChangeWeapon()
	{
		if (current_weapon_object != null)
		{
			Object.Destroy(current_weapon_object);
		}
		if (current_weapon != null)
		{
			current_weapon_object = Object.Instantiate(current_weapon.GetWeaponPrefab(), RequestWeaponSocket().transform);
			Transform grip = current_weapon.GetGrip();
			current_weapon_object.transform.localPosition = grip.localPosition;
			current_weapon_object.transform.localRotation = grip.localRotation;
			current_damage = current_weapon.GetDamage();
		}
	}

	public void ChangeWeapon(Weapon new_weapon)
	{
		if (current_weapon_object != null)
		{
			Object.Destroy(current_weapon_object);
		}

		current_weapon = new_weapon;
		current_weapon_object = Object.Instantiate(current_weapon.GetWeaponPrefab(), RequestWeaponSocket().transform);

		Transform grip = current_weapon.GetGrip();
		current_weapon_object.transform.localPosition = grip.localPosition;
		current_weapon_object.transform.localRotation = grip.localRotation;

		current_damage = current_weapon.GetDamage();

		AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
		List<KeyValuePair<AnimationClip, AnimationClip>> list = new List<KeyValuePair<AnimationClip, AnimationClip>>();
		AnimationClip[] animationClips = animatorOverrideController.animationClips;

		foreach (AnimationClip animationClip in animationClips)
		{
			if (animationClip.name == original_attack_anim_clip)
			{
				AnimationClip animationClip2 = current_weapon.GetAnimationClip();
				original_attack_anim_clip = animationClip2.name;
				list.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationClip, animationClip2));
			}
			else
			{
				list.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationClip, animationClip));
			}
		}

		animatorOverrideController.ApplyOverrides(list);
		anim.runtimeAnimatorController = animatorOverrideController;
	}

	private GameObject RequestWeaponSocket()
	{
		WeaponSocket[] componentsInChildren = GetComponentsInChildren<WeaponSocket>();
		_ = componentsInChildren.Length;
		return componentsInChildren[0].gameObject;
	}

	private void OnEnemyClick(RaycastHit hit, int layerHit)
	{
		if (layerHit == enemy_layer)
		{
			current_target = hit.collider.gameObject;
		}
		else
		{
			current_target = null;
		}
	}

	private void Update()
	{
		if (current_target != null)
		{
			base.transform.LookAt(current_target.transform);
			if (Vector3.Distance(base.transform.position, current_target.transform.position) <= attack_radius)
			{
				anim.SetBool("IsAttacking", value: true);
			}
		}
		else
		{
			anim.SetBool("IsAttacking", value: false);
		}

		bool @bool = anim.GetBool("IsBlocking");
		if (Input.GetKeyDown(KeyCode.Q) && !@bool && Time.time - last_block_time >= block_cooldown)
		{
			this.notifyBlockUseObservers();
			anim.SetBool("IsBlocking", value: true);
			p_health.isInvincible = true;
			last_block_time = Time.time;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.transform.position, attack_radius);
	}

	public void Hit()
	{
		current_target.GetComponent<EnemyHealth>().TakeDamage(current_damage);
	}

	public void BlockEnded()
	{
		p_health.isInvincible = false;
		anim.SetBool("IsBlocking", value: false);
	}
}
