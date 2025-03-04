using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CatBase : MonoBehaviour
{
    public string displayName = "Killer Kitty";
    public int upgradeLevel;
    public float value = 10;
    public bool isColiding;

    public WaveManager waveMan;
    public List<RatBase> Enemys;

    public Transform spanPos;
    public GameObject prodectile;


    private void OnCollisionStay(Collision collision)
    {
        isColiding = true;
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
    public void FineWaveManager() { waveMan = FindAnyObjectByType<WaveManager>(); }
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

        if (Enemys.Count > 0 && Vector3.Distance(transform.position, Enemys[curentTaget].transform.position) < ransh) transform.LookAt(Enemys[curentTaget].transform);
        return Enemys[curentTaget];
    }
    void SpaneProdetils() 
    {
        spanPos.LookAt(FindeTarget().transform);
        for (float i = 0; i < prodektilCont; i++)
        {
            Instantiate(prodectile, spanPos.position, spanPos.rotation).transform.rotation = 
                Quaternion.Euler(0, ( (i - (prodektilCont-1)/2) / prodektilCont ) * prodekilSpred, 0) * spanPos.rotation;
        }
    }
    void Update()
    {
        // Should run when enemys die or get spawned in : temp
        UpdateEnemyCont();
        target = FindeTarget();

        // Attack locking
        atackTimer += Time.deltaTime;

        if (Enemys.Count() > 0 && atackTimer > atackDilay && Vector3.Distance(transform.position, target.transform.position) < ransh)
        { SpaneProdetils(); atackTimer = 0; } 
    }

    // UPGRADE RELATED METHODS

    /// <summary>
    /// Create Upgrade Menu and give it the Cats info
    /// </summary>
    public void ShowUpgrade()
    {
        CatUpgrade upgradeMenu = Resources.Load("Cat Upgrade").GameObject().GetComponent<CatUpgrade>();
        upgradeMenu.gameObject.transform.position = transform.position + Vector3.up * 10; // Position above the cat
        upgradeMenu.cat = this;
    }

    /// <summary>
    /// Increases the Cats stats and its value
    /// </summary>
    public void Upgrade()
    {
        upgradeLevel++;
        value += value / 2;

        /////////////////////////////////////////////////////
        // SOMEONE NEEDS THE ACCTUAL UPGRADING OF THE CATS //           !!!
        /////////////////////////////////////////////////////
    }

    /// <summary>
    /// Destroys the Cat & nothing else
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
