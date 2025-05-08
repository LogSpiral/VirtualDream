using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.BaseClasses;
using System;
using Terraria.ID;
using VirtualDream.Contents.StarBound.Buffs;
using VirtualDream.Contents.StarBound.Materials;
using VirtualDream.Contents.StarBound.TimeBackTracking;
using VirtualDream.Contents.StarBound.Weapons.Broken;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.IxodoomClaw
{
    public class IxodoomClaw_Broken : IxodoomClaw 
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 40;
            item.shoot = ModContent.ProjectileType<IxodoomClaw_BrokenProj>();
            item.useTime = 45;
            item.rare = ModContent.RarityType<BrokenRarity>();
        }
        public override WeaponState State => WeaponState.Broken;
        public override bool AltFunctionUse(Player player) => false;
        public override WeaponRepairRecipe RepairRecipe()
        {
            var recipe = GetEmptyRecipe();
            recipe.AddIngredient(ItemID.SpiderFang, 50);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.VialofVenom, 50);
            recipe.AddIngredient(ItemID.Stinger, 30);
            recipe.AddIngredient(ItemID.StyngerBolt, 30);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.SetResult<IxodoomClaw>();
            return recipe;
        }
    }
    public class IxodoomClaw_BrokenProj : VertexHammerProj, IStarboundWeaponProjectile
    {

        public override float MaxTime => Player.itemAnimationMax;
        public override string Texture => base.Texture.Replace("BrokenProj", "Broken");
        public override Vector2 CollidingSize => base.CollidingSize * 2;

        public override Vector2 CollidingCenter => new(projTex.Size().X / 3 - 16, 16);
        public override Vector2 DrawOrigin => base.DrawOrigin + new Vector2(-16, 12);
    }
    public class IxodoomClaw : StarboundWeaponBase
    {
        public override WeaponRepairRecipe RepairRecipe()
        {
            return GetEmptyRecipe().AddIngredient<AncientEssence>(5000).SetResult<IxodoomClawEX>();
        }
        public override bool BossDrop => true;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("死亡主宰爪");
        }
        public Item item => Item;

        public override void SetDefaults()
        {
            item.damage = 200;
            item.crit = 21;
            item.DamageType = DamageClass.Melee;
            item.width = 88;
            item.height = 84;
            item.rare = MyRareID.Tier1;
            item.useTime = 30;
            item.useAnimation = 30;
            item.knockBack = 6;
            item.useStyle = 1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<IxodoomClawProj>();
            item.shootSpeed = 1f;
            item.noUseGraphic = true;
            item.noMelee = true;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
    public class IxodoomClawEX : IxodoomClaw
    {
        public override WeaponRepairRecipe RepairRecipe() => GetEmptyRecipe();
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("死亡主宰爪EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.crit = 32;
            item.rare = MyRareID.Tier2;
        }
    }
    public class IxodoomClawHT : IxodoomClaw
    {
        public override WeaponState State => WeaponState.False_UL;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n它的刀刃上附着着高度的剧毒\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("死亡主宰爪HT");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 450;
            item.crit = 50;
            item.rare = MyRareID.Tier3;
        }
        public override void HoldItem(Player player)
        {
            int d = MyDustId.PinkBubble;
            float W = (3.1415926f / 180) * (float)VirtualDreamSystem.ModTime2;
            for (int n = 0; n < 8; n++)
            {
                Dust dust1 = Dust.NewDustPerfect(player.Center + (0.5f * 7 * W + MathHelper.TwoPi * n / 8).ToRotationVector2() * (float)(Math.Tan(4 * W) / Math.Sin(4 * W) + 8) * 25, d, new Vector2(0f, 0f), 0, Color.White, 1.5f);
                dust1.noGravity = true;
            }
        }
    }
    public class IxodoomClawProj : VertexHammerProj,IStarboundWeaponProjectile
    {
        public override string HammerName => base.HammerName;
        public override float MaxTime => (controlState == 2 ? 2f : 1f) * this.UpgradeValue(30, 24, 18);
        public override float Factor => base.Factor;
        public override Vector2 CollidingSize => base.CollidingSize * 2;
        //public override Vector2 projCenter => base.projCenter + new Vector2(Player.direction * 16, -16);
        public override Vector2 CollidingCenter => base.CollidingCenter;//new Vector2(projTex.Size().X / 3 - 16, 16)
        public override Vector2 DrawOrigin => base.DrawOrigin + new Vector2(-12, 12);
        public override Color color => base.color;
        public override Color VertexColor(float time) => default;
        public override float MaxTimeLeft => (controlState == 2 ? 0.75f : 1f) * this.UpgradeValue(10, 8, 7);
        public override float Rotation => base.Rotation;

        public override bool UseRight => true;
        public override (int X, int Y) FrameMax => (3, 1);
        public override void OnKill(int timeLeft)
        {
            int max = (int)(30 * Factor);
            var vec = (CollidingCenter - DrawOrigin).RotatedBy(Rotation) + projCenter;
            if (Factor > 0.75f)
            {
                for (int n = 0; n < max; n++)
                {
                    Dust.NewDustPerfect(vec, this.UpgradeValue(MyDustId.YellowHallowFx, MyDustId.GreenFXPowder, MyDustId.PinkBubble), (MathHelper.TwoPi / max * n).ToRotationVector2() * Main.rand.NextFloat(2, 8)).noGravity = true;
                }
            }
            //if (factor == 1)
            //{
            //    Projectile.NewProjectile(projectile.GetSource_FromThis(), vec, default, ModContent.ProjectileType<HolyExp>(), player.GetWeaponDamage(player.HeldItem) * 3, projectile.knockBack, projectile.owner);
            //}
            base.OnKill(timeLeft);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (controlState == 2 && Player.CheckMana(this.UpgradeValue(20, 30, 50), true))
            {
                target.AddBuff(this.UpgradeValue(ModContent.BuffType<ToxicⅠ>(), ModContent.BuffType<ToxicⅡ>(), ModContent.BuffType<ToxicⅢ>()), this.UpgradeValue(600, 1200, 1800));
            }
        }
        public override Rectangle? frame => projTex.Frame(3, 1, this.UpgradeValue(0, 1, 2));
    }

}