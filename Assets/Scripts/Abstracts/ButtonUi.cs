using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonUi : MonoBehaviour
{
    private Button button;

    private Tween shakeTween;

    private void Awake() => button = GetComponent<Button>();

    public virtual void Start()
    {
        button.onClick.AddListener(() =>
        {
            ClickAnimation();
            OnClick();
        });
    }

    private void ClickAnimation()
    {
        if (shakeTween == null)
        {
            shakeTween = transform.DOShakeScale(.1f, .25f, 1).OnComplete(() =>
            {
                shakeTween = null;
            });
        }
    }

    public virtual void OnClick()
    {
        
    }
}
