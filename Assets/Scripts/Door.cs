using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public Vector3 closedPos;
    public Vector3 openPos;
    public float speed = 1f;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private bool isOpen = false;
    private AudioSource audioSource;

    void Start()
    {
        door.localPosition = closedPos;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float doorSpeed = speed * Time.fixedDeltaTime;

        if (isOpen)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, openPos, doorSpeed);
        }
        else
        {
            door.localPosition = Vector3.Lerp(door.localPosition, closedPos, doorSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
            if (doorCloseSound != null)
            {
                StopAndPlaySound(doorCloseSound);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            if (doorOpenSound != null)
            {
                StopAndPlaySound(doorOpenSound);
            }
        }
    }

    void StopAndPlaySound(AudioClip sound)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(sound);
    }
}
