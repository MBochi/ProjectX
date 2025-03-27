using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private KnockBack kb;
    private ImpactFlash flash;
    private SpriteRenderer spriteRenderer;
    private Stats stats;
    private float flashDuration = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kb = GetComponent<KnockBack>();
        stats = GetComponent<Stats>();
        flash = GetComponent<ImpactFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public void OnHit(int damage, Vector2 direction, Vector2 knockback) 
    {
        kb.CallKnockBack(direction, knockback, 0f);
        flash.Flash(spriteRenderer, flashDuration, Color.red);
        TakeDamage(damage);
    }

    public void OnHit(int damage) 
    { 
    }
    public void OnObjectDestroyed() 
    { 
    }
    private void TakeDamage(int damage)
    {
        stats.SetCurrentHealth(stats.GetCurrentHealth() - damage);

        if (stats.GetCurrentHealth() <= 0) 
        {
            GameObject.Destroy(gameObject);
            Debug.Log("GameObject destroyed: " + gameObject.name);
        }
    }

}
