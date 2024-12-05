using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{
    [SerializeField] private Image[] items; // Ссылки на UI элементы для отображения спрайтов
    [SerializeField] private Sprite sprite; // Ссылки на UI элементы для отображения спрайтов
    private static int index = 0;

    public void AddItem(Sprite sprite1)
    {
        if (items == null || items.Length == 0)
        {
            Debug.LogError("Items array is not assigned or empty!");
            return;
        }

        if (index >= items.Length)
        {
            Debug.LogError("No more space in the items array!");
            return;
        }

        // Назначаем спрайт и увеличиваем индекс
        items[index].sprite = sprite1;
        items[index].enabled = true;
        index++;
    }
}
