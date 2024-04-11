using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoaderScript : MonoBehaviour
{
    //Variables containing the names of the Scenes Available
    private string mainMenuScene = "MainMenu";
    private string metronomeScene = "Metronome";
    private string exercisesScene = "Exercises";
    private string routinesScene = "Routines";
    private string spiderScene = "Spider";
    private string scalesScene = "Scales";
    private string chordProgressionsScene = "ChordTransitions";

    //METHODS
    public void openMainMenuScene() {
        SceneManager.LoadScene(mainMenuScene);
    }

    //Open Metronome Scene
    public void openMetronomeScene() {
        SceneManager.LoadScene(metronomeScene);
    }
    //Open Exercise Scene
    public void openExercisesScene() {
        SceneManager.LoadScene(exercisesScene);
    }

    //Open Routine Scene
    public void openRoutinesScene() {
        SceneManager.LoadScene(routinesScene);
    }

    public void openSpiderScene() {
        SceneManager.LoadScene(spiderScene);
    }

    public void openScalesScene() {
        SceneManager.LoadScene(scalesScene);
    }

    public void openChordProgressionsScene() {
        SceneManager.LoadScene(chordProgressionsScene);
    }
}
