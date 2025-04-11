using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private bool isCollecting = false;
    private bool isAttacking = false;
    private int coinCounter = 0;
    private TextMeshProUGUI _textMeshPro;
    private PlayerCombat playerCombat;

    [SerializeField] private GameObject collectable;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _textMeshPro = GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>();
        playerCombat = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
    }
    void Update()
    {
        if (isCollecting) 
        {
            Collect(collectable);
        }

        if (isAttacking)
        {
            playerCombat.Attack();
        }
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
            coinCounter++;
            _textMeshPro.text = coinCounter.ToString();
            Destroy(rayHit.collider.gameObject);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) isAttacking = true;
        else if (context.canceled) isAttacking = false;
    }
}
