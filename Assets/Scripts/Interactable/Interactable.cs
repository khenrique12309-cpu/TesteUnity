using UnityEngine;

public enum InteractableKind { Pickup, Repairable }

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Tooltip("Nome mostrado pelo sistema de raycast")]
    public string displayName = "Objeto";

    public InteractableKind kind = InteractableKind.Pickup;

    [Header("Item")]
    [Tooltip("Item que será adicionado ao inventário")]
    public ItemData itemData;

    [Tooltip("Tempo necessário (segundos) para concluir o reparo")]
    public float repairDuration = 3f;

    [HideInInspector]
    public bool isRepaired = false;

    public virtual void Pickup()
    {
        if (itemData == null)
        {
            Debug.LogWarning($"{displayName} não possui um ItemData configurado.");
            return;
        }

        bool added = InventoryManager.Instance.AddItem(itemData);

        if (added)
        {
            Debug.Log($"Picked up: {displayName}");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventário cheio. Não foi possível coletar o item.");
        }
    }

    public virtual void FinishRepair()
    {
        isRepaired = true;

        Debug.Log($"Repaired: {displayName}");

        enabled = false;
    }
}