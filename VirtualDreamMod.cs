/*
 * IllusionBoundMod 
 * IllusionBoundMod.cs
 * 作者：AW
 * 
 * 允许编辑修改
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

using VirtualDream.Effects;

using static Terraria.ModLoader.ModContent;

//using static VirtualDream.IllusionBoundOnIlFunctions;
// 用两个斜杠开头的句子都是注释QAQ，对程序运行没有任何影响，读我就行了，不用删

// 以上都是需要使用的程序集，不要乱动，不然代码会出问题

// 引入命名空间，这里就是MOD的名字IllusionBoundMod
namespace VirtualDream
{

    //public abstract class IllusionBoundTree : ModTree 
    //{
    //    public abstract Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset,int y);
    //    public abstract Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame,int y);
    //}
    // MOD的主类名字，需要与文件名、MOD名完全一致，并且继承Mod类
    public class IllusionBoundMod : Mod
    {
        public static float GlowLight => ((float)Math.Sin(IllusionBoundModSystem.ModTime * MathHelper.TwoPi / 120) + 1) / 2;
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

        private int iconFrame = 0;
        private byte iconFrameCounter = 0;

        private Texture2D[] icon = new Texture2D[22];
        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            //以下两行为获取Main.MenuUI的UIState集
            FieldInfo uiStateField = Main.MenuUI.GetType().GetField("_history", BindingFlags.NonPublic | BindingFlags.Instance);
            List<UIState> _history = (List<UIState>)uiStateField.GetValue(Main.MenuUI);
            //使用for遍历UIState集，寻找UIMods类的实例
            for (int x = 0; x < _history.Count; x++)
            {
                //检测当前UIState的类名全称是否是ModLoader的UIMods
                if (_history[x].GetType().FullName == "Terraria.ModLoader.UI.UIMods")
                {
                    //以下两行为获取UIMods的UI部件集
                    FieldInfo elementsField = _history[x].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> elements = (List<UIElement>)elementsField.GetValue(_history[x]);

                    //由之前 了解模组选择页面的构成 一节可知，包含了 包含UIList部件的UIPanel 的UIElement第一个被UIMods包含，故此UIElement位于UIMods的部件集的0号索引处
                    //以下两行用于获取UIElement的UI部件集
                    FieldInfo uiElementsField = elements[0].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> uiElements = (List<UIElement>)uiElementsField.GetValue(elements[0]);

                    //同理，由 了解模组选择页面的构成 一节可知，UIPanel第一个被UIElements包含，故UIPanel位于UIElement的UI部件集的0号索引处
                    //以下两行用于获取UIPanel的UI部件集
                    FieldInfo myModUIPanelField = uiElements[0].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> myModUIPanel = myModUIPanelField.GetValue(uiElements[0]) as List<UIElement>;

                    //同理，由 了解模组选择页面的构成 一节可知，UIList第一个被UIPanel包含，故UIList位于UIPanel的UI部件集的0号索引处
                    UIList uiList = (UIList)myModUIPanel[0];
                    //遍历uiList包含的子部件，寻找我们mod的UIModItem部件
                    for (int i = 0; i < uiList._items.Count; i++)
                    {
                        //动态Icon老bug了
                        //反射获取mod实例，检测其是否是我们的mod
                        if (uiList._items[i].GetType().GetField("_mod", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(uiList._items[i]).ToString() == Name)
                        {
                            //以下两行为获取我们mod的UIModItem的UI部件集
                            FieldInfo myUIModItemField = uiList._items[i].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                            List<UIElement> myUIModItem = (List<UIElement>)myUIModItemField.GetValue(uiList._items[i]);

                            float _modIconAdjust = (GetTexture("icon") == null ? 0 : 85);
                            UIElement badUnloader = myUIModItem.Find((UIElement e) => e.ToString() == "Terraria.ModLoader.UI.UIHoverImage" && e.Top.Pixels == 3);
                            //遍历UIModItem的UI部件集
                            for (int j = 0; j < myUIModItem.Count; j++)
                            {
                                //如果当前UI部件是UIImage，且其宽高均为80
                                if (myUIModItem[j] is UIImage && myUIModItem[j].Width.Pixels == 80 && myUIModItem[j].Height.Pixels == 80)
                                {
                                    //修改此UI部件的贴图
                                    (myUIModItem[j] as UIImage).SetImage(icon[iconFrame]);
                                    //退出循环
                                    break;
                                }
                            }
                            //最后按逆序逐一SetValue
                            myUIModItemField.SetValue(uiList._items[i], myUIModItem);
                            myModUIPanel[0] = uiList;
                            myModUIPanelField.SetValue(uiElements[0], myModUIPanel);
                            uiElementsField.SetValue(elements[0], uiElements);
                            elementsField.SetValue(_history[x], elements);
                            uiStateField.SetValue(Main.MenuUI, _history);
                            //退出循环
                            break;
                        }
                    }
                    //退出循环
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
            //将exampleUI实例化
            //vertexUI = new Items.Others.StrawBerryLinerUI();
            ////将exampleUI初始化
            //vertexUI.Activate();
            ////将exampleUserInterface实例化
            //vertexUserInterface = new UserInterface();
            ////让exampleUserInterface代理exampleUI的事件触发
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

        private void FilterManager_EndCapture_IllusionBound(On.Terraria.Graphics.Effects.FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            GraphicsDevice gd = Main.instance.GraphicsDevice;
            if (bloomValue > 0)
                UseBloom(gd);
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
        private void Main_OnResolutionChanged(Vector2 obj)//在分辨率更改时，重建render防止某些bug
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

            //取样
            graphicsDevice.SetRenderTarget(render);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.CurrentTechnique.Passes[0].Apply();//取亮度超过m值的部分

            Bloom.Parameters["m"].SetValue(0.6f);

            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //处理
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.Parameters["uScreenResolution"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
            Bloom.Parameters["uRange"].SetValue(2.5f);
            Bloom.Parameters["uIntensity"].SetValue(bloomValue);
            for (int i = 0; i < 3; i++)//交替使用两个RenderTarget2D，进行多次模糊
            {
                Bloom.CurrentTechnique.Passes["GlurV"].Apply();//横向
                graphicsDevice.SetRenderTarget(Main.screenTarget);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);

                Bloom.CurrentTechnique.Passes["GlurH"].Apply();//纵向
                graphicsDevice.SetRenderTarget(render);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            }
            Main.spriteBatch.End();

            //叠加到原图上
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);//Additive把模糊后的部分加到Main.screenTarget里
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

            //TODO 临时注释
            //StormTreeTopGlow = GetTexture("Tiles/StormZone/StormTree_Tops_Glow");
            //MoriyaEyeTex = GetTexture("Items/TouhouProject/ZunHat/MoriyaSuwakoHatEye");

        }
        //public static Effect grayEffect;




        ////建立一个类型为ExampleUI的变量
        //public Items.Others.StrawBerryLinerUI vertexUI;
        ////建立一个类型为UserInterface的变量
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
            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 铜锭", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup(CopperBarRG, group1);
            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 银锭", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup(SilverBarRG, group2);
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 金锭", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup(GoldBarRG, group3);
            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 火把", new int[]
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
            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 油漆", new int[]
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
            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 钴蓝锭", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup(CobaltRG, group6);
            RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 秘银锭", new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup(MythrilBarRG, group7);
            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " 精金锭", new int[]
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
            SpectreModeO = KeybindLoader.RegisterKeybind(Mod, "灵界模式开启", "V");
            OriginPoint = KeybindLoader.RegisterKeybind(Mod, "设置原点模式", "O");
            IHat = KeybindLoader.RegisterKeybind(Mod, "设置I帽模式", "I");
            JHat = KeybindLoader.RegisterKeybind(Mod, "设置J帽模式", "J");
            PlanetDestroyerMode = KeybindLoader.RegisterKeybind(Mod, "行星毁灭者长戟模式切换", "F");
            SeasonRelease = KeybindLoader.RegisterKeybind(Mod, "季节解放", "C");
            aegisaltAuto = KeybindLoader.RegisterKeybind(Mod, "自动霓磷盐隐身", "N");
            instance = this;
        }

        public static double ModTime;
        public static double ModTime2;
        public static int TimeStopCount;

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
            //if (Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm)
            //{
            //    Tiles.StormZone.StormRain.MakeRain();
            //}
            //counter = 0;
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
            //spriteBatch.DrawString(Main.fontMouseText, "这事好的", new Vector2(960, 560), Color.Cyan);
            //spriteBatch.End();
            //spriteBatch.Begin();
        }
    }

    public class IllusionBoundGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.BrokenHeroSword)
            {
                var targetText = item.GetType().GetField("_nameOverride", BindingFlags.Instance | BindingFlags.NonPublic);
                targetText.SetValue(item, "英雄禁断之剑");
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
    //            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Items.Others.ExtremeBreakTheRuleManaCrystal>());
    //        }
    //        if (!npc.friendly && npc.value > 0f && NPC.downedMoonlord)
    //        {
    //            if (npc.lifeMax > 2000)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Items.Others.AncientEssence>(), Main.rand.Next(40, 60));
    //            }
    //            else if (npc.lifeMax > 1000)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Items.Others.AncientEssence>(), Main.rand.Next(25, 30));
    //            }
    //            else if (npc.lifeMax > 500)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Items.Others.AncientEssence>(), Main.rand.Next(15, 20));
    //            }
    //            else if (npc.lifeMax > 100)
    //            {
    //                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Items.Others.AncientEssence>(), Main.rand.Next(5, 10));
    //            }
    //            if (player.ZoneSnow)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.CryonicExtract>(), npc);
    //            }
    //            if (player.ZoneJungle)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.VenomSample>(), npc);
    //            }
    //            if (player.ZoneUnderworldHeight)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.ScorchedCore>(), npc);
    //            }
    //            if (player.ZoneDesert || player.ZoneUndergroundDesert)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.SharpenedClaw>(), npc);
    //            }
    //            if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.HardenedCarapace>(), npc);
    //            }
    //            if (player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHallow)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.Leather>(), npc);
    //            }
    //            if (player.ZoneSkyHeight)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.PhaseMatter>(), npc);
    //            }
    //            if (player.ZoneMeteor)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.StickOfRAM>(), npc);
    //            }
    //            if (player.ZoneDungeon)
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.StaticCell>(), npc);
    //            }
    //            if (!(player.ZoneSnow || player.ZoneJungle || player.ZoneUnderworldHeight || player.ZoneDesert || player.ZoneUndergroundDesert || player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHallow || player.ZoneSkyHeight || player.ZoneMeteor || player.ZoneDungeon))
    //            {
    //                LowerMaterials(ItemType<Items.Others.Materials.LivingRoot>(), npc);
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

