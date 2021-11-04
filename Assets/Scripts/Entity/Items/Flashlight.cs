using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class Flashlight : Item
{
    [SerializeField] private bool isActivated = false;
    GameObject player;
    public GameObject flashlight;
    GameObject playerFlashlight;
    public float intensity = 1.1f;

    private void Start() {
        player = GameObject.Find("Player");
        playerFlashlight = Instantiate(flashlight);
        playerFlashlight.transform.parent = player.transform;
        playerFlashlight.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    private void FixedUpdate() {
        KeyCode key = KeyCode.None;
        if      (Input.GetKey (KeyCode.W))  key = KeyCode.W;
        else if (Input.GetKey (KeyCode.A))  key = KeyCode.A;
        else if (Input.GetKey (KeyCode.S))  key = KeyCode.S;
        else if (Input.GetKey (KeyCode.D))  key = KeyCode.D;

        if (Input.GetKeyUp (key))  key = KeyCode.None;

        Vector2 moveDelta = new Vector2(0,0); // (0, 0) by default
        switch (key) {
            case KeyCode.W:  playerFlashlight.transform.rotation = Quaternion.Euler(0, 0, 0f); break;
            case KeyCode.A:  playerFlashlight.transform.rotation = Quaternion.Euler(0, 0, 90f); break;
            case KeyCode.S:  playerFlashlight.transform.rotation = Quaternion.Euler(0, 0, 180f); break;
            case KeyCode.D:  playerFlashlight.transform.rotation = Quaternion.Euler(0, 0, 270f); break;
        }
    }

    public override void utilise()
    {
        isActivated = !isActivated;
        if (isActivated) {
            playerFlashlight.GetComponent<Light2D>().intensity = intensity;
        } else {
            playerFlashlight.GetComponent<Light2D>().intensity = 0f;
        }
    }
}
