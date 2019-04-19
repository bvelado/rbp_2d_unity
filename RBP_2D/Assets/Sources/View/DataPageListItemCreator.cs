using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DataPageListItemCreator : MonoBehaviour
{
    [SerializeField] private DataPageView _dataPageView;
    [SerializeField] private TMP_InputField _widthInput;
    [SerializeField] private TMP_InputField _heightInput;

    public void AddNewItem()
    {
        int w, h;
        if(int.TryParse(_widthInput.text, out w) && int.TryParse(_heightInput.text, out h))
        {
            _dataPageView.AddNewItem(new Vector2Int(w, h));

            _widthInput.text = "";
            _heightInput.text = "";
        }

        EventSystem.current.SetSelectedGameObject(_widthInput.gameObject);

    }
}
