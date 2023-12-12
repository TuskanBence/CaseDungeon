using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The Player class represents the player character in the game.
/// </summary>
public class Player : MonoBehaviour, IDataPersistence
{
    /// <summary>
    /// The duration of the push effect when colliding with an enemy.
    /// </summary>
    [SerializeField] public float pushDuration;

    /// <summary>
    /// The force applied during the push effect when colliding with an enemy.
    /// </summary>
    [SerializeField] public float pushForce;

    /// <summary>
    /// Reference to the player health bar slider.
    /// </summary>
    [SerializeField] public Slider healtBar;

    /// <summary>
    /// Reference to the death text.
    /// </summary>
    [SerializeField] public TextMeshProUGUI deathtext;

    /// <summary>
    /// The maximum health of the player.
    /// </summary>
    public int maxHealth { get; set; }

    /// <summary>
    /// The current health of the player.
    /// </summary>
    public int currentHealth { get; set; }

    /// <summary>
    /// The amount of money the player currently has.
    /// </summary>
    public int currentMoney { get; set; }

    /// <summary>
    /// The attack range of the player.
    /// </summary>
    public int range { get; set; }

    /// <summary>
    /// The damage dealt by the player.
    /// </summary>
    public int damage { get; set; }

    /// <summary>
    /// Static reference to the player instance.
    /// </summary>
    public static Player playerInstance;

    /// <summary>
    /// Shows whether the player is in a shop area.
    /// </summary>
    public bool isInShopArea = false;

    /// <summary>
    /// Shows whether the player is in an upgrade room.
    /// </summary>
    public bool inUpgradeRoom;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Called before Start
    /// </summary>
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

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    void Start()
    {
        setMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        // Sell all cases when 'G' is pressed in the shop area
        if (Input.GetKeyDown(KeyCode.G) && isInShopArea)
        {
            SellAllCases();
            InventoryController.instance.UpdateMoney(currentMoney);
        }
    }

    /// <summary>
    /// Sells all cases in the player's inventory.
    /// </summary>
    private void SellAllCases()
    {
        for (int i = 0; i < InventoryController.instance.selectedItemGrid.gridSizeWidth; i++)
        {
            for (int j = 0; j < InventoryController.instance.selectedItemGrid.gridSizeHeight; j++)
            {
                Case currentCase = InventoryController.instance.selectedItemGrid.GetCase(i, j);

                if (currentCase != null)
                {
                    currentMoney += currentCase.caseValue;
                    Inventory.Instance.cases.Remove(currentCase);
                    InventoryController.instance.selectedItemGrid.CleanGridReference(currentCase);
                    // Destroy the game object
                    Destroy(currentCase.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Player takes damage.
    /// </summary>
    /// <param name="damageAmount">The amount of damage the player takes.</param>
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        setCurerntHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Sets the healtBars maximum value to the maximum health of the player.
    /// </summary>
    /// <param name="maxhealth">The maximum health value.</param>
    public void setMaxHealth(int maxhealth)
    {
        healtBar.maxValue = maxhealth;
        healtBar.value = maxhealth;
    }

    /// <summary>
    ///  Sets the healtBars current value to the current health of the player.
    /// </summary>
    /// <param name="currenthealth">The current health value.</param>
    public void setCurerntHealth(int currenthealth)
    {
        healtBar.value = currentHealth;
    }

    /// <summary>
    /// Handles the player's death.
    /// </summary>
    void Die()
    {
       
        deathtext.gameObject.SetActive(true);
        StartCoroutine(closeGame(3.0f));   
    }

    /// <summary>
    /// Pushes the player away from an enemy.
    /// </summary>
    /// <param name="enemyAI">Enemy that pushed the player.</param>
    public void PushPlayer(EnemyAI enemyAI)
    {
        Vector2 pushDirection = (this.transform.position - enemyAI.transform.position).normalized;
        Vector2 newPosition = (Vector2)this.transform.position + pushDirection * pushForce;
        StartCoroutine(MovePlayerOverTime(transform, newPosition, pushDuration));
    }

    /// <summary>
    /// Moves the player over time to a target position.
    /// </summary>
    /// <param name="Playertransform">The transform of the player.</param>
    /// <param name="targetPosition">The target position to move towards.</param>
    /// <param name="pushDuration">The duration of the push.</param>
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

    /// <summary>
    /// Gets the attack range of the player.
    /// </summary>
    /// <returns>The attack range value.</returns>
    internal int getRange()
    {
        return range;
    }

    /// <summary>
    /// Gets the damage value of the player.
    /// </summary>
    /// <returns>The damage value.</returns>
    internal int getDamage()
    {
        return damage;
    }

    /// <summary>
    /// Closes the game after a specified delay.
    /// </summary>
    /// <param name="delay">The delay before closing the game.</param>
    /// <returns>IEnumerator for coroutine execution.</returns>
    IEnumerator closeGame(float delay)
    {
        gameObject.transform.position = new Vector3(100,100,0);
        yield return new WaitForSeconds(delay);
       Destroy(gameObject);
        Application.Quit();
    }

    /// <summary>
    /// Loads player data from the provided GameData.
    /// </summary>
    /// <param name="data">The GameData containing player data.</param>
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

    /// <summary>
    /// Saves player data to the provided GameData.
    /// </summary>
    /// <param name="data">The GameData to save player data to.</param>
    public void SaveData(ref GameData data)
    {
        data.playerMaxHealth = maxHealth;
        data.playerCurrentHealth = currentHealth;
        data.playerDamage = damage;
        data.playerRange = range;
        data.playerMoney = currentMoney;
    }
}
