using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TextVisols : MonoBehaviour
{
    public TMP_Text text;

    void runAnimashens(float time, Vector3 pos)
    {
        transform.position = Vector3.Lerp(pos, pos + Vector3.up * 1 / 10f, time);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1 / 10f, time);
    }
    public IEnumerator runTextAnimashen(string displauyText, Vector3 pos)
    {
        transform.position = pos;
        text.text = displauyText;

        for (float time = 0; time < 1; time += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            runAnimashens(time, pos);
        }

        Destroy(gameObject);
    }
    public IEnumerator runTextAnimashen(string displauyText, Vector3 pos, Color setCostom)
    {
        transform.position = pos;
        text.color = setCostom;
        text.text = displauyText;
        float time = 0;
        while (text.alpha > 0.05f)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime * 30;

            runAnimashens(time, pos);
        }

        Destroy(gameObject);
    }
}
