using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public float attackRadius = 10f;
    public Transform attackPoint;
    public bool showGizmo = false;
  
    
    void Start()
    {

    }

    
    void Update()
    {

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (showGizmo)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius / 10);
        }
        
    }
}
