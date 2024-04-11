using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChordTransitions : MonoBehaviour
{
    [SerializeField] private GameObject chordDisplayText;
    [SerializeField] private MetronomeTimerScript metronomeTimer;
    [SerializeField] private Button[] chordButtons = new Button[14];
    [SerializeField] private Image[] chordImages;

    private string[] chordArray = new string[6];
    private int currentChordIndex = 0;
    private float currentTempo;
    private float currentTimerInterval;
    private bool isTransitioning = false;

    private Dictionary<string, Image> chordImageCollection = new Dictionary<string, Image>();
    private List<Chord> chordList = new List<Chord>(); // List to store created chords

    void Start()
    {
        //maps each chord button's name to its corresponding image
        for (int i = 0; i < chordButtons.Length; i++)
        {
            chordImageCollection[chordButtons[i].name] = chordImages[i];
         
        }
    }
    void Awake()
    {
        // Hide chord images on wake
        CloseChordImages();
    }

    // Class Methods
    public void CloseChordImages()
    {
        foreach (Image chordImage in chordImages)
        {
            chordImage.gameObject.SetActive(false);
        }
    }

    public void ToggleChordImageVisibility(Chord chord, bool isVisible)
    {
        if (chordImageCollection.TryGetValue(chord.name, out Image chordImage))
        {
            chordImage.gameObject.SetActive(isVisible);
        }
        else
        {
            Debug.LogError("Chord image not found: " + chord.name); //Used for testing
        }
    }
    public void CloseDisplayText()
    {
        GameObject chordDisplayText = GameObject.FindWithTag("ChordDisplayText");
        chordDisplayText.SetActive(false);
    }

    public void OnChordButtonClicked()
    {
        if (currentChordIndex < chordArray.Length)
        {
            GameObject selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (selectedButton != null)
            {
                string chordName = selectedButton.name.Replace(" Button", "");
                currentChordIndex++;
                Debug.Log("Added chord: " + chordName); //Used for testing

                // Create a new Chord and add it to the list
                Chord newChord = new Chord(chordName, metronomeTimer.CDTimer, metronomeTimer.tempo, chordImageCollection);
                chordList.Add(newChord);
                Debug.Log("Chord List Length: " + chordList.Count); //Used for testing
            }
        }
        else
        {
            Debug.LogWarning("Chord array is full. Cannot add more chords."); //Used for testing
        }
    }

    // Initiates the transition of displaying chords when transition button is pressed
    public void OnTransitionClick(){
        StartCoroutine(TransitionChord());
    }

    // Coroutine responsible for transitioning chords
    public IEnumerator TransitionChord()
    {
        isTransitioning = true;

        // Iterates through each chord in the chord list
        foreach (Chord chord in chordList) 
        {
            // Shows the chord image
            ToggleChordImageVisibility(chord, true);
        
            // Sets the tempo and timer duration for the metronom
            metronomeTimer.SetTempo(Mathf.RoundToInt(chord.tempo));
            metronomeTimer.SetTimerDuration(chord.duration);

            // Starts the metronome
            metronomeTimer.StartMetronome();

            // Waits for the specified duration
            yield return new WaitForSeconds(chord.duration);

            // Stops the metronome
            metronomeTimer.StopMetronome();

            // Hidse the chord image
            ToggleChordImageVisibility(chord, false);
        }
        isTransitioning = false;
    }
}

[System.Serializable]
public class Chord
{
    public string name;
    public float duration; // Duration in beats
    public int tempo;
    public Image chordImage;    // Tempo for the chord

    public Chord(string chordName, float chordDuration, int chordTempo, Dictionary<string, Image> chordImageDictionary)
    {
        name = chordName;
        duration = chordDuration;
        tempo = chordTempo;
        
        if (chordImageDictionary.TryGetValue(chordName, out Image associatedImage))
        {
            chordImage = associatedImage;
        }
        else
        {
            Debug.LogError("Chord image not found for chord: " + chordName);
        }
    }
}

/*
public class ChordButton : MonoBehaviour
{
    public string chord; 
}
*/



