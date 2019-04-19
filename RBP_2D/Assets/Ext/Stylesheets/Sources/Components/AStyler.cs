using UnityEngine;

public abstract class AStyler : MonoBehaviour, IStyler
{
    [SerializeField] protected StylesheetAsset stylesheet;
    public StylesheetAsset Stylesheet { get { return stylesheet; } }
    public abstract void ApplyStyle();
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
