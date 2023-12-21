#region

using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.GUI;
using Common.Language;
using ImGuiNET;
using AEAssist.MemoryApi;
using Common.Helper;
//using System;
//using System.Media;


#endregion

namespace 残光.贤者;

public class 贤者覆盖层界面
{
    private bool isHorizontal;
    //private bool openMusicPlayer = false;

    public void 更新日志(JobViewWindow jobViewWindow)
    {
        ImGui.Text($"该ACR仅能用于日随，如果你用它打高难奶死人了");
        ImGui.Text($"找我反馈别怪我骂你。");
        ImGui.Text($"11.4更新");
        ImGui.Text($"加入了自动康复逻辑、GCD奶逻辑和海马给死刑T的逻辑");
        ImGui.Text($"修复了道中小怪的dot逻辑、群奶和单奶的连续奶逻辑");
        ImGui.Text($"如果不想给低血量小怪上毒，请设置不上dot的血量百分比阈值");
        ImGui.Text($"日随有逻辑优化的部分记得找作者反馈，谢谢。");
        ImGui.Text($"顺带一提，别乱玩轮盘赌，真的会炸掉游戏");
        ImGui.Text($"有需要自行调节各个单奶/群奶的奶量阈值时");
        ImGui.Text($"请前往acr设置界面进行设置");
        ImGui.Text($"特别鸣谢F佬，Anmi，Ricky在撰写ACR时提供的帮助");
        ImGui.Text($"特别鸣谢Rio布鲁，Minami在测试ACR时提供的帮助");
    }



    public void DrawGeneral(JobViewWindow jobViewWindow)
    {

        if (ImGui.CollapsingHeader("插入技能状态"))
        {
            if (ImGui.Button("清除队列"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
            }

            ImGui.SameLine();
            if (ImGui.Button("清除一个"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
            }

            ImGui.Text("-------能力技-------");
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                    ImGui.Text(spell.Name);
            ImGui.Text("-------GCD-------");
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
                    ImGui.Text(spell.Name);
        }


    }

    //public void DrawTimeLine(JobViewWindow jobViewWindow)
    //{
    //    var currTriggerline = AI.Instance.TriggerlineData.CurrTriggerLine;
    //    var notice = "无";
    //    if (currTriggerline != null) notice = $"[{currTriggerline.Author}]{currTriggerline.Name}";

    //    ImGui.Text(notice);
    //    if (currTriggerline != null)
    //    {
    //        ImGui.Text("导出变量:".Loc());
    //        ImGui.Indent();
    //        foreach (var v in currTriggerline.ExposedVars)
    //        {
    //            var oldValue = AI.Instance.ExposedVars.GetValueOrDefault(v);
    //            ImGuiHelper.LeftInputInt(v, ref oldValue);
    //            AI.Instance.ExposedVars[v] = oldValue;
    //        }

    //        ImGui.Unindent();
    //    }
        
        
    //}

    public void DrawDev(JobViewWindow jobViewWindow)
    {



        if (ImGui.TreeNode("循环"))
        {
            ImGui.Text($"爆发药：{Qt.GetQt("爆发药")}");

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("技能释放"))
        {
            ImGui.Text($"上个技能：{Core.Get<IMemApiSpellCastSucces>().LastSpell}");
            ImGui.Text($"上个GCD：{Core.Get<IMemApiSpellCastSucces>().LastGcd}");
            ImGui.Text($"上个能力技：{Core.Get<IMemApiSpellCastSucces>().LastAbility}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("小队"))
        {
            ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
            ImGui.Text($"小队坦克数量：{PartyHelper.CastableTanks.Count}");
            ImGui.TreePop();
        }

        if (!ImGui.TreeNode("轮盘赌"))
            return;
        if (ImGui.Button("来一枪"))
            轮盘赌();
        ImGui.TreePop();

        void 轮盘赌()
        {
            if (new Random().Next(1, 7) == 1)
                Core.Get<IMemApiSendMessage>().SendMessage("/xlkill");
            else
                LogHelper.Print("没中");
        }





        //if (ImGui.Button("打开音乐播放器"))
        //{
        //    // 在按钮点击事件中，设置一个标志位，表示需要打开音乐播放器界面
        //    openMusicPlayer = true;
        //}


        //if (openMusicPlayer)
        //{
        //    DrawMusicPlayer();
        //}

    }
    //private void DrawMusicPlayer()
    //{

    //    ImGui.Begin("音乐播放器");


    //    // 在窗口中绘制本地音乐播放器的内容
    //    ImGui.Text("本地音乐播放器");
    //    if (ImGui.Button("播放"))
    //    {
    //        // 处理播放按钮的点击事件
    //        // 在这里可以添加播放音乐的逻辑
    //    }
    //    if (ImGui.Button("暂停"))
    //    {
    //        // 处理暂停按钮的点击事件
    //        // 在这里可以添加暂停音乐的逻辑
    //    }
    //    float volume = 0.5f;
    //    if (ImGui.SliderFloat("音量", ref volume, 0.0f, 1.0f))
    //    {
    //        // 处理音量滑动条的值变化事件
    //        // 在这里可以添加调整音量的逻辑
    //    }

    //    if (ImGui.Button("关闭"))
    //    {
    //        openMusicPlayer = false;
    //    }

    //    // 结束ImGui窗口
    //    ImGui.End();



    //    void PlayMusic(string filePath)
    //    {

    //        // 创建一个MediaPlayer对象
    //        SoundPlayer player = new SoundPlayer(filePath);

    //        // 播放音频
    //        player.Play();

    //        // 停止播放音频
    //        player.Stop();
    //    }
    //}

}

public static class Qt
{
    public static bool GetQt(string qtName)
    {
        return 贤者基础条目.职业视图窗口.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return 贤者基础条目.职业视图窗口.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return 贤者基础条目.职业视图窗口.SetQt(qtName, qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        贤者基础条目.职业视图窗口.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        贤者基础条目.职业视图窗口.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        贤者基础条目.职业视图窗口.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return 贤者基础条目.职业视图窗口.GetQtArray();
    }
}