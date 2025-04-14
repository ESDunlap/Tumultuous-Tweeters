using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    public bool destroyNextRestart = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 8f)
        {
            AudioClip clip = _clips[UnityEngine.Random.Range(0, _clips.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
            destroyNextRestart = true;
        }
        else
        {
            Debug.Log("Collision was too slow: " +collision.relativeVelocity.magnitude);
        }
    }
}
