using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{
	private RawImage healthBarRawImage;

	[SerializeField]
	private GameObject player;

	private PlayerHealth health;

	private void Awake()
	{
		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
		}
		health = player.GetComponent<PlayerHealth>();
		health.notifyHealthChangesObservers += ChangeHealth;
		healthBarRawImage = GetComponent<RawImage>();
	}

	private void ChangeHealth(float cur_health)
	{
		float x = 0f - health.health_fraction / 2f - 0.5f;
		healthBarRawImage.uvRect = new Rect(x, 0f, 0.5f, 1f);
	}
}
