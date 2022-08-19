using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

using Terraria.DataStructures;
using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.ErchiusEye
{
    public class ErchiusEye : GlowItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("能源之眼");
            Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        //{
        //    if(Mod.HasAsset((Texture + "_Glow").Replace("VirtualDream/", "")))
        //    spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow", false), item.Center - Main.screenPosition, null, Color.White, rotation, IllusionBoundMod.GetTexture(Texture + "_Glow", false).Size() * .5f, scale, 0, 0);
        //}
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
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        //public virtual int crystalCount => Main.rand.Next(3, 7);
        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        //{
        //    if (player.altFunctionUse == 2)
        //    {
        //        //Vector2 vec = Main.MouseWorld - player.Center;
        //        //vec = Vector2.Normalize(vec);
        //        //for (float i = -MathHelper.Pi / 24; i <= MathHelper.Pi / 24; i += MathHelper.Pi / 24)
        //        //{
        //        //    Vector2 finalVec = (vec.ToRotation()).ToRotationVector2() * 25f;
        //        //    Projectile.NewProjectile(source, position, finalVec, ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusCrystal>(), damage, knockBack, player.whoAmI);
        //        //}
        //        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ErchiusCrystalProj>(), damage, knockBack, player.whoAmI, crystalCount, Main.rand.Next(5));

        //    }
        //    else
        //    {
        //        Projectile.NewProjectile(source, position, new Vector2(0, 0), ModContent.ProjectileType<ErchiusLaser>(), damage * 3 / 2, knockBack, player.whoAmI);
        //    }
        //    return false;
        //}
        //public override bool CanUseItem(Player player)
        //{
        //    if (player.altFunctionUse == 2)
        //    {
        //        item.damage = 50;
        //        item.useTime = 30;
        //        item.useAnimation = 30;
        //        item.shoot = ModContent.ProjectileType<ErchiusCrystalProj>();
        //    }
        //    else
        //    {
        //        item.damage = 50;
        //        item.useTime = 20;
        //        item.useAnimation = 20;
        //        item.shoot = ModContent.ProjectileType<ErchiusLaser>();
        //    }
        //    return player.ownedProjectileCounts[ModContent.ProjectileType<ErchiusLaser>()] < 1;
        //}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MechanicalEye);
            recipe.AddIngredient(ItemID.Lens, 50);
            recipe.AddIngredient(ItemID.BlackLens);
            recipe.AddIngredient(ItemID.Amethyst, 15);
            //recipe.AddIngredient<Materials.ErchiusCrystal>(20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        //public override Color? GetAlpha(Color lightColor)
        //{
        //    return Color.White;
        //}
    }
    public class ErchiusEyeEX : ErchiusEye
    {
        //public override int crystalCount => base.crystalCount + 3;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("能源之眼EX");
            Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 275;
            item.mana = 8;
            item.rare = MyRareID.Tier2;
        }
        //public override bool CanUseItem(Player player)
        //{
        //    if (player.altFunctionUse == 2)
        //    {
        //        item.damage = 75;
        //        item.useTime = 24;
        //        item.useAnimation = 24;
        //        item.shoot = ModContent.ProjectileType<ErchiusCrystalProj>();
        //    }
        //    else
        //    {
        //        item.damage = 75;
        //        item.useTime = 16;
        //        item.useAnimation = 16;
        //        item.shoot = ModContent.ProjectileType<ErchiusLaser>();
        //    }
        //    return true;
        //}
        public override void AddRecipes()
        {
        }
    }
    public class ErchiusEyeDL : ErchiusEye
    {
        //public override int crystalCount => base.crystalCount + 8;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("能源之眼DL");
            Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n死亡激光(DeathLaser)\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
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
            var Time = IllusionBoundModSystem.ModTime2 / 60f * MathHelper.Pi;
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
        //public override bool CanUseItem(Player player)
        //{
        //    if (player.altFunctionUse == 2)
        //    {
        //        item.damage = 225;
        //        item.useTime = 19;
        //        item.useAnimation = 19;
        //        item.shoot = ModContent.ProjectileType<ErchiusCrystalProj>();
        //    }
        //    else
        //    {
        //        item.damage = 225;
        //        item.useTime = 13;
        //        item.useAnimation = 13;
        //        item.shoot = ModContent.ProjectileType<ErchiusLaser>();
        //    }
        //    return base.CanUseItem(player);
        //}
    }
    public class ErchiusLaser : RangedHeldProjectile
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
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo itemSource)
            {
                sourceItemType = itemSource.Item.type;
            }
        }
        public int sourceItemType;
        public T UpgradeValue<T>(T normal, T extra, T ultra, T defaultValue = default)
        {
            var type = sourceItemType;//Player.HeldItem.type
            if (type == ModContent.ItemType<ErchiusEye>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<ErchiusEyeEX>())
            {
                return extra;
            }

            if (type == ModContent.ItemType<ErchiusEyeDL>())
            {
                return ultra;
            }

            return defaultValue;
        }
        public override void OnCharging(bool left, bool right)
        {
            Projectile.friendly = left && Factor > 0.5f;
            if (left && (int)Projectile.ai[0] % 20 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item15);

            }
            if (right && (int)Projectile.ai[0] % UpgradeValue(40, 30, 20) == 0)
            {

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), ShootCenter, Projectile.velocity * 32f, ModContent.ProjectileType<ErchiusCrystalProj>(), Player.GetWeaponDamage(Player.HeldItem), Projectile.knockBack, Player.whoAmI, Main.rand.Next(4) + UpgradeValue(3, 6, 11), Main.rand.Next(5));
                SoundEngine.PlaySound(SoundID.Item84);

            }
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 19;
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(40f, 30f, 20f), 0, 1);
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
            Main.spriteBatch.DrawQuadraticLaser_PassNormal(ShootCenter, Vector2.Normalize(Projectile.velocity), Color.Purple, 1024 * factor, 256 * factor, 0.2f * factor, 4, UpgradeValue(1, 1, 10));
            //Main.spriteBatch.DrawQuadraticLaser_PassColorBar(ShootCenter, Vector2.Normalize(Projectile.velocity), 10, 4096 * factor, 1024 * factor, 0, 4, UpgradeValue(1, 1, 10), timeOffset: true);//0.2f * factor

            //var sb = Main.spriteBatch;
            //var render = IllusionBoundMod.Instance.render;
            //GraphicsDevice gd = Main.instance.GraphicsDevice;
            //sb.End();
            //sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //gd.SetRenderTarget(render);
            //gd.Clear(Color.Transparent);
            //Main.spriteBatch.DrawQuadraticLaser_PassNormal(ShootCenter, Vector2.Normalize(Projectile.velocity), Color.Purple, 4096 * factor, 1024 * factor, 0.2f * factor, 4, UpgradeValue(1, 1, 10));
            //sb.End();
            //gd.SetRenderTarget(Main.screenTargetSwap);
            //gd.Clear(Color.Transparent);
            //sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
            //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg");// Backgrounds/StarSky_0 Backgrounds/StarSkyv2  Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg
            //IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
            //IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
            //                                                             //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
            //IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.35f);
            //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
            //IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.30f);
            //IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.LocalPlayer.Center + new Vector2(0.707f) * (float)IllusionBoundMod.ModTime * 8);
            //IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Main.DiscoColor.ToVector4());//Color.Cyan.ToVector4()//default(Vector4)//Color.Cyan.ToVector4()//new Vector4(1, 0, 0.25f, 1)
            //                                                                                           //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
            //                                                                                           //Main.NewText("!!!");
            //IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)  64, 48

            //sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
            //sb.End();

            ////最后在screenTarget上把刚刚的结果画上
            //gd.SetRenderTarget(Main.screenTarget);
            //gd.Clear(Color.Transparent);
            //sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            ////sb.End();


        }
        public override (int X, int Y) FrameMax => (10, 3);
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            frame = texture.Frame(FrameMax.X, FrameMax.Y, (int)Projectile.ai[0] / 2 % 9, UpgradeValue(0, 1, 2));
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.immune[Projectile.owner] = UpgradeValue(5, 3, 1);
        }
    }
    public class ErchiusCrystalProj : ErchiusCrystalPiece
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("能源水晶块");
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
            Main.spriteBatch.DrawShaderTail(projectile, IllusionBoundMod.HeatMap[5], IllusionBoundMod.AniTexes[10], IllusionBoundMod.AniTexes[6]);
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
                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Main.rand.NextFloat(0, MathHelper.TwoPi).ToRotationVector2() * 16f, ModContent.ProjectileType<ErchiusCrystalPiece>(), projectile.damage, 5f, projectile.owner);
            }
            projectile.height = projectile.width = 160;
            base.PreKill(timeLeft);
            return true;
        }

    }
    public class ErchiusCrystalPiece : ModProjectile
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
            Main.spriteBatch.DrawShaderTail(projectile, IllusionBoundMod.HeatMap[5], IllusionBoundMod.AniTexes[10], IllusionBoundMod.AniTexes[6]);
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
