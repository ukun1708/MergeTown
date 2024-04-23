using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    [SerializeField] private TMP_Text coinPerSecondText;

    [SerializeField] private Transform coinIcon;

    [Inject] private GameManager gameManager;

    private float timer;

    private Tween shakeTween;

    private void Start()
    {
        timer = 1;

        Subscribe();
    }

    private void Subscribe()
    {
        gameManager.coin.Subscribe(value =>
        {
            coinText.text = value.ToString();

            if (shakeTween == null)
            {
                shakeTween = coinIcon.transform.DOShakeScale(.1f, .25f, 1).OnComplete(() =>
                {
                    shakeTween = null;
                });
            }
        });
        gameManager.coinPerSecond.Subscribe(value =>
        {
            coinPerSecondText.text = value.ToString();
        });
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 1;

                gameManager.coin.Value += gameManager.coinPerSecond.Value;
            }
        }
    }
}
