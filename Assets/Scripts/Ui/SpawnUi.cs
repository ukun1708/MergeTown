using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class SpawnUi : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [Inject] private GameManager gameManager;
    public void Spawn(int value)
    {
        gameManager.coin.Value += value;

        transform.localScale = Vector3.zero;
        text.text = value.ToString();

        transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack);
        transform.DOMoveY(transform.position.y + 1f, .5f).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, .25f).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
