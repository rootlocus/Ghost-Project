using UnityEngine;

public class EMF : Item
{
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] Zone zone;
    private bool isActivated = false;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        zone = GameObject.FindGameObjectWithTag("Zone").GetComponent<Zone>();
    }

    public override void utilise()
    {
        isActivated = !isActivated;
    }

    public void PlaySound()
    {
        if (!audioPlayer.isPlaying && isActivated)
            audioPlayer.Play();
    }

    public void StopSound()
    {
        if (audioPlayer.isPlaying)
            audioPlayer.Stop();
    }
}
