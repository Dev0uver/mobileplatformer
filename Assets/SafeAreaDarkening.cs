using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaDarkening : MonoBehaviour
{
    public RectTransform topMask;
    public RectTransform bottomMask;
    public RectTransform leftMask;
    public RectTransform rightMask;

    void Start()
    {
        AdjustSafeArea();
    }

    void AdjustSafeArea()
    {
        Rect safeArea = Screen.safeArea;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Нормализуем safe area (0-1 координаты относительно экрана)
        Vector2 safeMin = new Vector2(safeArea.xMin / screenWidth, safeArea.yMin / screenHeight);
        Vector2 safeMax = new Vector2(safeArea.xMax / screenWidth, safeArea.yMax / screenHeight);

        // Верхняя маска
        if (topMask)
        {
            topMask.anchorMin = new Vector2(0, safeMax.y); // Начинается от верхнего края safe area
            topMask.anchorMax = new Vector2(1, 1);         // До верхнего края экрана
            topMask.offsetMin = Vector2.zero;
            topMask.offsetMax = Vector2.zero;
        }

        // Нижняя маска
        if (bottomMask)
        {
            bottomMask.anchorMin = new Vector2(0, 0);        // Начинается от нижнего края экрана
            bottomMask.anchorMax = new Vector2(1, safeMin.y); // До нижнего края safe area
            bottomMask.offsetMin = Vector2.zero;
            bottomMask.offsetMax = Vector2.zero;
        }

        // Левая маска
        if (leftMask)
        {
            leftMask.anchorMin = new Vector2(0, safeMin.y);  // Начинается от левого края safe area
            leftMask.anchorMax = new Vector2(safeMin.x, safeMax.y); // До левого края safe area
            leftMask.offsetMin = Vector2.zero;
            leftMask.offsetMax = Vector2.zero;
        }

        // Правая маска
        if (rightMask)
        {
            rightMask.anchorMin = new Vector2(safeMax.x, safeMin.y); // Начинается от правого края safe area
            rightMask.anchorMax = new Vector2(1, safeMax.y);         // До правого края экрана
            rightMask.offsetMin = Vector2.zero;
            rightMask.offsetMax = Vector2.zero;
        }
    }

    void Update()
    {
        // Обновляем маски при изменении разрешения
        AdjustSafeArea();
    }
}
