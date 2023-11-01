namespace 残光.贤者;

public class 贤者战斗数据
{
    public static 贤者战斗数据 Instance = new();

    public void Reset()
    {
        Instance = new 贤者战斗数据();
    }
}