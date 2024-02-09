using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    public GameObject checkpoint;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
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
}
