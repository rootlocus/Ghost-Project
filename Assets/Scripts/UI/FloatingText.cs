using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool isActive;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        isActive = true;
        lastShown = Time.time;
        go.SetActive(isActive);
    }

    public void Hide()
    {
        isActive = false;
        go.SetActive(isActive);
    }

    public void UpdateFloatingText()
    {
        if (!isActive)
            return;
        
        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
