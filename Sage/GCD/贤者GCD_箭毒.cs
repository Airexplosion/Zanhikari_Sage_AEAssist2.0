using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.GCD;

public class 贤者GCD_箭毒 : ISlotResolver
{
    public Spell GetSpell()
    {//还是嵌套替换 箭毒
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Toxikon.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级没到不打
        if (Core.Me.ClassLevel < 66) return -3;
        //红豆关了不打
        if (!Qt.GetQt("红豆")) return -2;
        //开了蛇刺保留且蛇刺只有一个了不打
        if (Qt.GetQt("保留1红豆") && Core.Get<IMemApiSage>().Addersting() < 2) return -6;
        //蛇毒没了不打
        if (Core.Get<IMemApiSage>().Addersting() <= 0) return -1;
        //如果蛇刺还有
        if (Core.Get<IMemApiSage>().Addersting()>= 1)
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 8);
            if (aoeCount >= 2) return 2;
        }
        if (Core.Get<IMemApiMove>().IsMoving())
            //还有层数就打
            if (Core.Get<IMemApiSage>().Addersting() >= 1)
                return 1;

        //一般不打
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