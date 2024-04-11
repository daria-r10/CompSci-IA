using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
    public GameObject editTimerPopUpPanel;
    public GameObject saveExercisePanel;
    public GameObject editMetronomePopUpPanel;
    //METHODS FOR OPENING/CLOSING PANELS
    public void ToggleMetronomePanel(){
        editMetronomePopUpPanel.SetActive(!editMetronomePopUpPanel.activeSelf);
    }
    public void CloseMetronomePanel(){
        editMetronomePopUpPanel.SetActive(false);
    }
    public void ToggleTimerPanel(){
        editTimerPopUpPanel.SetActive(!editTimerPopUpPanel.activeSelf);
    }
    public void CloseTimerPanel(){
        editTimerPopUpPanel.SetActive(false);
    }
    public void ToggleSaveExercisePanel(){
        saveExercisePanel.SetActive(!saveExercisePanel.activeSelf);
    }

    public void CloseSaveExercisePanel(){
        saveExercisePanel.SetActive(false);
    }
    void Start()
    {
        GameObject editMetronomePopUpPanel = GameObject.FindWithTag("MetronomePanel");
        GameObject saveExercisePanel = GameObject.FindWithTag("SaveExercisePanel");

        if (editMetronomePopUpPanel != null)
        {
            CloseMetronomePanel();
        }
        if (saveExercisePanel != null)
        {
            CloseSaveExercisePanel();
        }
        CloseTimerPanel();
    }
}
