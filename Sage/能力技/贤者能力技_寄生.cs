using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;

//这里包含寄生青汁和自生的调用逻辑
public class 贤者能力技_寄生 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (Core.Me.ClassLevel < 50) return -5;
        //QT奶人关了就跳过
        if (!Qt.GetQt("群奶"))
        {
            return -102;
        }
         //QT奶人关了就跳过
        if (!Qt.GetQt("奶人"))
        {
            return -100;
        }
        //冷却没好跳了
        if (!SpellsDefine.Ixochole.IsReady()) return -6;

        //防止使用过奶之后连按
        if (SpellsDefine.Ixochole.RecentlyUsed(2000)|| SpellsDefine.PhysisIi.RecentlyUsed(2000)|| SpellsDefine.Physis.RecentlyUsed(2000)) return -5;

        //如果 小队队员.周围30米范围.的血量大于0 且 血量低于设定中各自奶量阈值数值的角色数量 大于 设定的群奶数目 则执行
        if (PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.寄生阈值) >= 贤者设置.实例.群奶数目)
        {
            return 2;
        }
        //常关，只有需要奶的时候才通过Check
        return -1;
    }

    public void Build(Slot slot)
    {   //将获取到的技能加入slot释放
        slot.Add(Getspell());
    }

    Spell Getspell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Ixochole.GetSpell().Id).GetSpell();
    }
}