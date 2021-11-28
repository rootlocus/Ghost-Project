using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;
    [SerializeField] TextMeshPro chat;
    [SerializeField] float paddingX = 0.125f;
    [SerializeField] float paddingY = 0.12f;
    [SerializeField] float divideX = 1.5f;
    [SerializeField] float divideY = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
        chat.SetText("WOW THIS IS A LONG TEXT");
        chat.ForceMeshUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 textSize = chat.GetRenderedValues(false);

        Vector2 padding = new Vector2(0.125f, 0.12f);
        background.size = textSize + padding;

        background.transform.localPosition = new Vector3(background.size.x / 1.5f, background.size.y / 2.2f);
    }
}
