using System.Collections.Generic;
using UnityEngine;

public class SaveGameTreeInteraction : MonoBehaviour, IInteractable
{
	[SerializeField]
	private int heal_amount;

	[SerializeField]
	private List<string> lines;

	[SerializeField]
	private float interact_radius;

	[SerializeField]
	private GameObject particles;

	private bool hasInteracted;

	private GameObject player;

	private CameraRaycaster raycaster;

	private DialoguePrinter dialogue_printer;

	private Outline outline;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		dialogue_printer = Object.FindObjectOfType<DialoguePrinter>();
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
			if (lines.Count > 0)
			{
				dialogue_printer.SetDialogue(lines);
			}
			player.GetComponent<PlayerHealth>().TakeDamage(-heal_amount);
			hasInteracted = true;
			outline.enabled = false;
			if ((bool)particles)
			{
				Object.Destroy(particles);
			}
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
		if (!hasInteracted && Vector3.Distance(base.transform.position, player.transform.position) <= interact_radius)
		{
			outline.enabled = true;
		}
		else
		{
			outline.enabled = false;
		}
	}
}
