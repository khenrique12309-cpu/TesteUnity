using UnityEngine;
using UnityEngine.UI;

public class RaycastUI : MonoBehaviour
{
    [Tooltip("CanvasGroup para ocultar/exibir a UI")]
    public CanvasGroup group;

    public Text nameText;
    public Text promptText;

    [Tooltip("Barra de progresso (Image com Fill Amount)")]
    public Image progressBar;

    void Reset()
    {
        // tenta encontrar componentes na hierarquia
        group = GetComponent<CanvasGroup>();
    }

    public void Show(string name, string prompt)
    {
        if (group != null) group.alpha = 1f;
        if (nameText != null) nameText.text = name;
        if (promptText != null) promptText.text = prompt;
    }

    public void Hide()
    {
        if (group != null) group.alpha = 0f;
        if (nameText != null) nameText.text = "";
        if (promptText != null) promptText.text = "";
        if (progressBar != null) progressBar.fillAmount = 0f;
    }

    public void SetProgress(float t)
    {
        if (progressBar != null) progressBar.fillAmount = Mathf.Clamp01(t);
    }
}
