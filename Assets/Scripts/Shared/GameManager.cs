using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public FloatingTextManager floatingTextManager;

    [Header("Game BGM")]
    AudioSource bgmPlayer;
    AudioSource sfxPlayer;
    [SerializeField] AudioClip[] bgmClips;
    
    //TODO put into scriptable object? then initialize load
    [Header("Haunting Assets")]
    [SerializeField] List<GameObject> hauntings;
    [SerializeField] int chosenHaunt = 0;
    [SerializeField] string ghostName = "Anon";

    [Header("Clues Assets")]
    float minTimeClueSound = 2.0f;
    float maxTimeClueSound = 20.0f;
    float initTimeSound = 2.0f;
    [SerializeField] GameObject[] furnitures;
    [SerializeField] int maxClues = 2;

    void Awake() 
    {
        if (furnitures.Length == 0)
        {
            furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        }
        instance = this;
        SceneManager.sceneLoaded += SaveState;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeLevelBGM();
        InitializeHauntingRoom();
        SpawnCluesOnFurnitures();
    }

    void SpawnCluesOnFurnitures()
    {
        furnitures = ShuffleGameObjects(furnitures);
        for (int i = 0; i < maxClues; i++)
        {
            furnitures[i].GetComponent<Furniture>().AddClue();
        }
    }
    GameObject[] ShuffleGameObjects(GameObject[] gameObjects)
    {
        // Loops through array
        for (int i = gameObjects.Length - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            GameObject temp = gameObjects[i];

            // Swap the new and old values
            gameObjects[i] = gameObjects[rnd];
            gameObjects[rnd] = temp;
        }

        return gameObjects;
    }

    void InitializeHauntingRoom()
    {
        //TODO: Find and Load in all haunting rooms
        chosenHaunt = Random.Range(0, hauntings.Count);
        hauntings[chosenHaunt].SetActive(true);
        //InvokeRepeating("ClueFoundTrigger", initTimeSound, Random.Range(minTimeClueSound, maxTimeClueSound));
    }

    //void ClueFoundTrigger()
    //{
    //    ClueFoundEvent?.Raise();
    //}

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
