using TMPro;
using UnityEngine;

public class InfoCard : MonoBehaviour
{
    /// <summary>
    /// Leave 0 to not be killed by time
    /// </summary>
    [Header("Attributes")]
    [Tooltip("Leave 0 to not be killed by time")]
    public float lifeTime;
    public string title = "Card Title";
    [TextArea] public string description = "The following description has not be set!";
    public Side spawnSide;
    public enum Side { left, right }

    [Header("References")]
    [SerializeField] protected TMP_Text titleDisplay;
    [SerializeField] protected TMP_Text descriptionDisplay;

    void Start()
    {
        SetDisplays();
        SetSide(spawnSide);

        if (lifeTime > 0) Destroy(gameObject, lifeTime);
    }

    public void SetValues(float lifeTime, string title, string description, Side side)
    {
        this.lifeTime = lifeTime;
        this.title = title;
        this.description = description;
        spawnSide = side;

        SetSide(spawnSide);
        SetDisplays();

        if (lifeTime > 0) Destroy(gameObject, lifeTime);
    }

    public void SetValuesAndStats(float lifeTime, string title, string description, string[] statNames, float[] statValues, Side side)
    {
        this.lifeTime = lifeTime;
        this.title = title;
        this.description = description;
        spawnSide = side;

        TrySetStats(statNames, statValues, false);
        SetSide(spawnSide);
        SetDisplays();

        if (lifeTime > 0) Destroy(gameObject, lifeTime);
    }

    public void SetValues(float lifeTime, string title, string description, Color titleColor, Color descriptionColor, Side side)
    {
        this.lifeTime = lifeTime;
        this.title = title;
        this.description = description;
        titleDisplay.color = titleColor;
        descriptionDisplay.color = descriptionColor;
        spawnSide = side;

        SetSide(spawnSide);
        SetDisplays();

        if (lifeTime > 0) Destroy(gameObject, lifeTime);
    }

    public void SetValuesAndStats(float lifeTime, string title, string description, string[] statNames, float[] statValues, Color titleColor, Color descriptionColor, Side side)
    {
        this.lifeTime = lifeTime;
        this.title = title;
        this.description = description;
        titleDisplay.color = titleColor;
        descriptionDisplay.color = descriptionColor;
        spawnSide = side;

        TrySetStats(statNames, statValues, false);
        SetSide(spawnSide);
        SetDisplays();

        if (lifeTime > 0) Destroy(gameObject, lifeTime);
    }

    public void Kill()
    {
        // animation and sound?

        Destroy(gameObject);
    }

    protected void TrySetStats(string[] names, float[] values, bool preview)
    {
        StatDisplay statsManager = GetComponentInChildren<StatDisplay>();

        if (statsManager != null)
        {
            statsManager.CreateStats(names, values, preview);
        }
    }

    protected virtual void SetDisplays()
    {
        titleDisplay.text = title;
        descriptionDisplay.text = description;
    }

    protected void SetSide(Side side)
    {
        RectTransform transform = GetComponent<RectTransform>();

        // Numbers should work in any use case because the Canvas Scaler
        switch (side)
        {
            case Side.left:
                transform.anchoredPosition = new Vector2(90, transform.anchoredPosition.y);

                break;

            case Side.right:
                transform.anchoredPosition = new Vector2(1430, transform.anchoredPosition.y);

                break;
        }
    }
}