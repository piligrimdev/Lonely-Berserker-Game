using UnityEngine;

public class BillBoarding : MonoBehaviour
{
	private void LateUpdate()
	{
		base.transform.LookAt(Camera.main.transform.position);
		base.transform.rotation = Quaternion.Euler(0f, base.transform.rotation.eulerAngles.y, 0f);
	}
}
