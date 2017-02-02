using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2DamageCalculator
{
    internal class Weapon
    {
        public int LowDamage { get; set; }

        public int HighDamage { get; set; }

        public bool PlusOne { get; set; }

        public int PlusOnePercentage { get; set; }

        public int CritDamage { get; set; }

        public int CritRating { get; set; }

        public int AimRating { get; set; }
    }
}