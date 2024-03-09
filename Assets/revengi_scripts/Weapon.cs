using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapon")]
public class Weapon : ScriptableObject
{
	[SerializeField]
	private Sound attack_sound;

	[SerializeField]
	private GameObject weapon_prefab;

	[SerializeField]
	private AnimationClip weapon_anim;

	[SerializeField]
	private Transform grip;

	[SerializeField]
	private int damage;

	public GameObject GetWeaponPrefab()
	{
		return weapon_prefab;
	}

	public Transform GetGrip()
	{
		return grip;
	}

	public Sound GetAttackSound()
	{
		return attack_sound;
	}

	public int GetDamage()
	{
		return damage;
	}

	public AnimationClip GetAnimationClip()
	{
		return weapon_anim;
	}
}
