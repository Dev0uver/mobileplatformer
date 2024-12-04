using UnityEngine;

public class DynamicCameraResize : MonoBehaviour
{
    public float targetWidth = 1920f; // Базовое разрешение (ширина)
    public float targetHeight = 1080f; // Базовое разрешение (высота)

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        // Получаем safe area
        Rect safeArea = Screen.safeArea;

        // Рассчитываем базовый и текущий аспекты
        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;


        // Рассчитываем ортографический размер
        if (windowAspect >= targetAspect)
        {
            // Экран шире, чем базовый: масштабируем по высоте
            cam.orthographicSize = (targetHeight / 200f) * (safeArea.height / Screen.height);
        }
        else
        {
            // Экран выше, чем базовый: масштабируем по ширине
            cam.orthographicSize = ((targetWidth / 200f) / windowAspect);
        }

        // Смещение камеры для учета safe area
        float verticalOffset = ((safeArea.yMin - (Screen.height - safeArea.yMax)) / Screen.height) * cam.orthographicSize * 2;
        cam.transform.position = new Vector3(cam.transform.position.x, verticalOffset, cam.transform.position.z);
}

    void Update()
    {
        // Следим за изменением разрешения экрана
        AdjustCameraSize();
    }
}
