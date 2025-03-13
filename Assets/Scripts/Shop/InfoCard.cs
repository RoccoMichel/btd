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
    public string description = "The following description has not be set!";

    [Header("References")]
    [SerializeField] private TMP_Text titleDisplay;
    [SerializeField] private TMP_Text descriptionDisplay;

    void Start()
    {
        SetDisplays();

        if (lifeTime > 0) Destroy(gameObject, lifeTime);

    }

    public void SetValues(float lifeTime, string title, string description)
    {
        this.lifeTime = lifeTime;
        this.title = title;
        this.description = description;

        SetDisplays();

        if (lifeTime > 0) Destroy(gameObject, lifeTime);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    void SetDisplays()
    {
        titleDisplay.text = title;
        descriptionDisplay.text = description;
    }
}