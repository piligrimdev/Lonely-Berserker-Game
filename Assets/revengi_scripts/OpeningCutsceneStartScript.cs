using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class OpeningCutsceneStartScript : MonoBehaviour
{
	[SerializeField]
	private List<string> lines;

	[SerializeField]
	private float lines_cooldown;

	private DialoguePrinter dialogue_printer;

	private AICharacterControl player_movement;

	private void Awake()
	{
		Cursor.visible = false;
		dialogue_printer = Object.FindObjectOfType<DialoguePrinter>();
		player_movement = GameObject.FindWithTag("Player").gameObject.GetComponent<AICharacterControl>();
		dialogue_printer.SetLinesCooldown(lines_cooldown);
		dialogue_printer.SetDialogue(lines);
		player_movement.enabled = false;
	}
}
