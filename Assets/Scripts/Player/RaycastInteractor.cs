using UnityEngine;
using UnityEngine.UI;

public class RaycastInteractor : MonoBehaviour
{
    [Tooltip("Distância máxima do raycast para interagir")]
    public float maxDistance = 3f;

    [Tooltip("LayerMask para objetos interagíveis. Por padrão tudo")]
    public LayerMask interactLayer = ~0;

    public KeyCode interactKey = KeyCode.E;

    [Tooltip("Referência ao componente de UI que mostra nome/prompt/progresso")]
    public RaycastUI ui;

    private Interactable currentInteractable;
    private float repairTimer = 0f;

    void Awake()
    {
        // Define por padrão a LayerMask para a layer chamada "Interactable" se existir
        int mask = LayerMask.GetMask("Interactable");
        if (mask != 0)
        {
            interactLayer = mask;
        }
        else
        {
            Debug.LogWarning("Layer 'Interactable' não encontrada. Verifique Layers no project settings.", this);
        }
    }

    void Update()
    {
        DoRaycast();
        HandleInput();
    }

    void DoRaycast()
    {
        currentInteractable = null;

        var cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayer))
        {
            var interact = hit.collider.GetComponentInParent<Interactable>();
            if (interact != null)
            {
                currentInteractable = interact;
                if (interact.kind == InteractableKind.Pickup)
                {
                    ui.Show(interact.displayName, $"Pressione '{interactKey}' para coletar");
                    ui.SetProgress(0f);
                }
                else if (interact.kind == InteractableKind.Repairable)
                {
                    if (interact.isRepaired)
                    {
                        ui.Show(interact.displayName, "Reparado");
                        ui.SetProgress(1f);
                    }
                    else
                    {
                        float pct = Mathf.Clamp01(repairTimer / Mathf.Max(0.0001f, interact.repairDuration));
                        ui.Show(interact.displayName, $"Segure '{interactKey}' para reparar ({Mathf.RoundToInt(pct*100)}%)");
                        ui.SetProgress(pct);
                    }
                }
                return;
            }
        }

        // nenhum objeto válido
        ui.Hide();
        repairTimer = 0f;
    }

    void HandleInput()
    {
        if (currentInteractable == null) return;

        if (currentInteractable.kind == InteractableKind.Pickup)
        {
            if (Input.GetKeyDown(interactKey))
            {
                currentInteractable.Pickup();
                ui.Hide();
            }
        }
        else if (currentInteractable.kind == InteractableKind.Repairable)
        {
            if (currentInteractable.isRepaired) return;

            if (Input.GetKey(interactKey))
            {
                repairTimer += Time.deltaTime;
                float pct = Mathf.Clamp01(repairTimer / currentInteractable.repairDuration);
                ui.SetProgress(pct);
                if (repairTimer >= currentInteractable.repairDuration)
                {
                    currentInteractable.FinishRepair();
                    ui.Show(currentInteractable.displayName, "Reparado");
                    ui.SetProgress(1f);
                    repairTimer = 0f;
                }
            }
            else if (Input.GetKeyUp(interactKey) || !Input.GetKey(interactKey))
            {
                // soltou antes de completar
                if (repairTimer > 0f)
                {
                    repairTimer = 0f;
                    ui.SetProgress(0f);
                }
            }
        }
    }
}
