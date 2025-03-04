using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CatBase : MonoBehaviour
{
    public string displayName = "Killer Kitty";
    public int upgradeLevel;
    public float value = 10;
    public bool isColliding;

    public WaveManager waveMan;
    public List<RatBase> Enemies;

    public Transform spanPos;
    public GameObject projectile;

    // Stats
    public float attackDelay, projectileSpread, range;
    public int projectileCount;

    float attackTimer = 0;
    RatBase target;

    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
   
    public void FineWaveManager() { waveMan = FindAnyObjectByType<WaveManager>(); }
    void UpdateEnemyCount()
    {
        Enemies = FindObjectsByType<RatBase>(FindObjectsSortMode.None).ToList();
    }
    RatBase FindTarget()
    {
        float minDis = Mathf.Infinity;
        int currentTarget = 0;
        for (int i = 0; i < Enemies.Count; i++)
        {
            float dis = Vector3.Distance(Enemies[i].transform.position, transform.position);
            if (dis < minDis)
            {
                currentTarget = i;
                minDis = dis;
            }
        }

        if (Enemies.Count > 0 && Vector3.Distance(transform.position, Enemies[currentTarget].transform.position) < range)
        {
            transform.LookAt(Enemies[currentTarget].transform);
            return Enemies[currentTarget];
        }

        return null;

    }
    void SpawnProjectiles() 
    {
        spanPos.LookAt(FindTarget().transform);
        for (float i = 0; i < projectileCount; i++)
        {
            Instantiate(projectile, spanPos.position, spanPos.rotation).transform.rotation = 
                Quaternion.Euler(0, ( (i - (projectileCount-1)/2) / projectileCount ) * projectileSpread, 0) * spanPos.rotation;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) ShowUpgrade();
    }
    void Update()
    {
        // Should run when enemies die or get spawned in : temp
        UpdateEnemyCount();
        target = FindTarget();
        // Attack locking
        attackTimer += Time.deltaTime;

        if (Enemies.Count() > 0 && attackTimer > attackDelay && Vector3.Distance(transform.position, target.transform.position) < range)
        { SpawnProjectiles(); attackTimer = 0; } 
    }

    // UPGRADE RELATED METHODS

    /// <summary>
    /// Create Upgrade Menu and give it the Cats info
    /// </summary>
    public void ShowUpgrade()
    {
        CatUpgrade upgradeMenu = Resources.Load("Upgrade Menu").GameObject().GetComponent<CatUpgrade>();
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

        float ran = Random.Range(0f, 1f);

        if (ran < 0.3f) attackTimer *= 0.9f;
        else if (ran < 0.6f) projectileSpread *= 0.9f;
        else if (ran < 0.9f) range *= 1.2f;
        else projectileCount++;


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
