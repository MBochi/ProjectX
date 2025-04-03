using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldToCollect : MonoBehaviour
{
    private bool isHolding = false;

    void Update()
    {
        if (isHolding) { Debug.Log("HOLDING"); }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if(context.started) isHolding = true;
        else if (context.canceled) isHolding = false;
    }
}
