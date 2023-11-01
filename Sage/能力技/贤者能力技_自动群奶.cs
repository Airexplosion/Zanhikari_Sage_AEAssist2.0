using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;

//这里包含寄生青汁和自生的调用逻辑
public class 贤者能力技_自动群奶 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
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
        //如果在GetSpell里获取到的技能是Kardia 也跳过，这说明所有这个块下的可调用的技能都进入冷却了
        if (Getspell()==SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }

        //如果 小队队员.周围30米范围.的血量大于0 且 血量低于设定中各自奶量阈值数值的角色数量 大于 设定的群奶数目 则执行
        if (PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.自生阈值) >= 贤者设置.实例.群奶数目 ||
            PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.寄生阈值) >= 贤者设置.实例.群奶数目)
            LogHelper.Print($"周围30米低于阈值的通过");
            return 2;
        //常关，只有需要奶的时候才通过Check
        return -1;
    }

    public void Build(Slot slot)
    {   //将获取到的技能加入slot释放
        slot.Add(Getspell());
    }

    Spell Getspell()
    {   //小队队员.周围30米范围.的血量大于0的角色数目
        var 要被自生奶的 = PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.自生阈值);
        var 要被寄生奶的 = PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.寄生阈值);
        //优先自生，其次寄生
        //如果 自生清汁不在冷却
        if (SpellsDefine.PhysisIi.IsReady()||SpellsDefine.Physis.IsReady())
        {   //如果周围30米范围.的血量大于0且低于自生奶量阈值的角色数目大于设定的PP值
            if (要被自生奶的 >= 贤者设置.实例.群奶数目)
            {   //就返回自生进技能队列
                LogHelper.Print($"自生通过");
                return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.PhysisIi.GetSpell().Id).GetSpell();
            }
        }
        //如果 寄生清汁不在冷却 且 豆子大于1个（留下一个豆子给坚角）
        else if (SpellsDefine.Ixochole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>1)
        {   //如果周围30米范围.的血量大于0且低于寄生奶量阈值的角色数目大于设定的PP值
            if (要被寄生奶的 >= 贤者设置.实例.群奶数目)
            {   //就返回寄生青汁进技能队列
                LogHelper.Print($"寄生通过");
                return SpellsDefine.Ixochole.GetSpell();
            }
        }


        //否则返回Kardia这个技能，会被上面的Check判断跳过这个块的判断
        return SpellsDefine.Kardia.GetSpell();
    }
}