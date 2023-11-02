using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;
//这里只包含单海马死刑相关
public class 贤者能力技_单海马死刑 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {    //等级不到跳了
        if (Core.Me.ClassLevel< 70) return -3;
        //QT奶人关了就过
        if (!Qt.GetQt("奶人"))
        {
            return -100;
        }
        //海马没好跳了
        if (!SpellsDefine.Haima.IsReady())return -3 ;

        //检查要吃死刑了准备给
        if (DeathSentenceHelper.IsDeathSentence(Core.Me.GetCurrTarget())) return 1;
        //检查是否有满足条件的目标
        CharacterAgent 单海马目标;
        单海马目标 = PartyHelper.CastableAlliesWithin30.FirstOrDefault(r =>
            r.CurrentHealth > 0 && r.IsTank());

        if (!单海马目标.IsValid) return -2;

        return -1;//常关，只有死刑才给挂
    }

    public void Build(Slot slot)
    {

        var 技能目标 = Core.Me.GetCurrTargetsTarget();
        slot.Add(new Spell(SpellsDefine.Haima, 技能目标));

    }
}