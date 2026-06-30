using System;
using BreakInfinity;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [field: SerializeField] public BigDouble Coins { get; private set; } = new BigDouble(0);


    [SerializeField] private BigDouble BaseCoinsPerClick  = new BigDouble(1);
    public BigDouble CoinsPerClick
    {
        get
        {
            if (StatHolder != null)
            {
                return StatHolder.Calculate(StatType.Click, BaseCoinsPerClick);
            }
            return BaseCoinsPerClick;
        }
    }

    [SerializeField] private BigDouble BaseCoinsPerSecond = new BigDouble(0);
    public BigDouble CoinsPerSecond
    {
        get
        {
            if (StatHolder != null)
            {
                return StatHolder.Calculate(StatType.Passive, BaseCoinsPerSecond);
            }
            return BaseCoinsPerSecond;
        }
    }

    public UnityEvent CoinsUpdated;

    public StatHolder StatHolder { get; private set; } = new();

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

    public void AddCoins(BigDouble amount)
    {
        Coins += amount;
        CoinsUpdated?.Invoke();
    }

    public void SetCoins(BigDouble amount)
    {
        Coins = amount;
        CoinsUpdated?.Invoke();
    }

    public void SetStats(StatHolder statHolder)
    {
        StatHolder = statHolder;
    }
}