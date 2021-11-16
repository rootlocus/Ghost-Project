using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteScreenUI : MonoBehaviour
{
    Canvas canvas;
    AudioSource audioPlayer;
    Image imageDisplay;

    [SerializeField] Sprite winBackground;
    [SerializeField] Sprite loseBackground;


    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        imageDisplay = gameObject.GetComponentInChildren<Image>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void ToggleWinScreen()
    {
        imageDisplay.sprite = winBackground;
        //audioPlayer.clip = winBGM;
        ToggleTitleScreen();
    }

    public void ToggleLoseScreen()
    {
        imageDisplay.sprite = loseBackground;
        //audioPlayer.clip = loseBGM;
        ToggleTitleScreen();
    }

    void ToggleTitleScreen()
    {
        canvas.enabled = !canvas.enabled;
        audioPlayer.Play();
    }

    public void ReloadLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
