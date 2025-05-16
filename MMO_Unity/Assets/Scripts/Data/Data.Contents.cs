using System;
using System.Collections.Generic;

namespace Data
{
#region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new();

        public Dictionary<int, Stat> MakeDic()
        {
            Dictionary<int, Stat> tempDic = new();

            foreach (var stat in stats)
            {
                tempDic.Add(stat.level, stat);
            }

            return tempDic;
        }
    }
#endregion
}