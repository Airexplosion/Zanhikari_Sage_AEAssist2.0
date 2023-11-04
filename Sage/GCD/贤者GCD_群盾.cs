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
        //等级低于30直接别用了，均衡没学会呢
        if (Core.Me.ClassLevel < 30) return -3;
        //群盾QT没开不刷
        if (!Qt.GetQt("群盾")) return -7;
        //拉人QT没开不刷
        if (!Qt.GetQt("奶人")) return -2;
        //30级还没学会均衡，不刷
        if (Core.Me.ClassLevel < 30) return -3;

        //蓝量小于3000不刷
        if (Core.Me.CurrentMana < 3000) return -3;
        //自己身上有群盾不刷
        if (Core.Me.HasAura(2609)) return -6;

        //判断现在读条的是不是AOE 是的话就准备放技能

        if (TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(), 10))
        {
            return 2;
        }


        // if (Qt.GetQt("刷个群盾")) return 1;
        //其他情况 常关
        return -1;
    }

    public void Build(Slot slot)
    {

        //如果身上没有均衡，就塞个均衡进去
        if(SpellsDefine.Zoe.IsReady()) slot.Add(SpellsDefine.Zoe.GetSpell());
        if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
        slot.Add(SpellsDefine.EukrasianPrognosis.GetSpell());
    }
}