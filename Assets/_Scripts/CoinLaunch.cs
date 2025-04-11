using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLaunch : MonoBehaviour
{
    private Rigidbody2D rb;

    private float upperPowerThreshold = 12f;
    private float lowerPowerThreshold = 0f;
    [SerializeField] private float despawnTime = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        LaunchCoin();
        StartCoroutine(DespawnCoin(despawnTime));
    }

    public void LaunchCoin()
    {
        float randomXForce = Random.Range(lowerPowerThreshold, upperPowerThreshold);
        float randomYForce = Random.Range(lowerPowerThreshold, upperPowerThreshold);

        Vector2 XForce = Vector2.right * randomYForce;
        Vector2 YForce = Vector2.up * randomXForce;

        rb.AddForce(new Vector2(XForce.x, YForce.y), ForceMode2D.Impulse);
    }

    private IEnumerator DespawnCoin(float despawnTime)
    {
        float halfDespawnTime = despawnTime / 2;
        yield return new WaitForSeconds(halfDespawnTime);
        // TODO - Flash Sprite
        yield return new WaitForSeconds(halfDespawnTime);
        Destroy(gameObject);
    }
}
