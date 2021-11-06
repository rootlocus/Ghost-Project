using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteScreenUI : MonoBehaviour
{
    Canvas canvas;
    AudioSource endBGM;

    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        endBGM = gameObject.GetComponent<AudioSource>();
    }

    public void ToggleCompletedTitle()
    {
        canvas.enabled = !canvas.enabled;
        endBGM.Play();
    }
}
