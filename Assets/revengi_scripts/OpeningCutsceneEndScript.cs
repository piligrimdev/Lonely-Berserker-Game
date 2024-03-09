using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class OpeningCutsceneEndScript : MonoBehaviour
{
	private AICharacterControl player_movement;

	private void Awake()
	{
		Cursor.visible = true;
		player_movement = GameObject.FindWithTag("Player").gameObject.GetComponent<AICharacterControl>();
		player_movement.enabled = true;
	}
}
