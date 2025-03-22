using TMPro;
using UnityEngine;

public class UpgradeCard : InfoCard
{
    public CatBase cat;
    GameUI shop;

    [Header("References")]
    [SerializeField] private TMP_Text upgradeDisplay;
    [SerializeField] private TMP_Text sellDisplay;

    public void SetUpgradeValues(Side side, CatBase cat, GameUI shop)
    {
        spawnSide = side;
        this.cat = cat;
        this.shop = shop;

        SetSide(spawnSide);
        SetDisplays();
    }

    private void Update()
    {
        if (cat == null) Kill();
        if (shop == null) shop = FindAnyObjectByType<GameUI>().GetComponent<GameUI>();

        SetDisplays();
    }

    protected override void SetDisplays()
    {
        TrySetStats(cat.GetStatNames(), cat.GetStatValues(), true);

        titleDisplay.text = $"{cat.displayName}\n(lv. {cat.upgradeLevel})";
        upgradeDisplay.text = $"UPGRADE\n$ {Mathf.Abs(Mathf.Round(cat.value * 2))}";
        sellDisplay.text = $"SELL\n$ {Mathf.Abs(Mathf.Round(cat.value))}";
    }

    public void Upgrade()
    {
        // Cost is 2 times of cat value rounded up
        if (shop.session.CanPurchase(Mathf.Abs(Mathf.Round(cat.value * 2))))
        {
            shop.session.Expenditure(Mathf.Abs(Mathf.Round(cat.value * 2)));
            cat.Upgrade();
            shop.balanceDisplay.color = Color.yellow;
        }
        else shop.balanceDisplay.color = Color.red;
    }

    public void Sell()
    {
        // Sell value is half of cat value
        shop.session.Profit(Mathf.Abs(cat.value));
        shop.balanceDisplay.color = Color.green;
        cat.Kill();
        Kill();
    }
}
