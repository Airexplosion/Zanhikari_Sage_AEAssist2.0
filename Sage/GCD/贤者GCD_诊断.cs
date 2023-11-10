using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;

//这里包含寄生青汁和自生的调用逻辑
public class 贤者GCD_诊断 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
         //QT奶人关了就跳过
        if (!Qt.GetQt("奶人"))
        {
            return -100;
        }

        if (!Qt.GetQt("单奶"))
        {
            return -104;
        }

        List<uint> 排除buff = new List<uint>
            {
            3255,//排除出死入生
            AurasDefine.Holmgang,//排除死斗
            AurasDefine.Superbolide//排除超火流星
            };
        var 单奶目标 = PartyHelper.CastableAlliesWithin30
        .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.GCD单奶阈值 &&
                    !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
        .OrderBy(r => r.CurrentHealthPercent)
        .FirstOrDefault();
        //豆子只剩1的时候准备触发这个逻辑
            if (单奶目标.IsValid)
            {
                //看看是不是在移动
                if (Core.Get<IMemApiMove>().IsMoving())
                {   //但身上没你的盾
                    if (!单奶目标.HasAura(2607)&&!(Core.Me.ClassLevel < 30))
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

        //常关，只有需要奶的时候才通过Check
        return -1;
    }

    public void Build(Slot slot)
    {  //设定一下list
        List<uint> 排除buff = new List<uint>
            {
            3255,//排除出死入生
            AurasDefine.Holmgang,//排除死斗
            AurasDefine.Superbolide//排除超火流星
            };
        var 单奶目标 = PartyHelper.CastableAlliesWithin30
        .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.GCD单奶阈值 &&
                    !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
        .OrderBy(r => r.CurrentHealthPercent)
        .FirstOrDefault();



        //如果在移动
        if (Core.Get<IMemApiMove>().IsMoving())
        {   //但目标身上没盾
            if (!单奶目标.HasAura(2607)&& Qt.GetQt("单盾")&& !(Core.Me.ClassLevel < 30))
            {   //检测一下是否有均衡，没有加一个

                if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
                //刷单盾
                slot.Add(new Spell(SpellsDefine.EukrasianDiagnosis, 单奶目标));
            }
        }
        else
        {
            if (!单奶目标.HasAura(2607) && Qt.GetQt("单盾") && !(Core.Me.ClassLevel < 30))
            {   //检测一下是否有均衡，没有加一个

                if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
                //刷单盾
                slot.Add(new Spell(SpellsDefine.EukrasianDiagnosis, 单奶目标));
            }
            slot.Add(new Spell(SpellsDefine.Diagnosis, 单奶目标));
        }
    }

}