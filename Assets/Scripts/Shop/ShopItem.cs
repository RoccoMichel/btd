using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private GameUI ui;
    private PlaseCat buildingSystem;

    [Tooltip("File name in Resources")]
    public GameObject structure;
    public TMP_Text price;
    public int cost;
    [SerializeField] protected KeyCode shortcutKey;

    private void Start()
    {
        if (price != null) price.text = $"$ {cost}";

        if (ui == null)
        {
            try { ui = FindAnyObjectByType<GameUI>().GetComponent<GameUI>(); }
            catch { Debug.LogError("No GameUI script was found in active scene"); }
        }
        if (buildingSystem == null)
        {
            try { buildingSystem = FindAnyObjectByType<PlaseCat>().GetComponent<PlaseCat>(); }
            catch { Debug.LogError("No PlaseCat script was found in active scene"); }
        }
    }

    private void FixedUpdate()
    {
        price.color = Color.Lerp(price.color, Color.white, .1f);

        // Purchase item by button press
        if (Input.GetKeyDown(shortcutKey) && FindAnyObjectByType<PlaseCat>().cat == null)
            Purchase();

    }

    public void Purchase()
    {
        if (ui.session.CanPurchase(cost))
        {
            ui.session.Expenditure(cost);

            buildingSystem.cat = Instantiate(structure);
            buildingSystem.cat.GetComponent<CatBase>().FineWaveManager();
            buildingSystem.VisualizeRange();
            cost = Mathf.RoundToInt(cost * 1.5f);
            price.text = $"$ {cost}";
        }
        else PurchaseFail();
    }

    void PurchaseFail()
    {
        price.color = Color.red;
        ui.balanceDisplay.color = Color.red;
    }
}
