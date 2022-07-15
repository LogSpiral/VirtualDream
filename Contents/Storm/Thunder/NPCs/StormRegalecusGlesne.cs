using System;
//using VirtualDream.Items.Weapons.UniqueWeapon;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using Terraria.ID;
using Terraria.Utilities;

using static Terraria.ModLoader.ModContent;
using static VirtualDream.Utils.IllusionBoundExtensionMethods;

namespace VirtualDream.Contents.Storm.Thunder.NPCs
{
    public abstract class StormRegalecusGlesne : ModNPC
    {
        public NPC npc => NPC;

        public override string Texture => "VirtualDream/Contents/Storm/Thunder/NPCs/StormRegalecusGlesne_" + part;
        protected int part;
        //protected bool tailed;
        public static int[] StormRegalecusGlesneTypes = new int[] { NPCType<StormRegalecusGlesne_Head>(), NPCType<StormRegalecusGlesne_Body_1>(), NPCType<StormRegalecusGlesne_Body_2>(), NPCType<StormRegalecusGlesne_Body_3>(), NPCType<StormRegalecusGlesne_Tail>() };
        public override void SetStaticDefaults()
        {
            //风暴皇带鱼
            //电鳞皇影
            DisplayName.SetDefault("电鳞皇带鱼");
        }
        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.npcSlots = 5f;
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.damage = 80;
            npc.defense = 10;
            npc.lifeMax = 500;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath8;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
            npc.value = 10000f;
            npc.scale = 1f;
            npc.dontCountMe = part != 0;
        }
        protected virtual void SummonLighting()
        {

        }
        public override void AI()
        {
            bool flag = false;
            float num4 = 0.2f;
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || (flag && Main.player[npc.target].position.Y < Main.worldSurface * 16.0))
            {
                npc.TargetClosest(true);
            }
            if (Main.player[npc.target].dead || (flag && Main.player[npc.target].position.Y < Main.worldSurface * 16.0))
            {
                if (npc.timeLeft > 300)
                {
                    npc.timeLeft = 300;
                }
                if (flag)
                {
                    npc.velocity.Y = npc.velocity.Y + num4;
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if ((int)npc.ai[0] == 0 && part == 0)
                {
                    npc.ai[3] = npc.whoAmI;
                    npc.realLife = npc.whoAmI;
                    int num6 = npc.whoAmI;
                    for (int k = 0; k < 14; k++)
                    {
                        int num7 = NPCType<StormRegalecusGlesne_Body_1>();
                        if (k == 1 || k == 8)
                        {
                            num7 = NPCType<StormRegalecusGlesne_Body_1>();
                        }
                        else if (k == 11)
                        {
                            num7 = NPCType<StormRegalecusGlesne_Body_2>();
                        }
                        else if (k == 12)
                        {
                            num7 = NPCType<StormRegalecusGlesne_Body_3>();
                        }
                        else if (k == 13)
                        {
                            num7 = NPCType<StormRegalecusGlesne_Tail>();
                        }
                        int num8 = NPC.NewNPC(npc.GetSource_FromAI(), (int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), num7, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num8].ai[3] = npc.whoAmI;
                        Main.npc[num8].realLife = npc.whoAmI;
                        Main.npc[num8].ai[1] = num6;
                        Main.npc[num6].ai[0] = num8;
                        //NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num8, 0f, 0f, 0f, 0, 0, 0);
                        num6 = num8;
                    }
                    //tailed = true;
                }
                else if (!Main.npc[(int)npc.ai[1]].active || (npc.type != NPCType<StormRegalecusGlesne_Head>() && !StormRegalecusGlesneTypes.ContainsValue(Main.npc[(int)npc.ai[1]].type)))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }

            if (!Main.npc[(int)npc.ai[0]].active || (npc.type != NPCType<StormRegalecusGlesne_Tail>() && !StormRegalecusGlesneTypes.ContainsValue(Main.npc[(int)npc.ai[0]].type)))
            {
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.active = false;
                NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
            }
            int num29 = (int)(npc.position.X / 16f) - 1;
            int num30 = (int)((npc.position.X + npc.width) / 16f) + 2;
            int num31 = (int)(npc.position.Y / 16f) - 1;
            int num32 = (int)((npc.position.Y + npc.height) / 16f) + 2;
            if (num29 < 0)
            {
                num29 = 0;
            }
            if (num30 > Main.maxTilesX)
            {
                num30 = Main.maxTilesX;
            }
            if (num31 < 0)
            {
                num31 = 0;
            }
            if (num32 > Main.maxTilesY)
            {
                num32 = Main.maxTilesY;
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;
            }
            else if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = -1;
            }
            float num37 = 11f;
            float num38 = 0.25f;
            Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num40 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2;
            float num41 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2;
            num40 = (int)(num40 / 16f) * 16;
            num41 = (int)(num41 / 16f) * 16;
            vector2.X = (int)(vector2.X / 16f) * 16;
            vector2.Y = (int)(vector2.Y / 16f) * 16;
            num40 -= vector2.X;
            num41 -= vector2.Y;
            float num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num40 = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - vector2.X;
                    num41 = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - vector2.Y;
                }
                catch
                {
                }
                npc.rotation = (float)Math.Atan2((double)num41, (double)num40) + 1.57f;
                num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
                int num54 = 40;
                num53 = (num53 - num54) / num53;
                num40 *= num53;
                num41 *= num53;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num40;
                npc.position.Y = npc.position.Y + num41;
                if (num40 < 0f)
                {
                    npc.spriteDirection = 1;
                }
                else if (num40 > 0f)
                {
                    npc.spriteDirection = -1;
                }
            }
            else
            {
                if (part != 0 && npc.soundDelay == 0)
                {
                    float num55 = num53 / 40f;
                    if (num55 < 10f)
                    {
                        num55 = 10f;
                    }
                    if (num55 > 20f)
                    {
                        num55 = 20f;
                    }
                    npc.soundDelay = (int)num55;
                    SoundEngine.PlaySound(SoundID.Roar, npc.position);
                }
                num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
                float num56 = Math.Abs(num40);
                float num57 = Math.Abs(num41);
                float num58 = num37 / num53;
                num40 *= num58;
                num41 *= num58;
                bool flag6 = false;
                if (((npc.velocity.X > 0f && num40 < 0f) || (npc.velocity.X < 0f && num40 > 0f) || (npc.velocity.Y > 0f && num41 < 0f) || (npc.velocity.Y < 0f && num41 > 0f)) && Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > num38 / 2f && num53 < 300f)
                {
                    flag6 = true;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num37)
                    {
                        npc.velocity *= 1.1f;
                    }
                }
                if (npc.position.Y > Main.player[npc.target].position.Y || (double)(Main.player[npc.target].position.Y / 16f) > Main.worldSurface || Main.player[npc.target].dead)
                {
                    flag6 = true;
                    if (Math.Abs(npc.velocity.X) < num37 / 2f)
                    {
                        if (npc.velocity.X == 0f)
                        {
                            npc.velocity.X = npc.velocity.X - npc.direction;
                        }
                        npc.velocity.X = npc.velocity.X * 1.1f;
                    }
                    else if (npc.velocity.Y > -num37)
                    {
                        npc.velocity.Y = npc.velocity.Y - num38;
                    }
                }
                if (!flag6)
                {
                    if ((npc.velocity.X > 0f && num40 > 0f) || (npc.velocity.X < 0f && num40 < 0f) || (npc.velocity.Y > 0f && num41 > 0f) || (npc.velocity.Y < 0f && num41 < 0f))
                    {
                        if (npc.velocity.X < num40)
                        {
                            npc.velocity.X = npc.velocity.X + num38;
                        }
                        else if (npc.velocity.X > num40)
                        {
                            npc.velocity.X = npc.velocity.X - num38;
                        }
                        if (npc.velocity.Y < num41)
                        {
                            npc.velocity.Y = npc.velocity.Y + num38;
                        }
                        else if (npc.velocity.Y > num41)
                        {
                            npc.velocity.Y = npc.velocity.Y - num38;
                        }
                        if ((double)Math.Abs(num41) < (double)num37 * 0.2 && ((npc.velocity.X > 0f && num40 < 0f) || (npc.velocity.X < 0f && num40 > 0f)))
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num38 * 2f;
                            }
                            else
                            {
                                npc.velocity.Y = npc.velocity.Y - num38 * 2f;
                            }
                        }
                        if ((double)Math.Abs(num40) < (double)num37 * 0.2 && ((npc.velocity.Y > 0f && num41 < 0f) || (npc.velocity.Y < 0f && num41 > 0f)))
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num38 * 2f;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num38 * 2f;
                            }
                        }
                    }
                    else if (num56 > num57)
                    {
                        if (npc.velocity.X < num40)
                        {
                            npc.velocity.X = npc.velocity.X + num38 * 1.1f;
                        }
                        else if (npc.velocity.X > num40)
                        {
                            npc.velocity.X = npc.velocity.X - num38 * 1.1f;
                        }
                        if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num37 * 0.5)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num38;
                            }
                            else
                            {
                                npc.velocity.Y = npc.velocity.Y - num38;
                            }
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < num41)
                        {
                            npc.velocity.Y = npc.velocity.Y + num38 * 1.1f;
                        }
                        else if (npc.velocity.Y > num41)
                        {
                            npc.velocity.Y = npc.velocity.Y - num38 * 1.1f;
                        }
                        if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num37 * 0.5)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num38;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num38;
                            }
                        }
                    }
                }
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            }
            SummonLighting();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 vector2, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition, null, drawColor, npc.rotation, new Vector2(47, 78), 1f, npc.spriteDirection != 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow_1"), npc.Center - Main.screenPosition, null, Color.White * IllusionBoundMod.GlowLight, npc.rotation, new Vector2(47, 78), 1f, npc.spriteDirection != 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow_2"), npc.Center - Main.screenPosition, null, Color.White * IllusionBoundMod.GlowLight, npc.rotation, new Vector2(47, 78), 1f, npc.spriteDirection != 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
            spriteBatch.Begin();
            return false;
        }
    }
    public class StormRegalecusGlesne_Head : StormRegalecusGlesne
    {
        public static int MaxCount = 1;
        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
        //    int count = 0;
        //    foreach (var tar in Main.npc)
        //    {
        //        if (tar.type == NPCType<StormRegalecusGlesne_Head>())
        //        {
        //            count++;
        //        }
        //    }
        //    return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.4f * MathHelper.Clamp(((float)MaxCount - count) / MaxCount, 0, 1) : 0;
        //}
        private int Timer;
        protected override void SummonLighting()
        {
            Timer += (int)IllusionBoundMod.ModTime2 % 20 == 0 ? 1 : 0;
            if (Timer >= 15)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), Main.LocalPlayer.Center + new Vector2((float)GaussianRandom(0, .5 / 3.0, Main.rand) * 960, -560), default, ProjectileType<StormLighting>(), 50, 0, Main.myPlayer);
                Timer = 0;
            }
        }
        public StormRegalecusGlesne_Head()
        {
            part = 0;
        }
    }
    public class StormRegalecusGlesne_Body_1 : StormRegalecusGlesne
    {
        public StormRegalecusGlesne_Body_1()
        {
            part = 1;
        }
    }
    public class StormRegalecusGlesne_Body_2 : StormRegalecusGlesne
    {
        public StormRegalecusGlesne_Body_2()
        {
            part = 2;
        }
    }
    public class StormRegalecusGlesne_Body_3 : StormRegalecusGlesne
    {
        public StormRegalecusGlesne_Body_3()
        {
            part = 3;
        }
    }
    public class StormRegalecusGlesne_Tail : StormRegalecusGlesne
    {
        public StormRegalecusGlesne_Tail()
        {
            part = 4;
        }
    }
    public class StormLighting : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("风暴雷霆");
        }

        private Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            //projectile.friendly = true;
            projectile.hostile = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 30;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        private readonly MyLightTree tree = new MyLightTree();
        private Vector2[] Pos;
        public override void AI()
        {
            if (projectile.timeLeft == 30)
            {
                Pos = new Vector2[Main.rand.Next(8, 12)];
                Pos[0] = projectile.Center;
                for (int n = 1; n < Pos.Length; n++)
                {
                    float factor = 1 - (float)n / Pos.Length;
                    Pos[n] = Pos[n - 1] + new Vector2(Main.rand.Next(192, 256) * factor, 0).RotatedBy(GaussianRandom(0.5, 0.16 * factor, Main.rand) * MathHelper.Pi);
                }
                tree.Generate(Pos);
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int n = 0; n < Pos.Length - 1; n++)
            {
                for (float l = 0; l < 1; l += 0.025f)
                {
                    if (targetHitbox.Contains(Vector2.Lerp(Pos[n + 1], Pos[n], l).ToPoint()))
                    {
                        return true;
                    }
                }
            }
            return tree.Check(targetHitbox);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 2; i++)
            {
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, MyDustId.ElectricCyan, 0, 0, 100, Color.White, 0.3f);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.immune[projectile.owner] = 3;
            //if (StormRegalecusGlesne.StormRegalecusGlesneTypes.ContainsValue(target.type) || IllusionBoundMod.StormNPCType.ContainsValue(target.type))
            //{
            //    target.life += (int)(damage * 1.5f);
            //    target.life = target.life.ValueRange(0, target.lifeMax);
            //}
            for (int i = 0; i < 2; i++)
            {
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, MyDustId.ElectricCyan, 0, 0, 100, Color.White, 0.3f);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
        public override void PostDraw(Color lightColor)
        {
            tree.Draw(Main.spriteBatch, projectile.Center - Main.screenPosition, (Pos[1] - Pos[0]).ToRotation(), MathHelper.Clamp(-Math.Abs(projectile.timeLeft - 15) + 15, 0, 10) / 10f);
        }
    }
    public class MyLightTree
    {
        private class Node
        {
            public float rad, size, length;
            public List<Node> children;
            public Node(float rad, float size, float length)
            {
                this.rad = rad;
                this.size = size;
                this.length = length;
                this.children = new List<Node>();
            }
        };
        private Node root;
        private UnifiedRandom random;
        public MyLightTree(UnifiedRandom random)
        {
            root = null;
            this.random = random;
        }
        public MyLightTree()
        {
            root = null;
            this.random = Main.rand;
        }
        public List<Vector2> keyPoints;
        public void Generate(Vector2[] Pos)
        {
            root = new Node(0, 1f, (Pos[1] - Pos[0]).Length());
            keyPoints = new List<Vector2>
            {
                Pos[0]
            };
            root = _build(root, true, Pos, 2);
            AddKeyPoints(Pos[0], (Pos[1] - Pos[0]).ToRotation(), root);
        }
        //public int cnt;
        private Node _build(Node node, bool root, Vector2[] Pos = null, int index = 0, Vector2 posForUnRoot = default, float oldR = 0)
        {
            //
            //cnt++;
            if (root)
            {
                //keyPoints.Add(Pos[index - 1]);
                float rot = (Pos[index] - Pos[index - 1]).ToRotation() - (Pos[index - 1] - Pos[index - 2]).ToRotation();
                Node child1 = new Node(rot, node.size - 1f / Pos.Length, (Pos[index] - Pos[index - 1]).Length());
                if (index < Pos.Length - 1)
                {
                    node.children.Add(_build(child1, true, Pos: Pos, index: index + 1));
                }
                else
                {
                    //keyPoints.Add(Pos[index]);
                    node.children.Add(child1);
                }
                //for (int i = 0; i < 4; i++)//舍弃
                //{
                //    float r = random.NextFloat(-MathHelper.Pi / 8, MathHelper.Pi / 8);
                //    Node child2 = new Node(r + rot, rand() * node.size * 0.9f, node.length * 0.4f);
                //    node.children.Add(_build(child2, false, posForUnRoot: Pos[index]));
                //}
                //for (int i = 0; i < MathHelper.Clamp(random.Next(5), 1, 4); i++)
                //{
                //    float r = random.NextFloat(-MathHelper.Pi / 8, MathHelper.Pi / 8);
                //    Node child2 = new Node(r, rand() * node.size * 0.9f, node.length * 0.4f);
                //    node.children.Add(_build(child2, false, posForUnRoot: Pos[index - 1] + new Vector2(node.length * 0.4f, 0).RotatedBy(r + node.rad)));
                //}
                for (int i = 0; i < 2; i++)
                {
                    float r = random.NextFloat(-MathHelper.Pi / 8, MathHelper.Pi / 8);
                    Node child2 = new Node(r, rand() * node.size * 0.9f, node.length * 0.4f);
                    node.children.Add(_build(child2, false, posForUnRoot: Pos[index - 1] + new Vector2(node.length * 0.4f, 0).RotatedBy(r + node.rad)));
                }
            }
            else
            {
                //keyPoints.Add(posForUnRoot);
                //if ((node.size < 0.1f || node.length < 1 )&& random.NextFloat(0, 1) < 0.7f) return node;
                if ((node.size < 0.1f || node.length < 1) && random.NextFloat(0, 1) < 0.55f)
                {
                    return node;
                }

                for (int i = 0; i < 2; i++)
                {
                    float r = random.NextFloat(-MathHelper.Pi / 4, MathHelper.Pi / 4);
                    //Vector2 unit = (node.rad + r).ToRotationVector2();
                    Node child = new Node(r, rand() * node.size * 0.8f, node.length * 0.8f);
                    node.children.Add(_build(child, false, posForUnRoot: posForUnRoot + new Vector2(0.8f * node.length, 0).RotatedBy(r + oldR), oldR: r));
                }
            }
            return node;
        }
        private float rand()
        {
            double u = -2 * Math.Log(random.NextDouble());
            double v = 2 * Math.PI * random.NextDouble();
            return (float)Math.Max(0, Math.Sqrt(u) * Math.Cos(v) * 0.3 + 0.5);
        }
        public float rand(double mu, double sigma)
        {
            double u = -2 * Math.Log(random.NextDouble());
            double v = 2 * Math.PI * random.NextDouble();
            return (float)(Math.Sqrt(u) * Math.Cos(v) * sigma + mu);
        }
        public void Draw(SpriteBatch sb, Vector2 pos, float r, float Alpha)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            _draw(sb, pos, r, root, Color.Cyan * 0.4f * Alpha, 5f);
            _draw(sb, pos, r, root, Color.White * 0.6f * Alpha, 3f);
            sb.End();
            sb.Begin();
        }
        private void AddKeyPoints(Vector2 pos, float r, Node node)
        {
            Vector2 unit = (r + node.rad).ToRotationVector2();
            for (int i = 1; i < 5; i++)
            {
                Vector2 target = pos + unit * node.length * 0.25f * i;
                bool flag = true;
                for (int n = 0; n < keyPoints.Count; n++)
                {
                    flag &= (keyPoints[n] - target).Length() > 64;
                }
                if (flag)
                {
                    keyPoints.Add(target);
                }
            }
            foreach (var child in node.children)
            {
                AddKeyPoints(pos + unit * node.length, r + node.rad, child);
            }
        }
        private void _draw(SpriteBatch sb, Vector2 pos, float r, Node node, Color c, float factor)
        {
            Vector2 unit = (r + node.rad).ToRotationVector2();
            for (float i = 0; i <= node.length; i += 0.3f)
            {
                sb.Draw(TextureAssets.MagicPixel.Value, pos + unit * i, new Rectangle(0, 0, 1, 1), c, 0,
                    new Vector2(0.5f, 0.5f), Math.Max(node.size * factor, 0.3f), SpriteEffects.None, 0f);
            }

            foreach (var child in node.children)
            {
                _draw(sb, pos + unit * node.length, r + node.rad, child, c, factor);
            }
        }
        public bool Check(Rectangle hitbox)
        {
            foreach (var pt in keyPoints)
            {
                if ((hitbox.Center.ToVector2() - pt).Length() <= (hitbox.Width + hitbox.Height) / 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
