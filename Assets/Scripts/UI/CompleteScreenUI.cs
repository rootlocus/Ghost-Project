using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteScreenUI : MonoBehaviour
{
    Canvas canvas;
    Image imageDisplay;
    GameObject button;

    [SerializeField] Sprite winBackground;
    [SerializeField] Sprite loseBackground;
    [SerializeField] Sprite tutorialBackground;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        imageDisplay = GetComponentInChildren<Image>();
        button = GetComponentInChildren<Button>().gameObject;
        button.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            ToggleTutorialScreen();
        }
    }

    public void ToggleTutorialScreen()
    {
        imageDisplay.sprite = tutorialBackground;
        ToggleTitleScreen();
        button.SetActive(!button.activeSelf);
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
