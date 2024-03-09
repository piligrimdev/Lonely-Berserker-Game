using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextSceneInteract : MonoBehaviour
{
    [SerializeField] int scene_number;
    [SerializeField]
    private float fading_time = 1f;


    [SerializeField] private Image fade_image;

    [SerializeField]
    private float interact_radius;

    private AudioManager audio_mg;

    private float menu_fade_alpha;

    private bool started = false;
    private bool hasInteracted = false;

    private GameObject player;

    private void Start()
    {

        audio_mg = FindObjectOfType<AudioManager>();
        player = GameObject.FindWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.CompareTag("Player"))
        {
            hasInteracted = true;
            audio_mg.gameObject.SetActive(false);
            started = true;
        }
    }

    private void Update()
    {
        if (started)
        {
            menu_fade_alpha += 1f * Time.deltaTime;
            Color n_color = new Color(0f, 0f, 0f, menu_fade_alpha);
            fade_image.color = n_color;

            if (menu_fade_alpha > 1f)
            {
                audio_mg.gameObject.SetActive(false);
                SceneManager.LoadScene(scene_number);
                this.enabled = false;
            }
        }
    }

}
