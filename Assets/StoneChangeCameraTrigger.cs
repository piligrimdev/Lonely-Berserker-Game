using UnityEngine;

public class StoneChangeCameraTrigger : MonoBehaviour
{
	[SerializeField]
	private GameObject v_camera;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			v_camera.SetActive(value: true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			v_camera.SetActive(value: false);
		}
	}
}
