using CombatRoutine;
using Common;
using Common.Define;

namespace 残光.贤者.GCD;
//复活的调用
public class 贤者GCD_复活 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //即刻没转好不拉
        if (!SpellsDefine.Swiftcast.IsReady()) return -3;
        //拉人QT没开不拉
        if (!Qt.GetQt("拉人")) return -2;
        //蓝量小于2400不拉
        if (Core.Me.CurrentMana < 2400) return -2;
        //死人身上已经有复活buff了不拉
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        if (!skillTarget.IsValid) return -1;
        //其他情况 常开，随时准备拉
        return 1;
    }

    public void Build(Slot slot)
    {   //把死了的人加进目标
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        //即刻加入slot
        slot.Add(SpellsDefine.Swiftcast.GetSpell());
        //复活目标加入slot
        slot.Add(new Spell(SpellsDefine.Egeiro, skillTarget));
    }
}