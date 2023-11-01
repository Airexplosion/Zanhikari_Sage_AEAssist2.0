using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace �й�.����.������;
//���ε� ���˾���
public class ����������_���� : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //��ȴû�ò���
        if (!SpellsDefine.LucidDreaming.IsReady())
            return -1;
        //LogHelper.Info("MANA"+Core.Me.CurrentMana);

        //��������8000����
        if (Core.Me.CurrentMana > 8000) return -2;
        return 0;
    }

    public void Build(Slot slot)
    {   //��������
        slot.Add(SpellsDefine.LucidDreaming.GetSpell());
    }
}