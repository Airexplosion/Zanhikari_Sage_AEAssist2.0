#region

using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;



#endregion

namespace �й�.����.GCD;

public class ����GCD_���� : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (!SpellsDefine.Pneuma.IsReady()) return -10;//����ȴ����

        if (!Qt.GetQt("����"))//���QT�ǹصģ���AOE�򣬾��η�Χ���оʹ�û�о͵���Ҫ���ٴ�
        {   //���ģ��Ͳ�������߼�
            if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 25, 4) >= 2)
                return 2;
        }   
        if (Qt.GetQt("����"))//���QT���ˣ�
        {
            if (PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.������ֵ) >= ��������.ʵ��.Ⱥ����Ŀ)
                return 3;

            else if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 25, 4) >= 2)
                return 4;
        }


        //�����ƶ� / ����ӽ�� �Ŵ�
        if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.Swiftcast)) return -2;
        return -2;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Pneuma.GetSpell());
    }

}