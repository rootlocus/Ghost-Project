using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameObjective = null;
    [SerializeField] TMP_Text memorabiliaText = null;
    [SerializeField] TMP_Text memorabiliaCounterText = null;
    [SerializeField] TMP_Text exorcistObjective = null;
    [SerializeField] GameObject memorabiliaObjective = null;
    [SerializeField] bool isDisplaying = false;

    void Start()
    {
        StartCoroutine("CheckObjectives");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J) && !isDisplaying)
        {
            StartCoroutine("CheckObjectives");
        }
    }

    [Button("Found Name")]
    public void FoundName()
    {
        nameObjective.fontStyle = FontStyles.Strikethrough;
        StartCoroutine("CheckObjectives");
    }

    [Button("Found Memorabilia")]
    public void FoundMemorabilia()
    {
        memorabiliaText.fontStyle = FontStyles.Strikethrough;
        memorabiliaCounterText.fontStyle = FontStyles.Strikethrough;
        StartCoroutine("CheckObjectives");
    }

    [Button("Exorcist")]
    public void ExorcistGhost()
    {
        exorcistObjective.fontStyle = FontStyles.Strikethrough;
        StartCoroutine("CheckObjectives");
    }

    [Button("Fade In")]
    public void ShowAllObjectives()
    {
        //StartCoroutine(FadeInObjectives(0.5f, label));
        StartCoroutine(FadeInObjectives(0.5f, nameObjective));
        StartCoroutine(FadeInObjectives(0.5f, memorabiliaText));
        StartCoroutine(FadeInObjectives(0.5f, memorabiliaCounterText));
        StartCoroutine(FadeInObjectives(0.5f, exorcistObjective));
    }
    
    [Button("Fade Out")]
    public void HideAllObjectives()
    {
        //StartCoroutine(FadeOutObjectives(0.5f, label));
        StartCoroutine(FadeOutObjectives(0.5f, nameObjective));
        StartCoroutine(FadeOutObjectives(0.5f, memorabiliaText));
        StartCoroutine(FadeOutObjectives(0.5f, memorabiliaCounterText));
        StartCoroutine(FadeOutObjectives(0.5f, exorcistObjective));
    }

    public void UpdateMemorabiliaCounter(int counter, int maxCounter)
    {
        memorabiliaCounterText.SetText("(" + counter + " / " + maxCounter + ")");
    }

    IEnumerator CheckObjectives()
    {
        isDisplaying = true;
        ShowAllObjectives();
        yield return new WaitForSeconds(5);
        HideAllObjectives();
        yield return new WaitForSeconds(2);
        isDisplaying = false;
    }

    IEnumerator FadeInObjectives(float timeSpeed, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    IEnumerator FadeOutObjectives(float timeSpeed, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
}
