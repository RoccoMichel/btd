using TMPro;
using UnityEngine;

public class CatUpgrade : MonoBehaviour
{
    // add variables to CatBase for upgrade level & 

    internal CatBase cat;
    protected GameUI shop;

    public bool suacidalSell = false;
    public bool suacidalUpgrade = false;

    [Header("References")]
    [SerializeField] private TMP_Text nameDisplay;
    [SerializeField] private TMP_Text upgradeDisplay;
    [SerializeField] private TMP_Text sellDisplay;

    
    void Update()
    {
        if (cat == null) KillYourSelf();
        if (shop == null) shop = FindAnyObjectByType<GameUI>().GetComponent<GameUI>();

        transform.LookAt(Camera.main.transform);

        // adjust scale based on distance from camera

        RefreshDisplays();
    }

    /// <summary>
    /// Sell the Cat, give the money & destroy everything related
    /// </summary>
    public void Sell()
    {
        // Animation & Sound ?

        // Sell value is half of cat value
        shop.session.Profit(cat.value / 2);
        shop.balanceDisplay.color = Color.green;
        cat.Kill();
        KillYourSelf();
    }

    public void Upgrade()
    {
        // Cost is 2 times of cat value rounded up
        if (shop.session.TryPurchase(Mathf.RoundToInt(cat.value * 2)))
        {
            shop.session.Expenditure(Mathf.RoundToInt(cat.value * 2));
            cat.Upgrade();
            shop.balanceDisplay.color = Color.yellow;
        }
        else shop.balanceDisplay.color = Color.red;

        // Animation & Sound ?
    }

    public void RefreshDisplays()
    {
        nameDisplay.text = $"{cat.displayName}\n(lv. {cat.upgradeLevel})";
        upgradeDisplay.text = $"UPGRADE\n$ {Mathf.RoundToInt(cat.value * 2)}";
        sellDisplay.text = $"$ {Mathf.RoundToInt(cat.value / 2)}";
        // Sell value is half of cat value
    }

    public void KillYourSelf()
    {
        // add wait time, animation & sound

        Destroy(gameObject);
    }
}