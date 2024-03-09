using UnityEngine;

public class Hitmark : MonoBehaviour
{
	[SerializeField]
	private GameObject hitmark_canvas;

	[SerializeField]
	private float dist;

	private CameraRaycaster camera_raycaster;

	private GameObject canvas;

	private void Start()
	{
		Camera.main.GetComponent<CameraRaycaster>().notifyMouseClickObservers += SpawnHitmark;
	}

	private void Update()
	{
		if (canvas != null && Vector3.Distance(canvas.transform.position, base.transform.position) <= dist)
		{
			Object.Destroy(canvas);
		}
	}

	private void SpawnHitmark(RaycastHit hit, int layer)
	{
		if (layer == 6)
		{
			if (canvas != null)
			{
				Object.Destroy(canvas);
			}
			canvas = Object.Instantiate(hitmark_canvas, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
		}
	}
}
