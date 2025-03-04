using UnityEngine;

public class WorldUpgradeButton : MonoBehaviour
{
    [SerializeField] private CatUpgrade upgradeMenu;
    [SerializeField] private Methods method;

    public enum Methods { Sell, Upgrade }

    private void OnMouseOver()
    {
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
}