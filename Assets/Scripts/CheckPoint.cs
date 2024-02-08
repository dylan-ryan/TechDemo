using System.Collections.Generic;
using UnityEngine;

public class CheckpointPrefab : MonoBehaviour
{
    private const string playerTag = "Player";
    private static Dictionary<GameObject, Vector3> checkpointPositions = new Dictionary<GameObject, Vector3>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StoreCheckpointPosition(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    void StoreCheckpointPosition(GameObject player)
    {
        checkpointPositions[player] = transform.position;
        Debug.Log("Checkpoint position stored for player.");
    }

    void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        player.GetComponent<CharacterController>().enabled = false;
        if (player != null && checkpointPositions.ContainsKey(player))
        {
            player.transform.position = checkpointPositions[player];
            Debug.Log("Player respawned at checkpoint: " + checkpointPositions[player]);
        }
        else
        {
            Debug.LogWarning("Player not found or no checkpoint position stored. Unable to respawn.");
        }
        player.GetComponent<CharacterController>().enabled = true;
    }
}