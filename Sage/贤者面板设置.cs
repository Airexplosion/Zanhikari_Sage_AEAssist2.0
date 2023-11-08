using CombatRoutine.View;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace 残光.贤者;

public class 贤者面板设置 : ISettingUI
{
    public string Name => "贤者";
    private bool 设置;
    public void Draw()
    {
        /*ImGui.Checkbox(Language.Instance.ToggleWildFireFirst,
            ref MCHSettings.Instance.WildfireFirst);

        ImGuiHelper.LeftInputInt(Language.Instance.InputStrongGcdCheckTime,
            ref MCHSettings.Instance.StrongGCDCheckTime, 1000, 10000, 1000);*/
        ImGui.Text("此ACR最好只用来打日随，你要是想用这玩意打高难，那算你牛逼");
        ImGui.Text("按住Ctrl左键单击滑块可以直接输入数字");


        // ImGui.Checkbox("治疗详细设置", ref 设置);
        // if (设置)
        // {
        ImGuiHelper.LeftInputInt("群奶数目", ref 贤者设置.实例.群奶数目);
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("自生阈值", ref 贤者设置.实例.自生阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("寄生阈值", ref 贤者设置.实例.寄生阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("输血阈值", ref 贤者设置.实例.单海马阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("白牛阈值", ref 贤者设置.实例.白牛阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("灵橡阈值", ref 贤者设置.实例.灵橡阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("贤炮阈值", ref 贤者设置.实例.贤炮阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("GCD群奶阈值", ref 贤者设置.实例.GCD群奶阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }

        if (ImGui.SliderFloat("GCD单奶阈值", ref 贤者设置.实例.GCD单奶阈值, 0.0f, 0.99f))
        {
            贤者设置.实例.保存();
        }
        ImGui.Text("这个滑块是用来设置血量低于多少时不给目标上dot的。");
        if (ImGui.SliderFloat("不上dot阈值", ref 贤者设置.实例.不上dot阈值, 0.0f, 1.0f))
        {
            贤者设置.实例.保存();
        }

        
        ImGui.Text("点击此按钮设置为默认阈值设置");
        if (ImGui.Button("默认设置"))
        {
            贤者设置.实例.自生阈值 = 0.70f;
            贤者设置.实例.寄生阈值 = 0.65f;
            贤者设置.实例.单海马阈值 = 0.75f;
            贤者设置.实例.白牛阈值 = 0.5f;
            贤者设置.实例.灵橡阈值 = 0.45f;
            贤者设置.实例.贤炮阈值 = 0.65f;
            贤者设置.实例.GCD群奶阈值 = 0.50f;
            贤者设置.实例.GCD单奶阈值 = 0.40f;
            贤者设置.实例.群奶数目 = 2;
            Qt.NewDefault("群盾", false);
            Qt.SetQt("群盾", false);
            Qt.NewDefault("单盾", false);
            Qt.SetQt("单盾", false);
            贤者设置.实例.保存();
        }

        ImGui.Text("点击此按钮设置为本分奶阈值设置");
        if (ImGui.Button("本分奶设置"))
        {
            贤者设置.实例.自生阈值 = 0.80f;
            贤者设置.实例.寄生阈值 = 0.75f;
            贤者设置.实例.单海马阈值 = 0.75f;
            贤者设置.实例.白牛阈值 = 0.65f;
            贤者设置.实例.灵橡阈值 = 0.7f;
            贤者设置.实例.贤炮阈值 = 0.65f;
            贤者设置.实例.GCD群奶阈值 = 0.6f;
            贤者设置.实例.GCD单奶阈值 = 0.55f;
            贤者设置.实例.群奶数目 = 2;
            Qt.NewDefault("群盾", true);
            Qt.SetQt("群盾", true);
            Qt.NewDefault("单盾", true);
            Qt.SetQt("单盾", true);
            贤者设置.实例.保存();
        }

        ImGui.Text("点击此按钮设置为输出奶阈值设置");
        if (ImGui.Button("输出奶设置"))
        {
            贤者设置.实例.自生阈值 = 0.60f;
            贤者设置.实例.寄生阈值 = 0.55f;
            贤者设置.实例.单海马阈值 = 0.75f;
            贤者设置.实例.白牛阈值 = 0.55f;
            贤者设置.实例.灵橡阈值 = 0.5f;
            贤者设置.实例.贤炮阈值 = 0.5f;
            贤者设置.实例.GCD群奶阈值 = 0.2f;
            贤者设置.实例.GCD单奶阈值 = 0.1f;
            贤者设置.实例.群奶数目 = 3;
            Qt.NewDefault("群盾", false);
            Qt.SetQt("群盾", false);
            Qt.NewDefault("单盾", false);
            Qt.SetQt("单盾", false);
            贤者设置.实例.保存();
        }

        // }
    }
}