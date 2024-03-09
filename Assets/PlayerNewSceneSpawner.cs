using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewSceneSpawner : MonoBehaviour
{
    [SerializeField] Transform spawn_point;
    
    private void Awake()
    {
        GameObject player = FindObjectOfType<PlayerHealth>().gameObject;
        print("hi");
        player.transform.position = spawn_point.position;
    }
}
