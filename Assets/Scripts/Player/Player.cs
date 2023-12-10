using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour,IDataPersistence
{
    [SerializeField]public float pushDuration;
    [SerializeField] public float pushForce;
    [SerializeField] public Slider healtBar; // Reference to the player
    [SerializeField] public TextMeshProUGUI deathtext;
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public int currentMoney { get; set; }
    public int range { get; set; }
    public int damage { get; set; }
  
    public static Player playerInstance;
    public bool isInShopArea=false;
    public bool inUpgradeRoom;

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
        inUpgradeRoom = false;
    }
    void Start()
    {
      
        setMaxHealth(maxHealth);
        currentHealth = maxHealth;

        
    }
    private void Update()
    {
       
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
                    currentMoney+=currentCase.caseValue;
                    Inventory.Instance.cases.Remove(currentCase);
                   
                    InventoryController.instance.selectedItemGrid.CleanGridReference(currentCase);
                    // Destroy the game object
                    Destroy(currentCase.gameObject);
                }
            }
        }
     
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
        deathtext.gameObject.SetActive(true);
        StartCoroutine(closeGame(3f));
        Application.Quit();
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

    IEnumerator closeGame(float delay)
    {
        // Restart the level (replace "YourSceneName" with the actual name of your scene)
        
        yield return new WaitForSeconds(delay);

       
    }

    public void LoadData(GameData data)
    {
        maxHealth = data.playerMaxHealth;
        currentHealth = data.playerCurrentHealth;
        damage = data.playerDamage;
        range = data.playerRange;
        currentMoney = data.playerMoney;
        setMaxHealth(maxHealth);
        setCurerntHealth(currentHealth);
    }

    public void SaveData(ref GameData data)
    {
        data.playerMaxHealth = maxHealth;
        data.playerCurrentHealth = currentHealth;
        data.playerDamage = damage;
        data.playerRange = range;
        data.playerMoney = currentMoney;
    }
}
