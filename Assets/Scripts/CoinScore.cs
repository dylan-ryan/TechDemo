using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        CoinManager.Instance.IncrementCoinCount();
        UpdateCoinText();
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
}