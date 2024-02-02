using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject prefab;
    public GameObject playerPrefab;
    Vector3 LastCheckPoint;
    Vector3 LastPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefab.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        LastPoint = playerPrefab.transform.position;
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerPrefab.GetComponent<CharacterController>().enabled = false;
            playerPrefab.transform.position = LastCheckPoint;
            playerPrefab.GetComponent<CharacterController>().enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerPrefab)
        {
            LastCheckPoint = playerPrefab.transform.position;
        }
    }
}
