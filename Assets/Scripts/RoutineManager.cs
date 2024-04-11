using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using TMPro;
using UnityEngine.UI;

using ExerciseRoutine;

namespace ExerciseRoutine {
    [System.Serializable]
    public class Routine
    {
        public string Name;
        public List<Exercise> Exercises;

        public Routine(string name /* List<Exercise> exercises*/)
        {
            Name = name;
            Exercises = new List<Exercise>();
            Debug.Log("Routine created.");
        }
    }

    public class RoutineManager : MonoBehaviour
    {
        public static RoutineManager Instance { get; private set; }
        private Routine currentRoutine;
        private ExerciseManager exerciseManager;
        public TMP_InputField inputRoutineName;

        public Button createRoutineButton;
        public Button slotExercise1;
        public TextMeshProUGUI slot1ExerciseText;
        public Button slotExercise2;
        public TextMeshProUGUI slot2ExerciseText;
        public Button slotExercise3;
        public TextMeshProUGUI slot3ExerciseText;
        public Button slotExercise4;
        public TextMeshProUGUI slot4ExerciseText;
        public Button slotExercise5;
        public TextMeshProUGUI slot5ExerciseText;

        private string[] jsonFileNames;
        private List<Exercise> selectedExercises = new List<Exercise>();
        void Awake()
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
            exerciseManager = FindObjectOfType<ExerciseManager>();
            
            if (exerciseManager != null)
            {
                Debug.Log("Exercises available: " + exerciseManager.exerciseCount); //for testing

                jsonFileNames = exerciseManager.jsonFileNames;

                foreach (string fileName in jsonFileNames) //for testing
                {
                    Debug.Log("JSON File Name: " + fileName); 
                }
            }
            else
            {
                Debug.Log("ExerciseManager not found."); //for testing
            }
        }

        //METHODS FOR ADDING/REMOVING AN EXERCISE FROM ROUTINE
        public void AddExercise(Exercise exercise)
        {
            selectedExercises.Add(exercise);
        }

        public void RemoveExercise(Exercise exercise)
        {
            selectedExercises.Remove(exercise);
        }
        //METHODS FOR CREATING A ROUTINE

        public Routine CreateRoutine(string routineName)
        {
            Routine routine = new Routine(routineName /*selectedExercises*/);
            currentRoutine = routine;
            return routine; // Return the created routine
            
        }

        public void OnCreateRoutineButton(){
            string routineName = inputRoutineName.text;
            CreateRoutine(routineName);
            UpdateExerciseSlotText(2f);
        }

        //METHODS FOR UPDATING THE SLOTS (doesn't work)
        public IEnumerator UpdateExerciseSlotText(float delay)
        {
            yield return new WaitForSeconds(delay);

            slot1ExerciseText.text = GetSlotText(0);
            slot2ExerciseText.text = GetSlotText(1);
            slot3ExerciseText.text = GetSlotText(2);
            slot4ExerciseText.text = GetSlotText(3);
            slot5ExerciseText.text = GetSlotText(4);
            
        }

        public string GetSlotText(int index)
        {
                string exerciseName = jsonFileNames[index];
                return exerciseName;
                Debug.Log("JSON File Name at position " + index + ": " + exerciseName);
            
        }


        // METHODS FOR SAVING AND LOADING A ROUTINE
        public void SaveRoutine()
        {
            currentRoutine.Name = inputRoutineName.text;
            //currentRoutine.Exercises = selectedExercises;
            string routineJson = JsonUtility.ToJson(currentRoutine);
            string fileName = currentRoutine.Name + ".json";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            
            File.WriteAllText(filePath, routineJson);
            Debug.Log("Routine saved to: " + filePath); //for testing
        }
        public static Routine LoadRoutine(string routineName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, routineName);

            if (File.Exists(filePath))
            {
                string routineJson = File.ReadAllText(filePath);
                Routine loadedRoutine = JsonUtility.FromJson<Routine>(routineJson);
                Debug.Log("Routine loaded from" + filePath);
                return loadedRoutine;
            }
            else
            {
                Debug.LogWarning("There are no saved routines available."); //for testing
                return null;
            }
        }
        
    }

}
