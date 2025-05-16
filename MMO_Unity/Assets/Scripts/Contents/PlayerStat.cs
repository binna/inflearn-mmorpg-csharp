using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp
    {
        get => _exp;
        set
        {
            _exp = value;

            int level = Level;
            while (true)
            {
                if (Managers.Data.StatDict.TryGetValue(level + 1, out var stat) == false)
                    break;
                
                if (_exp < stat.totalExp)
                    break;

                level++;
            }

            if (_level != level)
            {
                Debug.Log("Level Up");
                Level = level;
                SetStat(Level);
            }
        }
    }

    // 표현식 바디
    public int Gold
    {
        get => _gold;
        set => _gold = value;
    }
    
    private void Start()
    {
        _level = 1;
        
        SetStat(_level);
        
        _defence = 5;
        _moveSpeed = 5.0f;
        
        _exp = 0;
        _gold = 0;
    }

    public void SetStat(int level)
    {
        Data.Stat data = Managers.Data.StatDict[level];
        
        _hp = data.maxHp;
        _maxHp = data.maxHp;
        _attack = data.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
