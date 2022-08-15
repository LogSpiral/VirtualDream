//using System;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using IllusionBoundMod.Utils;
//using Microsoft.Xna.Framework.Graphics;
//using System.Collections.Generic;

//namespace IllusionBoundMod.NPCs.ErchiusHorror
//{
//    //public static class ErchiusHorrorTools
//    //{
//    //	public static Effect LaserEffect;
//    //       static ErchiusHorrorTools()
//    //       {
//    //		LaserEffect = ModContent.g
//    //       }
//    //}
//    [AutoloadBossHead]
//    public class ErchiusHorror : ModNPC
//    {
//        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
//        {
//            SoundEngine.PlaySound(SoundID.Item, npc.Center, 27);
//        }
//        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
//        {
//            SoundEngine.PlaySound(SoundID.Item, npc.Center, 27);
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔");
//            Main.npcFrameCount[npc.type] = 7;
//        }

//        public override void SetDefaults()
//        {
//            npc.width = 300;
//            npc.height = 300;
//            npc.knockBackResist = 0f;
//            npc.aiStyle = -1;
//            npc.damage = 20;
//            //npc.DeathSound = SoundID.NPCDeath1;
//            npc.noGravity = true;
//            npc.noTileCollide = false;
//            npc.defense = 50;
//            npc.lifeMax = 200000;
//            npc.value = 10000f;
//            npc.friendly = false;
//            npc.boss = true;
//            music = ModLoader.GetMod("IllusionMusicMod") != null ? ModLoader.GetMod("IllusionMusicMod").GetSoundSlot(SoundType.Music, "Sounds/Music/CasketOfStar") : MusicID.Boss5;
//            musicPriority = MusicPriority.BossHigh;
//        }
//        public override bool PreAI()
//        {
//            if (targetPlayer == null)
//            {
//                npc.active = false;
//                npc.life -= 233333333;
//            }
//            return true;
//        }
//        private int Stage
//        {
//            get
//            {
//                if (npc.life >= npc.lifeMax / 2)
//                {
//                    return 0;
//                }
//                else if (npc.life >= npc.lifeMax / 4)
//                {
//                    return 1;
//                }
//                else if (npc.life >= npc.lifeMax / 100)
//                {
//                    return 2;
//                }
//                else
//                {
//                    return 3;
//                }
//            }
//        }
//        private int oldAiStyle;
//        private int[] indexOfTinyCrystal = new int[] { -1, -1, -1, -1 };
//        private int oldLife;
//        private int tinyCrystalTimer;
//        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
//        {
//            if (Stage == 1 && npc.ai[2] > 0)
//            {
//                damage /= 2;
//            }
//            else if (Stage == 1 && npc.ai[2] > 0)
//            {
//                damage = 0;
//            }
//        }
//        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
//        {
//            if (Stage == 1 && npc.ai[2] > 0)
//            {
//                damage /= 2;
//            }
//            else if (Stage == 2 && npc.ai[2] > 0)
//            {
//                damage = 0;
//            }
//        }
//        private void AttackMode_0()
//        {
//            //事过渡模式哦
//            //ai3 ： 当前所处模式(不止当前函数)

//            if ((npc.Center - targetPlayer.Center).Length() > 1200f && npc.ai[0] == 0)
//            {
//                Tools.LinerDust(npc.Center, targetPlayer.Center - new Vector2(0, 480), MyDustId.PinkBubble);
//                npc.Center = targetPlayer.Center - new Vector2(0, 480);
//                for (int n = 0; n < 72; n++)
//                {
//                    Dust.NewDustPerfect(npc.Center - (MathHelper.Pi / 72 * n).ToRotationVector2() * 64, MyDustId.PinkBubble, (MathHelper.Pi / 36 * n).ToRotationVector2() * 16, newColor: Color.White);
//                }
//                if (Stage != 0)
//                {
//                    NewTinyCrystal(true);
//                }
//            }
//            //else if (npc.ai[0] >= 60)
//            //{
//            //	npc.velocity.Y = (float)Math.Sin(npc.ai[0] / 30 * MathHelper.Pi);
//            //	npc.velocity.X = 0;
//            //}
//            npc.ai[0]++;
//            //if (npc.ai[0] % 12 == 0) 
//            //{
//            //	if (npc.ai[0] <= 60)
//            //	{
//            //		npc.frameCounter++;
//            //	}
//            //	else 
//            //	{
//            //		npc.frameCounter--;
//            //	}
//            //}
//            npc.frameCounter = (int)(-1f / 650f * npc.ai[0] * (npc.ai[0] - 120));
//            if ((int)npc.ai[0] == 40)
//            {
//                for (int n = 0; n < 18; n++)
//                {
//                    Projectile.NewProjectile(npc.Center, (MathHelper.TwoPi / 18 * n).ToRotationVector2() * 8, ModContent.ProjectileType<Fairy.LightJadeBullet>(), 25, 0, Main.myPlayer, 2);
//                }
//            }
//            if ((int)npc.ai[0] == 80)
//            {
//                for (int n = 0; n < 36; n++)
//                {
//                    Projectile.NewProjectile(npc.Center - (MathHelper.TwoPi / 36 * n).ToRotationVector2() * 64, (MathHelper.TwoPi / 36 * n).ToRotationVector2() * 16, ModContent.ProjectileType<Fairy.LightJadeBullet>(), 35, 0, Main.myPlayer, 2);
//                }
//            }
//            if (npc.ai[0] >= 120)
//            {
//                npc.ai[0] = 0;
//                npc.ai[3] = Main.rand.Next(1, 4 + Stage);
//                while ((int)npc.ai[3] == oldAiStyle)
//                {
//                    oldAiStyle = (int)npc.ai[3];
//                    npc.ai[3] = Main.rand.Next(1, 4 + Stage);
//                }
//            }

