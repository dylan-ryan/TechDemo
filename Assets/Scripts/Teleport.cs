using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject destination;
    private AudioSource teleportSound;
    public float newFOV = 90f;
    public float originalFOV = 60f;

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

            StartCoroutine(ChangeFOV(player.GetComponentInChildren<Camera>()));

            Invoke("EnableTeleport", 5f);
        }
    }

    private void EnableTeleport()
    {
        destination.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator ChangeFOV(Camera playerCamera)
    {
        float startFOV = playerCamera.fieldOfView;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            playerCamera.fieldOfView = Mathf.Lerp(startFOV, newFOV, t);
            yield return null;
        }

        playerCamera.fieldOfView = newFOV;

        duration = 0.2f;
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            playerCamera.fieldOfView = Mathf.Lerp(newFOV, startFOV, t);
            yield return null;
        }
        playerCamera.fieldOfView = startFOV;
    }

}