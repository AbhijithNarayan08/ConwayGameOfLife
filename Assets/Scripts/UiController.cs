using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private Button m_StartButton = null;

    [SerializeField]
    private Button m_ClearButton = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI currentGen = null; 

    private int genNum = 0;

    private string genTxt = "Gen : ";

    public static event Action StartEvent;

    public static event Action ClearEvent;

    public void Start()
    {
        m_StartButton.onClick.AddListener(() => StartEvent?.Invoke());
        
        m_ClearButton.onClick.AddListener
        (
            () =>
            { 
                ClearEvent?.Invoke();
                genNum = 0;
                currentGen.text = genTxt + genNum.ToString();
            }
        );

        GridManager.genCounterEvent += (()=>{currentGen.text = genTxt + genNum++.ToString();}); 
    }

    void OnDisable()
    {
        m_StartButton.onClick.RemoveListener(() => StartEvent?.Invoke());
        m_ClearButton.onClick.RemoveListener(() => ClearEvent?.Invoke());

        GridManager.genCounterEvent -= (()=>{});  
    }
}
