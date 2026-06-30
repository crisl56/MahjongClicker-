using System;
using BreakInfinity;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [field: SerializeField] public BigDouble Coins { get; private set; } = new BigDouble(0);
    [field: SerializeField] public BigDouble CoinsPerClick { get; private set; } = new BigDouble(1);
    [field: SerializeField] public BigDouble CoinsPerSecond { get; private set; } = new BigDouble(0);
    public UnityEvent CoinsUpdated;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"More than one CoinManager! Destroying {this.gameObject.name}");
            Destroy(this.gameObject);
        }

        Instance = this;
    }

    public void Update()
    {
        AddCoins(CoinsPerSecond * Time.deltaTime);
    }

    public void Click()
    {
        AddCoins(CoinsPerClick);
    }

    public bool TrySpend(BigDouble amount)
    {
        if(Coins < amount) return false;
        Coins -= amount;
        CoinsUpdated?.Invoke();
        return true;
    }

    void AddCoins(BigDouble amount)
    {
        Coins += amount;
        CoinsUpdated?.Invoke();
    }

    void SetCoins(BigDouble amount)
    {
        Coins = amount;
        CoinsUpdated?.Invoke();
    }
}