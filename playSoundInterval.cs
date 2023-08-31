using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundInterval : MonoBehaviour
{
    public AudioClip clip;
    public float interval = 60f;

    private AudioSource source;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            source.PlayOneShot(clip);
            timer = interval;
        }
    }
}
