using DG.Tweening;
using UnityEngine;

public class StartState : State
{
    private House house;

    public StartState(House house)
    {
        this.house = house;
    }

    public override void Enter()
    {
        base.Enter();

        if (house.currentPlatform != null)
        {
            house.childTransform.DOLocalMoveY(0f, .25f).SetEase(Ease.OutBack);
        }        
    }
    public override void Exit()
    {
        base.Exit();
    }
}
