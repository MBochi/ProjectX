using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Stats : MonoBehaviour
{   
    [SerializeField] private StatsSO statsSO;
    private InventoryManager inventoryManager;
    [SerializeField] TMP_Text maxHealthText, minAttackDamageText, maxAttackDamageText, defenseText;

    public int maxHealth, currentHealth, defense, minAttackDamage, maxAttackDamage, attackSpeed, attackRadius, movementSpeed, bounty;
    private void Awake()
    {
        maxHealth = statsSO.maxHealth;
        currentHealth = statsSO.currentHealth;
        defense = statsSO.defense;
        minAttackDamage = statsSO.minAttackDamage;
        maxAttackDamage = statsSO.maxAttackDamage;
        attackSpeed = statsSO.attackSpeed;
        attackRadius = statsSO.attackRadius;
        movementSpeed = statsSO.movementSpeed;
        bounty = statsSO.bounty;
    }

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        if (SceneManager.GetSceneByName("HUB").isLoaded)
            UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {

        if (maxAttackDamage == 0)
        {
            inventoryManager.minAttackDamageTextContainer.SetActive(false);
            inventoryManager.minAttackDamageTextSeperatorContainer.SetActive(false);
        }
        else
        {
            inventoryManager.minAttackDamageTextContainer.SetActive(true);
            inventoryManager.minAttackDamageTextSeperatorContainer.SetActive(true);
        }

        maxHealthText.text = maxHealth.ToString();
        minAttackDamageText.text = minAttackDamage.ToString();
        maxAttackDamageText.text = maxAttackDamage.ToString();
        defenseText.text = defense.ToString();
    }
}


