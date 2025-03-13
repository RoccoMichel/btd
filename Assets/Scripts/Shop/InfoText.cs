using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    public string info = "INFO TEXT";
    public float lifeTime = 2;
    public float fadeAt = 1.4f;
    private float timeAlive;

    public float moveSpeed = 1;
    public float colorFadeSpeed = 0.2f;
    public Color targetColor = Color.black;
    private TMP_Text tmp;

    void Start()
    {
        tmp = GetComponent<TMP_Text>();

        tmp.text = info;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * moveSpeed;

        if (timeAlive < fadeAt) return;
        tmp.color = Color.Lerp(tmp.color, targetColor, colorFadeSpeed);
    }
}