using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace VirtualDream.Contents.StarBound.Weapons
{
    public enum WeaponState : byte
    {
        False,
        False_EX,
        False_UL,
        True,
        True_EX,
        True_UL,
        Broken
    }
    /// <summary>
    /// 星界边境成长型武器对应的基类
    /// 以下是需要经常重写的属性
    /// State
    /// BossDrop
    /// UpgradeNeed
    /// </summary>
    public abstract class StarboundWeaponBase : ModItem
    {
        public const float defaultBrokenHurt = 150000f;
        public const int defaultBrokenKill = 500;

        public const float defaultNormalHurt = 500000f;
        public const int defaultNormalKill = 2000;

        public const float defaultExtraHurt = 2500000f;
        public const int defaultExtraKill = 4000;

        public const float defaultUltraHurt = 12500000f;
        public const int defaultUltraKill = 18000;
        public static (float, int) DefaultBroken => (defaultBrokenHurt, defaultBrokenKill);
        public static (float, int) DefaultNormal => (defaultNormalHurt, defaultNormalKill);
        public static (float, int) DefaultExtra => (defaultExtraHurt, defaultExtraKill);
        public static (float, int) DefaultUltra => (defaultUltraHurt, defaultUltraKill);


        public float hurtCount;
        public int killCount;
        public virtual WeaponState State => WeaponState.False;
        public virtual bool BossDrop => false;
        public EntitySource_StarboundWeapon GetSource_StarboundWeapon() => new(this);
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (Mod.HasAsset((Texture + "_Glow").Replace("VirtualDream/", "")))
                spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow", false), Item.Center - Main.screenPosition, null, Color.White, rotation, IllusionBoundMod.GetTexture(Texture + "_Glow", false).Size() * .5f, scale, 0, 0);
        }
        public virtual (float hurt, int kill) UpgradeNeed => State switch
        {
            WeaponState.Broken => DefaultBroken,
            WeaponState.False or WeaponState.True => DefaultNormal,
            WeaponState.False_EX or WeaponState.True_EX => DefaultExtra,
            WeaponState.False_UL or WeaponState.True_UL => DefaultUltra,
            _ => (0, 0)
        };
        public virtual float DamageFactor => 100000;
        public virtual float CritFactor => 10000;
        public virtual float PeriodMultiplyer
        {
            get
            {
                if (BossDrop)
                    return (byte)State switch
                    {
                        1 or 3 => 25,
                        2 or 4 => 60,
                        5 => 150,
                        6 => 5,
                        0 or _ => 9,
                    };
                return (byte)State switch
                {
                    1 => 60,
                    4 => 150,
                    6 => 5,
                    0 or 3 or _ => 25,
                };
            }

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(GetSource_StarboundWeapon(), position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        public virtual float UnitHealth => IllusionBoundExtensionMethods.HardmodeValue(1, 1.5f, 2f) * 50 * PeriodMultiplyer;
        public virtual float RealHurtCount => Math.Min(MaxLevel ? hurtCount : MathHelper.Clamp(hurtCount, 0, UpgradeNeed.hurt), (MaxLevel ? killCount : MathHelper.Clamp(killCount, 0, UpgradeNeed.kill)) * UnitHealth);
        public virtual bool MaxLevel => (byte)State % 3 == (BossDrop ? 2 : 1);
        public override void LoadData(TagCompound tag)
        {
            hurtCount = tag.GetFloat("hurtCount");
            killCount = tag.GetInt("killCount");
            base.LoadData(tag);
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("hurtCount", hurtCount);
            tag.Add("killCount", killCount);
            base.SaveData(tag);
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit *= 2 - 1 / (RealHurtCount / CritFactor + 1);
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage *= MathF.Log(RealHurtCount + MathF.E * DamageFactor) - MathF.Log(DamageFactor);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var hurtTip = new TooltipLine(Mod, "Hurt!", $"目前这把武器已经造成了{hurtCount}点伤害");
            var colorStart = Color.Gray;
            var colorMax = Color.Orange;
            if (MaxLevel) 
            {
                colorStart = Color.Orange;
                colorMax = Color.Lerp(Color.Orange,Color.Red, 0.5f + MathF.Sin((float)IllusionBoundModSystem.ModTime / 120f * MathHelper.Pi) * .5f);
            }
            hurtTip.OverrideColor = Color.Lerp(colorStart, colorMax, MathHelper.Clamp(Terraria.Utils.GetLerpValue(0, UpgradeNeed.hurt + 1, hurtCount, true), 0, 1));
            var killTip = new TooltipLine(Mod, "kill!", $"目前这把武器已经夺去{killCount}个生命");
            if (MaxLevel)
            {
                colorStart = Color.Red;
                colorMax = Color.Lerp(Color.Red, Color.DarkRed, 0.5f + MathF.Sin((float)IllusionBoundModSystem.ModTime / 120f * MathHelper.Pi) * .5f);
            }
            killTip.OverrideColor = Color.Lerp(colorStart, colorMax, MathHelper.Clamp(Terraria.Utils.GetLerpValue(0, UpgradeNeed.kill + 1, killCount, true), 0, 1));
            tooltips.Add(hurtTip);
            tooltips.Add(killTip);
            if (!MaxLevel)
            {
                bool canUpg = hurtCount >= UpgradeNeed.hurt && killCount >= UpgradeNeed.kill;
                var upgradeTip = new TooltipLine(Mod, "upgrade!", canUpg ? "已达成修复/升级条件" : $"还需造成{MathHelper.Clamp(UpgradeNeed.hurt - hurtCount, 0, float.MaxValue)}点伤害与{MathHelper.Clamp(UpgradeNeed.kill - killCount, 0, float.MaxValue)}击杀以达到修复/升级条件");
                upgradeTip.OverrideColor = canUpg ? (Main.hslToRgb(0.75f, 0.75f + MathF.Sin((float)IllusionBoundModSystem.ModTime / 120f * MathHelper.Pi) * .25f, .5f)) : Color.Gray;
                tooltips.Add(upgradeTip);
            }
        }
    }
    /// <summary>
    /// 星界边境成长型武器弹幕对应的基类
    /// </summary>
    public abstract class StarboundWeaponProjectile : ModProjectile
    {
        //0 赝品   | 赝品
        //1 赝品ex | 赝品ex
        //2 赝品ul | 真品
        //3 真品   | 真品ex
        //4 真品ex | 残品
        //4 真品ul
        //5 残品
        public virtual T UpgradeValue<T>(params T[] values)
        {
            try
            {
                if (weapon.BossDrop) return values[(byte)weapon.State];
                return values[(byte)weapon.State switch
                {
                    0 => 0,
                    1 => 1,
                    3 => 2,
                    4 => 3,
                    6 => 4,
                    _ => 0
                }];
            }
            catch
            {
                Main.NewText("兄啊，你自己的码都能写歪来啊");
                return default;
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_StarboundWeapon starboundWeapon)
            {
                weapon = starboundWeapon.Item;
            }
            if (source is EntitySource_ItemUse_WithAmmo _source)
            {
                _sourceItem = _source.Item;
            }
        }
        public StarboundWeaponBase weapon;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (weapon != null)
            {
                if (target.type == Terraria.ID.NPCID.TargetDummy) return;
                if (target.life - damage <= 0)
                {
                    weapon.killCount++;
                }
                weapon.hurtCount += damage;
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        private Item _sourceItem;
        public Item sourceItem => _sourceItem ?? weapon.Item;
        public Player Player => Main.player[Projectile.owner];
    }
    /// <summary>
    /// 星界边境成长型武器手持弹幕对应的基类
    /// 以下是需要经常重写的属性
    /// Charged
    /// FrameMax
    /// Factor
    /// </summary>
    public abstract class StarboundHeldProjectile : StarboundWeaponProjectile, IChannelProj
    {
        public virtual void OnCharging(bool left, bool right) { }
        public virtual void OnRelease(bool charged, bool left) { Projectile.Kill(); }
        public virtual bool UseLeft => true;
        public virtual bool UseRight => false;
        public virtual bool Charging => (UseLeft && Player.controlUseItem) || (UseRight && Player.controlUseTile);
        public virtual bool Charged => true;
        public virtual (int X, int Y) FrameMax => (1, 1);
        public virtual Texture2D GlowEffect
        {
            get
            {
                if (Mod.HasAsset(GlowTexture.Replace("VirtualDream/", "")))
                {
                    return IllusionBoundMod.GetTexture(GlowTexture, false);
                }
                return null;
            }
        }
        public virtual float Factor => 0;
        public virtual Color GlowColor => Color.White;
        public byte controlState;
        public Texture2D projTex => TextureAssets.Projectile[Projectile.type].Value;

    }
    public class EntitySource_StarboundWeapon : IEntitySource
    {
        public readonly Entity Entity;

        public readonly StarboundWeaponBase Item;

        public string Context
        {
            get;
        }

        public EntitySource_StarboundWeapon(StarboundWeaponBase item, string context = null)
        {
            Item = item;
            Context = context;
        }
    }
}
