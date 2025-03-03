using UnityEngine;
using System.Collections.Generic;

public class Session : MonoBehaviour
{
    /* 
     * currency
     * interest
     * 
     */
    [Header("Player Economy")]
    public float currency;

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
        // reference the wave manager
    }

    public void Lose()
    {
        Debug.LogWarning($"Player lost at wave {wave}");
    }
}