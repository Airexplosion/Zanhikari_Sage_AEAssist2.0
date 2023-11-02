using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace  残光.贤者.GCD;
//这里是失衡和注药的判断
public class 贤者GCD_基础 : ISlotResolver
{
    public Spell GetSpell()
    {   //如果开了AOE选项
        if (Qt.GetQt("AOE"))
        {   //判断一下周围五米范围内是不是有2个以上的，有的话把失衡添加进slot
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
            if (aoeCount >= 2)
            //return SpellsDefine.Holy.GetSpell();//这里有个Holy是干啥的，不知道，抄了
            //这里是嵌套的获取失衡在各个等级的ID的替换
            {
                if (!(Core.Me.ClassLevel < 46)) ;//等级没到就不打
                {
                    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell();
                }
            }
        }
        ////如果在移动，且开了失衡走位，且GCD转到0了
        //if (Core.Get<IMemApiMove>().IsMoving()&& Qt.GetQt("失衡走位") && AI.Instance.GetGCDDuration()==0)
        //{   //那就返回失衡
        //    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell();
        //}
        //除此之外，只返回注药
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Dosis.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
            //如果获取到的是失衡，允许执行
        if (GetSpell() ==
            Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell()) return 0;
            //如果在移动，且不具有即刻
        if (Core.Get<IMemApiMove>().IsMoving()&&!Core.Me.HasMyAura(AurasDefine.Swiftcast))
        {   //假设开了失衡走位QT，就允许通过
            //if (Qt.GetQt("失衡走位"))
            //{
            //    return 0;
            //}
                //否则在移动且无即刻就不让过
                return -1;
        }
        //如果在移动，且获取到的技能不是失衡，那就不许打
        if (Core.Get<IMemApiMove>().IsMoving()&& GetSpell() != Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell())
            return -1;

            return 0;
    }

    public void Build(Slot slot)
    {   //获取一下spell，如果是null，就直接过了
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}