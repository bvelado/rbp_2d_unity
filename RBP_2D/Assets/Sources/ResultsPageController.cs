using UnityEngine;

public class ResultsPageController : MonoBehaviour
{
    [SerializeField] private PackView _packView;

    public void Open(PackData packData)
    {
        _packView.DisplayPack(packData);
    }
}
