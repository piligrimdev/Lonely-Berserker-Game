using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycaster : MonoBehaviour
{
	public delegate void OnCursorLayerChange(int newLayer);

	public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);

	[SerializeField]
	private int[] layerPriorities;

	private float maxRaycastDepth = 100f;

	private int topPriorityLayerLastFrame = -1;

	public event OnCursorLayerChange notifyLayerChangeObservers;

	public event OnClickPriorityLayer notifyMouseClickObservers;

	private void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			NotifyObserersIfLayerChanged(5);
			return;
		}
		RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), maxRaycastDepth);
		RaycastHit? raycastHit = FindTopPriorityHit(raycastHits);
		if (!raycastHit.HasValue)
		{
			NotifyObserersIfLayerChanged(0);
			return;
		}
		int layer = raycastHit.Value.collider.gameObject.layer;
		NotifyObserersIfLayerChanged(layer);
		if (Input.GetMouseButtonDown(0))
		{
			this.notifyMouseClickObservers(raycastHit.Value, layer);
		}
	}

	private void NotifyObserersIfLayerChanged(int newLayer)
	{
		if (newLayer != topPriorityLayerLastFrame)
		{
			topPriorityLayerLastFrame = newLayer;
			this.notifyLayerChangeObservers(newLayer);
		}
	}

	private RaycastHit? FindTopPriorityHit(RaycastHit[] raycastHits)
	{
		int[] array = layerPriorities;
		foreach (int num in array)
		{
			Dictionary<float, RaycastHit> dictionary = new Dictionary<float, RaycastHit>();
			for (int j = 0; j < raycastHits.Length; j++)
			{
				RaycastHit value = raycastHits[j];
				if (value.collider.gameObject.layer == num)
				{
					dictionary.Add(value.distance, value);
				}
			}
			if (dictionary.Count != 0)
			{
				return dictionary[dictionary.Keys.ToList().Min()];
			}
		}
		return null;
	}
}
