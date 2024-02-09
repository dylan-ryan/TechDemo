using System.Collections.Generic;
using UnityEngine;

public class CheckpointPrefab : MonoBehaviour
{
    // Dictionary = variable that stores multiple information like player and location.
    private static Dictionary<GameObject, Vector3> checkpointPositions = new Dictionary<GameObject, Vector3>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointPos(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void CheckpointPos(GameObject player)
    {
        //saves players Vector3 in checkpoint trigger
        checkpointPositions[player] = transform.position;
        Debug.Log("Checkpoint position stored for player.");
    }

    public void Respawn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterController>().enabled = false;
        //ContainsKey is apart of the dictionary class to see if a specific variable exists in it.
        if (checkpointPositions.ContainsKey(player))
        {
            player.transform.position = checkpointPositions[player];
        }
        else
        {
            player.transform.position = Vector3.zero;
        }
        player.GetComponent<CharacterController>().enabled = true;
    }
}