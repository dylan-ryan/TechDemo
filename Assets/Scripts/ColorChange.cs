using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject cube;
    public Material redMaterial;
    public Material blueMaterial;
    public AudioClip enterSound;
    public AudioClip exitSound;

    private Renderer cubeRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeColor(redMaterial);
            PlaySound(enterSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeColor(blueMaterial);
            PlaySound(exitSound);
        }
    }

    private void ChangeColor(Material newMaterial)
    {
        if (cubeRenderer != null)
        {
            cubeRenderer.material = newMaterial;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
