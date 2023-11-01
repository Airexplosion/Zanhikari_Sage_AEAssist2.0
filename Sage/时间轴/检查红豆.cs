using CombatRoutine.TriggerModel;
using Common;
using Common.Define;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace 残光.贤者.时间轴
{
    public class 检查红豆 : ITriggerCond
    {
        public string Remark { get; set; }
        
        public CompareType CompareType = CompareType.LessEqual;

        public int Count;
        public string DisplayName => "SGE/检测蛇胆".Loc();
        
        public void Check()
        {
            
        }

        public bool Draw()
        {
            ImGui.Text("蛇胆数量");
            ImGui.SameLine();
            ImGuiHelper.DrawEnum("", ref CompareType, size: 50, nameMap: CompareUtil.CompareTypeNameMap);
            ImGui.SameLine();
            ImGuiHelper.LeftInputInt("",ref Count);
            return true;
        }

        public bool Handle(ITriggerCondParams triggerCondParams)
        {
            var currValue = Core.Get<IMemApiSage>().Addersgall();
            switch (CompareType)
            {
                case CompareType.Equal:
                    return currValue == Count;
                case CompareType.NotEqual:
                    return currValue != Count;
                case CompareType.Greater:
                    return currValue > Count ;
                case CompareType.GreaterEqual:
                    return currValue >= Count ;
                case CompareType.Less:
                    return currValue < Count ;
                case CompareType.LessEqual:
                    return currValue <= Count ;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}