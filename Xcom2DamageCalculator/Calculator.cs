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
        public CombatValues CalculateCombatValues(CombatValues currentRun, double Aim, double Graze, double CritChance, double TargetDodge)
        {
            Aim = Aim / 100;
            Graze = Graze / 100;
            CritChance = CritChance / 100;
            TargetDodge = TargetDodge / 100;
            double MissChance = (1 - Aim + (Graze / 2));
            double HitChance = Aim - (Graze / 2);

            currentRun.currentCritChance = HitChance * (CritChance * (1 - TargetDodge));
            currentRun.currentHitChance = (HitChance * CritChance * TargetDodge) + ((HitChance * (1 - CritChance)) * (1 - TargetDodge)) + (Graze * (CritChance * (1 - TargetDodge)));
            currentRun.currentGraze = HitChance * TargetDodge * (1 - CritChance) + (Graze * CritChance * TargetDodge) + (Graze * (1 - CritChance) * (1 - TargetDodge));
            currentRun.currentMiss = MissChance + (Graze * TargetDodge * (1 - CritChance));

            return currentRun;
        }

        public CombatValues CrunchDamage(CombatValues currentRun, double hitChance, double critChance, double grazeChance, int lowDamage, int highDamage, int accuracy, int plusOneChance, int critDamage, int critRate)
        {
            int i = 0;
            currentRun.damageRolls = new List<float>();
            while (i < 50000)
            {
                Random rrr = new Random();

                float currentDamage = rrr.Next(lowDamage, highDamage);
                int hr = rrr.Next(1, 100);
                int pr = rrr.Next(1, 100);
                float hitRoll = (float)((float)hr / 100);
                float plusRoll = (float)((float)pr / 100);
                if (plusRoll <= plusOneChance)
                {
                    currentDamage = currentDamage + 1;
                }
                if (hitRoll < grazeChance)
                {
                    currentRun.damageRolls.Add(currentDamage / 2);
                }
                else if (hitRoll > grazeChance && hitRoll < (grazeChance + hitChance))
                {
                    currentRun.damageRolls.Add(currentDamage);
                }
                else if (hitRoll > (grazeChance + hitChance) && hitRoll < ((grazeChance + hitChance + critChance)))
                {
                    float x = currentDamage + critDamage;
                    currentRun.damageRolls.Add(x);
                }
                else
                {
                    currentRun.damageRolls.Add(0);
                }
                i++;
            }
            currentRun.damageRolls.Sort();
            var result = currentRun.damageRolls.GroupBy(n => n)
                    .Select(c => new { Key = c.Key, total = c.Count() });
            return currentRun;
        }
    }
}