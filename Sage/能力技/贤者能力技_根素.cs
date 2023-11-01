using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;
//根素的，缺了就用
public class 贤者能力技_根素 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {  //等级低于76直接别用了，根素没学会呢
        if (Core.Me.ClassLevel < 76) return -3;
        //冷却没好不用
        if (!SpellsDefine.Rhizomata.IsReady())
            return -1;
        //LogHelper.Info("MANA"+Core.Me.CurrentMana);

        //豆子大于等于2颗不用
        if (Core.Get<IMemApiSage>().Addersgall() >= 2) return -2;

        return 0;
    }

    public void Build(Slot slot)
    {   //根素
        slot.Add(SpellsDefine.Rhizomata.GetSpell());
    }
}