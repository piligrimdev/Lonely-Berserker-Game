using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvasSpawner : MonoBehaviour
{
    Camera CamLookAt;
    [SerializeField] GameObject canvas_prefab;
    GameObject canvas;
    void Start()
    {
        CamLookAt = Camera.main;
        canvas = Instantiate(canvas_prefab, transform.position, Quaternion.identity, transform);
    }

    void Update()
    {
        canvas.transform.LookAt(CamLookAt.transform);
    }
}
