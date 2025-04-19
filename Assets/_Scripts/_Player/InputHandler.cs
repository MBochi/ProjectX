using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public bool isCollecting = false;
    public bool isAttacking = false;
    public bool isHealing = false;
    public bool openInventory = false;
    private TextMeshProUGUI coinCounterText;
    private TextMeshProUGUI potionCounterText;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private PlayerInput playerInput;
    private Stats playerStats;

    private StaticInventory staticInventory;
    private InventoryManager inventoryManager;

    [SerializeField] private GameObject collectable;

    private void Awake()
    {
        _mainCamera = Camera.main;
        coinCounterText = GameObject.Find("CoinCounterText").GetComponent<TextMeshProUGUI>();
        potionCounterText = GameObject.Find("PotionCounterText").GetComponent<TextMeshProUGUI>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        staticInventory = GameObject.Find("Canvas").GetComponent<StaticInventory>();
        try
        {
            inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        }
        catch 
        {
            Debug.LogWarning("No GameObject 'InventoryManager' in scene found");
        }
    }

    private void Start()
    {
        potionCounterText.text = staticInventory.healthPotionAmount.ToString();
    }
    void Update()
    {
        if (isCollecting) 
        {
            Collect(collectable);
        }

        if (isAttacking)
        {
            playerController.Attack();
        }

        if (SceneManager.GetSceneByName("Gameplay").isLoaded)
        {
            if (isHealing)
            {
                if (staticInventory.healthPotionAmount > 0 && playerStats.currentHealth != playerStats.maxHealth)
                {
                    playerHealth.Heal(staticInventory.healAmount);
                    staticInventory.healthPotionAmount--;
                    potionCounterText.text = staticInventory.healthPotionAmount.ToString();
                }
            }
        }

        if (openInventory)
        {
            inventoryManager.OpenAndCloseInventory(openInventory);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) isAttacking = true;
        else if (context.canceled) isAttacking = false;
    }

    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.started) isCollecting = true;
        else if (context.canceled) isCollecting = false;
    }

    public void Collect(GameObject collectable)
    {
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        if (rayHit.collider.gameObject.name.Contains(collectable.name))
        {
            staticInventory.coinAmount++;
            coinCounterText.text = staticInventory.coinAmount.ToString();
            Destroy(rayHit.collider.gameObject);
        }
    }

    public void OnHeal(InputAction.CallbackContext context)
    {
        if (context.started) isHealing = true;
        else if (context.canceled) isHealing = false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started && Input.GetButtonDown("Inventory")) openInventory = true;
        else if (context.canceled) openInventory = false;
    }
}
