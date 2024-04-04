using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin(other.gameObject);
        }
    }

    void CollectCoin(GameObject player)
    {
        CoinManager.Instance.IncrementCoinCount();
        UpdateCoinText();
        PlayCollectSound(player);
        Destroy(gameObject);
    }

    void UpdateCoinText()
    {
        GameObject coinTextObject = GameObject.FindGameObjectWithTag("CoinText");
        if (coinTextObject != null)
        {
            Text coinText = coinTextObject.GetComponent<Text>();
            if (coinText != null)
            {
                coinText.text = "Coins: " + CoinManager.Instance.GetCoinCount().ToString();
            }
        }
    }

    void PlayCollectSound(GameObject player)
    {
        AudioSource audioSource = player.GetComponent<AudioSource>();
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}
