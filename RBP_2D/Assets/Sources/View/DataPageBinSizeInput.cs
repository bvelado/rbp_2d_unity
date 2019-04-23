using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataPageBinSizeInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _widthInput;
    [SerializeField] private TMP_InputField _heightInput;
    
    public Vector2Int GetSize()
    {
        int w, h;
        if (int.TryParse(_widthInput.text, out w) && int.TryParse(_heightInput.text, out h))
        {
            _widthInput.text = "";
            _heightInput.text = "";

            return new Vector2Int(w, h);
        }
        else
        {
            return Vector2Int.zero;
        }
    }

    public void SetSize(Vector2Int size)
    {
        _widthInput.text = string.Format("{0}",size.x);
        _heightInput.text = string.Format("{0}", size.y);
    }
}
