using JetBrains.Annotations;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public RectTransform phone;

    public KeyCode phoneKey = KeyCode.Z;

    public float animationSpeed = 8f;

    private bool phoneOpen = false;

    private Vector2 closedPosition;
    private Vector2 openPosition;

    // Painel que mostra os ícones dos apps (seletor)
    public GameObject appsPanel;

    // Telas/Apps individuais na mesma ordem dos botões: Shop, Sound, Phone, Minigame, Messages, Map
    public GameObject[] appScreens = new GameObject[6];

    // Referência para a tela atualmente aberta (ou null)
    private GameObject currentApp;

    void Start()
    {
        openPosition = phone.anchoredPosition;

        closedPosition = openPosition + new Vector2(0f, -700f);

        phone.anchoredPosition = closedPosition;

        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(phoneKey))
        {
            TogglePhone();
        }

        Vector2 targetPosition;

        if (phoneOpen)
        {
            targetPosition = openPosition;
        }
        else
        {
            targetPosition = closedPosition;
        }

        phone.anchoredPosition = Vector2.Lerp(phone.anchoredPosition, targetPosition, animationSpeed * Time.deltaTime);
    }
    

    public void TogglePhone ()
    {
        phoneOpen = !phoneOpen;

        if (phoneOpen)
        {
            UnlockCursor();
            ShowApps();
        }
        else
        {
            LockCursor();
            CloseAllApps();
        }
        
    }

    // Mostra o painel de seleção de apps
    public void ShowApps()
    {
        if (appsPanel != null) appsPanel.SetActive(true);
        CloseAllApps();
    }

    // Fecha todas as telas/apps
    public void CloseAllApps()
    {
        for (int i = 0; i < appScreens.Length; i++)
        {
            if (appScreens[i] != null) appScreens[i].SetActive(false);
        }
        currentApp = null;
    }

    // Abre app por índice (0..5)
    public void OpenAppByIndex(int index)
    {
        if (index < 0 || index >= appScreens.Length) return;
        if (appsPanel != null) appsPanel.SetActive(false);
        CloseAllApps();
        if (appScreens[index] != null)
        {
            appScreens[index].SetActive(true);
            currentApp = appScreens[index];
        }
    }

    // Métodos conveniência para ligar aos 6 botões via Inspector
    public void OpenShop() => OpenAppByIndex(0);
    public void OpenSound() => OpenAppByIndex(1);
    public void OpenPhone() => OpenAppByIndex(2);
    public void OpenMinigame() => OpenAppByIndex(3);
    public void OpenMessages() => OpenAppByIndex(4);
    public void OpenMap() => OpenAppByIndex(5);

    // Botão voltar do celular: fecha app atual e retorna ao seletor
    public void BackToApps()
    {
        if (currentApp != null)
        {
            currentApp.SetActive(false);
            currentApp = null;
        }
        if (appsPanel != null) appsPanel.SetActive(true);
    }

    // Fecha app atual e esconde o celular
    public void ClosePhone()
    {
        CloseAllApps();
        phoneOpen = false;
        if (phone != null)
        {
            phone.anchoredPosition = closedPosition;
        }
        LockCursor();
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}