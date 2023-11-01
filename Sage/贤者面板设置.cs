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


        // ImGui.Checkbox("治疗详细设置", ref 设置);
        // if (设置)
        // {
            ImGuiHelper.LeftInputInt("群奶数目", ref 贤者设置.实例.群奶数目);
            ImGuiHelper.LeftInputFloat("自生阈值", ref 贤者设置.实例.自生阈值, 0.01f);
            ImGuiHelper.LeftInputFloat("寄生阈值", ref 贤者设置.实例.寄生阈值, 0.01f);
            ImGuiHelper.LeftInputFloat("单海马阈值", ref 贤者设置.实例.单海马阈值, 0.01f);
            ImGuiHelper.LeftInputFloat("白牛阈值", ref 贤者设置.实例.白牛阈值, 0.01f);
            ImGuiHelper.LeftInputFloat("灵橡阈值", ref 贤者设置.实例.灵橡阈值, 0.01f);
            ImGuiHelper.LeftInputFloat("贤炮阈值", ref 贤者设置.实例.贤炮阈值, 0.01f);

        // }

        if (ImGui.Button("保存设置")) 贤者设置.实例.保存();
    }
}