using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者;

public class 贤者事件处理 : IRotationEventHandler
{

    // public void OnEnterRotation()
    // {
    //     LogHelper.Print("该ACR仅能用于日随，如果你用它打高难奶死人了，找我反馈别怪我骂你。");
    //     LogHelper.Print("11.1更新 - 基本能跑，日随有逻辑优化的部分记得找作者反馈，谢谢。");
    //     LogHelper.Print("顺带一提，轮盘赌真的会炸游戏，别乱玩。");
    // }
    public void OnResetBattle()
    {
        贤者战斗数据.Instance.Reset();
        Qt.Reset();
    }
    
    public Task OnPreCombat()
    {
        return Task.CompletedTask;
    }

    public async Task OnNoTarget()
    {   //如果奶人QT关了，就返回
        if (!Qt.GetQt("奶人"))
        {
            return;
        }

        if (!Qt.GetQt("单奶"))
        {
            return;
        }

        if (!Qt.GetQt("单盾"))
        {
            return;
        }
        if (Core.Me.ClassLevel < 30) return;
        //获取可以施放技能的坦克队友中第一个没有你的单盾的对象 id2607的buff是均衡诊断
        if (!PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasMyAura(2607)).IsNull())
        {   
            var slot = new Slot();
            //如果单盾没法用，则用一下均衡，如果可以，直接单盾
            if (!Core.Get<IMemApiSage>().Eukrasia())
            {
                slot.Add(SpellsDefine.Eukrasia.GetSpell());
            }
            slot.Add(new Spell(SpellsDefine.EukrasianDiagnosis, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasMyAura(2607))));
            //执行并等待
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
        // var 列表 = Core.Get<IMemApiData>().HighEndTerritory();
        // if (!列表.Any(t => t.Item1 == Core.Get<IMemApiZoneInfo>().GetCurrTerrId()))
        // {
        //     LogHelper.Print("丫喜欢用这个打高难？给你游戏炸了啊！");
        //     // Core.Get<IMemApiSendMessage>().SendMessage("/xlkill");
        //     
        // }
    }
}