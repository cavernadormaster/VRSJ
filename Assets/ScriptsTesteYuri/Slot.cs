using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Image background;
    public Item item;
    public bool isOccupied = false;

    private Inventory inventory;
    private ItemPanel itemPanel;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        itemPanel = FindObjectOfType<ItemPanel>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler != null)
            {
                if (!isOccupied)
                {
                    item = itemDragHandler.item;
                    itemDragHandler.slot.item = null;
                    itemDragHandler.slot.isOccupied = false;
                    itemDragHandler.slot.background.color = Color.white;
                    inventory.UpdateInventory();
                    isOccupied = true;
                    background.color = Color.gray;
                }
            }
        }
    }

    public void OnClick()
    {
        if (isOccupied)
        {
            itemPanel.ShowPanel(item);
        }
    }
}