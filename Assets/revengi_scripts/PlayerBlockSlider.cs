using UnityEngine;
using UnityEngine.UI;

public class PlayerBlockSlider : MonoBehaviour
{
	private bool stopTimer = true;

	private Slider slider;

	private float time_pressed;

	private bool isReady;

	private void Start()
	{
		slider = GetComponent<Slider>();
		GameObject gameObject = GameObject.FindWithTag("Player");
		gameObject.GetComponent<PlayerMelee>().notifyBlockUseObservers += SliderUse;
		slider.maxValue = gameObject.GetComponent<PlayerMelee>().block_cooldown;
		slider.value = slider.maxValue;
	}

	private void Update()
	{
		float num = Time.time - time_pressed;
		if (num >= slider.maxValue)
		{
			stopTimer = true;
		}
		if (!stopTimer)
		{
			GetComponent<Slider>().value = Mathf.Clamp(num, 0f, GetComponent<Slider>().maxValue);
		}
	}

	private void SliderUse()
	{
		time_pressed = Time.time;
		stopTimer = false;
	}
}
