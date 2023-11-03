using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;

//这里包含寄生青汁和自生的调用逻辑
public class 贤者GCD_预后 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

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

        //都在冷却了才群盾
        if (!SpellsDefine.PhysisIi.IsReady() && !SpellsDefine.Physis.IsReady() && !SpellsDefine.Ixochole.IsReady())
        {       
            //如果 小队队员.周围30米范围.的血量大于0 且 血量低于设定中各自奶量阈值数值的角色数量 大于 设定的群奶数目 则准备执行
            if (PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.GCD群奶阈值) >= 贤者设置.实例.群奶数目)
            {
                //看看是不是在移动
                if (Core.Get<IMemApiMove>().IsMoving())
                {   //但身上没群盾
                    if (!Core.Me.HasAura(2609))
                    {
                        return 5;
                    }
                    return -5;
                }
                //没在移动
                if (!Core.Get<IMemApiMove>().IsMoving())
                {
                    return 9;
                }
            }
        }


        //常关，只有需要奶的时候才通过Check
        return -1;
    }

    public void Build(Slot slot)
    {   //如果在移动
        if (Core.Get<IMemApiMove>().IsMoving())
        {   //但身上没群盾
            if (!Core.Me.HasAura(2609)&&!Qt.GetQt("群盾")&& !(Core.Me.ClassLevel < 30))
            {   //检测一下是否有均衡，没有加一个
                if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
                //刷群盾
                slot.Add(SpellsDefine.EukrasianPrognosis.GetSpell());
            }
        }
        //如果没在移动
        if (!Core.Get<IMemApiMove>().IsMoving())
        {
            if (Core.Me.HasAura(2609) && !Qt.GetQt("群盾"))//如果有群盾身上
            {//那就直接刷预后
                slot.Add(SpellsDefine.Prognosis.GetSpell());
            }
            else
            {//没有群盾刷个群盾先
                if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
                slot.Add(SpellsDefine.EukrasianPrognosis.GetSpell());
            }
        }
    }

}