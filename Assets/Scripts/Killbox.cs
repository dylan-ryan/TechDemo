using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    public GameObject checkpoint;
    public AudioClip deathSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
            PlayDeathSound();
        }
    }

    void RespawnPlayer(GameObject player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        CheckpointPrefab checkpointScript = checkpoint.GetComponentInChildren<CheckpointPrefab>();
        if (checkpointScript != null)
        {
            checkpointScript.Respawn();
        }
        else
        {
            player.transform.position = Vector3.zero;
        }
        player.GetComponent<CharacterController>().enabled = true;
    }
    void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
