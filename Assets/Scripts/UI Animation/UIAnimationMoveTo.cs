using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIAnimationMoveTo : UIAnimationBase
{
    [SerializeField] private RectTransform rectTrans;
    [SerializeField] private Vector2 startAnchorPos;

    [SerializeField] private bool endPosWithOffset;

    [SerializeField, ShowIf(nameof(endPosWithOffset))] private Vector2 offsetToEnd;
    [SerializeField, ShowIf(nameof(NotUseOffsetForEndPos))] private Vector2 endAnchorPos;
    
    [SerializeField] private EasingType animEaseType = EasingType.OutQuad;
    [SerializeField, ShowIf(nameof(UseAnimCurve))] private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool NotUseOffsetForEndPos => !endPosWithOffset;
    private bool UseAnimCurve => animEaseType == EasingType.Custom;
    
    public override void PlayAnim()
    {
        base.PlayAnim();
        rectTrans.DOKill();
        rectTrans.anchoredPosition = startAnchorPos;
        
        var targetPos = endPosWithOffset ? (startAnchorPos + offsetToEnd) : endAnchorPos;
        var tween = rectTrans.DOAnchorPos(targetPos, animTime).SetDelay(delayAnimTime)
            .SetUpdate(unscaleTime).OnComplete(OnAnimComplete);
        
        if (UseAnimCurve)
            tween.SetEase(animCurve);
        else
            tween.SetEase((Ease)animEaseType);
    }
    
    private void Reset()
    {
        rectTrans = GetComponent<RectTransform>();
    }

#if UNITY_EDITOR
    [Button]
    public void GetStartAnchorPos()
    {
        startAnchorPos = rectTrans.anchoredPosition;
    }
    
    [Button]
    public void GetEndAnchorPos()
    {
        endAnchorPos = rectTrans.anchoredPosition;
    }
#endif
}