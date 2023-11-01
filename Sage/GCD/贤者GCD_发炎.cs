using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.GCD;
//这部分是发炎的判断
public class 贤者GCD_发炎 : ISlotResolver
{
    public Spell GetSpell()
    {//发炎的嵌套替换获取技能
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Phlegma.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级没到不打
        if (Core.Me.ClassLevel < 26) return -3;
        //发炎QT关了不打
        if (!Qt.GetQt("发炎")) return -2;
        //距离目标大于6了不打
        if (Core.Me.GetCurrTarget().DistanceMelee(Core.Me) > 6) return -1;
        //获取到的技能的冷却状态大于1.9了打 就是快转好了
        if (GetSpell().Charges > 1.9)
        {
            return 2;
        }


        if (GetSpell().Charges >= 1)
        {
            //6米内的目标周围5米内有2个以上的怪物就打
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 6, 5);
            if (aoeCount >= 2) return 2;
        }

        //在移动的时候
        if (Core.Get<IMemApiMove>().IsMoving())
            //还有层数就打
            if (GetSpell().Charges >= 1)
                return 1;
        //有层数

        //我感觉这里应该是常关的
        return -1;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}