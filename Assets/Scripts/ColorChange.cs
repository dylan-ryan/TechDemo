using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject cube;
    public Material redMaterial;
    public Material blueMaterial;

    private Renderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeColor(redMaterial);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeColor(blueMaterial);
        }
    }

    private void ChangeColor(Material newMaterial)
    {
        if (cubeRenderer != null)
        {
            cubeRenderer.material = newMaterial;
        }
    }
}