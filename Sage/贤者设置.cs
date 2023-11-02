using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

namespace 残光.贤者;

public class 贤者设置
{
    public static 贤者设置 实例;
    private static string 文件路径;

    public static void Build(string settingPath)
    {
        文件路径 = Path.Combine(settingPath, "贤者设置.json");
        if (!File.Exists(文件路径))
        {
            实例 = new 贤者设置();
            实例.保存();
            return;
        }

        try
        {
            实例 = JsonHelper.FromJson<贤者设置>(File.ReadAllText(文件路径));
        }
        catch (Exception e)
        {
            实例 = new 贤者设置();
            LogHelper.Error(e.ToString());
        }
    }
    //这里是默认的阈值
    public float 自生阈值 = 0.7f;
    public float 寄生阈值 = 0.65f;
    public float 单海马阈值 = 0.6f;
    public float 白牛阈值 = 0.5f;
    public float 灵橡阈值 = 0.4f;
    public float 贤炮阈值 = 0.65f;

    public int 群奶数目 = 2;
    
    public JobViewSave 职业视图保存 = new(){MainColor = new Vector4(40 / 255f, 173 / 255f, 70 / 255f, 0.8f)};
    public Dictionary<string, object> StyleSetting = new();
    public void 保存()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(文件路径));
        File.WriteAllText(文件路径, JsonHelper.ToJson(this));
    }
}