using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platforms;

    public List<Platform> platformList = new();

    public House[] houses;

    [SerializeField] public Box box;

    public IntReactiveProperty coin = new();

    public IntReactiveProperty coinPerSecond = new();

    public FloatReactiveProperty levelValue = new();

    public FloatReactiveProperty levelMaxValue = new();

    public SpawnUi spawnCoinUi;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        StartAnimation();
    }

    private async void StartAnimation()
    {
        for (int i = 0; i < platforms.childCount; i++)
        {
            platforms.GetChild(i).transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);

            await UniTask.Delay(25);
        }
    }
}
