using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2DamageCalculator
{
    public class CombatValues
    {
        public double currentCritChance { get; set; }

        public double currentHitChance { get; set; }

        public double currentGraze { get; set; }

        public double currentMiss { get; set; }

        public List<float> damageRolls { get; set; }

        public int AimBonus { get; set; }

        public int LowDamage { get; set; }

        public int HighDamage { get; set; }

        public int CritDamage { get; set; }

        public int CritBonus { get; set; }
    }
}