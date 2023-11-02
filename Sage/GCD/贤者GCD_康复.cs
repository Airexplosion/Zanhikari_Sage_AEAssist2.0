using CombatRoutine;
using Common;
using Common.Define;

namespace 残光.贤者.GCD;
//复活的调用
public class 贤者GCD_康复 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        //康复QT没开不康复
        if (!Qt.GetQt("自动康复")) return -2;
        //在移动不康复
        if (Core.Get<IMemApiMove>().IsMoving()) return -3;
        //有人身上有可以解除的buff了，准备康复
        if (PartyHelper.CastableAlliesWithin30.Any(agent => agent.HasCanDispel())) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Esuna, PartyHelper.CastableAlliesWithin30.FirstOrDefault(agent => agent.HasCanDispel())));
    }
}