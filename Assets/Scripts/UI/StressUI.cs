using UnityEngine;
using UnityEngine.UI;

public class StressUI : MonoBehaviour
{
    [SerializeField] Image[] stressPoints = null;
    [SerializeField] Sprite defaultHealthSprite = null;
    [SerializeField] Sprite lostHealthSprite = null;
    [SerializeField] Player player;

    void Awake()
    {
        if (!player) player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void DisplayStressBar()
    {
        int stressIndex = player.GetStress() - 1;
        for (int i = 0; i < stressPoints.Length; i++)
        {
            if (i <= stressIndex) {
                stressPoints[i].sprite = defaultHealthSprite;
            } else {
                stressPoints[i].sprite = lostHealthSprite;
            }
        }
    }
}
