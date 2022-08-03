/*
 * IllusionBoundMod 
 * IllusionBoundMod.cs
 * ���ߣ�AW
 * 
 * ����༭�޸�
 */
global using Microsoft.Xna.Framework;

global using Terraria;
global using Terraria.Audio;
global using Terraria.GameContent;
global using Terraria.ModLoader;

global using VirtualDream.Utils;
global using VirtualDream.Utils.BaseClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using VirtualDream.Tiles.StormZone;
//using VirtualDream.NPCs.StormZone;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

using Terraria.GameContent.UI.Elements;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
//using VirtualDream.UI.Spectre;
//using VirtualDream.UI;
using Terraria.UI;
using VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana;
using VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.OculusReaver;
using VirtualDream.Effects;

using static Terraria.ModLoader.ModContent;

//using static VirtualDream.IllusionBoundOnIlFunctions;
// ������б�ܿ�ͷ�ľ��Ӷ���ע��QAQ���Գ�������û���κ�Ӱ�죬���Ҿ����ˣ�����ɾ

// ���϶�����Ҫʹ�õĳ��򼯣���Ҫ�Ҷ�����Ȼ����������

// ���������ռ䣬�������MOD������IllusionBoundMod
namespace VirtualDream
{

    //public abstract class IllusionBoundTree : ModTree 
    //{
    //    public abstract Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset,int y);
    //    public abstract Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame,int y);
    //}
    // MOD���������֣���Ҫ���ļ�����MOD����ȫһ�£����Ҽ̳�Mod��
    public class IllusionBoundMod : Mod
    {
        public static float GlowLight => IllusionBoundModSystem.glowLight;
        public static double ModTime => IllusionBoundModSystem.ModTime;
        public static double ModTime2 => IllusionBoundModSystem.ModTime2;

