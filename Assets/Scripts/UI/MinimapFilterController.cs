
using UnityEngine;
using UnityEngine.UI;

public class MinimapFilterController : MonoBehaviour
{
    [Header("Imagem da câmera do minimapa")]
    [SerializeField] private RawImage mapImage;

    private Material filterMaterial;

    public enum FilterMode
    {
        Normal = 0,
        XRay = 1,
        White = 2,
        Black = 3
    }

    private void Awake()
    {
        if (mapImage == null || mapImage.material == null)
        {
            Debug.LogError(
                "Atribua o Raw Image e seu material no Inspector."
            );

            enabled = false;
            return;
        }

        filterMaterial = new Material(mapImage.material);
        mapImage.material = filterMaterial;

        SetFilter(FilterMode.Normal);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetFilter(FilterMode.Normal);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetFilter(FilterMode.XRay);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetFilter(FilterMode.White);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetFilter(FilterMode.Black);
    }

    public void SetFilter(FilterMode mode)
    {
        if (filterMaterial != null)
            filterMaterial.SetFloat("_Mode", (float)mode);
    }

    public void SetNormal() => SetFilter(FilterMode.Normal);
    public void SetXRay() => SetFilter(FilterMode.XRay);
    public void SetWhite() => SetFilter(FilterMode.White);
    public void SetBlack() => SetFilter(FilterMode.Black);

    private void OnDestroy()
    {
        if (filterMaterial != null)
            Destroy(filterMaterial);
    }
}