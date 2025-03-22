using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsBar : MonoBehaviour
{
    [SerializeField] Slider previewSlider;
    public void SetValues(string text, float value)
    {
        GetComponent<TMP_Text>().text = text;
        GetComponent<Slider>().value = value;
    }
    public void SetValues(string text, float currentValue, float nextValue)
    {
        GetComponent<TMP_Text>().text = text;
        GetComponent<Slider>().value = currentValue;
        if (previewSlider != null) previewSlider.value = nextValue;
    }
}
