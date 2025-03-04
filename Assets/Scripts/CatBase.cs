using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class CatBase : MonoBehaviour
{
    [Header("Instance Attributes")]
    public string displayName = "Killer Kitty";
    public int upgradeLevel = 1;
    public float value = 10;
    public bool isColliding;
    public AudioClip AtackSond;

    [Header("References")]
    public List<RatBase> Enemies = new();
    public WaveManager waveMan;
    public List<AudioClip> shootSound;
    AudioSource AS;

    public Transform spawnPos;
    public GameObject projectile;

    [Header("Statistics")]
    public float attackDelay = 1;
    public float projectileSpread;
    public float range = 5;
    public int projectileCount = 3;
    public bool hasBoomb;

    float attackTimer = 0;
    RatBase target;
    CatUpgrade upgradeMenu;
    SplineContainer splin;
    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    private void Start()
    {
        if (hasBoomb) splin = FindAnyObjectByType<SplineContainer>();

        AS = GetComponent<AudioSource>();
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
    Vector3 findCloseSplinePos()
    {
        for (int i = 0; i < 1000; i++)
        {
            float ran = Random.Range(0f, 1f);
            Vector3 pos = splin.EvaluatePosition(ran);
            if (Vector3.Distance(pos, transform.position) < range)
                return pos;
        }

        return Vector3.zero;
    }
    void SpawnProjectiles() 
    {
        if (hasBoomb)
        {
            Vector3 pos = findCloseSplinePos();
            if (pos == Vector3.zero) return;

            spawnPos.position = pos;
        }
        else
        { 
            spawnPos.LookAt(FindTarget().transform);
        }

        for (float i = 0; i < projectileCount; i++)
        {
            //AS.Stop();
            //AS.clip = shootSound[Random.Range(0, shootSound.Count - 1)];

            AS.pitch = Random.Range(0.95f, 1.05f);
            AS.PlayOneShot(AtackSond);
            

            Instantiate(projectile, spawnPos.position, spawnPos.rotation).transform.rotation =
                Quaternion.Euler(0, ((i - (projectileCount - 1) / 2) / projectileCount) * projectileSpread, 0) * spawnPos.rotation;
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


        if (target != null
            && Enemies.Count() > 0 
            && attackTimer > attackDelay 
            && Vector3.Distance(transform.position, target.transform.position) < range)
        { SpawnProjectiles(); attackTimer = 0; }


        if (hasBoomb && attackTimer > attackDelay) { SpawnProjectiles(); attackTimer = 0; }
    }

    // UPGRADE RELATED METHODS

    /// <summary>
    /// Create Upgrade Menu and give it the Cats info
    /// </summary>
    public void ShowUpgrade()
    {

        if (upgradeMenu != null) 
        { 
            upgradeMenu.KillYourSelf(); 
            return; 
        } // Don't create dublicate upgradeMenus if spam clicking

        upgradeMenu = Instantiate(Resources.Load("Upgrade Menu").GameObject().GetComponent<CatUpgrade>());
        upgradeMenu.gameObject.transform.position = transform.position + Vector3.up * 6; // Position above the cat
        upgradeMenu.cat = this;
    }

    /// <summary>
    /// Increases the Cats stats and its value
    /// </summary>
    public void Upgrade()
    {
        upgradeLevel++;
        value *= 1.5f;

        float ran = Random.Range(0f, 1f);

        if (ran < 0.3f) attackTimer *= 0.6f;
        else if (ran < 0.6f) projectileSpread *= 0.6f;
        else if (ran < 0.9f) range *= 1.4f;
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
