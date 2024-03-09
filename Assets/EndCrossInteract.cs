using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCrossInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float fading_time = 1f;

    [SerializeField]
    private string teleport_sound_name;

    [SerializeField] Transform teleport_position;

    [SerializeField] private Image fade_image;

    [SerializeField]
    private float interact_radius;

    private AudioManager audio_mg;

    private float menu_fade_alpha;

    private bool started = false;
    private bool hasInteracted = false;

    private GameObject player;
    private Outline outline;
    private CameraRaycaster raycaster;

    private void Start()
    {
        audio_mg = FindObjectOfType<AudioManager>();
        player = GameObject.FindWithTag("Player");
        raycaster = Camera.main.GetComponent<CameraRaycaster>();
        raycaster.notifyLayerChangeObservers += EnableOutline;
        outline = base.gameObject.AddComponent<Outline>();
        outline.enabled = true;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
    }

    public void Interact()
    {
        if (!hasInteracted && Vector3.Distance(base.transform.position, player.transform.position) <= interact_radius)
        {
            started = true;
            outline.enabled = false;
        }
    }

    private void EnableOutline(int layer)
    {
        if (layer == base.gameObject.layer && Vector3.Distance(base.transform.position, player.transform.position) <= interact_radius)
        {
            outline.OutlineColor = Color.green;
        }
        else
        {
            outline.OutlineColor = Color.yellow;
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
                SceneManager.LoadScene(3);
                this.enabled = false;
            }
        }
    }

}
