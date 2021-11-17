using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public FloatingTextManager floatingTextManager;

    //TODO put into scriptable object? then initialize load
    [BoxGroup("Haunting"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] List<GameObject> hauntings;
    [BoxGroup("Haunting"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int chosenHaunt = 0;
    [BoxGroup("Haunting"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] string ghostName = "Anon";

    float minTimeClueSound = 2.0f;
    float maxTimeClueSound = 20.0f;
    float initTimeSound = 2.0f;
    [BoxGroup("Clues")]
    [SerializeField] GameObject[] furnitures;
    [BoxGroup("Clues")]
    [SerializeField] int maxClues = 2;
    [SerializeField] AudioManager audioManager;
    [SerializeField] CompleteScreenUI levelCompleteUI;

    void Awake() 
    {
        if (furnitures.Length == 0) furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        InitializeHauntingRoom();
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!levelCompleteUI) levelCompleteUI = GameObject.Find("CompletedCanvas").GetComponent<CompleteScreenUI>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += SaveState;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeLevelBGM();
        SpawnCluesOnFurnitures();
    }

    [Button("Initialization Prefil", ButtonSizes.Large)]
    void PrefilPrefabs()
    {
        // Find way to not deactive gameobject
        GameObject[] hauntingGroupings = GameObject.FindGameObjectsWithTag("Haunting");

        foreach (GameObject haunt in hauntingGroupings)
        {
            hauntings.Add(haunt);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        floatingTextManager = GameObject.Find("FloatingTextManager").GetComponent<FloatingTextManager>();
        furnitures = GameObject.FindGameObjectsWithTag("Furniture");
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
        chosenHaunt = Random.Range(0, hauntings.Count-1);
        hauntings[chosenHaunt].SetActive(true);
        //InvokeRepeating("ClueFoundTrigger", initTimeSound, Random.Range(minTimeClueSound, maxTimeClueSound));
    }

    void Update()
    {
        // hauntings[hauntingRoom].PlayRandomSound();
    }

    void InitializeLevelBGM()
    {
        audioManager.PlayBGM("BGM_1");
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

    public void LoseGame()
    {
        levelCompleteUI.ToggleLoseScreen();
        audioManager.PlayBGM("LevelLose");
    }

    public void WinGame()
    {
        levelCompleteUI.ToggleWinScreen();
        audioManager.PlayBGM("LevelWin");
    }
}
