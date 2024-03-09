using UnityEngine;

public class StatsManager : MonoBehaviour
{
	[SerializeField]
	private int enemies_count;

	[SerializeField]
	private int enemies_killed;

	[SerializeField]
	private float time_passed;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		EnemyHealth[] array = Object.FindObjectsOfType<EnemyHealth>();
		enemies_count = array.Length;
		time_passed = 0f;
	}

	private void IncreaseKilledCounter()
	{
		enemies_killed++;
	}

	public void SetTimePassed(float time)
	{
		time_passed = time;
	}

	public void UpdateKilledEnemies()
	{
		EnemyHealth[] array = Object.FindObjectsOfType<EnemyHealth>();
		enemies_killed = enemies_count - array.Length;
	}

	public string kill_stat()
	{
		return enemies_killed + "/" + enemies_count;
	}

	public string time_stat()
	{
		int num = Mathf.FloorToInt(time_passed / 60f);
		int num2 = Mathf.FloorToInt(time_passed - (float)num * 60f);
		return $"{num:0}:{num2:00}";
	}
}
