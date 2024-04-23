using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class House : MonoBehaviour
{
    public int id;

    public Platform currentPlatform;

    public Platform targetPlatform;

    public Outline outline;

    private StateMachine stateMachine;

    private StartState startState;

    private SelectState selectState;

    public Tween moveY, moveTween;

    public Vector3 startPos, offset;

    public Transform childTransform;

    [HideInInspector] public bool dragged;

    [HideInInspector]
    [Inject] public MergeManager mergeManager;
    [Inject] private VfxManager vfxManager;
    [Inject] private SoundManager soundManager;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        stateMachine = new StateMachine();
        startState = new StartState(this);
        selectState = new SelectState(this);
        stateMachine.Initialize(startState);
    }

    public void Spawn(Platform platform)
    {
        transform.localScale = Vector3.zero;

        dragged = false;

        transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);

        currentPlatform = platform;

        currentPlatform.house = this;

        currentPlatform.box = null;

        vfxManager.PlayVFX(VfxManager.VfxType.houseSpawn, transform.position);

        soundManager.PlaySound(SoundManager.AudioType.click, 1f, Random.Range(.9f, 1.1f));
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void OnMouseDown()
    {
        stateMachine.ChangeState(selectState);
    }

    private void OnMouseUp()
    {
        SetStartState();
    }

    public void SetStartState()
    {
        stateMachine.ChangeState(startState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Platform platform))
        {
            targetPlatform = platform;
        }
    }
    public void BackPosition()
    {
        moveTween = transform.DOMove(startPos, .25f).SetEase(Ease.OutBack);
        moveY.Kill();
        moveY = childTransform.DOLocalMoveY(0f, .25f).SetEase(Ease.OutBack);
    }
}
