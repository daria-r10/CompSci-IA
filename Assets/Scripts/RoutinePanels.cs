using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoutinePanels : MonoBehaviour
{
    public GameObject loadAvailableExercisesPanel;
    public GameObject loadPreBuiltExercisesPanel;
    void Start()
    {
        CloseLoadAvailableExercisesPanel();
    }

    public void ToggleLoadAvailableExercisesPanel(){
        loadAvailableExercisesPanel.SetActive(!loadAvailableExercisesPanel.activeSelf);
    }

    public void CloseLoadAvailableExercisesPanel(){
        loadAvailableExercisesPanel.SetActive(false);
    }

    public void ToggleLoadPreBuiltExercisesPanel(){
        loadPreBuiltExercisesPanel.SetActive(!loadPreBuiltExercisesPanel.activeSelf);
    }

    public void CloseLoadPreBuiltExercisesPanel(){
        loadPreBuiltExercisesPanel.SetActive(false);
    }
}
