using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMetronomeScript : MonoBehaviour
{
    public GameObject metronomePanel;
    public Button openMetronomeButton;
    public Button saveMetronomeButton;

    public void TogglePanel(){
        metronomePanel.SetActive(!metronomePanel.activeSelf);
    }

    public void ClosePanel(){
        metronomePanel.SetActive(false);
    }
    void Start()
    {
        ClosePanel();
    }

    
}
