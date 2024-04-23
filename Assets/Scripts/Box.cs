using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class Box : MonoBehaviour
{
    public Platform platform;

    [Inject] private DiContainer container;
    [Inject] private GameManager gameManager;
    [Inject] private VfxManager vfxManager;
    [Inject] private SoundManager soundManager;

    private Tween scaleTw, moveTw;

    public void Spawn()
    {
        transform.localScale = Vector3.zero;

        scaleTw = transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack);

        platform.box = this;

        moveTw = transform.DOMove(platform.transform.position, .25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            var fxPos = new Vector3(transform.position.x, .5f, transform.position.z);

            vfxManager.PlayVFX(VfxManager.VfxType.boxSpawn, fxPos);

            soundManager.PlaySound(SoundManager.AudioType.click, 1f, Random.Range(.9f, 1.1f));
        });
    }

    private void OnMouseDown()
    {
        SpawnHouse();
    }

    private void SpawnHouse()
    {
        var house = container.InstantiatePrefab(gameManager.houses[0].gameObject, transform.position, Quaternion.identity, null);

        House _house = house.GetComponent<House>();

        _house.Spawn(platform);    

        scaleTw.Kill();

        moveTw.Kill();

        Destroy(gameObject);
    }
}
