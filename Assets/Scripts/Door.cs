using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Door : MonoBehaviour
{
    Transform endPos;
    Transform startPos;
    float speed = 1;
    float startTime;
    float length;
    float journey;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        length = Vector3.Distance(startPos.position, endPos.position);

    }

    // Update is called once per frame
    public void Update()
    {
            float distance = (Time.time - startTime) * speed;
            journey = distance / length;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == gameObject.CompareTag("Player"))
        {
            door.transform.localPosition = Vector3.Lerp(startPos.position, endPos.position, journey);
        }
    }
}
