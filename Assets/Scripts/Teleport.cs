using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject destination;
    private AudioSource teleportSound;

    private void Start()
    {
        teleportSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (destination != null)
        {
            if (teleportSound != null)
            {
                teleportSound.Play();
            }

            destination.GetComponent<Collider>().enabled = false;

            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = destination.transform.position;
            player.GetComponent <CharacterController>().enabled = true;

            Invoke("EnableTeleport", 5f);
        }
    }

    private void EnableTeleport()
    {
        destination.GetComponent<Collider>().enabled = true;
    }
}