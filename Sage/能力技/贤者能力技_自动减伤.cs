using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace �й�.����.������;
//������� Ⱥ���� ���� �����۵ĵ����߼�
public class ����������_�Զ����� : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {  //����QT��������
        if (!Qt.GetQt("����"))
        {
            return -101;
        }

        //����QT��������
        if (!Qt.GetQt("����"))
        {
            return -100;
        }
        //getspell������Kardia˵�����õļ��ܶ�����ȴ Ҳ����
        if (Getspell()==SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }
        //�ж����ڶ������ǲ���AOE �ǵĻ���׼���ż���

        if (!Core.Me.GetCurrTarget().IsNull() && Core.Me.GetCurrTarget().IsCasting)
        {
            if (Core.Me.GetCurrTarget().TotalCastTime - Core.Me.GetCurrTarget().CurrentCastTime < 10.0)
            {
                if (Core.Me.GetCurrTarget().CastingSpellId.GetSpell().IsBossAoe())
                {
                    return 2;
                }
            }
                    
        }

        //ֻ����Ҫ���˵ĵط�������Ϊ��ţ��������˻��⣬���Բ���Ҫһֱ����
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Getspell());
    }

    Spell Getspell()
    {   //������Ӻ��˶����� ������
        if (SpellsDefine.Kerachole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>=1)
        {
            //��������к���/�����۵�buff���Ͳ�����
            if (Core.Me.HasAura(2613) || Core.Me.HasAura(3003))
            {

                return SpellsDefine.Kardia.GetSpell();
                
            }

            return SpellsDefine.Kerachole.GetSpell();
        }
        //����û�÷�Ⱥ����
        if (SpellsDefine.Panhaima.IsReady())
        {
            //�������������/�����۵�buff���Ͳ�����
            if (Core.Me.HasAura(3003) || Core.Me.HasAura(2938))
            {

                return SpellsDefine.Kardia.GetSpell();
            }

            return SpellsDefine.Panhaima.GetSpell();
        }
        //Ⱥ����û�÷�������
        if (SpellsDefine.Holos.IsReady())
        {
            //�������������/Ⱥ�����buff���Ͳ����� id2938 ���� 2613Ⱥ���� 3003������
            if (Core.Me.HasAura(2938) || Core.Me.HasAura(2613))
            {

                return SpellsDefine.Kardia.GetSpell();
            }

            return SpellsDefine.Holos.GetSpell();
            
        }


        //�����з���kardia ��check�Լ�����
        return SpellsDefine.Kardia.GetSpell();
    }
}