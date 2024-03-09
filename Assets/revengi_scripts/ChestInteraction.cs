using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
	[SerializeField]
	private Weapon loot;

	[SerializeField]
	private List<string> lines;

	[SerializeField]
	private float interact_radius;

	private bool hasInteracted;

	private GameObject player;

	private CameraRaycaster raycaster;

	private DialoguePrinter dialogue_printer;

	private AudioManager audio_mg;

	private Outline outline;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		dialogue_printer = Object.FindObjectOfType<DialoguePrinter>();
		audio_mg = Object.FindObjectOfType<AudioManager>();
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
			if(audio_mg)
				audio_mg.Play("item_pickup");
			if (dialogue_printer)
				dialogue_printer.SetDialogue(lines);

			player.GetComponent<PlayerMelee>().ChangeWeapon(loot);
			hasInteracted = true;
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
}
