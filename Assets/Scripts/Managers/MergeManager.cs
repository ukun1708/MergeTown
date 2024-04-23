using DG.Tweening;
using UnityEngine;
using Zenject;

public class MergeManager : MonoBehaviour
{
    private Tween tween;

    [Inject] private DiContainer container;
    [Inject] private GameManager gameManager;
    [Inject] private VfxManager vfxManager;
    [Inject] private SoundManager soundManager;

    private float timer, pitch;

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            pitch = 1f;
        }
    }

    public void CheckHouses(Platform currentMergePlatform, Platform targetMergePlatform)
    {
        if (currentMergePlatform.house.id == targetMergePlatform.house.id)
        {
            if (currentMergePlatform.house.id == gameManager.houses.Length - 1)
            {
                currentMergePlatform.house.BackPosition();
            }
            else
            {
                tween.Kill();
                tween = currentMergePlatform.house.transform.DOMove(targetMergePlatform.transform.position, 0.1f).OnComplete(() =>
                {
                    currentMergePlatform.house.transform.DOScale(Vector3.zero, .25f);
                    targetMergePlatform.house.transform.DOScale(Vector3.zero, .25f);

                    Destroy(currentMergePlatform.house.gameObject, .3f);
                    Destroy(targetMergePlatform.house.gameObject, .3f);

                    var newId = (int)currentMergePlatform.house.id + 1;

                    var newHouse = container.InstantiatePrefab(gameManager.houses[newId], targetMergePlatform.transform.position, Quaternion.identity, null);

                    newHouse.transform.localScale = Vector3.zero;
                    
                    newHouse.transform.SetPositionAndRotation(targetMergePlatform.transform.position, Quaternion.identity);
                    
                    newHouse.SetActive(true);
                    
                    newHouse.GetComponent<House>().dragged = false;

                    newHouse.transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack);

                    House currentHouse = newHouse.GetComponent<House>();

                    currentHouse.currentPlatform = targetMergePlatform;

                    targetMergePlatform.house = currentHouse;

                    currentMergePlatform.house = null;

                    vfxManager.PlayVFX(VfxManager.VfxType.merge, currentHouse.transform.position);

                    timer = 2f;

                    pitch += .1f;

                    soundManager.PlaySound(SoundManager.AudioType.click, 1f, pitch);

                    gameManager.coinPerSecond.Value++;

                    gameManager.levelValue.Value += newId;
                });
            }
        }
        else
        {
            var currentHouse = currentMergePlatform.house;
            var targetHouse = targetMergePlatform.house;

            targetMergePlatform.house = currentHouse;
            currentMergePlatform.house = targetHouse;

            currentHouse.currentPlatform = targetMergePlatform;
            targetHouse.currentPlatform = currentMergePlatform;

            currentHouse.transform.DOMove(targetMergePlatform.transform.position, 0.1f);
            targetHouse.transform.DOMove(currentMergePlatform.transform.position, 0.1f).OnComplete(() =>
            {
                currentHouse.SetStartState();
                targetHouse.SetStartState();
            });
        }
    }
}
