using UnityEngine;

public class Descriptor : MonoBehaviour
{
    public string title;
    [TextArea] public string description;
    [Header("Leave 0 for it to not disappear")]
    public float displayTime;

    GameUI ui;

    void Start()
    {
        ui = FindAnyObjectByType<GameUI>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) ui.InstantiateInfoCard(displayTime, title, description, InfoCard.Side.right);
    }
}
