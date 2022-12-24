using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.OculusReaver
{
    //public class IDToolTip : GlobalItem 
    //{
    //    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    //    {
    //        tooltips.Add(new TooltipLine(Mod, "ID", $"物品的id是{item.type}"));
    //    }
    //}
    public class OculusReaver : StarboundWeaponBase
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("当你凝视着它的时候，它也许也凝视着你。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("目珠掠夺者");
        }
        public Item item => Item;

        public override void SetDefaults()
        {
            item.damage = 150;
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
            item.shoot = ModContent.ProjectileType<OculusReaverProj>();
            item.shootSpeed = 1f;
            item.noUseGraphic = true;
            item.noMelee = true;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void AddRecipes()
        {
            //Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient(ItemID.SpiderFang, 50);
            //recipe1.AddIngredient(ItemID.Bone, 25);
            //recipe1.AddIngredient(ItemID.VialofVenom, 50);
            //recipe1.AddIngredient(ItemID.Stinger, 30);
            //recipe1.AddIngredient(ItemID.StyngerBolt, 30);
            //recipe1.AddIngredient(ItemID.Ectoplasm, 15);
            //recipe1.AddTile(TileID.MythrilAnvil);
            //recipe1.SetResult(this);
            //recipe1.AddRecipe();
        }
    }
    public class OculusReaverEX : OculusReaver
    {
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("当你凝视着它的时候，它也许也凝视着你。\n当你一脸嫌弃看向你的敌人的时候，它也会一脸嫌弃地看着你的猎物。(x\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("目珠掠夺者EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 250;
            item.crit = 32;
            item.rare = MyRareID.Tier3;
        }
    }
    public class OculusReaverProj : VertexHammerProj
    {
        //TODO 添加血刺弹幕
        public static void ShootSharpTears(Vector2 targetPos, Player player, Projectile projectile, StarboundWeaponProjectile starboundWeaponProjectile)
        {
            player.LimitPointToPlayerReachableArea(ref targetPos);
            Vector2 vector22 = targetPos + Main.rand.NextVector2Circular(8f, 8f);
            Vector2 value7 = player.FindSharpTearsSpot(vector22).ToWorldCoordinates(Main.rand.Next(17), Main.rand.Next(17));
            if ((player.Center - value7).Length() < 48) value7 = targetPos;
            Vector2 vector23 = (vector22 - value7).SafeNormalize(-Vector2.UnitY) * 16f;
            Projectile.NewProjectile(starboundWeaponProjectile.weapon.GetSource_StarboundWeapon(), value7.X, value7.Y, vector23.X, vector23.Y, ProjectileID.SharpTears, projectile.damage / 12, projectile.knockBack, player.whoAmI, 0f, Main.rand.NextFloat() * 0.5f + 0.5f);
        }
        public override string HammerName => base.HammerName;
        public override float MaxTime => (controlState == 2 ? 2f : 1f) * UpgradeValue(12, 9);
        public override float Factor => base.Factor;
        public override Vector2 CollidingSize => base.CollidingSize * 2;
        //public override Vector2 projCenter => base.projCenter + new Vector2(Player.direction * 16, -16);
        public override Vector2 CollidingCenter => base.CollidingCenter;//new Vector2(projTex.Size().X / 3 - 16, 16)
        public override Vector2 DrawOrigin => base.DrawOrigin + new Vector2(-12, 12);
        public override Color color => base.color;
        public override Color VertexColor(float time) => default;
        public override float MaxTimeLeft => (controlState == 2 ? 0.75f : 1f) * UpgradeValue(8, 7);
        public override float Rotation => base.Rotation;
        public override bool UseRight => true;
        public override (int X, int Y) FrameMax => (2, 1);

        public override void Kill(int timeLeft)
        {

            //Lighting.add

            int max = (int)(30 * Factor);
            var vec = (CollidingCenter - DrawOrigin).RotatedBy(Rotation) + projCenter;
            if (Factor > 0.75f)
            {
                for (int n = 0; n < max; n++)
                {
                    Dust.NewDustPerfect(vec, UpgradeValue(MyDustId.YellowHallowFx, MyDustId.GreenFXPowder, MyDustId.PinkBubble), (MathHelper.TwoPi / max * n).ToRotationVector2() * Main.rand.NextFloat(2, 8)).noGravity = true;
                }
            }
            //if (factor == 1)
            //{
            //    Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), vec, default, ModContent.ProjectileType<HolyExp>(), player.GetWeaponDamage(player.HeldItem) * 3, projectile.knockBack, projectile.owner);
            //}
            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void OnRelease(bool charged, bool left)
        {
            if (Charged)
            {
                if (left)
                {
                    if (Player.CheckMana(UpgradeValue(5, 7), true))
                    {
                        int max = UpgradeValue(2, 5);
                        for (int n = 0; n < max; n++)
                        {
                            Vector2 pointPoisition2 = Player.Center + new Vector2(128 * Player.direction, 0) * ((Projectile.ai[1] + (float)n / max) / MaxTimeLeft) * max;
                            ShootSharpTears(pointPoisition2, Player, Projectile, this);
                        }
                    }
                }
                else
                {
                    if ((int)Projectile.ai[1] == 1 && Player.CheckMana(UpgradeValue(50, 70), true))
                    {
                        Player.Teleport(Main.MouseWorld, 1);
                        SoundEngine.PlaySound(SoundID.Item60, Projectile.position);
                        Projectile.NewProjectileDirect(weapon.GetSource_StarboundWeapon(), Player.Center, default, ModContent.ProjectileType<OculusReaverTear>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, UpgradeValue(2, 3)).rotation = Projectile.rotation;// + Vector2.Normalize(Main.MouseWorld - Player.Center) * 60
                    }
                }

            }
            //Main.NewText(new NPCs.Baron.Baron().CanTownNPCSpawn(10, 10));
            base.OnRelease(charged, left);
        }
        public override Rectangle? frame => projTex.Frame(2, 1, UpgradeValue(0, 1));
        public override bool PreDraw(ref Color lightColor)
        {
            base.PreDraw(ref lightColor);
            var tex = ModContent.Request<Texture2D>(Texture + "_Lid").Value;
            var f = (float)Math.Sin(IllusionBoundMod.ModTime / 60f * MathHelper.Pi) * 10 - 6.0001f;
            var cen = projCenter + new Vector2(26).RotatedBy(Rotation - MathHelper.PiOver2);
            var vec = Main.MouseWorld - cen;
            var _s = 1 - 1 / (vec.Length() / 32 + 1);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture.Replace("OculusReaverProj", "pupil")).Value, cen - Main.screenPosition + vec.SafeNormalize(default) * _s * 4, null, Color.White, 0, new Vector2(2), 2f * (1.5f - _s * .5f), 0, 0);
            //Main.spriteBatch.DrawHammer(this, tex, lightColor, tex.Frame(1, 4, 0, f > 0 ? (int)f : 0));

            Vector2 origin = DrawOrigin;
            float rotation = Rotation;
            switch (flip)
            {
                case SpriteEffects.FlipHorizontally:
                    origin.X = projTex.Size().X / FrameMax.X - origin.X;
                    rotation += MathHelper.PiOver2;

                    break;
                case SpriteEffects.FlipVertically:
                    origin.Y = projTex.Size().Y / FrameMax.Y - origin.Y;
                    break;
            }
            Main.spriteBatch.Draw(tex, projCenter - Main.screenPosition, tex.Frame(1, 4, 0, f > 0 ? (int)f : 0), lightColor, rotation, origin, scale, flip, 0);
            return false;
        }
    }
    public class OculusReaverTear : StarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("裂空之眼");
        }
        Projectile projectile => Projectile;
        public override bool PreDraw(ref Color lightColor)
        {

            //var fac = projectile.ai[0].SymmetricalFactor(90, 10);
            //var render = IllusionBoundMod.Instance.render;
            //var gd = Main.graphics.GraphicsDevice;
            //var sb = Main.spriteBatch;
            ////先在自己的render上画这个弹幕
            //sb.End();
            //gd.SetRenderTarget(render);
            //gd.Clear(Color.Transparent);
            //sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
            //IllusionBoundMod.TransformEffect.Parameters["factor1"].SetValue(fac);
            //IllusionBoundMod.TransformEffect.CurrentTechnique.Passes[0].Apply();
            //sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(512), (state == 3 ? 2f : 1.5f) * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)
            //sb.End();
            ////然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
            ////原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
            //gd.SetRenderTarget(Main.screenTargetSwap);
            //gd.Clear(Color.Transparent);
            //sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
            //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
            //IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
            //IllusionBoundModSystem.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTaRGet的不是这个render，否则会报错）
            //                                                             //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
            //IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
            //IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
            //IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.player[projectile.owner].Center + projectile.rotation.ToRotationVector2() * (float)IllusionBoundMod.ModTime * 8);
            //IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
            ////IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
            ////Main.NewText("!!!");
            //IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)

            //sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
            //sb.End();

            ////最后在screenTarget上把刚刚的结果画上
            //gd.SetRenderTarget(Main.screenTarget);
            //gd.Clear(Color.Transparent);
            //sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            return false;
        }
        public override void SetDefaults()
        {
            projectile.width = 90;
            projectile.height = 90;
            projectile.DamageType = DamageClass.Melee;
            projectile.friendly = true;
            projectile.timeLeft = 180;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;

        }
        public override bool ShouldUpdatePosition() => false;
        int state => (int)projectile.ai[1];
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 10;
            base.OnHitNPC(target, damage, knockback, crit);

        }
        public override void AI()
        {
            projectile.ai[0]++;
            if ((int)projectile.ai[0] % (state == 3 ? 10 : 20) == 0)
            {
                int n = 0;
                foreach (var target in Main.npc)
                {
                    var length = (target.Center - projectile.Center).Length();
                    if (target.active && !target.friendly && target.chaseable && !target.dontTakeDamage && length < (state == 2 ? 1024 : 768) && Main.rand.NextFloat(0, length * length / 128) < length)
                    {
                        n++;
                        //var rand = Main.rand.NextFloat(0, MathHelper.TwoPi);
                        //var p = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), default, default, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, state - 2);
                        //p.rotation = rand;
                        //p.frame = target.whoAmI + 1;
                        //p.height = p.width = 20;
                        //p.Center = target.Center - rand.ToRotationVector2() * (state == 3 ? 192 : 128);
                        //p.localAI[0] = Main.rand.NextFloat(0, 1);
                        OculusReaverProj.ShootSharpTears(target.Center, Player, projectile, this);

                    }
                    if (n > (state == 3 ? 8 : 5)) break;
                }
            }
            if (projectile.ai[0] > 300) projectile.Kill();
        }
    }
}