//        }
//        private void AttackMode_1()
//        {
//            //事旋转激光哦
//            if ((int)npc.ai[0] % 150 == 0)
//            {
//                //				if (npc.ai[1] + Main.rand.Next(4) >= 6) 
//                if (npc.ai[1] + Main.rand.Next(3) >= 4)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    oldAiStyle = 1;
//                    return;
//                }
//                npc.ai[0] = 1;
//                npc.ai[1]++;
//                Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorRotLaser>(), 55, 0, Main.myPlayer, npc.ai[1] % 2 == 1 ? MathHelper.Pi / 5 : 0, npc.ai[1] % 2 == 1 ? -1f : 1f);
//            }
//            //if ((int)npc.ai[0] % 10 == 0)
//            //{
//            //	for (int n = 0; n < 6; n++)
//            //	{
//            //		float r = (MathHelper.TwoPi / 150f * npc.ai[0] + MathHelper.Pi / 3 * n);
//            //		Projectile.NewProjectile(npc.Center + r.ToRotationVector2() * 80f, (r + MathHelper.PiOver2).ToRotationVector2() * 4f, ModContent.ProjectileType<Fairy.StarBullet>(), 25, 1, Main.myPlayer, 4, 1);
//            //	}
//            //}
//            if ((int)npc.ai[0] % (20 - 5 * Stage) == 0)
//            {
//                for (int n = 0; n < 6; n++)
//                {
//                    float r = (MathHelper.TwoPi / 150f * npc.ai[0] + MathHelper.Pi / 3 * n);
//                    Projectile.NewProjectile(npc.Center + r.ToRotationVector2() * 80f, (r + MathHelper.PiOver2).ToRotationVector2() * 4f, ModContent.ProjectileType<Fairy.StarBullet>(), 15, 0, Main.myPlayer, 4, 1);
//                }
//            }
//            npc.ai[0]++;
//        }
//        private void AttackMode_2()
//        {
//            //事聚合激光哦
//            if ((int)npc.ai[0] % 60 == 0)
//            {
//                //if (npc.ai[1] + Main.rand.Next(5) >= 10)
//                if (npc.ai[1] + Main.rand.Next(3) >= 4)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    oldAiStyle = 2;
//                    return;
//                }
//                npc.ai[0] = 1;
//                npc.ai[1]++;
//                Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center), ModContent.ProjectileType<ErchiusHorrorAggregateLaser>(), 55, 0, Main.myPlayer);
//            }
//            if ((int)npc.ai[0] % (30 - 10 * Stage) == 0)
//            {
//                for (int n = 0; n < 3; n++)
//                {
//                    Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center).RotatedBy(-MathHelper.Pi / 3 * (1 - npc.ai[0] / 60f)) * 4 * (n + 1), ModContent.ProjectileType<Fairy.ConeBullet>(), 25, 0, Main.myPlayer, 5, 1);
//                    Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center).RotatedBy(MathHelper.Pi / 3 * (1 - npc.ai[0] / 60f)) * 4 * (n + 1), ModContent.ProjectileType<Fairy.ConeBullet>(), 25, 0, Main.myPlayer, 5, 1);
//                }
//            }
//            //if ((int)npc.ai[0] % 10 == 0)
//            //{
//            //	for (int n = 0; n < 3; n++) 
//            //	{
//            //		Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center).RotatedBy(-MathHelper.Pi / 3 * (1 - npc.ai[0] / 60f)) * 4 * (n + 1), ModContent.ProjectileType<Fairy.ConeBullet>(), 35, 2, Main.myPlayer, 5, 1);
//            //		Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center).RotatedBy(MathHelper.Pi / 3 * (1 - npc.ai[0] / 60f)) * 4 * (n + 1), ModContent.ProjectileType<Fairy.ConeBullet>(), 35, 2, Main.myPlayer, 5, 1);
//            //	}
//            //}
//            npc.ai[0]++;
//        }
//        private void AttackMode_3()
//        {
//            //事旋转激光二号哦
//            if ((int)npc.ai[0] % 60 == 0)
//            {
//                //if (npc.ai[1] + Main.rand.Next(6) >= 9)
//                if (npc.ai[1] + Main.rand.Next(3) >= 4)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    oldAiStyle = 3;
//                    return;
//                }
//                npc.ai[0] = 1;
//                npc.ai[1]++;
//                float l = (npc.Center - targetPlayer.Center).Length();
//                if (l < 100f)
//                {
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorRotStopLaser>(), 55, 0, Main.myPlayer, (targetPlayer.Center - npc.Center).ToRotation() + MathHelper.Pi / 6, npc.ai[1] % 2 == 1 ? -1f : 1f);
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorRotStopLaser>(), 55, 0, Main.myPlayer, (targetPlayer.Center - npc.Center).ToRotation(), npc.ai[1] % 2 == 1 ? -1f : 1f);
//                }
//                else
//                {
//                    float a = -50 + (float)Math.Sqrt(4 * l * l - 30000) / 2;
//                    float r = (float)Math.Acos((a * a - l * l - 10000) / (-200 * l)) * (npc.ai[1] % 2 == 1 ? -1f : 1f);
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorRotStopLaser>(), 55, 0, Main.myPlayer, Tools.GetRad(targetPlayer.Center - npc.Center) + r + MathHelper.Pi / 6, npc.ai[1] % 2 == 1 ? -1f : 1f);//+ MathHelper.Pi / 12 * (npc.ai[1] % 2 == 1 ? -1f : 1f)
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorRotStopLaser>(), 55, 0, Main.myPlayer, Tools.GetRad(targetPlayer.Center - npc.Center) + r, npc.ai[1] % 2 == 1 ? -1f : 1f);//npc.Center - targetPlayer.Center
//                }
//            }
//            if ((int)npc.ai[0] % (30 - 5 * Stage) == 0)
//            {
//                for (int n = 0; n < 12; n++)
//                {
//                    Projectile.NewProjectile(npc.Center, (MathHelper.Pi / 6 * n + MathHelper.Pi / 360 * npc.ai[0]).ToRotationVector2() * 8f, ModContent.ProjectileType<Fairy.CrystalBullet>(), 15, 0, Main.myPlayer, 5, 1);
//                }
//            }
//            //if ((int)npc.ai[0] % 10 == 0)
//            //{
//            //	for (int n = 0; n < 12; n++)
//            //	{
//            //		Projectile.NewProjectile(npc.Center, (MathHelper.Pi / 6 * n + MathHelper.Pi / 360 * npc.ai[0]).ToRotationVector2() * 8f, ModContent.ProjectileType<Fairy.CrystalBullet>(), 25, 2, Main.myPlayer, 5, 1);
//            //	}
//            //}
//            npc.ai[0]++;
//        }
//        private void AttackMode_4()
//        {
//            //事平行四边形激光网哦
//            if ((int)npc.ai[0] % (250 - 50 * Stage) == 0)
//            {
//                float r = Main.rand.NextFloat(0, MathHelper.TwoPi);
//                for (int n = 0; n < 6; n++)
//                {
//                    Vector2 vec = (r + MathHelper.Pi / 3 * n).ToRotationVector2();
//                    Projectile.NewProjectile(npc.Center + vec * 200, new Vector2(vec.Y, -vec.X) * 16, ModContent.ProjectileType<Fairy.StarBullet>(), 35, 0, Main.myPlayer, 4, 2);
//                }
//            }
//            if ((int)npc.ai[0] % 300 == 0)
//            {
//                //if (npc.ai[1] + Main.rand.Next(3) >= 5)
//                if (npc.ai[1] + Main.rand.Next(2) >= 3)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    oldAiStyle = 4;
//                    return;
//                }
//                npc.ai[0] = 1;
//                npc.ai[1]++;
//                Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorMartixLaser>(), 55, 0, Main.myPlayer);
//            }
//            npc.ai[0]++;
//        }
//        private void AttackMode_5()
//        {
//            //事极限火花哦
//            if ((int)npc.ai[0] % 15 == 0)
//            {
//                for (int n = 0; n < 6; n++)
//                {
//                    Vector2 vec1 = (MathHelper.Pi / 3 * n * (1 + npc.ai[0] / 150f)).ToRotationVector2();
//                    Projectile.NewProjectile(npc.Center - vec1 * 200 * (npc.ai[0] / 150f), vec1 * (npc.ai[0] / 150f + 0.2f) * 16f, ModContent.ProjectileType<Fairy.HugeStarBullet>(), 45, 0, Main.myPlayer, 2, 1);
//                    Vector2 vec2 = (-MathHelper.Pi / 3 * n * (1 + npc.ai[0] / 150f)).ToRotationVector2();
//                    Projectile.NewProjectile(npc.Center - vec2 * 200 * (npc.ai[0] / 150f), vec2 * (npc.ai[0] / 150f + 0.2f) * 16f, ModContent.ProjectileType<Fairy.HugeStarBullet>(), 45, 0, Main.myPlayer, 2, 1);
//                }
//            }
//            if ((int)npc.ai[0] % 150 == 0)
//            {
//                //if (npc.ai[1] + Main.rand.Next(3) >= 5)
//                if (npc.ai[1] + Main.rand.Next(3) >= 4)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    oldAiStyle = 5;
//                    return;
//                }
//                npc.ai[0] = 1;
//                npc.ai[1]++;
//                Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center), ModContent.ProjectileType<ErchiusHorrorSparkLaser>(), 75, 0, Main.myPlayer, (targetPlayer.Center - npc.Center).ToRotation());
//            }
//            npc.ai[0]++;
//        }
//        private void NewTinyCrystal(bool LifeMax = false)
//        {
//            for (int n = 0; n < 4; n++)
//            {
//                if (indexOfTinyCrystal[n] == -1)
//                {
//                    Vector2 vec = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
//                    indexOfTinyCrystal[n] = NPC.NewNPC((int)vec.X, (int)vec.Y, ModContent.NPCType<TinyErchiusCrystal>(), ai1: npc.whoAmI);
//                    npc.ai[2]++;
//                }
//                else
//                {
//                    if (!Main.npc[indexOfTinyCrystal[n]].active || Main.npc[indexOfTinyCrystal[n]].type != ModContent.NPCType<TinyErchiusCrystal>())
//                    {
//                        Vector2 vec = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
//                        indexOfTinyCrystal[n] = NPC.NewNPC((int)vec.X, (int)vec.Y, ModContent.NPCType<TinyErchiusCrystal>(), ai1: npc.whoAmI);
//                        npc.ai[2]++;
//                    }
//                    else if (Main.npc[indexOfTinyCrystal[n]].life != Main.npc[indexOfTinyCrystal[n]].lifeMax && LifeMax)
//                    {
//                        Main.npc[indexOfTinyCrystal[n]].life = Main.npc[indexOfTinyCrystal[n]].lifeMax;
//                        Main.npc[indexOfTinyCrystal[n]].Center = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
//                    }
//                }
//                Main.npc[indexOfTinyCrystal[n]].Center = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
//            }
//        }
//        public override void AI()
//        {
//            if (targetPlayer == null || !targetPlayer.active)
//            {
//                return;
//            }
//            //targetPlayer.Hitbox = new Rectangle((int)targetPlayer.Center.X - 8, (int)targetPlayer.Center.Y - 8, 16, 16);
//            targetPlayer.noKnockback = true;
//            if (Stage < 3)
//            {
//                if (npc.life < npc.lifeMax / 4 && oldLife > npc.lifeMax / 4 && npc.life < oldLife)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    NewTinyCrystal();
//                    npc.ai[3] = 5;
//                    npc.life = npc.lifeMax / 4 - 1;
//                }
//                else if (npc.life < npc.lifeMax / 2 && oldLife > npc.lifeMax / 2 && npc.life < oldLife)
//                {
//                    npc.ai = new float[] { 0, 0, npc.ai[2], 0 };
//                    NewTinyCrystal(true);
//                    npc.ai[3] = 4;
//                    npc.life = npc.lifeMax / 2 - 1;
//                }
//                if (Stage != 0 && npc.ai[2] == 0)
//                {
//                    tinyCrystalTimer++;
//                }
//                if (tinyCrystalTimer >= (Main.expertMode ? 1200 : 1500))
//                {
//                    NewTinyCrystal();
//                    tinyCrystalTimer = 0;
//                }
//                switch ((int)npc.ai[3])
//                {
//                    case 0:
//                        {
//                            AttackMode_0();
//                            break;
//                        }
//                    case 1:
//                        {
//                            AttackMode_1();
//                            break;
//                        }
//                    case 2:
//                        {
//                            AttackMode_2();
//                            break;
//                        }
//                    case 3:
//                        {
//                            AttackMode_3();
//                            break;
//                        }
//                    case 4:
//                        {
//                            AttackMode_4();
//                            break;
//                        }
//                    case 5:
//                        {
//                            AttackMode_5();
//                            break;
//                        }
//                    default:
//                        {
//                            AttackMode_1();
//                            break;
//                        }
//                }
//            }
//            else
//            {
//                if (npc.life < npc.lifeMax / 100 && oldLife > npc.lifeMax / 100 && npc.life < oldLife)
//                {
//                    npc.ai = new float[4];
//                    npc.life = npc.lifeMax / 100 - 1;
//                }
//                npc.ai[0]++;
//                npc.ai[1]++;
//                if (npc.ai[0] % 20 == 0)
//                {
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorTinyLaser>(), 25, 0, Main.myPlayer);
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorTinyLaser>(), 25, 0, Main.myPlayer, (targetPlayer.Center - npc.Center).ToRotation(), 1);
//                }
//                if (npc.ai[0] % 40 == 0)
//                {
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorTinyLaser>(), 25, 0, Main.myPlayer, Main.rand.NextFloat(0, MathHelper.TwoPi), 1);
//                }
//                if (npc.ai[1] % 300 == 0)
//                {
//                    Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center), ModContent.ProjectileType<ErchiusHorrorSparkLaser>(), 75, 0, Main.myPlayer, (targetPlayer.Center - npc.Center).ToRotation());
//                }
//                npc.life -= 2;
//                npc.checkDead();
//                npc.friendly = true;
//            }
//            oldLife = npc.life;
//        }
//        public Player targetPlayer
//        {
//            get
//            {
//                Vector2 cen = npc.Center;
//                Player target = null;
//                float distanceMax = 4096f;
//                foreach (Player player in Main.player)
//                {
//                    float currentDistance = Vector2.Distance(cen, player.Center);
//                    if (currentDistance < distanceMax)
//                    {
//                        distanceMax = currentDistance;
//                        target = player;
//                    }
//                }
//                return target;
//            }
//        }
//        public override void NPCLoot()
//        {
//            for (int k = 1; k <= 6; k++)
//            {
//                Vector2 pos = npc.position + new Vector2(Main.rand.Next(npc.width - 8), Main.rand.Next(npc.height / 2));
//                Gore.NewGore(npc.Center, Tools.RandVec(16), mod.GetGoreSlot("Gores/ErchiusHorrorFragments_" + k), 2f);
//            }
//            SoundEngine.PlaySound(SoundID.Zombie, npc.Center, 104);
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            Rectangle rectangle = new Rectangle(0, 55 * (int)npc.frameCounter, 55, 55);
//            Rectangle rectangle1 = new Rectangle(0, Stage * 221, 221, 221);
//            if (Stage < 3)
//            {
//                spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition, rectangle, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(55, 55) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Eye"), npc.Center + Vector2.Normalize(targetPlayer.Center - npc.Center) * ((targetPlayer.Center - npc.Center).Length() * 7.5f / ((targetPlayer.Center - npc.Center).Length() + 1)) - Main.screenPosition, rectangle, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(55, 55) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Body"), npc.Center - Main.screenPosition, rectangle1, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Body_Glow"), npc.Center - Main.screenPosition, rectangle1, Color.White * IllusionBoundMod.GlowLight, 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Glow"), npc.Center - Main.screenPosition, new Rectangle((int)npc.ai[0] % 3 * 221, 0, 221, 221), Color.White * 0.2f * (int)(5 - npc.frameCounter), 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//            }
//            else
//            {
//                Vector2 offset = Tools.RandVec(32);
//                rectangle1 = new Rectangle(0, 442, 221, 221);
//                spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition, new Rectangle(0, 330, 55, 55), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(55, 55) * 0.5f, 2f, 0, 0);
//                //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Eye"), npc.Center + Vector2.Normalize(targetPlayer.Center - npc.Center) * ((targetPlayer.Center - npc.Center).Length() * 7.5f / ((targetPlayer.Center - npc.Center).Length() + 1)) - Main.screenPosition, rectangle, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(55, 55) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Body"), npc.Center - Main.screenPosition, rectangle1, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Body_Glow"), npc.Center - Main.screenPosition, rectangle1, Color.White * IllusionBoundMod.GlowLight, 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Glow"), npc.Center - Main.screenPosition, new Rectangle((int)npc.ai[0] % 3 * 221, 0, 221, 221), Color.White * 0.2f * (int)(5 - npc.frameCounter), 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                for (int n = 0; n < 4; n++)
//                {
//                    float r = Main.rand.NextFloat(0.125f, 0.625f);
//                    spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition + offset.RotatedBy(MathHelper.PiOver2 * n), new Rectangle(0, 330, 55, 55), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16) * r, 0, new Vector2(55, 55) * 0.5f, 2f, 0, 0);
//                    //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Eye"), npc.Center + offset.RotatedBy(MathHelper.PiOver2 * n) + Vector2.Normalize(targetPlayer.Center - npc.Center) * ((targetPlayer.Center - npc.Center).Length() * 7.5f / ((targetPlayer.Center - npc.Center).Length() + 1)) - Main.screenPosition, rectangle, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16) * r, 0, new Vector2(55, 55) * 0.5f, 2f, 0, 0);
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Body"), npc.Center + offset.RotatedBy(MathHelper.PiOver2 * n) - Main.screenPosition, rectangle1, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16) * r, 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Body_Glow"), npc.Center + offset.RotatedBy(MathHelper.PiOver2 * n) - Main.screenPosition, rectangle1, Color.White * IllusionBoundMod.GlowLight * r, 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusHorror_Glow"), npc.Center + offset.RotatedBy(MathHelper.PiOver2 * n) - Main.screenPosition, new Rectangle((int)npc.ai[0] % 3 * 221, 0, 221, 221), Color.White * r * 0.2f * (int)(5 - npc.frameCounter), 0, new Vector2(221, 221) * 0.5f, 2f, 0, 0);
//                }
//            }
//            return false;
//        }
//    }
//    public class TinyErchiusCrystal : ModNPC
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔小型水晶");
//        }
//        private NPC ownerNPC => Main.npc[(int)npc.ai[1]];
//        //private ErchiusHorror ownerModNPC => (ErchiusHorror)ownerNPC.modNPC;
//        public override void SetDefaults()
//        {
//            npc.width = 60;
//            npc.height = 60;
//            npc.knockBackResist = 0f;
//            npc.aiStyle = -1;
//            npc.damage = 20;
//            //npc.DeathSound = SoundID.NPCDeath1;
//            npc.noGravity = true;
//            npc.noTileCollide = false;
//            npc.defense = 35;
//            npc.lifeMax = 10000;
//            npc.friendly = false;
//        }
//        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
//        {
//            SoundEngine.PlaySound(SoundID.Item, npc.Center, 27);
//        }
//        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
//        {
//            SoundEngine.PlaySound(SoundID.Item, npc.Center, 27);
//        }
//        public override bool? CanBeHitByItem(Player player, Item item)
//        {
//            if (Main.expertMode)
//            {
//                return (player.Center - npc.Center).Length() < (ownerNPC.life >= ownerNPC.lifeMax / 4 ? 800 : 480);
//            }
//            return ownerNPC.life >= ownerNPC.lifeMax / 4 || (player.Center - npc.Center).Length() < 640;
//        }
//        public override bool? CanBeHitByProjectile(Projectile projectile)
//        {
//            if (!projectile.friendly)
//            {
//                return null;
//            }
//            if (Main.expertMode)
//            {
//                return (Main.player[projectile.owner].Center - npc.Center).Length() < (ownerNPC.life >= ownerNPC.lifeMax / 4 ? 800 : 480);
//            }
//            return ownerNPC.life >= ownerNPC.lifeMax / 4 || (Main.player[projectile.owner].Center - npc.Center).Length() < 640;
//        }
//        public override void NPCLoot()
//        {
//            for (int n = 0; n < 24; n++)
//            {
//                Dust.NewDustPerfect(npc.Center, MyDustId.PinkBubble, (MathHelper.Pi / 12 * n).ToRotationVector2() * 4, 0, Color.White);
//            }
//            for (int k = 0; k <= 4; k++)
//            {
//                Gore.NewGore(npc.Center, Tools.RandVec(4), mod.GetGoreSlot("Gores/TinyCrystalFragments"), 1f);
//            }
//            ownerNPC.ai[2]--;
//        }
//        public override void AI()
//        {
//            npc.ai[0]++;
//            if (ownerNPC.type != ModContent.NPCType<ErchiusHorror>() || !ownerNPC.active)
//            {
//                npc.active = false;
//                npc.life -= 495 * 514;
//            }
//            npc.timeLeft = 495;
//            if (Main.expertMode && (int)Main.time % 8 == 0)
//            {
//                npc.ai[2] += 4 - ownerNPC.ai[2];
//                if (npc.ai[2] >= 60 || npc.ai[3] > ownerNPC.ai[2])
//                {
//                    npc.ai[2] = 0;
//                    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<ErchiusHorrorTinyLaser>(), 25, 0, Main.myPlayer);
//                }
//                npc.ai[3] = ownerNPC.ai[2];
//            }
//        }
//        private float Length => (((ErchiusHorror)ownerNPC.modNPC).targetPlayer.Center - npc.Center).Length();
//        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition, null, Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16) * MathHelper.Clamp(npc.ai[0] / 60f, 0, 1), 0, new Vector2(16, 22), 1f, 0, 0);
//            if (Main.expertMode)
//            {
//                if (Length < (ownerNPC.life >= ownerNPC.lifeMax / 4 ? 800 : 480))
//                {
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/TinyErchiusCrystal_Glow"), npc.Center - Main.screenPosition, null, Color.White * IllusionBoundMod.GlowLight, 0, new Vector2(16, 22), 1f, 0, 0);
//                }
//            }
//            else
//            {
//                if (Length < 640 || ownerNPC.life >= ownerNPC.lifeMax / 4)
//                {
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/TinyErchiusCrystal_Glow"), npc.Center - Main.screenPosition, null, Color.White * IllusionBoundMod.GlowLight, 0, new Vector2(16, 22), 1f, 0, 0);
//                }
//            }
//            if (ownerNPC.life <= ownerNPC.lifeMax / 4)
//            {
//                npc.defense = 75;
//                for (int n = 0; n < 6; n++)
//                {
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/TinyErchiusCrystal_Shield"), npc.Center + (npc.ai[0] * MathHelper.TwoPi / 180 + MathHelper.Pi / 3 * n).ToRotationVector2() * 56 - Main.screenPosition, null, Color.White * MathHelper.Clamp(npc.ai[0] / 60f, 0, 1), npc.ai[0] * MathHelper.TwoPi / 180 + MathHelper.Pi / 3 * n - MathHelper.PiOver4, new Vector2(12, 16), 1f, 0, 0);
//                }
//            }
//            return false;
//        }
//    }
//    public class ErchiusHorrorRotLaser : ModProjectile
//    {
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            if (projectile.timeLeft > 180)
//            {
//                return false;
//            }
//            for (int n = 0; n < 6; n++)
//            {
//                float point = 0f;
//                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + projectile.velocity.RotatedBy(MathHelper.Pi / 3 * n) * 200f, projectile.velocity.RotatedBy(MathHelper.Pi / 3 * n) * 1600f + projectile.Center, 18 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)), ref point))
//                    return true;
//            }
//            return false;
//        }
//        //public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
//        //{
//        //	Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
//        //}
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            projectile.velocity = projectile.ai[0].ToRotationVector2();
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars4 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars5 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars6 = new List<CustomVertexInfo>();
//            for (int i = 200; i <= 1600; i += 10)
//            {
//                var factor = (i - 200f) / 1400f;
//                var normalDir1 = -projectile.velocity;
//                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
//                //float width = projectile.timeLeft <= 180 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)) : 4f;
//                float width = projectile.timeLeft <= 180 ? 16 * (float)Math.Pow(i + 1, 1 / 4f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)) : 4f;
//                float xValue = (factor / 100f - 0.5f) * 3 / 4 + 0.5f;
//                float zValue = projectile.timeLeft <= 180 ? 2 * factor * (1 - factor) + 0.125f : 0.25f;
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * width + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * -width + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 2) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 2) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                bars4.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 3) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars4.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 3) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                bars5.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 4) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars5.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 4) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                bars6.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 5) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars6.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 5) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//            }
//            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList3 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList4 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList5 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList6 = new List<CustomVertexInfo>();
//            if (bars1.Count > 2)
//            {
//                for (int i = 0; i < bars1.Count - 2; i += 2)
//                {
//                    triangleList1.Add(bars1[i]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 3]);
//                }
//                for (int i = 0; i < bars2.Count - 2; i += 2)
//                {
//                    triangleList2.Add(bars2[i]);
//                    triangleList2.Add(bars2[i + 2]);
//                    triangleList2.Add(bars2[i + 1]);
//                    triangleList2.Add(bars2[i + 1]);
//                    triangleList2.Add(bars2[i + 2]);
//                    triangleList2.Add(bars2[i + 3]);
//                }
//                for (int i = 0; i < bars3.Count - 2; i += 2)
//                {
//                    triangleList3.Add(bars3[i]);
//                    triangleList3.Add(bars3[i + 2]);
//                    triangleList3.Add(bars3[i + 1]);
//                    triangleList3.Add(bars3[i + 1]);
//                    triangleList3.Add(bars3[i + 2]);
//                    triangleList3.Add(bars3[i + 3]);
//                }
//                for (int i = 0; i < bars4.Count - 2; i += 2)
//                {
//                    triangleList4.Add(bars4[i]);
//                    triangleList4.Add(bars4[i + 2]);
//                    triangleList4.Add(bars4[i + 1]);
//                    triangleList4.Add(bars4[i + 1]);
//                    triangleList4.Add(bars4[i + 2]);
//                    triangleList4.Add(bars4[i + 3]);
//                }
//                for (int i = 0; i < bars5.Count - 2; i += 2)
//                {
//                    triangleList5.Add(bars5[i]);
//                    triangleList5.Add(bars5[i + 2]);
//                    triangleList5.Add(bars5[i + 1]);
//                    triangleList5.Add(bars5[i + 1]);
//                    triangleList5.Add(bars5[i + 2]);
//                    triangleList5.Add(bars5[i + 3]);
//                }
//                for (int i = 0; i < bars6.Count - 2; i += 2)
//                {
//                    triangleList6.Add(bars6[i]);
//                    triangleList6.Add(bars6[i + 2]);
//                    triangleList6.Add(bars6[i + 1]);
//                    triangleList6.Add(bars6[i + 1]);
//                    triangleList6.Add(bars6[i + 2]);
//                    triangleList6.Add(bars6[i + 3]);
//                }
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
//                float width = projectile.timeLeft <= 180 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)) : 4f;
//                for (int n = 0; n < 6; n++)
//                {
//                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity.RotatedBy(MathHelper.TwoPi / 6 * n) * 200 - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 2f * width / 24, 0, 0);
//                }
//                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
//                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
//                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
//                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
//                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
//                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(3 / 4f, 1, 0.5f).ToVector4());
//                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MaskColor[6];
//                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
//                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList2.ToArray(), 0, triangleList2.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList3.ToArray(), 0, triangleList3.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList4.ToArray(), 0, triangleList4.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList5.ToArray(), 0, triangleList5.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList6.ToArray(), 0, triangleList6.Count / 3);
//                Main.graphics.GraphicsDevice.RasterizerState = originalState;
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            }
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        //{
//        //	projectile.velocity = projectile.ai[0].ToRotationVector2();
//        //	spriteBatch.End();
//        //	spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//        //	List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> bars4 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> bars5 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> bars6 = new List<CustomVertexInfo>();
//        //	for (int i = 200; i <= 1600; i += 10)
//        //	{
//        //		var factor = (i - 200f) / 1400f;
//        //		var normalDir1 = -projectile.velocity;
//        //		normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
//        //		//float width = projectile.timeLeft <= 180 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)) : 4f;
//        //		float width = projectile.timeLeft <= 180 ? 16 * (float)Math.Pow(i + 1, 1 / 4f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)) : 4f;
//        //		float xValue = (factor / 100f - 0.5f) * 3 / 4 + 0.5f;
//        //		float zValue = projectile.timeLeft <= 180 ? 2 * factor * (1 - factor) + 0.125f : 0.25f;
//        //		bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * width + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//        //		bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * -width + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//        //		bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//        //		bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//        //		bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 2) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//        //		bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 2) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//        //		bars4.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 3) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//        //		bars4.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 3) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//        //		bars5.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 4) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//        //		bars5.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 4) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//        //		bars6.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 5) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//        //		bars6.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 5) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//        //	}
//        //	List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> triangleList2 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> triangleList3 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> triangleList4 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> triangleList5 = new List<CustomVertexInfo>();
//        //	List<CustomVertexInfo> triangleList6 = new List<CustomVertexInfo>();
//        //	if (bars1.Count > 2)
//        //	{
//        //		for (int i = 0; i < bars1.Count - 2; i += 2)
//        //		{
//        //			triangleList1.Add(bars1[i]);
//        //			triangleList1.Add(bars1[i + 2]);
//        //			triangleList1.Add(bars1[i + 1]);
//        //			triangleList1.Add(bars1[i + 1]);
//        //			triangleList1.Add(bars1[i + 2]);
//        //			triangleList1.Add(bars1[i + 3]);
//        //		}
//        //		for (int i = 0; i < bars2.Count - 2; i += 2)
//        //		{
//        //			triangleList2.Add(bars2[i]);
//        //			triangleList2.Add(bars2[i + 2]);
//        //			triangleList2.Add(bars2[i + 1]);
//        //			triangleList2.Add(bars2[i + 1]);
//        //			triangleList2.Add(bars2[i + 2]);
//        //			triangleList2.Add(bars2[i + 3]);
//        //		}
//        //		for (int i = 0; i < bars3.Count - 2; i += 2)
//        //		{
//        //			triangleList3.Add(bars3[i]);
//        //			triangleList3.Add(bars3[i + 2]);
//        //			triangleList3.Add(bars3[i + 1]);
//        //			triangleList3.Add(bars3[i + 1]);
//        //			triangleList3.Add(bars3[i + 2]);
//        //			triangleList3.Add(bars3[i + 3]);
//        //		}
//        //		for (int i = 0; i < bars4.Count - 2; i += 2)
//        //		{
//        //			triangleList4.Add(bars4[i]);
//        //			triangleList4.Add(bars4[i + 2]);
//        //			triangleList4.Add(bars4[i + 1]);
//        //			triangleList4.Add(bars4[i + 1]);
//        //			triangleList4.Add(bars4[i + 2]);
//        //			triangleList4.Add(bars4[i + 3]);
//        //		}
//        //		for (int i = 0; i < bars5.Count - 2; i += 2)
//        //		{
//        //			triangleList5.Add(bars5[i]);
//        //			triangleList5.Add(bars5[i + 2]);
//        //			triangleList5.Add(bars5[i + 1]);
//        //			triangleList5.Add(bars5[i + 1]);
//        //			triangleList5.Add(bars5[i + 2]);
//        //			triangleList5.Add(bars5[i + 3]);
//        //		}
//        //		for (int i = 0; i < bars6.Count - 2; i += 2)
//        //		{
//        //			triangleList6.Add(bars6[i]);
//        //			triangleList6.Add(bars6[i + 2]);
//        //			triangleList6.Add(bars6[i + 1]);
//        //			triangleList6.Add(bars6[i + 1]);
//        //			triangleList6.Add(bars6[i + 2]);
//        //			triangleList6.Add(bars6[i + 3]);
//        //		}
//        //		spriteBatch.End();
//        //		spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
//        //		float width = projectile.timeLeft <= 180 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 180f)) : 4f;
//        //		for (int n = 0; n < 6; n++) 
//        //		{
//        //			spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity.RotatedBy(MathHelper.TwoPi / 6 * n) * 200 - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 2f * width / 24, 0, 0);
//        //		}
//        //		RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
//        //		var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
//        //		var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
//        //		IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
//        //		IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
//        //		IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(3 / 4f, 1, 0.5f).ToVector4());
//        //		Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
//        //		Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
//        //		Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MaskColor[6];
//        //		Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
//        //		Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
//        //		Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
//        //		IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
//        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
//        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList2.ToArray(), 0, triangleList2.Count / 3);
//        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList3.ToArray(), 0, triangleList3.Count / 3);
//        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList4.ToArray(), 0, triangleList4.Count / 3);
//        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList5.ToArray(), 0, triangleList5.Count / 3);
//        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList6.ToArray(), 0, triangleList6.Count / 3);
//        //		Main.graphics.GraphicsDevice.RasterizerState = originalState;
//        //		spriteBatch.End();
//        //		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//        //	}
//        //	spriteBatch.End();
//        //	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//        //	return false;
//        //}
//        public override bool ShouldUpdatePosition()
//        {
//            return false;
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔旋转激光");
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.hostile = true;
//            projectile.friendly = false;
//            projectile.aiStyle = -1;
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.timeLeft = 210;
//            projectile.penetrate = -1;
//        }
//        public override void AI()
//        {
//            projectile.ai[0] += MathHelper.Pi / 180 / 3 * Math.Sign(projectile.ai[1]);
//            if (projectile.timeLeft == 180)
//            {
//                SoundEngine.PlaySound(SoundID.Item, projectile.Center, 12);
//            }
//        }
//    }
//    public class ErchiusHorrorAggregateLaser : ModProjectile
//    {
//        private Player targetPlayer
//        {
//            get
//            {
//                Vector2 cen = projectile.Center;
//                Player target = null;
//                float distanceMax = 4096f;
//                foreach (Player player in Main.player)
//                {
//                    float currentDistance = Vector2.Distance(cen, player.Center);
//                    if (currentDistance < distanceMax)
//                    {
//                        distanceMax = currentDistance;
//                        target = player;
//                    }
//                }
//                return target;
//            }
//        }
//        //public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
//        //{
//        //	Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
//        //}
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            if (projectile.ai[1] < 60 || targetPlayer == null)
//            {
//                return false;
//            }
//            float point = 0f;
//            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + projectile.velocity * 200f, projectile.velocity * 1600f + projectile.Center, projectile.ai[0] >= 1 ? 48 : 32, ref point))
//            {
//                return true;
//            }
//            if (projectile.ai[0] >= 1)
//            {
//                return false;
//            }
//            point = 0f;
//            //Vector2 unit = Vector2.Normalize(projectile.velocity).RotatedBy((1 - projectile.ai[0]) * MathHelper.TwoPi / 6 - MathHelper.PiOver2);
//            Vector2 unit = projectile.velocity.RotatedBy((1 - projectile.ai[0]) * MathHelper.TwoPi / 6);
//            //Vector2.Dot(new Vector2(-unit.Y, unit.X), targetPlayer.velocity) < 0
//            if (targetPlayer.velocity.Length() > 8f && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), unit * 200f + projectile.Center, unit * 1600f + projectile.Center, 16, ref point))
//            {
//                return true;
//            }
//            point = 0f;
//            unit = projectile.velocity.RotatedBy(-(1 - projectile.ai[0]) * MathHelper.TwoPi / 6);
//            if (targetPlayer.velocity.Length() > 8f && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), unit * 200f + projectile.Center, unit * 1600f + projectile.Center, 16, ref point))
//            {
//                return true;
//            }
//            return false;
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
//            for (int i = 200; i <= 1600; i += 10)
//            {
//                var factor = (i - 200f) / 1400f;
//                var normalDir = -projectile.velocity;
//                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
//                float width = projectile.ai[0] < 1 ? 32 : 48;
//                float xValue = (factor / 100f - 0.5f) * 3 / 4 + 0.5f;
//                float zValue = projectile.ai[1] >= 60 ? (projectile.ai[0] < 1 ? 2 * factor * (1 - factor) + 0.125f : 0.75f) : 0.125f;
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir * width + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir * -width + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                if (projectile.ai[0] < 1)
//                {
//                    width *= 0.5f;
//                    zValue *= 0.75f;
//                    bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir * width).RotatedBy((1 - projectile.ai[0]) * MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                    bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir * -width).RotatedBy((1 - projectile.ai[0]) * MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                    bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir * width).RotatedBy(-(1 - projectile.ai[0]) * MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                    bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir * -width).RotatedBy(-(1 - projectile.ai[0]) * MathHelper.TwoPi / 6) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                }
//            }
//            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList3 = new List<CustomVertexInfo>();
//            if (bars1.Count > 2)
//            {
//                for (int i = 0; i < bars1.Count - 2; i += 2)
//                {
//                    triangleList1.Add(bars1[i]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 3]);
//                }
//                if (projectile.ai[0] < 1)
//                {
//                    for (int i = 0; i < bars2.Count - 2; i += 2)
//                    {
//                        triangleList2.Add(bars2[i]);
//                        triangleList2.Add(bars2[i + 2]);
//                        triangleList2.Add(bars2[i + 1]);
//                        triangleList2.Add(bars2[i + 1]);
//                        triangleList2.Add(bars2[i + 2]);
//                        triangleList2.Add(bars2[i + 3]);
//                    }
//                    for (int i = 0; i < bars3.Count - 2; i += 2)
//                    {
//                        triangleList3.Add(bars3[i]);
//                        triangleList3.Add(bars3[i + 2]);
//                        triangleList3.Add(bars3[i + 1]);
//                        triangleList3.Add(bars3[i + 1]);
//                        triangleList3.Add(bars3[i + 2]);
//                        triangleList3.Add(bars3[i + 3]);
//                    }
//                }
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
//                float width = projectile.ai[0] < 1 ? 1.33f : 2f;
//                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity.RotatedBy((1 - projectile.ai[0]) * MathHelper.TwoPi / 6) * 200 - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, 0, new Vector2(16, 16), width / 2, 0, 0);
//                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity * 200 - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, 0, new Vector2(16, 16), width, 0, 0);
//                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity.RotatedBy(-(1 - projectile.ai[0]) * MathHelper.TwoPi / 6) * 200 - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, 0, new Vector2(16, 16), width / 2, 0, 0);
//                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
//                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
//                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
//                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
//                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
//                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(3 / 4f, 1, 0.5f).ToVector4());
//                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MaskColor[6];
//                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
//                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
//                if (projectile.ai[0] < 1)
//                {
//                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList2.ToArray(), 0, triangleList2.Count / 3);
//                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList3.ToArray(), 0, triangleList3.Count / 3);
//                }
//                Main.graphics.GraphicsDevice.RasterizerState = originalState;
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            }
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//        public override bool ShouldUpdatePosition()
//        {
//            return false;
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔聚合激光");
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.hostile = true;
//            projectile.aiStyle = -1;
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.timeLeft = 180;
//            projectile.friendly = false;
//            projectile.penetrate = -1;
//        }
//        public override void AI()
//        {
//            if (projectile.ai[1] >= 60)
//            {
//                if (projectile.ai[0] < 1)
//                    projectile.ai[0] += 1 / 60f;
//            }
//            else
//            {
//                projectile.ai[1]++;
//            }
//        }
//    }
//    public class ErchiusHorrorRotStopLaser : ModProjectile
//    {
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            if (projectile.timeLeft > 30)
//            {
//                return false;
//            }
//            float vR = projectile.velocity.ToRotation();
//            for (int n = 0; n < 6; n++)
//            {
//                float point = 0f;
//                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + projectile.velocity.RotatedBy(MathHelper.Pi / 3 * n) * 200f + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + n * MathHelper.Pi / 3).ToRotationVector2() * 200f, projectile.velocity.RotatedBy(MathHelper.Pi / 3 * n) * 1600f + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + n * MathHelper.Pi / 3).ToRotationVector2() * 200f, 18 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 30f)), ref point))
//                    return true;
//            }
//            return false;
//        }
//        //public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
//        //{
//        //	Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
//        //}
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            projectile.velocity = projectile.ai[0].ToRotationVector2();
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars4 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars5 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars6 = new List<CustomVertexInfo>();
//            for (int i = 200; i <= 1600; i += 10)
//            {
//                var factor = (i - 200f) / 1400f;
//                var normalDir1 = -projectile.velocity;
//                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
//                //float width = projectile.timeLeft <= 30 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 30f)) : 4f;
//                float width = projectile.timeLeft <= 30 ? 16 * (float)Math.Pow(i + 1, 1 / 4f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 30f)) : 4f;
//                float xValue = (factor / 100f - 0.5f) * 3 / 4 + 0.5f;
//                float zValue = projectile.timeLeft <= 30 ? 2 * factor * (1 - factor) + 0.125f : 0.25f;
//                float vR = projectile.velocity.ToRotation();
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * width + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * -width + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 0, zValue)));
//                bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 0, zValue)));
//                bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 2) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 2).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 2) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 2).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 0, zValue)));
//                bars4.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 3) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 3).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars4.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 3) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 3).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 0, zValue)));
//                bars5.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 4) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 4).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars5.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 4) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 4).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 0, zValue)));
//                bars6.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.TwoPi / 6 * 5) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 5).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars6.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.TwoPi / 6 * 5) + projectile.Center + (MathHelper.TwoPi / 3 * projectile.ai[1] + vR + MathHelper.TwoPi / 6 * 5).ToRotationVector2() * 200f, Color.White, new Vector3(xValue, 0, zValue)));
//            }
//            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList3 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList4 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList5 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList6 = new List<CustomVertexInfo>();
//            if (bars1.Count > 2)
//            {
//                for (int i = 0; i < bars1.Count - 2; i += 2)
//                {
//                    triangleList1.Add(bars1[i]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 3]);
//                }
//                for (int i = 0; i < bars2.Count - 2; i += 2)
//                {
//                    triangleList2.Add(bars2[i]);
//                    triangleList2.Add(bars2[i + 2]);
//                    triangleList2.Add(bars2[i + 1]);
//                    triangleList2.Add(bars2[i + 1]);
//                    triangleList2.Add(bars2[i + 2]);
//                    triangleList2.Add(bars2[i + 3]);
//                }
//                for (int i = 0; i < bars3.Count - 2; i += 2)
//                {
//                    triangleList3.Add(bars3[i]);
//                    triangleList3.Add(bars3[i + 2]);
//                    triangleList3.Add(bars3[i + 1]);
//                    triangleList3.Add(bars3[i + 1]);
//                    triangleList3.Add(bars3[i + 2]);
//                    triangleList3.Add(bars3[i + 3]);
//                }
//                for (int i = 0; i < bars4.Count - 2; i += 2)
//                {
//                    triangleList4.Add(bars4[i]);
//                    triangleList4.Add(bars4[i + 2]);
//                    triangleList4.Add(bars4[i + 1]);
//                    triangleList4.Add(bars4[i + 1]);
//                    triangleList4.Add(bars4[i + 2]);
//                    triangleList4.Add(bars4[i + 3]);
//                }
//                for (int i = 0; i < bars5.Count - 2; i += 2)
//                {
//                    triangleList5.Add(bars5[i]);
//                    triangleList5.Add(bars5[i + 2]);
//                    triangleList5.Add(bars5[i + 1]);
//                    triangleList5.Add(bars5[i + 1]);
//                    triangleList5.Add(bars5[i + 2]);
//                    triangleList5.Add(bars5[i + 3]);
//                }
//                for (int i = 0; i < bars6.Count - 2; i += 2)
//                {
//                    triangleList6.Add(bars6[i]);
//                    triangleList6.Add(bars6[i + 2]);
//                    triangleList6.Add(bars6[i + 1]);
//                    triangleList6.Add(bars6[i + 1]);
//                    triangleList6.Add(bars6[i + 2]);
//                    triangleList6.Add(bars6[i + 3]);
//                }
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
//                float width = projectile.timeLeft <= 30 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 30f)) : 8f;
//                for (int n = 0; n < 6; n++)
//                {
//                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity.RotatedBy(MathHelper.TwoPi / 6 * n) * 200 - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 2f * width / 24, 0, 0);
//                }
//                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
//                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
//                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
//                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
//                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
//                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(3 / 4f, 1, 0.5f).ToVector4());
//                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MaskColor[6];
//                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
//                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList2.ToArray(), 0, triangleList2.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList3.ToArray(), 0, triangleList3.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList4.ToArray(), 0, triangleList4.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList5.ToArray(), 0, triangleList5.Count / 3);
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList6.ToArray(), 0, triangleList6.Count / 3);
//                Main.graphics.GraphicsDevice.RasterizerState = originalState;
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            }
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//        public override bool ShouldUpdatePosition()
//        {
//            return false;
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔旋转激光");
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.hostile = true;
//            projectile.aiStyle = -1;
//            projectile.width = 1;
//            projectile.friendly = false;
//            projectile.height = 1;
//            projectile.timeLeft = 150;
//            projectile.penetrate = -1;
//        }
//        public override void AI()
//        {
//            if (projectile.timeLeft > 30)
//            {
//                projectile.ai[0] += MathHelper.Pi / 120 * projectile.ai[1];
//            }
//            if (projectile.timeLeft == 30)
//            {
//                SoundEngine.PlaySound(SoundID.Item, projectile.Center, 12);
//            }
//        }
//    }
//    public class ErchiusHorrorMartixLaser : ModProjectile
//    {
//        private Vector2 baseOfX;
//        private Vector2 baseOfY;
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔平行四边形激光");
//        }
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            if (projectile.timeLeft > 30)
//            {
//                return false;
//            }
//            Vector2 startVec = projectile.Center - 5 * baseOfY;
//            Vector2 endVec = projectile.Center + 5 * baseOfY;
//            for (int n = -5; n <= 5; n++)
//            {
//                float point = 0f;
//                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), startVec + n * baseOfX, endVec + n * baseOfX, 4, ref point))
//                    return true;
//            }
//            startVec = projectile.Center - 5 * baseOfX;
//            endVec = projectile.Center + 5 * baseOfX;
//            for (int n = -5; n <= 5; n++)
//            {
//                float point = 0f;
//                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), startVec + n * baseOfY, endVec + n * baseOfY, 4, ref point))
//                    return true;
//            }
//            return false;
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.hostile = true;
//            projectile.aiStyle = -1;
//            projectile.width = 1;
//            projectile.friendly = false;
//            projectile.height = 1;
//            projectile.timeLeft = 300;
//            projectile.penetrate = -1;
//        }
//        private Player targetPlayer
//        {
//            get
//            {
//                Vector2 cen = projectile.Center;
//                Player target = null;
//                float distanceMax = 4096f;
//                foreach (Player player in Main.player)
//                {
//                    float currentDistance = Vector2.Distance(cen, player.Center);
//                    if (currentDistance < distanceMax)
//                    {
//                        distanceMax = currentDistance;
//                        target = player;
//                    }
//                }
//                return target;
//            }
//        }
//        private Vector2 GetVec(Vector2 vec, float length)
//        {
//            return Vector2.Normalize(vec) * MathHelper.Clamp(vec.Length(), 0, length);
//        }
//        public override void AI()
//        {