        public static IllusionBoundMod Instance;
        public static Mod mod;
        public static float lightConst;
        public static bool HarderActive
        {
            get
            {
                try
                {
                    //Bug 0
                    if (Main.LocalPlayer == null || !Main.LocalPlayer.TryGetModPlayer<Contents.InfiniteNightmare.InfiniteNightmarePlayer>(out var result))
                    {
                        return false;
                    }

                    return result.ReallyInfiniteNightmareModeActive || IHarderActive;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IHarderActive
        {
            get
            {
                try
                {
                    //Bug 0-1
                    if (Main.LocalPlayer == null || !Main.LocalPlayer.TryGetModPlayer<Contents.InfiniteNightmare.InfiniteNightmarePlayer>(out var result))
                    {
                        return false;
                    }

                    return result.InfiniteNightmareModeActive;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool UnderGroundActive;
        //public static StormRain[] rain = new StormRain[750];
        //public static int[] StormNPCType = new int[] { ModContent.NPCType<NegativeElectricalSlime>(), ModContent.NPCType<PositiveElectricalSlime>(), ModContent.NPCType<NeutralSlime>(), ModContent.NPCType<ElectricalHarpy>() };
        public static ElectricTriangle[] electricTriangle = new ElectricTriangle[100];
        private int iconFrame = 0;
        private byte iconFrameCounter = 0;

        private Texture2D[] icon = new Texture2D[22];
        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            //��������Ϊ��ȡMain.MenuUI��UIState��
            FieldInfo uiStateField = Main.MenuUI.GetType().GetField("_history", BindingFlags.NonPublic | BindingFlags.Instance);
            List<UIState> _history = (List<UIState>)uiStateField.GetValue(Main.MenuUI);
            //ʹ��for����UIState����Ѱ��UIMods���ʵ��
            for (int x = 0; x < _history.Count; x++)
            {
                //��⵱ǰUIState������ȫ���Ƿ���ModLoader��UIMods
                if (_history[x].GetType().FullName == "Terraria.ModLoader.UI.UIMods")
                {
                    //��������Ϊ��ȡUIMods��UI������
                    FieldInfo elementsField = _history[x].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> elements = (List<UIElement>)elementsField.GetValue(_history[x]);

                    //��֮ǰ �˽�ģ��ѡ��ҳ��Ĺ��� һ�ڿ�֪�������� ����UIList������UIPanel ��UIElement��һ����UIMods�������ʴ�UIElementλ��UIMods�Ĳ�������0��������
                    //�����������ڻ�ȡUIElement��UI������
                    FieldInfo uiElementsField = elements[0].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> uiElements = (List<UIElement>)uiElementsField.GetValue(elements[0]);

                    //ͬ���� �˽�ģ��ѡ��ҳ��Ĺ��� һ�ڿ�֪��UIPanel��һ����UIElements��������UIPanelλ��UIElement��UI��������0��������
                    //�����������ڻ�ȡUIPanel��UI������
                    FieldInfo myModUIPanelField = uiElements[0].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> myModUIPanel = myModUIPanelField.GetValue(uiElements[0]) as List<UIElement>;

                    //ͬ���� �˽�ģ��ѡ��ҳ��Ĺ��� һ�ڿ�֪��UIList��һ����UIPanel��������UIListλ��UIPanel��UI��������0��������
                    UIList uiList = (UIList)myModUIPanel[0];
                    //����uiList�������Ӳ�����Ѱ������mod��UIModItem����
                    for (int i = 0; i < uiList._items.Count; i++)
                    {
                        //��̬Icon��bug��
                        //�����ȡmodʵ����������Ƿ������ǵ�mod
                        if (uiList._items[i].GetType().GetField("_mod", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(uiList._items[i]).ToString() == Name)
                        {
                            //��������Ϊ��ȡ����mod��UIModItem��UI������
                            FieldInfo myUIModItemField = uiList._items[i].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                            List<UIElement> myUIModItem = (List<UIElement>)myUIModItemField.GetValue(uiList._items[i]);

                            float _modIconAdjust = (GetTexture("icon") == null ? 0 : 85);
                            UIElement badUnloader = myUIModItem.Find((UIElement e) => e.ToString() == "Terraria.ModLoader.UI.UIHoverImage" && e.Top.Pixels == 3);
                            //����UIModItem��UI������
                            for (int j = 0; j < myUIModItem.Count; j++)
                            {
                                //�����ǰUI������UIImage�������߾�Ϊ80
                                if (myUIModItem[j] is UIImage && myUIModItem[j].Width.Pixels == 80 && myUIModItem[j].Height.Pixels == 80)
                                {
                                    //�޸Ĵ�UI��������ͼ
                                    (myUIModItem[j] as UIImage).SetImage(icon[iconFrame]);
                                    //�˳�ѭ��
                                    break;
                                }
                            }
                            //���������һSetValue
                            myUIModItemField.SetValue(uiList._items[i], myUIModItem);
                            myModUIPanel[0] = uiList;
                            myModUIPanelField.SetValue(uiElements[0], myModUIPanel);
                            uiElementsField.SetValue(elements[0], uiElements);
                            elementsField.SetValue(_history[x], elements);
                            uiStateField.SetValue(Main.MenuUI, _history);
                            //�˳�ѭ��
                            break;
                        }
                    }
                    //�˳�ѭ��
                    break;
                }
            }
            iconFrameCounter++;
            if (iconFrameCounter >= 6)
            {
                iconFrame++;
                iconFrame %= 22;
                iconFrameCounter = 0;
            }
            orig(self, gameTime);
        }
        //public static float StormTileGrowLight 
        //{
        //	get 
        //	{
        //		return ((float)Math.Sin(ModTime * MathHelper.TwoPi / 120) + 1) / 2;
        //	}
        //}
        public static bool TestFlag = true;

        //public Texture2D originTex;
        public static Effect GetEffect(string path, bool autoModName = true) => Request<Effect>((autoModName ? "VirtualDream/" : "") + path, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D GetTexture(string path, bool autoModName = true) => Request<Texture2D>((autoModName ? "VirtualDream/" : "") + path, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

        public override void Load()
        {
            //IllusionBoundExtensionMethods.ShaderItemEffectInWorld
            //��exampleUIʵ����
            //vertexUI = new Items.Others.StrawBerryLinerUI();
            ////��exampleUI��ʼ��
            //vertexUI.Activate();
            ////��exampleUserInterfaceʵ����
            //vertexUserInterface = new UserInterface();
            ////��exampleUserInterface����exampleUI���¼�����
            //vertexUserInterface.SetState(vertexUI);

            //var modtrees = typeof(TileLoader).GetField("trees", BindingFlags.Static | BindingFlags.NonPublic);
            //trees = modtrees.GetValue(null) as IDictionary<int, ModTree>;
            mod = this;
            Instance = this;
            //grayEffect = GetEffect("Effects/GrayScale");
            //GameShaders.Armor.BindShader(ModContent.ItemType<Items.Dyes.ContrastChangeDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/ContrastChangeGlassFA")), "ContrastChange"));
            var effect = GetEffect("Effects/IllusionBoundScreenGlass");
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                Filters.Scene["VirtualDream:" + pass.Name] = new Filter(new IllusionScreenShaderData(new Ref<Effect>(effect), pass.Name), EffectPriority.Medium);
                Filters.Scene["VirtualDream:" + pass.Name].Load();
            }
            //Filters.Scene["IllusionBoundMod:MagnifyingGlass"] = new Filter(new TestScreenShaderData(new Ref<Effect>(GetEffect("Effects/MagnifyingGlass")), "Test"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:MagnifyingGlass"].Load();
            //Filters.Scene["IllusionBoundMod:MagicalMagnifyingGlass"] = new Filter(new IllusionScreenShaderData(new Ref<Effect>(GetEffect("Effects/MagicalMagnifyingGlass")), "Magical"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:MagicalMagnifyingGlass"].Load();
            //Filters.Scene["IllusionBoundMod:CleverGlass"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/CleverGlass")), "Clever"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:CleverGlass"].Load();
            //Filters.Scene["IllusionBoundMod:RainbowGlass"] = new Filter(new RainbowScreenShaderData(new Ref<Effect>(GetEffect("Effects/RainbowGlass")), "Rainbow"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:RainbowGlass"].Load();
            //Filters.Scene["IllusionBoundMod:ContrastGlass"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/ContrastGlass")), "Contrast"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:ContrastGlass"].Load();
            //Filters.Scene["IllusionBoundMod:ContrastGlassV2"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/ContrastUpGlassLevel2")), "ContrastDown"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:ContrastGlassV2"].Load();
            //Filters.Scene["IllusionBoundMod:ShikieikiGlass"] = new Filter(new ShikieikiScreenShaderData(new Ref<Effect>(GetEffect("Effects/ShikieikiGlass")), "Shikieiki"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:ShikieikiGlass"].Load();
            //Filters.Scene["IllusionBoundMod:InversPhaseGlass"] = new Filter(new InversPhaseScreenShaderData(new Ref<Effect>(GetEffect("Effects/InversPhaseGlass")), "InversPhase"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:InversPhaseGlass"].Load();
            //Filters.Scene["IllusionBoundMod:ZenithGlass"] = new Filter(new ZenithScreenShaderData(new Ref<Effect>(GetEffect("Effects/ZenithGlass")), "Zenith"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:ZenithGlass"].Load();
            //Filters.Scene["IllusionBoundMod:ContrastDownGlass"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/ContrastDownGlass")), "ContrastDown"), EffectPriority.Medium);
            //Filters.Scene["IllusionBoundMod:ContrastDownGlass"].Load();
            //Filters.Scene["IllusionBoundMod:HeatDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(4f), EffectPriority.Low);
            //Filters.Scene["IllusionBoundMod:HeatDistortion"].Load();
            //Filters.Scene["IllusionBoundMod:HeatDistortion_"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/DesertEffect")), "Desert").UseImage("Images/Misc/noise", 0, null).UseIntensity(4f), EffectPriority.Low);
            //Filters.Scene["IllusionBoundMod:HeatDistortion_"].Load();

            //SkyManager.Instance["VirtualDream:Storm"] = new StormSky();
            //SkyManager.Instance["VirtualDream:BigApe"] = new BigApeSky();
            //SkyManager.Instance["VirtualDream:ErchiusHorror"] = new ErchiusHorrorSky();


            //AddEquipTexture(null, EquipType.Legs, "JunkoRobe_Legs", "IllusionBoundMod/Items/TouhouProject/Junko/JunkoRobe_Legs");
            ////On.Terraria.NPC.UpdateNPC += TimeStopUpdate;
            //On.Terraria.Main.DrawTiles += DrawTilesS;
            //On.Terraria.NPC.Collision_MoveNormal += CollisionMove;
            ////On.Terraria.NPC.UpdateNPC += UPDNPC;
            //IL.Terraria.Player.beeType += MyBeeType;
            //On.Terraria.Utilities.UnifiedRandom.Next_int_int += UnifiedRandom_Next_int_int;
            //IL.Terraria.Main.DrawTiles += _DrawTreeHook;
            On.Terraria.Graphics.Effects.FilterManager.EndCapture += FilterManager_EndCapture_IllusionBound;
            On.Terraria.Main.DrawProjectiles += Main_DrawProjectiles_VirtualDream;
            Main.OnResolutionChanged += Main_OnResolutionChanged;
            Main.RunOnMainThread(CreateRender);
            //originSun = Main.sunTexture;
            //Main.sunTexture = IllusionBoundMod.GetTexture("Sun");

            //originTex = Main.inventoryBack10Texture;
            //Main.inventoryBack10Texture = IllusionBoundMod.GetTexture("Inventory_Back10");

            for (int i = 0; i < icon.Length; i++)
            {
                icon[i] = GetTexture($"icons/icon_ani_{i}");
            }
            On.Terraria.Main.DrawMenu += Main_DrawMenu;
            //On.Terraria.Main.DrawPlayer += WeaponDisplayDrawPlayer;
        }

        private void Main_DrawProjectiles_VirtualDream(On.Terraria.Main.orig_DrawProjectiles orig, Main self)
        {
            orig.Invoke(self);
            if (!(Lighting.Mode == Terraria.Graphics.Light.LightMode.Retro || Lighting.Mode == Terraria.Graphics.Light.LightMode.Trippy)) return;
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            List<int> indexer = new List<int>();
            Player player = null;
            List<Projectile> oculusTears = new List<Projectile>();
            SpriteBatch spriteBatch = Main.spriteBatch;
            #region ��������
            foreach (var proj in Main.projectile)
            {
                if (proj.active && proj.ModProjectile != null && proj.ModProjectile is SolusEnergyShard shard)
                {
                    foreach (var swoosh in shard.swooshes)
                    {
                        if (swoosh != null && swoosh.Active)
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                var f = i / 24f;
                                var lerp = f.Lerp(1 - swoosh.timeLeft / 30f, 1);
                                float theta2 = (1.8375f * lerp - 1.125f) * MathHelper.Pi + MathHelper.Pi;
                                if (swoosh.direction == 1) theta2 = MathHelper.TwoPi - theta2;
                                var scaler = 50 * shard.Player.GetAdjustedItemScale(shard.Player.HeldItem) / (float)Math.Sqrt(swoosh.xScaler) * (Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity).M11 * .5f;
                                Vector2 newVec = -2 * (theta2.ToRotationVector2() * new Vector2(swoosh.xScaler, 1)).RotatedBy(swoosh.rotation) * scaler * (1 + (1 - swoosh.timeLeft / 30f));
                                var realColor = Color.Lerp(Color.White, Color.Orange, f);
                                realColor.A = (byte)((1 - f).HillFactor2(1) * swoosh.timeLeft / 30f * 255);
                                bars.Add(new CustomVertexInfo(swoosh.center + newVec, realColor, new Vector3(1 - f, 1, 0.6f)));
                                realColor.A = 0;
                                bars.Add(new CustomVertexInfo(swoosh.center, realColor, new Vector3(0, 0, 0.6f)));
                            }
                            indexer.Add(bars.Count - 2);
                            player = shard.Player;
                        }
                    }
                }
                if (proj.active && proj.type == ProjectileType<OculusReaverTear>()) oculusTears.Add(proj);
            }
            #endregion
            #region ���׵�����
            if (bars.Count > 2)
            {
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;

                SamplerState sampler = SamplerState.LinearClamp;
                CustomVertexInfo[] triangleList = new CustomVertexInfo[(bars.Count - 2) * 3];//
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    if (indexer.ToArray().ContainsValue(i)) continue;
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
                //GraphicsDevice gd = Main.instance.GraphicsDevice;
                //RenderTarget2D render = IllusionBoundMod.Instance.render;
                //SpriteBatch spriteBatch = Main.spriteBatch;
                //spriteBatch.End();
                //gd.SetRenderTarget(render);
                //gd.Clear(Color.Transparent);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans * 2);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
                IllusionBoundMod.ShaderSwooshEX.Parameters["uTransform"].SetValue(model * projection * trans);
                IllusionBoundMod.ShaderSwooshEX.Parameters["uLighter"].SetValue(0);
                IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(0);//-(float)Main.time * 0.06f
                IllusionBoundMod.ShaderSwooshEX.Parameters["checkAir"].SetValue(true);
                IllusionBoundMod.ShaderSwooshEX.Parameters["airFactor"].SetValue(1);
                IllusionBoundMod.ShaderSwooshEX.Parameters["gather"].SetValue(true);
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/BaseTex_7");
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/AniTex");
                Main.graphics.GraphicsDevice.Textures[2] = TextureAssets.Item[player.HeldItem.type].Value;
                Main.graphics.GraphicsDevice.Textures[3] = IllusionBoundMod.HeatMap[24];

                Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
                Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
                Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
                Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;

                IllusionBoundMod.ShaderSwooshEX.CurrentTechnique.Passes[2].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0, bars.Count - 2);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans * 2);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix

                //IllusionBoundMod.Distort.Parameters["offset"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
                //IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);

                //IllusionBoundMod.Distort.Parameters["position"].SetValue(new Vector2(0, 2.5f));
                //IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.05f);
                //for (int n = 0; n < 1; n++)
                //{
                //    gd.SetRenderTarget(Main.screenTargetSwap);
                //    gd.Clear(Color.Transparent);
                //    IllusionBoundMod.Distort.CurrentTechnique.Passes[7].Apply();
                //    spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);



                //    gd.SetRenderTarget(Main.screenTarget);
                //    gd.Clear(Color.Transparent);
                //    IllusionBoundMod.Distort.CurrentTechnique.Passes[6].Apply();
                //    spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                //}
                //IllusionBoundMod.Distort.Parameters["position"].SetValue(new Vector2(0, 2.5f));
                //IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(0.707f));//projectile.rotation.ToRotationVector2() * -0.006f
                //for (int n = 0; n < 1; n++)
                //{
                //    gd.SetRenderTarget(Main.screenTargetSwap);
                //    gd.Clear(Color.Transparent);
                //    IllusionBoundMod.Distort.CurrentTechnique.Passes[5].Apply();
                //    spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);

                //    gd.SetRenderTarget(Main.screenTarget);
                //    gd.Clear(Color.Transparent);
                //    IllusionBoundMod.Distort.CurrentTechnique.Passes[4].Apply();
                //    spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                //}

                //spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                //spriteBatch.Draw(render, Vector2.Zero, Color.White);
                //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans * 2);
            }
            #endregion

            if (oculusTears.Count > 0)
            {
                var sb = Main.spriteBatch;
                //�����Լ���render�ϻ������Ļ
                sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
                foreach (var projectile in oculusTears)
                {
                    var fac = projectile.ai[0].SymmetricalFactor(90, 10) * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f);
                    //var fac = projectile.ai[0].HillFactor2(180);
                    IllusionBoundMod.TransformEffect.Parameters["factor1"].SetValue(fac);
                    IllusionBoundMod.TransformEffect.Parameters["factor2"].SetValue((float)ModTime / 30f);
                    IllusionBoundMod.TransformEffect.CurrentTechnique.Passes[0].Apply();
                    sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, new Color(1, 0, 0.25f), projectile.rotation, new Vector2(512), ((int)projectile.ai[1] == 3 ? 2.5f : 2f) * 46 / 512, 0, 0);//new Rectangle(240,240,92,92)
                }
                sb.End();

            }
        }

        private void FilterManager_EndCapture_IllusionBound(On.Terraria.Graphics.Effects.FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            GraphicsDevice gd = Main.instance.GraphicsDevice;
            if ((Lighting.Mode == Terraria.Graphics.Light.LightMode.White || Lighting.Mode == Terraria.Graphics.Light.LightMode.Color))
            {
                if (!Main.drawToScreen)
                {
                    List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
                    List<int> indexer = new List<int>();
                    Player player = null;
                    List<Projectile> oculusTears = new List<Projectile>();

                    #region ��������
                    foreach (var proj in Main.projectile)
                    {
                        if (proj.active && proj.ModProjectile != null && proj.ModProjectile is SolusEnergyShard shard)
                        {

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
                                        var scaler = 50 * shard.Player.GetAdjustedItemScale(shard.Player.HeldItem) / (float)Math.Sqrt(swoosh.xScaler) * (Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity).M11 * .5f;
                                        Vector2 newVec = -2 * (theta2.ToRotationVector2() * new Vector2(swoosh.xScaler, 1)).RotatedBy(swoosh.rotation) * scaler * (1 + (1 - swoosh.timeLeft / 30f));
                                        var realColor = Color.Lerp(Color.White, Color.Orange, f);
                                        realColor.A = (byte)((1 - f).HillFactor2(1) * swoosh.timeLeft / 30f * 255);
                                        bars.Add(new CustomVertexInfo(swoosh.center + newVec, realColor, new Vector3(1 - f, 1, 0.6f)));
                                        realColor.A = 0;
                                        bars.Add(new CustomVertexInfo(swoosh.center, realColor, new Vector3(0, 0, 0.6f)));
                                    }
                                    indexer.Add(bars.Count - 2);
                                    player = shard.Player;
                                }
                            }
                        }
                        if (proj.active && proj.type == ProjectileType<OculusReaverTear>()) oculusTears.Add(proj);
                    }
                    #endregion
                    if (bars.Count > 2 || oculusTears.Count > 0)
                    {
                        SpriteBatch spriteBatch = Main.spriteBatch;
                        var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                        var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                        var trans = Main.GameViewMatrix != null ? Main.GameViewMatrix.TransformationMatrix : Matrix.Identity;
                        var resultMatrix = model * projection * trans;
                        #region ���׵�����
                        if (bars.Count > 2)
                        {

                            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                            SamplerState sampler = SamplerState.LinearClamp;
                            CustomVertexInfo[] triangleList = new CustomVertexInfo[(bars.Count - 2) * 3];//
                            for (int i = 0; i < bars.Count - 2; i += 2)
                            {
                                if (indexer.ToArray().ContainsValue(i)) continue;
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
                            //GraphicsDevice gd = Main.instance.GraphicsDevice;
                            RenderTarget2D render = IllusionBoundMod.Instance.render;
                            //SpriteBatch spriteBatch = Main.spriteBatch;
                            //spriteBatch.End();
                            gd.SetRenderTarget(render);
                            gd.Clear(Color.Transparent);
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans * 2);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
                            IllusionBoundMod.ShaderSwooshEX.Parameters["uTransform"].SetValue(resultMatrix);
                            IllusionBoundMod.ShaderSwooshEX.Parameters["uLighter"].SetValue(0);
                            IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(0);//-(float)Main.time * 0.06f
                            IllusionBoundMod.ShaderSwooshEX.Parameters["checkAir"].SetValue(true);
                            IllusionBoundMod.ShaderSwooshEX.Parameters["airFactor"].SetValue(1);
                            IllusionBoundMod.ShaderSwooshEX.Parameters["gather"].SetValue(true);
                            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/BaseTex_7");
                            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/AniTex");
                            Main.graphics.GraphicsDevice.Textures[2] = TextureAssets.Item[player.HeldItem.type].Value;
                            Main.graphics.GraphicsDevice.Textures[3] = IllusionBoundMod.HeatMap[24];

                            Main.graphics.GraphicsDevice.SamplerStates[0] = sampler;
                            Main.graphics.GraphicsDevice.SamplerStates[1] = sampler;
                            Main.graphics.GraphicsDevice.SamplerStates[2] = sampler;
                            Main.graphics.GraphicsDevice.SamplerStates[3] = sampler;

                            IllusionBoundMod.ShaderSwooshEX.CurrentTechnique.Passes[2].Apply();
                            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0, bars.Count - 2);
                            Main.graphics.GraphicsDevice.RasterizerState = originalState;
                            spriteBatch.End();
                            //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans * 2);//Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix

                            IllusionBoundMod.Distort.Parameters["offset"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
                            IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);

                            IllusionBoundMod.Distort.Parameters["position"].SetValue(new Vector2(0, 3f));
                            IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.2f);
                            for (int n = 0; n < 3; n++)
                            {
                                gd.SetRenderTarget(Main.screenTargetSwap);
                                gd.Clear(Color.Transparent);
                                IllusionBoundMod.Distort.CurrentTechnique.Passes[7].Apply();
                                spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);



                                gd.SetRenderTarget(Main.screenTarget);
                                gd.Clear(Color.Transparent);
                                IllusionBoundMod.Distort.CurrentTechnique.Passes[6].Apply();
                                spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                            }
                            IllusionBoundMod.Distort.Parameters["position"].SetValue(new Vector2(0, 5f));
                            IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(0.707f) * -0.006f);//projectile.rotation.ToRotationVector2() * -0.006f


