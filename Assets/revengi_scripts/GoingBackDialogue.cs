using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingBackDialogue : MonoBehaviour
{
	[SerializeField]
	private List<string> lines;

	private DialoguePrinter dialogue_printer;

	[SerializeField]
	private float cooldown_time = 5f;

	private bool isInteractable = true;

	private void Start()
	{
		dialogue_printer = Object.FindObjectOfType<DialoguePrinter>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && isInteractable)
		{
			dialogue_printer.SetDialogue(lines);
			isInteractable = false;
			StartCoroutine(cooldown());
		}
	}

	private IEnumerator cooldown()
	{
		yield return new WaitForSeconds(cooldown_time);
		isInteractable = true;
	}
}
