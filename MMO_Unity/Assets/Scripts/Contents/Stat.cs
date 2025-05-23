using Unity.VisualScripting;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defence;
    [SerializeField]
    protected float _moveSpeed;
    
    public int Level
    {
        get => _level;
        set => _level = value;
    }
    
    public int Hp
    {
        get => _hp;
        set => _hp = value;
    }
    
    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = value;
    }
    
    public int Attack
    {
        get => _attack;
        set => _attack = value;
    }
    
    public int Defence
    {
        get => _defence;
        set => _defence = value;
    }
    
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defence = 5;
        _moveSpeed = 5.0f;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int demeage = Mathf.Max(0, attacker.Attack - Defence);
        Hp -= demeage;

        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 15;
        }
        
        Managers.Game.Despawn(gameObject);
    }
}
