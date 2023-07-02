global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Terraria;
global using Terraria.Audio;
global using Terraria.GameContent;
global using Terraria.ModLoader;
global using VirtualDream.Utils;
global using LogSpiralLibrary.CodeLibrary;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
//using VirtualDream.Tiles.StormZone;
//using VirtualDream.NPCs.StormZone;
using System.Reflection;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
//using VirtualDream.UI.Spectre;
//using VirtualDream.UI;
using Terraria.UI;
using Terraria.UI.Chat;
using VirtualDream.Contents.StarBound.NPCs.Bosses.AsraNox;
using VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana;
using VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.OculusReaver;
using VirtualDream.Effects;

using static Terraria.ModLoader.ModContent;
using System.Linq;
using VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.AsuterosaberuDX;
using LogSpiralLibrary;

namespace VirtualDream
{
    //public class VirtualDreamMenu : ModMenu
    //{
    //    public override string DisplayName => base.DisplayName + nameof(VirtualDreamMenu);
    //}
    //public class StarboundMenu : ModMenu
    //{
    //    public override string DisplayName => base.DisplayName + nameof(StarboundMenu);

    //}
    //public abstract class IllusionBoundTree : ModTree
    //{
    //    public abstract Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset, int y);
    //    public abstract Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame, int y);
    //}
    // MOD的主类名字，需要与文件名、MOD名完全一致，并且继承Mod类
    public class VirtualDreamMod : Mod
    {
        public static float GlowLight => VirtualDreamSystem.glowLight;
        public static double ModTime => LogSpiralLibrarySystem.ModTime;
        public static double ModTime2 => LogSpiralLibrarySystem.ModTime2;

