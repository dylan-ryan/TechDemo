using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 destination;
    public float speed;
    Vector3 newPosition;
    public GameObject player;
    void Start()
    {
        newPosition = destination;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x >= destination.x && transform.position.y >= destination.y && transform.position.z >= destination.z)
        {
            newPosition = origin;
        }
        else if (transform.position.x <= origin.x && transform.position.y <= origin.y && transform.position.z <= origin.z)
        {
            newPosition = destination;
        }
        transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}