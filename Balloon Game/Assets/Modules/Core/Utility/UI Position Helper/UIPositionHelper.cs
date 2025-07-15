using Modules.Core.Utility.Singleton;
using UnityEngine;

public class UIPositionHelper : Singleton<UIPositionHelper>
{
    public static Vector2 GetCanvasCenterPosition(RectTransform target)
    {
        Canvas canvas = target.GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Нет Canvas у объекта: " + target.name);
            return Vector2.zero;
        }
        
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        Vector2 localPoint;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenCenter,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint
        );

        return localPoint;
    }
}