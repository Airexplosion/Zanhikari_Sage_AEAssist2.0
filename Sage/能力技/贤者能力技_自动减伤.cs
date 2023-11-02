using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;
//这里包含 群海马 罩子 整体论的调用逻辑
public class 贤者能力技_自动减伤 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {  //减伤QT关了跳过
        if (!Qt.GetQt("减伤"))
        {
            return -101;
        }

        //奶人QT关了跳过
        if (!Qt.GetQt("奶人"))
        {
            return -100;
        }
        //getspell返回了Kardia说明调用的技能都在冷却 也跳过
        if (Getspell()==SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }
        //判断现在读条的是不是AOE 是的话就准备放技能

        if (!Core.Me.GetCurrTarget().IsNull() && Core.Me.GetCurrTarget().IsCasting)
        {
            if (Core.Me.GetCurrTarget().TotalCastTime - Core.Me.GetCurrTarget().CurrentCastTime < 10.0)
            {
                if (Core.Me.GetCurrTarget().CastingSpellId.GetSpell().IsBossAoe())
                {
                    return 2;
                }
            }
                    
        }

        //只在需要减伤的地方开，因为白牛和这个减伤互斥，所以不需要一直开着
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Getspell());
    }

    Spell Getspell()
    {   //如果罩子好了豆子有 放罩子
        if (SpellsDefine.Kerachole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>=1)
        {
            //如果身上有海马/整体论的buff，就不放了
            if (Core.Me.HasAura(2613) || Core.Me.HasAura(3003))
            {

                return SpellsDefine.Kardia.GetSpell();
                
            }

            return SpellsDefine.Kerachole.GetSpell();
        }
        //罩子没好放群海马
        if (SpellsDefine.Panhaima.IsReady())
        {
            //如果身上有罩子/整体论的buff，就不放了
            if (Core.Me.HasAura(3003) || Core.Me.HasAura(2938))
            {

                return SpellsDefine.Kardia.GetSpell();
            }

            return SpellsDefine.Panhaima.GetSpell();
        }
        //群海马没好放整体论
        if (SpellsDefine.Holos.IsReady())
        {
            //如果身上有罩子/群海马的buff，就不放了 id2938 罩子 2613群海马 3003整体论
            if (Core.Me.HasAura(2938) || Core.Me.HasAura(2613))
            {

                return SpellsDefine.Kardia.GetSpell();
            }

            return SpellsDefine.Holos.GetSpell();
            
        }


        //都不行返回kardia 让check自己跳过
        return SpellsDefine.Kardia.GetSpell();
    }
}