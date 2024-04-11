using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeEditPanel : MonoBehaviour
{
    public GameObject editPopUpPanel;

    public void TogglePanel(){
        editPopUpPanel.SetActive(!editPopUpPanel.activeSelf);
    }

    public void ClosePanel(){
        editPopUpPanel.SetActive(false);
    }
    void Start()
    {
        ClosePanel();
    }
}
