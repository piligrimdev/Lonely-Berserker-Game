using TMPro;
using UnityEngine;

public class StatsChecker : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI killed_enemies_text;

	[SerializeField]
	private TextMeshProUGUI time_passed_text;

	private void Awake()
	{
		StatsManager statsManager = Object.FindObjectOfType<StatsManager>();
		killed_enemies_text.text = statsManager.kill_stat();
		time_passed_text.text = statsManager.time_stat();
	}
}
