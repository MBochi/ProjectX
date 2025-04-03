using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private float knockBackForceTime = 0.2f;
    private float knockBackTime = 1f;
    private float hitDirectionForce = 1f;
    private float constForce = 1f;
    private float knockBackjumpForce;

    private float upperPowerThreshold = 1.5f;
    private float lowerPowerThreshold = 0.75f;

    private Rigidbody2D rb;
    private Coroutine knockBackCoroutine;
    public bool IsBeingKnockedBack { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        IsBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockBackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;
        knockBackjumpForce = Random.Range(lowerPowerThreshold, upperPowerThreshold);

        float _elapsedTime = 0f;
        while (_elapsedTime < knockBackForceTime)
        {
            _elapsedTime += Time.deltaTime;

            _knockBackForce = _hitForce + _constantForce;

            if (inputDirection != 0)
            {
                _combinedForce = _knockBackForce + new Vector2(inputDirection, 0f);
            }
            else
            {
                _combinedForce = _knockBackForce;
            }

            Vector2 _jumpForce = Vector2.up * knockBackjumpForce;
            rb.AddForce(new Vector2(_combinedForce.x, _jumpForce.y), ForceMode2D.Impulse);

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(knockBackTime);
        IsBeingKnockedBack = false;
    }

    public void CallKnockBack(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockBackCoroutine = StartCoroutine(KnockBackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
