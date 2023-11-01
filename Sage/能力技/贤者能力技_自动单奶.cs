using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace �й�.����.������;
//������� ��ţ ���� ���� ���
public class ����������_�Զ����� : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //QT���˹��˾͹�
        if (!Qt.GetQt("����"))
        {
            return -100;
        }

        //getspell��KardiaҲ�� ˵��������ȴ��
        if (Getspell() == SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }

        //������һ��
        if (Core.Get<IMemApiSage>().Addersgall() < 2)
            return -6;


        //�����Χ��ʮ�׷�Χ��С�ӳ�Ա û���� Ѫ��������ֵ�� ���ڵ���1�� ��׼������ ���������жϣ�ֻ��T,�ų�����������,��ʬ����� 

        List<uint> �ų�buff = new List<uint>
        {
            3255,//�ų���������
            AurasDefine.Holmgang,//�ų�����
            AurasDefine.Superbolide//�ų���������
        };

        var ����Ŀ��Ѫ�� = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.��������ֵ && r.IsTank() &&
                                              !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(�ų�buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();

        var ��ţĿ��Ѫ�� = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.��ţ��ֵ &&
                                              !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(�ų�buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();

        var ����Ŀ��Ѫ�� = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ��������.ʵ��.������ֵ &&
                                              !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(�ų�buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();



        if (����Ŀ��Ѫ��.IsValid||
            ��ţĿ��Ѫ��.IsValid||
            ����Ŀ��Ѫ��.IsValid)
            return 2;
        //���� ֻ�������Ǹ����˲Ż���
        return -1;
    }

    public void Build(Slot slot)
    {   //�ѻ�ȡ���ļ��ܼ���slot Ŀ���趨Ϊ С���б���Ѫ����͵��Ǹ�



        //����ͨ�˵�
        var ����Ŀ�� = PartyHelper.CastableAlliesWithin30   //��Χ��ʮ�׷�Χ
        .Where(r => r.CurrentHealth > 0)   //û����
        .OrderBy(r => r.CurrentHealthPercent) //��ǰ�����ٷֱ�����
        .FirstOrDefault();

        //��T��Ŀ������
        var ����Ŀ���T = PartyHelper.CastableAlliesWithin30
        .Where(r => r.CurrentHealth > 0 && r.IsTank())
        .OrderBy(r => r.CurrentHealthPercent)
        .FirstOrDefault();


        //�����ȡ�����ֺ����ǾͶ�T�� ���ж���û�л�ϸ�һ��
        if (Getspell() == SpellsDefine.Haima.GetSpell())
        {
            if (SpellsDefine.Krasis.IsReady()) slot.Add(SpellsDefine.Krasis.GetSpell());
            slot.Add(new Spell(Getspell().Id, ����Ŀ���T));
        }
        //����������������Ѫ��
        else slot.Add(new Spell(Getspell().Id, ����Ŀ��));
    }

    Spell Getspell()
    {   //�趨һ��Ѫ����ֵ�ı��� С���б�.��֪����ɶ.����.����ֵ�ٷֱ�
        var С����ͽ�ɫѪ�� = PartyHelper.CastableParty.FirstOrDefault().CurrentHealthPercent;

        //�����������ȴ����
        if (SpellsDefine.Haima.IsReady())
        {   //����Ѫ����������ǲ����ص�
            if (С����ͽ�ɫѪ�� <= ��������.ʵ��.��������ֵ)
            {   //�ǵĻ��ͰѺ���ӽ�ȥ��
                    return SpellsDefine.Haima.GetSpell();
            }
        }
        //���������ȴû�ã��Ϳ��������ţ��û�� ˳����һ�¶��ӹ���
        if (SpellsDefine.Taurochole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>=1)
        {   //����Ѫ����������ǲ����ص�
            if (С����ͽ�ɫѪ�� <= ��������.ʵ��.��ţ��ֵ)
            {   //�ǵĻ��ͰѰ�ţ�ӽ�ȥ��
                return SpellsDefine.Taurochole.GetSpell();
            }
        }
        //������� ��ţ ��ȴû�ã��Ϳ�����������û�� ˳����һ�¶��ӹ���

        if (SpellsDefine.Druochole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>=1)
        {   //����Ѫ����������ǲ����ص�
            if (С����ͽ�ɫѪ�� <= ��������.ʵ��.������ֵ)
            {   //�ǵĻ��Ͱ�����ӽ�ȥ��
                return SpellsDefine.Druochole.GetSpell();
            }
        }

        //�����оͷ��ظ�Kardia ��check�ж�������

        return SpellsDefine.Kardia.GetSpell();
    }
}