        public static VirtualDreamMod Instance;
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
        public static ElectricTriangle[] electricTriangle = new ElectricTriangle[100];
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
        public static Effect GetEffect(string path, bool autoModName = true) => Request<Effect>((autoModName ? "VirtualDream/" : "") + path, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D GetTexture(string path, bool autoModName = true) => Request<Texture2D>((autoModName ? "VirtualDream/" : "") + path, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public override void Load()
        {
            mod = this;
            Instance = this;
            if (Main.netMode == NetmodeID.Server) return;
            var effect = GetEffect("Effects/IllusionBoundScreenGlass");
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                Filters.Scene["VirtualDream:" + pass.Name] = new Filter(new IllusionScreenShaderData(new Ref<Effect>(effect), pass.Name), EffectPriority.Medium);
                Filters.Scene["VirtualDream:" + pass.Name].Load();
            }
            On.Terraria.UI.Chat.ChatManager.DrawColorCodedString_SpriteBatch_DynamicSpriteFont_TextSnippetArray_Vector2_Color_float_Vector2_Vector2_refInt32_float_bool += MoreMoreHeart;
            for (int i = 0; i < icon.Length; i++)
            {
                icon[i] = GetTexture($"icons/icon_ani_{i}");
            }
        }
        /// <summary>
        /// 烂活
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        /// <param name="snippets"></param>
        /// <param name="position"></param>
        /// <param name="baseColor"></param>
        /// <param name="rotation"></param>
        /// <param name="origin"></param>
        /// <param name="baseScale"></param>
        /// <param name="hoveredSnippet"></param>
        /// <param name="maxWidth"></param>
        /// <param name="ignoreColors"></param>
        /// <returns></returns>
        private Vector2 MoreMoreHeart(On.Terraria.UI.Chat.ChatManager.orig_DrawColorCodedString_SpriteBatch_DynamicSpriteFont_TextSnippetArray_Vector2_Color_float_Vector2_Vector2_refInt32_float_bool orig, SpriteBatch spriteBatch, ReLogic.Graphics.DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth, bool ignoreColors)
        {
            string date = DateTime.Now.ToShortDateString();
            var data = date.Split('/');
            TextSnippet[] result = snippets;
            if (data[1] == "5" && (data[2] == "21" || data[2] == "20"))
            {
                var _snippets = new TextSnippet[snippets.Length + 2];
                snippets.CopyTo(_snippets, 1);
                //var method = typeof(ItemTagHandler).GetMethod("Parse", BindingFlags.Instance | BindingFlags.NonPublic);
                var methods = typeof(ITagHandler).GetMethods();
                var snippet = (TextSnippet)(methods[0]?.Invoke(new ItemTagHandler(), new object[] { ItemID.Heart.ToString(), baseColor, null }));
                if (snippet == null) Main.NewText("??");
                _snippets[0] = snippet;
                _snippets[snippets.Length + 1] = snippet;
                result = _snippets;
            }

            return orig.Invoke(spriteBatch, font, result, position, baseColor, rotation, origin, baseScale, out hoveredSnippet, maxWidth, ignoreColors);

        }
        #region TestCodes
        public static byte[] musicBuffer;
        private void MP3AudioTrack_ReadAheadPutAChunkIntoTheBuffer(On.Terraria.Audio.MP3AudioTrack.orig_ReadAheadPutAChunkIntoTheBuffer orig, MP3AudioTrack self)
        {
            //if (NPC.AnyNPCs(ModContent.NPCType<AsraNox>()) && Main.gamePaused && Main.audioSystem is LegacyAudioSystem audioSystem)
            //{
            //    audioSystem.PauseAll();
            //    //var track = audioSystem.AudioTracks[Main.curMusic];

            //    //track.Stop(AudioStopOptions.Immediate);
            //}
            //else
            //{

            //}
            orig.Invoke(self);
            //var fieldInfo = typeof(MP3AudioTrack).GetField("_mp3Stream", BindingFlags.NonPublic | BindingFlags.Instance);
            //var fieldInfo2 = typeof(MP3AudioTrack).GetField("_bufferToSubmit", BindingFlags.NonPublic | BindingFlags.Instance);
            //var fieldInfo3 = typeof(MP3AudioTrack).GetField("_soundEffectInstance", BindingFlags.NonPublic | BindingFlags.Instance);

            //var mp3str = (XPT.Core.Audio.MP3Sharp.MP3Stream)fieldInfo.GetValue(self);
            //long position = mp3str.Position;
            //Main.NewText($"Mp3Length更新前:{position}");

            //{
            //    byte[] bufferToSubmit = (byte[])fieldInfo2.GetValue(self);
            //    int count = mp3str.Read(bufferToSubmit, 0, bufferToSubmit.Length);
            //    Main.NewText("读取了" + count);
            //    if (count < 1)
            //    {
            //        self.Stop(AudioStopOptions.Immediate);
            //    }
            //    else
            //    {
            //        byte[] newbuffer = new byte[bufferToSubmit.Length];
            //        int offsetor = (int)(ModTime / 10) % 32;
            //        for (int n = 0; n < bufferToSubmit.Length; n++)
            //        {
            //            newbuffer[n] = bufferToSubmit[n];
            //        }
            //        musicBuffer = newbuffer;
            //        ((DynamicSoundEffectInstance)fieldInfo3.GetValue(self)).SubmitBuffer(newbuffer);
            //    }
            //}
            //Main.NewText($"Mp3Length更新后:{mp3str.Position}");
            //Main.NewText($"Mp3Length差值:{mp3str.Position - position}");
        }
        //private void MP3AudioTrack_ReadAheadPutAChunkIntoTheBuffer(On.Terraria.Audio.MP3AudioTrack.orig_ReadAheadPutAChunkIntoTheBuffer orig, MP3AudioTrack self)
        //{
        //    var fieldInfo = typeof(MP3AudioTrack).GetField("_mp3Stream", BindingFlags.NonPublic | BindingFlags.Instance);
        //    var fieldInfo2 = typeof(MP3AudioTrack).GetField("_bufferToSubmit", BindingFlags.NonPublic | BindingFlags.Instance);
        //    var fieldInfo3 = typeof(MP3AudioTrack).GetField("_soundEffectInstance", BindingFlags.NonPublic | BindingFlags.Instance);

        //    var mp3str = (XPT.Core.Audio.MP3Sharp.MP3Stream)fieldInfo.GetValue(self);
        //    long position = mp3str.Position;
        //    Main.NewText($"Mp3Length更新前:{position}");
        //    //orig.Invoke(self);
        //    //orig.Invoke(self);

        //    {
        //        byte[] bufferToSubmit = (byte[])fieldInfo2.GetValue(self);
        //        int count = mp3str.Read(bufferToSubmit, 0, bufferToSubmit.Length);
        //        Main.NewText("读取了"+count);
        //        if (count < 1)
        //        {
        //            self.Stop(AudioStopOptions.Immediate);
        //        }
        //        //else if (mp3str.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
        //        //{
        //        //    self.Stop(AudioStopOptions.Immediate);
        //        //}
        //        else
        //        {
        //            byte[] newbuffer = new byte[bufferToSubmit.Length];
        //            int offsetor = (int)(ModTime / 10) % 32;
        //            //Main.NewText("当前频率系数" + offsetor);

        //            for (int n = 0; n < bufferToSubmit.Length; n++)
        //            {
        //                //var angle = MathHelper.TwoPi * n / 4096f * offsetor;
        //                //newbuffer[n] = (byte)((MathF.Sin(angle) * .67f + MathF.Sin(angle * .5f) * .33f) * 127 + 128);
        //                //newbuffer[n] = (byte)(((n / 4096f * offsetor) % 1 > .5f ? 1 : 0) * 127 + 128);
        //                //var factor = bufferToSubmit[n] / 255f;
        //                //newbuffer[n] = (byte)((1 - MathF.Cos(factor * MathHelper.TwoPi)) * .5f * 255);
        //                //newbuffer[n] = (byte)(((int)(factor * 9) / 9f) * 255);

        //                //newbuffer[n] = (byte)((MathF.Sin(MathHelper.TwoPi * MathF.Pow(n / 4096f * offsetor / 4f, 2f))) * 127 + 128);
        //                newbuffer[n] = bufferToSubmit[n];
        //            }
        //            musicBuffer = newbuffer;
        //            //Main.NewText($"Mp3Length更新中:{mp3str.Position}");
        //            ((DynamicSoundEffectInstance)fieldInfo3.GetValue(self)).SubmitBuffer(newbuffer);
        //            //Main.NewText($"Mp3Length更新后:{mp3str.Position}");
        //        }
        //    }
        //    Main.NewText($"Mp3Length更新后:{mp3str.Position}");
        //    Main.NewText($"Mp3Length差值:{mp3str.Position - position}");
        //    //try
        //    //{
        //    //    mp3str.Position += 418;
        //    //}
        //    //catch
        //    //{
        //    //    mp3str.Position = 0L;
        //    //}
        //}

        private int UnifiedRandom_Next_int_int(On.Terraria.Utilities.UnifiedRandom.orig_Next_int_int orig, Terraria.Utilities.UnifiedRandom self, int minValue, int maxValue)
        {
            //var value = orig.Invoke(self, minValue, maxValue);
            ////List<int> myList = new List<int>();
            ////var scaler = (maxValue - minValue) * .125f;
            ////for (int n = 0; n < maxValue; n++)
            ////{
            ////    var omgNum = Terraria.Utils.GetLerpValue(minValue, maxValue, value);
            ////    omgNum = (1 - (float)Math.Cos(Math.Sqrt(omgNum) * MathHelper.TwoPi)) * scaler + 1;
            ////    int k = 0;
            ////    while (k < omgNum)
            ////    {
            ////        myList.Add(n);
            ////        k++;
            ////    }
            ////}
            //return value;//(int)((1 - factor) * minValue + factor * maxValue)  //
            //List<int> myList = new List<int>();
            //for (int n = minValue; n < maxValue; n++)
            //{
            //    int k = 0;
            //    while (n + k * k * k < maxValue)
            //    {
            //        myList.Add(n);
            //        k++;
            //    }
            //}
            //return false ? self.Next(myList) : orig.Invoke(self, minValue, maxValue);//myList.Count > 0
            //    var value = maxValue;
            //    int counter = 0;
            //mylabel:
            //    value = orig.Invoke(self, minValue, value);
            //    counter++;
            //    if (counter < 2) goto mylabel;
            return orig.Invoke(self, minValue, maxValue);
        }

        private int UnifiedRandom_Next_int(On.Terraria.Utilities.UnifiedRandom.orig_Next_int orig, Terraria.Utilities.UnifiedRandom self, int maxValue)
        {
            var value = orig.Invoke(self, maxValue);
            return value;// >= maxValue / 2 ? maxValue - 1 : 0
        }
        #endregion

        public override void Unload()
        {
            Instance = null;
        }
        public static Effect Bloom;
        public static float bloomValue;
        public static Effect ColorfulEffect;
        public static Effect CleverEffect;
        public static Effect ShaderSwoosh;
        public static Effect OriginEffect;
        public static Effect TextureEffect;
        public override void PostSetupContent()
        {
            //TODO 把这个移入
            ColorfulEffect = GetEffect("Effects/Trail2");
            CleverEffect = GetEffect("Effects/CleverGlass");
            OriginEffect = GetEffect("Effects/PixelShader");
            TextureEffect = GetEffect("Effects/TextureEffect");
            Bloom = GetEffect("Effects/Bloom1");
        }
    }
    public class VirtualDreamSystem : ModSystem
    {
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
        public VirtualDreamSystem instance;
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
            instance = this;
        }

