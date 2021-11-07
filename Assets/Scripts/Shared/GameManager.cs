using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public FloatingTextManager floatingTextManager;

    [Header("Game BGM")]
    [SerializeField] AudioClip[] bgmClips;

    AudioSource bgmPlayer;
    AudioSource sfxPlayer;
    
    //TODO put into scriptable object? then initialize load
    [Header("Haunting Assets")]
    [SerializeField] GameEvent ClueFoundEvent;
    [SerializeField] List<GameObject> hauntings;
    [SerializeField] int chosenHaunt = 0;
    [SerializeField] string ghostName = "Anon";
    
    float minTimeClueSound = 2.0f;
    float maxTimeClueSound = 20.0f;
    float initTimeSound = 2.0f;
    void Awake() 
    {
        instance = this;
        SceneManager.sceneLoaded += SaveState;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeLevelBGM();
        InitializeHauntingRoom();
    }

    private void InitializeHauntingRoom()
    {
        //TODO: Find and Load in all haunting rooms
        chosenHaunt = Random.Range(0, hauntings.Count);
        hauntings[chosenHaunt].SetActive(true);
        InvokeRepeating("ClueFoundTrigger", initTimeSound, Random.Range(minTimeClueSound, maxTimeClueSound));
    }

    void ClueFoundTrigger()
    {
        ClueFoundEvent?.Raise();
    }

    void Update()
    {
        // hauntings[hauntingRoom].PlayRandomSound();
    }

    void InitializeLevelBGM()
    {
        bgmPlayer = gameObject.GetComponent<AudioSource>();
        bgmPlayer.clip = bgmClips[Random.Range(0, bgmClips.Length)];
        ToggleAudioPlayer();
    }

    public void ToggleAudioPlayer() 
    {
        if (bgmPlayer.isPlaying) {
            bgmPlayer.Stop();
        } else {
            bgmPlayer.Play();
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
