using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardTeleport : MonoBehaviour, IInteractable
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
        player = FindObjectOfType<PlayerMelee>().gameObject;
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
            audio_mg.Play(teleport_sound_name);
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
                StartCoroutine(FadeOut());
                this.enabled = false;
            }
        }
    }

    IEnumerator FadeOut()
    {
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().SetTarget(teleport_position);
        player.transform.position = teleport_position.position;
        yield return new WaitForSeconds(1);
        fade_image.color = new Color(0, 0, 0, 0);
        this.enabled = false;
    }
}
