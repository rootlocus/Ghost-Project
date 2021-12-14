using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField, BoxGroup("Entities")] Player player;

    //float minTimeClueSound = 2.0f;
    //float maxTimeClueSound = 20.0f;
    //float initTimeSound = 2.0f;
    [SerializeField, BoxGroup("Clues")] GameObject[] furnitures;
    [SerializeField, BoxGroup("Clues"), DisableInEditorMode] int maxMemorabiliaCount = 5;
    [SerializeField, BoxGroup("Clues")] Zone zone;

    [BoxGroup("Inventory")]
    [SerializeField] GameObject[] defaultItems;
    [BoxGroup("Inventory")]
    [SerializeField] InventoryUI inventoryUI;

    [SerializeField, BoxGroup("Other Managers")] FloatingTextManager floatingTextManager;
    [SerializeField, BoxGroup("Other Managers")] AudioManager audioManager;
    [SerializeField, BoxGroup("Other Managers")] CompleteScreenUI levelCompleteUI;
    [SerializeField, BoxGroup("Other Managers")] ClueFoundHandler memorabiliaManager;
    public static bool enableTutorial = false;

    void Awake() 
    {
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!levelCompleteUI) levelCompleteUI = GameObject.Find("CompletedCanvas").GetComponent<CompleteScreenUI>();
        if (!inventoryUI) inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryUI>();
        if (furnitures.Length == 0) furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        if (!zone) zone = GameObject.FindGameObjectWithTag("Zone").GetComponent<Zone>();
        if (!memorabiliaManager) memorabiliaManager = GetComponent<ClueFoundHandler>();
        maxMemorabiliaCount = memorabiliaManager.GetMaxMemorabilia();

        MaintainSingletonInstance();
    }

    void Start()
    {
        InitializePlayerInventory();
        InitializeLevelBGM();
        SpawnMemorabiliaOnFurnitures(); // if no furnitures then skip
    }

    void MaintainSingletonInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void InitializePlayerInventory()
    {
        foreach (var item in defaultItems)
        {
            inventoryUI.AddItem(item);
        }
    }

    [Button("Initialization Prefil", ButtonSizes.Large)]
    void PrefilPrefabs()
    {
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!levelCompleteUI) levelCompleteUI = GameObject.Find("CompletedCanvas").GetComponent<CompleteScreenUI>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        floatingTextManager = GameObject.Find("FloatingTextManager").GetComponent<FloatingTextManager>();
        furnitures = GameObject.FindGameObjectsWithTag("Furniture");
    }

    void SpawnMemorabiliaOnFurnitures()
    {
        furnitures = ShuffleGameObjects(furnitures);
        for (int i = 0; i < maxMemorabiliaCount; i++)
        {
            furnitures[i].GetComponent<Furniture>().AddClue();
        }
        zone.transform.position = furnitures[0].transform.position;
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

    void InitializeLevelBGM()
    {
        audioManager.PlayBGM("BGM_6");
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState(Scene s, LoadSceneMode mode)
    {
        //Debug.Log("save");
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
        Debug.Log("WIN");
        levelCompleteUI.ToggleWinScreen();
        audioManager.PlayBGM("LevelWin");
    }

    public void SetEnableTutorial(bool flag)
    {
        enableTutorial = flag;
    }

}
