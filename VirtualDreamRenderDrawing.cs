using LogSpiralLibrary;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.GameContent.Drawing;
using VirtualDream.Contents.StarBound.NPCs.Bosses.AsraNox;
using VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana;
using VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.AsuterosaberuDX;
using VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.OculusReaver;
using static LogSpiralLibrary.LogSpiralLibraryMod;
using static Terraria.ModLoader.ModContent;
using VirtualDream.Contents.StarBound.TimeBackTracking;

namespace VirtualDream
{
    public class VirtualDreamRenderDrawing : RenderBasedDrawing
    {
        public override void CommonDrawingMethods(SpriteBatch spriteBatch)
        {
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            List<int> indexer = new List<int>();
            //Player player = null;
            List<Projectile> oculusTears = new List<Projectile>();
            List<Projectile> astralTears = new List<Projectile>();
            List<Projectile> solusKatanaFractal = new List<Projectile>();
            var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;
            #region 遍历查找
            float? dir = 0;
            int timeleft = 0;
            GetMyDataOut(bars, oculusTears, astralTears, solusKatanaFractal, null, indexer, ref dir, ref timeleft);
            #endregion
            #region 日炎刀合批
            if (bars.Count > 2 || solusKatanaFractal.Count > 0)
            {
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                var resultMatrix = model * trans * projection;
                SolarKatanaDrawing(solusKatanaFractal, indexer, null, null, null, spriteBatch, trans, resultMatrix, bars, null, dir, false);
            }
            #endregion
            if (oculusTears.Count > 0 || astralTears.Count > 0)
            {
                //先在自己的render上画这个弹幕
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
                OculusTearsDrawingContent(oculusTears, spriteBatch, new Color(1, 0, 0.25f));
                AstralTearsDrawingContent(astralTears, spriteBatch, false, trans);
                spriteBatch.End();
            }
        }
        private static void OculusTearsDrawingContent(List<Projectile> oculusTears, SpriteBatch spriteBatch, Color color)
        {
            foreach (var projectile in oculusTears)
            {
                var fac = projectile.ai[0];
                fac = fac < 30 ? ((fac * fac * 0.02022f - fac * 0.606f + 5) * (1 - 0.03f * fac)) : ((-90 / (fac - 181f) * 1.25f) * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f * (fac - 30).SymmetricalFactor(75, 15)));
                //var fac = projectile.ai[0].SymmetricalFactor(90, 10) * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f);
                //var fac = projectile.ai[0].HillFactor2(180);
                TransformEffectEX.Parameters["factor1"].SetValue(fac);
                TransformEffectEX.Parameters["factor2"].SetValue((float)ModTime / 30f);
                TransformEffectEX.CurrentTechnique.Passes[1].Apply();
                spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, color, projectile.rotation, new Vector2(512), ((int)projectile.ai[1] == 3 ? 2.5f : 2f) * 46 / 512 * new Vector2(3, 1), 0, 0);//new Rectangle(240,240,92,92)
            }
        }
        private static void AstralTearsDrawingContent(List<Projectile> astralTears, SpriteBatch spriteBatch, bool containsSlash, Matrix trans)
        {
            if (containsSlash)
            {
                foreach (var projectile in astralTears)
                {
                    if ((int)projectile.ai[1] == 1)
                    {
                        Vector2 scale = new Vector2(1, MathHelper.Clamp(projectile.velocity.Length() / 3f, 1, 10));
                        var _color = Main.hslToRgb(projectile.localAI[0] % 1, 1, 0.75f);
                        var tex = VirtualDreamMod.GetTexture(projectile.ModProjectile.Texture.Replace("AstralTear", "CrystalLight"), false);
                        spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, _color * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale * 1.5f, 0, 0);
                        spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, Color.White * projectile.ai[0].SymmetricalFactor(7.5f, 7.5f), projectile.rotation - MathHelper.PiOver2, new Vector2(36), scale, 0, 0);
                    }
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
            }

            foreach (var projectile in astralTears)
            {
                if ((int)projectile.ai[1] != 1)
                {
                    var fac = MathF.Pow(projectile.ai[0].SymmetricalFactor(90, 30), 4) * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f);
                    //TransformEffect.Parameters["factor1"].SetValue(fac);
                    //TransformEffect.Parameters["factor2"].SetValue((float)ModTime / 30f);
                    //TransformEffect.CurrentTechnique.Passes[0].Apply();
                    //spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, containsSlash ? Color.White : Color.Cyan, projectile.rotation, new Vector2(512), ((int)projectile.ai[1] == 3 ? 2.5f : 2f) * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)// * new Vector2(3, 1)

                    TransformEffectEX.Parameters["factor1"].SetValue(fac);
                    TransformEffectEX.Parameters["factor2"].SetValue((float)ModTime / 30f);
                    TransformEffectEX.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, containsSlash ? Color.White : Color.Cyan, projectile.rotation, new Vector2(512), ((int)projectile.ai[1] == 3 ? 2.5f : 2f) * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)// * new Vector2(3, 1)
                }
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
        }
        public override void RenderDrawingMethods(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, RenderTarget2D render, RenderTarget2D renderAirDistort)
        {

            if (!Main.drawToScreen)
            {

                List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
                List<CustomVertexInfo> bars_2 = new List<CustomVertexInfo>();

                List<int> indexer = new List<int>();
                List<Projectile> oculusTears = new List<Projectile>();
                List<Projectile> astralTears = new List<Projectile>();
                List<Projectile> solusKatanaFractal = new List<Projectile>();

                int timeLeft = -1919810;
                float? director = null;
                #region 遍历查找
                GetMyDataOut(bars, oculusTears, astralTears, solusKatanaFractal, bars_2, indexer, ref director, ref timeLeft);
                var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;

                #endregion
                if (bars.Count > 2 || oculusTears.Count > 0 || astralTears.Count > 0 || solusKatanaFractal.Count > 0)
                {
                    var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                    var resultMatrix = model * trans * projection;
                    #region 日炎刀合批
                    if (bars.Count > 2 || solusKatanaFractal.Count > 0)
                    {
                        SolarKatanaDrawing(solusKatanaFractal, indexer, graphicsDevice, render, renderAirDistort, spriteBatch, trans, resultMatrix, bars, bars_2, director, true);
                    }
                    #endregion
                    if (oculusTears.Count > 0)
                    {
                        OculusTearsDrawing(oculusTears, graphicsDevice, render, spriteBatch, trans);
                    }
                    if (astralTears.Count > 0)
                    {
                        AstralTearsDrawing(astralTears, graphicsDevice, render, spriteBatch, trans);
                    }

                }

                List<Projectile> windOfTimeProjs = new List<Projectile>();
                foreach (var proj in Main.projectile)
                {
                    if (proj.type == ProjectileType<WindOfTimeReactionProj>() && proj.active)
                    {
                        windOfTimeProjs.Add(proj);
                    }
                }
                #region MyRegion
                var sb = Main.spriteBatch;
                #region Render
                graphicsDevice.SetRenderTarget(Instance.Render_AirDistort);
                graphicsDevice.Clear(Color.Transparent);
                #endregion
                sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
                foreach (var wind in windOfTimeProjs)
                {
                    sb.Draw(MagicZone[14].Value, wind.Center - Main.screenPosition, null, Color.White * ((1 - MathF.Cos(MathHelper.TwoPi * wind.timeLeft / 180f)) * .5f), (float)ModTime / 60f, new Vector2(200), (1 - wind.timeLeft / 180f) * 4, 0, 0);
                }
                sb.End();
                #region render
                graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
                graphicsDevice.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                Main.graphics.GraphicsDevice.Textures[2] = Misc[18].Value;
                AirDistortEffect.Parameters["uScreenSize"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
                AirDistortEffect.Parameters["strength"].SetValue(.001f);
                AirDistortEffect.Parameters["rotation"].SetValue(0);
                AirDistortEffect.Parameters["tex0"].SetValue(Instance.Render_AirDistort);
                AirDistortEffect.CurrentTechnique.Passes[0].Apply();//ApplyPass
                sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                sb.End();
                graphicsDevice.SetRenderTarget(Main.screenTarget);
                graphicsDevice.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                sb.End();
                #endregion
                #endregion
            }

            if (VirtualDreamMod.bloomValue > 0)
                UseBloom(graphicsDevice);
            #region MyRegion
            //#region Render
            ////var gd = Main.graphics.GraphicsDevice;
            ////先在自己的render上画这个弹幕
            ////sb.End();
            //graphicsDevice.SetRenderTarget(render);
            //graphicsDevice.Clear(Color.Transparent);
            //#endregion
            //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);
            //Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            //Main.spriteBatch.End();
            //#region render
            ////然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
            ////原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
            //graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            //graphicsDevice.Clear(Color.Transparent);
            //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
            //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg");// Backgrounds/StarSky_0 Backgrounds/StarSkyv2  Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg
            //IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
            //IllusionBoundModSystem.Distort.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前graphicsDevice.SetRenderTaRGet的不是这个render，否则会报错）
            //                                                             //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
            //IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.35f);
            //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
            //IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.30f);
            //IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.LocalPlayer.Center + new Vector2(0.707f) * (float)IllusionBoundMod.ModTime * 8);
            //IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Red.ToVector4());//Color.Cyan.ToVector4()//default(Vector4)//Color.Cyan.ToVector4()//new Vector4(1, 0, 0.25f, 1)
            //                                                                                      //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
            //                                                                                      //Main.NewText("!!!");
            //IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(960, 560));//new Vector2(1280, 2758)//new Vector2(960,560)  64, 48

            //Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
            //Main.spriteBatch.End();

            ////最后在screenTarget上把刚刚的结果画上
            //graphicsDevice.SetRenderTarget(Main.screenTarget);
            //graphicsDevice.Clear(Color.Transparent);
            //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            //Main.spriteBatch.End();
            //#endregion
            #endregion

        }
        private static void GetMyDataOut(List<CustomVertexInfo> bars, List<Projectile> oculusTears, List<Projectile> astralTears, List<Projectile> solusKatanaFractal, List<CustomVertexInfo> bars_2, List<int> indexer, ref float? director, ref int timeLeft)
        {
            foreach (var proj in Main.projectile)
            {
                if (proj.active && proj.ModProjectile != null && proj.ModProjectile is SolusEnergyShard shard)
                {
                    var shardPlayer = Main.player[proj.owner];
                    foreach (var swoosh in shard.swooshes)
                    {
                        if (swoosh != null && swoosh.Active)
                        {
                            for (int i = 0; i < 45; i++)
                            {
                                var f = i / 44f;
                                var lerp = f.Lerp(1 - swoosh.timeLeft / 30f, 1);
                                float theta2 = (1.8375f * lerp - 1.125f) * MathHelper.Pi + MathHelper.Pi;
                                if (swoosh.direction == 1) theta2 = MathHelper.TwoPi - theta2;
                                var scaler = 50 * shardPlayer.GetAdjustedItemScale(shardPlayer.HeldItem) / (float)Math.Sqrt(swoosh.xScaler) * .5f;// (Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity).M11 * .5f
                                Vector2 newVec = -2 * (theta2.ToRotationVector2() * new Vector2(swoosh.xScaler, 1)).RotatedBy(swoosh.rotation) * scaler * (1 + (1 - swoosh.timeLeft / 30f));
                                var realColor = Color.Lerp(Color.White, Color.Orange, f);
                                realColor.A = (byte)((1 - f).HillFactor2(1) * swoosh.timeLeft / 30f * 255);
                                bars.Add(new CustomVertexInfo(swoosh.center + newVec, realColor, new Vector3(1 - f, 1, 0.6f)));
                                bars_2?.Add(new CustomVertexInfo(swoosh.center + newVec * 1.5f, realColor, new Vector3(1 - f, 1, 0.6f)));

                                realColor.A = 0;
                                bars.Add(new CustomVertexInfo(swoosh.center, realColor, new Vector3(0, 0, 0.6f)));
                                bars_2?.Add(new CustomVertexInfo(swoosh.center, realColor, new Vector3(0, 0, 0.6f)));

                            }
                            indexer?.Add(bars.Count - 2);

                            if (swoosh.timeLeft > timeLeft)
                            {
                                timeLeft = swoosh.timeLeft;
                                director = swoosh.direction;
                            }
                        }

                    }
                }
                if (proj.active && proj.type == ProjectileType<OculusReaverTear>()) oculusTears.Add(proj);
                if (proj.active && proj.type == ProjectileType<AstralTear>() && (int)proj.ai[1] != 0) astralTears.Add(proj);
                if (proj.active && (proj.type == ProjectileType<SolusKatanaFractal>() || proj.type == ProjectileType<SolusLevatine>())) solusKatanaFractal.Add(proj);
            }
        }
        private static void SolarKatanaDrawing(List<Projectile> solusKatanaFractal, List<int> indexer, GraphicsDevice graphicsDevice, RenderTarget2D render, RenderTarget2D renderAirDistort, SpriteBatch spriteBatch, Matrix trans, Matrix resultMatrix, List<CustomVertexInfo> bars, List<CustomVertexInfo> bars_2, float? director, bool useRender)
        {

            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

            SamplerState sampler = SamplerState.LinearWrap;
            //
            if (solusKatanaFractal.Count > 0)
            {
                foreach (var projectile in solusKatanaFractal)
                {
                    if (projectile.ModProjectile is SolusKatanaFractal solusKatana) solusKatana.DrawOthers();
                    var max = projectile.oldPos.Length - 1;
                    for (int n = 0; n < projectile.oldPos.Length; n++)
                    {
                        if (projectile.oldPos[n] == default) { max = n; break; }
                    }
                    var min = 0;
                    var currentVec = projectile.oldPos[0];
                    for (int n = 1; n < projectile.oldPos.Length; n++)
                    {
                        if (projectile.oldPos[n] != currentVec) { min = n - 1; break; }
                        currentVec = projectile.oldPos[n];
                    }
                    if (max - min < 2) { /*Main.NewText("太短了太短了！！  " + max + "   " + projectile.localAI[0] + "   " + projectile.oldPos[0]);*/ continue; }
                    bool isLevatine = projectile.type == ProjectileType<SolusLevatine>();
                    float _scaler = isLevatine ? 1600 : 98f;
                    var multiValue = isLevatine ? MathHelper.Clamp((210f - projectile.timeLeft).HillFactor2(210) * 2, 0, 1) : 1;//1 - (projectile.localAI[0] - 60) / 90f
                    bars.Add(new CustomVertexInfo(projectile.oldPos[0] + projectile.oldRot[0].ToRotationVector2() * _scaler, default(Color), new Vector3(1, 1, 0.6f)));
                    bars_2?.Add(bars[^1] with { Position = projectile.oldPos[0] + projectile.oldRot[0].ToRotationVector2() * _scaler * 1.5f });
                    bars.Add(new CustomVertexInfo(projectile.oldPos[0], default(Color), new Vector3(0, 0, 0.6f)));
                    bars_2?.Add(bars[^1]);
                    for (int i = min; i < max; i++)
                    {
                        var f = (i - min) / (max - min - 1f);
                        f = 1 - f;
                        var alphaLight = 0.6f;
                        var realColor = Color.Lerp(Color.White, Color.Orange, f);
                        //var _f = 6 * f / (3 * f + 1);//6 * f / (3 * f + 1) /(float)Math.Pow(f,instance.maxCount)
                        //_f = MathHelper.Clamp(_f, 0, 1);
                        realColor.A = (byte)((1 - f).HillFactor2(1) * 255);
                        //realColor.A = 51;
                        bars.Add(new CustomVertexInfo(projectile.oldPos[i] + projectile.oldRot[i].ToRotationVector2() * _scaler, realColor * multiValue, new Vector3(1 - f, 1, alphaLight)));
                        bars_2?.Add(bars[^1] with { Position = projectile.oldPos[i] + projectile.oldRot[i].ToRotationVector2() * _scaler * 1.5f });

                        realColor.A = isLevatine ? realColor.A : (byte)0;
                        bars.Add(new CustomVertexInfo(projectile.oldPos[i], realColor * multiValue, new Vector3(0, 0, alphaLight)));
                        bars_2?.Add(bars[^1]);

                    }
                    indexer.Add(bars.Count - 2);
                }
            }
            if (bars.Count > 2)
            {

                CustomVertexInfo[] triangleList = new CustomVertexInfo[(bars.Count - 2) * 3];
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    if (indexer.ToArray().Contains(i)) continue;
                    var k = i / 2;
                    if (6 * k < triangleList.Length)
                    {
                        triangleList[6 * k] = bars[i];
                        triangleList[6 * k + 1] = bars[i + 2];
                        triangleList[6 * k + 2] = bars[i + 1];
                    }
                    if (6 * k + 3 < triangleList.Length)
                    {
                        triangleList[6 * k + 3] = bars[i + 1];
                        triangleList[6 * k + 4] = bars[i + 2];
                        triangleList[6 * k + 5] = bars[i + 3];
                    }
                }
                if (useRender)
                {
                    graphicsDevice.SetRenderTarget(render);
                    graphicsDevice.Clear(Color.Transparent);
                }

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
                ShaderSwooshEX.Parameters["uTransform"].SetValue(resultMatrix);
                ShaderSwooshEX.Parameters["uLighter"].SetValue(0);
                ShaderSwooshEX.Parameters["uTime"].SetValue(-(float)VirtualDreamSystem.ModTime * 0.03f);//-(float)Main.time * 0.06f
                ShaderSwooshEX.Parameters["checkAir"].SetValue(false);
                ShaderSwooshEX.Parameters["airFactor"].SetValue(1);
                ShaderSwooshEX.Parameters["gather"].SetValue(true);
                ShaderSwooshEX.Parameters["distortScaler"].SetValue(0);
                ShaderSwooshEX.Parameters["lightShift"].SetValue(0f);

                Main.graphics.GraphicsDevice.Textures[0] = BaseTex[5].Value;
                Main.graphics.GraphicsDevice.Textures[1] = AniTex[3].Value;
                Main.graphics.GraphicsDevice.Textures[2] = TextureAssets.Item[ItemType<SolusKatana>()].Value;
                Main.graphics.GraphicsDevice.Textures[3] = HeatMap[27].Value;

                Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
                Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
                Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
                Main.graphics.GraphicsDevice.SamplerStates[3] = SamplerState.AnisotropicClamp;

                ShaderSwooshEX.CurrentTechnique.Passes[2].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0, bars.Count - 2);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                if (useRender)
                {
                    graphicsDevice.SetRenderTarget(renderAirDistort);
                    graphicsDevice.Clear(Color.Transparent);
                    ShaderSwooshEX.Parameters["distortScaler"].SetValue(1.5f);
                    ShaderSwooshEX.CurrentTechnique.Passes[2].Apply();
                    for (int i = 0; i < bars_2.Count - 2; i += 2)
                    {
                        if (indexer.ToArray().Contains(i)) continue;
                        var k = i / 2;
                        if (6 * k < triangleList.Length)
                        {
                            triangleList[6 * k] = bars_2[i];
                            triangleList[6 * k + 1] = bars_2[i + 2];
                            triangleList[6 * k + 2] = bars_2[i + 1];
                        }
                        if (6 * k + 3 < triangleList.Length)
                        {
                            triangleList[6 * k + 3] = bars_2[i + 1];
                            triangleList[6 * k + 4] = bars_2[i + 2];
                            triangleList[6 * k + 5] = bars_2[i + 3];
                        }
                    }
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0, bars.Count - 2);


                    spriteBatch.End();
                    graphicsDevice.SetRenderTarget(Main.screenTargetSwap);//将画布设置为这个
                    graphicsDevice.Clear(Color.Transparent);//清空
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix



                    //Vector2 direct = (instance.swooshFactorStyle == SwooshFactorStyle.每次开始时决定系数 ? modPlayer.kValue : ((modPlayer.kValue + modPlayer.kValueNext) * .5f)).ToRotationVector2() * -0.1f * fac.SymmetricalFactor2(0.5f, 0.2f) * instance.distortFactor;//(u + v)
                    //RenderEffect.Parameters["offset"].SetValue((director ?? MathHelper.PiOver4).ToRotationVector2() * -0.03f);//设置参数时间
                    //RenderEffect.Parameters["invAlpha"].SetValue(0);
                    //RenderEffect.Parameters["tex0"].SetValue(renderAirDistort);
                    //RenderEffect.CurrentTechnique.Passes[0].Apply();//ApplyPass
                    Main.instance.GraphicsDevice.Textures[2] = Misc[18].Value;
                    AirDistortEffect.Parameters["uScreenSize"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
                    AirDistortEffect.Parameters["strength"].SetValue(.0005f);
                    AirDistortEffect.Parameters["rotation"].SetValue(0f);
                    AirDistortEffect.Parameters["tex0"].SetValue(renderAirDistort);
                    AirDistortEffect.CurrentTechnique.Passes[0].Apply();//ApplyPass


                    spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);//绘制原先屏幕内容
                    graphicsDevice.SetRenderTarget(Main.screenTarget);
                    graphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);

                    RenderEffect.Parameters["offset"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));

                    RenderEffect.Parameters["position"].SetValue(new Vector2(0f, 6f));
                    RenderEffect.Parameters["tier2"].SetValue(0.2f);
                    RenderEffect.Parameters["invAlpha"].SetValue(1f);

                    for (int n = 0; n < 2; n++)
                    {
                        graphicsDevice.SetRenderTarget(renderAirDistort);
                        RenderEffect.Parameters["tex0"].SetValue(render);
                        graphicsDevice.Clear(Color.Transparent);
                        RenderEffect.CurrentTechnique.Passes[9].Apply();
                        spriteBatch.Draw(render, Vector2.Zero, Color.White);



                        graphicsDevice.SetRenderTarget(render);
                        RenderEffect.Parameters["tex0"].SetValue(renderAirDistort);
                        graphicsDevice.Clear(Color.Transparent);
                        RenderEffect.CurrentTechnique.Passes[8].Apply();
                        spriteBatch.Draw(renderAirDistort, Vector2.Zero, Color.White);
                    }
                    //Distort.Parameters["position"].SetValue(new Vector2(0, 5f));
                    //Distort.Parameters["ImageSize"].SetValue(new Vector2(0.707f) * -0.006f);//projectile.rotation.ToRotationVector2() * -0.006f


                    //for (int n = 0; n < 1; n++)
                    //{
                    //graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
                    //graphicsDevice.Clear(Color.Transparent);
                    //Distort.CurrentTechnique.Passes[5].Apply();
                    //spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);

                    //graphicsDevice.SetRenderTarget(Main.screenTarget);
                    //graphicsDevice.Clear(Color.Transparent);
                    //Distort.CurrentTechnique.Passes[4].Apply();
                    //spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                    //}
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    graphicsDevice.SetRenderTarget(Main.screenTarget);
                    spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                    spriteBatch.Draw(render, Vector2.Zero, Color.White);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
                if (solusKatanaFractal.Count > 0)
                {
                    foreach (var projectile in solusKatanaFractal)
                    {
                        (projectile.ModProjectile as SolusKatanaFractal)?.DrawSword();
                        (projectile.ModProjectile as SolusLevatine)?.DrawLaser();

                    }
                }
                spriteBatch.End();
            }
        }
        private static void OculusTearsDrawing(List<Projectile> oculusTears, GraphicsDevice graphicsDevice, RenderTarget2D render, SpriteBatch spriteBatch, Matrix trans)
        {
            #region Render
            //var gd = Main.graphics.GraphicsDevice;
            //先在自己的render上画这个弹幕
            //sb.End();
            graphicsDevice.SetRenderTarget(render);
            graphicsDevice.Clear(Color.Transparent);
            #endregion
            //sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
            OculusTearsDrawingContent(oculusTears, spriteBatch, Color.White);
            spriteBatch.End();
            #region render
            //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
            //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
            graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            graphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
            Main.graphics.GraphicsDevice.Textures[1] = VirtualDreamMod.GetTexture("Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg");// Backgrounds/StarSky_0 Backgrounds/StarSkyv2  Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg
            RenderEffect.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前graphicsDevice.SetRenderTarget的不是这个render，否则会报错）
                                                             //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
            RenderEffect.Parameters["invAlpha"].SetValue(0.35f);
            RenderEffect.Parameters["lightAsAlpha"].SetValue(true);
            RenderEffect.Parameters["tier2"].SetValue(0.30f);
            RenderEffect.Parameters["position"].SetValue(Main.LocalPlayer.Center + new Vector2(0.707f) * (float)ModTime * 8);
            RenderEffect.Parameters["maskGlowColor"].SetValue(new Vector4(1, 0, 0.25f, 1));//Color.Cyan.ToVector4()//default(Vector4)//Color.Cyan.ToVector4()//new Vector4(1, 0, 0.25f, 1)
                                                                                           //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
                                                                                           //Main.NewText("!!!");
            RenderEffect.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)  64, 48
            RenderEffect.Parameters["inverse"].SetValue(false);

            RenderEffect.CurrentTechnique.Passes[1].Apply();

            spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")

            spriteBatch.End();

            //最后在screenTarget上把刚刚的结果画上
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            spriteBatch.End();
            #endregion
        }
        private static void AstralTearsDrawing(List<Projectile> astralTears, GraphicsDevice graphicsDevice, RenderTarget2D render, SpriteBatch spriteBatch, Matrix trans)
        {
            var sb = Main.spriteBatch;
            #region Render
            graphicsDevice.SetRenderTarget(render);
            graphicsDevice.Clear(Color.Transparent);
            #endregion
            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, trans);
            AstralTearsDrawingContent(astralTears, spriteBatch, true, trans);
            sb.End();
            #region render
            graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            graphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Main.graphics.GraphicsDevice.Textures[1] = VirtualDreamMod.GetTexture("Backgrounds/StarSkyv3");
            RenderEffect.Parameters["tex0"].SetValue(render);
            RenderEffect.Parameters["invAlpha"].SetValue(0.35f);
            RenderEffect.Parameters["lightAsAlpha"].SetValue(true);
            RenderEffect.Parameters["tier2"].SetValue(0.30f);
            RenderEffect.Parameters["position"].SetValue(Main.LocalPlayer.Center + new Vector2(0.707f) * (float)ModTime * 8);
            RenderEffect.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());
            RenderEffect.Parameters["ImageSize"].SetValue(new Vector2(64, 48));
            RenderEffect.Parameters["inverse"].SetValue(false);

            RenderEffect.CurrentTechnique.Passes[1].Apply();
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();
            #endregion
        }
        private void UseBloom(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //取样
            graphicsDevice.SetRenderTarget(Instance.Render);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            VirtualDreamMod.Bloom.CurrentTechnique.Passes[0].Apply();//取亮度超过m值的部分

            VirtualDreamMod.Bloom.Parameters["m"].SetValue(0.6f);

            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //处理
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            VirtualDreamMod.Bloom.Parameters["uScreenResolution"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
            VirtualDreamMod.Bloom.Parameters["uRange"].SetValue(2.5f);
            VirtualDreamMod.Bloom.Parameters["uIntensity"].SetValue(VirtualDreamMod.bloomValue);
            for (int i = 0; i < 3; i++)//交替使用两个RenderTarget2D，进行多次模糊
            {
                VirtualDreamMod.Bloom.CurrentTechnique.Passes["GlurV"].Apply();//横向
                graphicsDevice.SetRenderTarget(Main.screenTarget);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(Instance.Render, Vector2.Zero, Color.White);

                VirtualDreamMod.Bloom.CurrentTechnique.Passes["GlurH"].Apply();//纵向
                graphicsDevice.SetRenderTarget(Instance.Render);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            }
            Main.spriteBatch.End();

            //叠加到原图上
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);//Additive把模糊后的部分加到Main.screenTarget里
            Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            Main.spriteBatch.Draw(Instance.Render, Vector2.Zero, Color.White);
            Main.spriteBatch.End();
            VirtualDreamMod.bloomValue = 0;
        }
    }
}
