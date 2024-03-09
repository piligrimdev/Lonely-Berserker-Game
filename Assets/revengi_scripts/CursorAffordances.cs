using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordances : MonoBehaviour
{
	[SerializeField]
	private int UI_layer = 5;

	[SerializeField]
	private int walkable_layer = 6;

	[SerializeField]
	private int enemy_layer = 7;

	[SerializeField]
	private int interact_layer = 8;

	private CameraRaycaster raycaster;

	[SerializeField]
	private Texture2D UI_cursor;

	[SerializeField]
	private Texture2D walk_cursor;

	[SerializeField]
	private Texture2D attack_cursor;

	[SerializeField]
	private Texture2D interact_cursor;

	[SerializeField]
	private Texture2D unknown_cursor;

	[SerializeField]
	private Vector2 action_point = new Vector2(96f, 96f);

	private void Start()
	{
		raycaster = GetComponent<CameraRaycaster>();
		raycaster.notifyLayerChangeObservers += ChangeCursor;
	}

	private void ChangeCursor(int newLayer)
	{
		if (newLayer == walkable_layer)
		{
			Cursor.SetCursor(walk_cursor, action_point, CursorMode.ForceSoftware);
		}
		else if (newLayer == enemy_layer)
		{
			Cursor.SetCursor(attack_cursor, action_point, CursorMode.ForceSoftware);
		}
		else if (newLayer == interact_layer)
		{
			Cursor.SetCursor(interact_cursor, action_point, CursorMode.ForceSoftware);
		}
		else if (newLayer == UI_layer)
		{
			Cursor.SetCursor(UI_cursor, action_point, CursorMode.ForceSoftware);
		}
		else
		{
			Cursor.SetCursor(unknown_cursor, action_point, CursorMode.ForceSoftware);
		}
	}
}
