using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class UIAnimationBase : MonoBehaviour
{
    [SerializeField] protected string description;
    [SerializeField] protected bool isShowOnEnable = true;
    [SerializeField] protected float animTime = 0.3f;
    [SerializeField] protected float delayAnimTime;
    [SerializeField] protected bool unscaleTime;
    
    [SerializeField] protected bool hasEvents;
    [SerializeField, ShowIf(nameof(hasEvents))] protected UnityEvent startEvent;
    [SerializeField, ShowIf(nameof(hasEvents))] protected UnityEvent animStartEvent;
    [SerializeField, ShowIf(nameof(hasEvents))] protected UnityEvent animCompleteEvent;
    
    private void OnEnable()
    {
        if (isShowOnEnable)
        {
            PlayAnim();
        }
    }

    public virtual void PlayAnim()
    {
        startEvent?.Invoke();
    }
    
    protected void OnAnimComplete() => animCompleteEvent?.Invoke();

#if UNITY_EDITOR
    [Button]
    public void TestAnim()
    {
        PlayAnim();
    }
    
#endif
}

public enum EasingType
{
    Linear = 1,
    InSine,
    OutSine,
    InOutSine,
    InQuad,
    OutQuad,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    InElastic,
    OutElastic,
    InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    InBounce,
    OutBounce,
    InOutBounce,
    Flash,
    InFlash,
    OutFlash,
    InOutFlash,
    Custom
}