using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIAnimationGroup : MonoBehaviour
{
    [SerializeField] private List<UIAnimationBase> anims;

    [Button]
    public void PlayAnims()
    {
        anims.ForEach(item => item.PlayAnim());
    }
}