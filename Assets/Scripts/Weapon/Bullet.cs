using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage { get; set; }
    private void Start()
    {
        Player player = GetComponentInParent<Player>();
        Debug.Log(player);
        damage = player.getDamage();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
