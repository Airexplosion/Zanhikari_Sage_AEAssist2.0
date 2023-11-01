using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.GCD;
//这部分是均衡注药的调用
public class 贤者GCD_Dot : ISlotResolver
{
    public Spell GetSpell()
    {//注药的嵌套获取，都能用
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Dosis.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级低于30直接别用了，均衡没学会呢
        if(Core.Me.ClassLevel < 30) return -3;
        //dotQT关了也别用
        if (!Qt.GetQt("DOT")) return -2;
        //加入dot黑名单了也别用
        if (DotBlacklistHelper.IsBlackList(Core.Me.GetCurrTarget()))
            return -10;
        //不是boss不上毒了
        if (!Core.Me.GetCurrTarget().IsBoss())
        {
            //如果在移动，可以小上一下
            if (Core.Get<IMemApiMove>().IsMoving())
            {
                return 8;
            }
            //TargetHelper.IsBoss(Core.Me.GetCurrTarget());
            //不在移动不许上
                return -8;
        }

        //如果身上有爆发药，可以提前续一下dot
        if (Core.Me.HasAura(AurasDefine.Medicated))
        {
            if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosis, 6000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIi, 6000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIii, 6000))
            {
                return -1;
            }
            return 2;
        }
        //如果没有均衡，那就判断一下是不是大于4s，大于不上dot
        if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosis, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIi, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIii, 4000))
            return -1;
        
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        //如果身上没有均衡，就塞个均衡进去
        if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
        slot.Add(spell);
        //slot.AppendSequence();
    }
}