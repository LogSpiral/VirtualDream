using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using VirtualDream.Utils;
using static VirtualDream.Utils.IllusionBoundExtensionMethods;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using ReLogic.Graphics;
using Terraria.DataStructures;
using System.Linq;
using Terraria.ModLoader.IO;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.AsuterosaberuDX
{
    //    public class AsuterosaberuDX : ModItem
    //    {
    //        public override bool CanUseItem(Player player)
    //        {
    //            return player.ownedProjectileCounts[item.shoot] < 1;
    //        }
    //        public override void SetStaticDefaults()
    //        {
    //            Tooltip.SetDefault("彩虹色的剑刃锋利到能够劈开空间\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND][c/9933cc:我觉得这个没有把参数方程贴出来的必要]");
    //            DisplayName.SetDefault("天文军刀豪华版");
    //        }
    //        public Item item => Item;
    //        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    //        {
    //            //var f = Filters.Scene["Sandstorm"].GetShader().Shader.Parameters;
    //            ////int n;
    //            ////for (n = 0; n < f.Count; n++) 
    //            ////{
    //            ////    if (f[n].GetHashCode() == f["uImageSize2"].GetHashCode()) break;
    //            ////}
    //            ////Main.NewText(n);
    //            ////var list = (List<EffectParameter>)f.GetType().GetField("pParameter", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(f);
    //            //List<EffectParameter>.Enumerator enumerator = f.GetEnumerator();
    //            //int n = 0;
    //            //if (enumerator.MoveNext())
    //            //{
    //            //    do
    //            //    {
    //            //        //Main.NewText(enumerator.Current.Name);
    //            //        n++;
    //            //        spriteBatch.DrawString(Main.fontMouseText, enumerator.Current.Name, new Vector2(120,300 + n * 32), Color.White);
    //            //    }
    //            //    while (enumerator.MoveNext());
    //            //}

    //            //var list = (Dictionary<string, Filter>)Filters.Scene.GetType().GetField("_effects", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(Filters.Scene);
    //            //var enumerator = list.Keys.GetEnumerator();
    //            //int n = 0;
    //            //if (enumerator.MoveNext())
    //            //{
    //            //    do
    //            //    {
    //            //        //Main.NewText(enumerator.Current.Name);
    //            //        n++;
    //            //        spriteBatch.DrawString(Main.fontMouseText, enumerator.Current, new Vector2(120, 120 + n * 16), Color.White);
    //            //    }
    //            //    while (enumerator.MoveNext());
    //            //}

    //            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    //        }
    //        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
    //        {
    //            item.useTime = player.itemAnimationMax;
    //            IllusionBoundOnIlFunctions.ChangeShooshStyle(player);
    //            //Main.NewText("!!!!");
    //            if (player.altFunctionUse != 2) SoundEngine.PlaySound(SoundID.Item60, player.position);
    //            Projectile.NewProjectile(source, player.Center, Vector2.One, type, damage, knockBack, player.whoAmI, (player.altFunctionUse == 2 ? 0 : 1) * player.itemAnimationMax, player.altFunctionUse == 2 ? 1 : 0);
    //            return false;
    //        }
    //        public override void SetDefaults()
    //        {
    //            item.DamageType = DamageClass.Melee;
    //            item.crit = 14;
    //            item.width = 92;
    //            item.width = 92;
    //            item.useTime = 24;
    //            item.useAnimation = 24;
    //            item.knockBack = 6;
    //            item.useStyle = 1;
    //            item.autoReuse = true;
    //            item.value = Item.sellPrice(0, 10, 0, 0);
    //            item.rare = MyRareID.Tier2;
    //            item.noUseGraphic = true;
    //            item.noMelee = true;
    //            item.shootSpeed = 10f;
    //            item.damage = 100;
    //            //item.channel = true;
    //            item.shoot = ModContent.ProjectileType<AsuterosaberuDXSwoosh>();

    //        }
    //        public override bool AltFunctionUse(Player player)
    //        {
    //            return true;
    //        }
    //        public override void AddRecipes()
    //        {
    //            Recipe recipe1 = CreateRecipe();
    //            recipe1.AddIngredient(ItemID.WhitePhasesaber);
    //            recipe1.AddIngredient(ItemID.RedPhasesaber);
    //            recipe1.AddIngredient(ItemID.GreenPhasesaber);
    //            recipe1.AddIngredient(ItemID.BluePhasesaber);
    //            recipe1.AddIngredient(ItemID.YellowPhasesaber);
    //            recipe1.AddIngredient(ItemID.PurplePhasesaber);
    //            recipe1.AddIngredient(ItemID.FragmentStardust, 30);
    //            recipe1.AddIngredient(ItemID.FragmentVortex, 30);
    //            recipe1.AddIngredient(ItemID.FragmentNebula, 30);
    //            recipe1.AddIngredient(ItemID.FragmentSolar, 30);
    //            recipe1.AddIngredient(ItemID.LunarBar, 10);
    //            recipe1.AddIngredient<Materials.AncientEssence>(1000);
    //            recipe1.SetResult(this);
    //            recipe1.AddRecipe();
    //        }
    //    }
    //    public class AsuterosaberuDXEX : AsuterosaberuDX
    //    {
    //        public override void SetStaticDefaults()
    //        {
    //            Tooltip.SetDefault("彩虹色的剑刃锋利到能够劈开空间\n破碎吧，属于空间的秩序\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");//\n[c / 9933cc: X:cos(3T) * sin((5T) + 5) Y: sin(3T) * sin((5T) + 5)]
    //            DisplayName.SetDefault("天文军刀豪华版EX");
    //        }
    //        public override void SetDefaults()
    //        {
    //            base.SetDefaults();
    //            item.crit = 36;
    //            item.rare = MyRareID.Tier3;
    //            item.useTime = 18;
    //            item.useAnimation = 18;
    //            item.knockBack = 8;
    //            item.value = Item.sellPrice(0, 20, 0, 0);
    //            item.damage = 250;
    //        }
    //    }
    //    public class AsuterosaberuCSM : AsuterosaberuDX
    //    {
    //        public override void SetStaticDefaults()
    //        {
    //            Tooltip.SetDefault("源生纯粹七元霓辉空间水晶所铸造的利刃，也许它是斩断了空间逃离了原本的世界来到这里？\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");//\n[c / 9933cc: X:cos(3T) * sin((5T) + 5) Y: sin(3T) * sin((5T) + 5)]
    //            DisplayName.SetDefault("天文军刀CSM");
    //        }
    //        public override void SetDefaults()
    //        {
    //            base.SetDefaults();
    //            item.crit = 36;
    //            item.rare = MyRareID.Tier3;
    //            item.useTime = 12;
    //            item.useAnimation = 12;
    //            item.knockBack = 8;
    //            item.value = Item.sellPrice(0, 20, 0, 0);
    //            item.damage = 250;
    //        }
    //    }
    //    public class AsuterosaberuDXSwoosh : ModProjectile
    //    {
    //        Projectile projectile => Projectile;
    //        public override string Texture => "VirtualDream/Contents.StarBound/Weapons/UniqueWeapon/AsuterosaberuDX/AsuterosaberuDX";
    //        public override void SetDefaults()
    //        {
    //            projectile.width = projectile.height = 2;
    //            projectile.friendly = true;
    //            projectile.aiStyle = -1;
    //            projectile.light = 0.2f;
    //            projectile.DamageType = DamageClass.Melee;
    //            projectile.penetrate = -1;
    //            projectile.tileCollide = false;
    //        }
    //        public override void SetStaticDefaults()
    //        {
    //            DisplayName.SetDefault("天文军刀豪华版");
    //            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
    //        }
    //        public override bool ShouldUpdatePosition() => false;
    //        public override void AI()
    //        {
    //            //Main.NewText("淦");
    //            if ((int)projectile.ai[1] == 1 && drawPlayer.controlUseTile && projectile.frame == 0)
    //            {
    //                projectile.frameCounter = 2;
    //                if (projectile.ai[0] >= drawPlayer.itemAnimationMax * 0.75f)
    //                {
    //                    projectile.frameCounter = 1;
    //                }
    //                else if (projectile.ai[0] < drawPlayer.itemAnimationMax)
    //                {
    //                    projectile.ai[0] += 0.5f;
    //                }
    //            }
    //            else
    //            {
    //                projectile.frame = 1;
    //                projectile.ai[0]--;
    //                if (projectile.ai[0] <= 0)
    //                    projectile.Kill();
    //            }
    //            if ((int)projectile.ai[0] == drawPlayer.itemAnimationMax / 4 && projectile.frameCounter == 1)
    //            {
    //                SoundEngine.PlaySound(SoundID.Item60, projectile.position);
    //                Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), drawPlayer.Center + Vector2.Normalize(Main.MouseWorld - drawPlayer.Center) * 60, default, ModContent.ProjectileType<AstralTearRemake>(), projectile.damage, projectile.knockBack, projectile.owner, 0, extra ? 3 : 2).rotation = (targetPos - drawPlayer.Center).ToRotation();
    //            }
    //            //Main.NewText();

    //            projectile.velocity = (targetPos - drawPlayer.Center).SafeNormalize(default);
    //            //drawPlayer.heldProj = projectile.whoAmI;
    //            base.AI();
    //        }
    //        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    //        {
    //            if (targetPos == default) return false;
    //            float point = 0f;
    //            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), drawPlayer.Center, targetPos, 22, ref point);
    //        }
    //        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    //        {
    //            target.immune[projectile.owner] = 5;
    //            base.OnHitNPC(target, damage, knockback, crit);
    //        }
    //        Vector2 targetPos;
    //        public Player drawPlayer => Main.player[projectile.owner];
    //        public void DrawStarSwoosh(float fac, bool extra = false, float size = 1f)
    //        {
    //            try
    //            {
    //                if (IllusionBoundMod.ShaderSwooshEX == null) return;
    //                if (IllusionBoundMod.IMBellEffect == null) return;
    //                if (Main.GameViewMatrix == null) return;
    //                var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;
    //                var modPlayer = drawPlayer.GetModPlayer<WeaponDisplayForPlayer>();
    //                //var fac = modPlayer.factorGeter;
    //                bool neg = modPlayer.negativeDir;
    //                fac = neg ? 1 - fac : fac;
    //                var drawCen = (drawPlayer.gravDir == -1 ? new Vector2(drawPlayer.Center.X, (2 * (Main.screenPosition + new Vector2(960, 560)) - drawPlayer.Center - new Vector2(0, 96)).Y) : drawPlayer.Center) + new Vector2(0, drawPlayer.gfxOffY);
    //                float rotVel = extra && modPlayer.swingCount % 3 == 2 ? IllusionSwooshConfigClient.instance.rotationVelocity : 1;//
    //                var theta = (1.2375f * fac * rotVel - 1.125f) * MathHelper.Pi;
    //                CustomVertexInfo[] c = new CustomVertexInfo[6];
    //                var itemTex = TextureAssets.Item[drawPlayer.HeldItem.type].Value;
    //                float xScaler = IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? MathHelper.Lerp(modPlayer.kValue, modPlayer.kValueNext, fac) : modPlayer.kValue;//获取x轴方向缩放系数
    //                float scaler = itemTex.Size().Length() * drawPlayer.HeldItem.scale / xScaler * 0.5f * trans.M11 * size;//对椭圆进行位似变换(你直接说坐标乘上一个系数不就好了吗，屑阿汪// * IllusionSwooshConfigClient.instance.swooshSize
    //                var swooshAniFac = neg ? 4 * fac - 3 : 4 * fac;
    //                swooshAniFac = MathHelper.Clamp(swooshAniFac, 0, 1);
    //                var theta3 = (1.2375f * swooshAniFac * rotVel - 1.125f) * MathHelper.Pi;//这里是又一处插值
    //                var cos = (float)Math.Cos(theta) * scaler;
    //                var sin = (float)Math.Sin(theta) * scaler;//这里(cos,sin)对应的位置就是我们希望贴图右上角所在的位置，而(0,0)对应的位置是贴图左下角所在的位置

    //                var rotator = IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? MathHelper.Lerp(modPlayer.rotationForShadow, modPlayer.rotationForShadowNext, fac) : modPlayer.rotationForShadow;

    //                var u = new Vector2(xScaler * (cos - sin), -cos - sin).RotatedBy(rotator);
    //                var v = new Vector2(-xScaler * (cos + sin), sin - cos).RotatedBy(rotator);
    //                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
    //                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
    //                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
    //                List<CustomVertexInfo> bars = new List<CustomVertexInfo>();

    //                float xScaler3 = IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? MathHelper.Lerp(modPlayer.kValue, modPlayer.kValueNext, swooshAniFac) : modPlayer.kValue;
    //                var rotator3 = IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? MathHelper.Lerp(modPlayer.rotationForShadow, modPlayer.rotationForShadowNext, swooshAniFac) : modPlayer.rotationForShadow;
    //                Color realColor = Color.White;
    //                SamplerState sampler;
    //                switch (IllusionSwooshConfigClient.instance.swooshSampler)
    //                {
    //                    default:
    //                    case SwooshSamplerState.各向异性: sampler = SamplerState.AnisotropicClamp; break;
    //                    case SwooshSamplerState.线性: sampler = SamplerState.PointClamp; break;
    //                    case SwooshSamplerState.点: sampler = SamplerState.PointClamp; break;
    //                }
    //                if (!drawPlayer.controlUseTile)
    //                {
    //                    for (int i = 0; i < 25; i++)
    //                    {
    //                        var f = (i + 1) / 25f;//分割成25次惹，f从1/25f到1
    //                        var theta2 = f.Lerp(theta3, theta, true);//快乐线性插值
    //                        var xScaler2 = (IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? f : 1).Lerp(xScaler3, xScaler, true);
    //                        var rotator2 = (IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? f : 1).Lerp(rotator3, rotator, true);
    //                        var cos2 = (float)Math.Cos(theta2) * scaler;
    //                        var sin2 = (float)Math.Sin(theta2) * scaler;
    //                        var u2 = new Vector2(xScaler2 * (cos2 - sin2), -cos2 - sin2).RotatedBy(rotator2);
    //                        var v2 = new Vector2(-xScaler2 * (cos2 + sin2), sin2 - cos2).RotatedBy(rotator2);
    //                        var newVec = u2 + v2;
    //                        var alphaLight = 0.6f;
    //                        var _f = 6 * f / (3 * f + 1);//f;// 
    //                        _f = MathHelper.Clamp(_f, 0, 1);
    //                        realColor.A = (byte)(_f * 255);//.MultiplyRGBA(new Color(1,1,1,_f))
    //                        bars.Add(new CustomVertexInfo(drawCen + newVec, realColor, new Vector3(1 - f, 1, alphaLight)));//(3 * f - 4) / (4 * f - 3)//快乐连顶点
    //                        realColor.A = 0;
    //                        bars.Add(new CustomVertexInfo(drawCen, realColor, new Vector3(0, 0, alphaLight)));
    //                    }
    //                    //Main.NewText(new Vector3(fac, MathHelper.Clamp(modPlayer.negativeDir ? (4 * fac - 3) : 4 * fac, 0, 1), modPlayer.negativeDir ? -1 : 1));

    //                    List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
    //                    if (bars.Count > 2)
    //                    {
    //                        for (int i = 0; i < bars.Count - 2; i += 2)
    //                        {
    //                            triangleList.Add(bars[i]);
    //                            triangleList.Add(bars[i + 2]);
    //                            triangleList.Add(bars[i + 1]);

    //                            triangleList.Add(bars[i + 1]);
    //                            triangleList.Add(bars[i + 2]);
    //                            triangleList.Add(bars[i + 3]);
    //                        }


    //                        var render = IllusionBoundMod.Instance.render;
    //                        var gd = Main.graphics.GraphicsDevice;
    //                        var sb = Main.spriteBatch;
    //                        //先在自己的render上画这个弹幕
    //                        sb.End();
    //                        gd.SetRenderTarget(render);
    //                        gd.Clear(Color.Transparent);
    //                        sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix

    //                        IllusionBoundMod.ShaderSwooshEX.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
    //                        IllusionBoundMod.ShaderSwooshEX.Parameters["uLighter"].SetValue(IllusionSwooshConfigClient.instance.luminosityFactor);
    //                        IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(0);//-(float)Main.time * 0.06f
    //                        IllusionBoundMod.ShaderSwooshEX.Parameters["checkAir"].SetValue(IllusionSwooshConfigClient.instance.checkAir);
    //                        IllusionBoundMod.ShaderSwooshEX.Parameters["airFactor"].SetValue(1f);
    //                        IllusionBoundMod.ShaderSwooshEX.Parameters["gather"].SetValue(IllusionSwooshConfigClient.instance.gather);


    //                        Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/BaseTex_" + IllusionSwooshConfigClient.instance.ImageIndex);//字面意义，base那个是不会随时间动的，ani那个会动//BaseTex
    //                        Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/AniTex");
    //                        Main.graphics.GraphicsDevice.Textures[2] = itemTex;
    //                        //if (IllusionSwooshConfigClient.instance.swooshColorType == SwooshColorType.函数生成热度图) 
    //                        //{
    //                        //    var colorBar = new Texture2D(Main.graphics.GraphicsDevice,300,60);
    //                        //    colorBar.SetData<Color>();
    //                        //    Main.graphics.GraphicsDevice.Textures[3] = colorBar;
    //                        //}


    //                        Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
    //                        Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
    //                        Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
    //                        Main.graphics.GraphicsDevice.SamplerStates[3] = sampler;
    //                        IllusionBoundMod.ShaderSwooshEX.CurrentTechnique.Passes[0].Apply();
    //                        Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
    //                        Main.graphics.GraphicsDevice.RasterizerState = originalState;

    //                        sb.End();
    //                        //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
    //                        //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
    //                        gd.SetRenderTarget(Main.screenTargetSwap);
    //                        gd.Clear(Color.Transparent);
    //                        sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
    //                        Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
    //                        IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
    //                        IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
    //                                                                                     //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
    //                        IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
    //                        IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
    //                        IllusionBoundMod.Distort.Parameters["position"].SetValue(drawPlayer.Center);
    //                        IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
    //                        IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)

    //                        sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
    //                        sb.End();

    //                        //最后在screenTarget上把刚刚的结果画上
    //                        gd.SetRenderTarget(Main.screenTarget);
    //                        gd.Clear(Color.Transparent);
    //                        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
    //                        sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
    //                        //sb.End();


    //                        //Main.spriteBatch.Begin(SpriteSortMode.Immediate, alphaBlend ? BlendState.NonPremultiplied : BlendState.Additive, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
    //                        //sb.Draw(render, Vector2.Zero, Color.White);

    //                        //IllusionBoundMod.ShaderSwooshEX.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
    //                        //IllusionBoundMod.ShaderSwooshEX.Parameters["uLighter"].SetValue(IllusionSwooshConfigClient.instance.luminosityFactor);
    //                        //IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(0);//-(float)Main.time * 0.06f
    //                        //Main.graphics.GraphicsDevice.Textures[0] = GetTexture("IllusionBoundMod/Images/BaseTex_7");//字面意义，base那个是不会随时间动的，ani那个会动//BaseTex
    //                        //Main.graphics.GraphicsDevice.Textures[1] = GetTexture("IllusionBoundMod/Images/AniTex");
    //                        //Main.graphics.GraphicsDevice.Textures[2] = itemTex;
    //                        ////if (IllusionSwooshConfigClient.instance.swooshColorType == SwooshColorType.函数生成热度图) 
    //                        ////{
    //                        ////    var colorBar = new Texture2D(Main.graphics.GraphicsDevice,300,60);
    //                        ////    colorBar.SetData<Color>();
    //                        ////    Main.graphics.GraphicsDevice.Textures[3] = colorBar;
    //                        ////}


    //                        //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicClamp;
    //                        //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.AnisotropicClamp;
    //                        //Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.AnisotropicClamp;
    //                        //Main.graphics.GraphicsDevice.SamplerStates[3] = SamplerState.AnisotropicClamp;

    //                        //var passCount = 0;
    //                        //if (IllusionSwooshConfigClient.instance.swooshColorType == SwooshColorType.武器贴图对角线) passCount++;
    //                        ////if (alphaBlend) passCount += 2;
    //                        //IllusionBoundMod.ShaderSwooshEX.CurrentTechnique.Passes[passCount].Apply();
    //                        //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
    //                        //Main.graphics.GraphicsDevice.RasterizerState = originalState;
    //                    }
    //                }

    //                var tp = u + v + drawPlayer.Center;
    //                if (tp != default)
    //                {
    //                    targetPos = tp;
    //                    drawPlayer.bodyFrame.Y = 112 + 56 * (int)(Math.Abs(new Vector2(-targetPos.Y, targetPos.X).ToRotation()) / MathHelper.Pi * 3);
    //                }
    //                else
    //                {
    //                    Main.NewText("???");
    //                }
    //                if (fac != 0)
    //                {
    //                    //var color = Main.hslToRgb(0.33f, 0.75f, 0.75f);
    //                    //.5f
    //                    var num0 = modPlayer.negativeDir ? 1 : 0;
    //                    c[0] = new CustomVertexInfo(drawCen, Color.Transparent, new Vector3(0, 1, 0));//因为零向量固定是左下角所以纹理固定(0,1)
    //                    c[1] = new CustomVertexInfo(u + drawCen, Color.Transparent, new Vector3(num0 ^ 1, num0 ^ 1, 0));//这一处也许有更优美的写法
    //                    c[2] = new CustomVertexInfo(v + drawCen, Color.Transparent, new Vector3(num0, num0, 0));
    //                    c[3] = c[1];
    //                    c[4] = new CustomVertexInfo(u + v + drawCen, Color.Transparent, new Vector3(1, 0, 0));//因为u+v固定是右上角所以纹理固定(1,0)

    //                    //Main.NewText(targetPos);
    //                    c[5] = c[2];
    //                    //Main.spriteBatch.DrawLine(u + v + drawPlayer.Center, drawPlayer.Center, Color.Red);
    //                    Main.spriteBatch.End();
    //                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
    //                    IllusionBoundMod.IMBellEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
    //                    //将变换矩阵作用在正交投影矩阵上，具体结果以及意义我下次再想想
    //                    //半年前就问过零群各位大佬，他们都说没必要搞懂，tr图像变换矩阵而已。
    //                    IllusionBoundMod.IMBellEffect.Parameters["uTime"].SetValue((float)Main.time / 60 % 1);//传入时间偏移量
    //                    IllusionBoundMod.IMBellEffect.Parameters["uItemColor"].SetValue(Vector4.One);
    //                    //传入顶点绘制出的物品的颜色，这里采用环境光，和sb.Draw的那个color参数差不多(吧
    //                    IllusionBoundMod.IMBellEffect.Parameters["uItemGlowColor"].SetValue(Vector4.One);
    //                    Main.graphics.GraphicsDevice.Textures[0] = itemTex;//传入物品贴图
    //                    Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/IMBellTex");//传入因时间而x纹理坐标发生偏移的灰度图，这里其实并不明显，你可以参考我mod里的无间之钟在黑暗环境下的效果
    //                    Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.GetTexture("Images/Style_18");//传入固定叠加的灰度图
    //                                                                                                              //上面这两个灰度图叠加后作为插值的t，大概是这样的映射:t=0时最终物品上的颜色是0(黑色，additive模式下是透明的)，t=0.5时是color（顶点传入的color参数，不是上面uItemColor,t=1时是1(白色)
    //                    Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
    //                    Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
    //                    Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
    //                    //Main.graphics.GraphicsDevice.SamplerStates[3] = sampler;

    //                    IllusionBoundMod.IMBellEffect.CurrentTechnique.Passes[1].Apply();//这里是第三个pass，可以直接写下标不必写pass名(
    //                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, c, 0, 2);
    //                    Main.graphics.GraphicsDevice.RasterizerState = originalState;

    //                    Main.spriteBatch.End();
    //                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
    //                }
    //                //float num20 = (u+v).ToRotation() * drawPlayer.direction;
    //                //drawPlayer.bodyFrame.Y = drawPlayer.bodyFrame.Height * 3;
    //                //if ((double)num20 < -0.75)
    //                //{
    //                //    drawPlayer.bodyFrame.Y = drawPlayer.bodyFrame.Height * 2;
    //                //    if (drawPlayer.gravDir == -1f)
    //                //    {
    //                //        drawPlayer.bodyFrame.Y = drawPlayer.bodyFrame.Height * 4;
    //                //    }
    //                //}
    //                //if ((double)num20 > 0.6)
    //                //{
    //                //    drawPlayer.bodyFrame.Y = drawPlayer.bodyFrame.Height * 4;
    //                //    if (drawPlayer.gravDir == -1f)
    //                //    {
    //                //        drawPlayer.bodyFrame.Y = drawPlayer.bodyFrame.Height * 2;
    //                //        return;
    //                //    }
    //                //}
    //                //var vel = u + v;
    //                //bodyRec = drawPlayer.bodyFrame;
    //                //bodyRec.Y = 112 + 56 * (int)(Math.Abs(new Vector2(-vel.Y, vel.X).ToRotation()) / MathHelper.Pi * 3);
    //                ////drawPlayer.bodyFrame.Y = 112 + 56 * (int)(Math.Abs(new Vector2(-vel.Y, vel.X).ToRotation()) / MathHelper.Pi * 3);
    //                //Main.spriteBatch.End();
    //                //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
    //            }
    //            catch
    //            {

    //            }
    //        }
    //        Vector2[] oldPlayerPos = new Vector2[30];
    //        //(Vector2, Vector2) PairGetter(int index) => (projectile.oldPos[index], oldPlayerPos[index]);
    //        public void DrawStarSwoosh_Roll(float fac, bool extra = false, float size = 1f)
    //        {
    //            try
    //            {
    //                if (IllusionBoundMod.ShaderSwooshEX == null) return;
    //                if (IllusionBoundMod.IMBellEffect == null) return;
    //                if (Main.GameViewMatrix == null) return;
    //                var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;
    //                var modPlayer = drawPlayer.GetModPlayer<WeaponDisplayForPlayer>();
    //                //var fac = modPlayer.factorGeter;
    //                fac = 1 - fac;
    //                var drawCen = (drawPlayer.gravDir == -1 ? new Vector2(drawPlayer.Center.X, (2 * (Main.screenPosition + new Vector2(960, 560)) - drawPlayer.Center - new Vector2(0, 96)).Y) : drawPlayer.Center) + new Vector2(0, drawPlayer.gfxOffY);
    //                float rotVel = projectile.frameCounter == 1 ? MathHelper.Clamp(IllusionSwooshConfigClient.instance.rotationVelocity, 1, 3) : 1;
    //                CustomVertexInfo[] c = new CustomVertexInfo[6];
    //                var itemTex = TextureAssets.Item[drawPlayer.HeldItem.type].Value;
    //                float xScaler = IllusionSwooshConfigClient.instance.swooshFactorStyle == SwooshFactorStyle.系数中间插值 ? MathHelper.Lerp(modPlayer.kValue, modPlayer.kValueNext, fac) : modPlayer.kValue;//获取x轴方向缩放系数
    //                float scaler = itemTex.Size().Length() * drawPlayer.HeldItem.scale / xScaler * 0.5f * trans.M11 * size;//对椭圆进行位似变换(你直接说坐标乘上一个系数不就好了吗，屑阿汪// * IllusionSwooshConfigClient.instance.swooshSize
    //                var swooshAniFac = 1 - (float)Math.Sqrt(1 - fac * fac);
    //                swooshAniFac = MathHelper.Clamp(swooshAniFac, 0, 1);
    //                var vec = Main.MouseWorld - drawPlayer.Center;
    //                vec.Y *= drawPlayer.gravDir;
    //                if (!Main.gamePaused)
    //                {
    //                    drawPlayer.direction = Math.Sign(vec.X);

    //                    projectile.rotation = vec.ToRotation();
    //                }


    //                //var theta = ((float)Math.Pow(fac, 2)).Lerp(MathHelper.Pi / 8 * 3, -MathHelper.PiOver2 - MathHelper.Pi / 8) * rotVel;
    //                var theta = (-MathHelper.PiOver2 - MathHelper.Pi / 8 - MathHelper.Pi / 8 * 3) * (float)Math.Pow(fac, 2) * rotVel * 2 + MathHelper.Pi / 8 * 3;

    //                theta = drawPlayer.direction == -1 ? MathHelper.Pi * 1f - theta : theta;

    //                var cos = (float)Math.Cos(theta) * scaler;
    //                var sin = (float)Math.Sin(theta) * scaler;//这里(cos,sin)对应的位置就是我们希望贴图右上角所在的位置，而(0,0)对应的位置是贴图左下角所在的位置

    //                var u = new Vector2(xScaler * (cos - sin), -cos - sin).RotatedBy(projectile.rotation);
    //                var v = new Vector2(-xScaler * (cos + sin), sin - cos).RotatedBy(projectile.rotation);
    //                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
    //                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
    //                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
    //                List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
    //                Color realColor = Color.White;
    //                SamplerState sampler;
    //                switch (IllusionSwooshConfigClient.instance.swooshSampler)
    //                {
    //                    default:
    //                    case SwooshSamplerState.各向异性: sampler = SamplerState.AnisotropicClamp; break;
    //                    case SwooshSamplerState.线性: sampler = SamplerState.PointClamp; break;
    //                    case SwooshSamplerState.点: sampler = SamplerState.PointClamp; break;
    //                }

    //                var tp = u + v;
    //                if (tp != default)
    //                {
    //                    targetPos = tp + drawPlayer.Center;
    //                    drawPlayer.bodyFrame.Y = 112 + 56 * (int)(Math.Abs(new Vector2(-targetPos.Y, targetPos.X).ToRotation()) / MathHelper.Pi * 3);
    //                    projectile.oldPos.UpdateArray(tp + drawCen, tp + drawCen, !Main.gamePaused);
    //                    oldPlayerPos.UpdateArray(drawCen, drawCen, !Main.gamePaused);
    //                }
    //                else
    //                {
    //                    Main.NewText("???");
    //                }
    //                int n;
    //                for (n = 29; n >= 0; n--)
    //                {
    //                    if (n == 0 || projectile.oldPos[n] != projectile.oldPos[n - 1]) break;
    //                }
    //                if (n > 1)
    //                {
    //                    var oldpos = projectile.oldPos.CatMullRomCurve(n * 4, (0, n));
    //                    var oldplayerpos = oldPlayerPos.CatMullRomCurve(n * 4, (0, n));
    //                    var nl = oldpos.Length;
    //                    for (int i = 0; i < nl; i++)// * 4
    //                    {
    //                        var f = (i + 1f) / nl;
    //                        var alphaLight = 0.6f;
    //                        var _f = 6 * f / (3 * f + 1);//f;// 
    //                        _f = MathHelper.Clamp(_f, 0, 1);
    //                        realColor.A = (byte)(_f * 255);//.MultiplyRGBA(new Color(1,1,1,_f))
    //                        bars.Add(new CustomVertexInfo(oldpos[nl - 1 - i], realColor, new Vector3(1 - f, 1, alphaLight)));//(3 * f - 4) / (4 * f - 3)//快乐连顶点
    //                        realColor.A = 0;
    //                        bars.Add(new CustomVertexInfo(oldplayerpos[nl - 1 - i], realColor, new Vector3(0, 0, alphaLight)));
    //                    }
    //                    List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
    //                    for (int i = 0; i < bars.Count - 2; i += 2)
    //                    {
    //                        triangleList.Add(bars[i]);
    //                        triangleList.Add(bars[i + 2]);
    //                        triangleList.Add(bars[i + 1]);

    //                        triangleList.Add(bars[i + 1]);
    //                        triangleList.Add(bars[i + 2]);
    //                        triangleList.Add(bars[i + 3]);
    //                    }
    //                    var render = IllusionBoundMod.Instance.render;
    //                    var gd = Main.graphics.GraphicsDevice;
    //                    var sb = Main.spriteBatch;
    //                    sb.End();
    //                    gd.SetRenderTarget(render);
    //                    gd.Clear(Color.Transparent);
    //                    sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);
    //                    IllusionBoundMod.ShaderSwooshEX.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
    //                    IllusionBoundMod.ShaderSwooshEX.Parameters["uLighter"].SetValue(IllusionSwooshConfigClient.instance.luminosityFactor);
    //                    IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(0);
    //                    IllusionBoundMod.ShaderSwooshEX.Parameters["checkAir"].SetValue(IllusionSwooshConfigClient.instance.checkAir);
    //                    IllusionBoundMod.ShaderSwooshEX.Parameters["airFactor"].SetValue(1f);
    //                    IllusionBoundMod.ShaderSwooshEX.Parameters["gather"].SetValue(IllusionSwooshConfigClient.instance.gather);
    //                    Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/BaseTex_" + IllusionSwooshConfigClient.instance.ImageIndex);
    //                    Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/AniTex");
    //                    Main.graphics.GraphicsDevice.Textures[2] = itemTex;
    //                    Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
    //                    Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
    //                    Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
    //                    Main.graphics.GraphicsDevice.SamplerStates[3] = sampler;
    //                    IllusionBoundMod.ShaderSwooshEX.CurrentTechnique.Passes[0].Apply();
    //                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
    //                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
    //                    sb.End();
    //                    gd.SetRenderTarget(Main.screenTargetSwap);
    //                    gd.Clear(Color.Transparent);
    //                    sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
    //                    Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
    //                    IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
    //                    IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);
    //                    IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
    //                    IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
    //                    IllusionBoundMod.Distort.Parameters["position"].SetValue(drawPlayer.Center);
    //                    IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
    //                    IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));
    //                    sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
    //                    sb.End();
    //                    gd.SetRenderTarget(Main.screenTarget);
    //                    gd.Clear(Color.Transparent);
    //                    sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
    //                    sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
    //                }
    //                var num0 = modPlayer.negativeDir ? 0 : 1;
    //                c[0] = new CustomVertexInfo(drawCen, Color.Transparent, new Vector3(0, 1, 0));
    //                c[1] = new CustomVertexInfo(u + drawCen, Color.Transparent, new Vector3(num0 ^ 1, num0 ^ 1, 0));
    //                c[2] = new CustomVertexInfo(v + drawCen, Color.Transparent, new Vector3(num0, num0, 0));
    //                c[3] = c[1];
    //                c[4] = new CustomVertexInfo(u + v + drawCen, Color.Transparent, new Vector3(1, 0, 0));
    //                c[5] = c[2];
    //                Main.spriteBatch.End();
    //                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
    //                IllusionBoundMod.IMBellEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
    //                IllusionBoundMod.IMBellEffect.Parameters["uTime"].SetValue((float)Main.time / 60 % 1);
    //                IllusionBoundMod.IMBellEffect.Parameters["uItemColor"].SetValue(Vector4.One);
    //                IllusionBoundMod.IMBellEffect.Parameters["uItemGlowColor"].SetValue(Vector4.One);
    //                Main.graphics.GraphicsDevice.Textures[0] = itemTex;//传入物品贴图
    //                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/IMBellTex");
    //                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.GetTexture("Images/Style_18");
    //                Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
    //                Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
    //                Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
    //                IllusionBoundMod.IMBellEffect.CurrentTechnique.Passes[1].Apply();
    //                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, c, 0, 2);
    //                Main.graphics.GraphicsDevice.RasterizerState = originalState;
    //                Main.spriteBatch.End();
    //                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
    //            }
    //            catch
    //            {

    //            }
    //        }
    //        public override bool PreDraw(ref Color lightColor)
    //        {
    //            //Main.NewText((targetPos, drawPlayer.Center));
    //            if (projectile.frameCounter > 0)
    //            {
    //                DrawStarSwoosh_Roll(projectile.ai[0] / drawPlayer.itemAnimationMax, extra);
    //            }
    //            else
    //            {
    //                DrawStarSwoosh(projectile.ai[0] / drawPlayer.itemAnimationMax, extra);

    //            }
    //            //if(targetPos!=default)
    //            //spriteBatch.DrawLine(targetPos, drawPlayer.Center, Color.White, 4, false, -Main.screenPosition);
    //            return false;
    //        }
    //        public virtual bool extra => drawPlayer.HeldItem.type == ModContent.ItemType<AsuterosaberuDXEX>() || drawPlayer.HeldItem.type == ModContent.ItemType<AsuterosaberuCSM>();
    //    }
    //    public class AstralTearRemake : ModProjectile
    //    {
    //        public override void SetStaticDefaults()
    //        {
    //            DisplayName.SetDefault("星辉裂空");
    //        }
    //        Projectile projectile => Projectile;
    //        public override bool PreDraw(ref Color lightColor)
    //        {
    //            var spriteBatch = Main.spriteBatch;

    //            switch (state)
    //            {

    //                case 0:
    //                case 1:
    //                    {
    //                        var render = IllusionBoundMod.Instance.render;
    //                        var gd = Main.graphics.GraphicsDevice;
    //                        //先在自己的render上画这个弹幕
    //                        spriteBatch.End();
    //                        gd.SetRenderTarget(render);
    //                        gd.Clear(Color.Transparent);
    //                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
    //                        Vector2 scale = new Vector2(1, MathHelper.Clamp(projectile.velocity.Length() / 3f, 1, 10));
    //                        spriteBatch.Draw(IllusionBoundMod.GetTexture("Projectiles/FinalFractal/FinalFractalLight"), projectile.Center - Main.screenPosition, null, Main.hslToRgb(projectile.localAI[0] % 1, 1, 0.75f) * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale * 1.5f, 0, 0);
    //                        spriteBatch.Draw(IllusionBoundMod.GetTexture("Projectiles/FinalFractal/FinalFractalLight"), projectile.Center - Main.screenPosition, null, Color.White * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale, 0, 0);
    //                        spriteBatch.End();
    //                        //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
    //                        //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
    //                        gd.SetRenderTarget(Main.screenTargetSwap);
    //                        gd.Clear(Color.Transparent);
    //                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
    //                        if (state == 1)
    //                        {
    //                            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
    //                            IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
    //                            IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
    //                                                                                         //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
    //                            IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
    //                            IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
    //                            IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.player[projectile.owner].Center);
    //                            IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
    //                            IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)
    //                        }


    //                        spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
    //                        spriteBatch.End();

    //                        //最后在screenTarget上把刚刚的结果画上
    //                        gd.SetRenderTarget(Main.screenTarget);
    //                        gd.Clear(Color.Transparent);
    //                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
    //                        spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);

    //                        if (state == 0)
    //                        {
    //                            spriteBatch.End();
    //                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
    //                            spriteBatch.Draw(render, Vector2.Zero, Color.White);
    //                            spriteBatch.End();
    //                            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
    //                        }


    //                        break;
    //                    }
    //                case 2:
    //                case 3:
    //                    {
    //                        var fac = projectile.ai[0].SymmetricalFactor(90, 10);
    //                        var render = IllusionBoundMod.Instance.render;
    //                        var gd = Main.graphics.GraphicsDevice;
    //                        var sb = Main.spriteBatch;
    //                        //先在自己的render上画这个弹幕
    //                        sb.End();
    //                        gd.SetRenderTarget(render);
    //                        gd.Clear(Color.Transparent);
    //                        sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
    //                        IllusionBoundMod.TransformEffect.Parameters["factor1"].SetValue(fac);
    //                        IllusionBoundMod.TransformEffect.CurrentTechnique.Passes[0].Apply();
    //                        sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(512), (state == 3 ? 2f : 1.5f) * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)
    //                        sb.End();
    //                        //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
    //                        //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
    //                        gd.SetRenderTarget(Main.screenTargetSwap);
    //                        gd.Clear(Color.Transparent);
    //                        sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
    //                        Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
    //                        IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
    //                        IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
    //                                                                                     //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
    //                        IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
    //                        IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
    //                        IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.player[projectile.owner].Center + projectile.rotation.ToRotationVector2() * (float)IllusionBoundMod.ModTime * 8);
    //                        IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
    //                        //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
    //                        //Main.NewText("!!!");
    //                        IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)

    //                        sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
    //                        sb.End();

    //                        //最后在screenTarget上把刚刚的结果画上
    //                        gd.SetRenderTarget(Main.screenTarget);
    //                        gd.Clear(Color.Transparent);
    //                        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
    //                        sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
    //                        //sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(512), 1.5f * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)
    //                        //sb.Draw(render, Vector2.Zero, Color.White);
    //                        break;
    //                    }
    //            }
    //            return false;
    //        }
    //        public override void SetDefaults()
    //        {
    //            projectile.width = 90;
    //            projectile.height = 90;
    //            projectile.DamageType = DamageClass.Melee;
    //            projectile.friendly = true;
    //            projectile.timeLeft = 180;
    //            projectile.aiStyle = -1;
    //            projectile.ignoreWater = true;
    //            projectile.penetrate = -1;
    //            projectile.tileCollide = false;

    //        }
    //        public override bool ShouldUpdatePosition() => false;
    //        int state => (int)projectile.ai[1];
    //        NPC target => projectile.frame == 0 ? null : Main.npc[projectile.frame - 1];
    //        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    //        {
    //            target.immune[projectile.owner] = 3;
    //        }
    //        public override void AI()
    //        {
    //            projectile.ai[0]++;
    //            switch (state)
    //            {
    //                case 0:
    //                case 1:
    //                    {
    //                        var oldpos = projectile.Center;
    //                        projectile.Center = target.Center + projectile.rotation.ToRotationVector2() * (state == 1 ? 192 : 128) * (2 / 15f * projectile.ai[0] - 1);
    //                        projectile.velocity = projectile.Center - oldpos;
    //                        if (projectile.ai[0] > 15) projectile.Kill();
    //                        break;
    //                    }
    //                case 2:
    //                case 3:
    //                    {
    //                        if ((int)projectile.ai[0] % (state == 3 ? 10 : 20) == 0)
    //                        {
    //                            int n = 0;
    //                            foreach (var target in Main.npc)
    //                            {
    //                                var length = (target.Center - projectile.Center).Length();
    //                                if (target.active && !target.friendly && target.chaseable && !target.dontTakeDamage && length < (state == 2 ? 1024 : 768) && Main.rand.NextFloat(0, length * length / 128) < length)
    //                                {
    //                                    n++;
    //                                    var rand = Main.rand.NextFloat(0, MathHelper.TwoPi);
    //                                    var p = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), default, default, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, state - 2);
    //                                    p.rotation = rand;
    //                                    p.frame = target.whoAmI + 1;
    //                                    p.height = p.width = 20;
    //                                    p.Center = target.Center - rand.ToRotationVector2() * (state == 3 ? 192 : 128);
    //                                    p.localAI[0] = Main.rand.NextFloat(0, 1);
    //                                }
    //                                if (n > (state == 3 ? 8 : 5)) break;
    //                            }
    //                        }
    //                        if (projectile.ai[0] > 300) projectile.Kill();
    //                        break;
    //                    }
    //            }
    //        }
    //    }
    //    //public class AsuterosaberuDXSwooshEX : AsuterosaberuDXSwoosh
    //    //{
    //    //}
    //    public class AsuterosaberuDXPlayer : ModPlayer 
    //    {

    //    }
    public class AsuterosaberuDX : GlowItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("彩虹色的剑刃锋利到能够劈开空间\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("天文军刀豪华版");
        }
        public Item item => Item;
        public override void SetDefaults()
        {
            item.damage = 100;
            item.crit = 14;
            item.DamageType = DamageClass.Melee;
            item.width = 92;
            item.rare = MyRareID.Tier1;
            item.height = 92;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 8;
            item.useStyle = 1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<AsuterosaberuDXProj>();
            item.shootSpeed = 1f;
            item.noUseGraphic = true;
            item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.WhitePhasesaber);
            recipe1.AddIngredient(ItemID.RedPhasesaber);
            recipe1.AddIngredient(ItemID.GreenPhasesaber);
            recipe1.AddIngredient(ItemID.BluePhasesaber);
            recipe1.AddIngredient(ItemID.YellowPhasesaber);
            recipe1.AddIngredient(ItemID.PurplePhasesaber);
            recipe1.AddIngredient(ItemID.FragmentStardust, 30);
            recipe1.AddIngredient(ItemID.FragmentVortex, 30);
            recipe1.AddIngredient(ItemID.FragmentNebula, 30);
            recipe1.AddIngredient(ItemID.FragmentSolar, 30);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient<Materials.AncientEssence>(1000);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class AsuterosaberuDXEX : AsuterosaberuDX
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("彩虹色的剑刃锋利到能够劈开空间\n破碎吧，属于空间的秩序\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");//\n[c / 9933cc: X:cos(3T) * sin((5T) + 5) Y: sin(3T) * sin((5T) + 5)]
            DisplayName.SetDefault("天文军刀豪华版EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 250;
            item.rare = MyRareID.Tier2;
            item.crit = 36;
            item.useTime = 18;
            item.useAnimation = 18;
        }
        public override void AddRecipes()
        {
        }
    }
    public class AsuterosaberuCSM : AsuterosaberuDXEX
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("源生纯粹七元霓辉空间水晶所铸造的利刃，也许它是斩断了空间逃离了原本的世界来到这里？\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");//\n[c / 9933cc: X:cos(3T) * sin((5T) + 5) Y: sin(3T) * sin((5T) + 5)]
            DisplayName.SetDefault("天文军刀CSM");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = MyRareID.Tier3;
            item.width = item.height = 94;
        }
    }
    public class AsuterosaberuDXProj : BossDrop.SolusKatana.SolusKatanaProj
    {
        public override string Texture => base.Texture;//"VirtualDream/Contents/StarBound/Weapons/UniqueWeapon/AsuterosaberuDX/AsuterosaberuDX"
        public override T UpgradeValue<T>(T normal, T extra, T ultra, T defaultValue = default)
        {
            var type = sourceItem.type;
            if (type == ModContent.ItemType<AsuterosaberuDX>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<AsuterosaberuDXEX>())
            {
                return extra;
            }

            if (type == ModContent.ItemType<AsuterosaberuCSM>())
            {
                return ultra;
            }

            return defaultValue;
        }
        public override bool DrawLaserFire => false;
        public override void OnChargedShoot()
        {
            SoundEngine.PlaySound(SoundID.Item60, projectile.position);
            Vector2 unit = (Main.MouseWorld - Player.Center).SafeNormalize(default);
            //for (int n = 0; n < 4; n++) 
            //{
            //    Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), Player.Center + unit * 64, default, ModContent.ProjectileType<AstralTear>(), projectile.damage / 9, projectile.knockBack, projectile.owner, 0, UpgradeValue(2, 3, 3)).rotation = unit.ToRotation();// + Vector2.Normalize(Main.MouseWorld - Player.Center) * 60
            //    unit = new Vector2(-unit.Y, unit.X);
            //}
            Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), Player.Center + unit * 64, default, ModContent.ProjectileType<AstralTear>(), projectile.damage / 9, projectile.knockBack, projectile.owner, 0, UpgradeValue(2, 3, 3)).rotation = unit.ToRotation();// + Vector2.Normalize(Main.MouseWorld - Player.Center) * 60

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("天文军刀豪华版");
        }
        public override void VertexInfomation(ref bool additive, ref int indexOfGreyTex, ref float endAngle, ref bool useHeatMap)
        {
            additive = true;
            indexOfGreyTex = UpgradeValue(5, 7, 7);
            useHeatMap = true;
        }
        public override void RenderInfomation(ref (float M, float Intensity, float Range) useBloom, ref (float M, float Range, Vector2 director) useDistort, ref (Texture2D fillTex, Vector2 texSize, Color glowColor, Color boundColor, float tier1, float tier2, Vector2 offset, bool lightAsAlpha) useMask)
        {
            //base.RenderInfomation(ref useBloom, ref useDistort, ref useMask);
            useMask = (IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3"), new Vector2(64, 48), Color.Cyan, Color.White, 0.1f, 0.11f, Player.Center + new Vector2(0.707f) * (float)IllusionBoundMod.ModTime * 8, true);
        }
    }
    public class AstralTear : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("星辉裂空");
        }
        Projectile projectile => Projectile;
        public override bool PreDraw(ref Color lightColor)
        {
            var spriteBatch = Main.spriteBatch;
            if (state == 0)
            {
                Vector2 scale = new Vector2(0.5f, MathHelper.Clamp(projectile.velocity.Length() / 3f, 1, 10));
                var _color = Main.hslToRgb(projectile.localAI[0] % 1, 1, 0.75f);
                _color.A = 0;
                var tex = IllusionBoundMod.GetTexture(Texture.Replace("AstralTear", "CrystalLight"), false);
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, _color * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale * 1.5f, 0, 0);
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, new Color(1f, 1f, 1f, 0f) * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale, 0, 0);
            }
            #region MyRegion
            //switch (state)
            //{

            //    case 0:
            //    case 1:
            //        {
            //            var render = IllusionBoundMod.Instance.render;
            //            var gd = Main.graphics.GraphicsDevice;
            //            //先在自己的render上画这个弹幕
            //            spriteBatch.End();
            //            gd.SetRenderTarget(render);
            //            gd.Clear(Color.Transparent);
            //            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //            Vector2 scale = new Vector2(1, MathHelper.Clamp(projectile.velocity.Length() / 3f, 1, 10));
            //            spriteBatch.Draw(IllusionBoundMod.GetTexture("Projectiles/FinalFractal/FinalFractalLight"), projectile.Center - Main.screenPosition, null, Main.hslToRgb(projectile.localAI[0] % 1, 1, 0.75f) * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale * 1.5f, 0, 0);
            //            spriteBatch.Draw(IllusionBoundMod.GetTexture("Projectiles/FinalFractal/FinalFractalLight"), projectile.Center - Main.screenPosition, null, Color.White * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale, 0, 0);
            //            spriteBatch.End();
            //            //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
            //            //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
            //            gd.SetRenderTarget(Main.screenTargetSwap);
            //            gd.Clear(Color.Transparent);
            //            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
            //            if (state == 1)
            //            {
            //                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
            //                IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
            //                IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
            //                                                                             //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
            //                IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
            //                IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
            //                IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.player[projectile.owner].Center);
            //                IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
            //                IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)
            //            }


            //            spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
            //            spriteBatch.End();

            //            //最后在screenTarget上把刚刚的结果画上
            //            gd.SetRenderTarget(Main.screenTarget);
            //            gd.Clear(Color.Transparent);
            //            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //            spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);

            //            if (state == 0)
            //            {
            //                spriteBatch.End();
            //                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //                spriteBatch.Draw(render, Vector2.Zero, Color.White);
            //                spriteBatch.End();
            //                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //            }


            //            break;
            //        }
            //    case 2:
            //    case 3:
            //        {
            //            var fac = projectile.ai[0].SymmetricalFactor(90, 10);
            //            var render = IllusionBoundMod.Instance.render;
            //            var gd = Main.graphics.GraphicsDevice;
            //            var sb = Main.spriteBatch;
            //            //先在自己的render上画这个弹幕
            //            sb.End();
            //            gd.SetRenderTarget(render);
            //            gd.Clear(Color.Transparent);
            //            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
            //            IllusionBoundMod.TransformEffect.Parameters["factor1"].SetValue(fac);
            //            IllusionBoundMod.TransformEffect.CurrentTechnique.Passes[0].Apply();
            //            sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(512), (state == 3 ? 2f : 1.5f) * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)
            //            sb.End();
            //            //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
            //            //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
            //            gd.SetRenderTarget(Main.screenTargetSwap);
            //            gd.Clear(Color.Transparent);
            //            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
            //            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Backgrounds/StarSkyv3");//StarSky_0
            //            IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
            //            IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
            //                                                                         //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
            //            IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.1f);
            //            IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.15f);
            //            IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.player[projectile.owner].Center + projectile.rotation.ToRotationVector2() * (float)IllusionBoundMod.ModTime * 8);
            //            IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
            //            //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
            //            //Main.NewText("!!!");
            //            IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)

            //            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
            //            sb.End();

            //            //最后在screenTarget上把刚刚的结果画上
            //            gd.SetRenderTarget(Main.screenTarget);
            //            gd.Clear(Color.Transparent);
            //            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            //            //sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(512), 1.5f * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)
            //            //sb.Draw(render, Vector2.Zero, Color.White);
            //            break;
            //        }
            //}
            #endregion
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
        NPC target => projectile.frame == 0 ? null : Main.npc[projectile.frame - 1];
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
        }
        public override void AI()
        {
            projectile.ai[0]++;
            switch (state)
            {
                case 0:
                case 1:
                    {
                        var oldpos = projectile.Center;
                        projectile.Center = target.Center + projectile.rotation.ToRotationVector2() * (state == 1 ? 384 : 256) * (2 / 15f * projectile.ai[0] - 1);
                        projectile.velocity = projectile.Center - oldpos;
                        if (projectile.ai[0] > 15) projectile.Kill();
                        break;
                    }
                case 2:
                case 3:
                    {
                        if ((int)projectile.ai[0] % (state == 3 ? 2 : 3) == 0)
                        {
                            //int n = 0;
                            foreach (var target in Main.npc)
                            {
                                var length = (target.Center - projectile.Center).Length();
                                if (target.active && !target.friendly && target.chaseable && !target.dontTakeDamage && length < (state == 2 ? 1024 : 768) && Main.rand.NextFloat(0, length * length / 128) < length)
                                {
                                    //n++;
                                    var rand = Main.rand.NextFloat(0, MathHelper.TwoPi);
                                    var p = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), default, default, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, state - 2);
                                    p.rotation = rand;
                                    p.frame = target.whoAmI + 1;
                                    p.height = p.width = 20;
                                    p.localAI[0] = Main.rand.NextFloat(0, 1);

                                    p.Center = target.Center - rand.ToRotationVector2() * (state == 3 ? 384 : 256);
                                    break;
                                }
                                //if (n > (state == 3 ? 8 : 5)) break;

                            }
                        }
                        if (projectile.ai[0] > 300) projectile.Kill();
                        break;
                    }
            }
        }
    }
}