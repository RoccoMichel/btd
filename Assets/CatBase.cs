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
    public float atackDilay, prodekilSpred, ransh;
    public int prodektilCont;
    
    float atackTimer = 0;
    RatBase target;
    public void findWhaveMan() { waveMan = FindAnyObjectByType<WaveManager>(); }
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

        if (Vector3.Distance(transform.position, Enemys[curentTaget].transform.position) < ransh) transform.LookAt(Enemys[curentTaget].transform);
        return Enemys[curentTaget];
    }
    void SpaneProdetils()
    {
        for (float i = 0; i < prodektilCont; i++)
        {
            Instantiate(prodectile, spanPos.position, spanPos.rotation).transform.rotation = Quaternion.Euler(0, ( (i - prodektilCont / 2f) / prodektilCont ) * prodekilSpred, 0) * transform.rotation;
        }
    }
    void Update()
    {
        // Sode run when enemys dey or get spand in : temp
        UpdateEnemyCont();
        target = FindeTarget();

        // Atack lodick
        atackTimer += Time.deltaTime;

        if (atackTimer > atackDilay && Vector3.Distance(transform.position, target.transform.position) < ransh)
        { SpaneProdetils(); atackTimer = 0; }
    }
}
