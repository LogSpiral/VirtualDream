using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.DataStructures;
using System.Collections.Generic;
using ReLogic.Content;
using VirtualDream.Contents.StarBound.Materials;
using ReLogic.Graphics;

namespace VirtualDream.Contents.StarBound.NPCs.Baron
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Baron : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName automatically assigned from localization files, but the commented line below is the normal approach.
            // DisplayName.SetDefault("Example Person");
            Main.npcFrameCount[Type] = 25; // The amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the npc that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            DisplayName.SetDefault("拜隆先生");
            //// Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
            //// NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
            //NPC.Happiness
            //    .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
            //    .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
            //    .SetBiomeAffection<ExampleSurfaceBiome>(AffectionLevel.Love) // Example Person likes the Example Surface Biome
            //    .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
            //    .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
            //    .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
            //    .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
            //; // < Mind the semicolon!
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("")
            });
        }

        // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
        // Returning false will allow you to manually draw your NPC
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            // This code slowly rotates the NPC in the bestiary
            // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
            {
                //drawModifiers.Rotation += (float)IllusionBoundMod.ModTime;
                //drawModifiers.Rotation = 0;
                // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }
            for (int n = 0; n < 4; n++)
                spriteBatch.DrawString(FontAssets.MouseText.Value, NPC.ai[n].ToString(), NPC.Center + new Vector2(0, -120 + 24 * n) - Main.screenPosition, Color.Red);
            return true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            //int num = NPC.life > 0 ? 1 : 5;

            //for (int k = 0; k < num; k++)
            //{
            //    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Sparkle>());
            //}
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        { // Requirements for the town NPC to spawn.
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                // Player has to have either an ExampleItem or an ExampleBlock in order for the NPC to spawn
                if (player.inventory.Any(item => item.type == ModContent.ItemType<AncientEssence>() && item.stack > 2000))
                {
                    return NPC.downedMoonlord;
                }
            }

            return false;
        }

        // Example Person needs a house built out of ExampleMod tiles. You can delete this whole method in your townNPC for the regular house conditions.
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            //int score = 0;
            //for (int x = left; x <= right; x++)
            //{
            //    for (int y = top; y <= bottom; y++)
            //    {
            //        int type = Main.tile[x, y].TileType;
            //        if (type == ModContent.TileType<ExampleBlock>() || type == ModContent.TileType<ExampleChair>() || type == ModContent.TileType<ExampleWorkbench>() || type == ModContent.TileType<ExampleBed>() || type == ModContent.TileType<ExampleDoorOpen>() || type == ModContent.TileType<ExampleDoorClosed>())
            //        {
            //            score++;
            //        }

            //        if (Main.tile[x, y].WallType == ModContent.WallType<ExampleWall>())
            //        {
            //            score++;
            //        }
            //    }
            //}

            //return score >= ((right - left) * (bottom - top)) / 2;
            return true;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new BaronProfile();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "拜隆先生",
                "一个古怪的电子人",
                "时之风吹拂的隐星",
                "星之河淹没的残风",
                "不再辉煌的不朽英雄",
                "遗忘与被遗忘的存在",
                "漫漫星河下的旅人",
                "过去与未来的见证者"
            };
        }

        public override void FindFrame(int frameHeight)
        {
            /*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
        }

        public override string GetChat()
        {
            //WeightedRandom<string> chat = new WeightedRandom<string>();
            //chat.Add("Yee");//上次咱们见面是什么时候来着...我这铁疙瘩脑袋多少有点生锈了。
            //return chat; // chat is implicitly cast to a string.
            if (Main.raining && Main.rand.NextBool(4))
            {
                ContentDecider = 1;
                return Main._shouldUseStormMusic ? "震撼。兴许我该收回我关于你这雨天比较温和的评价\n调侃。让暴风雨来得更猛烈些罢——非比喻的我不要。":"难熬。又是雨天...虽然说这里的雨天可比其他某些星球上的温和太多太多了\n但是这并不妨碍它会威胁我。";
            }
            var rand = Main.rand.Next(3);
            switch (rand)
            {
                case 0:
                    ContentDecider = 0;
                    return "平静。你不必对我的突然出现感到意外——你又不是第一次见到电子人——尽管和我很不一样，而且是半电子。\n尴尬。等等，我这话是不是说过了?";
                case 1:
                    ContentDecider = 3;
                    return "高兴。这片地方有太多新奇玩意值得探索。\n难过。我已经是一把老骨头了，没你们年轻人有活力了。";
                case 2:
                    ContentDecider = 2;
                    return "思考。你问我为什么要在几乎每句话最开始加上自己的感受？\n我不希望自己是个难被理解的家伙，仅此而已。\n或许我需要考虑一下改变说话风格，毕竟我的这习惯事与愿违了？";
            }
            return "";
        }
        /// <summary>
        /// 它决定当前讨论的话题，注释部分有话题表。
        /// </summary>
        public int ContentDecider
        {
            get => contentDecider;
            set { contentPlayer = 0; contentDecider = value; }
        }
        /// <summary>
        /// 它决定当前讨论的话题，注释部分有话题表。
        /// </summary>
        public int contentDecider;
        //0:基本介绍
        //1:关于雨天
        //2:关于用语的习惯
        //3:故事背景
        //4:
        //9:话题太"远"了


        /// <summary>
        /// 它决定当前话题的序数
        /// </summary>
        public int contentPlayer;

        public override void SetChatButtons(ref string button, ref string button2)
        {
            switch (contentDecider)
            {
                case 0:
                    switch (contentPlayer)
                    {
                        case 0:
                            button = "我是谁？";
                            break;
                        case 1:
                            button = "\"别讲谜语了，你到底是谁，你叫什么，你来这里出于什么目的？\"";
                            break;
                        case 2:
                            button = "我的过去？";
                            break;
                    }
                    break;
                case 1:
                    button = "";
                    break;
                case 2:
                    button = "";
                    break;
                case 3:
                    button = "";
                    break;
                case 4:
                    button = "";
                    break;
                case 5:
                    button = "";
                    break;
            }
            button2 = "你想听点别的东西？";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                var str = "";
                switch (contentPlayer)
                {
                    case 0:
                        str = "迷惑。我也说不太清楚\n有人说我是英雄，一位踏遍四方饱经沧桑的英雄\n有人说我是盗贼，掠尽了无数资源与财富\n有人说我是隐士，不问世事而自扫门前雪\n有人说我是旅人，在漫天繁星中漂泊。\n我不自我评价，一切好坏善恶由你们评说。";
                        break;
                    case 1:
                        str = "迷惑。我也说不太清楚\n有人说我是英雄，一位踏遍四方饱经沧桑的英雄\n有人说我是盗贼，掠尽了无数资源与财富\n有人说我是隐士，不问世事而自扫门前雪\n有人说我是旅人，在漫天繁星中漂泊。\n我不自我评价，一切好坏善恶由你们评说。";
                        break;
                    case 2:
                        str = "高兴。伙计，你要是愿意听我的故事详细到每一个细节，我乐意在这里讲上";
                        break;
                    default:
                        break;
                }
                contentPlayer++;
                Main.npcChatText = str;
            }
            else
            {
                contentDecider++;

            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
        }

        // Make this Town NPC teleport to the King and/or Queen statue when triggered.
        public override bool CanGoToStatue(bool toKingStatue) => true;

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        // todo: implement
        // public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
        // 	projType = ProjectileType<SparklingBall>();
        // 	attackDelay = 1;
        // }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }

    public class BaronProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            return ModContent.Request<Texture2D>("VirtualDream/Contents/StarBound/NPCs/Baron/Baron");
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("VirtualDream/Contents/StarBound/NPCs/Baron/Baron_Head");
    }
}