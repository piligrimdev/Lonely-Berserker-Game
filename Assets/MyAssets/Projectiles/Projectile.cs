using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public int damage = 10;
    [SerializeField] public int speed = 10;


    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.gameObject.GetComponent(typeof(IDamagable));
        if(damagable)
        {
            (damagable as IDamagable).TakeDamage(damage);
        }
        // TODO: мб при прохождении некоторых коллайдеров он не должен разбиваться
        Destroy(gameObject);
    }
}