                            for (int n = 0; n < 2; n++)
                            {
                                gd.SetRenderTarget(Main.screenTargetSwap);
                                gd.Clear(Color.Transparent);
                                IllusionBoundMod.Distort.CurrentTechnique.Passes[5].Apply();
                                spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);

                                gd.SetRenderTarget(Main.screenTarget);
                                gd.Clear(Color.Transparent);
                                IllusionBoundMod.Distort.CurrentTechnique.Passes[4].Apply();
                                spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                            }

                            spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                            spriteBatch.Draw(render, Vector2.Zero, Color.White);
                            spriteBatch.End();
                            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.Default, RasterizerState.CullNone, null, trans * 2);
                        }
                        #endregion
                        if (oculusTears.Count > 0)
                        {


                            var sb = Main.spriteBatch;
                            #region Render
                            var render = IllusionBoundMod.Instance.render;
                            //var gd = Main.graphics.GraphicsDevice;
                            //�����Լ���render�ϻ������Ļ
                            //sb.End();
                            gd.SetRenderTarget(render);
                            gd.Clear(Color.Transparent);
                            #endregion
                            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);
                            /*                    sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);*///Main.DefaultSamplerState//Main.GameViewMatrix.TransformationMatrix
                            foreach (var projectile in oculusTears)
                            {
                                //var fac = projectile.ai[0].SymmetricalFactor(90, 10) * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f);
                                ////var fac = projectile.ai[0].HillFactor2(180);
                                //IllusionBoundMod.TransformEffect.Parameters["factor1"].SetValue(fac);
                                var fac = projectile.ai[0];
                                fac = fac < 30 ? ((fac * fac * 0.02022f - fac * 0.606f + 5) * (1 - 0.03f * fac)) : ((-90 / (fac - 181f) * 1.25f) * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f * (fac - 30).SymmetricalFactor(75, 15)));
                                if(fac < 0)
                                Main.NewText((fac,projectile.ai[0]));
                                IllusionBoundMod.TransformEffect.Parameters["factor1"].SetValue(fac);
                                IllusionBoundMod.TransformEffect.Parameters["factor2"].SetValue((float)ModTime / 30f);
                                IllusionBoundMod.TransformEffect.CurrentTechnique.Passes[1].Apply();

                                //Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.Projectile[projectile.type].Value;
                                //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicWrap;
                                //IllusionBoundMod.TransformEffect.CurrentTechnique.Passes[0].Apply();
                                //CustomVertexInfo[] customVertexInfos = new CustomVertexInfo[6];
                                //var baseVec = new Vector2((int)projectile.ai[0] == 3 ? 92f : 69f).RotatedBy(projectile.rotation);
                                //var offsetTime = (float)ModTime / 60f;
                                //customVertexInfos[0] = new CustomVertexInfo(projectile.Center + baseVec - Main.screenPosition, new Vector3(1, 1, 1));
                                //customVertexInfos[1] = new CustomVertexInfo(projectile.Center + new Vector2(baseVec.Y, -baseVec.X) - Main.screenPosition, new Vector3(0, 1, 1));
                                //customVertexInfos[2] = new CustomVertexInfo(projectile.Center + new Vector2(-baseVec.Y, baseVec.X) - Main.screenPosition, new Vector3(1, 0, 1));
                                //customVertexInfos[3] = customVertexInfos[2];
                                //customVertexInfos[4] = customVertexInfos[1];
                                //customVertexInfos[5] = new CustomVertexInfo(projectile.Center - baseVec - Main.screenPosition, new Vector3(0, 0, 1));
                                //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, customVertexInfos, 0, 2);
                                sb.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(512), ((int)projectile.ai[1] == 3 ? 2.5f : 2f) * 46 / 512 * new Vector2(3, 1), 0, 0);//new Rectangle(240,240,92,92)
                            }

                            sb.End();
                            #region render
                            //Ȼ�������һ��render�������Ļ�����������Ǹ�����Ļ��render����shader�����Ļ���д���
                            //ԭ���Դ���screenTargetSwap����һ������ʹ�õ�render����ԭ�������������˾���
                            gd.SetRenderTarget(Main.screenTargetSwap);
                            gd.Clear(Color.Transparent);
                            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
                            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg");// Backgrounds/StarSky_0 Backgrounds/StarSkyv2  Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg
                            IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
                            IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render���Ե�����ͼʹ�û��߻��ơ���ǰ���ǵ�ǰgd.SetRenderTarget�Ĳ������render������ᱨ��
                                                                                         //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
                            IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.35f);
                            IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
                            IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.30f);
                            IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.LocalPlayer.Center + new Vector2(0.707f) * (float)IllusionBoundMod.ModTime * 8);
                            IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(new Vector4(1, 0, 0.25f, 1));//Color.Cyan.ToVector4()//default(Vector4)//Color.Cyan.ToVector4()//new Vector4(1, 0, 0.25f, 1)
                                                                                                                       //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
                                                                                                                       //Main.NewText("!!!");
                            IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(64, 48));//new Vector2(1280, 2758)//new Vector2(960,560)  64, 48

                            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")

                            sb.End();

                            //�����screenTarget�ϰѸոյĽ������
                            gd.SetRenderTarget(Main.screenTarget);
                            gd.Clear(Color.Transparent);
                            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                            sb.End();
                            #endregion

                        }
                    }
                }

                if (bloomValue > 0)
                    UseBloom(gd);

                #region Render
                //var gd = Main.graphics.GraphicsDevice;
                //�����Լ���render�ϻ������Ļ
                //sb.End();
                //gd.SetRenderTarget(render);
                //gd.Clear(Color.Transparent);
                #endregion
                //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);
                //Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                //Main.spriteBatch.End();
                #region render
                ////Ȼ�������һ��render�������Ļ�����������Ǹ�����Ļ��render����shader�����Ļ���д���
                ////ԭ���Դ���screenTargetSwap����һ������ʹ�õ�render����ԭ�������������˾���
                //gd.SetRenderTarget(Main.screenTargetSwap);
                //gd.Clear(Color.Transparent);
                //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);//, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone
                //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg");// Backgrounds/StarSky_0 Backgrounds/StarSkyv2  Contents/StarBound/Weapons/UniqueWeapon/OculusReaver/OculusReaverTearBkg
                //IllusionBoundMod.Distort.CurrentTechnique.Passes[1].Apply();
                //IllusionBoundMod.Distort.Parameters["tex0"].SetValue(render);//render���Ե�����ͼʹ�û��߻��ơ���ǰ���ǵ�ǰgd.SetRenderTarget�Ĳ������render������ᱨ��
                //                                                             //IllusionBoundMod.Distort.Parameters["offset"].SetValue((u + v) * -0.002f * (1 - 2 * Math.Abs(0.5f - fac)) * IllusionSwooshConfigClient.instance.distortFactor);
                //IllusionBoundMod.Distort.Parameters["invAlpha"].SetValue(0.35f);
                //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
                //IllusionBoundMod.Distort.Parameters["tier2"].SetValue(0.30f);
                //IllusionBoundMod.Distort.Parameters["position"].SetValue(Main.LocalPlayer.Center + new Vector2(0.707f) * (float)IllusionBoundMod.ModTime * 8);
                //IllusionBoundMod.Distort.Parameters["maskGlowColor"].SetValue(Color.Cyan.ToVector4());//Color.Cyan.ToVector4()//default(Vector4)//Color.Cyan.ToVector4()//new Vector4(1, 0, 0.25f, 1)
                //                                                                                      //IllusionBoundMod.Distort.Parameters["lightAsAlpha"].SetValue(true);
                //                                                                                      //Main.NewText("!!!");
                //IllusionBoundMod.Distort.Parameters["ImageSize"].SetValue(new Vector2(960, 560));//new Vector2(1280, 2758)//new Vector2(960,560)  64, 48

                //Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);//ModContent.GetTexture("IllusionBoundMod/Backgrounds/StarSky_1")
                //Main.spriteBatch.End();

                ////�����screenTarget�ϰѸոյĽ������
                //gd.SetRenderTarget(Main.screenTarget);
                //gd.Clear(Color.Transparent);
                //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                //Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                //Main.spriteBatch.End();
                #endregion
            }
            orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
        }
        public override void Unload()
        {
            //Main.inventoryBack10Texture = originTex;
            //trees.Clear();
            ////On.Terraria.NPC.UpdateNPC -= TimeStopUpdate;
            //On.Terraria.Main.DrawTiles -= DrawTilesS;
            ////On.Terraria.NPC.UpdateNPC -= UPDNPC;
            //IL.Terraria.Main.DrawTiles -= _DrawTreeHook;
            Instance = null;
            On.Terraria.Graphics.Effects.FilterManager.EndCapture -= FilterManager_EndCapture_IllusionBound;
            Main.OnResolutionChanged -= Main_OnResolutionChanged;
            On.Terraria.Main.DrawMenu -= Main_DrawMenu;
            //On.Terraria.Main.DrawPlayer -= WeaponDisplayDrawPlayer;
        }
        private void Main_OnResolutionChanged(Vector2 obj)//�ڷֱ��ʸ���ʱ���ؽ�render��ֹĳЩbug
        {
            Main.RunOnMainThread(CreateRender);
        }
        public static Effect Bloom;
        public static float bloomValue;
        private void UseBloom(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //ȡ��
            graphicsDevice.SetRenderTarget(render);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.CurrentTechnique.Passes[0].Apply();//ȡ���ȳ���mֵ�Ĳ���

            Bloom.Parameters["m"].SetValue(0.6f);

            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //����
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.Parameters["uScreenResolution"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
            Bloom.Parameters["uRange"].SetValue(2.5f);
            Bloom.Parameters["uIntensity"].SetValue(bloomValue);
            for (int i = 0; i < 3; i++)//����ʹ������RenderTarget2D�����ж��ģ��
            {
                Bloom.CurrentTechnique.Passes["GlurV"].Apply();//����
                graphicsDevice.SetRenderTarget(Main.screenTarget);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);

                Bloom.CurrentTechnique.Passes["GlurH"].Apply();//����
                graphicsDevice.SetRenderTarget(render);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            }
            Main.spriteBatch.End();

            //���ӵ�ԭͼ��
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);//Additive��ģ����Ĳ��ּӵ�Main.screenTarget��
            Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);
            Main.spriteBatch.End();
            bloomValue = 0;
        }
        public void CreateRender()
        {
            render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth == 0 ? 1920 : Main.screenWidth, Main.screenHeight == 0 ? 1120 : Main.screenHeight);
        }
        public RenderTarget2D render;
        public static Effect DefaultEffect;
        public static Effect ColorfulEffect;
        public static Effect FinalFractalTailEffect;
        public static Effect RainBowTailEffect;
        public static Effect LaserEffect;
        public static Effect ColorfulLaserEffect;
        public static Effect TerraEffect;
        public static Effect IMBellEffect;
        public static Texture2D[] HeatMap = new Texture2D[25];
        public static Texture2D[] BaseTexes = new Texture2D[4];
        public static Texture2D[] AniTexes = new Texture2D[11];
        public static Texture2D[] LaserTex = new Texture2D[4];
        public static Texture2D[] MagicZone = new Texture2D[4];
        public static Effect CleverEffect;
        public static Effect ShaderSwoosh;
        public static Effect OriginEffect;
        public static Effect TextureEffect;
        public static Effect ShaderSwooshEX;
        public static Effect Distort;
        public static Effect TransformEffect;

        private void LoadTex(Texture2D[] texs, string name)
        {
            for (int n = 0; n < texs.Length; n++)
            {
                texs[n] = GetTexture("Images/" + name + n);
            }
        }
        public static Texture2D MoriyaEyeTex;
        public static Texture2D StormTreeTopGlow;
        public override void PostSetupContent()
        {
            DefaultEffect = GetEffect("Effects/Trail");
            ColorfulEffect = GetEffect("Effects/Trail2");
            FinalFractalTailEffect = GetEffect("Effects/Trail3");
            RainBowTailEffect = GetEffect("Effects/Trail4");
            LaserEffect = GetEffect("Effects/Trail5");
            ColorfulLaserEffect = GetEffect("Effects/Trail6");
            TerraEffect = GetEffect("Effects/Trail7");
            CleverEffect = GetEffect("Effects/CleverGlass");
            IMBellEffect = GetEffect("Effects/ItemGlowEffect");//InfiniteNightmareBell2
            ShaderSwoosh = GetEffect("Effects/ShaderSwooshEffect");
            OriginEffect = GetEffect("Effects/PixelShader");
            TextureEffect = GetEffect("Effects/TextureEffect");
            ShaderSwooshEX = GetEffect("Effects/ShaderSwooshEffectEX");
            Distort = GetEffect("Effects/DistortEffect");
            TransformEffect = GetEffect("Effects/TransformEffect");
            Bloom = GetEffect("Effects/Bloom1");
            //MainColor_1 = GetTexture("Images/HeatMap_1");
            //MainColor_2 = GetTexture("Images/HeatMap_2");
            LoadTex(HeatMap, "HeatMap_");
            LoadTex(AniTexes, "Style_");
            LoadTex(BaseTexes, "Light_");
            LoadTex(LaserTex, "LaserTex_");
            LoadTex(MagicZone, "MagicZone/MagicZone_");
            //for (int n = 0; n < rain.Length; n++)
            //{
            //    rain[n] = new StormRain();
            //}

            //TODO ��ʱע��
            //StormTreeTopGlow = GetTexture("Tiles/StormZone/StormTree_Tops_Glow");
            //MoriyaEyeTex = GetTexture("Items/TouhouProject/ZunHat/MoriyaSuwakoHatEye");

        }
        //public static Effect grayEffect;




        ////����һ������ΪExampleUI�ı���
        //public Items.Others.StrawBerryLinerUI vertexUI;
        ////����һ������ΪUserInterface�ı���
        //public UserInterface vertexUserInterface;

        public const string CopperBarRG = "VirtualDream:CopperBar";
        public const string SilverBarRG = "VirtualDream:SilverBar";
        public const string GoldBarRG = "VirtualDream:GoldBar";
        public const string TorchRG = "VirtualDream:Torch";
        public const string PaintRG = "VirtualDream:Paint";
        public const string CobaltRG = "VirtualDream:CobaltBar";
        public const string MythrilBarRG = "VirtualDream:MythrilBar";
        public const string AdamantiteBarRG = "VirtualDream:AdamantiteBar";
        public override void AddRecipeGroups()
        {
            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ͭ��", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup(CopperBarRG, group1);
            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ����", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup(SilverBarRG, group2);
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ��", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup(GoldBarRG, group3);
            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ���", new int[]
            {
                ItemID.Torch,
                ItemID.BlueTorch,
                ItemID.BoneTorch,
                ItemID.CursedTorch,
                ItemID.DemonTorch,
                ItemID.GreenTorch,
                ItemID.IceTorch,
                ItemID.IchorTorch,
                ItemID.OrangeTorch,
                ItemID.PinkTorch,
                ItemID.PurpleTorch,
                ItemID.RainbowTorch,
                ItemID.RedTorch,
                ItemID.TikiTorch,
                ItemID.UltrabrightTorch,
                ItemID.WhiteTorch,
                ItemID.YellowTorch,
            });
            RecipeGroup.RegisterGroup(TorchRG, group4);
            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ����", new int[]
            {
                ItemID.BlackPaint,
                ItemID.BluePaint,
                ItemID.BrownPaint,
                ItemID.CyanPaint,
                ItemID.DeepBluePaint,
                ItemID.DeepCyanPaint,
                ItemID.DeepGreenPaint,
                ItemID.DeepLimePaint,
                ItemID.DeepOrangePaint,
                ItemID.DeepPinkPaint,
                ItemID.DeepPurplePaint,
                ItemID.DeepRedPaint,
                ItemID.DeepSkyBluePaint,
                ItemID.DeepTealPaint,
                ItemID.DeepVioletPaint,
                ItemID.DeepYellowPaint,
                ItemID.GrayPaint,
                ItemID.GreenPaint,
                ItemID.LimePaint,
                ItemID.NegativePaint,
                ItemID.OrangePaint,
                ItemID.PinkPaint,
                ItemID.PurplePaint,
                ItemID.RedPaint,
                ItemID.ShadowPaint,
                ItemID.SkyBluePaint,
                ItemID.TealPaint,
                ItemID.VioletPaint,
                ItemID.WhitePaint,
                ItemID.YellowPaint,
            });
            RecipeGroup.RegisterGroup(PaintRG, group5);
            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ������", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup(CobaltRG, group6);
            RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ������", new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup(MythrilBarRG, group7);
            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ����", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup(AdamantiteBarRG, group8);
        }


    }
    public class IllusionBoundModSystem : ModSystem
    {
        public IllusionBoundModSystem instance;
        public static ModKeybind SpectreModeO;
        public static ModKeybind OriginPoint;
        public static ModKeybind IHat;
        public static ModKeybind JHat;
        public static ModKeybind PlanetDestroyerMode;
        public static ModKeybind SeasonRelease;
        public static ModKeybind aegisaltAuto;

        public static bool MagnifyingGlassActive;
        public static bool MagicalMagnifyingGlassActive;
        public static bool CleverGlassActive;
        public static bool RainBowGlassActive;
        public static bool ContrastGlassActive;
        public static bool ShikieikiGlassActive;
        public static bool InversPhaseGlassActive;
        public static bool ContrastDownGlassActive;
        public static bool ContrastUPGlassActiveV2;
        public static bool ZenithGlassActive;
        public override void Load()
        {
            SpectreModeO = KeybindLoader.RegisterKeybind(Mod, "���ģʽ����", "V");
            OriginPoint = KeybindLoader.RegisterKeybind(Mod, "����ԭ��ģʽ", "O");
            IHat = KeybindLoader.RegisterKeybind(Mod, "����Iñģʽ", "I");
            JHat = KeybindLoader.RegisterKeybind(Mod, "����Jñģʽ", "J");
            PlanetDestroyerMode = KeybindLoader.RegisterKeybind(Mod, "���ǻ����߳��ģʽ�л�", "F");
            SeasonRelease = KeybindLoader.RegisterKeybind(Mod, "���ڽ��", "C");
            aegisaltAuto = KeybindLoader.RegisterKeybind(Mod, "�Զ�����������", "N");
            instance = this;
        }

        public static double ModTime;
        public static double ModTime2;
        public static int TimeStopCount;
        public static float glowLight;
        public override void UpdateUI(GameTime gameTime)
        {
            Utils.IllusionBoundExtensionMethods.ODEStarTimer += 1 / 120f;
            Utils.IllusionBoundExtensionMethods.ODEStarTimer %= 4;
            TimeStopCount -= (TimeStopCount > -300 && !Main.gamePaused) ? 1 : 0;
            //GlassLightUpdate(ref IllusionBoundWorld.StormGlassGrowLightPink, 1);
            //GlassLightUpdate(ref IllusionBoundWorld.StormGlassGrowLightPurple, 2);
            //GlassLightUpdate(ref IllusionBoundWorld.StormGlassGrowLightBlue, 3);
            //IllusionBoundWorld.StormGlassColor = (new Vector4(254, 200, 231, 255) * IllusionBoundWorld.StormGlassGrowLightPink + new Vector4(206, 153, 255, 255) * IllusionBoundWorld.StormGlassGrowLightPurple + new Vector4(149, 225, 233, 255) * IllusionBoundWorld.StormGlassGrowLightBlue).ToColor();
            ModTime++;
            ModTime2 += Main.gamePaused ? 0 : 1;
            if (!Main.gamePaused)
            {
                foreach (var item in IllusionBoundMod.electricTriangle)
                {
                    item?.Update();
                }
            }
            //if (Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm)
            //{
            //    Tiles.StormZone.StormRain.MakeRain();
            //}
            //counter = 0;
            glowLight = ((float)Math.Sin(ModTime * MathHelper.TwoPi / 120) + 1) / 2;
        }
        public override void PreUpdateEntities()
        {
            ControlScreenShader("VirtualDream:MagnifyingGlass", MagnifyingGlassActive);
            ControlScreenShader("VirtualDream:MagicalMagnifyingGlass", MagicalMagnifyingGlassActive);
            ControlScreenShader("VirtualDream:CleverGlass", CleverGlassActive);
            ControlScreenShader("VirtualDream:RainbowGlass", RainBowGlassActive);
            ControlScreenShader("VirtualDream:ContrastGlass", ContrastGlassActive);
            ControlScreenShader("VirtualDream:ContrastGlassV2", ContrastUPGlassActiveV2);
            ControlScreenShader("VirtualDream:ShikieikiGlass", ShikieikiGlassActive);
            ControlScreenShader("VirtualDream:InversPhaseGlass", InversPhaseGlassActive);
            ControlScreenShader("VirtualDream:ZenithGlass", ZenithGlassActive);
            ControlScreenShader("VirtualDream:ContrastDownGlass", ContrastDownGlassActive);
            //ControlScreenShader("Test", false);
            //ControlScreenShader("Test2", false);
            //ControlScreenShader("HeatDistortion", false);
            //ControlScreenShader("IllusionBoundMod:HeatDistortion", false);
            //var p = Filters.Scene["IllusionBoundMod:HeatDistortion_"].GetShader().Shader.Parameters;
            ////p["uImageSize1"].SetValue(new Vector2(1/256f));
            //p["uTime"].SetValue(Main.GlobalTime / 5f % 60);
            ////p["uIntensity"].SetValue(0.007f);
            ////Main.NewText(p["uTime"].GetValueSingle());
            ////p["uScreenResolution"].SetValue(new Vector2(1920, 1120));
            ////p[""]
            //ControlScreenShader("IllusionBoundMod:HeatDistortion_",true);

        }
        private void ControlScreenShader(string name, bool state)
        {
            //TODO null
            if ((!Filters.Scene[name]?.IsActive() ?? false) && state)
            {
                Filters.Scene.Activate(name);
            }
            if ((Filters.Scene[name]?.IsActive() ?? false) && !state)
            {
                Filters.Scene.Deactivate(name);
            }
        }
        //public override void UpdateMusic(ref int music, ref MusicPriority priority)
        //{
        //    Player player = Main.player[Main.myPlayer];
        //    if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
        //    {
        //        return;
        //    }
        //    if (Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm)
        //    {
        //        music = ModLoader.GetMod("IllusionMusicMod") != null ? ModLoader.GetMod("IllusionMusicMod").GetSoundSlot(SoundType.Music, "Sounds/Music/EienNoMiko") : MusicID.Rain;
        //        priority = MusicPriority.Environment;
        //    }
        //    if (player.GetModPlayer<IllusionBoundPlayer>().SpectreModeP)
        //    {
        //        music = ModLoader.GetMod("IllusionMusicMod") != null ? ModLoader.GetMod("IllusionMusicMod").GetSoundSlot(SoundType.Music, "Sounds/Music/SpectreMode") : MusicID.Dungeon;
        //        priority = MusicPriority.BossHigh;
        //    }
        //}
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            //new SpectreBar();
            //new SpringBar();
            //new SummerBar();
            //new AutumnBar();
            //new WinterBar();
            //new SoilBar();
            //for (int n = 0; n < 180; n++)
            //{
            //    var fac = (float)n;
            //    fac = fac < 30 ? ((fac * fac / 45f - fac * 0.666667f + 5) * (1 - 0.03f * fac)) : (0.625f * (0.8f + (float)Math.Sin(ModTime / 30 * MathHelper.Pi) * 0.2f * (fac - 30f).SymmetricalFactor(75, 15)));
            //    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(960, 600) + new Vector2(n - 90, 300 - fac * 128), new Rectangle(0, 0, 1, 1), Color.Cyan, 0, new Vector2(0.5f), 4, 0, 0);
            //}
            if (Contents.InfiniteNightmare.InfiniteNightmarePlayer.TooDarkBuffActive)
            {
                Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Dark"), new Vector2(0, 0), null, new Color(153, 153, 153, 153), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            if (Contents.InfiniteNightmare.InfiniteNightmarePlayer.TooDazzlingBuffActive)
            {
                Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Light"), new Vector2(0, 0), null, new Color(51, 51, 51, 51), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            //Main.NewText(Filters.Scene["IllusionBoundMod:HeatDistortion"].GetShader().Shader.Parameters["uImageSize1"].GetValueVector2());
            //Main.spriteBatch.Draw((Texture2D)Main.graphics.GraphicsDevice.Textures[1], new Vector2(120, 120), Color.White);
            //Main.spriteBatch.Draw(Main.screenTarget, new Vector2(64, 64), null, Color.White * .5f, 0, default, 1, 0, 0);

            //var ca = new Color(255, 255, 255, 255);
            //var cb = new Color(255, 0, 255, 255);
            //         spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(304, 264, 1312, 592), Color.Black);
            //         spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(320, 280, 960, 560), cb);
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, (Blend.Zero, Blend.SourceColor).GetBlendState(), SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(640, 280, 960, 560), ca);
            //         spriteBatch.End();
            //         spriteBatch.Begin();
            //         Main.NewText(cb);

            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            //var effect = GetEffect("Effects/TestTextEffect");
            //effect.Parameters["uTime"].SetValue((float)ModTime * 0.03f);
            //effect.Parameters["uColor"].SetValue(Main.hslToRgb((float)ModTime * 0.03f % 1, 1, 0.75f).ToVector4());
            //Main.graphics.GraphicsDevice.Textures[1] = MaskColor[4];
            //effect.CurrentTechnique.Passes[0].Apply();
            //spriteBatch.DrawString(Main.fontMouseText, "���ºõ�", new Vector2(960, 560), Color.Cyan);
            //spriteBatch.End();
            //spriteBatch.Begin();
        }
    }
    public class ElectricTriangle
    {
        public Vector2 position;
        public Vector2 velocity;
        //public int timeMax;
        public int cycle;
        public int timeLeft = -1;
        public int dustType;
        public float rotation;
        public float size;
        public bool Active => timeLeft > -1;
        public static int NewElectricTriangle(Vector2 position, float rotation = 0, float size = 16, Vector2 velocity = default, int cycle = 15, int timeLeft = 30, int? dustType = null)
        {
            int index = -1;
            for (int n = 0; n < IllusionBoundMod.electricTriangle.Length; n++)
            {
                var currentTri = IllusionBoundMod.electricTriangle[n];
                if (currentTri == null)
                {
                    IllusionBoundMod.electricTriangle[n] = new ElectricTriangle()
                    {
                        position = position,
                        rotation = rotation,
                        size = size,
                        velocity = velocity,
                        cycle = cycle,
                        timeLeft = timeLeft,
                        //timeMax = timm
                        dustType = dustType ?? MyDustId.CyanBubble
                    };
                    index = n;
                    break;
                }
                if (!currentTri.Active)
                {
                    currentTri.position = position;
                    currentTri.rotation = rotation;
                    currentTri.size = size;
                    currentTri.velocity = velocity;
                    currentTri.cycle = cycle;
                    currentTri.timeLeft = timeLeft;
                    currentTri.dustType = dustType ?? MyDustId.CyanBubble;
                    index = n;
                    break;
                }
            }
            return index;
        }
        public void Update()
        {
            if (timeLeft < 0) return;
            Dust.NewDustPerfect(position + (timeLeft / (float)cycle % 1).GetLerpValue_Loop(new Vector2(0.8660254f, -0.5f), new Vector2(0, 1), new Vector2(-0.8660254f, -0.5f)).RotatedBy(rotation) * size, dustType, default, 0, Color.White, .5f).noGravity = true;
            timeLeft--;
            position += velocity;
        }
    }
    public class IllusionBoundGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.BrokenHeroSword)
            {
                var targetText = item.GetType().GetField("_nameOverride", BindingFlags.Instance | BindingFlags.NonPublic);
                targetText.SetValue(item, "Ӣ�۽���֮��");
            }
        }
        //public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        //{
        //    TooltipLine tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
        //    if (item.type == ItemType<Items.Weapons.MagicDictionary.VoiceOfNature>())
        //    {
        //        tooltipLine.OverrideColor = Color.Lerp(new Color(107, 206, 107), new Color(206, 107, 206), (float)Math.Sin(MathHelper.Pi / 60 * IllusionBoundMod.ModTime) / 2 + 0.5f);
        //    }
        //    if (item.type == ItemType<Items.Weapons.FinalFractal>() || item.type == ItemType<Items.Weapons.MagicDictionary.MagicDictionary>())
        //    {
        //        tooltipLine.OverrideColor = Color.Lerp(new Color(99, 74, 187), new Color(20, 120, 118), (float)Math.Sin(MathHelper.Pi / 60 * IllusionBoundMod.ModTime) / 2 + 0.5f);
        //    }
        //}
    }
    //public class MaterialsDrop : GlobalNPC
    //{
    //    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    //    {
    //        Player player = Main.player[Main.myPlayer];
    //        if (npc.type == NPCID.Mothron && Main.rand.NextBool(4))
    //        {
    //            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Materials.ExtremeBreakTheRuleManaCrystal>());
    //        }
    //        if (!npc.friendly && npc.value > 0f && NPC.downedMoonlord)
    //        {
    //            if (npc.lifeMax > 2000)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Materials.AncientEssence>(), Main.rand.Next(40, 60));
    //            }
    //            else if (npc.lifeMax > 1000)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Materials.AncientEssence>(), Main.rand.Next(25, 30));
    //            }
    //            else if (npc.lifeMax > 500)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Materials.AncientEssence>(), Main.rand.Next(15, 20));
    //            }
    //            else if (npc.lifeMax > 100)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Materials.AncientEssence>(), Main.rand.Next(5, 10));
    //            }
    //            if (player.ZoneSnow)
    //            {
    //                LowerMaterials(ItemType<Materials.CryonicExtract>(), npc);
    //            }
    //            if (player.ZoneJungle)
    //            {
    //                LowerMaterials(ItemType<Materials.VenomSample>(), npc);
    //            }
    //            if (player.ZoneUnderworldHeight)
    //            {
    //                LowerMaterials(ItemType<Materials.ScorchedCore>(), npc);
    //            }
    //            if (player.ZoneDesert || player.ZoneUndergroundDesert)
    //            {
    //                LowerMaterials(ItemType<Materials.SharpenedClaw>(), npc);
    //            }
    //            if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
    //            {
    //                LowerMaterials(ItemType<Materials.HardenedCarapace>(), npc);
    //            }
    //            if (player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHallow)
    //            {
    //                LowerMaterials(ItemType<Materials.Leather>(), npc);
    //            }
    //            if (player.ZoneSkyHeight)
    //            {
    //                LowerMaterials(ItemType<Materials.PhaseMatter>(), npc);
    //            }
    //            if (player.ZoneMeteor)
    //            {
    //                LowerMaterials(ItemType<Materials.StickOfRAM>(), npc);
    //            }
    //            if (player.ZoneDungeon)
    //            {
    //                LowerMaterials(ItemType<Materials.StaticCell>(), npc);
    //            }
    //            if (!(player.ZoneSnow || player.ZoneJungle || player.ZoneUnderworldHeight || player.ZoneDesert || player.ZoneUndergroundDesert || player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHallow || player.ZoneSkyHeight || player.ZoneMeteor || player.ZoneDungeon))
    //            {
    //                LowerMaterials(ItemType<Materials.LivingRoot>(), npc);
    //            }
    //        }
    //    }
    //    private void LowerMaterials(int type, NPC npc)
    //    {
    //        if (Main.rand.NextBool(20))
    //        {
    //            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), type);
    //        }
    //    }
    //}
}

