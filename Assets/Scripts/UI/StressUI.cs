using UnityEngine;
using UnityEngine.UI;

public class StressUI : MonoBehaviour
{
    [SerializeField] Image[] stressPoints = null;

    void DisplayStressBar(int stressLevel)
    {
        int stressIndex = stressLevel - 1;
        for (int i = 0; i < stressPoints.Length; i++)
        {
            if (i <= stressIndex) {
                stressPoints[i].enabled = true;
            } else {
                stressPoints[i].enabled = false;
            }
        }
    }
}
