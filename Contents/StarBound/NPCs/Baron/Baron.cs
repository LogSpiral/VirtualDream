using System.Linq;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using ReLogic.Content;
using VirtualDream.Contents.StarBound.Materials;

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
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            // DisplayName.SetDefault("拜隆先生");
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
                new FlavorTextBestiaryInfoElement("也许你应该和他好好聊聊，这个简介里写不下那么多。")
            });
        }

        // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
        // Returning false will allow you to manually draw your NPC
        //public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        //{
        //    // This code slowly rotates the NPC in the bestiary
        //    // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
        //    if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
        //    {
        //        //drawModifiers.Rotation += (float)IllusionBoundMod.ModTime;
        //        //drawModifiers.Rotation = 0;
        //        // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
        //        NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
        //        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        //    }
        //    for (int n = 0; n < 4; n++)
        //        spriteBatch.DrawString(FontAssets.MouseText.Value, NPC.ai[n].ToString(), NPC.Center + new Vector2(0, -120 + 24 * n) - Main.screenPosition, Color.Red);
        //    return true;
        //}

        public override void HitEffect(NPC.HitInfo hit)
        {
            //int num = NPC.life > 0 ? 1 : 5;

            //for (int k = 0; k < num; k++)
            //{
            //    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Sparkle>());
            //}
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
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
            return [
                " 拜隆先生",
                " 一个古怪的电子人",
                " 时之风吹拂的隐星",
                " 星之河淹没的残风",
                " 不再辉煌的不朽英雄",
                " 遗忘与被遗忘的存在",
                " 漫漫星河下的旅人",
                " 过去与未来的见证者"
            ];
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.life < NPC.lifeMax && (int)Main.GameUpdateCount % 3 == 0) NPC.life++;
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
                return Main._shouldUseStormMusic ? "震撼。兴许我该收回我关于你这雨天比较温和的评价\n调侃。让暴风雨来得更猛烈些罢——非比喻的我不要。" : "难熬。又是雨天...虽然说这里的雨天可比其他某些星球上的温和太多太多了\n但是这并不妨碍它会威胁我。";
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
        //4:关于电子人
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
                        case 3:
                            button = "你还想听更多？";
                            break;
                        case 4:
                            button = "你想成为我的朋友？";
                            break;
                        case 5:
                            button = "为什么选择拒绝？";
                            break;
                        case 6:
                        case 7:
                            button = "继续。";
                            break;
                    }
                    break;
                case 1:
                    switch (contentPlayer)
                    {
                        case 0:
                            button = "我被雨淋到会怎样？";
                            break;
                        case 1:
                            button = "我淋雨会痛吗？";
                            break;
                        case 2:
                            button = "我为什么不给自己加一些防水措施？";
                            break;
                        case 3:
                            button = "我这么多年淋的雨没将我彻底消灭？";
                            break;
                    }
                    break;
                case 2:
                    switch (contentPlayer)
                    {
                        case 0:
                            button = "只有我会在每句话前面加点东西吗？";
                            break;
                        case 1:
                            button = "我为什么不试试不那样说话？";
                            break;
                    }
                    break;
                case 3:
                    switch (contentPlayer)
                    {
                        case 0:
                            button = "我是因为什么来到这里的？";
                            break;
                        case 1:
                            button = "名为泰拉大陆的这片土地，它特殊在哪里？";
                            break;
                        //case 2:
                        //    button = "为什么刚好是这个时候来呢？";
                        //    break;
                        case 2:
                            button = "有很多人知道这里突然能被观测到了吗？";
                            break;
                        case 3:
                            button = "还有什么特别的地方，只是不满足引力方程吗？";
                            break;
                        case 4:
                            button = "我在这里有什么研究成果？";
                            break;
                    }
                    break;
            }
            //if (contentPlayer == 0) button = "让我们继续这个话题";
            button2 = "你想听点别的东西？";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            var str = "yee";
            if (firstButton)
            {
                contentPlayer++;
            }
            else
            {
                ContentDecider = Main.rand.Next(new int[] { 0, 2, 3 });
                contentPlayer = 0;
            }
            switch (ContentDecider)
            {
                case 0:
                    switch (contentPlayer)
                    {
                        case 0:
                            str = "微笑。你想听点关于我自己的话题吗？";
                            break;
                        case 1:
                            str = "迷惑。我也说不太清楚\n有人说我是英雄，一位踏遍四方饱经沧桑的英雄\n有人说我是盗贼，掠尽了无数资源与财富\n有人说我是隐士，不问世事而自扫门前雪\n有人说我是旅人，在漫天繁星中漂泊。\n我不自我评价，一切好坏善恶由你们评说。";
                            break;
                        case 2:
                            str = "大笑。你不会把你的鼠标放在我身上吗，看见没。\n毫无疑问。我叫拜隆。\n疑惑。我是一位电子人，也许是个ai，也许有自己的意识？\n大笑。至少，很奇怪的，有让机器有意识的科技水平，却不能让机器防水\n思考。至于我来这的目的？你可以回想一下你自己的目的。毫无疑问，你也是个外来者。";
                            break;
                        case 3:
                            str = "高兴。伙计，你要是愿意听我的故事详细到每一个细节，我乐意在这里讲上个几年\n转折。不过...你肯定很忙对吧，而且有个家伙也让我没办法讲太多。";
                            break;
                        case 4:
                            str = "蜜汁微笑。兴许我们该换个话题了，你会慢慢了解我的，也许？";
                            break;
                        case 5:
                            str = "严肃。最好还是不了吧......\n身为一位电子人，我这辈子交过的朋友不会少。\n但是，但是，随着时间的推移，大伙们都生疏了，陌生了，冷漠了，渐行渐远了......";
                            break;
                        case 6:
                            str = "沧桑。还有不少，我看着他们仿佛是从土壤里探出头，\n托起初升的太阳，伸出稚嫩的枝芽，\n一不留神就已是枝繁叶茂了，一不留神就已是落英缤纷了，\n一不留神就已是果实累累，再一不留神，一不留神，就又成了漫天繁星中的一颗。";
                            break;
                        case 7:
                            str = "沉思。害怕的从来不是从来没有，\n而是曾经拥有，而眼睁睁地看着自己一点一点一个一个的失去，却什么也做不了......\n渐渐地，从害怕失去，到害怕拥有——我开始后悔和你说这么多了，\n兴许你又会是一个，一个个我将失去的朋友——伙计。";
                            break;
                        case 8:
                            str = "叹气。别在意我说的，我很荣幸能成为您的一位朋友...一位过客，一抹眼过云烟。";
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (contentPlayer)
                    {
                        case 0:
                            str = "惊讶。哦我的天哪，你是怎么找到这个对话的，如果你没使用代码或者ce之类的，快告诉阿汪你发现罢格了。";
                            break;
                        case 1:
                            str = "反感。如果我单纯是个铁疙瘩，还 只是 生点锈，但是显然，身为电子人，这雨对我造成的伤害大得多。";
                            break;
                        case 2:
                            str = "大笑。你要知道痛觉是为什么而存在的，伙计。\n痛觉负责告诉我们需要避免会造成危险的东西。\n我当然也有模拟痛觉的模块，甚至比你们的简单得多，传个参数的事情。\n淋雨对我来说兴许是致命的。";
                            break;
                        case 3:
                            str = "高兴。好问题。我已经是一把老骨头了，属于是能跑就行，鬼知道我替换某个零件下来会不会出问题。";
                            break;
                        case 4:
                            str = "坦白。实际上，我的表层有非常耐腐蚀的隔水涂层，里面也有各种防护措施，而且我的意识是在云端的。\n自豪。总之，用任何除了时间与空间以外的方法都不可能将我彻底消灭。\n大笑。其实是有个家伙编不下去了。";
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (contentPlayer)
                    {
                        case 0:
                            str = "微笑。你想听点关于我总是在句首表达我感受的话题吗？";
                            break;
                        case 1:
                            str = "否定。不是，所有电子人都是这样的语言风格。";
                            break;
                        case 2:
                            str = "大笑。这么多年都是这样，我习惯了，这也算我们共同的特色吧。\n你应该能从中多少感受到一丝滑稽与幽默？\n从某种角度上来讲，不这么说话都不像是个电子人了(笑\n刚刚我换了种风格，你猜猜我是在哪学的？";
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch (contentPlayer)
                    {
                        case 0:
                            str = "微笑。你想听点关于我为何而来到这里的话题吗？";
                            break;
                        case 1:
                            str = "高兴。不要问我为什么我自我介绍的时候不细说。\n你还没走出这颗星球看看对吧？宇宙中到处是这般充满生机的风景\n——让一位电子人说生机也许有点不太应景\n行星我见过太多太多了——比你迫害的史子还多\n——但是我们脚下的这片土地，是这么多年以来我见过的唯一的最特殊的。";
                            break;
                        case 2:
                            str = "认真。早就有人觉察到这附近其他天体的轨迹大不正常了\n——但是我们用各种或完善或假想的模型都对不上，除非那里有颗不存在的行星\n——最奇怪的就在于，就算是在前一两个月，这里都无法检测到，除了引力不存在别的东西，像暗物质一样。\n你肯定会好奇，为什么我刚好是这会过来，而不是早些时候之类。";
                            break;
                        case 3:
                            str = "失望。突然能观测到这不存在的行星，这固然是个不错的新闻\n——但我附近的绝大多数人并不在意这个\n——这东西的出现威胁不到他们的下午茶\n——或是让那下午茶更加香甜。";
                            break;
                        case 4:
                            str = "惊喜。特别的东西多了去了，这里许多现象是违反统一理论模型的\n——比如某面能发出Master Spark的三棱镜。\n这意味着，我们的理论体系依然是不完备的，统一理论并不统一，我们还能向着未知进发！\n我愿称之为 [c/FF6666:非统一魔法世界论] (划掉";
                            break;
                        case 5:
                            str = "思考。你这的科技树很——难以评价。但是只要你出不去，影响不到其他绝大多数地方\n——这就够了。统一理论在绝大多数情况下还是正确的。\n其实你要是好奇，我还是能让你略微看看外面的世界的......不过一个是怕你不能适应\n——另一个原因就是不是我不想而是某人做不到。";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            Main.npcChatText = str;
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