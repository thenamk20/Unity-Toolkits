using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIAnimationFadeAlpha : UIAnimationBase
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float startAlpha = 0f;
    [SerializeField] private float endAlpha = 1f;
    [SerializeField] private EasingType animEaseType = EasingType.OutQuad;
    
    [SerializeField, ShowIf(nameof(UseAnimCurve))] private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    private bool UseAnimCurve => animEaseType == EasingType.Custom;

    public override void PlayAnim()
    {
        base.PlayAnim();
        canvasGroup.DOKill();
        canvasGroup.alpha = startAlpha;
        
        var tween = canvasGroup.DOFade(endAlpha, animTime)
            .SetDelay(delayAnimTime).SetUpdate(unscaleTime).OnComplete(OnAnimComplete);

        if (UseAnimCurve)
            tween.SetEase(animCurve);
        else
            tween.SetEase((Ease)animEaseType);
    }
    
    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
}