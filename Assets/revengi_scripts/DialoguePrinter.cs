using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrinter : MonoBehaviour
{
	[SerializeField]
	private float appear_speed;

	[SerializeField]
	private float lines_cooldown;

	private float og_lines_cooldown;

	private TextMeshProUGUI text_mesh;

	private Image parent_background;

	[SerializeField]
	private List<string> lines;

	private int current_line;

	private bool isLinePrinted;

    private void Awake()
    {
        if (text_mesh == null)
        {
            text_mesh = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
	{
		og_lines_cooldown = lines_cooldown;
		//base.transform.parent.gameObject.SetActive(value: true);
		gameObject.SetActive(true);
		
		/*
		if (parent_background == null)
		{
			parent_background = base.gameObject.transform.parent.gameObject.GetComponent<Image>();
		}
		parent_background.enabled = true;*/
		text_mesh.text = "";
		current_line = 0;
		isLinePrinted = true;
		StartCoroutine(PopChar());
	}

	private void OnDisable()
	{
		disable();
	}

	private void Update()
	{
		if (isLinePrinted && current_line + 1 <= lines.Count)
		{
			text_mesh.text = "";
			if (current_line + 1 != lines.Count)
			{
				current_line++;
				StartCoroutine(PopChar());
			}
			else
			{
				base.enabled = false;
				lines_cooldown = og_lines_cooldown;
			}
		}
	}

	void disable()
	{
        StopAllCoroutines();
        //parent_background.enabled = false;
        text_mesh.text = "";
        //base.transform.parent.gameObject.SetActive(value: false);
        gameObject.SetActive(false);
    }

	private IEnumerator PopChar()
	{
		isLinePrinted = false;
		string text = lines[current_line];
		string[] array = text.Split(" ");
		foreach (string text2 in array)
		{
			if (text2.StartsWith("<color="))
			{
				text_mesh.text += text2.Replace("_", " ");
			}
			else
			{
				string text3 = text2;
				foreach (char c in text3)
				{
					text_mesh.text += c;
					yield return new WaitForSeconds(appear_speed);
				}
			}
			text_mesh.text += " ";
		}
		yield return new WaitForSeconds(lines_cooldown);
		isLinePrinted = true;
	}

	public void SetDialogue(List<string> dialogue_lines)
	{
		disable();
        lines = dialogue_lines;
        gameObject.SetActive(true);
		//base.transform.parent.gameObject.SetActive(value: true);
		base.enabled = true;
	}

	public void SetLinesCooldown(float seconds)
	{
		lines_cooldown = seconds;
	}
}
