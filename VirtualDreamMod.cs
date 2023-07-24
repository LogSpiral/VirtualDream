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
using VirtualDream.Contents.StarBound.Weapons;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.GameContent.ItemDropRules;
using VirtualDream.Contents.StarBound.Materials;

namespace VirtualDream
{
    public class VirtualDreamMod : Mod
    {
        public static float GlowLight => VirtualDreamSystem.glowLight;
        public static double ModTime => LogSpiralLibrarySystem.ModTime;
        public static double ModTime2 => LogSpiralLibrarySystem.ModTime2;

        public static VirtualDreamMod Instance;
        public static Mod mod;
        public static float lightConst;
        public static ElectricTriangle[] electricTriangle = new ElectricTriangle[100];
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
        private Vector2 MoreMoreHeart(Terraria.UI.Chat.On_ChatManager.orig_DrawColorCodedString_SpriteBatch_DynamicSpriteFont_TextSnippetArray_Vector2_Color_float_Vector2_Vector2_refInt32_float_bool orig, SpriteBatch spriteBatch, ReLogic.Graphics.DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth, bool ignoreColors)
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
        private void MP3AudioTrack_ReadAheadPutAChunkIntoTheBuffer(Terraria.Audio.On_MP3AudioTrack.orig_ReadAheadPutAChunkIntoTheBuffer orig, MP3AudioTrack self)
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

        private int UnifiedRandom_Next_int_int(Terraria.Utilities.On_UnifiedRandom.orig_Next_int_int orig, Terraria.Utilities.UnifiedRandom self, int minValue, int maxValue)
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

        private int UnifiedRandom_Next_int(Terraria.Utilities.On_UnifiedRandom.orig_Next_int orig, Terraria.Utilities.UnifiedRandom self, int maxValue)
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
    /// <summary>
    /// 各种杂项
    /// <br>包括但不限于之前无间地狱那边的配方和奇怪滤镜们</br>
    /// </summary>
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
        public static double ModTime => LogSpiralLibrarySystem.ModTime;
        public static double ModTime2 => LogSpiralLibrarySystem.ModTime2;
        public static float glowLight;
        public override void UpdateUI(GameTime gameTime)
        {
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
            //ControlScreenShader("VirtualDream:Magnifying", MagnifyingGlassActive);
            //ControlScreenShader("VirtualDream:MagicalMagnifying", MagicalMagnifyingGlassActive);
            //ControlScreenShader("VirtualDream:Clever", CleverGlassActive);
            //ControlScreenShader("VirtualDream:Rainbow", RainBowGlassActive);
            //ControlScreenShader("VirtualDream:Contrast", ContrastGlassActive);
            //ControlScreenShader("VirtualDream:ContrastV2", ContrastUPGlassActiveV2);
            //ControlScreenShader("VirtualDream:Shikieiki", ShikieikiGlassActive);
            //ControlScreenShader("VirtualDream:InversPhase", InversPhaseGlassActive);
            //ControlScreenShader("VirtualDream:Zenith", ZenithGlassActive);
            //ControlScreenShader("VirtualDream:ContrastDown", ContrastDownGlassActive);
        }
        private void ControlScreenShader(string name, bool state)
        {
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
        }
    }

    public class StarboundSystem : ModSystem
    {
        public static int[] brokenWeaponTypes;

