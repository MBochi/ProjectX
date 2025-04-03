using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private bool isHolding = false;
    private int coinCounter = 0;
    private TextMeshProUGUI _textMeshPro;


    [SerializeField] private GameObject collectable;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _textMeshPro = GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isHolding) 
        {
            Collect(collectable);
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.started) isHolding = true;
        else if (context.canceled) isHolding = false;
    }

    public void Collect(GameObject collectable)
    {
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);

        if (rayHit.collider.gameObject.name.Contains(collectable.name))
        {
            coinCounter++;
            _textMeshPro.text = coinCounter.ToString();
            Destroy(rayHit.collider.gameObject);
        }
    }
}
