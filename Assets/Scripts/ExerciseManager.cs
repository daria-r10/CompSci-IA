
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;

namespace ExerciseRoutine
{
    [System.Serializable]
    public class Exercise
    {
        public string Name;
        public string Type;
        public float Tempo;
        public float TimerDuration;

        public Exercise(string name, string type, float tempo, float timerDuration)
        {
            Name = name;
            Type = type;
            Tempo = tempo;
            TimerDuration = timerDuration;
        }
    }
    public class ExerciseManager : MonoBehaviour
    {
        public static ExerciseManager Instance { get; private set; }

        // VARIABLES FOR MANAGING EXERCISES
        public int exerciseCount;
        private Exercise currentExercise;
        private MetronomeTimerScript metronomeTimerScript;
        public string[] jsonFileNames;
        // UI ELEMENTS
        public Button saveExerciseButton;
        public Button createExerciseButton;
        public TMP_InputField inputExerciseName;
        public TextMeshProUGUI exerciseNameText;
        public TextMeshProUGUI preBuiltEx1;

        void Start()
        {
            metronomeTimerScript = GetComponent<MetronomeTimerScript>();

            // Gets file names of saved exercises
            jsonFileNames = GetJsonFileNames();
            exerciseCount = jsonFileNames.Length;
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //METHODS FOR CREATING AN EXERCISE
        public void OnButtonClick()
        {
            CreateNewExercise("DefaultName", "Spider", 120f, 60f);
        }

        private Exercise CreateNewExercise(string exerciseName, string exerciseType, float exerciseTempo, float exerciseTimerDuration)
        {
            // Use the values from MetronomeTimerScript to create an Exercise instance
            string type = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            float tempo = metronomeTimerScript.tempo;
            float timerDuration = metronomeTimerScript.CDTimer;

            // Create Exercise instance with the provided name
            Exercise exercise = new Exercise(exerciseName, type, tempo, timerDuration);
            currentExercise = exercise;

            Debug.Log("Setting current exercise: " + exercise.Name);
            Debug.Log(exercise);

            return exercise;
        }

        public void inputName(string name)
        {
            name = inputExerciseName.text; // gets exercise name from the input field
        }

        /*
        public void OnLoadButtonClick(){
            string exerciseName = exerciseNameText.text;
            LoadExercise(exerciseName);
        }
        */
        public void IncrementExerciseCount()
        {
            exerciseCount++;
            Debug.Log("Incrementing exercise count. Current count: " + exerciseCount);
        }

        public int GetExerciseCount()
        {
            return exerciseCount;
        }
        
        // METHODS FOR SAVING AN EXERCISE
        public void SaveToJson(){
            currentExercise.Name = inputExerciseName.text;
            currentExercise.Tempo = metronomeTimerScript.tempo;
            currentExercise.TimerDuration = metronomeTimerScript.CDTimer;

            string json = JsonUtility.ToJson(currentExercise, true);
            string fileName = currentExercise.Name + ".json";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            File.WriteAllText(filePath, json);
            IncrementExerciseCount();

            Debug.Log("Exercise Saved!");
            Debug.Log("Exercise saved to: " + filePath);
            Debug.Log("Exercise Name: " + currentExercise.Name);

        }

        public string[] GetJsonFileNames()
        {
            string directoryPath = Application.persistentDataPath; // Change this path if needed
            string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

            // Extract file names from full paths
            for (int i = 0; i < jsonFiles.Length; i++)
            {
                jsonFiles[i] = Path.GetFileName(jsonFiles[i]);
            }

            return jsonFiles;
        }
        
        public Exercise LoadExercise(string exerciseName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, exerciseName);

            if (File.Exists(filePath))
            {
                string exerciseJson = File.ReadAllText(filePath);
                Exercise loadedExercise = JsonUtility.FromJson<Exercise>(exerciseJson);
                Debug.Log("Exercise loaded from" + filePath);
                return loadedExercise;
            }
            else
            {
                Debug.LogWarning("There are no saved exercises available.");
                return null;
            }
        }

        

        

    
    }
}
    
    

