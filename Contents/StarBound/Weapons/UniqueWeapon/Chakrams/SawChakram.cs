using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class SawChakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("锯环刃");
            // Tooltip.SetDefault("遵循圣律的伐木僧们最喜欢的武器。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.rare = MyRareID.Tier2;
            item.shoot = ProjectileType<SawChakramProj>();
            item.height = item.width = 34;
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.SharpenedClaw>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.SharpenedClaw>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }

    public class SawChakramEX : SawChakram
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("锯环刃EX");
            // Tooltip.SetDefault("遵循圣律的伐木僧们最喜欢的武器。\n就用这个锯下那最没用的珍珠木吧。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 5;
            item.damage = 400;
            item.rare = MyRareID.Tier3;
        }

        public override WeaponState State => WeaponState.False_EX;

        public override bool Extra => true;

        public override void AddRecipes()
        {
        }
    }

    public class SawChakramProj : ChakramBaseProjectile
    {
        public override void AI()
        {
            base.AI();
            if (SpecialAttack)
            {
                foreach (var npc in Main.npc)
                {
                    if (npc.CanBeChasedBy())
                    {
                        float distance = (npc.Center - projectile.Center).Length();
                        if (distance != default)
                        {
                            npc.velocity += (projectile.Center - npc.Center).SafeNormalize(default) * 64 / distance;
                        }
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var unit = projectile.rotation.ToRotationVector2();
            for (int n = 0; n < 4; n++)
            {
                Dust dust = Dust.NewDustPerfect(projectile.Center + unit * 24, MyDustId.RedBlood, new Vector2(-unit.Y, unit.X), 100, Color.Red, 1f);
                //dust.scale = 0.4f + Main.rand.NextFloat(-1, 1) * 0.1f;
                //dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.3f;
                //dust.fadeIn *= .5f;
                dust.noGravity = true;
                dust.velocity *= (3f + Main.rand.NextFloat() * 4f) * 2;
                unit = new Vector2(-unit.Y, unit.X);
            }
            target.immune[projectile.owner] = 3;
            if (projectile.velocity != default) projectile.timeLeft = 150;
            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool hit => Projectile.velocity == default ? Projectile.timeLeft <= 30 : base.hit;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity != default)
            {
                Projectile.Center += Projectile.velocity;
                Projectile.velocity = default;
                Projectile.timeLeft = 150;
            }
            return false;
        }
    }
}