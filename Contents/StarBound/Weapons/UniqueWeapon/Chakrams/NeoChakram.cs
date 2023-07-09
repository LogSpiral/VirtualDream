using System;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class NeoChakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("新型环刃");
            // Tooltip.SetDefault("毁灭性的能量正发出噼啪声。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 225;
            item.shoot = ProjectileType<NeoChakramProj>();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = (Main.MouseWorld - player.Center) / 30f;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.StickOfRAM>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.StickOfRAM>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
    public class NeoChakramEX : NeoChakram
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("环刃EX");
            // Tooltip.SetDefault("毁灭性的能量正发出噼啪声。\n锁定目标，光束打击！\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override WeaponState State => WeaponState.False_EX;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 350;
            item.rare = MyRareID.Tier3;
            item.value *= 5;

        }
        public override bool Extra => true;
        public override void AddRecipes()
        {
        }
    }
    public class NeoChakramProj : ChakramBaseProjectile
    {
        public override void AI()
        {
            if (SpecialAttack)
            {
                var unit = projectile.rotation.ToRotationVector2();
                for (int n = 0; n < 2; n++)
                {
                    Dust dust = Dust.NewDustPerfect(projectile.Center + unit * 24, MyDustId.CyanBubble, new Vector2(-unit.Y, unit.X) * Main.rand.NextFloat(0.85f, 1.15f) * 4, 100, Color.White, 1f);
                    dust.scale = 0.4f + Main.rand.NextFloat(-1, 1) * 0.1f;
                    dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.3f;
                    dust.fadeIn *= .5f;
                    dust.noGravity = true;
                    //dust.velocity *= (3f + Main.rand.NextFloat() * 4f) * 2;
                    unit *= -1;
                }

                if ((int)VirtualDreamSystem.ModTime2 % 5 == 0)
                {
                    float distanceMax = 256;
                    NPC target = null;
                    foreach (var npc in Main.npc)
                    {
                        float distance = (npc.Center - projectile.Center).Length();
                        if (npc.CanBeChasedBy() && distance < distanceMax && Main.rand.Next(100) < MathF.Sqrt(1 - distance / distanceMax) * 100)
                        {
                            distance = distanceMax;
                            target = npc;
                        }
                    }
                    if (target != null)
                    {
                        var crit = Main.rand.Next(100) < projectile.CritChance;
                        Main.player[Projectile.owner].ApplyDamageToNPC(target, (int)(Projectile.damage * Main.rand.NextFloat(0.85f, 1.15f) * (crit ? 1 : .5f)), projectile.knockBack, Math.Sign(target.Center.X - projectile.Center.X), crit);
                        for (int n = 0; n < 2; n++)
                        {

                            var pos = unit * 17 + projectile.Center;
                            Vector2 lastPos = default;
                            for (int i = 0; i < 15; i++)
                            {
                                var fac = 15f - i;
                                fac /= 15;
                                lastPos = pos;
                                pos += (target.Center - pos).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 3, MathHelper.Pi / 3) * fac) * MathHelper.Lerp(0.05f, 1f, n / 14f);
                                var length = (pos - lastPos).Length();
                                for (int k = 0; k < length; k++)
                                {
                                    Dust dust = Dust.NewDustPerfect(Vector2.Lerp(pos, lastPos, k / (float)length), MyDustId.CyanBubble, default, 100, Color.White, 1f);
                                    dust.scale = 0.4f + Main.rand.NextFloat(-1, 1) * 0.1f;
                                    dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.3f;
                                    dust.fadeIn *= fac;
                                    dust.scale *= fac;
                                    dust.noGravity = true;
                                    dust.velocity = default;
                                }


                            }
                            //unit = new Vector2(-unit.Y, unit.X);
                            unit = -unit;
                        }
                    }
                }
            }
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[projectile.owner] = 3;
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool hit => Projectile.timeLeft <= 30;
        public override bool ShouldUpdatePosition()
        {
            return Projectile.timeLeft > 150 || Projectile.timeLeft <= 30;
        }
    }
}
