using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    [SerializeField] float timeBtwSpawns;
    [SerializeField] float startTimeBtwSpawns;

    [SerializeField] GameObject echo;
    Vector3 lastPosition;
    // Update is called once per frame
    void Update()
    {
        // change this to ui position instead
        if (transform.position != lastPosition)
        {
            if (timeBtwSpawns <= 0)
            {
                GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 1f);
                timeBtwSpawns = startTimeBtwSpawns;
            } else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
        lastPosition = transform.position;
    }
}
