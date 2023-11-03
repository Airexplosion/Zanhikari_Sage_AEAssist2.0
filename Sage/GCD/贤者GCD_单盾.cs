using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;

//这里包含寄生青汁和自生的调用逻辑
public class 贤者GCD_单盾 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级低于30直接别用了，均衡没学会呢
        if (Core.Me.ClassLevel < 30) return -3;
        //QT奶人关了就跳过
        if (!Qt.GetQt("奶人"))
        {
            return -100;
        }
        //吃死刑的目标没单盾再给，有就不给了
        if (Core.Me.GetCurrTargetsTarget().HasAura(2607)) return -7;

        if (!Qt.GetQt("单盾"))
        {
            return -102;
        }

        //检查要吃死刑了准备给
        if (TargetHelper.TargercastingIsDeathSentence(Core.Me.GetCurrTarget(), 10))
        {
            return 2;
        }


        //常关，只有死刑的时候才通过Check
        return -1;
    }

    public void Build(Slot slot)
    {
        if (!Core.Get<IMemApiSage>().Eukrasia()) slot.Add(SpellsDefine.Eukrasia.GetSpell());
        //刷单盾
        slot.Add(new Spell(SpellsDefine.EukrasianDiagnosis, Core.Me.GetCurrTargetsTarget()));
    }

}