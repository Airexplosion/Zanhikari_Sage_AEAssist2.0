using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace �й�.����.������;
//���صģ�ȱ�˾���
public class ����������_���� : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {  //�ȼ�����76ֱ�ӱ����ˣ�����ûѧ����
        if (Core.Me.ClassLevel < 76) return -3;
        //��ȴû�ò���
        if (!SpellsDefine.Rhizomata.IsReady())
            return -1;
        //LogHelper.Info("MANA"+Core.Me.CurrentMana);

        //���Ӵ��ڵ���2�Ų���
        if (Core.Get<IMemApiSage>().Addersgall() >= 2) return -2;

        return 0;
    }

    public void Build(Slot slot)
    {   //����
        slot.Add(SpellsDefine.Rhizomata.GetSpell());
    }
}