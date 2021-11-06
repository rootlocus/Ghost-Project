using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public FloatingTextManager floatingTextManager;

    [SerializeField] AudioClip[] bgmClips;
    AudioSource audioPlayer;

    void Awake() {
        instance = this;
        SceneManager.sceneLoaded += SaveState;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        audioPlayer = gameObject.GetComponent<AudioSource>();
        audioPlayer.clip = bgmClips[Random.Range(0, bgmClips.Length)];
        ToggleAudioPlayer();
    }

    public void ToggleAudioPlayer() {
        if (audioPlayer.isPlaying) {
            audioPlayer.Stop();
        } else {
            audioPlayer.Play();
        }
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState(Scene s, LoadSceneMode mode)
    {
        Debug.Log("save");
        // string s = "";
        // s += "0" + "|";
        // s += money.ToString() + "|";
        // s += experience.ToString() + '|';
        // s += "0";

        // PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState()
    {
        // if(PlayerPrefs.HasKey("SaveState"))
        //     return;

        // string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // money = int.Parse(data[1]);
        // experience = int.Parse(data[1]);

        Debug.Log("Load");
    }

}
