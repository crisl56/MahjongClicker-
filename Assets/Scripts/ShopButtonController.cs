using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    [SerializeField] BuffSO _buffData;
    private Button _button;
    private void Reset()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }


        if (CoinManager.Instance.StatHolder.HasBuff(_buffData))
        {
            _button.interactable = false;
        }
    }

    public void AddBuff()
    {
        CoinManager.Instance.StatHolder.CreateBuff(_buffData);
    }
}