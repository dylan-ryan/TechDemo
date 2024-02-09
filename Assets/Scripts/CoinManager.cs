using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int coinCount = 0;
    private static CoinManager instance;

    public static CoinManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CoinManager").AddComponent<CoinManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void IncrementCoinCount()
    {
        coinCount++;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}