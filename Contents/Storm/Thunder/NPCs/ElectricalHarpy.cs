using System;
using Terraria;

using Terraria.ID;

using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.Storm.Thunder.NPCs
{
    // This ModNPC serves as an example of a complete AI example.
    public class ElectricalHarpy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("雷羽鸟妖");
            Main.npcFrameCount[npc.type] = 6;
        }

        private NPC npc => NPC;

        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 34;
            npc.aiStyle = -1;
            npc.damage = 45;
            npc.defense = 18;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.knockBackResist = 0.6f;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 300f;
            //NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
        //	return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.8f : 0;
        //}
        public override void AI()
        {
            npc.noGravity = true;
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                {
                    npc.velocity.X = 2f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                {
                    npc.velocity.X = -2f;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                {
                    npc.velocity.Y = 1f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                {
                    npc.velocity.Y = -1f;
                }
            }
            npc.TargetClosest(true);
            if (npc.direction == -1 && npc.velocity.X > -4f)
            {
                npc.velocity.X = npc.velocity.X - 0.1f;
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = npc.velocity.X - 0.1f;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + 0.05f;
                }
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = -4f;
                }
            }
            else if (npc.direction == 1 && npc.velocity.X < 4f)
            {
                npc.velocity.X = npc.velocity.X + 0.1f;
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - 0.05f;
                }
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = 4f;
                }
            }
            if (npc.directionY == -1 && npc.velocity.Y > -1.5)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.velocity.Y > 1.5)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                }
                else if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.03f;
                }
                if (npc.velocity.Y < -1.5)
                {
                    npc.velocity.Y = -1.5f;
                }
            }
            else if (npc.directionY == 1 && npc.velocity.Y < 1.5)
            {
                npc.velocity.Y = npc.velocity.Y + 0.04f;
                if (npc.velocity.Y < -1.5)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.03f;
                }
                if (npc.velocity.Y > 1.5)
                {
                    npc.velocity.Y = 1.5f;
                }
            }
            if (npc.wet)
            {
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                npc.TargetClosest(true);
            }
            npc.ai[1] += 1f;
            if (npc.ai[1] > 200f)
            {
                if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.ai[1] = 0f;
                }
                float num206 = 0.12f;
                float num207 = 0.07f;
                float num208 = 3f;
                float num209 = 1.25f;
                if (npc.ai[1] > 1000f)
                {
                    npc.ai[1] = 0f;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] > 0f)
                {
                    if (npc.velocity.Y < num209)
                    {
                        npc.velocity.Y = npc.velocity.Y + num207;
                    }
                }
                else if (npc.velocity.Y > -num209)
                {
                    npc.velocity.Y = npc.velocity.Y - num207;
                }
                if (npc.ai[2] < -150f || npc.ai[2] > 150f)
                {
                    if (npc.velocity.X < num208)
                    {
                        npc.velocity.X = npc.velocity.X + num206;
                    }
                }
                else if (npc.velocity.X > -num208)
                {
                    npc.velocity.X = npc.velocity.X - num206;
                }
                if (npc.ai[2] > 300f)
                {
                    npc.ai[2] = -300f;
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] % 30 == 0 && npc.ai[0] <= 121)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        float num210 = 6f;
                        Vector2 vector27 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num211 = Main.player[npc.target].position.X + Main.player[npc.target].width * 0.5f - vector27.X + Main.rand.Next(-100, 101);
                        float num212 = Main.player[npc.target].position.Y + Main.player[npc.target].height * 0.5f - vector27.Y + Main.rand.Next(-100, 101);
                        float num213 = (float)Math.Sqrt((double)(num211 * num211 + num212 * num212));
                        num213 = num210 / num213;
                        num211 *= num213;
                        num212 *= num213;
                        int num214 = npc.damage / 5;
                        int num215 = ProjectileType<ElectricalFeather>();
                        int num216 = Projectile.NewProjectile(npc.GetSource_FromAI(), vector27.X, vector27.Y, num211, num212, num215, 20, 0f, Main.myPlayer, (int)npc.ai[3], npc.whoAmI);
                        npc.ai[3]++;
                        npc.ai[3] %= 60;
                        Main.projectile[num216].timeLeft = 300;
                    }
                }
                else if (npc.ai[0] >= 180 + Main.rand.Next(400))
                {
                    npc.ai[0] = 0f;
                    Player player = FindTargetPlayer();
                    if (player != null)
                    {
                        bool flag = true;
                        Vector2 vec = npc.Center + new Vector2(Main.rand.NextFloat(256, 512), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
                        for (int n = -1; n < 2; n++)
                        {
                            for (int i = -1; i < 2; i++)
                            {
                                flag &= !Main.tile[(int)vec.X / 16 + n, (int)vec.Y / 16 + i].HasTile;
                            }
                        }
                        while (!flag)
                        {
                            vec = npc.Center + new Vector2(Main.rand.NextFloat(256, 512), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
                            flag = true;
                            for (int n = -1; n < 2; n++)
                            {
                                for (int i = -1; i < 2; i++)
                                {
                                    flag &= !Main.tile[(int)vec.X / 16 + n, (int)vec.Y / 16 + i].HasTile;
                                }
                            }
                        }
                        if (( Main.expertMode) && Main.rand.NextFloat(0f, 1f) <= (1 - (float)Math.Pow((float)npc.life / npc.lifeMax, 2)))//IllusionBoundMod.HarderActive || //TODO 雷羽
                        {
                            for (int n = 0; n < 16; n++)
                            {
                                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(64, 0).RotatedBy(MathHelper.Pi / 8 * n), new Vector2(0, 4).RotatedBy(MathHelper.Pi / 8 * n), ProjectileType<ElectricalFeather>(), 10, 1, Main.myPlayer, (int)npc.ai[3], npc.whoAmI);
                                npc.ai[3]++;
                                npc.ai[3] %= 60;
                            }
                        }

                        for (int n = 0; n < 36; n++)
                        {
                            Dust dust = Dust.NewDustPerfect(npc.Center, MyDustId.CyanBubble, new Vector2(2, 0).RotatedBy(MathHelper.TwoPi / 36 * n), newColor: Color.White);
                            dust.noGravity = true;
                        }
                        npc.Center = vec;
                        for (int n = 0; n < 36; n++)
                        {
                            Dust dust = Dust.NewDustPerfect(vec, MyDustId.PinkBubble, new Vector2(4, 0).RotatedBy(MathHelper.TwoPi / 36 * n), newColor: Color.White);
                            dust.noGravity = true;
                        }

                    }
                }
            }
            for (int num31 = npc.oldPos.Length - 1; num31 > 0; num31--)
            {
                npc.oldPos[num31] = npc.oldPos[num31 - 1];
                oldFrameY[num31] = oldFrameY[num31 - 1];
            }
            npc.oldPos[0] = npc.Center;
            oldFrameY[0] = npc.frame.Y;
        }
        private int[] oldFrameY = new int[10];
        private Player FindTargetPlayer()
        {
            Vector2 cen = npc.Center;
            Player target = null;
            float distanceMax = 1280f;
            foreach (Player player in Main.player)
            {
                float currentDistance = Vector2.Distance(cen, player.Center);
                if (currentDistance < distanceMax)
                {
                    distanceMax = currentDistance;
                    target = player;
                }
            }
            return target;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //if (Main.gamePaused) npc.UpdateNPC(npc.whoAmI);
            Texture2D npcTexture = TextureAssets.Npc[npc.type].Value;
            for (int k = 1; k < npc.oldPos.Length; k++)
            {
                spriteBatch.Draw(npcTexture, npc.oldPos[k] - Main.screenPosition - new Vector2(0, 22), new Rectangle(0, oldFrameY[k], 100, 86), new Color(255 - 28 * k, 0, 255 - 28 * k, 0), npc.rotation, new Vector2(50, 43), npc.scale - 0.1f * k, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //IllusionBoundMod.Contents.Storm.Thunder.NPCs
            spriteBatch.Draw(VirtualDreamMod.GetTexture("Contents/Storm/Thunder/NPCs/ElectricalHarpy_Glow"), npc.Center - new Vector2(0, 22) - Main.screenPosition, new Rectangle(0, npc.frame.Y, 100, 86), Color.White * VirtualDreamMod.GlowLight, npc.rotation, new Vector2(50, 43), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = 1;
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = npc.velocity.X * 0.1f;
            npc.frameCounter += 1.0;
            if (npc.frameCounter >= 6.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            int num5;
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(npc.position, npc.width, npc.height, MyDustId.Red, -npc.velocity.X * 1f, -npc.velocity.Y * 1f, 100, Color.White, 1.0f);
            }
            if (npc.life > 0)
            {
                int num725 = 0;
                while (num725 < hit.Damage / npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hit.HitDirection, -1f, 0, default, 1f);
                    num5 = num725;
                    num725 = num5 + 1;
                }
                return;
            }
            for (int num726 = 0; num726 < 50; num726 = num5 + 1)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hit.HitDirection, -2f, 0, default, 1f);
                num5 = num726;
            }
            //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ElectricalHarpyWing"), 1f);
            //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ElectricalHarpyWing1"), 1f);
            return;
        }
    }
    public class ElectricalFeather : ModProjectile
    {
        private Projectile projectile => Projectile;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("雷霆羽刃");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 24;
            projectile.timeLeft = 120;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.immuneTime = 15;
            target.immune = true;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Before != null)
            {
                for (int n = 0; n <= 10; n++)
                {
                    Dust dust = Dust.NewDustPerfect(Vector2.Lerp(projectile.Center, Before.Center, n / 10f), MyDustId.ElectricCyan, default, newColor: Color.White);
                    dust.noGravity = true;
                }
            }
            for (int num31 = projectile.oldPos.Length - 1; num31 > 0; num31--)
            {
                projectile.oldPos[num31] = projectile.oldPos[num31 - 1];
            }
            projectile.oldPos[0] = projectile.Center;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
            for (int k = 1; k < projectile.oldPos.Length; k++)
            {
                Main.spriteBatch.Draw(projectileTexture, projectile.oldPos[k] - Main.screenPosition, null, new Color(255 - 28 * k, 0, 255 - 28 * k, 0), projectile.rotation, new Vector2(7, 12), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
            }
            return true;
        }
        private Projectile Before
        {
            get
            {
                if ((int)projectile.ai[0] % 2 == 1)
                {
                    foreach (var proj in Main.projectile)
                    {
                        if ((int)proj.ai[0] + 1 == (int)projectile.ai[0] && proj.type == projectile.type && (int)proj.ai[1] == (int)projectile.ai[1])
                        {
                            return proj;
                        }
                    }
                }
                return null;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Before != null)
            {
                bool flag = false;
                for (int n = 0; n <= 10; n++)
                {
                    flag |= targetHitbox.Contains(Vector2.Lerp(projectile.Center, Before.Center, n / 10f).ToPoint());
                }
                return flag || targetHitbox.Intersects(projHitbox);
            }
            return targetHitbox.Intersects(projHitbox);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}