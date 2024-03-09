using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	[SerializeField]
	private int interactable_layer = 8;

	private void Start()
	{
		Camera.main.GetComponent<CameraRaycaster>().notifyMouseClickObservers += OnInteractableClick;
	}

	private void OnInteractableClick(RaycastHit hit, int layerHit)
	{
		if (layerHit == interactable_layer)
		{
			(hit.collider.gameObject.GetComponent(typeof(IInteractable)) as IInteractable).Interact();
		}
	}
}