        public static double ModTime => LogSpiralLibrarySystem.ModTime;
        public static double ModTime2 => LogSpiralLibrarySystem.ModTime2;
        public static int TimeStopCount;
        public static float glowLight;
        public override void UpdateUI(GameTime gameTime)
        {
            TimeStopCount -= (TimeStopCount > -300 && !Main.gamePaused) ? 1 : 0;
            if (!Main.gamePaused)
            {
                foreach (var item in VirtualDreamMod.electricTriangle)
                {
                    item?.Update();
                }
            }
            glowLight = ((float)Math.Sin(ModTime * MathHelper.TwoPi / 120) + 1) / 2;
        }
        public override void PreUpdateEntities()
        {
            ControlScreenShader("VirtualDream:Magnifying", MagnifyingGlassActive);
            ControlScreenShader("VirtualDream:MagicalMagnifying", MagicalMagnifyingGlassActive);
            ControlScreenShader("VirtualDream:Clever", CleverGlassActive);
            ControlScreenShader("VirtualDream:Rainbow", RainBowGlassActive);
            ControlScreenShader("VirtualDream:Contrast", ContrastGlassActive);
            ControlScreenShader("VirtualDream:ContrastV2", ContrastUPGlassActiveV2);
            ControlScreenShader("VirtualDream:Shikieiki", ShikieikiGlassActive);
            ControlScreenShader("VirtualDream:InversPhase", InversPhaseGlassActive);
            ControlScreenShader("VirtualDream:Zenith", ZenithGlassActive);
            ControlScreenShader("VirtualDream:ContrastDown", ContrastDownGlassActive);
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
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            //var str = "";
            //if (Main.audioSystem is LegacyAudioSystem legacy)
            //{
            //    if (legacy.AudioTracks[Main.curMusic] is MP3AudioTrack audioTrack)
            //    {
            //        var fieldInfo = typeof(MP3AudioTrack).GetField("_mp3Stream", BindingFlags.NonPublic | BindingFlags.Instance);
            //        var fieldInfo2 = typeof(MP3AudioTrack).GetField("_bufferToSubmit", BindingFlags.NonPublic | BindingFlags.Instance);
            //        var mp3str = (XPT.Core.Audio.MP3Sharp.MP3Stream)fieldInfo.GetValue(audioTrack);
            //        var buffer = (byte[])fieldInfo2.GetValue(audioTrack);
            //        try
            //        {
            //            str = (mp3str.Position, mp3str.Length, mp3str.Frequency, buffer.Length, mp3str.CanTimeout).ToString();
            //        }
            //        catch { Main.NewText("报错了"); }
            //        //if ((int)ModTime % 600 == 0)
            //        //    mp3str.Position = 114514;
            //    }
            //}
            //if (str != "")
            //    Main.NewText(str);

            //var buffer = IllusionBoundMod.musicBuffer;
            //int length = buffer.Length;
            //Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, 1920, 1120), Color.White * .5f);
            //float rows = 16f;
            //float screenHeight = 1024f;
            //for (int n = 0; n < length - 1; n++)
            //{
            //    float factor1 = n / (length - 1f) ;
            //    float factor2 = (n + 1) / (length - 1f);
            //    float offsetY = (int)(factor1* rows) / rows;
            //    if (offsetY != (int)(factor2 * rows) / rows) continue;
            //    else offsetY *= screenHeight;
            //    factor1 = factor1 * rows % 1;
            //    factor2 = factor2 * rows % 1;
            //    Main.spriteBatch.DrawLine(new Vector2(factor1 * 1920, buffer[n] * (screenHeight / rows / 255f * .75f) + offsetY), new Vector2(factor2 * 1920, buffer[n + 1] * (screenHeight / rows / 255f * .75f) + offsetY), Color.Red, 4);
            //}
            //for (int n = 0; n < length; n++)
            //{
            //    float factor = n / (length - 1f);
            //    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(factor * 1920, 560 + buffer[n] - 255), new Rectangle(0, 0, 1, 1), Color.Red, 0, new Vector2(.5f), 4, 0, 0);
            //}
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
                Main.spriteBatch.Draw(VirtualDreamMod.GetTexture("Contents/InfiniteNightmare/Dark"), new Vector2(0, 0), null, new Color(153, 153, 153, 153), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            if (Contents.InfiniteNightmare.InfiniteNightmarePlayer.TooDazzlingBuffActive)
            {
                Main.spriteBatch.Draw(VirtualDreamMod.GetTexture("Contents/InfiniteNightmare/Light"), new Vector2(0, 0), null, new Color(51, 51, 51, 51), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
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
            //         spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
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
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
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
        public float dustSclaer;
        public bool Active => timeLeft > -1;
        public static int NewElectricTriangle(Vector2 position, float rotation = 0, float size = 16, Vector2 velocity = default, int cycle = 15, int timeLeft = 30, float dustScaler = .5f, int? dustType = null)
        {
            int index = -1;
            for (int n = 0; n < VirtualDreamMod.electricTriangle.Length; n++)
            {
                var currentTri = VirtualDreamMod.electricTriangle[n];
                if (currentTri == null)
                {
                    VirtualDreamMod.electricTriangle[n] = new ElectricTriangle()
                    {
                        position = position,
                        rotation = rotation,
                        size = size,
                        velocity = velocity,
                        cycle = cycle,
                        timeLeft = timeLeft,
                        //timeMax = timm
                        dustType = dustType ?? MyDustId.CyanBubble,
                        dustSclaer = dustScaler
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
                    currentTri.dustSclaer = dustScaler;
                    index = n;
                    break;
                }
            }
            return index;
        }
        public void Update()
        {
            if (timeLeft < 0) return;
            var d = Dust.NewDustPerfect(position + (timeLeft / (float)cycle % 1).ArrayLerp_Loop(new Vector2(0.8660254f, -0.5f), new Vector2(0, 1), new Vector2(-0.8660254f, -0.5f)).RotatedBy(rotation) * size, dustType, default, 0, Color.White, dustSclaer);
            d.noGravity = true;
            d.velocity = default;
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

