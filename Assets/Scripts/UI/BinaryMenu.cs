using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryMenu : MonoBehaviour
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private Action<bool> _callBack;

    private void Awake()
    {
        _yesButton.onClick.AddListener(OnYes);
        _noButton.onClick.AddListener(OnNo);
        gameObject.SetActive(false);
    }

    public void Open(Action<bool> Callback)
    {
        gameObject.SetActive(true);
        _callBack = Callback;
    }


    private void OnYes()
    {
        _callBack?.Invoke(true);
        gameObject.SetActive(false);
    }
    
    private void OnNo()
    {
        _callBack?.Invoke(false);
        gameObject.SetActive(false);
    }
}
