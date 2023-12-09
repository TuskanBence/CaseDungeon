using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]public float pushDuration;
    [SerializeField] public float pushForce;
    [SerializeField] public Slider healtBar; // Reference to the player
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public int currentMoney { get; set; }
    public int range { get; set; }
    public int damage { get; set; }
  
    public static Player playerInstance;
    public bool isInShopArea=false;
    internal bool comingFromShop;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        comingFromShop = false;
    }
    void Start()
    {
        refreshStats();
        setMaxHealth(maxHealth);
        currentHealth = maxHealth;

        
    }
    private void Update()
    {
        refreshStats();
        if (Input.GetKeyDown(KeyCode.G)&& isInShopArea)
        {
            SellAllCases();
            InventoryController.instance.UpdateMoney(currentMoney);
        }
    }
    private void SellAllCases()
    {
        for (int i = 0; i < InventoryController.instance.selectedItemGrid.gridSizeWidth; i++)
        {
            for (int j = 0; j < InventoryController.instance.selectedItemGrid.gridSizeHeight; j++)
            {
                Case currentCase = InventoryController.instance.selectedItemGrid.GetCase(i, j);

                if (currentCase != null)
                {
                    // Remove from the list
                    Inventory.Instance.cases.Remove(currentCase);
                    PlayerStatsManager.instance.addPlayerMoney(currentCase.caseValue);
                    InventoryController.instance.selectedItemGrid.CleanGridReference(currentCase);
                    // Destroy the game object
                    Destroy(currentCase.gameObject);
                }
            }
        }
        refreshStats();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        setCurerntHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void setMaxHealth(int maxhealth)
    {
        healtBar.maxValue = maxhealth;
        healtBar.value = maxhealth;
    }
    public void setCurerntHealth(int currenthealth)
    {
        healtBar.value = currentHealth;
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

    internal int getRange()
    {
        return range;
    }
    internal int getDamage()
    {
        return damage;
    }

    internal void refreshStats()
    {
        maxHealth = PlayerStatsManager.instance.getPlayerMaxHealth();
        range = PlayerStatsManager.instance.getPlayerRange();
        damage = PlayerStatsManager.instance.getPlayerDamage();
        currentMoney = PlayerStatsManager.instance.getPlayerMoney();
        InventoryController.instance.UpdateMoney(currentMoney);
    }

   
}
