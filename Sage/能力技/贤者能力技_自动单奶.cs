using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace 残光.贤者.能力技;
//这里包含 白牛 灵橡 海马 混合
public class 贤者能力技_自动单奶 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //QT奶人关了就过
        if (!Qt.GetQt("奶人"))
        {
            return -100;
        }

        //getspell是Kardia也过 说明都进冷却了
        if (Getspell() == SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }

        //豆子留一个
        if (Core.Get<IMemApiSage>().Addersgall() < 2)
            return -6;


        //如果周围三十米范围内小队成员 没死的 血量低于阈值的 大于等于1个 就准备开奶 单海马单独判断，只给T,排除死而不僵的,行尸走肉的 

        List<uint> 排除buff = new List<uint>
        {
            3255,//排除出死入生
            AurasDefine.Holmgang,//排除死斗
            AurasDefine.Superbolide//排除超火流星
        };

        var 海马目标血量 = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.单海马阈值 && r.IsTank() &&
                                              !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();

        var 白牛目标血量 = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.白牛阈值 &&
                                              !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();

        var 灵橡目标血量 = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.灵橡阈值 &&
                                              !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();



        if (海马目标血量.IsValid)
        {
            return 5;
        
        }

        if (白牛目标血量.IsValid)
        {
            return 3;
        }

        if (灵橡目标血量.IsValid)
        {
            return 2;
        }

        //常关 只有上面那个过了才会奶
        return -1;
    }

    public void Build(Slot slot)
    {   //把获取到的技能加入slot 目标设定为 小队列表中血量最低的那个



        //对普通人的
        var 技能目标 = PartyHelper.CastableAlliesWithin30   //周围三十米范围
        .Where(r => r.CurrentHealth > 0)   //没死的
        .OrderBy(r => r.CurrentHealthPercent) //当前健康百分比排序
        .FirstOrDefault();

        //对T的目标设置
        var 技能目标对T = PartyHelper.CastableAlliesWithin30
        .Where(r => r.CurrentHealth > 0 && r.IsTank())
        .OrderBy(r => r.CurrentHealthPercent)
        .FirstOrDefault();


        //如果获取的是胖海马，那就对T用 在判断有没有混合给一下
        if (Getspell() == SpellsDefine.Haima.GetSpell())
        {
            if (SpellsDefine.Krasis.IsReady()) slot.Add(SpellsDefine.Krasis.GetSpell());
            slot.Add(new Spell(Getspell().Id, 技能目标对T));
        }
        //其他情况正常给最低血的
        else slot.Add(new Spell(Getspell().Id, 技能目标));
    }

    Spell Getspell()
    {   //设定一下几个目标
        List<uint> 排除buff = new List<uint>
        {
            3255,//排除出死入生
            AurasDefine.Holmgang,//排除死斗
            AurasDefine.Superbolide//排除超火流星
        };
        
        var 海马目标 = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.单海马阈值 && r.IsTank() &&
                        !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        var 白牛目标 = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.白牛阈值 &&
                        !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();

        var 灵橡目标 = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= 贤者设置.实例.灵橡阈值 &&
                        !r.HasAura(AurasDefine.LivingDead) && !r.HasAura(AurasDefine.WalkingDead) && !r.HasAnyAura(排除buff, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();

        //如果单海马冷却好了
        if (SpellsDefine.Haima.IsReady())
        {   //看看存在不
            if (海马目标.IsValid)
            {   //是的话就把海马加进去了
                    return SpellsDefine.Haima.GetSpell();
            }
        }
        //如果海马冷却没好，就看看这个白牛好没好 顺便检查一下豆子够不
        if (SpellsDefine.Taurochole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>=1)
        {   //看看血量最低那人是不是特低
            if (白牛目标.IsValid)
            {   //是的话就把白牛加进去了
                return SpellsDefine.Taurochole.GetSpell();
            }
        }
        //如果海马 白牛 冷却没好，就看看这个灵橡好没好 顺便检查一下豆子够不

        if (SpellsDefine.Druochole.IsReady()&& Core.Get<IMemApiSage>().Addersgall()>=1)
        {   //看看血量最低那人是不是特低
            if (灵橡目标.IsValid)
            {   //是的话就把灵橡加进去了
                return SpellsDefine.Druochole.GetSpell();
            }
        }

        //都不行就返回个Kardia 让check判断跳过了

        return SpellsDefine.Kardia.GetSpell();
    }
}