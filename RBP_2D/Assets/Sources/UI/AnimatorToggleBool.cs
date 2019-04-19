using UnityEngine;

public class AnimatorToggleBool : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolParameterName;

    private bool _boolParameter;

    public void Toggle()
    {
        _boolParameter = !_boolParameter;

        UpdateParameter();
    }

    public void SetTrue()
    {
        _boolParameter = true;

        UpdateParameter();
    }

    public void SetFalse()
    {
        _boolParameter = false;

        UpdateParameter();
    }

    private void UpdateParameter()
    {
        _animator.SetBool(_boolParameterName, _boolParameter);
    }
}
