using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private TMP_Text tutorialText;
    private const float fadeAfter = 2f;

    void Start()
    {
        tutorialText = GetComponent<TMP_Text>();
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < fadeAfter) return;

        tutorialText.color = Color.Lerp(tutorialText.color, new Color(255, 255, 255, 0), 0.02f);
    }
}