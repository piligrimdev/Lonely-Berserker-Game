using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	private float fading_time = 1f;

	[SerializeField]
	private string menu_track_title;

	private AudioManager audio_mg;

	private Image fade_image;

	private GameObject menu;

	private float menu_fade_alpha;

	private bool started;

	private void Start()
	{
		menu = GameObject.Find("Menu");
		fade_image = GameObject.Find("MenuFade").GetComponent<Image>();
		audio_mg = Object.FindObjectOfType<AudioManager>();
	}

	public void StartGame()
	{
		//menu.SetActive(value: false);
		StartCoroutine(FadeTrack());
		started = true;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private IEnumerator FadeTrack()
	{
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(fading_time / 10f);
			audio_mg.DecreaseVolume(menu_track_title, fading_time / 10f);
		}
		SceneManager.LoadScene(1);
	}

	private void Update()
	{
		if (started)
		{
			menu_fade_alpha += 1f * Time.deltaTime;
			fade_image.color = new Color(0f, 0f, 0f, menu_fade_alpha);
		}
	}
}
