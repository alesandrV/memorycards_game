using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Create buttons and field. That's all. Nothing more here.
/// </summary>

public class Main : MonoBehaviour
{
    [SerializeField]
    private Transform buttonField;

    [SerializeField]
    private GameObject btn;

    // Add all our buttons with numbers.
    private void Awake()
    {
        for(int i = 0; i < 16; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(buttonField, false);
        }
    }
}
