using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public static class Utils
{
    #region Raycasted
    
    private static List<RaycastResult> _raycastResults = new List<RaycastResult>();

    public static bool HasUIElementAtMousePos()
    {
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        
        EventSystem.current.RaycastAll(eventData, _raycastResults);
        return _raycastResults.Count(result => result.gameObject.layer == LayerMask.NameToLayer("UI")) > 0;
    }

    public static bool CheckObjectRaycasted(GameObject needCheckObj)
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        EventSystem.current.RaycastAll(pointerEventData, _raycastResults);
        foreach (var result in _raycastResults)
        {
            if (result.gameObject == needCheckObj)
                return true;
        }
        
        return false;
    }
    
    public static bool CheckObjectRaycastedNotBlockOfAnother(GameObject needCheckObj, GameObject blockObj)
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var isRaycasted = false;
        var isBlocked = false;

        EventSystem.current.RaycastAll(pointerEventData, _raycastResults);
        
        foreach (var result in _raycastResults)
        {
            if (result.gameObject == needCheckObj)
                isRaycasted = true;

            if (result.gameObject == blockObj)
                isBlocked = true;
        }

        return isRaycasted && !isBlocked;
    }

    public static bool IsObjectCurrentlySelectedByEventSystem(GameObject obj) =>
        EventSystem.current.currentSelectedGameObject == obj;

    #endregion
    
    #region RectTF

    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    #endregion
    
    public static void DestroyChildren(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Object.Destroy(parent.GetChild(i).gameObject);
        }
    }
    
    public static void DestroyChildrenImmediate(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Object.DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }
}

    