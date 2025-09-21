using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class HardChakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("硬环刃");
            // Tooltip.SetDefault("坚固，沉重，而残忍地有效。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.shoot = ProjectileType<HardChakramProj>();
            item.height = item.width = 34;
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.HardenedCarapace>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.HardenedCarapace>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }

    public class HardChakramEX : HardChakram
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("硬环刃EX");
            // Tooltip.SetDefault("坚固，沉重，而残忍地有效。\n沉重到能够轻易地击飞对方。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 400;
            item.rare = MyRareID.Tier3;
            item.value *= 5;
        }

        public override WeaponState State => WeaponState.False_EX;

        public override bool Extra => true;

        public override void AddRecipes()
        {
        }
    }

    public class HardChakramProj : ChakramBaseProjectile
    {
        public override void AI()
        {
            if (Projectile.ai[0] == 1 && Projectile.timeLeft > 15)
            {
                Projectile.rotation = (float)VirtualDreamSystem.ModTime;

                var player = Main.player[Projectile.owner];
                int dust = Projectile.ai[1] == 1 ? MyDustId.GoldMaterial : MyDustId.ThinBrown;
                Projectile.Center = player.Center + new Vector2(128, 0).RotatedBy(-(float)VirtualDreamSystem.ModTime2 / 6);

                player.statDefense += Projectile.ai[1] == 1 ? 12 : 8;
                Dust dust1 = Dust.NewDustPerfect(player.Center + new Vector2(64, 0).RotatedBy((float)VirtualDreamSystem.ModTime2 / 4), dust, new Vector2(0, 0), 0, Color.White);
                Dust dust2 = Dust.NewDustPerfect(player.Center + new Vector2(64, 0).RotatedBy((float)VirtualDreamSystem.ModTime2 / 4 + MathHelper.Pi), dust, new Vector2(0, 0), 0, Color.White);
                dust1.noGravity = true;
                dust2.noGravity = true;
                Dust dust3 = Dust.NewDustPerfect(Projectile.Center + new Vector2(32, 0).RotatedBy((float)VirtualDreamSystem.ModTime2 / 4), dust, new Vector2(0, 0), 0, Color.White);
                Dust dust4 = Dust.NewDustPerfect(Projectile.Center + new Vector2(32, 0).RotatedBy((float)VirtualDreamSystem.ModTime2 / 4 + MathHelper.Pi), dust, new Vector2(0, 0), 0, Color.White);
                dust3.noGravity = true;
                dust4.noGravity = true;

                for (int n = Projectile.oldPos.Length - 1; n > 0; n--)
                {
                    Projectile.oldPos[n] = Projectile.oldPos[n - 1];
                    Projectile.oldRot[n] = Projectile.oldRot[n - 1];
                }
                Projectile.oldPos[0] = Projectile.Center;
                Projectile.oldRot[0] = Projectile.rotation;
            }
            else
                base.AI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (target.CanBeChasedBy())
            {
                var vec = Projectile.oldPos[0] - Projectile.oldPos[1];
                if (vec.Length() > 128) vec = Projectile.velocity;
                target.velocity += vec.SafeNormalize(default) * 16;
            }
        }
    }
}