using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MetronomeTimerScript : MonoBehaviour
{
    //Variables Metronome
    public int tempo; 
    private bool isMetronomeOn = false;
    public AudioSource metronomeAudio;
    public AudioClip clickSound;

    //Variables Timer
    public float CDTimer;
    private bool isTimerOn = false;
    private float timer = 0f;
    private int minutes = 0;
    private int seconds = 0;
    public AudioSource alarmSource;
    public AudioClip alarmSound;
    private bool isAlarmPlaying = false;

    //MetronomeTimer UI objects
    public Slider tempoSlider;
    public TextMeshProUGUI tempoText;
    public TextMeshProUGUI timerMinutesText;
    public TextMeshProUGUI timerSecondsText;
    public Button startStopButton;

    //MetronomeTimer UI objects (PopUp Window)
    public Slider minutesSlider;
    public Slider secondsSlider;
    public TextMeshProUGUI popUpMinutesText;
    public TextMeshProUGUI popUpSecondsText;
    public Button increaseTempoButton;
    public Button decreaseTempoButton;
    public Button resetButton;

    // CP UI Objects (ONLY USED IN THE CHORD PROGRESSION EXERCISE

    void Start()
    {
        
        tempoSlider.minValue = 30f; // min tempo value
        tempoSlider.maxValue = 240f; //max tempo value
        tempoSlider.onValueChanged.AddListener(OnTempoSliderValueChange);
        tempoSlider.value = 120; // default tempo
        UpdateTempoText(tempo);
       
        minutesSlider.minValue = 0; // min timer minutes value
        minutesSlider.maxValue = 99; //max timer minutes value
        secondsSlider.minValue = 0; // min timer seconds value
        secondsSlider.maxValue = 59; //max timer seconds value
       
        minutesSlider.onValueChanged.AddListener(OnMinutesSliderValueChanged);
        secondsSlider.onValueChanged.AddListener(OnSecondsSliderValueChanged);
        
        timerMinutesText.text = minutes.ToString("00");
        timerSecondsText.text = seconds.ToString("00");

        popUpMinutesText.text = minutes.ToString("00");
        popUpSecondsText.text = seconds.ToString("00");
        
    }

    void Update()
    {
        if (isMetronomeOn) {
            timer += Time.deltaTime;

            if (timer >= 60f /  tempo)
            {
                if (clickSound != null && metronomeAudio != null)
                {
                    metronomeAudio.PlayOneShot(clickSound);
                    // Resets 'timer' to zero after a beat
                    timer = 0f;
                }
            }
        }

        if (isTimerOn){
            if (CDTimer > 0)
            {
                CDTimer -= Time.deltaTime;
                UpdateMinutesText();
                UpdateSecondsText();
            }else 
                {
                    if (!isAlarmPlaying) // Check if the alarm sound is not already playing
                    {
                        isTimerOn = false;
                        CDTimer = 0;
                        timerMinutesText.text = "00";
                        timerSecondsText.text = "00";
                        if (alarmSound!= null && alarmSource != null )
                        {
                            alarmSource.PlayOneShot(alarmSound);
                            isAlarmPlaying = true; // Set the flag to indicate that the alarm sound is playing
                        }

                    }
                }
        }
    }

    //METRONOME METHODS
    public void OnTempoSliderValueChange(float value){
        tempo = Mathf.RoundToInt(value);
        UpdateTempoText(tempo);
    }
    public void OnIncreaseTempoClick(){
        if(tempo <240){
            tempo += 1;
            tempoText.text = tempo.ToString();
        }
    }
    public void OnDecreaseTempoClick(){
        if(tempo > 30){
            tempo -= 1;
            tempoText.text = tempo.ToString();
        }
    }
    public void UpdateTempoText(int value){
        int roundedTempoValue = Mathf.RoundToInt(tempoSlider.value / 5) * 5; //tempo will increase/decrease 
                                                            //in intervals of 5
        tempoText.text = roundedTempoValue.ToString(); 
    }
    public void StartMetronome(){
        isMetronomeOn = true;
    }
    public void StopMetronome(){
        isMetronomeOn = false;
    }
    public void SetTempo(int newTempo)
    {
        tempo = newTempo;
        UpdateTempoText(tempo);
    }

    //TIMER METHODS
    public void StartTimer(){
        isTimerOn = true;
        CDTimer = minutes * 60f + seconds;
    }
    public void StopTimer(){
   
        isTimerOn = false;
        CDTimer = 0;
    }
    public void OnMinutesSliderValueChanged(float value)
    {
        minutes = Mathf.RoundToInt(value);
        if (minutes < 10){ //ensures that minutes is always written in two digits (e.g 05)
            popUpMinutesText.text = "0" + minutes.ToString();
            timerMinutesText.text = "0" + minutes.ToString();
        }else{
            popUpMinutesText.text = minutes.ToString();
            timerMinutesText.text = minutes.ToString();
        }
        CDTimer= minutes * 60f;
    }
    public void OnSecondsSliderValueChanged(float value)
    {
        seconds = Mathf.RoundToInt(value);
        if (seconds < 10){
            popUpSecondsText.text = "0" + seconds.ToString();
            timerSecondsText.text = "0" + seconds.ToString();
        }else{
            popUpSecondsText.text = seconds.ToString();
            timerSecondsText.text = seconds.ToString();
        }
        CDTimer = minutes * 60f + seconds;
    }
    public void UpdateMinutesText(){
        int minutesLeft = Mathf.FloorToInt(CDTimer / 60f);
        timerMinutesText.text = minutesLeft.ToString("00");

    }
    public void UpdateSecondsText(){
        int secondsLeft = Mathf.FloorToInt(CDTimer % 60f);
        timerSecondsText.text = secondsLeft.ToString("00");
    }
    public void SetTimerDuration(float duration)
    {
        UpdateMinutesText();
        UpdateSecondsText();
    }

    public void StopAlarm()
    {
        if (alarmSource.isPlaying)
        {
            alarmSource.Stop();
            isAlarmPlaying = false;
        }
    }

    //METRONOME & TIMER COMMON METHODS
    public void OnStartStopButtonClick(){
        if(!isMetronomeOn)
        {
            StartMetronome();
            StartTimer();
            
        }else{
            StopMetronome();
            StopTimer();
            if (isAlarmPlaying)
            {
                StopAlarm();
            }
        }
    }
    public void OnResetClick(){
        tempoSlider.value = 120; // default tempo
        UpdateTempoText(tempo);
        OnTempoSliderValueChange(120);
        OnMinutesSliderValueChanged(0);
        OnSecondsSliderValueChanged(0);
        
    }
    
}