        public override void Load()
        {
            On_WorldGen.SpawnThingsFromPot += VirtualDream_BrokenWeaponFromPot;
            base.Load();
        }
        private void VirtualDream_BrokenWeaponFromPot(On_WorldGen.orig_SpawnThingsFromPot orig, int i, int j, int x2, int y2, int style)
        {
            if ((int)Player.GetClosestRollLuck(i, j, 200) == 0f)
            {
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, Main.rand.Next(brokenWeaponTypes));
                return;
            }
            orig.Invoke(i, j, x2, y2, style);
        }
        public override void PostSetupContent()
        {
            List<int> types = new();
            for (int n = 0; n < ItemLoader.ItemCount; n++)
            {
                var item = new Item(n);
                if (item.ModItem is StarboundWeaponBase weaponBase && weaponBase.State == WeaponState.Broken) types.Add(n);
            }
            brokenWeaponTypes = types.ToArray();
            base.PostSetupContent();
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            // Because world generation is like layering several images on top of each other, we need to do some steps between the original world generation steps.

            // Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
            // First, we find out which step "Shinies" is.
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Tile Cleanup"));

            if (ShiniesIndex != -1)
            {
                // Next, we insert our pass directly after the original "Shinies" pass.
                // ExampleOrePass is a class seen bellow
                tasks.Insert(ShiniesIndex + 1, new StarBoundBrokenWeaponPass());
            }
        }
        class StarBoundBrokenWeaponPass : GenPass
        {
            public StarBoundBrokenWeaponPass() : base("StarBoundBrokenWeaponFromChestAddition", 300)
            {

            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "添加...残破的武器!!";
                foreach (var chest in Main.chest)
                {
                    if (chest != null && chest.x < Main.maxTilesX && chest.y < Main.maxTilesY && WorldGen.genRand.NextBool(20))
                    {
                        chest.AddItemToShop(new Item(WorldGen.genRand.Next(brokenWeaponTypes)));
                    }
                }
            }
        }

        class StarBoundBrokenWeaponDrop : GlobalNPC
        {
            class MaterialDropCondition : IItemDropRuleCondition
            {
                Func<Player, bool> _condition;
                string _description;
                public MaterialDropCondition(Func<Player, bool> condition, string description)
                {
                    _condition = condition;
                    _description = description ?? "";
                }
                public bool CanDrop(DropAttemptInfo info) => NPC.downedMoonlord && !info.npc.friendly && info.npc.value > 0f && (_condition?.Invoke(info.player) ?? false);

                public bool CanShowItemDropInUI() => true;

                public string GetConditionDescription() => _description;
            }
            public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
            {
                #region 合成材料
                (Func<Player, bool>, int, string)[] datas = new (Func<Player, bool>, int, string)[]
                {
                            (player => player.ZoneSnow, ItemType<CryonicExtract>(),"雪地非常冷"),
                            (player => player.ZoneJungle, ItemType<VenomSample>(),"丛林是有毒的对吧"),
                            (player => player.ZoneUnderworldHeight, ItemType<ScorchedCore>(),"在这炼狱，你的心都要熔化了吧"),
                            (player => player.ZoneDesert || player.ZoneUndergroundDesert, ItemType<SharpenedClaw>(),"无垠沙海中的生物用锋利的爪撕碎猎物"),
                            (player => player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight, ItemType<HardenedCarapace>(),"岩层中的小动物们学会了如岩石般保护自己"),
                            (player => player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHallow,ItemType<Leather>(),"不是，为什么，皮革是找不到地方塞了吗"),
                            (player => player.ZoneSkyHeight,ItemType<PhaseMatter>(),"高处不胜寒，但是是月相物质"),
                            (player => player.ZoneMeteor,ItemType<StickOfRAM>(),"你这内存条是陨铁做的是吧"),
                            (player => player.ZoneDungeon,ItemType<StaticCell>(),"甚至比皮革那个还离谱"),
                            (player => player.ZonePurity,ItemType<LivingRoot>(),"最后一方净土上的生灵之根")
                };
                foreach (var data in datas)
                {
                    npcLoot.Add(ItemDropRule.ByCondition(new MaterialDropCondition(data.Item1, data.Item3), data.Item2, 20));
                }
                #endregion

                #region 破碎武器
                npcLoot.Add(ItemDropRule.OneFromOptions(npc.boss ? 30 : 1000, brokenWeaponTypes));
                #endregion
                base.ModifyNPCLoot(npc, npcLoot);
            }
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

