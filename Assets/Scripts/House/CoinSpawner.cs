using System;
using UnityEngine;
using Zenject;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int coinValue;

    [Inject] private DiContainer container;

    [Inject] private GameManager gameManager;

    [SerializeField] private float timer = 5f;

    private float startTimer;

    private void Start()
    {
        startTimer = timer;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = startTimer;

                Spawn();
            }
        }
    }

    private void Spawn()
    {
        var createPos = new Vector3(transform.position.x, 1f, transform.position.z);

        var coinUi = container.InstantiatePrefab(gameManager.spawnCoinUi, createPos, Quaternion.identity, null);

        coinUi.TryGetComponent(out SpawnUi ui);

        ui.Spawn(coinValue);
    }
}
