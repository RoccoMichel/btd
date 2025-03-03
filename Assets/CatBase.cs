using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CatBase : MonoBehaviour
{
    public WaveManager waveMan;
    public List<RatBase> Enemys;

    public Transform spanPos;
    public GameObject prodectile;
    public bool isColiding;

    private void OnCollisionStay(Collision collision)
    {
        isColiding = true;
        print("TOUCHI TOCUHUI");
    }

    private void OnCollisionExit(Collision collision)
    {
        isColiding = false;
    }
    // Stats
    public float atackDilay, prodekilSpred;
    public int prodektilCont;
    
    float atackTimer = 0;
    RatBase target;
    void UpdateEnemyCont()
    {
        Enemys = waveMan.GetComponentsInChildren<RatBase>().ToList();
    }
    RatBase FindeTarget()
    {
        float minDis = Mathf.Infinity;
        int curentTaget = 0;
        for (int i = 0; i < Enemys.Count; i++)
        {
            float dis = Vector3.Distance(Enemys[i].transform.position, transform.position);
            if (dis < minDis)
            {
                curentTaget = i;
                minDis = dis;
            }
        }

        transform.LookAt(Enemys[curentTaget].transform);
        return Enemys[curentTaget];
    }
    void SpaneProdetils()
    {
        for (float i = 0; i < prodektilCont; i++)
        {
            Instantiate(prodectile, spanPos.position, spanPos.rotation).transform.forward = (spanPos.forward + new Vector3
            {
                x = 1,
                y = 0,
                z = prodekilSpred * (i - prodektilCont/2f)


            }).normalized;
        }
    }
    void Update()
    {
        // Sode run when enemys dey or get spand in : temp
        UpdateEnemyCont();
        target = FindeTarget();

        // Atack lodick
        atackTimer += Time.deltaTime;

        if (atackTimer > atackDilay)
        { SpaneProdetils(); atackTimer = 0; }


    }
}
