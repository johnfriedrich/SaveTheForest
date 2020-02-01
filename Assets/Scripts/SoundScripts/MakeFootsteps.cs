using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFootsteps : MonoBehaviour
{
    [SerializeField]
    float _timeBetweenFootsteps;
    float _timer;
    AudioSource source;
    public AudioClip[] clips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        _timer = _timeBetweenFootsteps;
    }

    void Update()
    {
        if(Input.GetKey("w")
            || Input.GetKey("a")
            || Input.GetKey("s")
            || Input.GetKey("d"))
        {
            _timer -= Time.deltaTime;
        }

        if(_timer <= 0)
        {
            source.clip = clips[Random.Range(0, 6)];
            source.Play();
            _timer = _timeBetweenFootsteps;
        }
    }
}
