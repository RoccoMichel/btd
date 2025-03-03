using UnityEngine;

public class Session : MonoBehaviour
{
    [Header("Player Economy")]
    public float balance;
    public float minBalance;
    public float interestRate = 1; // not implemented

    [Header("Player Attributes")]
    public bool immortal = false;
    public float maxHealth = 100f;
    public float health = 100f;

    [Header("Session Details")]
    public int wave = 0;

    public void Damage(float amount)
    {
        if (immortal) return;

        if (amount < 0)
        {
            Debug.LogWarning("Cannot damage by a negative amount. If you wish to heal use: Session.Heal()");
            return;
        }

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
        Profit(wave * 100 + 1000);

        // reference the wave manager
    }

    public void Profit(float amount)
    {
        balance += Mathf.RoundToInt(amount);
    }

    public void Expenditure(float amount)
    {        
        balance -= Mathf.RoundToInt(amount);
    }

    public bool TryPurchase(int cost)
    {
        if (balance - cost < minBalance) return false;

        Expenditure(cost);
        return true;
    }

    public void Lose()
    {
        Debug.LogWarning($"Player lost at wave {wave}");
    }
}