using System;
using UnityEngine;
using UnityEngine.UI;

public class RoomWindow : MonoBehaviour
{
    public static RoomWindow Instance;
    
    [SerializeField] private Button readyButton;

    public Button ReadyButton => readyButton;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}    
