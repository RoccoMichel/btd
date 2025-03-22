using UnityEngine;

public class WorldUpgradeButton : MonoBehaviour
{
    [SerializeField] private CatUpgrade upgradeMenu;
    [SerializeField] private Methods method;

    public enum Methods { Sell, Upgrade }
    private void OnMouseOver()
    {
        if (method == Methods.Sell) upgradeMenu.suacidalSell = false;
        else upgradeMenu.suacidalUpgrade = false;

        if (Input.GetMouseButtonDown(0))
        {
            switch (method)
            {
                case Methods.Sell:
                    upgradeMenu.Sell();
                    break;
                case Methods.Upgrade:
                    upgradeMenu.Upgrade();
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        if (method == Methods.Sell) upgradeMenu.suacidalSell = true;
        else upgradeMenu.suacidalUpgrade = true;
    }
}