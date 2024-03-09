using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(AICharacterControl))]
public class PointMovement : MonoBehaviour
{
	private CameraRaycaster raycaster;

	private AICharacterControl ai_character;

	[SerializeField]
	private const int walkable_layer = 6;

	[SerializeField]
	private const int enemy_layer = 7;

	private GameObject target;

	private Transform m_Cam;

	private Vector3 m_CamForward;

	private Vector3 m_Move;

	private ThirdPersonCharacter character;

	private bool isPointMovemenetOn = true;

	private void Start()
	{
		ai_character = GetComponent<AICharacterControl>();
		character = GetComponent<ThirdPersonCharacter>();
		raycaster = Camera.main.GetComponent<CameraRaycaster>();
		raycaster.notifyMouseClickObservers += SetTarget;
		target = new GameObject();
		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", base.gameObject);
		}
	}

	private void SetTarget(RaycastHit hit, int layer)
	{
		switch (layer)
		{
		case 6:
			target.transform.position = hit.point;
			ai_character.SetTarget(target.transform);
			break;
		case 7:
			ai_character.SetTarget(hit.collider.gameObject.transform);
			break;
		default:
			MonoBehaviour.print("Uknown layer clicked");
			break;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			isPointMovemenetOn = !isPointMovemenetOn;
		}
	}

	private void ControlsMovement()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		if (m_Cam != null)
		{
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1f, 0f, 1f)).normalized;
			m_Move = axis2 * m_CamForward + axis * m_Cam.right;
		}
		else
		{
			m_Move = axis2 * Vector3.forward + axis * Vector3.right;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			m_Move *= 0.5f;
		}
		character.Move(m_Move, crouch: false, jump: false);
	}
}
