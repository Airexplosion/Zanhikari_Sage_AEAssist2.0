using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace �й�.����;

public class �����¼����� : IRotationEventHandler
{

    // public void OnEnterRotation()
    // {
    //     LogHelper.Print("��ACR�����������棬���������������������ˣ����ҷ�����������㡣");
    //     LogHelper.Print("11.1���� - �������ܣ��������߼��Ż��Ĳ��ּǵ������߷�����лл��");
    //     LogHelper.Print("˳��һ�ᣬ���̶���Ļ�ը��Ϸ�������档");
    // }
    public void OnResetBattle()
    {
        ����ս������.Instance.Reset();
        Qt.Reset();
    }
    
    public Task OnPreCombat()
    {
        return Task.CompletedTask;
    }

    public async Task OnNoTarget()
    {   //�������QT���ˣ��ͷ���
        if (!Qt.GetQt("����"))
        {
            return;
        }
        //��ȡ����ʩ�ż��ܵ�̹�˶����е�һ��û����ĵ��ܵĶ��� id2607��buff�Ǿ������
        if (!PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasMyAura(2607)).IsNull())
        {   
            var slot = new Slot();
            //�������û���ã�����һ�¾��⣬������ԣ�ֱ�ӵ���
            if (!Core.Get<IMemApiSage>().Eukrasia())
            {
                slot.Add(SpellsDefine.Eukrasia.GetSpell());
            }
            slot.Add(new Spell(SpellsDefine.EukrasianDiagnosis, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasMyAura(2607))));
            //ִ�в��ȴ�
            await slot.Run(false);
        }
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
        switch (spell.Id)
        {
        }

        switch (spell.Id)
        {
            case SpellsDefine.Eukrasia:
                AI.Instance.BattleData.LimitAbility = true;
                break;
            default:
                AI.Instance.BattleData.LimitAbility = false;
                break;
        }
    }

    public void OnBattleUpdate(int currTime)
    {
        // var �б� = Core.Get<IMemApiData>().HighEndTerritory();
        // if (!�б�.Any(t => t.Item1 == Core.Get<IMemApiZoneInfo>().GetCurrTerrId()))
        // {
        //     LogHelper.Print("Ѿϲ�����������ѣ�������Ϸը�˰���");
        //     // Core.Get<IMemApiSendMessage>().SendMessage("/xlkill");
        //     
        // }
    }
}