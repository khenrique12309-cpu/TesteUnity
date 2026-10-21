using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Configuração")]
    public int hotbarSize = 4;
    public int inventorySize = 11;

    [Header("Slots")]
    public InventorySlot[] hotbar;
    public InventorySlot[] inventory;

    [Header("UI")]
    public HotbarUI hotbarUI;

    public InventoryPanelUI inventoryPanelUI;

    void Awake()
    {
        Instance = this;

        hotbar = new InventorySlot[hotbarSize];
        inventory = new InventorySlot[inventorySize];

        for (int i = 0; i < hotbar.Length; i++)
        {
            hotbar[i] = new InventorySlot();
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = new InventorySlot();
        }
    }

    public bool AddItem(ItemData item)
    {
        // Primeiro tenta a Hotbar
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].IsEmpty())
            {
                hotbar[i].item = item;
                hotbar[i].quantity = 1;

                Debug.Log(item.itemName + " foi colocado na Hotbar.");

                UpdateUI();

                return true;
            }
        }

        // Depois tenta o Inventário
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].IsEmpty())
            {
                inventory[i].item = item;
                inventory[i].quantity = 1;

                Debug.Log(item.itemName + " foi colocado no Inventário.");

                UpdateUI();

                return true;
            }
        }

        Debug.Log("Hotbar e Inventário estão cheios!");

        return false;
    }

    public void UpdateUI()
    {
        if (hotbarUI != null)
        {
            hotbarUI.UpdateHotbar();
        }

        if (inventoryPanelUI != null)
        {
            inventoryPanelUI.UpdateInventory();
        }
    }
}