using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class UIAnimationFromScale : UIAnimationBase
{
    [SerializeField] protected Transform tweenTarget;
    [SerializeField] private float initScale;
    [SerializeField] private float endScale = 1f;
    [SerializeField] private bool separateAxisAnim;
    
    [ShowIf(nameof(NotSeparateAxis)), SerializeField] private EasingType easeType = EasingType.OutQuad;

    [ShowIf(nameof(UseAnimCurve)), SerializeField] private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [ShowIf(nameof(separateAxisAnim)), SerializeField] private EasingType animXEaseType = EasingType.OutQuad;
    [ShowIf(nameof(separateAxisAnim)), SerializeField] private EasingType animYEaseType = EasingType.OutQuad;

    [ShowIf(nameof(UseXAxisAnimInCurve)), SerializeField] private AnimationCurve animXAxisCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [ShowIf(nameof(UseYAxisAnimInCurve)), SerializeField] private AnimationCurve animYAxisCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool NotSeparateAxis => !separateAxisAnim;
    private bool UseAnimCurve => !separateAxisAnim && easeType == EasingType.Custom;
    private bool UseXAxisAnimInCurve => separateAxisAnim && animXEaseType == EasingType.Custom;
    private bool UseYAxisAnimInCurve => separateAxisAnim && animYEaseType == EasingType.Custom;
    
    public override void PlayAnim()
    {
        base.PlayAnim();
        tweenTarget.DOKill();
        var initLocalScale = Vector3.one * initScale;
        initLocalScale.z = 1;
        tweenTarget.localScale = initLocalScale;

        if (separateAxisAnim)
        {
            SeparateAnimInAxis();
        }
        else
        {
            var ease = (Ease)easeType;
            var tween = tweenTarget.DOScale(endScale, animTime)
                .SetUpdate(unscaleTime).SetDelay(delayAnimTime)
                .OnComplete(OnAnimComplete);

            if (UseAnimCurve)
                tween.SetEase(animCurve);
            else
                tween.SetEase(ease);
        }
    }
    
    private void SeparateAnimInAxis()
    {
        var dotweenXEase = (Ease)animXEaseType;
        var dotweenYEase = (Ease)animYEaseType;

        // x axis
        var xTween = tweenTarget.DOScaleX(endScale, animTime)
            .SetUpdate(unscaleTime).SetDelay(delayAnimTime)
            .OnComplete(OnAnimComplete);

        if (UseXAxisAnimInCurve)
            xTween.SetEase(animXAxisCurve);
        else
            xTween.SetEase(dotweenXEase);

        // y axis
        var yTween = tweenTarget.DOScaleY(endScale, animTime)
            .SetUpdate(unscaleTime).SetDelay(delayAnimTime);

        if (UseYAxisAnimInCurve)
            yTween.SetEase(animYAxisCurve);
        else
            yTween.SetEase(dotweenYEase);
    }

    private void Reset()
    {
        tweenTarget = transform;
    }
}
