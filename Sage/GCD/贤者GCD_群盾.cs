using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.GCD;
//复活的调用
public class 贤者GCD_群盾 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;


    public int Check()
    {
        //群盾QT没开不刷
        if (!Qt.GetQt("群盾")) return -7;
        //拉人QT没开不刷
        if (!Qt.GetQt("奶人")) return -2;

        //蓝量小于3000不刷
        if (Core.Me.CurrentMana < 3000) return -3;
        //自己身上有群盾不刷
        if (Core.Me.HasAura(2609) || Core.Me.HasAura(2866)) return -6;
        //不是boss aoe不刷 判断一下目标是不是在读条 且是boss，并且读条不会读时间太长，比群盾时间还长
        //判断现在读条的是不是AOE 是的话就准备放技能

        if (!Core.Me.GetCurrTarget().IsNull() && Core.Me.GetCurrTarget().IsCasting)
        {
            if (Core.Me.GetCurrTarget().TotalCastTime - Core.Me.GetCurrTarget().CurrentCastTime < 10.0)
            {
                if (Core.Me.GetCurrTarget().CastingSpellId.GetSpell().IsBossAoe())
                {
                    return 2;
                }
            }
                    
        }
        
        
        // if (Qt.GetQt("刷个群盾")) return 1;
        //其他情况 常关
        return -1;
    }

    public void Build(Slot slot)
    {

        //如果身上没有均衡，就塞个均衡进去
        if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
        slot.Add(SpellsDefine.EukrasianPrognosis.GetSpell());
    }
}