using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Projectile", menuName = "New Projectile")]
public class ScriptableProjecttileManager : ScriptableObject
{
    [Tooltip("How Long It Should Exist")]
    public float lifeTime;

    public float damage;
    public float speed;

    public bool doDamageWhenDied;
    [ShowIf("doDamageWhenDied")]
    public float damageRange;

    [Tooltip("Do An Efect When Destroyd")]
    public bool doEfect;
    [ShowIf("doEfect")]
    public bool doEfectWhenDied;
    [ShowIf("doEfect")]
    public bool doEfectWhenHitEnemy;
    [ShowIf("doEfect")]
    public GameObject efect;

    public bool bounce;

    public bool doPoisone;

    [ShowIf("doPoisone")]
    public float poisoneDamage, poisoneTimes;
}
