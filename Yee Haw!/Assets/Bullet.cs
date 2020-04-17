using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
 
    void OnTriggerEnter2D (Collider2D HitInfo)
    {
        Enemy enemy = HitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  
		}
        Destroy(gameObject);
	}
}
