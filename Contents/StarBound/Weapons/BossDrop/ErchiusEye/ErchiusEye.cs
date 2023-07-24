using LogSpiralLibrary;
using System;
using Terraria;

using Terraria.DataStructures;
using Terraria.ID;
using VirtualDream.Contents.StarBound.Materials;
using VirtualDream.Contents.StarBound.TimeBackTracking;
using VirtualDream.Contents.StarBound.Weapons.Broken;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.ErchiusEye
{
    public class ErchiusEye_Broken : ErchiusEye 
    {
        public override WeaponState State => WeaponState.Broken;
        public override void SetDefaults()
        {

            base.SetDefaults();
            item.shoot = ProjectileID.PurpleLaser;
            item.damage = 40;
            item.rare = ModContent.RarityType<BrokenRarity>();
            item.noUseGraphic = false;
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
        }
        public override bool AltFunctionUse(Player player) => false;
        public override WeaponRepairRecipe RepairRecipe()
        {
            var recipe = GetEmptyRecipe();
            recipe.AddIngredient(ItemID.MechanicalEye);
            recipe.AddIngredient(ItemID.Lens, 50);
            recipe.AddIngredient(ItemID.BlackLens);
            recipe.AddIngredient(ItemID.Amethyst, 15);
            recipe.AddIngredient<ErchiusCrystal>(20);
            recipe.SetResult<ErchiusEye>();
            return recipe;
        }
    }
    public class ErchiusEye : StarboundWeaponBase
    {
        public override WeaponRepairRecipe RepairRecipe()
        {
            return GetEmptyRecipe().AddIngredient<AncientEssence>(5000).SetResult<ErchiusEyeEX>();
        }
        public override bool BossDrop => true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("能源之眼");
            // Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public Item item => Item;
        public override void SetDefaults()
        {
            item.noMelee = true;
            item.damage = 150;
            item.DamageType = DamageClass.Magic;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.mana = 10;
            item.rare = MyRareID.Tier1;
            item.width = 44;
            item.height = 46;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.shootSpeed = 24f;
            item.useAnimation = 20;
            item.value = Item.sellPrice(silver: 3);
            item.shoot = ModContent.ProjectileType<ErchiusLaser>();

        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult) => reduce = player.ownedProjectileCounts[item.shoot] > 0 ? reduce : 0;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
    public class ErchiusEyeEX : ErchiusEye
    {
        public override WeaponRepairRecipe RepairRecipe()
        {
            return GetEmptyRecipe();
        }
        public override WeaponState State => WeaponState.False_EX;
        //public override int crystalCount => base.crystalCount + 3;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("能源之眼EX");
            // Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 275;
            item.mana = 8;
            item.rare = MyRareID.Tier2;
        }
        public override void AddRecipes()
        {
        }
    }
    public class ErchiusEyeDL : ErchiusEye
    {
        //public override int crystalCount => base.crystalCount + 8;
        public override WeaponState State => WeaponState.False_UL;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("能源之眼DL");
            // Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n死亡激光(DeathLaser)\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 400;
            item.mana = 6;
            item.rare = MyRareID.Tier3;
        }
        public override void HoldItem(Player player)
        {
            var Time = VirtualDreamSystem.ModTime2 / 60f * MathHelper.Pi;
            Dust dust = Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 256), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust dust1 = Dust.NewDustPerfect(player.Center - new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 256), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust dust2 = Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust dust3 = Dust.NewDustPerfect(player.Center - new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust dust4 = Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(Time) * 16, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust dust5 = Dust.NewDustPerfect(player.Center - new Vector2((float)Math.Cos(Time) * 16, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
            dust.noGravity = true;
            dust1.noGravity = true;
            dust2.noGravity = true;
            dust3.noGravity = true;
            dust4.noGravity = true;
            dust5.noGravity = true;

        }
        public override void AddRecipes()
        {
        }
    }
    public class ErchiusLaser : RangedHeldProjectile, IStarboundWeaponProjectile
    {
        //BossDropWeaponProj<ErchiusEye, ErchiusEyeEX, ErchiusEyeDL>
        public override Vector2 HeldCenter => base.HeldCenter + Projectile.velocity * 6;//Main.MouseWorld - Player.Center
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
            //Projectile.friendly = true;
        }
        public override bool UseRight => true;
        public override void OnCharging(bool left, bool right)
        {
            Projectile.friendly = left && Factor > 0.5f;
            if (left && (int)Projectile.ai[0] % 20 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item15);

            }
            if (right && (int)Projectile.ai[0] % this.UpgradeValue(40, 30, 20) == 0)
            {

                Projectile.NewProjectile(((IStarboundWeaponProjectile)this).weapon.GetSource_StarboundWeapon(), ShootCenter, Projectile.velocity * 32f, ModContent.ProjectileType<ErchiusCrystalProj>(), Player.GetWeaponDamage(Player.HeldItem), Projectile.knockBack, Player.whoAmI, Main.rand.Next(4) + this.UpgradeValue(3, 6, 11), Main.rand.Next(5));
                SoundEngine.PlaySound(SoundID.Item84);

            }
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 19;
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / this.UpgradeValue(40f, 30f, 20f), 0, 1);
            }
        }
        //public override bool PreDraw(ref Color lightColor)
        //{
        //    return base.PreDraw(ref lightColor);
        //}
        public override void PostDraw(Color lightColor)
        {
            if (Factor < 0.5f || !Player.controlUseItem) return;
            var factor = 2 * (Factor - 0.5f);
            Main.spriteBatch.DrawQuadraticLaser_PassNormal(ShootCenter, Vector2.Normalize(Projectile.velocity), Color.Purple, LogSpiralLibraryMod.AniTex[this.UpgradeValue(1, 1, 10)].Value, 1024 * factor, 256 * factor, 0.2f * factor, 4);
        }
        public override (int X, int Y) FrameMax => (10, 3);
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            frame = texture.Frame(FrameMax.X, FrameMax.Y, (int)Projectile.ai[0] / 2 % 9, this.UpgradeValue(0, 1, 2));
            origin = new Vector2(6, 14);
            scale = 2f;
        }
        public override Color GlowColor => base.GlowColor;//base.GlowColor * (MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(40f, 30f, 20f), 1, 2) - 1)
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Factor <= 0.5f || !Player.controlUseItem)
            {
                return false;
            }
            float point = 0f;
            var factor = 2 * (Factor - 0.5f);
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), ShootCenter, ShootCenter + Projectile.velocity * factor * 1024f, 18 * factor, ref point);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.immune[Projectile.owner] = this.UpgradeValue(5, 3, 1);
        }
    }
    public class ErchiusCrystalProj : ErchiusCrystalPiece
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("能源水晶块");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = projectile.height = 40;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, TextureAssets.Projectile[projectile.type].Value.Frame(5, 1, (int)projectile.ai[1] % 5, 0), Color.White, projectile.rotation, TextureAssets.Projectile[projectile.type].Value.Size() * .5f * new Vector2(0.25f, 1), 4f, 0, 0);
            if (!Main.gamePaused)
            {
                for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                {
                    projectile.oldPos[n] = projectile.oldPos[n - 1];
                }
                projectile.oldPos[0] = projectile.Center;
            }
            Main.spriteBatch.DrawShaderTail(projectile, LogSpiralLibraryMod.HeatMap[5].Value, LogSpiralLibraryMod.AniTex[10].Value, LogSpiralLibraryMod.BaseTex[8].Value);
            return false;
        }
        public override void AI()
        {
            projectile.rotation++;
            projectile.velocity.Y += 0.25f;
        }
        public override bool PreKill(int timeLeft)
        {
            for (int i = 0; i <= projectile.ai[0]; i++)
            {
                Projectile.NewProjectile(((IStarboundWeaponProjectile)this).weapon.GetSource_StarboundWeapon(), projectile.Center, Main.rand.NextFloat(0, MathHelper.TwoPi).ToRotationVector2() * 16f, ModContent.ProjectileType<ErchiusCrystalPiece>(), projectile.damage, 5f, projectile.owner);
            }
            projectile.height = projectile.width = 160;
            base.PreKill(timeLeft);
            return true;
        }

    }
    public class ErchiusCrystalPiece : ModProjectile, IStarboundWeaponProjectile
    {
        public Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 16;
            projectile.scale = 1f;
            projectile.timeLeft = 240;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.light = 1f;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, TextureAssets.Projectile[projectile.type].Value.Size() * .5f, 4f, 0, 0);
            if (!Main.gamePaused)
            {
                for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                {
                    projectile.oldPos[n] = projectile.oldPos[n - 1];
                }
                projectile.oldPos[0] = projectile.Center;
            }
            Main.spriteBatch.DrawShaderTail(projectile, LogSpiralLibraryMod.HeatMap[5].Value, LogSpiralLibraryMod.AniTex[10].Value, LogSpiralLibraryMod.BaseTex[8].Value);

            return false;
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.25f;
            if (projectile.velocity != default)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
        }
        public override bool PreKill(int timeLeft)
        {
            for (int n = 0; n < 30; n++)
            {
                Dust.NewDustPerfect(projectile.Center, MyDustId.PinkBubble, (n / 30f * MathHelper.TwoPi).ToRotationVector2()).noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);

            return true;
        }
    }
}
