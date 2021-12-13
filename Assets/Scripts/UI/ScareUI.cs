using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScareUI : MonoBehaviour
{
    [SerializeField] GameObject[] scares;

    [Button("Scare Player")]
    public void ActivateScare()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        scares[0].SetActive(true);

        yield return new WaitForSeconds(2f);

        scares[0].SetActive(false);
    }
}
