using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{
    [SerializeField] private Image[] items; // Ссылки на UI элементы для отображения спрайтов
    public static int index = 0;

    private Finish finish;

    private SoundPlayer soundPlayer;

    private void Start()
    {
        finish = FindObjectOfType<Finish>();
        soundPlayer = FindObjectOfType<SoundPlayer>();
    }

    public void AddItem(Sprite sprite)
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
        items[index].sprite = sprite;
        items[index].enabled = true;
        soundPlayer.PlayPickupSound(true);
        index++;

        if (index >= 3 && Finish.isClosed)
        {
           finish.OpenFinish();
        }
    }
}
