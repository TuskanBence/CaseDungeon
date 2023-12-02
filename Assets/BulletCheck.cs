using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet b = collision.gameObject.GetComponent<Bullet>();
        gameObject.GetComponentInParent<EnemyAI>().BulletCollison(b);
    }
}
