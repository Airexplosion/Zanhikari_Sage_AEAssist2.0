using System.Reflection;
using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common.Define;
using Common.Language;
using 残光.贤者;
using 残光.贤者.GCD;
using 残光.贤者.能力技;
using 残光.贤者.时间轴;

namespace 残光;

public class 贤者基础条目 : IRotationEntry
{
    public static JobViewWindow 职业视图窗口;
    
    private readonly 贤者覆盖层界面 _lazyOverlay = new();
    public string OverlayTitle { get; } = "日随用贤者";

    public void DrawOverlay()
    {
        
    }

    public string AuthorName { get; } = "Zanhikari";
    public Jobs TargetJob { get; } = Jobs.Sage;


    public List<ISlotResolver> SlotResolvers = new()
    {
        new 贤者GCD_复活(),
        new 贤者GCD_康复(),
        new 贤者能力技_自动减伤(),
        new 贤者能力技_自动群奶(),
        new 贤者能力技_单海马死刑(),
        new 贤者能力技_自动单奶(),

        new 贤者能力技_醒梦(),
        new 贤者能力技_心关(),
        new 贤者能力技_根素(),

        new 贤者GCD_Dot(),
        new 贤者GCD_发炎(),
        new 贤者GCD_箭毒(),
        new 贤者GCD_贤炮(),
        new 贤者GCD_群盾(),
        new 贤者GCD_预后(),
        new 贤者GCD_诊断(),
        new 贤者GCD_基础()


    };

    public Rotation Build(string settingFolder)
    {
        贤者设置.Build(settingFolder);
        return new Rotation(this, () => SlotResolvers)
            .SetRotationEventHandler(new 贤者事件处理())
            .AddSettingUIs(new 贤者面板设置())
            .AddSlotSequences();
            //.AddTriggerAction(new ZanhikariSgeQt())
            //.AddTriggerCondition(new CheckAddersgall());
    }

    public void OnLanguageChanged(LanguageType languageType)
    {
    }
    
    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
        jobViewWindow = new JobViewWindow(贤者设置.实例.职业视图保存, 贤者设置.实例.保存, OverlayTitle);
        职业视图窗口 = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        jobViewWindow.AddTab("轮盘赌", _lazyOverlay.自毁);
        jobViewWindow.AddQt("奶人", true);
        jobViewWindow.AddQt("AOE", true);
        jobViewWindow.AddQt("DOT", true);
        jobViewWindow.AddQt("发炎", true);
        jobViewWindow.AddQt("红豆", true);
        jobViewWindow.AddQt("保留1红豆", false);
        jobViewWindow.AddQt("自动心关", true);
        jobViewWindow.AddQt("拉人", true);
        jobViewWindow.AddQt("群盾", true);
        jobViewWindow.AddQt("减伤", true);
        jobViewWindow.AddQt("群奶", true);
        jobViewWindow.AddQt("自动康复", true);

        //jobViewWindow.AddQt("失衡走位", false);

        jobViewWindow.AddHotkey("群盾",new HotKeyResolver_NormalSpell(24292,SpellTargetType.Self,false));
        jobViewWindow.AddHotkey("LB",new HotKeyResolver_NormalSpell(24859,SpellTargetType.Self,false));
        jobViewWindow.AddHotkey("拯救", new HotKeyResolver_NormalSpell(24294, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("消化", new HotKeyResolver_NormalSpell(24301, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("群盾接消化", new HotkeyResolver_General("../../RotationPlugin/Zanhikari/Resources/群盾接消化.png",
    () =>
    {
        AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(SpellsDefine.Eukrasia.GetSpell());
        AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(SpellsDefine.EukrasianPrognosis.GetSpell());
        AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellsDefine.Pepsis.GetSpell());
    }));


        return true;
    }
}