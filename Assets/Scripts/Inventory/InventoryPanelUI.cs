
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelUI : MonoBehaviour
{
    [Header("Ícones dos 12 slots do inventário")]
    public Image[] slotIcons;

    void Start()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            InventorySlot slot = InventoryManager.Instance.inventory[i];

            if (slot.IsEmpty())
            {
                slotIcons[i].enabled = false;
            }
            else
            {
                slotIcons[i].enabled = true;
                slotIcons[i].sprite = slot.item.icon;
            }
        }
    }
}