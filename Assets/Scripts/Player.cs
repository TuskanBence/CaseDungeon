using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]public float pushDuration;
    [SerializeField] public float pushForce;
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    private void Awake()
    {
        
    }
    void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player has died!");
    }

    public void PushPlayer(EnemyAI enemyAI)
    {
        Vector2 pushDirection = (this.transform.position - enemyAI.transform.position).normalized;
        Vector2 newPosition = (Vector2)this.transform.position + pushDirection * pushForce;
        StartCoroutine(MovePlayerOverTime(transform, newPosition, pushDuration));

    }

    IEnumerator MovePlayerOverTime(Transform Playertransform, Vector2 targetPosition, float pushDuration)
    {
        float elapsed = 0f;
        Vector2 initialPosition = Playertransform.position;

        while (elapsed < pushDuration)
        {
            Playertransform.position = Vector2.Lerp(initialPosition, targetPosition, elapsed / pushDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the player ends up at the exact target position
        Playertransform.position = targetPosition;
    }
}
