using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class EnemyHealthBar : MonoBehaviour
{

    RawImage healthBarRawImage;

    EnemyHealth health;

    // Use this for initialization
    void Start()
    {
        health = GetComponentInParent<EnemyHealth>();
        healthBarRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            float xValue = (health.health_fraction / 2f) - 0.5f;
            healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
        catch { }
    }
}
