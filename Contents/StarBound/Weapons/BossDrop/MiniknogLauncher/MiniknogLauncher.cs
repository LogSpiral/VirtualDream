using Terraria.ID;
using Terraria.DataStructures;
using System;
using LogSpiralLibrary;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.MiniknogLauncher
{
    public class MiniknogLauncher : StarboundWeaponBase
    {
        public override bool BossDrop => true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("科技发展部发射器");
            Tooltip.SetDefault("微型导弹发射器，由科技发展部的顶级科学家开发。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        //{
        //    if (Mod.HasAsset((Texture + "_Glow").Replace("VirtualDream/", "")))
        //        spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow", false), item.Center - Main.screenPosition, null, Color.White, rotation, IllusionBoundMod.GetTexture(Texture + "_Glow", false).Size() * .5f, scale, 0, 0);
        //}
        public Item item => Item;
        public override bool CanConsumeAmmo(Item ammo, Player player) => player.ownedProjectileCounts[item.shoot] > 0;
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public override void SetDefaults()
        {
            item.damage = 125;
            item.knockBack = 0.25f;
            item.rare = MyRareID.Tier1;
            item.useStyle = ItemUseStyleID.Shoot;
            item.useAmmo = AmmoID.Rocket;
            item.DamageType = DamageClass.Ranged;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.width = 44;
            item.height = 28;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<MiniknogLauncherProj>();
            item.shootSpeed = 1f;
            item.channel = true;
            item.autoReuse = true;
            item.useTime = 20;
            item.useAnimation = 20;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(GetSource_StarboundWeapon(), position, velocity, item.shoot, damage, knockback, player.whoAmI);
            return false;
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    Vector2 vec = Main.MouseWorld - player.Center;
        //    vec = Vector2.Normalize(vec) * 16;
        //    var Dam = player.GetWeaponDamage(item);
        //    if (player.altFunctionUse == 2)
        //    {
        //        Time++;
        //        if (Time >= 20)
        //        {
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketG>(), Dam / 4 * 3, 5f, player.whoAmI);
        //            Time = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (player.channel)
        //        {
        //            Time++;
        //        }
        //        else
        //        {
        //            if (Time >= 80)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 24).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketE>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 24).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 24 + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 24 - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 60)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketE>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 40)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 24).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 24).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 20)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //        if (Time % 20 == 0)
        //        {
        //            var rect = player.Hitbox;
        //            rect.Offset(0, -64);
        //            if (Time == 20)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "穿墙导弹填充完毕", true)].velocity.Y = -1;
        //            }
        //            if (Time == 40)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "折射导弹填充完毕", true)].velocity.Y = -1;
        //            }
        //            if (Time == 60)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "Buff导弹填充完毕", true)].velocity.Y = -1;
        //            }
        //            if (Time == 80)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "爆炸导弹填充完毕", true)].velocity.Y = -1;
        //                rect.Offset(0, -32);
        //                Main.combatText[CombatText.NewText(rect, Color.Blue, "所有导弹填充完毕！", true)].velocity.Y = -2;
        //            }
        //        }

        //    }
        //}
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.RocketI, 50);
            recipe1.AddIngredient(ItemID.RocketII, 50);
            recipe1.AddIngredient(ItemID.RocketIII, 50);
            recipe1.AddIngredient(ItemID.RocketIV, 50);
            recipe1.AddIngredient(ItemID.TitaniumBar, 30);
            recipe1.AddIngredient(ItemID.SnowmanCannon);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class MiniknogLauncherEX : MiniknogLauncher
    {
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("科技发展部发射器EX");
            Tooltip.SetDefault("微型导弹发射器，由科技发展部的顶级科学家开发。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 225;
            item.rare = MyRareID.Tier2;
            item.width = 48;
            item.height = 28;
        }
        public override void AddRecipes()
        {
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    Vector2 vec = Main.MouseWorld - player.Center;
        //    vec = Vector2.Normalize(vec) * 16;
        //    if (player.altFunctionUse == 2)
        //    {
        //        Time++;
        //        if (Time >= 16)
        //        {
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketG>(), Dam / 4 * 3, 5f, player.whoAmI);
        //            Time = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (player.channel)
        //        {
        //            Time++;
        //        }
        //        else
        //        {
        //            if (Time >= 64)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketE>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 48)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 32)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 16)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //        if (Time % 16 == 0)
        //        {
        //            var rect = player.Hitbox;
        //            rect.Offset(0, -64);
        //            if (Time == 16)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "穿墙导弹填充完毕", true)].velocity.Y = -1;
        //            }
        //            if (Time == 32)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "折射导弹填充完毕", true)].velocity.Y = -1;
        //            }
        //            if (Time == 48)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "Buff导弹填充完毕", true)].velocity.Y = -1;
        //            }
        //            if (Time == 64)
        //            {
        //                Main.combatText[CombatText.NewText(rect, Color.Cyan, "爆炸导弹填充完毕", true)].velocity.Y = -1;
        //                rect.Offset(0, -32);
        //                Main.combatText[CombatText.NewText(rect, Color.Blue, "所有导弹填充完毕！", true)].velocity.Y = -2;
        //            }
        //        }
        //    }
        //}
    }
    public class MiniknogLauncherLT : MiniknogLauncher
    {
        public override WeaponState State => WeaponState.False_UL;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("科技发展部发射器LT");
            Tooltip.SetDefault("微型导弹发射器，由科技发展部的顶级科学家开发。\n极限科技(LimitTechnology)\n你以为是大猿人用tr的奇妙科技造的？其实是由河童工程师改造的(\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 450;
            item.rare = MyRareID.Tier3;
            item.width = 48;
            item.height = 36;
        }
        //public override bool AltFunctionUse(Player player)
        //{
        //    return true;
        //}
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    Vector2 vec = Main.MouseWorld - player.Center;
        //    vec = Vector2.Normalize(vec) * 16;
        //    if (player.altFunctionUse == 2)
        //    {
        //        Time++;
        //        if (Time >= 8)
        //        {
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketG>(), Dam / 4 * 3, 5f, player.whoAmI);
        //            Time = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (player.channel)
        //        {
        //            Time++;
        //        }
        //        else
        //        {
        //            if (Time >= 32)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketE>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 24)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 16)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 8)
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //        if (Time == 8)
        //        {
        //            Main.NewText("穿墙导弹填充完毕");
        //        }
        //        if (Time == 16)
        //        {
        //            Main.NewText("折射导弹填充完毕");
        //        }
        //        if (Time == 24)
        //        {
        //            Main.NewText("Buff导弹填充完毕");
        //        }
        //        if (Time == 32)
        //        {
        //            Main.NewText("爆炸导弹填充完毕");
        //            Main.NewText("[c/00FFFF:导弹填充完毕！]");
        //        }
        //    }
        //}
        public override void HoldItem(Player player)
        {
            var theta = (float)VirtualDreamMod.ModTime2 * MathHelper.Pi / 180f;
            float a = 256f * (float)(Math.Sin(6 * theta) + 0.5);
            //TimeD += MathHelper.Pi / 180;
            for (int n = 0; n < 6; n++)
            {
                Dust.NewDustPerfect(player.Center + (theta + MathHelper.Pi / 3 * n).ToRotationVector2() * a, MyDustId.CyanBubble, newColor: Color.White).noGravity = true;
            }
            //float X1 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 0) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float X2 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 1) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float X3 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 2) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float X4 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 3) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float X5 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 4) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float X6 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 5) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float Y1 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 0) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float Y2 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 1) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float Y3 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 2) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float Y4 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 3) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float Y5 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 4) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //float Y6 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 5) * (float)(Math.Sin(6 * TimeD) + 0.5f);
            //Dust dust1 = Dust.NewDustPerfect(player.Center + new Vector2(X1, Y1) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust2 = Dust.NewDustPerfect(player.Center + new Vector2(X2, Y2) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust3 = Dust.NewDustPerfect(player.Center + new Vector2(X3, Y3) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust4 = Dust.NewDustPerfect(player.Center + new Vector2(X4, Y4) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust5 = Dust.NewDustPerfect(player.Center + new Vector2(X5, Y5) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust6 = Dust.NewDustPerfect(player.Center + new Vector2(X6, Y6) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //dust1.noGravity = true;
            //dust2.noGravity = true;
            //dust3.noGravity = true;
            //dust4.noGravity = true;
            //dust5.noGravity = true;
            //dust6.noGravity = true;
        }
        public override void AddRecipes()
        {
        }
    }
    public class MiniknogLauncherProj : RangedHeldProjectile, IStarboundWeaponProjectile
    {
        //BossDropWeaponProj<ErchiusEye, ErchiusEyeEX, ErchiusEyeDL>
        public override Vector2 HeldCenter => base.HeldCenter + Projectile.velocity * 6;//Main.MouseWorld - Player.Center
        public override bool UseRight => true;
        public override void OnCharging(bool left, bool right)
        {
            if ((int)Projectile.ai[0] % this.UpgradeValue(20, 16, 12) == 0 && Projectile.ai[0] != 0)
            {
                if (right)
                {
                    ShootRocket(Projectile.velocity * 16, 5);
                    SoundEngine.PlaySound(SoundID.Item62);
                }

                else if ((int)Projectile.ai[0] <= this.UpgradeValue(80, 64, 48) && Player.PickAmmo(((IStarboundWeaponProjectile)this).sourceItem, out int _, out float _, out int _, out float _, out int _))
                {
                    var str = "";
                    switch ((int)Projectile.ai[0] / this.UpgradeValue(20, 16, 12))
                    {
                        case 1:
                            str = "穿墙";
                            break;
                        case 2:
                            str = "折射";
                            break;
                        case 3:
                            str = "Buff";
                            break;
                        case 4:
                            str = "爆炸";
                            break;
                    }
                    //CombatText.NewText(Player.Hitbox.Offset(new Point(0,48)),Color.Cyan,);
                    var rect = Player.Hitbox;
                    rect.Offset(0, -64);
                    var index = CombatText.NewText(rect, Color.Cyan, str + "导弹填充完毕", true);
                    if (index >= 0 && index < 100)
                    {
                        var text = Main.combatText[index];
                        text.velocity.Y = -16;
                        text.lifeTime /= this.UpgradeValue(3, 4, 5);
                    }
                    if ((int)Projectile.ai[0] == this.UpgradeValue(80, 64, 48))
                    {
                        rect.Offset(0, -32);
                        index = CombatText.NewText(rect, Color.Blue, "所有导弹填充完毕！", true);
                        if (index >= 0 && index < 100)
                        {
                            var text = Main.combatText[index];
                            text.velocity.Y = -16;
                            text.lifeTime /= this.UpgradeValue(3, 4, 5);
                        }
                    }
                }

            }
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 20;
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / this.UpgradeValue(80f, 64f, 48f), 0, 1);
            }
        }
        //public override bool PreDraw(ref Color lightColor)
        //{
        //    return base.PreDraw(ref lightColor);
        //}
        public override void OnRelease(bool charged, bool left)
        {
            if (!left) return;
            int tier = (int)(Factor * 4);
            int upgradeState = this.UpgradeValue(0, 1, 2);
            var vec = Projectile.velocity * 16;

            switch (tier)
            {
                case 1:
                    switch (upgradeState)
                    {
                        case 0:
                            ShootRocket(vec);
                            break;
                        case 1:
                        case 2:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 36));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 36));
                            break;
                    }
                    break;
                case 2:
                    switch (upgradeState)
                    {
                        case 0:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 24));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 24), 1);
                            break;
                        case 1:
                        case 2:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 12));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 12));
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 36), 1);
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 36), 1);
                            break;
                    }
                    break;
                case 3:
                    switch (upgradeState)
                    {
                        case 0:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 12));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 12), 1);
                            ShootRocket(vec, 2);
                            break;
                        case 1:
                        case 2:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 36 * 5));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 36 * 5));
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 12), 1);
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 12), 1);
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 36), 2);
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 36), 2);
                            break;
                    }
                    break;
                case 4:
                    switch (upgradeState)
                    {
                        case 0:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 8));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 8), 1);
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 24), 2);
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 24), 3);
                            break;
                        case 1:
                        case 2:
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 6));
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 6));
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 9), 1);
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 9), 1);
                            ShootRocket(vec.RotatedBy(-MathHelper.Pi / 18), 2);
                            ShootRocket(vec.RotatedBy(MathHelper.Pi / 18), 2);
                            ShootRocket(vec, 3);
                            break;
                    }
                    break;

            }
            if (tier > 0)
                SoundEngine.PlaySound(SoundID.Item62);

        }
        private void ShootRocket(Vector2 vel, int ai0 = 0)
        {
            Projectile.NewProjectile(((IStarboundWeaponProjectile)this).weapon.GetSource_StarboundWeapon(), ShootCenter, vel, ModContent.ProjectileType<MiniknogRocket>(), Projectile.damage, 5f, Projectile.owner, ai0);
            //0穿墙1折射2爆炸3buff4小爆炸5追踪//6普通爆炸效果//7小爆炸效果//8buff爆炸效果
        }
        public override (int X, int Y) FrameMax => (4, 3);
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            frame = texture.Frame(FrameMax.X, FrameMax.Y, (int)MathHelper.Clamp(Projectile.ai[0], 0, controlState == 1 ? this.UpgradeValue(80, 64, 48) : int.MaxValue) / this.UpgradeValue(5, 4, 3) % 4, this.UpgradeValue(0, 1, 2));
            origin = new Vector2(5, 10);
            scale = 2f;
        }
        public override Color GlowColor => base.GlowColor;//base.GlowColor * (MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(40f, 30f, 20f), 1, 2) - 1)
    }
    public class MiniknogRocket : ModProjectile, IStarboundWeaponProjectile
    {
        Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("科技发展部导弹");
        }
        public override bool PreKill(int timeLeft)
        {
            //projectile.type = 140;
            return true;
        }
        private void ShootTinyRocket()
        {
            Projectile.NewProjectile(((IStarboundWeaponProjectile)this).weapon.GetSource_StarboundWeapon(), projectile.Center, Main.rand.NextVector2Unit() * 16, projectile.type, projectile.damage / 2, 5f, projectile.owner, 4);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            if ((int)projectile.ai[0] == 3 || (int)projectile.ai[0] == 8)
            {
                target.AddBuff(44, 300);
                target.AddBuff(189, 300);
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.immuneTime = 0;
            if ((int)projectile.ai[0] == 3 || (int)projectile.ai[0] == 8)
            {
                target.AddBuff(44, 300);
                target.AddBuff(189, 300);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int n = 0; n < 30; n++)
            {
                Dust.NewDustPerfect(projectile.Center, MyDustId.ElectricCyan, Main.rand.NextVector2Unit() * 2 + projectile.velocity * .125f).noGravity = true;
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            }
            switch ((int)projectile.ai[0])
            {
                case 0:
                case 1:
                case 5:
                    var p1 = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, projectile.type, projectile.damage, 5f, projectile.owner, 6);
                    p1.height = p1.width = 160;
                    p1.timeLeft = 2;
                    p1.penetrate = -1;
                    p1.Center = projectile.Center;
                    SoundEngine.PlaySound(SoundID.Item74);
                    break;
                case 3:
                    p1 = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, projectile.type, projectile.damage, 5f, projectile.owner, 8);
                    p1.height = p1.width = 160;
                    p1.timeLeft = 2;
                    p1.penetrate = -1;
                    p1.Center = projectile.Center;
                    SoundEngine.PlaySound(SoundID.Item74);
                    break;
                case 4:
                    p1 = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, projectile.type, projectile.damage, 5f, projectile.owner, 7);
                    p1.height = p1.width = 80;
                    p1.timeLeft = 2;
                    p1.Center = projectile.Center;
                    p1.penetrate = -1;
                    SoundEngine.PlaySound(SoundID.Item74);

                    break;
                case 2:
                    for (int i = 0; i <= 3; i++)
                    {
                        ShootTinyRocket();
                    }
                    if (Main.rand.Next(10) < 5)
                    {
                        ShootTinyRocket();
                    }
                    if (Main.rand.Next(10) < 3)
                    {
                        ShootTinyRocket();
                    }
                    if (Main.rand.Next(10) < 1)
                    {
                        ShootTinyRocket();
                    }
                    p1 = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, projectile.type, projectile.damage, 5f, projectile.owner, 6);
                    p1.height = p1.width = 160;
                    p1.timeLeft = 2;
                    p1.penetrate = -1;
                    p1.Center = projectile.Center;
                    SoundEngine.PlaySound(SoundID.Item74);

                    break;
                case 6:
                case 8:
                    for (int n = 0; n < 30; n++)
                    {
                        Dust.NewDustPerfect(projectile.Center, MyDustId.CyanBubble, (n / 30f * MathHelper.TwoPi).ToRotationVector2() * 2, newColor: Color.White).noGravity = true;
                    }
                    break;
                case 7:
                    for (int n = 0; n < 15; n++)
                    {
                        Dust.NewDustPerfect(projectile.Center, MyDustId.CyanBubble, (n / 30f * MathHelper.TwoPi).ToRotationVector2(), newColor: Color.White).noGravity = true;
                    }
                    break;

            }

            //Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, ModContent.ProjectileType<ApeBossMissileExp>(), 10, 0, Main.myPlayer).hostile = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if ((int)projectile.ai[0] != 1)
                return true;

            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight((int)((projectile.position.X + projectile.width / 2) / 16f), (int)((projectile.position.Y + projectile.height / 2) / 16f), 78f / 255f, 139f / 255f, 240f / 255f);
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            if (projectile.timeLeft == 300)
            {
                if ((int)projectile.ai[0] == 0) projectile.tileCollide = false;
                if ((int)projectile.ai[0] == 4) projectile.timeLeft = 60;
                for (int n = projectile.oldPos.Length - 1; n > -1; n--)
                {
                    projectile.oldPos[n] = projectile.Center;
                }
            }
            if ((int)projectile.ai[0] == 5)
            {
                NPC target = null;
                float distanceMax = 1000f;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy)
                    {
                        float currentDistance = Vector2.Distance(npc.Center, projectile.Center);
                        if (currentDistance < distanceMax)
                        {
                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 targetVec = target.Center - projectile.Center;
                    targetVec.Normalize();
                    targetVec *= 60f;
                    projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
                }
            }
            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
            {
                projectile.oldPos[n] = projectile.oldPos[n - 1];
                projectile.oldRot[n] = projectile.oldRot[n - 1];
            }
            projectile.oldPos[0] = projectile.Center;
            projectile.oldRot[0] = projectile.rotation;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if ((int)projectile.ai[0] > 5) return false;
            SpriteBatch spriteBatch = Main.spriteBatch;
            DrawingMethods.DrawShaderTail(spriteBatch, projectile, LogSpiralLibraryMod.HeatMap[7].Value, LogSpiralLibraryMod.AniTex[2].Value, LogSpiralLibraryMod.BaseTex[12].Value, (int)projectile.ai[0] == 4 ? 10 : 20);
            //VirtualDreamDrawMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, Width: (int)projectile.ai[0] == 4 ? 10 : 20);
            if (projectile.timeLeft > 1)
            {
                spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(10, 7), (int)projectile.ai[0] == 4 ? 0.5f : 1, 0, 0);
            }
            return false;
        }
    }
}