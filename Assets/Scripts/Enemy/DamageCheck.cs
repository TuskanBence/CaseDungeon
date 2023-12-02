using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

            Player p = collision.gameObject.GetComponent<Player>();
            gameObject.GetComponentInParent<EnemyAI>().DamageCollision(p);
    }
}
