using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateCube : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spawnPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnCube(spawnPos.transform.position);
        }
    }

    private void SpawnCube(Vector3 spawnPos)
    {
        GameObject cubeInstance = Instantiate(cubePrefab, spawnPos, Quaternion.identity);

        Rigidbody cubeRigidbody = cubeInstance.AddComponent<Rigidbody>();

        cubeRigidbody.mass = 1f;
        cubeRigidbody.drag = 0.5f;
        cubeRigidbody.angularDrag = 0.5f;

    }
}