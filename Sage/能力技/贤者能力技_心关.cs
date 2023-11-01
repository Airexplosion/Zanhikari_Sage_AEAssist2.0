using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;
//自动心关的
public class 贤者能力技_心关 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //自动心关关了跳过
        if (!Qt.GetQt("自动心关")) return -2;
        //心关在冷却 跳过
        if (!SpellsDefine.Kardia.IsReady())
            return -1;
        //目标的目标有心关 跳过
        if (Core.Me.GetCurrTargetsTarget().HasMyAura(AurasDefine.Kardion)) return -3;
        //目标的目标是坦克 执行
        if (Core.Me.GetCurrTargetsTarget().IsTank()) return 1;
        //除此之外 都不管
        return -1;
    }

    public void Build(Slot slot)
    {   //把目标的目标塞进技能目标释放心关
        slot.Add(new Spell(SpellsDefine.Kardia, Core.Me.GetCurrTargetsTarget()));
    }
}