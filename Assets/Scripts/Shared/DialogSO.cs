using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog", order = 53)] // 1

public class DialogSO : ScriptableObject
{
    [SerializeField]
    public string entityName;
    [TextArea(3, 10)]
    [SerializeField]
    public string[] sentences;

    public string[] GetSentences()
    {
        return sentences;
    }

    public string GetEntityName()
    {
        return entityName;
    }

}
