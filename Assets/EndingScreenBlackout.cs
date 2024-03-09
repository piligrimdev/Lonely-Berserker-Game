using UnityEngine;
using UnityEngine.UI;

public class EndingScreenBlackout : MonoBehaviour
{
	private bool isReady;

	private Image blackout_screen;

	private float fade_alpha;

	private void Awake()
	{
		blackout_screen = GetComponent<Image>();
		isReady = true;
	}

	private void Update()
	{
		if (isReady)
		{
			fade_alpha += 1f * Time.deltaTime;
			blackout_screen.color = new Color(0f, 0f, 0f, Mathf.Clamp(fade_alpha, 0f, 1f));
		}
	}
}
