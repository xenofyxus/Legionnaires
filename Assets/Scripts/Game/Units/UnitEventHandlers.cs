using System;

namespace Game.Units
{
    public delegate void AttackingEventHandler(object sender,AttackingEventArgs eArgs);
    public class AttackingEventArgs:EventArgs
    {
        public UnitStat Damage{ get; private set; }

        public UnitBehaviour Target{ get; private set; }

        public AttackingEventArgs(UnitStat damage, UnitBehaviour target)
        {
            Damage = damage;
            Target = target;
        }
    }

    public delegate void AttackedEventHandler(object sender,AttackedEventArgs eArgs);
    public class AttackedEventArgs:EventArgs
    {
        public float Damage{ get; private set; }

        public UnitBehaviour Target{ get; private set; }

        public AttackedEventArgs(float damage, UnitBehaviour target)
        {
            Damage = damage;
            Target = target;
        }
    }

    public delegate void TakingDamageEventHandler(object sender,TakingDamageEventArgs eArgs);
    public class TakingDamageEventArgs:EventArgs
    {
        public UnitStat Damage{ get; private set; }

        public UnitBehaviour Attacker{ get; private set; }

        public TakingDamageEventArgs(UnitStat damage, UnitBehaviour attacker)
        {
            Damage = damage;
            Attacker = attacker;
        }
    }

    public delegate void TookDamageEventHandler(object sender,TookDamageEventArgs eArgs);
    public class TookDamageEventArgs:EventArgs
    {
        public float Damage{ get; private set; }

        public UnitBehaviour Attacker{ get; private set; }

        public TookDamageEventArgs(float damage, UnitBehaviour attacker)
        {
            Damage = damage;
            Attacker = attacker;
        }
    }

    public delegate void TakingHealEventHandler(object sender, TakingHealEventArgs eArgs);
    public class TakingHealEventArgs:EventArgs
    {
        public UnitStat Heal{ get; private set; }

        public UnitBehaviour Healer{ get; private set; }

        public TakingHealEventArgs(UnitStat heal, UnitBehaviour healer)
        {
            Heal = heal;
            Healer = healer;
        }
    }

    public delegate void TookHealEventHandler(object sender, TookHealEventArgs eArgs);
    public class TookHealEventArgs:EventArgs
    {
        public float Heal{get;private set;}

        public UnitBehaviour Healer{ get; private set;}

        public TookHealEventArgs(float heal, UnitBehaviour healer)
        {
            Heal = heal;
            Healer = healer;
        }
    }
}

