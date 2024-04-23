using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using TMPro;
using DG.Tweening;

public class LevelUi : MonoBehaviour
{
    [SerializeField] Transform icon;

    [SerializeField] Image bar;

    [SerializeField] TMP_Text text;

    [Inject] private GameManager gameManager;

    private Tween shakeTween;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        gameManager.levelValue.Subscribe(value => 
        {
            bar.fillAmount = value / gameManager.levelMaxValue.Value;

            text.text = value.ToString("0") + "/" + gameManager.levelMaxValue.Value.ToString();

            if (shakeTween == null)
            {
                shakeTween = icon.DOShakeScale(.1f, .25f, 1).OnComplete(() =>
                {
                    shakeTween = null;
                });
            }
        });
    }
} 
