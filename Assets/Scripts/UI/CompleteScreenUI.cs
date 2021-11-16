using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteScreenUI : MonoBehaviour
{
    Canvas canvas;
    Image imageDisplay;

    [SerializeField] Sprite winBackground;
    [SerializeField] Sprite loseBackground;


    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        imageDisplay = gameObject.GetComponentInChildren<Image>();
    }

    public void ToggleWinScreen()
    {
        imageDisplay.sprite = winBackground;
        ToggleTitleScreen();
    }

    public void ToggleLoseScreen()
    {
        imageDisplay.sprite = loseBackground;
        ToggleTitleScreen();
    }

    void ToggleTitleScreen()
    {
        canvas.enabled = !canvas.enabled;
    }

    public void ReloadLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
