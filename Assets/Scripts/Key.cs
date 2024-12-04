using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool disappear = false;
    private Animator anim;

    [SerializeField] private Sprite itemSprite; // Спрайт предмета
    [SerializeField] private ItemsController itemsController; // Ссылка на ItemsController

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("disappear", disappear);
    }

    public void GetItem()
    {
        if (isPlayerInRange && !disappear)
        {
            disappear = true;
            anim.SetBool("disappear", disappear);

            // Увеличиваем счет
            Score.score += 5;

            // Передаем спрайт в ItemsController
            if (itemsController != null)
            {
                itemsController.AddItem(itemSprite);
            }
            else
            {
                Debug.LogError("ItemsController is not assigned!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
