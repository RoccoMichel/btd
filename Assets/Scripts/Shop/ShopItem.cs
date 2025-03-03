using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class uiItem : MonoBehaviour
{
    GameUI ui;
    PlaseCat buildingSystem;

    [Tooltip("File name in Resources")]
    public string structureName;
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
        if (ui.session.CanPurchase(cost))
        {
            ui.session.Expenditure(cost);

            buildingSystem.cat = Resources.Load(structureName).GameObject();
            buildingSystem.setOldMaterols();
        }
        else PurchaseFail();
    }

    void PurchaseFail()
    {
        price.color = Color.red;
        ui.balanceDisplay.color = Color.red;
    }
}