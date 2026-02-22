using UnityEngine;

public class Barricade : MonoBehaviour
{
    public ItemType item;
    public Collider2D doorCollider;


    public StageManager stageManager;

    private void Awake()
    {
        GlobalManager.OnSceneChanged += RefreshActiveness;
    }

    private void RefreshActiveness()
    {
        bool active = GlobalManager.inventory.Contains(item);
        gameObject.SetActive(active);

        doorCollider.gameObject.SetActive(!active);
    }
}
