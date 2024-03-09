using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathScreen : MonoBehaviour
{
	[SerializeField]
	private Image blackout_screen;

	[SerializeField]
	private TextMeshProUGUI text_mesh;

	[SerializeField]
	private Image button_image;

	[SerializeField]
	private TextMeshProUGUI button_text;

	private bool isReady;

	private float fade_alpha;

	private void Awake()
	{
		isReady = false;
		GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().notifyHealthChangesObservers += CheckForDeath;
	}

	private void CheckForDeath(float cur_health)
	{
		if (cur_health <= 0f && !isReady)
		{
			isReady = true;
			blackout_screen.gameObject.SetActive(value: true);
			text_mesh.gameObject.SetActive(value: true);
			button_image.gameObject.SetActive(value: true);
		}
	}

	private void Update()
	{
		if (isReady)
		{
			fade_alpha += 1f * Time.deltaTime;
			blackout_screen.color = new Color(0f, 0f, 0f, Mathf.Clamp(fade_alpha, 0f, 0.75f));
			button_image.color = new Color(188f, 0f, 0f, Mathf.Clamp(fade_alpha, 0f, 0.75f));
			text_mesh.color = new Color(188f, 0f, 0f, fade_alpha);
			button_text.color = new Color(0f, 0f, 0f, fade_alpha);
		}
	}
}
