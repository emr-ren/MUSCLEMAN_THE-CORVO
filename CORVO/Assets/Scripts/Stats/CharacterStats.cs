using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Battle Stats")]
    public Stat strength;           // +1 damage +%0.01 crit
    public Stat agility;        // +1 evasion +0.01 crit
    public Stat intelligence;  // +1 magic damage  +3 magic resistance 
    public Stat healthIncreaseX5;    //healt increase  , vitality  her bir puanı +5 can olucak  CalculateTotalHealth fonksiyonunda ayarladim

    [Header("Offensive Stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;      

    [Header("Defansive Stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;    //savusturma dodge
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;
    
    
    public int currentHealth;

    public System.Action Health;
    public bool isDead { get; private set; }

    public EntityFX fx { get; private set; }

    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();

        critPower.SetDefaultValue(150);

        currentHealth = CalculateTotalHealth();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCritHit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }


       /////////////Metota alinabilir --  prive int TargetArmor ////////////
        totalDamage -= _targetStats.armor.GetValue();
        
        /* Armor bugu can artisi
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        */
        /////////////////////////////////////////
        

        _targetStats.TakeDamage(totalDamage);
        //DoMagicalDamage(_targetStats);
    }

    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();


        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicalDamage = TargetMagicResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);
    }

    private static int TargetMagicResistance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAilment(bool _ignite, bool _chill, bool _shock)
    {
        if (isIgnited || isChilled || isShocked)
            return;

        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;     
    }


    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;

        }
        return false;
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealth(_damage);

        GetComponent<Entity>().DamageEffect();
        fx.StartCoroutine("HitFX");
        if (currentHealth < 0 && !isDead)
            Dead();

    }

    protected virtual void DecreaseHealth(int _damage)
    {
        currentHealth -= _damage;

        if (Health != null)
        {
            Health();
        }
    }

    protected virtual void Dead()
    {
        isDead = true;
    }
    public void KillEntity()
    {
        if (!isDead)
            Dead();
    }

    private bool CanCritHit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0,100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.01f;

        float critDamage = _damage * totalCritPower;
                    //Sayiyi yuvarladim
        return Mathf.RoundToInt(critDamage);
    }

    public int CalculateTotalHealth()
    {
        return maxHealth.GetValue() + healthIncreaseX5.GetValue() * 5;
    }
}
