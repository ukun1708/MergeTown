using DG.Tweening;
using UnityEngine;
using Zenject;

public class SelectState : State
{
    private House house;

    public SelectState(House house)
    {
        this.house = house;
    }

    public override void Enter()
    {
        base.Enter();

        house.moveTween.Kill();
        house.startPos = house.transform.position;
        house.moveY.Kill();
        house.moveY = house.childTransform.DOLocalMoveY(.5f, .25f).SetEase(Ease.OutBack);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (var raycastHit in raycastHits)
        {
            if (raycastHit.collider.name == "Ground")
            {
                house.offset = house.transform.position - raycastHit.point;
            }
        }

        house.outline.enabled = true;

        house.dragged = true;
    }
    public override void Exit()
    {
        base.Exit();

        house.dragged = false;

        house.outline.enabled = false;

        if (house.targetPlatform == house.currentPlatform)
        {
            house.BackPosition();
        }
        else
        {
            if (house.targetPlatform.box == null)
            {
                if (house.targetPlatform.house != null)
                {
                    house.mergeManager.CheckHouses(house.currentPlatform, house.targetPlatform);
                }
                else
                {
                    house.transform.DOMove(house.targetPlatform.transform.position, 0.25f);

                    house.currentPlatform.house = null;

                    house.currentPlatform = house.targetPlatform;

                    house.currentPlatform.house = house;

                    house.startPos = house.currentPlatform.transform.position;
                    house.moveY = house.childTransform.DOLocalMoveY(0f, .25f).SetEase(Ease.OutBack);
                }
            }
            else
            {
                house.BackPosition();
            }
        }
    }
    public override void Update()
    {
        base.Update();
        Dragged();
    }
    void Dragged()
    {
        if (house.dragged == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] raycastHits = Physics.RaycastAll(ray, Mathf.Infinity);

            foreach (var raycastHit in raycastHits)
            {
                if (raycastHit.collider.name == "Ground")
                {
                    Vector3 pos = raycastHit.point + house.offset;
                    house.transform.position = new Vector3(pos.x, house.transform.position.y, pos.z);
                }
            }
        }
    }
}

