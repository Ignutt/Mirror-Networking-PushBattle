using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RegistrationWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    public UnityEvent onCorrectInput;

    private void Awake()
    {
        onCorrectInput.AddListener(() =>
        {
            Utils.PlayerName = nameInputField.text;
        });
    }

    public void EnterName()
    {
        if (string.IsNullOrEmpty(nameInputField.text)) return;
        
        onCorrectInput.Invoke();
    }
}
