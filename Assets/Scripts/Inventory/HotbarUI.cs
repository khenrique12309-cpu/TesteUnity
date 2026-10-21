using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    public Image[] slotIcons;

    void Start()
    {
        UpdateHotbar();
    }

    public void UpdateHotbar()
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            InventorySlot slot = InventoryManager.Instance.hotbar[i];

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