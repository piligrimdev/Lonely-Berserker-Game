using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] int xRotatesPerMinute;
    [SerializeField] int yRotatesPerMinute;
    [SerializeField] int zRotatesPerMinute;

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.right, 6 * Time.deltaTime * xRotatesPerMinute);
        transform.RotateAround(transform.position, Vector3.up, 6 * Time.deltaTime * yRotatesPerMinute);
        transform.RotateAround(transform.position, Vector3.forward, 6 * Time.deltaTime * zRotatesPerMinute);
    }
}
