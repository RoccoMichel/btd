using UnityEngine;
using UnityEngine.Rendering;

public class Session : MonoBehaviour
{
    [Header("Player Economy")]
    public bool infiniteWealth;
    public float balance;
    public float minBalance;
    public float interestRate = 1;

    [Header("Player Attributes")]
    public bool immortal = false;
    public float maxHealth = 100f;
    public float health = 100f;

    [Header("Session Details")]
    public int wave = 0;
    public int maxWaves = 20;
    public Volume damageEffect;

    private void Update()
    {
        damageEffect.weight = Mathf.Lerp(damageEffect.weight, 0.001f, Time.deltaTime);
        if (damageEffect.weight > 1) damageEffect.weight = 1;
        if (damageEffect.weight < 0) damageEffect.weight = 0;
    }

    public void Damage(float amount)
    {
        if (immortal) return;

        if (amount < 0)
        {
            Debug.LogWarning("Cannot damage by a negative amount. If you wish to heal use: Session.Heal()");
            return;
        }

        // play damage effect
        damageEffect.weight += Mathf.Ceil(amount) / health;

        // subtract a rounded up value between 0 and maxHealth from health
        health -= Mathf.Clamp(Mathf.Ceil(amount), 0, maxHealth);

        // Lose Condition
        if (health <= 0) Lose();
    }

    public void Heal(float amount)
    {
        if (amount < 0) Debug.LogWarning("Cannot Heal by a negative amount. If you wish to Damage use: Session.Damage()");

        // add a rounded up positive value between 0 and maxHealth to health
        health += Mathf.Clamp(Mathf.Ceil(Mathf.Abs(amount)), 0, maxHealth);
    }

    public void NextWave()
    {
        wave++;

        // give reward money
        //Profit(wave * 100 + 1000);
    }

    /// <summary>
    /// ADD money from balance
    /// </summary>
    public void Profit(float amount)
    {
        balance += Mathf.Abs(Mathf.CeilToInt(amount * (1 + (interestRate / 10))));
    }

    /// <summary>
    /// REMOVE money from balance
    /// </summary>
    public void Expenditure(float amount)
    {
        if (infiniteWealth) return;

        balance -= Mathf.Abs(Mathf.RoundToInt(amount));
    }

    /// <summary>
    /// Is player balance big enough for this purchase?
    /// </summary>
    /// <returns>If the player can afford this purchase</returns>
    public bool CanPurchase(int cost)
    {
        if (infiniteWealth) return true;

        if (balance - cost < minBalance) return false;

        return true;
    }

    /// <summary>
    /// Universal Lose State to end the game
    /// </summary>
    public void Lose()
    {
        Debug.LogWarning($"Player lost at wave {wave}");
        GameUI.Instance.OnGameLose();
    }
}