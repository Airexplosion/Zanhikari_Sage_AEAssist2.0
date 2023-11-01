using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace �й�.����.������;

//�������������֭�������ĵ����߼�
public class ����������_�Զ�Ⱥ�� : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        //QT���˹��˾�����
        if (!Qt.GetQt("Ⱥ��"))
        {
            return -102;
        }
         //QT���˹��˾�����
        if (!Qt.GetQt("����"))
        {
            return -100;
        }
        //�����GetSpell���ȡ���ļ�����Kardia Ҳ��������˵������������µĿɵ��õļ��ܶ�������ȴ��
        if (Getspell()==SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }

        //��� С�Ӷ�Ա.��Χ30�׷�Χ.��Ѫ������0 �� Ѫ�������趨�и���������ֵ��ֵ�Ľ�ɫ���� ���� �趨��Ⱥ����Ŀ ��ִ��
        if (PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.������ֵ) >= ��������.ʵ��.Ⱥ����Ŀ ||
            PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.������ֵ) >= ��������.ʵ��.Ⱥ����Ŀ)
            LogHelper.Print($"��Χ30�׵�����ֵ��ͨ��");
            return 2;
        //���أ�ֻ����Ҫ�̵�ʱ���ͨ��Check
        return -1;
    }

    public void Build(Slot slot)
    {   //����ȡ���ļ��ܼ���slot�ͷ�
        slot.Add(Getspell());
    }

    Spell Getspell()
    {   //С�Ӷ�Ա.��Χ30�׷�Χ.��Ѫ������0�Ľ�ɫ��Ŀ
        var Ҫ�������̵� = PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.������ֵ);
        var Ҫ�������̵� = PartyHelper.CastableAlliesWithin30.Count(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.������ֵ);
        //������������μ���
        //��� ������֭������ȴ
        if (SpellsDefine.PhysisIi.IsReady()||SpellsDefine.Physis.IsReady())
        {   //�����Χ30�׷�Χ.��Ѫ������0�ҵ�������������ֵ�Ľ�ɫ��Ŀ�����趨��PPֵ
            if (Ҫ�������̵� >= ��������.ʵ��.Ⱥ����Ŀ)
            {   //�ͷ������������ܶ���
                LogHelper.Print($"����ͨ��");
                return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.PhysisIi.GetSpell().Id).GetSpell();
            }
        }
        //��� ������֭������ȴ �� ���Ӵ���1��������һ�����Ӹ���ǣ�
        else if (SpellsDefine.Ixochole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>1)
        {   //�����Χ30�׷�Χ.��Ѫ������0�ҵ��ڼ���������ֵ�Ľ�ɫ��Ŀ�����趨��PPֵ
            if (Ҫ�������̵� >= ��������.ʵ��.Ⱥ����Ŀ)
            {   //�ͷ��ؼ�����֭�����ܶ���
                LogHelper.Print($"����ͨ��");
                return SpellsDefine.Ixochole.GetSpell();
            }
        }


        //���򷵻�Kardia������ܣ��ᱻ�����Check�ж������������ж�
        return SpellsDefine.Kardia.GetSpell();
    }
}