using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Door : MonoBehaviour
{
    public Transform door;
    public Vector3 closedPos;
    public Vector3 openPos;
    public float speed = 1f;

    private bool isOpen = false;

    void Start()
    {
        door.localPosition = closedPos;
    }

    void FixedUpdate()
    {
        float doorSpeed = speed * Time.fixedDeltaTime;

        if (isOpen == true)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, openPos, doorSpeed);
        }
        if (isOpen == false)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, closedPos, doorSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}

