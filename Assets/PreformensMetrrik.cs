using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreformensMetrrik : MonoBehaviour
{
    public bool prefomMetrick;
    public int dataPonts;
    public float scale;
    public float[] data = new float[10];
    public LineRenderer line;
    public List<Transform> TragetFrameRate;
    void Start()
    {
        data = new float[dataPonts];
        line.positionCount = dataPonts;
    }
    void draGrafe()
    {
        Vector3 pos = transform.position + transform.forward;
        for (int i = 0; i < data.Length; i++)
        {
            line.SetPosition(i, new Vector3(( (i*-0.5f) / data.Length), data[i]*-scale, 0));
        }
        
        for (int i = 0; i < TragetFrameRate.Count; i++)
        {
            TragetFrameRate[i].localPosition = Vector3.right * -.25f 
                + Vector3.up * -scale * ((1/60f) + (1/100f)*i);

            if (i != 0) TragetFrameRate[i].localPosition += Vector3.right * 0.02f;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)) prefomMetrick = !prefomMetrick;

        if (prefomMetrick)
        {
            line.gameObject.SetActive(true);

            for (int i = 1; i < data.Length; i++)
            {
                if (i == data.Length - 1) data[i] = Time.deltaTime;
                data[i - 1] = data[i];
            }
            draGrafe();
        }
        else
        {
            line.gameObject.SetActive(false);
        }
    }
}
