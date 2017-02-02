using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2DamageCalculator
{
    //H = Hit chance - graze effect (so Hit of 90% means H = 80)
    //G = Graze chance (20% unless hit is over 100%, or you modify this value)
    //M = Miss chance (Hit of 70 means M = 20)
    //C = Crit chance (Crit of 30 means C = 30)
    //D = Dodge chance (Dodge of 15 means D = 15)

    //Crits = HC(1 - D)
    //Hits = HCD + H(1 - C)(1 - D) + GC(1 - D)
    //Graze = HD(1 - C) + GCD + G(1 - C)(1 - D)
    //Miss = M + GD(1 - C)

    public class Calculator
    {
        public CombatValues CalculateCombatValues(CombatValues currentRun, int Aim, int Graze, int CritChance, int TargetDodge)
        {
            int MissChance = (1 - Aim + (Graze / 2));
            int HitChance = Aim - (Graze / 2);

            currentRun.currentCritChance = HitChance * (CritChance * (1 - TargetDodge));
            currentRun.currentHitChance = (HitChance * CritChance * TargetDodge) + ((HitChance * (1 - CritChance)) * (1 - TargetDodge)) + (Graze * (CritChance * (1 - TargetDodge)));
            currentRun.currentGraze = HitChance * TargetDodge * (1 - CritChance) + (Graze * CritChance * TargetDodge) + (Graze * (1 - CritChance) * (1 - TargetDodge));
            currentRun.currentMiss = MissChance + (Graze * TargetDodge * (1 - CritChance));

            return currentRun;
        }
    }
}