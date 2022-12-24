using Terraria.ID;
using Terraria.DataStructures;
using VirtualDream.Contents.StarBound.Buffs;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Whips
{
    public class LucainesEnergyWhip : WhipBase_Item
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("一根由纯能量制成的鞭子，裂开吧！\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("鲁卡恩的能量鞭");
        }
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            type = ModContent.ProjectileType<LucainesEnergyWhipProj>();

            damage = 200;
            item.rare = MyRareID.Tier2;
        }
        public Item item => Item;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && player.CheckMana(50, true))
            {
                Projectile.NewProjectile(GetSource_StarboundWeapon(), player.Center, velocity * 8, ModContent.ProjectileType<LucainesEnergyWhipShock>(), (int)(damage * 1.2f), knockback, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 45;
                item.useTime = 45;
            }
            else
            {
                item.useAnimation = 30;
                item.useTime = 30;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.StaticCell>(30);
            recipe1.AddIngredient<Materials.AncientEssence>(1800);
            recipe1.AddIngredient(ItemID.LunarBar, 15);
            recipe1.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class LucainesEnergyWhipEX : LucainesEnergyWhip
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("一根由纯能量制成的鞭子，裂开吧！\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("鲁卡恩的能量鞭");
        }
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            base.WhipInfo(ref type, ref damage, ref knockBack, ref shootSpeed, ref animationTime);
            damage = 400;
            item.rare = MyRareID.Tier3;
        }
        public override WeaponState State => WeaponState.False_EX;
        public override void AddRecipes()
        {
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 30;
                item.useTime = 30;
            }
            else
            {
                item.useAnimation = 20;
                item.useTime = 20;
            }
            return true;
        }
    }
    public class LucainesEnergyWhipProj : WhipBase_Projectile
    {
        public override void WhipSettings(ref int segments, ref float rangeMultiplier)
        {
            if (Player.altFunctionUse == 2) rangeMultiplier *= 1.5f;
            rangeMultiplier *= UpgradeValue(1.25f, 1.5f, 1f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Electrified>(), 180);
        }
        //public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        //{
        //    //if (source is EntitySource_ItemUse eisource)
        //    //{
        //    //    if (eisource.Item.type == ModContent.ItemType<LucainesEnergyWhip>()) return normal;
        //    //    if (eisource.Item.type == ModContent.ItemType<LucainesEnergyWhipEX>()) return extra;
        //    //}

        //    if (sourceItemType == ModContent.ItemType<LucainesEnergyWhip>()) return normal;
        //    if (sourceItemType == ModContent.ItemType<LucainesEnergyWhipEX>()) return extra;
        //    if (Player.HeldItem.type == ModContent.ItemType<LucainesEnergyWhip>()) return normal;
        //    if (Player.HeldItem.type == ModContent.ItemType<LucainesEnergyWhipEX>()) return extra;
        //    Main.NewText($"Def拉你个大麻瓜 {sourceItemType} {Player.HeldItem.type} {ModContent.ItemType<LucainesEnergyWhip>()} {ModContent.ItemType<LucainesEnergyWhipEX>()}");

        //    return defaultValue;
        //}
        public override void AI_Other(float factor)
        {
            if (Main.rand.NextFloat() < factor / 2f)
            {
                ElectricTriangle.NewElectricTriangle(Projectile.WhipPointsForCollision[Projectile.WhipPointsForCollision.Count - 1], Main.rand.NextFloat(0, MathHelper.TwoPi));
            }
        }
    }
    public class LucainesEnergyWhipShock : StarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("鲁卡恩能量珠");
            Main.projFrames[projectile.type] = 11;
        }
        Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 80;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Melee;
            projectile.ignoreWater = true;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.light = 0.5f;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            projectile.frame = projectile.frameCounter / 5;
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            projectile.velocity *= 0.925f;
            projectile.alpha = 255 - (int)((1 - projectile.timeLeft / 60f).HillFactor2() * 255);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
        }
    }
}