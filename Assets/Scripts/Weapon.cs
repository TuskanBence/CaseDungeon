using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float Range;
    [SerializeField] public float FireRate;
    float nextTimeToFire = 0f;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject bulett;
    [SerializeField] public Transform shootPoint;
    [SerializeField] public float force;
    bool Detected = false;
    Vector2 direction;
    public LayerMask targetLayer;
    void Update()
    {
       // Vector2 targetPos = target.position;
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Range, targetLayer);
        Debug.DrawRay(transform.position, direction, Color.red);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = collider.transform;
            }
        }
        if (closestEnemy != null)
        {
            direction = (Vector2)closestEnemy.position - (Vector2)transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Range, targetLayer);
            if (hit)
            {
                if (hit.collider.gameObject.layer==10)
                {
                    if (!Detected)
                    {
                        Detected = true;
                    }
                }
            }
            else
            {
                if (Detected)
                {
                    Detected = false;
                }
            }
            if (Detected)
            {

                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    shoot();
                }
            }
        }
    }


    private void shoot()
    {
        GameObject bulletIns = Instantiate(bulett, shootPoint.position, Quaternion.identity);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
