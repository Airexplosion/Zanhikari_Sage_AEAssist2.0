﻿using CombatRoutine;
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



        //如果在移动，判断一下周围目标数目 决定是不是要小上一下

        var 周围目标 = TargetHelper.GetNearbyEnemyCount(Core.Me, 10, 10);
        if (!Core.Get<IMemApiTarget>().IsBoss(Core.Me.GetCurrTarget()))
        {   //在移动 并且周围确实有小怪，在跟着跑，且GCD到0了，允许上毒
            if (Core.Get<IMemApiMove>().IsMoving() && 周围目标 >= 3 && AI.Instance.GetGCDDuration() == 0)
            {
                if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosis, 4000) ||
                    Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIi, 4000) ||
                    Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIii, 4000))
                { 
                    return -1; 
                }
                else
                {
                    return 5;
                }
                
            }
            return -5;
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
        //如果没有爆发药，那就判断一下是不是大于4s，大于不上dot
        if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosis, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIi, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIii, 4000))
            return -1;

        //如果快死了，不上dot，阈值由玩家自行设定
        if (Core.Me.GetCurrTarget().CurrentHealthPercent < 贤者设置.实例.不上dot阈值)
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