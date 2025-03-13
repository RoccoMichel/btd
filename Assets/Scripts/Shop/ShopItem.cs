using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("Item info:")] [TextArea] [SerializeField] 
    string itemDescription = "Description missing!";

    private GameUI ui;
    private PlaseCat buildingSystem;



    public GameObject structure;
    public TMP_Text price;
    public int cost;

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
    }

    public void Purchase()
    {
        if (buildingSystem.cat == null)
        {
            if (ui.session.CanPurchase(cost))
            {
                ui.session.Expenditure(cost);

                buildingSystem.cat = Instantiate(structure);
                buildingSystem.cat.GetComponent<CatBase>().FineWaveManager();
                buildingSystem.VisualizeRange();
                cost = Mathf.RoundToInt(cost * 1.5f);
                price.text = $"$ {cost}";

                ui.DestroyInfoCard();
            }
            else PurchaseFail();
        }
    }

    void PurchaseFail()
    {
        price.color = Color.red;
        ui.balanceDisplay.color = Color.red;
    }

    public void ShowInfoCard()
    {
        if (ui == null)
        {
            Debug.LogWarning("Cannot Display Info Card without GameUI!");
            return;
        }

        // Show Information if it is a cat
        if (structure.TryGetComponent(out CatBase cat))
        {
            string description;

            if (cat.isLure) description = $"${cost}\n\n{itemDescription}";
            else description = $"${cost}\n\nRange: {cat.range}\n Speed: {Mathf.RoundToInt(10*(1f/cat.attackDelay))}\n\n{itemDescription}";

            ui.InstantiateInfoCard(0, cat.displayName, description);

            return;
        }

        // Show Text if it is not a Cat
        ui.InstantiateInfoCard(0, structure.name, $"${cost}\n\n{itemDescription}");
    }
}