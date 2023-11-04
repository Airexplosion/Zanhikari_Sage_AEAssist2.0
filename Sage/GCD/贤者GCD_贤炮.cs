#region

using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;



#endregion

namespace 残光.贤者.GCD;

public class 贤者GCD_贤炮 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //虽然isredy包含，但等级不到还是不打
        if (Core.Me.ClassLevel < 90) return -3;

        if (!SpellsDefine.Pneuma.IsReady()) return -10;//在冷却跳过

        //不在移动 / 不需咏唱 才打
        if (Core.Get<IMemApiMove>().IsMoving() || Core.Me.HasAura(AurasDefine.Swiftcast)) return -2;

        if (!Qt.GetQt("治疗"))//如果QT是关的，则当AOE打，矩形范围内有就打，没有就等需要奶再打
        {   //开的，就不走这个逻辑
            if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 25, 3) >= 2)
                return 2;
        }   
        if (Qt.GetQt("治疗"))//如果QT开了，
        {
            if (PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.贤炮阈值) >= 贤者设置.实例.群奶数目)
            {
                return 2;
            }

            else if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 25, 3) >= 2)
                return 4;
        }


        
        return -2;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Pneuma.GetSpell());
    }

}