//            if (projectile.timeLeft > 180)
//            {
//                baseOfX = GetVec(targetPlayer.Center - projectile.Center, 256f);
//            }
//            else if (projectile.timeLeft > 60)
//            {
//                baseOfY = GetVec(targetPlayer.Center - projectile.Center, 256f);
//            }
//            else if (projectile.timeLeft <= 30 && projectile.timeLeft % 30 == 0)
//            {
//                SoundEngine.PlaySound(SoundID.Item, projectile.Center, 13);
//            }
//            projectile.hostile = projectile.timeLeft <= 30;
//            //if (projectile.timeLeft <= 30 && projectile.timeLeft % 5 == 0) 
//            //{
//            //	Vector2 startVec = projectile.Center - 5 * baseOfY;
//            //	Vector2 endVec = projectile.Center + 5 * baseOfY;
//            //	for (int n = -5; n <= 5; n++)
//            //	{
//            //		Tools.LinerDust(startVec + n * baseOfX, endVec + n * baseOfX, MyDustId.PinkBubble, 8);
//            //	}
//            //	startVec = projectile.Center - 5 * baseOfX;
//            //	endVec = projectile.Center + 5 * baseOfX;
//            //	for (int n = -5; n <= 5; n++)
//            //	{
//            //		Tools.LinerDust(startVec + n * baseOfY, endVec + n * baseOfY, MyDustId.PinkBubble, 8);
//            //	}
//            //}
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            if (projectile.timeLeft > 30)
//            {
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusCrystalArrow"), baseOfX + projectile.Center - 24 * Vector2.Normalize(baseOfX) - Main.screenPosition, new Rectangle(0, 0, 32, 48), Color.White * MathHelper.Clamp((300 - projectile.timeLeft) / 30f, 0, 1), baseOfX.ToRotation() + MathHelper.PiOver2, new Vector2(16, 24), 1f, 0, 0);
//                spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusCrystalArrow"), (baseOfX - 48 * Vector2.Normalize(baseOfX)) * 0.5f + projectile.Center - Main.screenPosition, new Rectangle(0, 48, 32, 48), Color.White * MathHelper.Clamp((300 - projectile.timeLeft) / 30f, 0, 1), baseOfX.ToRotation() + MathHelper.PiOver2, new Vector2(16, 24), new Vector2(1, (baseOfX.Length() - 48) / 48), 0, 0);
//                if (projectile.timeLeft < 180)
//                {
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusCrystalArrow"), baseOfY + projectile.Center - 24 * Vector2.Normalize(baseOfY) - Main.screenPosition, new Rectangle(0, 0, 32, 48), Color.White * MathHelper.Clamp((180 - projectile.timeLeft) / 30f, 0, 1), baseOfY.ToRotation() + MathHelper.PiOver2, new Vector2(16, 24), 1f, 0, 0);
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/ErchiusCrystalArrow"), (baseOfY - 48 * Vector2.Normalize(baseOfY)) * 0.5f + projectile.Center - Main.screenPosition, new Rectangle(0, 48, 32, 48), Color.White * MathHelper.Clamp((180 - projectile.timeLeft) / 30f, 0, 1), baseOfY.ToRotation() + MathHelper.PiOver2, new Vector2(16, 24), new Vector2(1, (baseOfY.Length() - 48) / 48), 0, 0);
//                }
//            }
//            else
//            {
//                Rectangle rectangle = new Rectangle(0, projectile.timeLeft % 2 * 6, 10, 6);
//                for (int n = -5; n <= 5; n++)
//                {
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/MartixLaserTex"), projectile.Center + n * baseOfY - Main.screenPosition, rectangle, Color.White, baseOfX.ToRotation(), new Vector2(5, 3), new Vector2(baseOfX.Length() * 5 / 3, 2), 0, 0);
//                    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/ErchiusHorror/MartixLaserTex"), projectile.Center + n * baseOfX - Main.screenPosition, rectangle, Color.White, baseOfY.ToRotation(), new Vector2(5, 3), new Vector2(baseOfY.Length() * 5 / 3, 2), 0, 0);
//                }
//            }
//            return false;
//        }
//    }
//    public class ErchiusHorrorSparkLaser : ModProjectile
//    {
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            if (projectile.timeLeft > 300)
//            {
//                return false;
//            }
//            for (int n = -1; n <= 1; n++)
//            {
//                float point = 0f;
//                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + projectile.velocity.RotatedBy(MathHelper.Pi / 24 * n) * 144f, projectile.velocity.RotatedBy(MathHelper.Pi / 24 * n) * 1544f + projectile.Center, 96 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)), ref point))
//                    return true;
//            }
//            return false;
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            projectile.velocity = projectile.ai[0].ToRotationVector2();
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
//            for (int i = 144; i <= 1544; i += 10)
//            {
//                var factor = (i - 144f) / 1400f;
//                var normalDir1 = -projectile.velocity;
//                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
//                float width = projectile.timeLeft <= 300 ? 64 * (float)Math.Pow(i / 4, 1 / 2f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)) : 16f;
//                float xValue = (factor / 100f - 0.5f) * 3 / 4 + 0.5f;
//                float zValue = projectile.timeLeft <= 300 ? 1 - (float)Math.Pow(factor, 1 / 4f) : 0.25f;
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * width + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * -width + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                if (projectile.timeLeft <= 300)
//                {
//                    bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(MathHelper.Pi / 24) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                    bars2.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(MathHelper.Pi / 24) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                    bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * width).RotatedBy(-MathHelper.Pi / 24) + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                    bars3.Add(new CustomVertexInfo((projectile.velocity * i + normalDir1 * -width).RotatedBy(-MathHelper.Pi / 24) + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//                }
//            }
//            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList2 = new List<CustomVertexInfo>();
//            List<CustomVertexInfo> triangleList3 = new List<CustomVertexInfo>();
//            if (bars1.Count > 2)
//            {
//                for (int i = 0; i < bars1.Count - 2; i += 2)
//                {
//                    triangleList1.Add(bars1[i]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 3]);
//                }
//                if (projectile.timeLeft <= 300)
//                {
//                    for (int i = 0; i < bars2.Count - 2; i += 2)
//                    {
//                        triangleList2.Add(bars2[i]);
//                        triangleList2.Add(bars2[i + 2]);
//                        triangleList2.Add(bars2[i + 1]);
//                        triangleList2.Add(bars2[i + 1]);
//                        triangleList2.Add(bars2[i + 2]);
//                        triangleList2.Add(bars2[i + 3]);
//                    }
//                    for (int i = 0; i < bars3.Count - 2; i += 2)
//                    {
//                        triangleList3.Add(bars3[i]);
//                        triangleList3.Add(bars3[i + 2]);
//                        triangleList3.Add(bars3[i + 1]);
//                        triangleList3.Add(bars3[i + 1]);
//                        triangleList3.Add(bars3[i + 2]);
//                        triangleList3.Add(bars3[i + 3]);
//                    }
//                }
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
//                //float width = projectile.timeLeft <= 300 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)) : 4f;
//                //spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity * 200 - Main.screenPosition, new Rectangle(32, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 8f * width / 24, 0, 0);
//                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
//                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
//                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
//                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
//                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
//                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(3 / 4f, 1, 0.5f).ToVector4());
//                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MaskColor[6];
//                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
//                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
//                if (projectile.timeLeft <= 300)
//                {
//                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList2.ToArray(), 0, triangleList2.Count / 3);
//                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList3.ToArray(), 0, triangleList3.Count / 3);
//                }
//                Main.graphics.GraphicsDevice.RasterizerState = originalState;
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            }
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//        public override bool ShouldUpdatePosition()
//        {
//            return false;
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔极限火花");
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.hostile = true;
//            projectile.friendly = false;
//            projectile.aiStyle = -1;
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.timeLeft = 360;
//            projectile.penetrate = -1;
//        }
//        public override void AI()
//        {
//            if (projectile.timeLeft <= 300)
//            {
//                Vector2 vec = targetPlayer.Center - projectile.Center;
//                projectile.ai[0] -= MathHelper.Pi / 180 / 3 * Math.Sign(vec.X * projectile.velocity.Y - vec.Y * projectile.velocity.X);
//            }
//            if (projectile.timeLeft == 300)
//            {
//                SoundEngine.PlaySound(SoundID.Zombie, projectile.Center, 104);
//            }
//        }
//        public Player targetPlayer
//        {
//            get
//            {
//                Vector2 cen = projectile.Center;
//                Player target = null;
//                float distanceMax = 4096f;
//                foreach (Player player in Main.player)
//                {
//                    float currentDistance = Vector2.Distance(cen, player.Center);
//                    if (currentDistance < distanceMax)
//                    {
//                        distanceMax = currentDistance;
//                        target = player;
//                    }
//                }
//                return target;
//            }
//        }
//    }
//    public class ErchiusHorrorTinyLaser : ModProjectile
//    {
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            if (projectile.timeLeft > 30)
//            {
//                return false;
//            }
//            float point = 0f;
//            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + projectile.velocity * 20f, projectile.velocity * 1600f + projectile.Center, 8 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)), ref point);
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            projectile.velocity = projectile.ai[0].ToRotationVector2();
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
//            for (int i = 20; i <= 1600; i += 10)
//            {
//                var factor = (i - 20f) / 1580f;
//                var normalDir1 = -projectile.velocity;
//                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
//                float width = projectile.timeLeft <= 30 ? 16 * (float)Math.Pow(i / 4, 1 / 2f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 30f)) : 4f;
//                float xValue = (factor / 100f - 0.5f) * 3 / 4 + 0.5f;
//                float zValue = projectile.timeLeft <= 30 ? 1 - (float)Math.Pow(factor, 1 / 4f) : 0.25f;
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * width + projectile.Center, Color.White, new Vector3(xValue, 1 / 16f, zValue)));
//                bars1.Add(new CustomVertexInfo(projectile.velocity * i + normalDir1 * -width + projectile.Center, Color.White, new Vector3(xValue, 0, zValue)));
//            }
//            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
//            if (bars1.Count > 2)
//            {
//                for (int i = 0; i < bars1.Count - 2; i += 2)
//                {
//                    triangleList1.Add(bars1[i]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 1]);
//                    triangleList1.Add(bars1[i + 2]);
//                    triangleList1.Add(bars1[i + 3]);
//                }
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
//                //float width = projectile.timeLeft <= 300 ? 24 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)) : 4f;
//                //spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], projectile.Center + projectile.velocity * 200 - Main.screenPosition, new Rectangle(32, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 8f * width / 24, 0, 0);
//                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
//                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
//                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
//                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
//                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
//                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(3 / 4f, 1, 0.5f).ToVector4());
//                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
//                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MaskColor[6];
//                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
//                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
//                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
//                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
//                Main.graphics.GraphicsDevice.RasterizerState = originalState;
//                spriteBatch.End();
//                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            }
//            spriteBatch.End();
//            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//        public override bool ShouldUpdatePosition()
//        {
//            return false;
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源煞魔小型火花");
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.hostile = true;
//            projectile.friendly = false;
//            projectile.aiStyle = -1;
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.timeLeft = 90;
//            projectile.penetrate = -1;
//        }
//        public override void AI()
//        {
//            if (projectile.ai[1] == 1)
//            {
//                projectile.ai[1] = 0;
//                projectile.timeLeft = 45;
//            }
//            if (projectile.timeLeft >= 60)
//            {
//                Vector2 vec = targetPlayer.Center - projectile.Center;
//                projectile.ai[0] = vec.ToRotation();
//            }
//            if (projectile.timeLeft == 30)
//            {
//                SoundEngine.PlaySound(SoundID.Item, projectile.Center, 12);
//            }
//        }
//        public Player targetPlayer
//        {
//            get
//            {
//                Vector2 cen = projectile.Center;
//                Player target = null;
//                float distanceMax = 4096f;
//                foreach (Player player in Main.player)
//                {
//                    float currentDistance = Vector2.Distance(cen, player.Center);
//                    if (currentDistance < distanceMax)
//                    {
//                        distanceMax = currentDistance;
//                        target = player;
//                    }
//                }
//                return target;
//            }
//        }
//    }
//}