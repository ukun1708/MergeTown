using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.ComponentModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonBox : ButtonUi
{
    [SerializeField] private TMP_Text boxCountText;

    [SerializeField] private Image image;

    [SerializeField] private float fillAmountMaxValue;

    [SerializeField] private IntReactiveProperty boxCount = new();

    private FloatReactiveProperty fillAmountCurrentValue = new();

    [Inject] private DiContainer container;
    [Inject] private GameManager gameManager;

    [SerializeField] Transform boxCountIcon;

    private Tween shakeTween;

    public override void Start()
    {
        base.Start();

        fillAmountCurrentValue.Value = fillAmountMaxValue;

        Subscribe();
    }

    private void Subscribe()
    {
        boxCount.Subscribe(value =>
        {
            boxCountText.text = value.ToString();

            if (shakeTween == null)
            {
                shakeTween = boxCountIcon.DOShakeScale(.1f, .1f, 1).OnComplete(() =>
                {
                    shakeTween = null;
                });
            }
        });
        fillAmountCurrentValue.Subscribe(value =>
        {
            image.fillAmount = fillAmountCurrentValue.Value / fillAmountMaxValue;
        });
    }

    private void Update()
    {
        if (fillAmountCurrentValue.Value > 0)
        {
            fillAmountCurrentValue.Value -= Time.deltaTime;

            if (fillAmountCurrentValue.Value <= 0)
            {
                fillAmountCurrentValue.Value = fillAmountMaxValue;

                boxCount.Value++;
            }
        }
    }

    public override void OnClick()
    {
        if (boxCount.Value > 0)
        {
            SpawnBox();
        }
        if (boxCount.Value == 0)
        {
            if (fillAmountCurrentValue.Value > 1)
            {
                fillAmountCurrentValue.Value -= 1f;
            }
        }
    }

    private void SpawnBox()
    {
        for (int i = 0; i < gameManager.platformList.Count; i++)
        {
            if (gameManager.platformList[i].house == null && gameManager.platformList[i].box == null)
            {
                Platform platform = gameManager.platformList[i];

                Vector3 createPos = new Vector3(platform.transform.position.x, 5f, platform.transform.position.z);

                var box = container.InstantiatePrefab(gameManager.box.gameObject, createPos, Quaternion.identity, null);

                Box _box = box.GetComponent<Box>();

                _box.platform = gameManager.platformList[i];

                _box.Spawn();

                boxCount.Value--;

                break;
            }
        }
    }
}
