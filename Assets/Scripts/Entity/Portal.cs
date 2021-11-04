using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;


    protected override void onCollide(Collider2D col)
    {
        if (col.name == "Player")
        {
            //Teleport player
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
