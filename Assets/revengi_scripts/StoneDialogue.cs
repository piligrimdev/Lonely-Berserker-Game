using System.Collections.Generic;
using UnityEngine;

public class StoneDialogue : MonoBehaviour, IInteractable
{
	[SerializeField]
	private List<string> lines;

	[SerializeField]
	private float interact_radius;

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
			dialogue_printer.SetDialogue(lines);
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
