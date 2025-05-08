using LogSpiralLibrary;
using LogSpiralLibrary.CodeLibrary.DataStructures;
using LogSpiralLibrary.CodeLibrary.DataStructures.Drawing;
using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;

using static VirtualDream.Contents.StarBound.NPCs.Bosses.BigApe.BigApeTools;


namespace VirtualDream.Contents.StarBound.NPCs.Bosses.BigApe
{
    public static class BigApeTools
    {
        public const string ApePath = "Contents/StarBound/NPCs/Bosses/BigApe/";
        //public static BigApeEyeMode GetEyeGlow(this BigApeAttackMode attackMode)
        //{
        //    int i = (int)attackMode;
        //    return i < 2 ? 0 : (i > 3 ? BigApeEyeMode.Laser : BigApeEyeMode.Normal);
        //}
    }

    [AutoloadBossHead]
    public class BigApe : ModNPC
    {
        private NPC npc => NPC;
        public int[] indexOfProjector = new int[] { -1, -1, -1, -1 };
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("大猿人");
            NPCID.Sets.MustAlwaysDraw[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.width = 224;
            npc.height = 224;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.damage = 20;
            //npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.defense = 50;
            npc.lifeMax = 200000;
            npc.value = 10000f;
            npc.friendly = true;
            //npc.dontTakeDamageFromHostiles = true;
            npc.dontTakeDamage = true;
            npc.boss = true;
            //Music = MusicID.Boss3;
            //ModLoader.t
            Music = ModLoader.TryGetMod("VirtualDreamMusic", out Mod music) ? MusicLoader.GetMusicSlot(music, "Assets/Music/StrawberryCrisis") : MusicID.Boss3;
            //MusicPriority = MusicPriority.BossHigh;

        }
        public override bool PreAI()
        {
            if (targetPlayer == null)
            {
                npc.active = false;
                npc.life -= 233333333;
            }
            return true;
        }
        public BigApeAttackMode attackMode;
        //public BigApeEyeMode EyeMode => attackMode.GetEyeGlow();
        public int stage = 0;
        //private void NewProjector(bool LifeMax = false)
        //{
        //    for (int n = 0; n < 4; n++)
        //    {
        //        if (indexOfTinyCrystal[n] == -1)
        //        {
        //            Vector2 vec = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
        //            indexOfTinyCrystal[n] = NPC.NewNPC((int)vec.X, (int)vec.Y, ModContent.NPCType<TinyErchiusCrystal>(), ai1: npc.whoAmI);
        //            npc.ai[2]++;
        //        }
        //        else
        //        {
        //            if (!Main.npc[indexOfTinyCrystal[n]].active || Main.npc[indexOfTinyCrystal[n]].type != ModContent.NPCType<TinyErchiusCrystal>())
        //            {
        //                Vector2 vec = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
        //                indexOfTinyCrystal[n] = NPC.NewNPC((int)vec.X, (int)vec.Y, ModContent.NPCType<TinyErchiusCrystal>(), ai1: npc.whoAmI);
        //                npc.ai[2]++;
        //            }
        //            else if (Main.npc[indexOfTinyCrystal[n]].life != Main.npc[indexOfTinyCrystal[n]].lifeMax && LifeMax)
        //            {
        //                Main.npc[indexOfTinyCrystal[n]].life = Main.npc[indexOfTinyCrystal[n]].lifeMax;
        //                Main.npc[indexOfTinyCrystal[n]].Center = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
        //            }
        //        }
        //        Main.npc[indexOfTinyCrystal[n]].Center = npc.Center + (MathHelper.PiOver2 * n + MathHelper.PiOver4).ToRotationVector2() * 640;
        //    }
        //    Tile myTile = new Tile();
        //    (int, int) whoami = default;
        //    for (int n = 0; n < Main.maxTilesX; n++) 
        //    {
        //        for (int i = 0; i < Main.maxTilesY; i++)
        //        {
        //            if (myTile.GetHashCode() == Main.tile[n, i].GetHashCode()) whoami = (n, i);
        //        }
        //    }
        //}
        public int FrameCounter
        {
            get => (int)npc.frameCounter;
            set => npc.frameCounter = value;
        }
        public float[] randomChance = new float[] { 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        public override void AI()
        {
            if (targetPlayer == null || !targetPlayer.active)
            {
                return;
            }
            if (stage == 0)
            {
                PreAttackAniStage();
            }
            else if (stage < 5)
            {
                AttackStage();
                targetPlayer.wingTime = 10;
            }
            else
            {
                DeathStage();
            }
        }
        public void FrameChanger(int maxTime, bool IsLaser = true, int timeuse = -1)
        {
            var adder = IsLaser ? 0 : -4;
            if (timeuse == -1)
            {
                timeuse = (int)npc.ai[0];
            }

            if (timeuse > 30 && timeuse < maxTime - 30)
            {
                if (Main.GameUpdateCount % 10 == 0)
                {
                    FrameCounter = (FrameCounter == (10 + adder) ? 11 : 10) + adder;
                }
            }
            else if (timeuse > 20 && timeuse < maxTime - 20)
            {
                FrameCounter = 9 + adder;
            }
            else if (timeuse > 10 && timeuse < maxTime - 10)
            {
                FrameCounter = 8 + adder;
            }
            else
            {
                FrameCounter = 0;
            }
        }
        public void PreAttackAniStage()
        {
            //FrameCounter += (FrameCounter < 43 && Main.GameUpdateCount % 4 == 0) ? 1 : 0;
            if (Main.GameUpdateCount % 6 == 0)
            {
                if (FrameCounter == 0)
                {
                    var targets = new List<NPC>();
                    var type = ModContent.NPCType<BigApeProjector>();
                    for (int n = 0; n < 200; n++)
                    {
                        var target = Main.npc[n];
                        if (target.type == type && target.active && (int)target.ai[2] == npc.whoAmI)
                        {
                            targets.Add(target);
                        }
                    }
                    var m = targets.Count;
                    if (m < 4)
                    {
                        for (int n = 0; n < 4 - m; n++)
                        {
                            int i = NPC.NewNPC(npc.GetSource_FromAI(), Main.mouseX, Main.mouseY, ModContent.NPCType<BigApeProjector>(), 0, 0, 1, npc.whoAmI, (3 - n) * MathHelper.PiOver2);
                            for (int k = 0; k < 4; k++)
                            {
                                if (indexOfProjector[k] == -1)
                                {
                                    indexOfProjector[k] = i;
                                    break;
                                }
                            }
                        }
                        for (int n = 0; n < m; n++)
                        {
                            var target = targets[n];
                            target.ai[1] = 1;
                            target.ai[3] = n * MathHelper.PiOver2;
                        }
                    }
                    //IllusionBoundOnIlFunctions
                }
                if (FrameCounter < 43)
                {
                    FrameCounter++;
                }
                else { stage++; FrameCounter = 12; attackMode = BigApeAttackMode.AfterAttack; }
            }
        }

        private void RandomAttack()
        {
            //attackMode = BigApeAttackMode.Charge;
            //if (stage == 4) attackMode = BigApeAttackMode.Tesseract;
            //return;
            float randValue = Main.rand.NextFloat(10000);
            randValue /= 10000f;
            int modeCount = 1 + 2 * stage;
            float valueSum = 0;
            for (int n = 0; n < modeCount; n++)
            {
                valueSum += randomChance[n];
            }
            randValue *= valueSum;
            for (int n = modeCount - 1; n >= 0; n--)
            {
                valueSum -= randomChance[n];
                if (randValue > valueSum)
                {
                    randomChance[n] *= .5f;
                    attackMode = (BigApeAttackMode)(n + 2);
                    //Main.NewText(attackMode);
                    //Main.NewText(randomChance[n]);
                    //Main.NewText(modeCount);

                    //if (attackMode == BigApeAttackMode.Tesseract) attackMode = BigApeAttackMode.ProfessorStrawberry;
                    return;
                }
            }
            //randValue *= ValueOfAttackType[0] + ValueOfAttackType[1] + ValueOfAttackType[2] + ValueOfAttackType[3];
            //if (randValue < ValueOfAttackType[0])
            //{
            //    NormalAttackType = 0;
            //    ValueOfAttackType[0] *= .5f;
            //}
            //else if (randValue < ValueOfAttackType[0] + ValueOfAttackType[1])
            //{
            //    NormalAttackType = 1;
            //    ValueOfAttackType[1] *= .5f;
            //}
            //else if (randValue < ValueOfAttackType[0] + ValueOfAttackType[1] + ValueOfAttackType[2])
            //{
            //    NormalAttackType = 2;
            //    ValueOfAttackType[2] *= .5f;
            //}
            //else
            //{
            //    NormalAttackType = 3;
            //    ValueOfAttackType[3] *= .5f;
            //}
        }
        public void AttackStage()
        {
            //FrameCounter += Main.GameUpdateCount % 10 == 0 ? 1 : 0;
            ////switch (attackMode) 
            ////{
            ////	case BigApeAttackMode.None: FrameCounter %= 4;break;
            ////	case BigApeAttackMode.AfterAttack:if (FrameCounter >= 15) { FrameCounter = 0;attackMode = BigApeAttackMode.None; }break;
            ////}
            //if (attackMode == BigApeAttackMode.AfterAttack) 
            //{

            //}
            //else switch (EyeMode) 
            //{
            //	case 0: FrameCounter %= 4; break;
            //}
            //Main.NewText(attackMode);
            //Main.NewText(Main.rand.Next(2, 3 + 2 * stage));
            switch (attackMode)
            {
                case BigApeAttackMode.None:
                    {
                        FrameCounter += Main.GameUpdateCount % 10 == 0 ? 1 : 0;
                        FrameCounter %= 4;
                        npc.ai[0]++;
                        if (npc.ai[0] >= 80)
                        {
                            npc.ai[0] = 0;
                            //attackMode = (BigApeAttackMode)Main.rand.Next(2, 6);
                            /*attackMode = BigApeAttackMode.LaserFists;*///(BigApeAttackMode)Main.rand.Next(new int[] { 2, 3, 5 })//(BigApeAttackMode)Main.rand.Next(new int[] { 2, 3, 4, 5, 6, 8 })//(BigApeAttackMode)Main.rand.Next(2, 3 + 2 * stage)
                            RandomAttack();
                        }
                    }
                    break;
                case BigApeAttackMode.AfterAttack:
                    {
                        FrameCounter += Main.GameUpdateCount % 10 == 0 ? 1 : 0;
                        if (FrameCounter >= 15)
                        {
                            FrameCounter = 0;
                            attackMode = BigApeAttackMode.None;
                        }
                    }
                    break;
                case BigApeAttackMode.Charge: Charge(); break;
                case BigApeAttackMode.HomingMissiles: HomingMissiles(); break;
                case BigApeAttackMode.LaserFists: LaserFists(); break;
                case BigApeAttackMode.LaserDaggers: LaserDaggers(); break;
                case BigApeAttackMode.LaserSpray: LaserSpray(); break;
                case BigApeAttackMode.LaserCross: LaserCross(); break;
                case BigApeAttackMode.BulletHell: BulletHell(); break;
                case BigApeAttackMode.Tesseract: Tesseract(); break;
                case BigApeAttackMode.ProfessorStrawberry: ProfessorStrawberry(); break;
            }
            //if (stage == 4 && Main.expertMode && attackMode != BigApeAttackMode.ProfessorStrawberry && (int)attackMode > 1)
            //{
            //    foreach (var i in indexOfProjector)
            //    {
            //        if (i != -1 && Main.npc[i].ai[0] == 0)
            //        {
            //            Main.npc[i].ai[0] = 2;
            //        }
            //    }
            //}
        }
        public void DeathStage()
        {
            if (Main.GameUpdateCount % 6 == 0)
            {
                if (FrameCounter < 52)
                {
                    FrameCounter++;
                }
                else
                {
                    //npc.StrikeNPCNoInteraction(9999, 0f, 0);
                    npc.life -= int.MaxValue;
                    npc.checkDead();
                }
            }
            //Main.NewText(FrameCounter);
        }
        public override void OnKill()
        {
            Main.NewText("!!!你做到了!!!");

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            //name = "究极绝对" + name;
            //potionType = ModContent.ItemType<StrawberryCrystal>();
            //base.BossLoot(ref name, ref potionType);
        }
        public void Charge()
        {
            //randomChance.v0++;
            var modAi0 = (int)npc.ai[0] % 240;
            if (modAi0 == 0)
            {
                npc.ai[1] = targetPlayer.Center.X;
                npc.ai[2] = targetPlayer.Center.Y;
                npc.ai[3] = targetPlayer.Center.X > npc.Center.X ? 1 : -1;
            }
            npc.ai[0]++;
            var factor = modAi0 / 240f;
            //npc.Center = factor < 0.5f ? Vector2.Lerp(npc.Center, new Vector2(npc.ai[1] - 960 * npc.ai[3], npc.ai[2]), factor * 4 * factor) : Vector2.Lerp(new Vector2(npc.ai[1] - 960 * npc.ai[3], npc.ai[2]), new Vector2(npc.ai[1] + 960 * npc.ai[3], npc.ai[2]), factor * 2 - 1);
            npc.Center = factor < 0.5f ?
                Vector2.Lerp(npc.Center, new Vector2(npc.ai[1] - 800 * npc.ai[3], npc.ai[2]), (float)Math.Pow(factor * 2, 3)) :
                Vector2.Lerp(npc.Center, new Vector2(npc.ai[1] + 800 * npc.ai[3], npc.ai[2]), (float)Math.Pow(factor * 2 - 1, 3));
            if (npc.velocity != default)
            {
                npc.velocity *= 0.9f;
            }
            else if (npc.velocity.Length() < 1)
            {
                npc.velocity = default;
            }
            //if (factor >= 0.5f && (int)npc.ai[0] % 8 < 3)
            //foreach (int n in new int[] { -1, 1 })
            //{
            //    for (int i = 0; i < 4; i++) 
            //    {
            //        Projectile.NewProjectile(npc.Center + new Vector2(-66 * n, -18), (MathHelper.PiOver2 * i + npc.ai[0] / 60 * MathHelper.Pi * n).ToRotationVector2() * 4, ModContent.ProjectileType<LightPellet>(), 10, 0, Main.myPlayer);
            //    }
            //}
            FrameChanger(240, false, modAi0);
            //if (Math.Abs(0.5f - factor) < 0.4f)
            //{
            //    if (Main.GameUpdateCount % 10 == 0)
            //        FrameCounter = FrameCounter == 6 ? 7 : 6;
            //}
            //else if (Math.Abs(0.5f - factor) < 0.425f)
            //{
            //    FrameCounter = 5;
            //}
            //else if (Math.Abs(0.5f - factor) < 0.45f)
            //{
            //    FrameCounter = 4;
            //}
            //else
            //{
            //    FrameCounter = 0;
            //}
            if (modAi0 == 120)
            {
                int count = 0;
                foreach (var n in indexOfProjector)
                {
                    if (n == -1)
                    {
                        count++;
                    }
                }
                foreach (var n in indexOfProjector)
                {
                    if (n != -1)
                    {
                        var owner = Main.npc[n];
                        var vec = Vector2.Normalize(npc.Center - owner.Center);
                        Projectile.NewProjectileDirect(npc.GetSource_FromAI(), owner.Center, new Vector2(-vec.Y, vec.X), ModContent.ProjectileType<LightDagger_Charge>(), 100 + 25 * count, 0, Main.myPlayer, n, npc.ai[3]).frame = count;
                    }
                }
            }
            if ((int)npc.ai[0] / 240 >= 2 + stage / 2)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void HomingMissiles()
        {
            if (npc.ai[0] == 0)
            {
                npc.ai[2] = targetPlayer.Center.X;
                npc.ai[3] = targetPlayer.Center.Y - 320;
            }
            npc.ai[0]++;
            //var factor = npc.ai[0] / 240f;
            //FrameChanger(240, false);
            //if (factor <= 0.5f)
            //{
            //    npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[2], npc.ai[3]), factor * 2);
            //}
            //NPCs.ErchiusHorror.ErchiusHorror
            FrameChanger(900, false);
            if ((int)npc.ai[0] <= 120f)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[2], npc.ai[3]), npc.ai[0] / 120f);
            }
            npc.ai[1] += (float)Math.Log10(npc.ai[0] / 2 * 5) / ((Main.expertMode ? 50f : 70f) - 5 * stage);
            while ((int)npc.ai[1] > 0)
            {
                npc.ai[1] -= 0.5f;
                var ind = Main.rand.Next(4);
                while (indexOfProjector[ind] == -1)
                {
                    ind = Main.rand.Next(4);
                }
                var owner = Main.npc[indexOfProjector[ind]];
                var vec = Vector2.Normalize(targetPlayer.Center - owner.Center);
                SoundEngine.PlaySound(SoundID.Item100, owner.Center);

                var p = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), owner.Center, vec.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 12, MathHelper.Pi / 12)) * 32, ModContent.ProjectileType<ApeBossMissile>(), 50, 0, Main.myPlayer);
                p.frameCounter = Main.rand.Next(2);
                if (p.frameCounter > 0)
                {
                    var vec1 = targetPlayer.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(64);
                    p.ai[0] = vec1.X;
                    p.ai[1] = vec1.Y;
                }
            }
            if (npc.ai[0] >= 900)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void LaserFists()
        {
            FrameChanger(1020, false);
            //if ((int)npc.ai[0] == 60 || (int)npc.ai[0] == 300 || (int)npc.ai[0] == 600)
            //{
            //    Projectile.NewProjectile(npc.Center, Vector2.Normalize(targetPlayer.Center - npc.Center) * 2, ModContent.ProjectileType<EnergyFist>(), 120, 4, Main.myPlayer, 1);
            //}
            int type = ModContent.ProjectileType<EnergyFist>();
            int counter = (int)npc.ai[0] - 120;
            if (counter == -120)
            {
                npc.ai[1] = targetPlayer.Center.X;
                npc.ai[2] = targetPlayer.Center.Y - 320;
            }
            else if (counter < 0)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[1], npc.ai[2]), npc.ai[0] / 120f);
            }
            else
            {
                if (counter < 300)
                {
                    if (counter % (28 - 4 * stage) == 0)
                    {
                        var matrix = new Matrix(0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        var vec = Vector2.Lerp(new Vector2(960, 560), new Vector2(960, -560), npc.ai[0] % 150 / 150);
                        var vec2 = new Vector2(-8, 0);
                        for (int n = 0; n < 4; n++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + vec, vec2, type, 30, 2, Main.myPlayer);
                            vec = vec.ApplyMatrix(matrix);
                            vec2 = vec2.ApplyMatrix(matrix);
                        }
                    }
                }
                else if (counter < 600)
                {
                    if (counter % (16 - stage) == 0)
                    {
                        for (int n = 0; n < (3 + stage / 2); n++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560)), new Vector2(Main.rand.NextFloat(4, 8), 6) * 1.5f, type, 35, 0, Main.myPlayer);
                        }
                    }

                }
                else
                {
                    var c = stage == 1 ? 80 : 80 - stage * 10;
                    if (counter == 900 - c)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            var vec = new Vector2(-32, 0).RotatedBy(MathHelper.PiOver4 * (1 + 2 * n));
                            Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center - vec * 90, vec, type, 50, 2, Main.myPlayer);
                        }
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(960, -320) - new Vector2(144), new Vector2(-1, 0), type, 50, 5, Main.myPlayer, 3);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(-960, 320) - new Vector2(144), new Vector2(1, 0), type, 50, 5, Main.myPlayer, 3);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(320, 960) - new Vector2(144), new Vector2(0, -1), type, 50, 5, Main.myPlayer, 3);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(-320, -960) - new Vector2(144), new Vector2(0, 1), type, 50, 5, Main.myPlayer, 3);
                    }
                    else if (counter % c == 0 && (counter < 900 - 2 * c || stage == 2 || stage == 3))
                    {

                        //var vec = targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560));
                        //Projectile.NewProjectile(vec, Vector2.Normalize(targetPlayer.Center - vec) * 3f + Main.rand.NextVector2Unit(), type, 120, 0, Main.myPlayer, 3);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(960, 0) - new Vector2(144), new Vector2(-1, 0), type, 50, 5, Main.myPlayer, 3);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(-960, 0) - new Vector2(144), new Vector2(1, 0), type, 50, 5, Main.myPlayer, 3);
                        if (counter >= 720)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(0, 960) - new Vector2(144), new Vector2(0, -1), type, 50, 5, Main.myPlayer, 3);
                            Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(0, -960) - new Vector2(144), new Vector2(0, 1), type, 50, 5, Main.myPlayer, 3);
                        }

                    }
                }
                if (!Terraria.Utils.CenteredRectangle(npc.Center, new Vector2(1920)).Contains(targetPlayer.Center.ToPoint()) && counter % 60 == 0)
                {
                    Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(960, 0) - new Vector2(144), new Vector2(-1, 0), type, 50, 5, Main.myPlayer, 3);
                    Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(-960, 0) - new Vector2(144), new Vector2(1, 0), type, 50, 5, Main.myPlayer, 3);
                    Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(0, 960) - new Vector2(144), new Vector2(0, -1), type, 50, 5, Main.myPlayer, 3);
                    Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + new Vector2(0, -960) - new Vector2(144), new Vector2(0, 1), type, 50, 5, Main.myPlayer, 3);
                }
                if (counter % 300 == 0)
                {
                    foreach (var p in Main.projectile)
                    {
                        if (p.active && p.type == type)
                        {
                            p.timeLeft = 60;
                        }
                    }
                }
            }

            npc.ai[0]++;
            if ((int)npc.ai[0] >= 1020)
            {
                Array.Clear(npc.ai);
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
            //if ((int)npc.ai[0] % 10 == 0) 
            //{
            //    Projectile.NewProjectileDirect(npc.Center, default, ModContent.ProjectileType<EnergyFist>(), 120, 4, Main.myPlayer, 1).frame = npc.whoAmI;
            //}

        }
        public void LaserDaggers()
        {
            if (npc.ai[0] == 0)
            {
                npc.ai[1] = targetPlayer.Center.X;
                npc.ai[2] = targetPlayer.Center.Y;
                var tar = npc.Center;
                for (int n = 0; n < 61; n++)
                {
                    var fac = n / 600f;
                    tar = Vector2.Lerp(tar, new Vector2(npc.ai[1], npc.ai[2] - 320), (float)Math.Pow(fac * 5, 3));
                }
                var p = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), tar, default, ModContent.ProjectileType<LightDagger_2>(), 35, 0, Main.myPlayer);
                p.frameCounter = npc.whoAmI;
                p.frame = targetPlayer.whoAmI;
                p.localAI[0] = stage > 2 ? 3 : 2;
            }
            npc.ai[0]++;
            var factor = npc.ai[0] / 600f;
            var f2 = Math.Abs(0.5f - factor);
            if ((int)npc.ai[0] <= 60)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[1], npc.ai[2] - 320), (float)Math.Pow(factor * 5, 3));
            }
            else if ((int)npc.ai[0] <= 540)
            {
                for (int n = -1; n < 2; n += 2)
                {
                    var v = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 16f);
                    var u = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 96f);
                    var cen = npc.Center + new Vector2(-66 * n, -18) + v;
                    var f = MathHelper.Clamp(npc.ai[0] / 120f - 1, 0, 1);
                    f *= f;
                    var target = Vector2.Lerp(npc.Center + new Vector2(0, 320), targetPlayer.Center, f);
                    if ((int)npc.ai[0] % (int)(MathHelper.Clamp(f2 * 2, 0.2f, 0.8f) * 10) == 0)
                    {
                        //for (int i = 0; i < 6; i++)
                        //{
                        //    var v = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 16f);
                        //    var u = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 96f);
                        //    var cen = npc.Center + new Vector2(-66 * n, -18) + v;
                        //    var f = MathHelper.Clamp(npc.ai[0] / 120f - 1, 0, 1);
                        //    f *= f;
                        //    var target = Vector2.Lerp(npc.Center + new Vector2(0, 320), targetPlayer.Center, f);
                        //    Projectile.NewProjectile(cen, Vector2.Normalize(target + u - cen) * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer);
                        //}
                        Projectile.NewProjectileDirect(npc.GetSource_FromAI(), cen, Vector2.Normalize(target + (stage != 4 ? u : u * .5f) - cen) * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer).timeLeft = 45;

                    }
                    if (stage == 4 && (int)npc.ai[0] % (int)(MathHelper.Clamp(f2 * 2, 0.2f, 0.8f) * 10) == 1)
                    {
                        Projectile.NewProjectileDirect(npc.GetSource_FromAI(), cen, Vector2.Normalize(target + u * .5f - cen).RotatedBy(MathHelper.Pi / 6 * n) * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer).timeLeft = 45;
                    }
                }
                if ((int)npc.ai[0] % 60 == 0)
                {
                    var n = (int)npc.ai[0] % 120 / 60;
                    if (n == 0)
                    {
                        n--;
                    }

                    var cen = npc.Center + new Vector2(-66 * n, -18);
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), cen, (MathHelper.TwoPi / 3f * (i - 0.25f)).ToRotationVector2() * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
                    }
                }
            }
            if (npc.velocity != default)
            {
                npc.velocity *= 0.9f;
            }
            else if (npc.velocity.Length() < 1)
            {
                npc.velocity = default;
            }

            FrameChanger(600);
            if (factor >= 1)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void LaserSpray()
        {
            if (npc.ai[0] == 0)
            {
                npc.ai[1] = targetPlayer.Center.X;
                npc.ai[2] = targetPlayer.Center.Y;
                //var tar = npc.Center;
                //for (int n = 0; n < 61; n++)
                //{
                //    var fac = n / 600f;
                //    tar = Vector2.Lerp(tar, new Vector2(npc.ai[1], npc.ai[2] - 320), (float)Math.Pow(fac * 5, 3));
                //}
                //var p = Projectile.NewProjectileDirect(tar, default, ModContent.ProjectileType<LightDagger_2>(), 35, 0, Main.myPlayer);
                //p.frameCounter = npc.whoAmI;
                //p.frame = targetPlayer.whoAmI;
            }
            npc.ai[0]++;
            if ((int)npc.ai[0] <= 60)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[1], npc.ai[2] - 320), (float)Math.Pow(npc.ai[0] / 60f, 3));
            }
            else if ((int)npc.ai[0] <= 840)
            {
                for (int n = -1; n < 2; n += 2)
                {
                    var cen = npc.Center + new Vector2(-66 * n, -18);
                    if (((int)npc.ai[0] - 60) % 390 >= 200)
                    {
                        if ((int)npc.ai[0] % 10 >= 6)
                        {
                            for (int i = -1; i < 2; i += 2)
                            {
                                Projectile.NewProjectileDirect(npc.GetSource_FromAI(), cen, i * (n * npc.ai[0] / 240 * MathHelper.TwoPi).ToRotationVector2() * (8 + stage), ModContent.ProjectileType<LightPellet>(), 70, 0, Main.myPlayer).timeLeft = 180;
                                SoundEngine.PlaySound(SoundID.Item68);
                            }
                        }
                    }
                    else
                    {
                        if ((int)npc.ai[0] % 8 == 0)
                        {
                            //for (int i = 0; i < 6; i++)
                            //{
                            //    var v = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 16f);
                            //    var u = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 96f);
                            //    var cen = npc.Center + new Vector2(-66 * n, -18) + v;
                            //    var f = MathHelper.Clamp(npc.ai[0] / 120f - 1, 0, 1);
                            //    f *= f;
                            //    var target = Vector2.Lerp(npc.Center + new Vector2(0, 320), targetPlayer.Center, f);
                            //    Projectile.NewProjectile(cen, Vector2.Normalize(target + u - cen) * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer);
                            //}
                            for (int i = -1; i < 2; i += 2)
                            {
                                Projectile.NewProjectileDirect(npc.GetSource_FromAI(), cen, i * (n * npc.ai[0] / 240 * MathHelper.TwoPi).ToRotationVector2() * (8 + stage), ModContent.ProjectileType<LightPellet>(), 70, 0, Main.myPlayer).timeLeft = 180;
                                SoundEngine.PlaySound(SoundID.Item68);
                            }
                        }
                    }

                }
                //if ((int)npc.ai[0] % 60 == 0)
                //{
                //    var n = (int)npc.ai[0] % 120 / 60;
                //    if (n == 0) n--;
                //    var cen = npc.Center + new Vector2(-66 * n, -18);
                //    for (int i = 0; i < 3; i++)
                //    {
                //        Projectile.NewProjectile(cen, (MathHelper.TwoPi / 3f * (i - 0.25f)).ToRotationVector2() * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
                //    }
                //}
            }
            else if ((int)npc.ai[0] <= 1140)
            {
                for (int n = -1; n < 2; n += 2)
                {
                    var cen = npc.Center + new Vector2(-66 * n, -18);
                    if ((int)npc.ai[0] % 6 == 0)
                    {
                        var theta = (float)(Math.Pow(npc.ai[0] - 1140, 2) * Math.Pow(MathHelper.Pi / 45, 3));
                        for (int i = 0; i < stage; i++)
                        {
                            var rot = theta + MathHelper.TwoPi / stage * i;
                            if (n == -1)
                            {
                                rot = MathHelper.Pi - rot;
                            }

                            Projectile.NewProjectile(npc.GetSource_FromAI(), cen, rot.ToRotationVector2() * (8 + stage), ModContent.ProjectileType<LightPellet>(), 50, 0, Main.myPlayer);
                            SoundEngine.PlaySound(SoundID.Item68);
                        }
                    }
                }
            }
            if (npc.ai[0] > 60 && npc.ai[0] < 1140)
            {
                var vec1 = npc.Center - targetPlayer.Center;
                if (vec1.Length() >= 1024)
                {
                    targetPlayer.velocity += Vector2.Normalize(vec1) * 6;
                    //if ((int)Main.time % 5 == 0)
                    //{
                    //    Projectile.NewProjectile(targetPlayer.Center + Main.rand.NextFloat(0, Main.rand.NextFloat(0, 512)) * Main.rand.NextVector2Unit(), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(1f, 2f), npc.whoAmI);
                    //}
                }
            }

            if (npc.velocity != default)
            {
                npc.velocity *= 0.9f;
            }
            else if (npc.velocity.Length() < 1)
            {
                npc.velocity = default;
            }
            //if ((int)npc.ai[0] > 60 && (int)npc.ai[0] < 1140)
            //{
            //    if (Main.GameUpdateCount % 10 == 0)
            //        FrameCounter = FrameCounter == 10 ? 11 : 10;
            //}
            //else if ((int)npc.ai[0] > 45 && (int)npc.ai[0] < 1155)
            //{
            //    FrameCounter = 9;
            //}
            //else if ((int)npc.ai[0] > 30 && (int)npc.ai[0] < 1170)
            //{
            //    FrameCounter = 8;
            //}
            //else
            //{
            //    FrameCounter = 0;
            //}
            FrameChanger(1200);
            if ((int)npc.ai[0] >= 1200)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void LaserCross()
        {
            if (stage < 3)
            {
                return;
            }
            //if ((int)npc.ai[0] % 120 == 0)
            //{
            //    npc.ai[1] = targetPlayer.Center.X;
            //    npc.ai[2] = targetPlayer.Center.Y;
            //    if ((int)npc.ai[0] != 0 && (int)npc.ai[0] < 720)
            //    {
            //        var vec = Main.rand.NextFloat(0, Main.rand.NextFloat(0, 512)) * Main.rand.NextVector2Unit();
            //        npc.ai[1] += vec.X;
            //        npc.ai[2] += vec.Y;
            //    }
            //}
            //if ((int)npc.ai[0] % 120 == 30)
            //{
            //    Projectile.NewProjectile(new Vector2(npc.ai[1], npc.ai[2]), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer);
            //}

            if ((int)npc.ai[0] == 0)
            {
                npc.ai[1] = targetPlayer.Center.X;
                npc.ai[2] = targetPlayer.Center.Y;
            }
            else if ((int)npc.ai[0] % 10 == 0 && (int)npc.ai[0] >= 60 && (int)npc.ai[0] <= 1140)
            {
                if ((int)npc.ai[0] % (60 - 10 * stage) == 0)
                {
                    Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + Main.rand.NextFloat(0, Main.rand.NextFloat(0, 512)) * Main.rand.NextVector2Unit(), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(1f, 2f), npc.whoAmI);
                }
                if ((int)npc.ai[0] % 120 == 0)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        if (indexOfProjector[n] != -1)
                        {
                            var owner = Main.npc[indexOfProjector[n]];
                            var vec = Vector2.Normalize(targetPlayer.Center - owner.Center);
                            SoundEngine.PlaySound(SoundID.Item100, owner.Center);
                            //for (int k = 0; k < stage * 2 - 4; k++)
                            //{
                            //    Projectile.NewProjectileDirect(owner.Center, vec.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 12, MathHelper.Pi / 12) + k * MathHelper.TwoPi / (stage * 2 - 4)) * 32, ModContent.ProjectileType<ApeBossMissile>(), 50, 0, Main.myPlayer).localAI[0] = 1;
                            //}
                            Projectile.NewProjectileDirect(npc.GetSource_FromAI(), owner.Center, vec * 16, ModContent.ProjectileType<ApeBossMissile>(), 50, 0, Main.myPlayer).localAI[0] = 1;
                            break;
                        }

                    }
                }
                if ((int)npc.ai[0] >= 840)
                {
                    if (stage == 4 || (int)npc.ai[0] % 20 == 0)
                    {
                        Projectile.NewProjectileDirect(npc.GetSource_FromAI(), targetPlayer.Center, default, ModContent.ProjectileType<StrawberryCross>(), 75, 2, Main.myPlayer, 1f, npc.whoAmI).localAI[0] = 1;
                    }
                }
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(Main.rand.NextFloat(-960, 960), Main.rand.NextFloat(-960, 960)), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(0.75f, 1.5f), npc.whoAmI);
            }
            if ((int)npc.ai[0] % 4 == 0)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + (npc.ai[0] / 20).ToRotationVector2() * 1024, default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
            }

            if (npc.ai[0] > 60 && npc.ai[0] < 1140)
            {
                var vec1 = npc.Center - targetPlayer.Center;
                if (vec1.Length() >= 1024)
                {
                    targetPlayer.velocity += Vector2.Normalize(vec1) * 4;
                    if ((int)Main.time % 5 == 0)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + Main.rand.NextFloat(0, Main.rand.NextFloat(0, 512)) * Main.rand.NextVector2Unit(), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(1f, 2f), npc.whoAmI);
                    }
                }
            }
            if ((int)npc.ai[0] < 60)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[1], npc.ai[2] - 320), (float)Math.Pow(npc.ai[0] / 59f, 3));
            }

            npc.ai[0]++;
            FrameChanger(1200);
            if ((int)npc.ai[0] >= 1200)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void BulletHell()
        {
            //Main.NewText("??");
            //npc.ai[0]++;
            if (stage < 3)
            {
                return;
            }

            var period = (Main.expertMode ? 1200 : 1000);
            if (npc.ai[0] == 0)
            {
                npc.ai[2] = targetPlayer.Center.X;
                npc.ai[3] = targetPlayer.Center.Y - 320;
            }
            if ((int)npc.ai[0] <= 60f)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[2], npc.ai[3]), npc.ai[0] / 60f);
            }
            if (npc.ai[0] > 60 && npc.ai[0] < period - 60)
            {
                var vec1 = npc.Center - targetPlayer.Center;
                if (vec1.Length() >= 1024)
                {
                    targetPlayer.velocity += Vector2.Normalize(vec1) * 4;
                    if ((int)Main.time % 5 == 0)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + Main.rand.NextFloat(0, Main.rand.NextFloat(0, 512)) * Main.rand.NextVector2Unit(), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(1f, 2f), npc.whoAmI);
                    }
                }
            }
            //var timer = (int)npc.ai[0] % period;
            //if (timer == period / 10)
            //{
            //    npc.Center = targetPlayer.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(256, 1024 * (Main.expertMode ? 0.75f : 1));
            //}
            //npc.rotation = (targetPlayer.Center - npc.Center).ToRotation();
            //if ((int)npc.ai[0] / period < 3)
            //{
            //    if (timer == period / 10 * 3)
            //    {
            //        for (int n = 0; n < 16; n++)
            //        {
            //            var r = n * MathHelper.Pi / 8 + (targetPlayer.Center - npc.Center).ToRotation();
            //            Projectile.NewProjectile(new Vector2(64, 0).RotatedBy(r) + npc.Center, new Vector2(32, 0).RotatedBy(r), ModContent.ProjectileType<LightPellet>(), 50, 0, Main.myPlayer, period / 5 * 4);
            //        }
            //    }
            //    if (timer == period / 10 * 5)
            //    {
            //        for (int n = 0; n < 16; n++)
            //        {
            //            var r = n * MathHelper.Pi / 8 + (targetPlayer.Center - npc.Center).ToRotation();
            //            Projectile.NewProjectile(new Vector2(64, 0).RotatedBy(r) + npc.Center, new Vector2(32, 0).RotatedBy(r + MathHelper.Pi / 8 * 5), ModContent.ProjectileType<LightPellet>(), 50, 0, Main.myPlayer, period / 5 * 3);
            //        }
            //        for (int n = 0; n < 16; n++)
            //        {
            //            var r = n * MathHelper.Pi / 8 + (targetPlayer.Center - npc.Center).ToRotation();
            //            Projectile.NewProjectile(new Vector2(64, 0).RotatedBy(r) + npc.Center, new Vector2(32, 0).RotatedBy(r - MathHelper.Pi / 8 * 5), ModContent.ProjectileType<LightPellet>(), 50, 0, Main.myPlayer, period / 5 * 3);
            //        }
            //    }
            //    if (timer < period / 10 && timer % 10 == 0)
            //    {
            //        Projectile.NewProjectile(npc.Center, Main.rand.NextVector2Unit() * 10, ModContent.ProjectileType<LightPellet>(), 10, 0, Main.myPlayer, Main.myPlayer);
            //    }
            //}
            //else
            //{
            //    if (timer == period / 10 * 4)
            //    {

            //    }
            //}
            FrameChanger(period);

            if ((int)npc.ai[0] >= period)
            {
                Array.Clear(npc.ai);
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }

            const float c = 6f;
            npc.ai[0]++;
            targetPlayer.velocity += Vector2.Normalize(npc.Center - targetPlayer.Center) * .1f;
            if ((int)npc.ai[0] % 60 < (Main.expertMode ? 18 : 15 + (stage - 3) * 6) && (int)npc.ai[0] % 6 == 0)
            {
                for (int n = 0; n < (int)c; n++)
                {
                    var va = npc.ai[0] / 300;//Main.time / 300
                    var d = (Main.expertMode ? 1024 : 1280);
                    var v = new Vector2(d, 0).RotatedBy((va + n / c) * MathHelper.TwoPi);
                    Projectile.NewProjectileDirect(npc.GetSource_FromAI(), v + npc.Center, Vector2.Normalize(v).RotatedBy(MathHelper.Pi / 24f) * -(Main.expertMode ? 8f : 6 + (stage - 3) * 2), ModContent.ProjectileType<LightPellet>(), 30, 0, Main.myPlayer, 9).timeLeft = (Main.expertMode ? 225 : 300);
                    var v2 = new Vector2(d + 256, 0).RotatedBy((-va + n / c) * MathHelper.TwoPi);
                    Projectile.NewProjectileDirect(npc.GetSource_FromAI(), v2 + npc.Center, Vector2.Normalize(v2).RotatedBy(MathHelper.Pi / 24f) * -(Main.expertMode ? 8f : 6 + (stage - 3) * 2), ModContent.ProjectileType<LightPellet>(), 30, 0, Main.myPlayer, 4).timeLeft = (Main.expertMode ? 225 : 300);
                    for (int i = 0; i < 15; i++)
                    {
                        var vec = (i / 15f * MathHelper.TwoPi).ToRotationVector2();
                        Dust.NewDustPerfect(v + npc.Center, MyDustId.GreenFXPowder, vec, 0, Color.White).noGravity = true;
                        Dust.NewDustPerfect(v2 + npc.Center, MyDustId.GreenFXPowder, vec, 0, Color.White).noGravity = true;
                    }
                }
            }

            //if ((int)npc.ai[0] >= 500)
            //{
            //    npc.ai[0] = 0;
            //    npc.ai[1] = 0;
            //    npc.ai[2] = 0;
            //    npc.ai[3] = 0;
            //    attackMode = BigApeAttackMode.AfterAttack;
            //    FrameCounter = 12;
            //}
        }
        public void Tesseract()
        {
            if (npc.ai[0] == 0)
            {
                npc.ai[2] = targetPlayer.Center.X;
                npc.ai[3] = targetPlayer.Center.Y - 320;
            }
            if ((int)npc.ai[0] <= 60f)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[2], npc.ai[3]), npc.ai[0] / 60f);
            }
            if (npc.ai[0] == 60)
            {
                Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, default, ModContent.ProjectileType<LaserTesseract>(), 40, 3, Main.myPlayer, 0, targetPlayer.whoAmI).frameCounter = npc.whoAmI;
            }
            FrameChanger(1140);
            npc.ai[0]++;
            if (npc.ai[0] >= 1140)
            {
                //foreach (var p in Main.projectile) 
                //{
                //    if (p.type == ModContent.ProjectileType<LaserTesseract>()) 
                //    {
                //        p.ai[0] = 960;
                //    }
                //}
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void ProfessorStrawberry_Old()
        {
            FrameChanger(1800);
            foreach (var i in indexOfProjector)
            {
                if (i != -1 && Main.npc[i].ai[0] == 0)
                {
                    Main.npc[i].ai[0] = 2;
                }
            }
            if (npc.ai[0] == 0)
            {
                npc.ai[2] = targetPlayer.Center.X;
                npc.ai[3] = targetPlayer.Center.Y - 320;
            }
            if ((int)npc.ai[0] <= 60f)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[2], npc.ai[3]), npc.ai[0] / 60f);
            }
            if ((int)npc.ai[0] == 0)
            {
                for (int n = 0; n < 30; n++)
                {
                    Projectile.NewProjectile(npc.GetSource_FromAI(), new Vector2(npc.ai[2], npc.ai[3]) + (n * MathHelper.TwoPi / 30).ToRotationVector2() * 1024, default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
                }
            }
            if (npc.ai[0] > 60 && npc.ai[0] < 1740)
            {
                var vec1 = npc.Center - targetPlayer.Center;
                if (vec1.Length() >= 1024)
                {
                    targetPlayer.velocity += Vector2.Normalize(vec1) * 4;
                    if ((int)Main.time % 3 == 0)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + Main.rand.NextFloat(0, Main.rand.NextFloat(0, 256)) * Main.rand.NextVector2Unit(), default, ModContent.ProjectileType<StrawberryCross>(), 150, 2, Main.myPlayer, Main.rand.NextFloat(1.5f, 2.5f), npc.whoAmI);
                    }
                }
                if (npc.ai[0] < 600)
                {
                    if ((int)npc.ai[0] % 10 == 0)
                    {
                        var theta = (npc.ai[0] - 60) / 540f * MathHelper.TwoPi;
                        for (int n = 0; n < 6; n++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + (4 / 3 * theta + MathHelper.TwoPi / 6 * n + theta / 12).ToRotationVector2() * 1024 * (float)Math.Sin(theta), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
                        }
                        //Projectile.NewProjectile(npc.Center + (8 * theta).ToRotationVector2() * 1024 * (float)Math.Sin(6 * theta), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);

                    }
                    if ((int)npc.ai[0] % 60 == 0)
                    {
                        var n = (int)npc.ai[0] % 120 / 60;
                        if (n == 0)
                        {
                            n--;
                        }

                        var cen = npc.Center + new Vector2(-66 * n, -18);
                        for (int i = 0; i < 3; i++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), cen, (MathHelper.TwoPi / 3f * (i - 0.25f)).ToRotationVector2() * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
                        }
                    }
                }
                else if (npc.ai[0] < 1200)
                {
                    if ((int)npc.ai[0] % 4 == 0)
                    {
                        var fac = npc.ai[0] % 120 / 120;
                        for (int n = 0; n < 4; n++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(-1024 + 512 * n, fac.Lerp(-960, 960)), default, ModContent.ProjectileType<StrawberryCross_PS>(), 50, 2, Main.myPlayer, 2.5f, npc.whoAmI);
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(-768 + 512 * n, (960 + fac * 1920) % 1920 - 960), default, ModContent.ProjectileType<StrawberryCross_PS>(), 50, 2, Main.myPlayer, 2.5f, npc.whoAmI);
                        }

                    }
                }
                else
                {
                    if ((int)npc.ai[0] == 1200 || (int)npc.ai[0] == 1500)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, default, ModContent.ProjectileType<BigApeVectorField>(), 50, 0, Main.myPlayer);
                    }
                    if (npc.ai[0] >= 1200 && npc.ai[0] <= 1380)
                    {
                        if (npc.ai[0] % 15 == 0)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(Main.rand.NextFloat(-960, 960), Main.rand.NextFloat(-960, 960)), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(0.75f, 1.5f), npc.whoAmI);
                        }
                    }
                    if (npc.ai[0] >= 1500 && npc.ai[0] <= 1680)
                    {
                        if (npc.ai[0] % 10 == 0)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(Main.rand.NextFloat(-960, 960), Main.rand.NextFloat(-960, 960)), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(0.75f, 1.5f), npc.whoAmI);
                        }
                    }
                }
                //if ((int)npc.ai[0] % 300 == 0) 
                //{
                //    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<BigApeVectorField>(), 30, 0, Main.myPlayer);

                //}
            }
            if ((int)npc.ai[0] % 4 == 0)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + (npc.ai[0] / 20).ToRotationVector2() * 1024, default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
            }

            npc.ai[0]++;


            if ((int)npc.ai[0] >= 1800)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public void ProfessorStrawberry()
        {
            FrameChanger(1920);
            foreach (var i in indexOfProjector)
            {
                if (i != -1 && Main.npc[i].ai[0] == 0)
                {
                    Main.npc[i].ai[0] = 2;
                }
            }
            if (npc.ai[0] == 0)
            {
                npc.ai[2] = targetPlayer.Center.X;
                npc.ai[3] = targetPlayer.Center.Y - 320;
            }
            if ((int)npc.ai[0] <= 60f)
            {
                npc.Center = Vector2.Lerp(npc.Center, new Vector2(npc.ai[2], npc.ai[3]), npc.ai[0] / 60f);
            }
            if ((int)npc.ai[0] == 0)
            {
                for (int n = 0; n < 30; n++)
                {
                    Projectile.NewProjectile(npc.GetSource_FromAI(), new Vector2(npc.ai[2], npc.ai[3]) + (n * MathHelper.TwoPi / 30).ToRotationVector2() * 1024, default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
                }
            }
            if (npc.ai[0] > 60 && npc.ai[0] < 1860)
            {
                var vec1 = npc.Center - targetPlayer.Center;
                if (vec1.Length() >= 1024)
                {
                    targetPlayer.velocity += Vector2.Normalize(vec1) * 4;
                    if ((int)Main.time % 3 == 0)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), targetPlayer.Center + Main.rand.NextFloat(0, Main.rand.NextFloat(0, 256)) * Main.rand.NextVector2Unit(), default, ModContent.ProjectileType<StrawberryCross>(), 150, 2, Main.myPlayer, Main.rand.NextFloat(1.5f, 2.5f), npc.whoAmI);
                    }
                }
                if (npc.ai[0] < 660)
                {
                    if ((int)(npc.ai[0] - 60) % 300 == 60)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(targetPlayer.Center.X > npc.Center.X ? 1024 : -1024, 0), default, ModContent.ProjectileType<StrawberryLaser>(), 50, 0, Main.myPlayer, -MathHelper.PiOver2, targetPlayer.Center.X > npc.Center.X ? 2 : 1);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(targetPlayer.Center.X > npc.Center.X ? 1024 : -1024, 0), default, ModContent.ProjectileType<StrawberryLaser>(), 50, 0, Main.myPlayer, MathHelper.PiOver2, targetPlayer.Center.X > npc.Center.X ? 2 : 1);

                        for (int n = 0; n < 9; n++)
                        {
                            for (int k = 0; k < 9; k++)
                            {
                                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(n * 256, (k + (n % 2 / 2f)) * 256) - new Vector2(1024), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(0.75f, 1.5f), npc.whoAmI);
                            }
                        }
                    }
                }
                else if (npc.ai[0] < 1260)
                {
                    if ((int)npc.ai[0] % 4 == 0)
                    {
                        //var fac = npc.ai[0] % 120 / 120;
                        //for (int n = 0; n < 4; n++)
                        //{
                        //    Projectile.NewProjectile(npc.Center + new Vector2(-1024 + 512 * n, fac.Lerp(-960, 960)), default, ModContent.ProjectileType<StrawberryCross_PS>(), 50, 2, Main.myPlayer, 2.5f, npc.whoAmI);
                        //    Projectile.NewProjectile(npc.Center + new Vector2(-768 + 512 * n, (960 + fac * 1920) % 1920 - 960), default, ModContent.ProjectileType<StrawberryCross_PS>(), 50, 2, Main.myPlayer, 2.5f, npc.whoAmI);
                        //}
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(Main.rand.NextFloat(-960, 960), Main.rand.NextFloat(-960, 960)), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, Main.rand.NextFloat(0.75f, 1.5f), npc.whoAmI);
                    }
                    if ((int)(npc.ai[0] - 60) % 300 == 60)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, default, ModContent.ProjectileType<StrawberryLaser>(), 50, 0, Main.myPlayer, -MathHelper.PiOver2, 5);
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, default, ModContent.ProjectileType<StrawberryLaser>(), 50, 0, Main.myPlayer, MathHelper.PiOver2, 5);
                    }
                }
                else if (npc.ai[0] < 1860)
                {
                    if ((int)npc.ai[0] % 10 == 0)
                    {
                        var theta = (npc.ai[0] - 1260) / 540f * MathHelper.TwoPi;
                        for (int n = 0; n < 6; n++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + (4 / 3 * theta + MathHelper.TwoPi / 6 * n + theta / 12).ToRotationVector2() * 1024 * (float)Math.Sin(theta), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
                        }
                        //Projectile.NewProjectile(npc.Center + (8 * theta).ToRotationVector2() * 1024 * (float)Math.Sin(6 * theta), default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);

                    }
                    if ((int)npc.ai[0] % 60 == 0)
                    {
                        var n = (int)npc.ai[0] % 120 / 60;
                        if (n == 0)
                        {
                            n--;
                        }

                        var cen = npc.Center + new Vector2(-66 * n, -18);
                        for (int i = 0; i < 3; i++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), cen, (MathHelper.TwoPi / 3f * (i - 0.25f)).ToRotationVector2() * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
                        }
                    }
                }
                //if ((int)npc.ai[0] % 300 == 0) 
                //{
                //    Projectile.NewProjectile(npc.Center, default, ModContent.ProjectileType<BigApeVectorField>(), 30, 0, Main.myPlayer);

                //}
            }
            if ((int)npc.ai[0] % 4 == 0)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + (npc.ai[0] / 20).ToRotationVector2() * 1024, default, ModContent.ProjectileType<StrawberryCross>(), 50, 2, Main.myPlayer, 1f, npc.whoAmI);
            }

            npc.ai[0]++;


            if ((int)npc.ai[0] >= 1920)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                attackMode = BigApeAttackMode.AfterAttack;
                FrameCounter = 12;
            }
        }
        public Player targetPlayer
        {
            get
            {
                Vector2 cen = npc.Center;
                Player target = null;
                float distanceMax = 4096f;
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
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            switch (stage)
            {
                case 0: Draw_PreAttackAni(spriteBatch); break;
                case 1: case 2: case 3: case 4: Draw_Attack(spriteBatch); break;
                case 5: Draw_DeathAni(spriteBatch); break;
                    //case 2: Draw_Stage2(spriteBatch); break;
                    //case 3: Draw_Stage3(spriteBatch);break;
                    //case 4: Draw_Stage4(spriteBatch);break;
            }
            //sw.Stop();
            //Main.NewText(sw.ElapsedTicks);
            return false;
        }

        private void Draw_PreAttackAni(SpriteBatch spriteBatch)
        {
            var tex = VirtualDreamMod.GetTexture(ApePath + "BigApe_BeginningFrames");
            var scftex = VirtualDreamMod.GetTexture(ApePath + "BigApeScreenFrame_ANI");
            if (FrameCounter > 13)
            {
                var f = FrameCounter - 14;
                var v = new Vector2(480, 0) + (Main.GameUpdateCount / 300f * MathHelper.TwoPi).ToRotationVector2() * 64;
                var u = targetPlayer.velocity / (targetPlayer.velocity.Length() / 128f + 1);
                spriteBatch.Draw(scftex, targetPlayer.Center + v - Main.screenPosition - u, scftex.Frame(15, 2, f % 15, f / 15), Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                v.X *= -1;
                spriteBatch.Draw(scftex, targetPlayer.Center + v - Main.screenPosition - u, scftex.Frame(15, 2, f % 15, f / 15), Color.White, 0, new Vector2(48, 72), 3, 0, 0);
            }
            spriteBatch.Draw(tex, npc.Center - Main.screenPosition, tex.Frame(11, 4, FrameCounter % 11, FrameCounter / 11), Color.White, 0, new Vector2(56, 56), 3, 0, 0);
        }

        private void Draw_Attack(SpriteBatch spriteBatch)
        {
            var tex = VirtualDreamMod.GetTexture(ApePath + "BigApe_Stage" + stage + "Frames");
            var scftex = VirtualDreamMod.GetTexture(ApePath + "BigApeScreenFrame");
            var sctex = VirtualDreamMod.GetTexture(ApePath + "BigApeScreen");

            var v = new Vector2(480, 0) + (Main.GameUpdateCount / 300f * MathHelper.TwoPi).ToRotationVector2() * 64;
            var u = targetPlayer.velocity / (targetPlayer.velocity.Length() / 128f + 1);
            spriteBatch.Draw(scftex, targetPlayer.Center + v - Main.screenPosition - u, null, Color.White, 0, new Vector2(32, 72), 3, 0, 0);
            Rectangle rectangle;
            //switch (attackMode) 
            //{
            //    case 0:
            //    case BigApeAttackMode.AfterAttack:

            //        break;
            //}
            var attackType = (int)attackMode;
            if (attackType < 2)
            {
                rectangle = sctex.Frame(24, 1, (int)Main.GameUpdateCount / 4 % 4, 0);
            }
            else
            {
                rectangle = sctex.Frame(24, 1, (int)Main.GameUpdateCount / 4 % 2 + 2 * attackType, 0);
                //rectangle = sctex.Frame(24, 1, (int)Main.GameUpdateCount / 4 % 2 + 20, 0);
            }
            spriteBatch.Draw(sctex, targetPlayer.Center + v - Main.screenPosition - u, rectangle, Color.White, 0, new Vector2(32, 72), 3, 0, 0);
            v.X *= -1;
            spriteBatch.Draw(scftex, targetPlayer.Center + v - Main.screenPosition - u, null, Color.White, 0, new Vector2(32, 72), 3, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(sctex, targetPlayer.Center + v - Main.screenPosition - u, rectangle, Color.White, 0, new Vector2(32, 72), 3, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(tex, npc.Center - Main.screenPosition, tex.Frame(15, 1, FrameCounter, 0), Color.White, 0, new Vector2(56, 56), 3, 0, 0);

            if (attackMode == BigApeAttackMode.LaserDaggers || attackMode == BigApeAttackMode.LaserSpray || attackMode == BigApeAttackMode.BulletHell)
            {
                var fac = attackMode == BigApeAttackMode.LaserSpray ? MathHelper.Clamp(600 - Math.Abs(600 - npc.ai[0]), 0, 120) / 120f : MathHelper.Clamp(300 - Math.Abs(300 - npc.ai[0]), 0, 120) / 120f;
                if (attackMode == BigApeAttackMode.BulletHell)
                {
                    fac = MathHelper.Clamp((Main.expertMode ? 600 : 500) - Math.Abs((Main.expertMode ? 600 : 500) - npc.ai[0]), 0, 120) / 120f;
                }
                spriteBatch.DrawPath
                (
                    (r) => (r * MathHelper.TwoPi).ToRotationVector2() * fac * (attackMode == BigApeAttackMode.BulletHell && !Main.expertMode ? 1280 : 1024),
                    (f) => ((f + Main.GameUpdateCount / 600f) % 1).ArrayLerp(Color.Cyan, Color.White, Color.Blue, Color.Cyan),
                    LogSpiralLibraryMod.ShaderSwooshEffect,
                    LogSpiralLibraryMod.BaseTex[8].Value,
                    LogSpiralLibraryMod.Misc[6].Value,
                    npc.Center,
                    200,
                    width: 32 * fac,
                    kOfX: 16,
                    //looped: true,
                    lightFunc: (f) => 2 * fac
                );
            }
            if (attackMode == BigApeAttackMode.LaserFists)
            {
                var fac = MathHelper.Clamp(510 - Math.Abs(510 - npc.ai[0]), 0, 120) / 120f;
                spriteBatch.DrawPath
                (
                    (r) => r.ArrayLerp(new Vector2(960, 960), new Vector2(-960, 960), new Vector2(-960, -960), new Vector2(960, -960), new Vector2(960, 960)) + npc.Center * 0,
                    (f) => ((f + Main.GameUpdateCount / 600f) % 1).ArrayLerp(Color.Cyan, Color.White, Color.Blue, Color.Cyan),
                    LogSpiralLibraryMod.ShaderSwooshEffect,
                    LogSpiralLibraryMod.BaseTex[8].Value,
                    LogSpiralLibraryMod.Misc[6].Value,
                    npc.Center,
                    100,
                    width: 32 * fac,
                    kOfX: 16,
                    //looped: true,
                    lightFunc: (f) => 2 * fac
                );
            }
        }

        private void Draw_DeathAni(SpriteBatch spriteBatch)
        {
            var tex = VirtualDreamMod.GetTexture(ApePath + "BigApe_EndFrames");
            var scftexA = VirtualDreamMod.GetTexture(ApePath + "BigApeScreenFrame_ANI");

            var scftex = VirtualDreamMod.GetTexture(ApePath + "BigApeScreenFrame");
            var sctex = VirtualDreamMod.GetTexture(ApePath + "BigApeScreen");

            var v = new Vector2(480, 0) + (Main.GameUpdateCount / 300f * MathHelper.TwoPi).ToRotationVector2() * 64;
            var u = targetPlayer.velocity / (targetPlayer.velocity.Length() / 128f + 1);
            if (FrameCounter <= 51)
            {
                if (FrameCounter > 21)
                {
                    var f = 51 - FrameCounter;
                    spriteBatch.Draw(scftexA, targetPlayer.Center + v - Main.screenPosition - u, scftexA.Frame(15, 2, f % 15, f / 15), Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                    v.X *= -1;
                    spriteBatch.Draw(scftexA, targetPlayer.Center + v - Main.screenPosition - u, scftexA.Frame(15, 2, f % 15, f / 15), Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                }
                else
                {
                    var rectangle = sctex.Frame(24, 1, (int)Main.GameUpdateCount / 4 % 2 + 22, 0);
                    spriteBatch.Draw(scftex, targetPlayer.Center + v - Main.screenPosition - u, null, Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                    spriteBatch.Draw(sctex, targetPlayer.Center + v - Main.screenPosition - u, rectangle, Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                    v.X *= -1;
                    spriteBatch.Draw(scftex, targetPlayer.Center + v - Main.screenPosition - u, null, Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                    spriteBatch.Draw(sctex, targetPlayer.Center + v - Main.screenPosition - u, rectangle, Color.White, 0, new Vector2(48, 72), 3, 0, 0);
                    spriteBatch.Draw(tex, npc.Center - Main.screenPosition, tex.Frame(22, 1, FrameCounter, 0), Color.White, 0, new Vector2(56, 56), 3, 0, 0);
                }
            }
        }
        //void Draw_Stage2(SpriteBatch spriteBatch) { }
        //void Draw_Stage3(SpriteBatch spriteBatch) { }
        //void Draw_Stage4(SpriteBatch spriteBatch) { }
    }
    //public class BigApeProjector : ModNPC
    //{
    //    public override void SetStaticDefaults()
    //    {
    //        DisplayName.SetDefault("大猿人投影仪");
    //    }

    //    private NPC ownerNPC => Main.npc[(int)npc.ai[0]];
    //    public override void SetDefaults()
    //    {
    //        npc.width = 60;
    //        npc.height = 60;
    //        npc.knockBackResist = 0f;
    //        npc.aiStyle = -1;
    //        npc.damage = 20;
    //        npc.noGravity = true;
    //        npc.noTileCollide = false;
    //        npc.defense = 45;
    //        npc.lifeMax = 50000;
    //        npc.friendly = false;
    //    }
    //    public override void AI()
    //    {
    //        if (ownerNPC.type != ModContent.NPCType<BigApe>() || !ownerNPC.active)
    //        {
    //            npc.active = false;
    //            npc.life -= 495 * 514;
    //        }
    //        npc.timeLeft = 495;
    //        if (npc.velocity.Length() > 0.5f)
    //            npc.velocity *= 0.9f;
    //        else npc.velocity = default;
    //    }
    //    public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
    //    {

    //        return false;
    //    }
    //}
    public class BigApeProjector : ModNPC
    {
        private NPC npc => NPC;

        public float offsetRot => npc.ai[3];
        public NPC Owner => Main.npc[(int)npc.ai[2]];
        public float theta => offsetRot + Main.GameUpdateCount * velocityOfRot * MathHelper.TwoPi / 300f;
        public float velocityOfRot => npc.ai[1];
        //public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        //{
        //    //var m = Matrix.CreateRotationY(theta);
        //    //var s = GetArea(
        //    //    new Vector3(1, 1, 5).ApplyMatrix(m).Projectile(20),
        //    //    new Vector3(1, -1, 5).ApplyMatrix(m).Projectile(20),
        //    //    new Vector3(-1, 1, 5).ApplyMatrix(m).Projectile(20),
        //    //    new Vector3(-1, -1, 5).ApplyMatrix(m).Projectile(20));
        //    //if (!Main.gamePaused) 
        //    //{
        //    //    var v = new Vector2(theta % TwoPi, s);
        //    //    Main.NewText(v);
        //    //    Dust.NewDustPerfect(new Vector2(960, 560) + v * 32, MyDustId.CyanBubble).noGravity = true;
        //    //}
        //    //spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center + new Vector2(300 * m.M13, 0) * 1200 / (1200 - 300 * m.M33) - Main.screenPosition, TextureAssets.Npc[npc.type].Value.Frame(1, 8, 0, (int)(theta % TwoPi / TwoPi * 8)), Color.White, 0, new Vector2(), s * .25f + 1.75f, 0, 0);

        //    //spriteBatch.End();
        //    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
        //    //spriteBatch.End();
        //    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //    return false;
        //}
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int frame;
            switch (4 * npc.life / npc.lifeMax)
            {
                case 4: case 3: frame = 0; break;
                case 2: case 1: frame = 1; break;
                case 0: frame = 2; break;
                default: frame = 0; break;
            }
            var rec = new Rectangle(32, 32 * frame, 32, 32);
            if (npc.ai[0] > 0)
            {
                rec = new Rectangle(32 * ((int)Main.GameUpdateCount / 3 % 2 + 1), 96, 32, 32);
            }
            spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition, rec, npc.ai[0] > 0 ? Color.White : Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0, new Vector2(16), 3f, SpriteEffects.None, 0);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "lamp"), npc.Center - Main.screenPosition - new Vector2(0, 12), new Rectangle(32, 32 * frame, 32, 32), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), (npc.Center - Owner.Center).ToRotation(), new Vector2(30, 16), 3f, SpriteEffects.None, 0);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "booster"), npc.Center - Main.screenPosition, new Rectangle(32 + (int)Main.GameUpdateCount / 3 % 2 * 32, 0, 32, 32), Color.White, 0, new Vector2(16), 3f, 0, 0);
            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, npc.Center - Main.screenPosition - new Vector2(0, 12), new Rectangle(0, 0, 1, 1), Color.Red, 0, new Vector2(0.5f), 6, SpriteEffects.None, 0);
            //Main.NewText("草生");
            return false;
        }
        //public float Size => (float)Math.Cos(theta) * 0.57f + 1.21f;
        //public void DrawSelf(SpriteBatch spriteBatch, float size = 0)
        //{
        //    var realSize = size != 0 ? size : Size;
        //    spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center - Main.screenPosition, TextureAssets.Npc[npc.type].Value.Frame(1, 8, 0, (int)((theta + MathHelper.Pi / 8) % MathHelper.TwoPi / MathHelper.TwoPi * 8)), Color.White, 0, new Vector2(22, 31), realSize, 0, 0);
        //}
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("大猿人投影仪");
        }
        public override void SetDefaults()
        {
            npc.Center = Owner.Center;
            npc.width = 80;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.damage = 80;
            npc.defense = 60;
            npc.lifeMax = 50000;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (npc.life <= 0)
            {
                var targets = new List<NPC>();
                var type = npc.type;
                for (int n = 0; n < 200; n++)
                {
                    var target = Main.npc[n];
                    if (n != npc.whoAmI && target.type == type && target.active && (int)target.ai[2] == (int)npc.ai[2])
                    {
                        targets.Add(target);
                    }
                }
                for (int n = 0; n < targets.Count; n++)
                {
                    var target = targets[n];
                    target.ai[3] = MathHelper.TwoPi / targets.Count * n;
                    target.ai[1] = 5 / (targets.Count + 1);
                }
                if (Owner.ModNPC is BigApe modOwner)
                {
                    int counter = 0;
                    for (int n = 0; n < 4; n++)
                    {
                        if (modOwner.indexOfProjector[n] == npc.whoAmI)
                        {
                            modOwner.indexOfProjector[n] = -1;
                        }
                        else
                        {
                            if (modOwner.indexOfProjector[n] != -1)
                            {
                                Main.npc[modOwner.indexOfProjector[n]].life = Main.npc[modOwner.indexOfProjector[n]].lifeMax;
                                Main.npc[modOwner.indexOfProjector[n]].ai[0] = 120;
                            }
                            else
                            {
                                counter++;
                            }
                        }

                    }
                    modOwner.FrameCounter = counter == -3 ? 0 : 12;
                    modOwner.stage++;
                    Array.Clear(Owner.ai);
                    modOwner.attackMode = BigApeAttackMode.AfterAttack;
                    foreach (var p in Main.projectile)
                    {
                        if (p.type == ModContent.ProjectileType<LaserTesseract>() && p.ai[0] < 960)
                        {
                            p.ai[0] = 960;
                        }
                    }
                }
                Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, default, 140, 0, 0, Main.myPlayer).Kill();
            }
        }
        public override void AI()
        {
            npc.Center = Owner.Center + theta.ToRotationVector2() * 256;
            if (Owner == null || Owner.active == false)
            {
                npc.life -= 10000000;
            }
            if (npc.ai[0] > 0)
            {
                npc.ai[0]--;
                npc.dontTakeDamage = true;
            }
            else if (Owner.ModNPC is BigApe modOwner)
            {
                npc.dontTakeDamage = modOwner.stage == 0;
            }
        }
    }
    public enum BigApeAttackMode
    {
        None,
        AfterAttack,
        Charge,
        HomingMissiles,
        LaserFists,
        LaserDaggers,
        LaserSpray,
        LaserCross,
        BulletHell,
        Tesseract,
        ProfessorStrawberry
    }
    public abstract class BigApeProj : ModProjectile
    {
        public Projectile projectile => Projectile;
        public virtual bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
        public sealed override bool PreDraw(ref Color lightColor)
        {
            return PreDraw(Main.spriteBatch, lightColor);
        }
        public Texture2D projTex => TextureAssets.Projectile[projectile.type].Value;
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.immuneTime = 10;
        }
    }
    public class LightDagger_Charge : BigApeProj
    {
        public override string Texture => "VirtualDream/" + ApePath + "MirrorAttack";
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 1;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("激光匕首");
        }
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void AI()
        {
            if (projectile.timeLeft == 120)
            {
                projectile.rotation = projectile.velocity.ToRotation();
                for (int n = projectile.oldPos.Length - 1; n > -1; n--)
                {
                    projectile.oldPos[n] = projectile.Center + projectile.rotation.ToRotationVector2() * 256 * (3 + projectile.frame * .5f);
                }
            }
            else if (projectile.timeLeft >= 15 && projectile.timeLeft < 105)
            {
                projectile.rotation += MathHelper.TwoPi / 45 * projectile.ai[1];
            }
            //if (projectile.timeLeft % 10 == 0 && projectile.timeLeft >= 15 && projectile.timeLeft < 105)
            //    for (int n = 0; n < 5 + projectile.frame; n++)
            //    {
            //        var fac = n / (4f + projectile.frame);
            //        var vec = projectile.Center + projectile.rotation.ToRotationVector2() * 256 * (1 + projectile.frame * .5f) * fac;
            //        Main.NewText(fac);
            //        Projectile.NewProjectileDirect(vec, (projectile.rotation + MathHelper.PiOver2 * projectile.ai[1]).ToRotationVector2() * 16, ModContent.ProjectileType<LightPellet>(), 30, 0, Main.myPlayer, 9);

            //    }

            //if (projectile.timeLeft % 40 == 0) 
            //{
            //    var vec = projectile.rotation.ToRotationVector2() * 32f;
            //    vec = new Vector2(-vec.Y, vec.X);
            //    for (int n = 0; n < 2; n++) 
            //    {
            //        Projectile.NewProjectile(projectile.Center, vec, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
            //        vec = -vec;
            //    }
            //}
            projectile.Center = Main.npc[(int)projectile.ai[0]].Center;
            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
            {
                projectile.oldPos[n] = projectile.oldPos[n - 1];
                projectile.oldRot[n] = projectile.oldRot[n - 1];
            }
            projectile.oldPos[0] = projectile.Center + projectile.rotation.ToRotationVector2() * 256 * (3 + projectile.frame * .5f);
            projectile.oldRot[0] = projectile.rotation;
        }
        public void DrawLightSword(Color projColor, Vector2 size, float light, bool autoAdditive = true)
        {
            DrawLightSword(projColor, size, light, projectile.Center, projectile.rotation, autoAdditive);
        }
        public static void DrawLightSword(Color projColor, Vector2 size, float light, Vector2 center, float rotation, bool autoAdditive = true)
        {
            if (autoAdditive)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            Vector2[] key = new Vector2[] { new(0, -16), new(0, 16), new(96, -16), new(96, -24), new(112, 0), new(128, 0), new(96, 16), new(96, 24) };
            Vector2[] Bkey = key.CloneArray();
            //key.Mul(new Vector2(2f, 0.75f));
            //if (Main.expertMode)
            //{
            //    key.Mul(1.25f);
            //}
            key.Mul(new Vector2(1) / new Vector2(128, 48));
            key.Mul(size);
            key.RotatedBy(rotation);
            CustomVertexInfo[] triangles = new CustomVertexInfo[27];
            int[] vs = new int[] { 0, 2, 3, 2, 3, 4, 3, 4, 5, 4, 5, 7, 6, 4, 7, 1, 6, 7, 0, 1, 2, 1, 2, 6, 2, 4, 6 };
            int c = 0;
            void AddListTri(Vector2[] vectors, int index)
            {
                for (int n = 0; n < 3; n++)
                {
                    Vector2 vec = vectors[vs[n + 3 * index]] + center;
                    triangles[c] = new CustomVertexInfo(vec, projColor, new Vector3(Bkey[vs[n + 3 * index]].X / 128f, (Bkey[vs[n + 3 * index]].Y + 32) / 64, (index < 6 ? 0.75f : 2f) * light));
                    c++;
                }
            }
            for (int n = 0; n < 9; n++)
            {
                AddListTri(key, n);
            }
            //void setZ(int index, float value = 0)
            //{
            //    triangles[index].TexCoord.Z = value;
            //}
            for (int n = 0; n < vs.Length; n++)
            {
                if (new int[] { 0, 1, 3, 5, 7 }.Contains(vs[n]))
                {
                    //setZ(n);
                    triangles[n].TexCoord.Z = 0;
                }
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            Effect effect = LogSpiralLibraryMod.ShaderSwooshEffect;
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)VirtualDreamMod.ModTime * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = VirtualDreamMod.GetTexture(ApePath + "Style_10");
            Main.graphics.GraphicsDevice.Textures[1] = VirtualDreamMod.GetTexture(ApePath + "Style_9");

            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            effect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangles, 0, 9);
            #region 你瞅啥
            //if (projectile.localAI[0] >= 60)
            //{
            //    List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            //    for (int i = 0; i < projectile.oldRot.Length; ++i)
            //    {
            //        if (projectile.oldRot[i] == 0) break;
            //        var factor = i / (float)projectile.oldRot.Length;
            //        var color = (1 - factor).GetLerpValue(Color.White, projColor) * (float)Math.Sqrt(1 - factor);
            //        //var lumi = 0.75f;
            //        //var satu = 1f;
            //        //                        var color = ((1 - factor + (float)ThisIsAHummerMod.ModTime / 10) % 1).GetLerpColor(Main.hslToRgb(0, satu, lumi), Main.hslToRgb(0.125f, satu, lumi), Main.hslToRgb(0.25f, satu, lumi), Main.hslToRgb(0.375f, satu, lumi), Main.hslToRgb(0.5f, satu, lumi), Main.hslToRgb(0.625f, satu, lumi), Main.hslToRgb(0.75f, satu, lumi), Main.hslToRgb(0.875f, satu, lumi), Main.hslToRgb(0, satu, lumi));//亮瞎眼特效（x
            //        var w = MathHelper.Lerp(1f, 0f, factor);
            //        bars.Add(new CustomVertexInfo(projectile.oldRot[i].ToRotationVector2() * (Main.expertMode ? 320 : 256) * 0.875f + projectile.Center, color, new Vector3(factor, 1, w)));
            //    }
            //    CustomVertexInfo info = new CustomVertexInfo(projectile.Center, Color.White, new Vector3(0, 0, 0));
            //    List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //    if (bars.Count > 1)
            //    {
            //        for (int i = 0; i < bars.Count - 1; i += 1)
            //        {
            //            triangleList.Add(bars[i]);
            //            triangleList.Add(info);
            //            triangleList.Add(bars[i + 1]);
            //        }
            //    }
            //    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
            //}
            #endregion

            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            if (autoAdditive)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            try
            {
                var alpha2 = MathHelper.Clamp(4 - Math.Abs(60 - projectile.timeLeft) / 15f, 0, 1);
                if (projectile.timeLeft >= 15 && projectile.timeLeft < 105)
                {
                    float alpha = MathHelper.Clamp(1 - Math.Abs(60 - projectile.timeLeft) / 45f, 0, 1);
                    RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    List<CustomVertexInfo> bars = [];
                    for (int i = 0; i < 30; i++)
                    {
                        var f = 1 - (i + 1) / 30f;
                        var v = projectile.oldRot[i].ToRotationVector2() * 256;
                        spriteBatch.Draw(projTex, projectile.oldPos[i] - (3 + projectile.frame * .5f) * v - Main.screenPosition, null, f.ArrayLerp(Color.Blue, Color.Cyan, Color.White) * (i == 0 ? alpha2 : alpha) * f * f, projectile.oldRot[i], new Vector2(15, 12), new Vector2(8f + 4 * projectile.frame, 3f) / new Vector2(120, 24) * new Vector2(36, 23), 0, 0);
                        bars.Add(new CustomVertexInfo(projectile.oldPos[i] - 2 * v, f.ArrayLerp(Color.Blue, Color.Cyan, Color.White), new Vector3(1 - f, 1, 3 * f / (3 * f + 1) * alpha)));//(3 * f - 4) / (4 * f - 3)
                        bars.Add(new CustomVertexInfo(projectile.oldPos[i] - (3 + projectile.frame * .5f) * v, f.ArrayLerp(Color.Blue, Color.Cyan, Color.White), new Vector3(0, 0, 0)));
                    }
                    DrawLightSword(Color.Cyan, new Vector2(8f + 4 * projectile.frame, 3f) * new Vector2(36, 23), alpha, false);
                    //Main.NewText(new Vector3(fac, MathHelper.Clamp(modPlayer.negativeDir ? (4 * fac - 3) : 4 * fac, 0, 1), modPlayer.negativeDir ? -1 : 1));
                    List<CustomVertexInfo> triangleList = [];
                    //spriteBatch.Draw(projTex, projectile.oldPos[0] - Main.screenPosition, null, Color.White * alpha, projectile.rotation + MathHelper.PiOver2 * projectile.ai[1], new Vector2(4, 12), new Vector2(8f, 3f) * alpha, 0, 0);
                    if (bars.Count >= 4)
                    {
                        for (int i = 0; i < bars.Count - 2; i += 2)
                        {
                            triangleList.Add(bars[i]);
                            triangleList.Add(bars[i + 2]);
                            triangleList.Add(bars[i + 1]);

                            triangleList.Add(bars[i + 1]);
                            triangleList.Add(bars[i + 2]);
                            triangleList.Add(bars[i + 3]);
                        }
                        var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                        var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                        LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                        LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTime"].SetValue(-(float)Main.time * 0.06f);
                        //Main.graphics.GraphicsDevice.Textures[0] = ModContent.GetTexture("IllusionBoundMod/Images/BaseTex");
                        //Main.graphics.GraphicsDevice.Textures[1] = ModContent.GetTexture("IllusionBoundMod/Images/AniTex");
                        Main.graphics.GraphicsDevice.Textures[0] = LogSpiralLibraryMod.BaseTex[0].Value;//_7
                        Main.graphics.GraphicsDevice.Textures[1] = LogSpiralLibraryMod.AniTex[11].Value;
                        Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                        Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                        LogSpiralLibraryMod.ShaderSwooshEffect.CurrentTechnique.Passes[1].Apply();
                        Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                        Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    }

                    //int count = 0;
                    //for (int n = 0; n < 30; n++)
                    //{
                    //    count++;
                    //    if (projectile.oldPos[n] == Vector2.Zero) break;
                    //}
                    //var pos1 = new Vector2[count];
                    //for (int n = 0; n < count; n++)
                    //{
                    //    pos1[n] = projectile.oldPos[n];
                    //}
                    //if (count > 4)
                    //{
                    //    var pos = pos1.CatMullRomCurve(count * 3);
                    //    count *= 4;
                    //    spriteBatch.DrawPath
                    //    (
                    //        t => pos[(int)(119 * t)],
                    //        t => t.GetLerpValue(Color.Blue, Color.Cyan, Color.White) * alpha,
                    //        IllusionBoundMod.ShaderSwoosh,//mod.GetEffect("Effects/EightTrigramsFurnaceEffect")
                    //        IllusionBoundMod.MaskColor[6],
                    //        IllusionBoundMod.MaskColor[10],
                    //        counts: count,
                    //        width: 64 * alpha,
                    //        kOfX: 8,
                    //        pass: "ShaderSwooshEffect_2",
                    //        alwaysDoSth: true
                    //    );
                    //}
                    //else
                    //{
                    //    spriteBatch.DrawPath
                    //    (
                    //        t => pos1[(int)(29 * t)],
                    //        t => t.GetLerpValue(Color.Blue, Color.Cyan, Color.White) * alpha,
                    //        IllusionBoundMod.ShaderSwoosh,
                    //        IllusionBoundMod.MaskColor[6],
                    //        IllusionBoundMod.MaskColor[10],
                    //        width: 64,
                    //        kOfX: 8,
                    //        pass: "ShaderSwooshEffect_2",
                    //        alwaysDoSth: true
                    //    );
                    //}
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
                else
                {
                    spriteBatch.Draw(projTex, projectile.position - Main.screenPosition, null, Color.White * alpha2, projectile.rotation, new Vector2(15, 12), new Vector2(8f + 4 * projectile.frame, 3f) / new Vector2(120, 24) * new Vector2(36, 23), 0, 0);
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.rotation.ToRotationVector2() * 256f * (1 + projectile.frame * .5f), 24, ref point) && projectile.timeLeft >= 15 && projectile.timeLeft < 105;// || Terraria.Utils.CenteredRectangle(projectile.oldPos[0], new Vector2(8)).Intersects(targetHitbox)
        }
    }
    public class ApeBossMissile : BigApeProj
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.scale = 1f;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("大猿人导弹");
        }
        public override bool PreKill(int timeLeft)
        {
            projectile.type = 140;
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int n = 0; n < 30; n++)
            {
                Dust.NewDustPerfect(projectile.Center, MyDustId.ElectricCyan, Main.rand.NextVector2Unit() * 2 + projectile.velocity * .125f).noGravity = true;
            }
            Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, ModContent.ProjectileType<ApeBossMissileExp>(), 10, 0, Main.myPlayer).hostile = true;
        }
        public Vector2 targetPos => projectile.frameCounter > 0 ? new Vector2(projectile.ai[0], projectile.ai[1]) : default;
        public override void AI()
        {
            Lighting.AddLight((int)((projectile.position.X + projectile.width / 2) / 16f), (int)((projectile.position.Y + projectile.height / 2) / 16f), 78f / 255f, 139f / 255f, 240f / 255f);
            projectile.frame++;
            //Dust dust = Dust.NewDustPerfect(projectile.Center, MyDustId.Smoke, new Vector2(0, 0), 0, Color.White, 1.5f);
            //dust.noGravity = true;
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            if (projectile.timeLeft == 120)
            {
                for (int n = projectile.oldPos.Length - 1; n > -1; n--)
                {
                    projectile.oldPos[n] = projectile.Center;
                }
            }
            if (projectile.frame > 15)
            {
                if (projectile.frameCounter > 0)
                {
                    projectile.frameCounter++;
                    projectile.timeLeft = 119;
                    var d = Vector2.Distance(targetPos, projectile.Center);
                    if (Main.GameUpdateCount % 60 == 0)
                    {
                        var d2 = -1f;
                        foreach (var p in Main.player)
                        {
                            if (p.active)
                            {
                                var c = (targetPos - p.Center).Length();
                                d2 = (int)d2 == -1 || c < d ? c : d2;
                            }
                        }
                        if (d2 > 512)
                        {
                            projectile.Kill();
                            //projectile.frameCounter = 1;
                            //var vec = player.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(64);
                            //projectile.ai[0] = vec.X;
                            //projectile.ai[1] = vec.Y;
                        }
                    }

                    Vector2 targetVec = targetPos - projectile.Center;
                    targetVec.Normalize();
                    targetVec *= MathHelper.Clamp((projectile.frameCounter - 15) / 2f, 0, 16);
                    //projectile.velocity = (projectile.velocity * MathHelper.Clamp(d, 32, 512) + targetVec) / (MathHelper.Clamp(d, 32, 512) + 1);
                    projectile.velocity = (projectile.velocity * 8 + targetVec) / 9;
                    if (d < 32f)
                    {
                        projectile.timeLeft = 1;
                        //projectile.width = 80;
                        //projectile.height = 80;
                    }
                }
                else
                {
                    float d = (int)projectile.localAI[0] == 1 ? 4096f : 512f;
                    Player target = null;
                    for (int n = 0; n < 256; n++)
                    {
                        var player = Main.player[n];
                        if (player.active)
                        {
                            float c = (player.Center - projectile.Center).Length();
                            if (c < d)
                            {
                                d = c;
                                target = player;
                            }
                        }
                    }
                    if (target != null)
                    {
                        Vector2 targetVec = target.Center - projectile.Center;
                        targetVec.Normalize();
                        targetVec *= 40f;
                        //Main.NewText(projectile.localAI[0]);
                        projectile.velocity = (int)projectile.localAI[0] == 1 ? (projectile.velocity * 32f + targetVec * (projectile.timeLeft / 105f)) / (32f + projectile.timeLeft / 105f) : (projectile.velocity * MathHelper.Clamp(d, 32, 512) + targetVec) / (MathHelper.Clamp(d, 32, 512) + 1);
                        if (d < 32f)
                        {
                            projectile.timeLeft = 1;
                            //projectile.width = 80;
                            //projectile.height = 80;
                        }
                    }
                }
            }
            else
            {
                if (projectile.frameCounter > 0)
                {
                    projectile.frameCounter++;
                }
                projectile.velocity *= 0.925f;
            }
            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
            {
                projectile.oldPos[n] = projectile.oldPos[n - 1];
                projectile.oldRot[n] = projectile.oldRot[n - 1];
            }
            projectile.oldPos[0] = projectile.Center;
            projectile.oldRot[0] = projectile.rotation;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            DrawingMethods.DrawShaderTail(spriteBatch, projectile, LogSpiralLibraryMod.HeatMap[7].Value, LogSpiralLibraryMod.AniTex[2].Value, LogSpiralLibraryMod.BaseTex[12].Value, 20);
            if (projectile.timeLeft > 1)
            {
                spriteBatch.Draw(projTex, projectile.Center - Main.screenPosition, projTex.Frame(2, 1, projectile.frame / 3 % 2, 0), Color.White, projectile.rotation, projTex.Size() / new Vector2(4, 2), 2, 0, 0);
            }

            if (projectile.frameCounter > 0)
            {
                spriteBatch.Draw(LogSpiralLibraryMod.MagicZone[3].Value, targetPos - Main.screenPosition, null, Color.Cyan with { A = 0 }, -(float)VirtualDreamMod.ModTime / 60 * MathHelper.Pi, new Vector2(200), MathHelper.Clamp(projectile.frameCounter / 120f, 0, .25f), 0, 0);
            }
            return false;
        }

    }
    public class ApeBossMissileExp : BigApeProj
    {
        public override string Texture => "VirtualDream/" + ApePath + "strawberry_7";
        protected float Size = 1;
        protected float Alpha = 1;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (targetHitbox.Center.ToVector2() - projectile.Center).Length() <= (55 * Size + 16);
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("大猿人导弹");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 1;
            projectile.timeLeft = 30;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.immuneTime = 8;
        }
        public override void AI()
        {
            //Player player = Main.player[projectile.owner];
            //projectile.Center = player.Center;
            projectile.ai[0] += 0.2f;
            Size = (float)Math.Sqrt(projectile.ai[0]);
            Alpha = (float)Math.Sin(Math.Pow(projectile.ai[0] / 6 - 1, 2) * MathHelper.Pi);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //spriteBatch.Draw(TextureAssets.Projectile[ModContent.ProjectileType<Items.Weapons.UniqueWeapon.ElectronHugeJadeBullet>()].Value, projectile.Center - Main.screenPosition, null, Color.White * Alpha, 0, new Vector2(128, 128), Size * .5f, SpriteEffects.None, 0);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "ElectronHugeJadeBullet"), projectile.Center - Main.screenPosition, null, Color.White with { A = 0 } * Alpha, 0, new Vector2(128, 128), Size * .5f, SpriteEffects.None, 0);

            return false;
        }
    }
    public class EnergyFist : BigApeProj
    {
        public override void SetDefaults()
        {
            //Projectiles.Minecraft.DiamondIsUnbreakable
            projectile.width = 32;
            projectile.height = 32;
            projectile.timeLeft = 300;
            projectile.DamageType = DamageClass.Melee;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 40;

        }

        private Vector2 spawnPos;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("能量拳");
        }
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void AI()
        {
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            if (projectile.frameCounter == 0)
            {
                projectile.frameCounter = 1;
                switch ((int)projectile.ai[0])
                {
                    case 1:
                        {
                            projectile.width = 320;
                            projectile.height = 320;
                            projectile.timeLeft = 960;
                            for (int n = projectile.oldPos.Length - 1; n >= 0; n--)
                            {
                                projectile.oldPos[n] = projectile.Center;
                            }
                            break;
                        }
                    case 2:
                        {
                            projectile.width = 32;
                            projectile.height = 32;
                            projectile.timeLeft = 900;
                            break;
                        }
                    case 3:
                        {
                            projectile.width = 320;
                            projectile.height = 320;
                            projectile.timeLeft = 300;
                            for (int n = projectile.oldPos.Length - 1; n >= 0; n--)
                            {
                                projectile.oldPos[n] = projectile.Center;
                            }
                            break;
                        }
                }
                spawnPos = projectile.Center;

            }
            switch ((int)projectile.ai[0])
            {
                case 1:
                    {
                        float s = 4f + MathHelper.Clamp(projectile.alpha, 0, 12);
                        projectile.alpha--;
                        Player player = Main.player[projectile.owner];
                        if (Vector2.Distance(projectile.velocity, Vector2.Zero) < 1)
                        {
                            projectile.velocity = new Vector2(8, 0).RotatedBy(projectile.rotation);
                        }
                        if (Vector2.Distance(projectile.velocity, Vector2.Zero) < 24)
                        {
                            projectile.velocity *= 1.02f;
                        }
                        Vector2 d = projectile.Center - player.Center;
                        if (projectile.timeLeft > 60)
                        {
                            if (d.X + projectile.velocity.X >= 960)
                            {
                                projectile.Center = new Vector2(player.Center.X + 960, projectile.Center.Y);
                                projectile.velocity.X = -projectile.oldVelocity.X;
                                projectile.velocity *= s / Vector2.Distance(projectile.velocity, Vector2.Zero);
                                if (projectile.alpha <= 0)
                                {
                                    for (float n = 0; n < 10; n++)
                                    {
                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(960, -560 * (1 - n / 10f) + 560 * n / 10f), new Vector2(-Main.rand.NextFloat(8, 16), 0), projectile.type, projectile.damage / 3, projectile.knockBack / 2, projectile.owner);
                                    }
                                    projectile.alpha = 30;
                                }
                            }
                            else if (d.X + projectile.velocity.X <= -960)
                            {
                                projectile.Center = new Vector2(player.Center.X - 960, projectile.Center.Y);
                                projectile.velocity.X = -projectile.oldVelocity.X;
                                projectile.velocity *= s / Vector2.Distance(projectile.velocity, Vector2.Zero);
                                if (projectile.alpha <= 0)
                                {
                                    for (float n = 0; n < 10; n++)
                                    {
                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(-960, -560 * (1 - n / 10f) + 560 * n / 10f), new Vector2(Main.rand.NextFloat(8, 16), 0), projectile.type, projectile.damage / 3, projectile.knockBack / 2, projectile.owner);
                                    }
                                    projectile.alpha = 30;
                                }
                            }
                            if (d.Y + projectile.velocity.Y >= 560)
                            {
                                projectile.Center = new Vector2(projectile.Center.X, player.Center.Y + 560);
                                projectile.velocity.Y = -projectile.oldVelocity.Y;
                                projectile.velocity *= s / Vector2.Distance(projectile.velocity, Vector2.Zero);
                                if (projectile.alpha <= 0)
                                {
                                    for (float n = 0; n < 10; n++)
                                    {
                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(-960 * (1 - n / 10f) + 960 * n / 10f, 560), new Vector2(0, -Main.rand.NextFloat(8, 16)), projectile.type, projectile.damage / 3, projectile.knockBack / 2, projectile.owner);
                                    }
                                    projectile.alpha = 30;
                                }
                            }
                            else if (d.Y + projectile.velocity.Y <= -560)
                            {
                                projectile.Center = new Vector2(projectile.Center.X, player.Center.Y - 560);
                                projectile.velocity.Y = -projectile.oldVelocity.Y;
                                projectile.velocity *= s / Vector2.Distance(projectile.velocity, Vector2.Zero);
                                if (projectile.alpha <= 0)
                                {
                                    for (float n = 0; n < 10; n++)
                                    {
                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(-960 * (1 - n / 10f) + 960 * n / 10f, -560), new Vector2(-Main.rand.NextFloat(8, 16), 0), projectile.type, projectile.damage / 3, projectile.knockBack / 2, projectile.owner);
                                    }
                                    projectile.alpha = 30;
                                }
                            }


                        }

                        //for (int n = 0; n < 10; n++) 
                        //{
                        //    var vec = Vector2.Normalize(projectile.velocity);
                        //    Dust.NewDustPerfect(projectile.Center + vec * -160 + new Vector2(-vec.Y, vec.X) * Main.rand.NextFloat(-160, 160), MyDustId.CyanBubble, projectile.velocity, 255 - (int)(MathHelper.Clamp(projectile.timeLeft / 60, 0, 1) * 255), Color.White, 3f).noGravity = true;

                        //}

                        break;
                    }
                case 0:
                    {
                        Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Unit() * 16, MyDustId.CyanBubble, projectile.velocity, 255 - (int)(MathHelper.Clamp(projectile.timeLeft / 60, 0, 1) * 255), Color.White).noGravity = true;
                        break;
                    }
                case 3:
                    {
                        if (projectile.velocity.LengthSquared() < 576f)
                        {
                            projectile.velocity *= 1.05f;

                        }
                        break;
                    }
            }
            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
            {
                projectile.oldPos[n] = projectile.oldPos[n - 1];
                projectile.oldRot[n] = projectile.oldRot[n - 1];
            }
            projectile.oldPos[0] = projectile.Center - Vector2.Normalize(projectile.velocity) * 0.75f;// * projectile.width / 2
            projectile.oldRot[0] = projectile.rotation;

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D hugeTex = VirtualDreamMod.GetTexture(ApePath + "EnergyFistHuge");
            switch ((int)projectile.ai[0])
            {
                case 0:
                case 2:
                    {
                        //if (projectile.timeLeft <= 60 )
                        //{
                        //    projectile.hostile = false;
                        //    var alpha = projectile.timeLeft / 60f;
                        //    //Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
                        //    //int frameHeight = projectileTexture.Height / Main.projFrames[projectile.type];
                        //    //for (int k = 1; k < 10; k++)
                        //    //{
                        //    //    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition;
                        //    //    Color color = Color.White * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                        //    //    spriteBatch.Draw(projectileTexture, drawPos, new Rectangle(0, frameHeight * projectile.frame, projectileTexture.Width, frameHeight), color * alpha, projectile.rotation, new Vector2(8), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
                        //    //}
                        //    for (int n = 0; n < 4; n++)
                        //        spriteBatch.Draw(projTex, projectile.Center + new Vector2(4 - 4 * projectile.timeLeft / 60f, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 30f * MathHelper.Pi) - Main.screenPosition, null, Color.White * alpha, projectile.rotation, new Vector2(8), 2, 0, 0);

                        //    //DrawItSelf(spriteBatch, new Vector2(4 - 4 * projectile.timeLeft / 60f, 0).RotatedBy(PiOver2 * n + projectile.timeLeft / 30f * Pi), alpha);
                        //}
                        //else 
                        //{
                        //    //Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
                        //    //int frameHeight = projectileTexture.Height / Main.projFrames[projectile.type];
                        //    //for (int k = 1; k < 10; k++)
                        //    //{
                        //    //    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition;
                        //    //    Color color = Color.White * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                        //    //    spriteBatch.Draw(projectileTexture, drawPos, new Rectangle(0, frameHeight * projectile.frame, projectileTexture.Width, frameHeight), color, projectile.rotation, new Vector2(8), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
                        //    //}
                        //    //IllusionBoundExtensionMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, 70, alpha: 1, additive: true);

                        //    for (int n = 0; n < 4; n++)
                        //        spriteBatch.Draw(projTex, projectile.Center + new Vector2(8, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 15f * MathHelper.Pi) - Main.screenPosition, null, Color.White * .5f, projectile.rotation, new Vector2(8), 2, 0, 0);
                        //    spriteBatch.Draw(projTex, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(8), 2, 0, 0);

                        //}

                        projectile.hostile = !(projectile.timeLeft <= 30 || projectile.timeLeft >= 270);
                        var alpha1 = MathHelper.Clamp(150 - Math.Abs(150 - projectile.timeLeft), 0, 30) / 60f;
                        //IllusionBoundExtensionMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, 900, alpha: alpha1 * 2, additive: true);
                        for (int n = 0; n < 4; n++)
                        {
                            spriteBatch.Draw(projTex, projectile.Center + new Vector2(4 * alpha1, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 60f * MathHelper.Pi) - Main.screenPosition, null, Color.White with { A = 0 } * alpha1, projectile.rotation, new Vector2(8), 2, 0, 0);
                        }

                        spriteBatch.Draw(projTex, projectile.Center - Main.screenPosition, null, Color.White with { A = 0 } * alpha1, projectile.rotation, new Vector2(8), 2, 0, 0);
                        break;
                    }
                case 1:
                    {
                        projectile.hostile = !(projectile.timeLeft <= 60 || projectile.timeLeft >= 900);
                        var alpha1 = MathHelper.Clamp(480 - Math.Abs(480 - projectile.timeLeft), 0, 60) / 120f;
                        DrawingMethods.DrawShaderTail(spriteBatch, projectile, LogSpiralLibraryMod.HeatMap[7].Value, LogSpiralLibraryMod.AniTex[2].Value, LogSpiralLibraryMod.BaseTex[12].Value, 900, alpha: alpha1 * 2, additive: true);
                        //DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, 900, alpha: alpha1 * 2, additive: true);
                        for (int n = 0; n < 4; n++)
                        {
                            spriteBatch.Draw(hugeTex, projectile.Center + new Vector2(32 * alpha1, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 60f * MathHelper.Pi) - Main.screenPosition, null, Color.White with { A = 0 } * alpha1, projectile.rotation, new Vector2(160), 1, 0, 0);
                        }

                        spriteBatch.Draw(hugeTex, projectile.Center - Main.screenPosition, null, Color.White with { A = 0 } * alpha1, projectile.rotation, new Vector2(160), 1, 0, 0);
                        break;
                    }
                case 3:
                    {
                        if (projectile.timeLeft >= 240)
                        {
                            if (projectile.velocity != default)
                            {
                                var fac = (300 - projectile.timeLeft) / 60f;
                                //fac = (float)Math.Sin(MathHelper.Pi * Math.Sqrt(fac));
                                var unit = Vector2.Normalize(projectile.velocity) * 2560;
                                spriteBatch.DrawEffectLine_StartAndEnd(spawnPos - unit, spawnPos + unit, Color.Cyan, LogSpiralLibraryMod.AniTex[1].Value, 1 - fac, 1 - fac, 512 * fac);
                            }
                            //try
                            //{

                            //}
                            //catch (Exception e) 
                            //{
                            //    Main.NewText(e);
                            //    Main.NewText(spawnPos);
                            //    Main.NewText(projectile.velocity);
                            //}
                            //spriteBatch.DrawEffectLine((spawnPos, Vector2.Normalize(projectile.velocity)), Color.Cyan, 1 - 512 * (300 - projectile.timeLeft) / 60f, 1 - 512 * (300 - projectile.timeLeft) / 60f, 2000, 512 * (300 - projectile.timeLeft) / 60f, 0, 1);
                        }
                        projectile.hostile = !(projectile.timeLeft <= 60 || projectile.timeLeft >= 240);
                        var alpha1 = MathHelper.Clamp(150 - Math.Abs(150 - projectile.timeLeft), 0, 60) / 120f;
                        DrawingMethods.DrawShaderTail(spriteBatch, projectile, LogSpiralLibraryMod.HeatMap[7].Value, LogSpiralLibraryMod.AniTex[2].Value, LogSpiralLibraryMod.AniTex[2].Value, 900, alpha: alpha1 * 4, additive: false);

                        //DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, 900, ShaderTailMainStyle.MiddleLine2, alpha: alpha1);
                        for (int n = 0; n < 4; n++)
                        {
                            spriteBatch.Draw(hugeTex, projectile.Center + new Vector2(32 * alpha1, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 60f * MathHelper.Pi) - Main.screenPosition, null, Color.White with { A = 0 } * alpha1 * .5f, projectile.rotation, new Vector2(160), 1, 0, 0);
                        }

                        spriteBatch.Draw(hugeTex, projectile.Center - Main.screenPosition, null, Color.White with { A = 0 } * alpha1, projectile.rotation, new Vector2(160), 1, 0, 0);

                        break;
                    }
            }
            return false;
        }

    }
    public class LightPellet : BigApeProj
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("能量光团");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 4;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 300)
            {
                for (int n = projectile.oldPos.Length - 1; n >= 0; n--)
                {
                    projectile.oldPos[n] = projectile.Center;
                }
            }
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }

            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
            {
                projectile.oldPos[n] = projectile.oldPos[n - 1];
                projectile.oldRot[n] = projectile.oldRot[n - 1];
            }
            projectile.oldPos[0] = projectile.Center;
            projectile.oldRot[0] = projectile.rotation;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.timeLeft <= 60)
            {
                projectile.hostile = false;
                var alpha = projectile.timeLeft / 60f;

                Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
                int frameHeight = projectileTexture.Height / Main.projFrames[projectile.type];
                for (int k = 1; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition;
                    Color color = Color.White * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(projectileTexture, drawPos, new Rectangle(0, frameHeight * projectile.frame, projectileTexture.Width, frameHeight), color with { A = 0 } * alpha, projectile.rotation, new Vector2(4, 2), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
                }
                //IllusionBoundExtensionMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, 6, alpha: alpha);
                for (int n = 0; n < 4; n++)
                {
                    spriteBatch.Draw(projTex, projectile.Center + new Vector2(4 - 4 * projectile.timeLeft / 60f, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 30f * MathHelper.Pi) - Main.screenPosition, projTex.Frame(2, 1, (int)Main.GameUpdateCount / 4 % 2, 0), Color.White with { A = 0 } * alpha, projectile.rotation, new Vector2(4, 2), 2, 0, 0);
                }

                //DrawItSelf(spriteBatch, new Vector2(4 - 4 * projectile.timeLeft / 60f, 0).RotatedBy(PiOver2 * n + projectile.timeLeft / 30f * Pi), alpha);
            }
            else
            {
                Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
                int frameHeight = projectileTexture.Height / Main.projFrames[projectile.type];
                for (int k = 1; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition;
                    Color color = Color.White * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(projectileTexture, drawPos, new Rectangle(0, frameHeight * projectile.frame, projectileTexture.Width, frameHeight), color with { A = 0 }, projectile.rotation, new Vector2(4, 2), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
                }
                for (int n = 0; n < 4; n++)
                {
                    spriteBatch.Draw(projTex, projectile.Center + new Vector2(2, 0).RotatedBy(MathHelper.PiOver2 * n + projectile.timeLeft / 30f * MathHelper.Pi) - Main.screenPosition, projTex.Frame(2, 1, (int)Main.GameUpdateCount / 4 % 2, 0), Color.White with { A = 0 } * .5f, projectile.rotation, new Vector2(4, 2), 2, 0, 0);
                }
                //IllusionBoundExtensionMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.StarDust, ShaderTailStyle.Dust2, 6);
                spriteBatch.Draw(projTex, projectile.Center - Main.screenPosition, projTex.Frame(2, 1, (int)Main.GameUpdateCount / 4 % 2, 0), Color.White with { A = 0 }, projectile.rotation, new Vector2(4, 2), 2, 0, 0);
            }
            return false;
        }
    }
    public class LightDagger : BigApeProj
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("光之匕首");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 8;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
        }
        public Player target => Main.player[projectile.frameCounter];
        public override void AI()
        {

            switch ((int)projectile.ai[0])
            {
                case 0:
                    {
                        if (projectile.velocity != Vector2.Zero)
                        {
                            projectile.rotation = projectile.velocity.ToRotation();
                        }

                        break;
                    }
                case 1:
                    {
                        if (projectile.timeLeft == 300)
                        {
                            for (int n = projectile.oldPos.Length - 1; n > -1; n--)
                            {
                                projectile.oldPos[n] = projectile.Center;
                            }
                        }
                        if (projectile.timeLeft > 240)
                        {
                            projectile.velocity *= 0.95f;
                            if (projectile.velocity != Vector2.Zero)
                            {
                                projectile.rotation += MathHelper.Clamp(16 / projectile.velocity.LengthSquared(), 0, MathHelper.Pi / 6f);
                            }
                            //if (projectile.velocity != Vector2.Zero) projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Clamp(256 / projectile.velocity.LengthSquared(), 0, MathHelper.Pi / 6f) - MathHelper.Clamp(81 / projectile.oldVelocity.LengthSquared(), 0, MathHelper.Pi / 6f);
                        }
                        else if (projectile.timeLeft == 240)
                        {
                            projectile.velocity = Vector2.Normalize(target.Center - projectile.Center) * 4;
                            //projectile.velocity = Vector2.Normalize(target.Center + target.velocity * 30 - projectile.Center) * 4;
                        }
                        else
                        {
                            if (projectile.velocity.LengthSquared() < 256)
                            {
                                projectile.velocity *= 1.05f;
                            }
                            else if (projectile.velocity.LengthSquared() > 256)
                            {
                                projectile.velocity = Vector2.Normalize(projectile.velocity) * 32;
                            }

                            if (projectile.velocity != Vector2.Zero)
                            {
                                projectile.rotation = projectile.velocity.ToRotation();
                            }
                        }
                        for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 1];
                            projectile.oldRot[n] = projectile.oldRot[n - 1];
                        }
                        projectile.oldPos[0] = projectile.Center + projectile.rotation.ToRotationVector2() * 36;
                        projectile.oldRot[0] = projectile.rotation;
                        break;
                    }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            //* MathHelper.Clamp((300 - projectile.timeLeft) / 30f, 0, 1)
            if ((int)projectile.ai[0] == 1)
            {
                int count = 0;
                for (int n = 0; n < 30; n++)
                {
                    count++;
                    if (projectile.oldPos[n] == Vector2.Zero)
                    {
                        break;
                    }
                }
                var pos1 = new Vector2[count];
                for (int n = 0; n < count; n++)
                {
                    pos1[n] = projectile.oldPos[n];
                }
                //                spriteBatch.DrawPath
                //(
                //    pos1,
                //    t => t.GetLerpValue(Color.Blue, Color.Cyan, Color.White),
                //    mod.GetEffect("Effects/EightTrigramsFurnaceEffect"),
                //    IllusionBoundMod.MaskColor[6],
                //    IllusionBoundMod.MaskColor[10],
                //    doSth: (v, t) => spriteBatch.Draw(projTex, projectile.oldPos[t] - projectile.oldRot[t].ToRotationVector2() * 32 - Main.screenPosition, null, (t == 0 ? Color.White : (Color.Cyan * 0.8f)) * (1 - t), projectile.oldRot[t], new Vector2(18, 11.5f), 2 * (t == 0 ? 1 : 0.8f * (1 - t)), 0, 0),
                //    alwaysDoSth: true
                //);
                if (count > 4)
                {
                    var pos = pos1.CatMullRomCurve(count * 3);
                    count *= 4;
                    spriteBatch.DrawPath
                    (
                        t => pos[Math.Clamp((int)((count * 4 - 1) * t), 0, Math.Min(count * 4 - 1, 1))],
                        t => t.ArrayLerp(Color.Blue, Color.Cyan, Color.White),
                        LogSpiralLibraryMod.EightTrigramsFurnaceEffect,
                        LogSpiralLibraryMod.BaseTex[8].Value,
                        LogSpiralLibraryMod.AniTex[10].Value,
                        counts: count,
                        doSth: (v, t) =>
                        {
                            int num = (int)(119 * t);
                            if (num % 4 == 0)
                            {
                                spriteBatch.Draw(projTex, projectile.oldPos[num / 4] - projectile.oldRot[num / 4].ToRotationVector2() * 32 - Main.screenPosition,
                                    null, (t == 0 ? Color.White : (Color.Cyan * 0.8f)) * (1 - t), projectile.oldRot[num / 4], new Vector2(18, 11.5f), 2 * (t == 0 ? 1 : 0.8f * (1 - t)), 0, 0);
                            }
                        },
                        alwaysDoSth: true
                    );
                }
                else
                {
                    spriteBatch.DrawPath
                    (
                        t => pos1[Math.Clamp((int)((count - 1) * t), 0, Math.Min(count - 1, 1))],
                        t => t.ArrayLerp(Color.Blue, Color.Cyan, Color.White),
                        LogSpiralLibraryMod.EightTrigramsFurnaceEffect,
                        LogSpiralLibraryMod.BaseTex[8].Value,
                        LogSpiralLibraryMod.AniTex[10].Value,
                        doSth: (v, t) => spriteBatch.Draw(projTex, projectile.oldPos[(int)(29 * t)] - projectile.oldRot[(int)(29 * t)].ToRotationVector2() * 32 - Main.screenPosition, null, (t == 0 ? Color.White : (Color.Cyan * 0.8f)) * (1 - t), projectile.oldRot[(int)(29 * t)], new Vector2(18, 11.5f), 2 * (t == 0 ? 1 : 0.8f * (1 - t)), 0, 0),
                        alwaysDoSth: true
                    );
                }
            }
            else
            {
                spriteBatch.Draw(projTex, projectile.Center - Main.screenPosition, null, Color.White with { A = 0 }, projectile.rotation, new Vector2(18, 11.5f), 2, 0, 0);
                //spriteBatch.Draw(TextureAssets.MagicPixel.Value, projectile.Center + projectile.rotation.ToRotationVector2() * 36 - Main.screenPosition, new Rectangle(0, 0, 1, 1), Color.Red, 0, new Vector2(0.5f), 4, 0, 0);
            }
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class LightDagger_2 : BigApeProj
    {
        public override string Texture => "VirtualDream/" + ApePath + "LightDagger";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("光之匕首");
        }
        public float attackFac => MathHelper.Clamp(300 - Math.Abs(300 - projectile.timeLeft), 0, 120) / 120f;
        public bool attackAble => projectile.timeLeft >= 120 && projectile.timeLeft <= 480;
        public Vector2 GetVec(int t, int n, int i) => ((t / 300f * projectile.localAI[0] * MathHelper.TwoPi + MathHelper.Pi / 3 * n) * (i % 2 * 2 - 1)).ToRotationVector2() * 256 * attackFac * i + projectile.Center;
        public Vector2 this[int n, int i] => GetVec(projectile.timeLeft, n, i);
        //{
        //    get 
        //    {
        //        Main.NewText("!!!");
        //        return 
        //    }
        //}
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (!attackAble)
            {
                return false;
            }
            else
            {
                for (int n = 0; n < 6; n++)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if (targetHitbox.Intersects(Terraria.Utils.CenteredRectangle(this[n, i], new Vector2(16, 16))))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 8;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
        }
        public NPC owner => Main.npc[projectile.frameCounter];
        public Player target => Main.player[projectile.frame];
        public override void AI()
        {
            if (owner == null || !owner.active || owner.type != ModContent.NPCType<BigApe>())
            {
                projectile.Kill();
            }
            if (projectile.timeLeft == 480)
            {
                for (int n = 0; n < 6; n++)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        for (int k = 0; k < 30; k++)
                        {
                            var v = (MathHelper.TwoPi / 30 * k).ToRotationVector2();
                            Dust.NewDustPerfect(this[n, i] + v * 32, MyDustId.CyanBubble, -4 * v, 0, Color.White, Main.rand.NextFloat(0.5f, 2f)).noGravity = true;
                        }
                    }
                }
            }
            if (attackAble)
            {
                var vec = projectile.Center - target.Center;
                if (vec.Length() >= 1024)
                {
                    projectile.ai[0]++;
                    projectile.ai[1] += projectile.ai[0];
                    if (projectile.ai[0] > 60)
                    {
                        projectile.ai[0] = 0;
                        for (int n = 0; n < 8; n++)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                var v = (MathHelper.TwoPi / 3f * (i + (float)VirtualDreamMod.ModTime2 / 60f)).ToRotationVector2();
                                Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center + (MathHelper.PiOver4 * n).ToRotationVector2() * 256 + new Vector2(-v.Y, v.X) * 64, v * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
                            }
                        }

                    }
                    while (projectile.ai[1] > 200)
                    {
                        projectile.ai[1] -= 200;
                        for (int i = 0; i < 3; i++)
                        {
                            var v = (MathHelper.TwoPi / 3f * (i + (float)VirtualDreamMod.ModTime2 / 60f)).ToRotationVector2();
                            Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center + new Vector2(-v.Y, v.X) * 256, v * 32, ModContent.ProjectileType<LightDagger>(), 35, 0, Main.myPlayer, 1);
                        }
                        target.velocity += Vector2.Normalize(vec) * 8;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int n = 0; n < 6; n++)
            {
                for (int i = 1; i < 5; i++)
                {
                    spriteBatch.Draw(projTex, this[n, i] - Main.screenPosition, null, Color.White with { A = 0 } * (MathHelper.Clamp(300 - Math.Abs(300 - projectile.timeLeft), 0, 120) / 120f), (projectile.timeLeft / 300f * MathHelper.TwoPi * projectile.localAI[0] + MathHelper.Pi / 3 * n + MathHelper.PiOver2 * (attackFac * 3 + (1 - attackFac) * 2)) * (i % 2 * 2 - 1), new Vector2(18, 11.5f), (attackAble ? 2 : 1) * (i == 4 ? 1.5f : 1f), 0, 0);
                    spriteBatch.DrawPath
                    (
                        t => GetVec(projectile.timeLeft + (int)(t * 5 * projectile.localAI[0]), n, i),
                        t => (1 - t).ArrayLerp(Color.White, Color.Cyan, Color.Blue, Color.Purple) * attackFac,
                        LogSpiralLibraryMod.EightTrigramsFurnaceEffect,
                        LogSpiralLibraryMod.BaseTex[8].Value,
                        LogSpiralLibraryMod.AniTex[10].Value,
                        counts: (int)(5 * projectile.localAI[0]),
                        //widthFunc: t => (i == 4 ? 256 : 128) * (1 - (float)Math.Sqrt(t)),
                        width: i == 4 ? 32 : 16
                    //lightFunc: t => 1 - t
                    );
                }
            }
            return false;
        }
    }
    public class LaserTesseract : BigApeProj
    {
        public override string Texture => "VirtualDream/" + ApePath + "LightDagger";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("超立方体");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 8;
            projectile.timeLeft = 1080;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
        }
        public Vector2[] edgePoints;
        public double t => ((projectile.timeLeft) / 360.0) * MathHelper.Pi;////projectile.ai[0] / 60f * MathHelper.TwoPi//projectile.ai[0] > 240 ? (projectile.ai[0] > 300 ? projectile.ai[0] / 540f * MathHelper.TwoPi - MathHelper.Pi : MathHelper.Pi / 32400 * (projectile.ai[0] - 240) * (projectile.ai[0] - 240)) : 0;
        //projectile.ai[0] > 240 ? (projectile.ai[0] > 300 ? projectile.ai[0] / 540f * MathHelper.TwoPi - MathHelper.Pi : MathHelper.Pi / 32400 * (projectile.ai[0] - 240)* (projectile.ai[0] - 240)):0;
        public Vector2 tarCenter => Main.player[(int)projectile.ai[1]].Center;
        public Vector2 ownCenter => Main.npc[projectile.frameCounter].Center;
        public override void AI()
        {
            projectile.ai[0]++;
            //Main.NewText(this[(int)projectile.ai[0] % 16]);
            projectile.Center = ownCenter;
            //Main.NewText(tarCenter);
        }
        #region 旧的绘制函数
        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    //Vector4[] pointList = new Vector4[] { this[0], this[1], this[3], this[7], this[6], this[14], this[10], this[11], this[15] };
        //    var c = projectile.Center - Main.screenPosition;
        //    var pC = Main.screenPosition + new Vector2(960, 512) - projectile.Center;
        //    const float hZ = 3000;
        //    const float hW = 4000;
        //    Vector4[] pointList = new Vector4[] { this[0], this[1], this[3], this[7], this[6], this[2], this[10], this[8], this[9], this[1], this[5], this[7], this[15], this[11], this[9], this[13], this[5], this[4], this[6], this[14], this[15], this[13], this[12], this[14], this[10], this[11], this[3], this[2], this[0], this[4], this[12], this[8], this[0] };
        //    if (projectile.ai[0] < 240)
        //    {
        //        float factor = projectile.ai[0] / 240f;
        //        var p = factor.GetLerpValue(pointList);
        //        Color color = Color.White;
        //        for (int i = -1; i < 2; i += 2)
        //        {
        //            for (int n = 0; n < (int)(factor * 32); n++)
        //            {
        //                spriteBatch.DrawLine((pointList[n] + new Vector4(8)) * i, (pointList[n + 1] + new Vector4(8)) * i, color, hZ, hW, 4, false, c, pC);
        //            }
        //            spriteBatch.DrawLine((pointList[(int)(factor * 32)] + new Vector4(8)) * i, (p + new Vector4(8)) * i, color, hZ, hW, 4, false, c, pC);
        //            if ((int)projectile.ai[0] == 239)
        //            {
        //                for (int n = 0; n < 32; n++)
        //                {
        //                    IllusionBoundExtensionMethods.LinerDust((pointList[n] + new Vector4(8)) * i, (pointList[n + 1] + new Vector4(8)) * i, 2000, 1000,
        //                        d =>
        //                        {
        //                            d.noGravity = true;
        //                            d.velocity = Main.rand.NextVector2Unit();
        //                        }
        //                        , c + Main.screenPosition, pC, MyDustId.CyanBubble);
        //                }
        //            }
        //            var cen = new Vector2(-66 * i, -18);
        //            spriteBatch.DrawLine(new Vector4(cen, 0, 0), (p + new Vector4(8)) * i, color, hZ, hW, 4 * (1 - factor), false, c, pC);
        //        }

        //    }
        //    else
        //    {
        //        //for (int i = -1; i < 2; i++)
        //        //{
        //        //    const float hZ = 2000;
        //        //    const float hW = 1000;
        //        //    Color color = Color.White;
        //        //    for (int n = 0; n < 32; n++)
        //        //    {
        //        //        spriteBatch.DrawLine((pointList[n] + new Vector4(8)) * i, (pointList[n + 1] + new Vector4(8)) * i, color, hZ, hW, 4, false, projectile.Center - Main.screenPosition, tarCenter - ownCenter);
        //        //        //var cen = ownCenter + new Vector2(-66 * i, -18);
        //        //        //spriteBatch.DrawLine(new Vector4(cen, 0, 0), pointList[n] * i, color, hZ, hW, 4, false, projectile.Center, tarCenter);
        //        //    }
        //        //}

        //        //const float hZ = 1000;
        //        //const float hW = 2000;
        //        //Color color = Color.White;
        //        //for (int n = 0; n < 32; n++)
        //        //{
        //        //    spriteBatch.DrawLine(pointList[n], pointList[n + 1], color, hZ, hW, 4, false, projectile.Center - Main.screenPosition, tarCenter - ownCenter);
        //        //}
        //        var projPos = new Vector2[33];
        //        for (int n = 0; n < 33; n++)
        //        {
        //            projPos[n] = pointList[n].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
        //        }
        //        var lines = new (Vector2, Vector2)[32];

        //        //Color color = Color.White;
        //        //var s = pointList[0].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
        //        //for (int n = 0; n < 32; n++)
        //        //{
        //        //    lines[n].Item1 = s;
        //        //    lines[n].Item2 = pointList[n + 1].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
        //        //    s = lines[n].Item2;
        //        //    //spriteBatch.DrawLine(pointList[n], pointList[n + 1], color, hZ, hW, 4, false, projectile.Center - Main.screenPosition, tarCenter - ownCenter);
        //        //}
        //        for (int n = 0; n < 32; n++)
        //        {
        //            lines[n].Item1 = projPos[n];
        //            lines[n].Item2 = projPos[n + 1];
        //            //spriteBatch.DrawLine(pointList[n], pointList[n + 1], color, hZ, hW, 4, false, projectile.Center - Main.screenPosition, tarCenter - ownCenter);
        //        }
        //        var v = (MathHelper.Clamp(540 - Math.Abs(540 - projectile.ai[0]), 0, 120) / 120f);
        //        spriteBatch.DrawEffectLine_StartAndEnd(lines, Color.Cyan * v, 1, 1, 16 * v, 1);
        //        spriteBatch.End();
        //        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
        //        for (int n = 0; n < 256; n++)
        //        {
        //            spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Fairy.LightJadeBullet>()], ((n / 128f) % 1).GetLerpValue(projPos) - Main.screenPosition, new Rectangle(128, 0, 32, 32), Color.White * v, (float)Main.time / 60f * MathHelper.TwoPi, new Vector2(16), 1.5f, 0, 0);// + (float)Main.time / 6000f
        //        }
        //        spriteBatch.End();
        //        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //        //spriteBatch.DrawEffectLine_StartAndEnd(new (Vector2, Vector2)[] { (Main.screenPosition, Main.screenPosition + new Vector2(1920, 1120)) }, Color.Cyan, 1, 1, 16, 1);
        //    }
        //    //spriteBatch.DrawLine(projectile.Center, new Vector2(256, 0), Color.White, 4, true, -Main.screenPosition);
        //    return false;
        //}
        #endregion
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var c = projectile.Center - Main.screenPosition;
            var pC = Main.screenPosition + new Vector2(960, 512) - projectile.Center;
            const float hZ = 3000;
            const float hW = 4000;
            LoopArray<Vector4> pointList = new(new Vector4[] { this[0], this[1], this[3], this[7], this[6], this[2], this[10], this[8], this[9], this[1], this[5], this[7], this[15], this[11], this[9], this[13], this[5], this[4], this[6], this[14], this[15], this[13], this[12], this[14], this[10], this[11], this[3], this[2], this[0], this[4], this[12], this[8] });
            if (projectile.ai[0] < 240)
            {
                float factor = projectile.ai[0] / 240f;
                var p = factor.ArrayLerp_Loop(pointList.array);
                Color color = Color.White;
                for (int i = -1; i < 2; i += 2)
                {
                    for (int n = 0; n < (int)(factor * 32); n++)
                    {
                        spriteBatch.DrawLine((pointList[n] + new Vector4(8)) * i, (pointList[n + 1] + new Vector4(8)) * i, color, hZ, hW, 4, false, c, pC);
                    }
                    spriteBatch.DrawLine((pointList[(int)(factor * 32)] + new Vector4(8)) * i, (p + new Vector4(8)) * i, color, hZ, hW, 4, false, c, pC);
                    if ((int)projectile.ai[0] == 239)
                    {
                        for (int n = 0; n < 32; n++)
                        {
                            MiscMethods.LinerDust((pointList[n] + new Vector4(8)) * i, (pointList[n + 1] + new Vector4(8)) * i, hZ, hW,
                                d =>
                                {
                                    d.noGravity = true;
                                    d.velocity = Main.rand.NextVector2Unit();
                                }
                                , c + Main.screenPosition, pC, MyDustId.CyanBubble);
                        }
                    }
                    var cen = new Vector2(-66 * i, -18);
                    spriteBatch.DrawLine(new Vector4(cen, 0, 0), (p + new Vector4(8)) * i, color, hZ, hW, 4 * (1 - factor), false, c, pC);
                }

            }
            else
            {
                var projPos = new LoopArray<Vector2>(new Vector2[32]);
                for (int n = 0; n < 32; n++)
                {
                    projPos[n] = pointList[n].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
                }
                var v = (MathHelper.Clamp(540 - Math.Abs(540 - projectile.ai[0]), 0, 120) / 120f);

                try
                {

                    //if (Math.Abs((float)Math.Sin(t)) <= 0.994f && Math.Abs((float)Math.Cos(t)) <= 0.994f && Math.Abs((float)Math.Sin(t)) >= 0.01f && Math.Abs((float)Math.Cos(t)) >= 0.01f) 
                    //{
                    //    #region Outside
                    //    if (!Main.gamePaused)
                    //    {
                    //        var projPos2 = new LoopArray<Vector2>(new Vector2[16]);
                    //        for (int n = 0; n < 16; n++)
                    //        {
                    //            projPos2[n] = this[n].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
                    //        }
                    //        edgePoints = projPos2.array.EdgePoints();

                    //    }
                    //    spriteBatch.DrawOutSide(edgePoints);
                    //                            //float left = Main.screenPosition.X;
                    //    //float right = Main.screenPosition.X + 1920;
                    //    //float bottom = Main.screenPosition.Y;
                    //    //float top = Main.screenPosition.Y + 1120;

                    //    ////Vector2[] targetPoints = new Vector2[4];
                    //    ////foreach (var vec in edgePoints)
                    //    ////{
                    //    ////    if (vec.X < left) 
                    //    ////    {
                    //    ////        left = vec.X;
                    //    ////        targetPoints[0] = vec;
                    //    ////    }
                    //    ////    if (vec.X > right) right = vec.X;
                    //    ////    if (vec.Y < bottom) bottom = vec.Y;
                    //    ////    if (vec.Y > top) top = vec.Y;
                    //    ////}
                    //    //var edgePointsNative = edgePoints.CloneArray();
                    //    //binaryWriter.Write("PreVertex");

                    //    //Vector2[] targetPoints = IllusionBoundExtensionMethods.GetVertexPoints(ref edgePointsNative);//edgePoints.GetVertexPoints();
                    //    //binaryWriter.Write("PostVertex");

                    //    //if (targetPoints == null) return false;

                    //    //if (targetPoints[0].X - 200 < left) left = targetPoints[0].X - 200;
                    //    //if (targetPoints[1].Y + 200 > top) top = targetPoints[1].Y + 200;
                    //    //if (targetPoints[2].X + 200 > right) right = targetPoints[2].X + 200;
                    //    //if (targetPoints[3].Y - 200 < bottom) bottom = targetPoints[3].Y - 200;

                    //    ////CustomVertexInfo[] vertexs = new CustomVertexInfo[edgePoints.Length + 4];
                    //    ////LoopArray<CustomVertexInfo> vertexs = new LoopArray<CustomVertexInfo>(new CustomVertexInfo[edgePointsNative.Length + 4]);
                    //    //LoopArray<CustomVertexInfo> vertexs = new LoopArray<CustomVertexInfo>(new CustomVertexInfo[edgePointsNative.Length]);
                    //    //LoopArray<CustomVertexInfo> vertexs2 = new LoopArray<CustomVertexInfo>(new CustomVertexInfo[4]);

                    //    //List<CustomVertexInfo> vertexInfos = new List<CustomVertexInfo>();
                    //    //binaryWriter.Write("PreTri");

                    //    //var l = edgePointsNative.Length;
                    //    //for (int n = 0; n < l; n++)
                    //    //{
                    //    //    vertexs[n] = edgePointsNative[n].VertexInScreen(Color.Cyan, 0.5f);
                    //    //}
                    //    //vertexs2[0] = new Vector2(left, top).VertexInScreen(Color.Cyan, 0.5f);
                    //    //vertexs2[1] = new Vector2(right, top).VertexInScreen(Color.Cyan, 0.5f);
                    //    //vertexs2[2] = new Vector2(right, bottom).VertexInScreen(Color.Cyan, 0.5f);
                    //    //vertexs2[3] = new Vector2(left, bottom).VertexInScreen(Color.Cyan, 0.5f);
                    //    //var connecttingVertex = vertexs2[3];
                    //    //List<int> indexList = new List<int>();

                    //    //for (int n = 0; n < l; n++)
                    //    //{
                    //    //    int index = -1;
                    //    //    for (int i = 0; i < 4; i++)
                    //    //    {
                    //    //        if (targetPoints[i] == edgePointsNative[n] && !indexList.Contains(i))
                    //    //        {
                    //    //            index = i;
                    //    //            indexList.Add(i);
                    //    //            break;
                    //    //        }
                    //    //    }
                    //    //    if (index == -1)
                    //    //    {
                    //    //        vertexInfos.Add(vertexs[n - 1]);
                    //    //        vertexInfos.Add(vertexs[n]);
                    //    //        vertexInfos.Add(connecttingVertex);
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        //vertexInfos.Add(connecttingVertex);
                    //    //        //vertexInfos.Add(vertexs[n]);
                    //    //        //connecttingVertex = vertexs[l + index];
                    //    //        //vertexInfos.Add(connecttingVertex);
                    //    //        vertexInfos.Add(vertexs[n - 1]);
                    //    //        vertexInfos.Add(vertexs[n]);
                    //    //        vertexInfos.Add(connecttingVertex);

                    //    //        vertexInfos.Add(connecttingVertex);
                    //    //        vertexInfos.Add(vertexs[n]);
                    //    //        connecttingVertex = vertexs2[index];
                    //    //        vertexInfos.Add(connecttingVertex);
                    //    //    }
                    //    //}
                    //    //binaryWriter.Write("PostTri");

                    //    //spriteBatch.End();
                    //    //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                    //    //Effect effect = IllusionBoundMod.ShaderSwoosh;
                    //    //RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                    //    //var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    //    //var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                    //    //effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                    //    //effect.Parameters["uTime"].SetValue(0);
                    //    //Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.MaskColor[6];
                    //    //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.MaskColor[4];
                    //    //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    //    //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    //    //Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                    //    //effect.CurrentTechnique.Passes[0].Apply();
                    //    //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexInfos.ToArray(), 0, vertexInfos.Count / 3);
                    //    //Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    //    //spriteBatch.End();
                    //    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    //    #endregion
                    //}
                    //FileStream fileStream = new FileStream(@"D:\\TestTesseract.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //BinaryWriter binaryWriter = new BinaryWriter(fileStream);

                    //if (!Main.gamePaused)
                    //{
                    var projPos2 = new LoopArray<Vector2>(new Vector2[16]);
                    for (int n = 0; n < 16; n++)
                    {
                        projPos2[n] = this[n].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
                    }
                    //binaryWriter.Write("PreEdge");
                    //binaryWriter.Flush();
                    var wtfpoints = projPos2.array.DelRepeatData().ToList().CalcConvexHull().ToArray().ClockwiseSorting();
                    edgePoints = wtfpoints ?? edgePoints;//projPos2.array.DelRepeatData().EdgePoints()

                    //binaryWriter.Write("PostEdge");
                    //binaryWriter.Flush();
                    //}

                    if (edgePoints == null)
                    {
                        return false;
                    }

                    spriteBatch.DrawOutSide(edgePoints, v);
                    ////binaryWriter.Write("PreDraw");
                    ////binaryWriter.Flush();

                    //CustomVertexInfo[] vertexInfos = new CustomVertexInfo[edgePoints.Length * 3 - 6];
                    //for (int n = 0; n < edgePoints.Length - 2; n++)
                    //{
                    //    vertexInfos[3 * n] = edgePoints[0].VertexInScreen(Color.Cyan);
                    //    vertexInfos[3 * n + 1] = edgePoints[n + 1].VertexInScreen(Color.Cyan);
                    //    vertexInfos[3 * n + 2] = edgePoints[n + 2].VertexInScreen(Color.Cyan);
                    //}
                    //spriteBatch.End();
                    //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                    //Effect effect = IllusionBoundMod.ShaderSwoosh;
                    //RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                    //var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    //var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                    //effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                    //effect.Parameters["uTime"].SetValue(0);
                    //Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.MaskColor[6];
                    //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.MaskColor[4];
                    //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    //Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                    //effect.CurrentTechnique.Passes[0].Apply();
                    //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexInfos, 0, edgePoints.Length - 2);
                    //Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    //spriteBatch.End();
                    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

                    //binaryWriter.Write("PostDraw");
                    //binaryWriter.Flush();
                    //binaryWriter.Close();
                    //fileStream.Close();
                }
                catch
                {
                    return false;
                }
                var lines = new (Vector2, Vector2)[32];
                for (int n = 0; n < 32; n++)
                {
                    lines[n].Item1 = projPos[n];
                    lines[n].Item2 = projPos[n + 1];
                }

                spriteBatch.DrawEffectLine_StartAndEnd(lines, Color.Cyan * v, LogSpiralLibraryMod.AniTex[1].Value, 1, 1, 16 * v);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int n = 0; n < 256; n++)
                {
                    spriteBatch.Draw(LogSpiralLibraryMod.Misc[19].Value, ((n / 128f) % 1).ArrayLerp_Loop(projPos.array) - Main.screenPosition, new Rectangle(128, 0, 32, 32), Color.White * v, (float)Main.time / 60f * MathHelper.TwoPi, new Vector2(16), 1.5f, 0, 0);// + (float)Main.time / 6000f
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            //var c = projectile.Center - Main.screenPosition;
            if (projectile.ai[0] <= 240 || projectile.ai[0] > 960)
            {
                return false;
            }

            var pC = Main.screenPosition + new Vector2(960, 512) - projectile.Center;
            const float hZ = 3000;
            const float hW = 4000;
            var projPos = new Vector2[33];
            Vector4[] pointList = new Vector4[] { this[0], this[1], this[3], this[7], this[6], this[2], this[10], this[8], this[9], this[1], this[5], this[7], this[15], this[11], this[9], this[13], this[5], this[4], this[6], this[14], this[15], this[13], this[12], this[14], this[10], this[11], this[3], this[2], this[0], this[4], this[12], this[8], this[0] };
            for (int n = 0; n < 33; n++)
            {
                projPos[n] = pointList[n].Projectile(hW, new Vector3(pC, 0)).Projectile(hZ, pC) + projectile.Center;
            }
            for (int n = 0; n < 256; n++)
            {
                if (Terraria.Utils.CenteredRectangle(((n / 128f) % 1).ArrayLerp(projPos), new Vector2(24)).Intersects(targetHitbox))
                {
                    return true;//+ (float)Main.time / 6000f
                }
            }
            return false;
        }
        public Matrix Transform
        {
            get
            {
                var _t = t;
                var c = (float)Math.Cos(_t);
                var s = (float)Math.Sin(_t);
                //int offset = 0;
                //while (Math.Abs(c) >= 0.95f || Math.Abs(s) >= 0.95f) 
                //{
                //    offset++;
                //    _t = (projectile.timeLeft + offset) / 360.0 * MathHelper.Pi;
                //    c = (float)Math.Cos(_t);
                //    s = (float)Math.Sin(_t);
                //}
                //Main.NewText((c, s));
                return
                new Matrix
                (
                    c, 0, -s, 0,
                    0, 1, 0, 0,
                    s, 0, c, 0,
                    0, 0, 0, 1
                )
                * new Matrix
                (
                    1, 0, 0, 0,
                    0, c, -s, 0,
                    0, s, c, 0,
                    0, 0, 0, 1
                );
            }
        }
        public Vector4 this[int index] => (new Vector4(index / 8 % 2, index / 4 % 2, index / 2 % 2, index % 2) * 2 - new Vector4(1)).ApplyMatrix(Transform) * 384;
    }
    public class StrawberryCrystal : ModItem
    {
        public override string Texture => "VirtualDream/" + ApePath + "strawberry_7";
        public override void SetDefaults()
        {
            item.width = 43;
            item.height = 67;
            //item.useAnimation = 120;
            //item.useTime = 120;
            item.useAnimation = 24;
            item.useTime = 24;
            item.useStyle = ItemUseStyleID.HoldUp;
            item.autoReuse = true;
            //item.shoot = ModContent.ProjectileType<StrawberryCross>();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = default;
            position = Main.MouseWorld;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        //public VertexTriangle3_RigidList vt3rl;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("召唤 大猿人");//测试物品\n消灭鼠标附近4格以内的NPC
            // DisplayName.SetDefault("草莓水晶");
        }
        //public readonly static Vector4[] vertexs = new Vector4[16];
        public StrawberryCrystal()
        {
            //for (int n = 0; n < vertexs.Length; n++) 
            //{
            //    vertexs[n] = GetVector(n);
            //}
            //var vtr = new VertexTriangle3_Rigid(default, default, null, new Vector3[] { new Vector3(0, 0, 1), new Vector3(0, 1, 1), new Vector3(1, 0, 1) }, new Color[] { Color.Red, Color.Red, Color.Red }, 1);
            //var vtr2 = vtr;
            //vtr2.vertexs[0] = new Vector3(1, 1, 1);
            //vt3rl = new VertexTriangle3_RigidList(2000, vtr, vtr2);
        }
        public override void HoldStyle(Player player, Rectangle rectangle)
        {
            for (int n = 0; n < 15; n++)
            {
                Dust.NewDustPerfect(Main.MouseWorld + (MathHelper.TwoPi / 15 * n + Main.GameUpdateCount / 60).ToRotationVector2() * 64, MyDustId.RedBubble).noGravity = true;
            }
        }
        //private static Vector4 GetVector(int index) 
        //{
        //    return new Vector4(index / 8, index / 4 % 2, index / 2 % 2, index % 2) * 2 - Vector4.One;
        //}
        private static (Vector4 v, Vector4 u) GetVector(int index, float scaler = 128)
        {
            var v = new Vector4(index / 8, index / 4 % 2, index / 2 % 2, index % 2);
            var u = v;
            v -= Vector4.One * .5f;
            v *= scaler;
            var t = Main.time / 300 * MathHelper.TwoPi;//IllusionBoundMod.ModTime
            var c = (float)Math.Cos(t);
            var s = (float)Math.Sin(t);
            var matrix =
            new Matrix
            (
                c, 0, -s, 0,
                0, 1, 0, 0,
                s, 0, c, 0,
                0, 0, 0, 1
            )
            //*new Matrix
            //(
            //    c, 0, 0, -s,
            //    0, c, -s, 0,
            //    0, s, c, 0,
            //    s, 0, 0, c
            //)
            //* new Matrix
            //(
            //    1, 0, 0, 0,
            //    0, c, 0, -s,
            //    0, 0, 1, 0,
            //    0, s, 0, c
            //)
            * new Matrix
            (
                1, 0, 0, 0,
                0, c, -s, 0,
                0, s, c, 0,
                0, 0, 0, 1
            );
            //new Matrix
            //(
            //    c,-s,-s,-c,
            //    s, c, c,-s,
            //   -c, c, s, c,
            //    s,-c,-c, s
            //);
            v = v.ApplyMatrix(matrix);
            return (v, u);
        }

        //public static int NextPow(int min, int max, int times, bool aMax = false)
        //{
        //    if (times > 0) return aMax ? NextPow(Main.rand.Next(min, max), max, times - 1, aMax) : NextPow(min, Main.rand.Next(min, max), times - 1, aMax);
        //    return Main.rand.Next(min, max);
        //}
        private Player player => Main.LocalPlayer;//Main.player[item.owner]
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //Main.NewText("!!!");
            if (player == null || !player.active || player.HeldItem != item)
            {
                return true;
            }

            const float heightZ = 512;
            const float heightW = 256;
            const float width = 4;
            var c = player.Center - Main.screenPosition;
            var pC = Main.screenPosition + Main.ScreenSize.ToVector2() * .5f - player.Center;
            //for (int n = 0; n < 15; n++) 
            //{
            //    var v = GetVector(n);
            //    if ((int)v.W == -1) spriteBatch.DrawLine(v, new Vector4(0, 0, 0, 2), Color.White, heightZ, heightW, width, true);
            //    if ((int)v.Z == -1) spriteBatch.DrawLine(v, new Vector4(0, 0, 2, 0), Color.White, heightZ, heightW, width, true);
            //    if ((int)v.Y == -1) spriteBatch.DrawLine(v, new Vector4(0, 2, 0, 0), Color.White, heightZ, heightW, width, true);
            //    if ((int)v.X == -1) spriteBatch.DrawLine(v, new Vector4(2, 0, 0, 0), Color.White, heightZ, heightW, width, true);
            //}
            //int counter = 0;
            for (int n = 0; n < 15; n++)
            {
                var (v, u) = GetVector(n);
                if ((int)u.W == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 1).v, Main.hslToRgb(0, 1, 0.75f), heightZ, heightW, width, false, c, pC);
                }

                if ((int)u.Z == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 2).v, Main.hslToRgb(0, 0.75f, 0.75f), heightZ, heightW, width, false, c, pC);
                }

                if ((int)u.Y == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 4).v, Main.hslToRgb(0, 0.75f, 0.5f), heightZ, heightW, width, false, c, pC);
                }

                if ((int)u.X == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 8).v, Main.hslToRgb(0, 0.5f, 0.5f), heightZ, heightW, width, false, c, pC);
                }
            }
            //for (int n = 0; n < 15; n++)
            //{
            //    var (v, u) = GetVector(n);
            //    if ((int)u.W == 0) { spriteBatch.DrawLine(v, GetVector(n + 1).v, Main.hslToRgb(0, 1, 0.75f), heightZ, heightW, width, false, c, pC); counter++; }
            //    if ((int)u.Z == 0) { spriteBatch.DrawLine(v, GetVector(n + 2).v, Main.hslToRgb(0, 0.75f, 0.75f), heightZ, heightW, width, false, c, pC); counter++; }
            //    if ((int)u.Y == 0) { spriteBatch.DrawLine(v, GetVector(n + 4).v, Main.hslToRgb(0, 0.75f, 0.5f), heightZ, heightW, width, false, c, pC); counter++; }
            //    if ((int)u.X == 0) { spriteBatch.DrawLine(v, GetVector(n + 8).v, Main.hslToRgb(0, 0.5f, 0.5f), heightZ, heightW, width, false, c, pC); counter++; }
            //}
            //Main.NewText(counter);
            //vt3rl.Update
            //    (

            //    );
            //spriteBatch.Draw3DPlane(IllusionBoundMod.ShaderSwoosh, IllusionBoundMod.GetTexture(Path + "StrawBerryArea"), IllusionBoundMod.MaskColor[6], vt3rl);


            var rotation = (float)VirtualDreamMod.ModTime / 60 * MathHelper.Pi;
            var sin = (float)Math.Sin(rotation);
            var cos = (float)Math.Cos(rotation);
            //var (cos, sin) = MathF.SinCos();
            cos *= 64; sin *= 64;

            var cs = new CustomVertexInfo[6];
            cs[0] = new CustomVertexInfo(new Vector2(-cos, -sin), new Vector3(0, 0, 1));
            cs[1] = new CustomVertexInfo(new Vector2(-sin, cos), new Vector3(0, 1, 1));
            cs[2] = new CustomVertexInfo(new Vector2(sin, -cos), new Vector3(1, 0, 1));
            cs[3] = cs[0];
            cs[3].TexCoord = new Vector3(1, 1, 1);
            cs[3].Position *= -1;
            cs[4] = cs[1];
            cs[5] = cs[2];
            for (int n = 0; n < 6; n++)
            {
                cs[n].Position += Main.MouseWorld;
                cs[n].Color = Color.Red;
            }
            //var vtr = new VertexTriangle(new Vector3(0, 0, 1), new Vector3(0, 1, 1), new Vector3(1, 0, 1), Color.Red, Color.Red, Color.Red, new Vector2(-cos, -sin), new Vector2(-cos, sin), new Vector2(cos, -sin));
            //var vtr2 = vtr;
            //vtr2.vertexs[0] = new Vector3(1, 1, 1);
            //vtr2.positions[1] *= -1;
            //spriteBatch.DrawPlane(IllusionBoundMod.ShaderSwoosh, IllusionBoundMod.GetTexture(Path + "StrawBerryArea"), IllusionBoundMod.MaskColor[6], new VertexTriangleList(Main.MouseWorld, vtr, vtr2));
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new()
            {
                CullMode = CullMode.None
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTransform"].SetValue(model  * projection);//* Main.GameViewMatrix.TransformationMatrix
            LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea");
            Main.graphics.GraphicsDevice.Textures[1] = LogSpiralLibraryMod.BaseTex[8].Value;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            LogSpiralLibraryMod.ShaderSwooshEffect.CurrentTechnique.Passes[0].Apply();
            //var css = new VertexTriangleList(Main.MouseWorld, vtr, vtr2).ToVertexInfo();
            //Main.NewText(css[0].Position);
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, cs, 0, 2);
            for (int n = 0; n < 6; n++)
            {
                cs[n].Position -= Main.MouseWorld;
                cs[n].Position *= Main.mouseLeft ? 2 : 3;
                cs[n].Position.Y *= -1;
                cs[n].Position += Main.MouseWorld;
                cs[n].Color = Color.Red * (Main.mouseLeft ? .75f : .5f);
            }
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, cs, 0, 2);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            return true;
        }

        private Item item => Item;
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            const float heightZ = 512;
            const float heightW = 256;
            const float width = 4;
            var c = item.Center - Main.screenPosition;
            var pC = Main.screenPosition + new Vector2(960, 512) - item.Center;
            for (int n = 0; n < 15; n++)
            {
                var (v, u) = GetVector(n);
                if ((int)u.W == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 1).v, Main.hslToRgb(0, 1, 0.75f), heightZ, heightW, width, false, c, pC);
                }

                if ((int)u.Z == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 2).v, Main.hslToRgb(0, 0.75f, 0.75f), heightZ, heightW, width, false, c, pC);
                }

                if ((int)u.Y == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 4).v, Main.hslToRgb(0, 0.75f, 0.5f), heightZ, heightW, width, false, c, pC);
                }

                if ((int)u.X == 0)
                {
                    spriteBatch.DrawLine(v, GetVector(n + 8).v, Main.hslToRgb(0, 0.5f, 0.5f), heightZ, heightW, width, false, c, pC);
                }
            }
            return true;
        }

        //public override bool UseItem(Player player)
        //{
        //    //if((int)Main.time % 60 == 0)
        //    //Projectile.NewProjectile(Main.MouseWorld, default, ModContent.ProjectileType<StrawberryCross>(), 50, 0, Main.myPlayer);
        //    //foreach (var n in Main.npc)
        //    //{
        //    //    if (n.active && Vector2.Distance(Main.MouseWorld, n.Center) < 64)
        //    //    {
        //    //        n.life -= int.MaxValue;
        //    //        n.checkDead();
        //    //    }
        //    //}
        //    if(player.itemAnimation == 1)
        //    NPC.NewNPC((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, ModContent.NPCType<BigApe>());
        //    //Projectile.NewProjectile(player.Center + new Vector2(1920 * (player.itemAnimation - 60) / 120f, 0), default, ModContent.ProjectileType<StrawberryCross>(), 50, 0, Main.myPlayer);
        //    //Projectile.NewProjectile(player.Center + new Vector2(1920 * player.itemAnimation / 120f, -256), default, ModContent.ProjectileType<StrawberryCross>(), 50, 0, Main.myPlayer);
        //    return base.UseItem(player);
        //}

        //public override void UseStyle(Player player)
        //{
        //    foreach (var n in Main.npc)
        //    {
        //        if (n.active && Vector2.Distance(Main.MouseWorld, n.Center) < 64)
        //        {
        //            n.life -= int.MaxValue;
        //            n.checkDead();
        //        }
        //    }
        //}
    }
    //public class StrawberryCrystal_1 : ModItem
    //{
    //    public override string Texture => "VirtualDream" + Path + "strawberry_7";
    //    public override void SetDefaults()
    //    {
    //        item.width = 43;
    //        item.height = 67;
    //        item.useAnimation = 2;
    //        item.useTime = 2;
    //        item.useStyle = ItemUseStyleID.HoldUp;
    //        item.autoReuse = true;
    //    }
    //    public override void SetStaticDefaults()
    //    {
    //        Tooltip.SetDefault("测试物品\n翻转整个世界");
    //        DisplayName.SetDefault("草莓水晶_1");
    //    }
    //    public override bool UseItem(Player player)
    //    {
    //        var tileInfo = new Tile[Main.maxTilesX, Main.maxTilesY];
    //        for (int i = 0; i < Main.maxTilesX; i++) 
    //        {
    //            for (int j = 0; j < Main.maxTilesY; j++)
    //            {
    //                //var tile = Main.tile[Main.maxTilesX - i - 1, Main.maxTilesY - j - 1];
    //                tileInfo[i, j] = Main.tile[Main.maxTilesX - i - 1, Main.maxTilesY - j - 1];
    //            }
    //        }
    //        for (int i = 0; i < Main.maxTilesX; i++)
    //        {
    //            for (int j = 0; j < Main.maxTilesY; j++)
    //            {
    //                Main.tile[i, j] = tileInfo[i, j];
    //            }
    //        }
    //        return true;
    //    }
    //    //public class TileInfo : Tile 
    //    //{

    //    //}
    //}
    public class StrawberryCross : BigApeProj
    {
        public override string Texture => "VirtualDream/" + ApePath + "strawberry_7";

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.timeLeft > 240)
            {
                return false;
            }

            var fac = MathHelper.Clamp(120 - Math.Abs(120 - projectile.timeLeft), 0, 15) / 15f;
            if (Terraria.Utils.CenteredRectangle(projectile.Center, new Vector2(1 + fac, 0.8f) * 32 * projectile.ai[0]).Intersects(targetHitbox))
            {
                return true;
            }

            var rectangle = Terraria.Utils.CenteredRectangle(projectile.Center, new Vector2(0.8f, 1 + 2 * fac) * 32 * projectile.ai[0]);
            rectangle.Y += (int)(48 * projectile.ai[0] * fac);
            return rectangle.Intersects(targetHitbox);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Main.NewText("!!!");
            CustomVertexInfo[] vertexs = new CustomVertexInfo[17];
            for (int n = 0; n < vertexs.Length; n++)
            {
                vertexs[n] = default;
                //vertexs[n].Color = Main.hslToRgb((n / 17f ) % 1, 1, 0.5f);//+ (float)IllusionBoundMod.ModTime / 60f
                vertexs[n].Color = Color.Red;
            }
            Vector3[] indexs = new Vector3[]
            {
                new(0,1,15),
                new(1,15,14),
                new(1,2,14),
                new(2,16,14),
                new(2,3,5),
                new(2,5,6),
                new(2,6,16),
                new(3,4,5),
                new(6,7,9),
                new(6,9,10),
                new(6,16,10),
                new(7,8,9),
                new(10,16,14),
                new(10,13,14),
                new(10,11,13),
                new(11,12,13)
            };
            //IllusionBoundExtensionMethods.DrawShaderTail
            vertexs[0].Position = new Vector2(1.5f, 0);
            vertexs[1].Position = new Vector2(1f, 0.4f);
            vertexs[2].Position = new Vector2(0.5f, 0.5f);
            vertexs[3].Position = new Vector2(0.4f, 1f);
            vertexs[4].Position = new Vector2(0, 1.5f);
            vertexs[5].Position = new Vector2(-0.4f, 1f);
            vertexs[6].Position = new Vector2(-0.5f, 0.5f);
            vertexs[7].Position = new Vector2(-1f, 0.4f);
            vertexs[8].Position = new Vector2(-1.5f, 0);
            vertexs[9].Position = new Vector2(-1f, -0.4f);
            vertexs[10].Position = new Vector2(-0.5f, -0.5f);
            vertexs[11].Position = new Vector2(-0.4f, -2f);
            vertexs[12].Position = new Vector2(0, -2.5f);
            vertexs[13].Position = new Vector2(0.4f, -2f);
            vertexs[14].Position = new Vector2(0.5f, -0.5f);
            vertexs[15].Position = new Vector2(1f, -0.4f);
            vertexs[16].Position = new Vector2(0, 0);

            vertexs[0].TexCoord = new Vector3(1, 0.5f, 0);
            vertexs[1].TexCoord = new Vector3(0.5f, 0.22f, 0.5f);
            vertexs[2].TexCoord = new Vector3(0.25f, 0.22f, 0.75f);
            vertexs[14].TexCoord = new Vector3(0.25f, 0.78f, 0.75f);
            vertexs[15].TexCoord = new Vector3(0.5f, 0.78f, 0.5f);
            vertexs[16].TexCoord = new Vector3(0, 0.5f, 1f);
            //vertexs[0].TexCoord = new Vector3(1, 0.5f, 1);
            //vertexs[1].TexCoord = new Vector3(0.5f, 0.22f, 1);
            //vertexs[2].TexCoord = new Vector3(0.25f, 0.22f, 1);
            //vertexs[14].TexCoord = new Vector3(0.25f, 0.78f, 1);
            //vertexs[15].TexCoord = new Vector3(0.5f, 0.78f, 1);

            vertexs[4].TexCoord = vertexs[8].TexCoord = vertexs[12].TexCoord = vertexs[0].TexCoord;
            vertexs[11].TexCoord = vertexs[9].TexCoord = vertexs[3].TexCoord = vertexs[1].TexCoord;
            vertexs[10].TexCoord = vertexs[2].TexCoord;
            vertexs[6].TexCoord = vertexs[14].TexCoord;
            vertexs[5].TexCoord = vertexs[13].TexCoord = vertexs[7].TexCoord = vertexs[15].TexCoord;
            //Main.NewText(vertexs[4].TexCoord);

            //float factor = MathHelper.Clamp(60 - Math.Abs(60 - projectile.timeLeft), 0, 60) / 60f;
            //factor = factor * factor * factor;
            //float factor = 0.5f - 0.5f * (float)Math.Cos(MathHelper.TwoPi * projectile.timeLeft / 120f);
            float factor = MathHelper.Clamp(120 - Math.Abs(120 - projectile.timeLeft), 0, 15) / 15f;
            for (int n = 0; n < 17; n++)
            {
                //vertexs[n].Position *= MathHelper.Clamp(150 - Math.Abs(150 - projectile.timeLeft), 0, 60) / 60f * 32f;
                vertexs[n].TexCoord.Z *= factor;
                if (new int[] { 15, 0, 1 }.Contains(n))
                {
                    vertexs[n].Position.X = MathHelper.Lerp(0.5f, vertexs[n].Position.X, factor);
                }
                else if (new int[] { 7, 8, 9 }.Contains(n))
                {
                    vertexs[n].Position.X = MathHelper.Lerp(-0.5f, vertexs[n].Position.X, factor);

                }
                else if (new int[] { 3, 4, 5 }.Contains(n))
                {
                    vertexs[n].Position.Y = MathHelper.Lerp(0.5f, vertexs[n].Position.Y, factor);

                }
                else if (new int[] { 11, 12, 13 }.Contains(n))
                {
                    vertexs[n].Position.Y = MathHelper.Lerp(-0.5f, vertexs[n].Position.Y, factor);

                }
                vertexs[n].Position *= 32 * projectile.ai[0];
                vertexs[n].Position.Y *= -1;

                //vertexs[n].Position = vertexs[n].Position.RotatedBy(projectile.rotation);
                vertexs[n].Position += projectile.Center;
            }

            CustomVertexInfo[] tris = new CustomVertexInfo[48];
            for (int n = 0; n < 16; n++)
            {
                var index = indexs[n];
                tris[3 * n] = vertexs[(int)index.X];
                tris[3 * n + 1] = vertexs[(int)index.Y];
                tris[3 * n + 2] = vertexs[(int)index.Z];
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone,null,Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea"), projectile.Center - Main.screenPosition, null, new Color(255, 51, 51) * MathHelper.Clamp(factor + .5f, 0, 1), projectile.timeLeft / 60f * MathHelper.TwoPi, VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea").Size() * .5f, factor.Lerp(0.75f, 0.25f) * projectile.ai[0], 0, 0);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea"), projectile.Center - Main.screenPosition, null, new Color(255, 51, 51) * MathHelper.Clamp(1 - factor, 0, 1), -projectile.timeLeft / 60f * MathHelper.TwoPi, VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea").Size() * .5f, factor.Lerp(0.4f, 0.6f) * projectile.ai[0], 0, 0);
            if (projectile.timeLeft > 270 && projectile.frameCounter == 0)
            {
                (Vector2, Vector2)[] lines = new (Vector2, Vector2)[2];
                var fac = (300 - projectile.timeLeft) / 30f;
                for (int n = 0; n < 2; n++)
                {
                    //if (n == -1) 
                    //{
                    //    break;
                    //}
                    lines[n].Item2 = projectile.Center;
                    //lines[n].Item1 = Vector2.Lerp(Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18), projectile.Center, fac);
                    lines[n].Item1 = Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18);
                }
                spriteBatch.DrawEffectLine_StartAndEnd(lines, new Color(255, 51, 51) * fac, LogSpiralLibraryMod.AniTex[10].Value, 1, 1, 32 * (1 - fac), false);
            }
            if (projectile.timeLeft > 240 && projectile.frameCounter == 1)
            {
                (Vector2, Vector2)[] lines = new (Vector2, Vector2)[2];
                var fac = (255 - projectile.timeLeft) / 15f;
                for (int n = 0; n < 2; n++)
                {
                    //if (n == -1) 
                    //{
                    //    break;
                    //}
                    lines[n].Item2 = projectile.Center;
                    //lines[n].Item1 = Vector2.Lerp(Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18), projectile.Center, fac);
                    lines[n].Item1 = Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18);
                }
                spriteBatch.DrawEffectLine_StartAndEnd(lines, new Color(255, 51, 51) * fac, LogSpiralLibraryMod.AniTex[10].Value, 1, 1, 32 * (1 - fac), false);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new()
            {
                CullMode = CullMode.None,
                //FillMode = FillMode.WireFrame
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = LogSpiralLibraryMod.BaseTex[8].Value;
            Main.graphics.GraphicsDevice.Textures[1] = LogSpiralLibraryMod.AniTex[10].Value;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            LogSpiralLibraryMod.ShaderSwooshEffect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, tris, 0, 16);
            //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, new CustomVertexInfo[]
            //{
            //    vertexs[0],
            //    vertexs[1],
            //    vertexs[15]
            //}, 0, 1);
            //Main.NewText(vertexs[0].TexCoord, Color.Red);
            //Main.NewText(vertexs[1].TexCoord, Color.Cyan);
            //Main.NewText(vertexs[15].TexCoord, Color.Purple);

            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void AI()
        {
            if (projectile.frame == 0)
            {
                if (projectile.frameCounter == 1)
                {
                    projectile.timeLeft = 255;
                    projectile.frame = 1;
                }
            }

            if (projectile.timeLeft == 225)
            {
                SoundEngine.PlaySound(SoundID.Item12, projectile.Center);
            }
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 8;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("十字激光");
        }
    }
    public class StrawberryLaser : BigApeProj
    {
        //Bug 2
        public override string Texture => "VirtualDream/" + ApePath + "strawberry_7";
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.velocity * 3200f + projectile.Center, 8 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)), ref point);
        }

        private VertexTriangle3List loti;
        private Vector3 GetVec(Vector3 v, Vector3 size, float r) => (size * v).ApplyMatrix(Matrix.CreateRotationZ(projectile.ai[0]) * Matrix.CreateRotationX(r * (float)VirtualDreamMod.ModTime / 300f * MathHelper.TwoPi));//Main.time
        public void UpdateTris(float factor)
        {
            var size = new Vector3(20, 48 * factor, 48 * factor);
            loti.offset = projectile.Center;
            if (loti.tris == null)
            {
                NewTris(2000);
            }

            var vel = 1;
            loti.tris[0].positions[0] = GetVec(new Vector3(1, 1, -1), size, vel);
            loti.tris[1].positions[0] = GetVec(new Vector3(1, -1, 1), size, vel);
            loti.tris[0].positions[1] = loti.tris[1].positions[2] = GetVec(new Vector3(1, 1, 1), size, vel);
            loti.tris[0].positions[2] = loti.tris[1].positions[1] = GetVec(new Vector3(1, -1, -1), size, vel);
            for (int n = 0; n < 2; n++)
            {
                for (int i = 0; i < 3; i++)
                {
                    loti.tris[n].vertexs[i].Z = factor;
                }
            }
        }
        public void NewTris(float height)
        {
            VertexTriangle3[] tris = new VertexTriangle3[2];
            for (int n = 0; n < 2; n++)
            {
                tris[n] = new VertexTriangle3(default, default, default, default, default, default);
            }
            tris[0].vertexs[0] = new Vector3(0, 0, 1);
            tris[1].vertexs[0] = new Vector3(1, 1, 1);
            tris[0].vertexs[1] = tris[1].vertexs[2] = new Vector3(0, 1, 1);
            tris[0].vertexs[2] = tris[1].vertexs[1] = new Vector3(1, 0, 1);
            for (int n = 0; n < 2; n++)
            {
                for (int i = 0; i < 3; i++)
                {
                    tris[n].colors[i] = Color.Red;// Main.hslToRgb(0.8f, 0.75f, 0.75f)
                }
            }
            loti.tris = tris;
            loti.height = height;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var factor = (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f));
            var width = 300f * factor;
            var u = projectile.ai[0].ToRotationVector2();
            spriteBatch.DrawQuadraticLaser_PassNormal(projectile.Center, u, Color.Red, LogSpiralLibraryMod.AniTex[10].Value, 3200, width);//Main.hslToRgb(0.8f, 1, 0.75f)
            UpdateTris(factor);
            spriteBatch.Draw3DPlane(LogSpiralLibraryMod.ShaderSwooshEffect, VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea"), LogSpiralLibraryMod.BaseTex[8].Value, loti);//IllusionBoundMod.GetTexture("NPCs/BigApe/StrawBerryArea")//IllusionBoundMod.MagicZone[2]
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("四次元阳离子激光");
        }
        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.aiStyle = -1;
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            switch ((int)projectile.ai[1])
            {
                case 1:
                    {
                        projectile.Center += new Vector2(6f, 0);
                        break;
                    }
                case 2:
                    {
                        projectile.Center += new Vector2(-6f, 0);
                        break;
                    }
                case 3:
                    {
                        projectile.Center += new Vector2(0, 6f);
                        break;
                    }
                case 4:
                    {
                        projectile.Center += new Vector2(0, -6f);
                        break;
                    }
                case 5:
                    {
                        projectile.ai[0] += MathHelper.Pi / 300;
                        break;
                    }
                case 6:
                    {
                        projectile.ai[0] -= MathHelper.Pi / 300;
                        break;
                    }
            }
            projectile.velocity = projectile.ai[0].ToRotationVector2();
            if (projectile.timeLeft == 300)
            {
                SoundEngine.PlaySound(SoundID.Item12, projectile.Center);
            }
        }
    }
    public class StrawberryCross_PS : BigApeProj
    {
        //Bug 2-1
        public override string Texture => "VirtualDream/" + ApePath + "strawberry_7";

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var fac = (1 - (float)Math.Cos(MathHelper.Pi * projectile.timeLeft / 60)) / 2;
            if (Terraria.Utils.CenteredRectangle(projectile.Center, new Vector2(1 + fac, 0.8f) * 32 * projectile.ai[0]).Intersects(targetHitbox))
            {
                return true;
            }

            var rectangle = Terraria.Utils.CenteredRectangle(projectile.Center, new Vector2(0.8f, 1 + 2 * fac) * 32 * projectile.ai[0]);
            rectangle.Y += (int)(48 * projectile.ai[0] * fac);
            return rectangle.Intersects(targetHitbox);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Main.NewText("!!!");
            CustomVertexInfo[] vertexs = new CustomVertexInfo[17];
            for (int n = 0; n < vertexs.Length; n++)
            {
                vertexs[n] = default;
                //vertexs[n].Color = Main.hslToRgb((n / 17f ) % 1, 1, 0.5f);//+ (float)IllusionBoundMod.ModTime / 60f
                vertexs[n].Color = Color.Red;
                //Main.NewText("??????");
            }
            Vector3[] indexs = new Vector3[]
            {
                new(0,1,15),
                new(1,15,14),
                new(1,2,14),
                new(2,16,14),
                new(2,3,5),
                new(2,5,6),
                new(2,6,16),
                new(3,4,5),
                new(6,7,9),
                new(6,9,10),
                new(6,16,10),
                new(7,8,9),
                new(10,16,14),
                new(10,13,14),
                new(10,11,13),
                new(11,12,13)
            };
            //IllusionBoundExtensionMethods.DrawShaderTail
            vertexs[0].Position = new Vector2(1.5f, 0);
            vertexs[1].Position = new Vector2(1f, 0.4f);
            vertexs[2].Position = new Vector2(0.5f, 0.5f);
            vertexs[3].Position = new Vector2(0.4f, 1f);
            vertexs[4].Position = new Vector2(0, 1.5f);
            vertexs[5].Position = new Vector2(-0.4f, 1f);
            vertexs[6].Position = new Vector2(-0.5f, 0.5f);
            vertexs[7].Position = new Vector2(-1f, 0.4f);
            vertexs[8].Position = new Vector2(-1.5f, 0);
            vertexs[9].Position = new Vector2(-1f, -0.4f);
            vertexs[10].Position = new Vector2(-0.5f, -0.5f);
            vertexs[11].Position = new Vector2(-0.4f, -2f);
            vertexs[12].Position = new Vector2(0, -2.5f);
            vertexs[13].Position = new Vector2(0.4f, -2f);
            vertexs[14].Position = new Vector2(0.5f, -0.5f);
            vertexs[15].Position = new Vector2(1f, -0.4f);
            vertexs[16].Position = new Vector2(0, 0);

            vertexs[0].TexCoord = new Vector3(1, 0.5f, 0);
            vertexs[1].TexCoord = new Vector3(0.5f, 0.22f, 0.5f);
            vertexs[2].TexCoord = new Vector3(0.25f, 0.22f, 0.75f);
            vertexs[14].TexCoord = new Vector3(0.25f, 0.78f, 0.75f);
            vertexs[15].TexCoord = new Vector3(0.5f, 0.78f, 0.5f);
            vertexs[16].TexCoord = new Vector3(0, 0.5f, 1f);
            //vertexs[0].TexCoord = new Vector3(1, 0.5f, 1);
            //vertexs[1].TexCoord = new Vector3(0.5f, 0.22f, 1);
            //vertexs[2].TexCoord = new Vector3(0.25f, 0.22f, 1);
            //vertexs[14].TexCoord = new Vector3(0.25f, 0.78f, 1);
            //vertexs[15].TexCoord = new Vector3(0.5f, 0.78f, 1);

            vertexs[4].TexCoord = vertexs[8].TexCoord = vertexs[12].TexCoord = vertexs[0].TexCoord;
            vertexs[11].TexCoord = vertexs[9].TexCoord = vertexs[3].TexCoord = vertexs[1].TexCoord;
            vertexs[10].TexCoord = vertexs[2].TexCoord;
            vertexs[6].TexCoord = vertexs[14].TexCoord;
            vertexs[5].TexCoord = vertexs[13].TexCoord = vertexs[7].TexCoord = vertexs[15].TexCoord;
            //Main.NewText(vertexs[4].TexCoord);

            //float factor = MathHelper.Clamp(60 - Math.Abs(60 - projectile.timeLeft), 0, 60) / 60f;
            //factor = factor * factor * factor;
            //float factor = 0.5f - 0.5f * (float)Math.Cos(MathHelper.TwoPi * projectile.timeLeft / 120f);
            float factor = (1 - (float)Math.Cos(MathHelper.Pi * projectile.timeLeft / 60)) / 2;
            for (int n = 0; n < 17; n++)
            {
                //vertexs[n].Position *= MathHelper.Clamp(150 - Math.Abs(150 - projectile.timeLeft), 0, 60) / 60f * 32f;
                vertexs[n].TexCoord.Z *= factor;//MathHelper.Clamp(factor + .25f, 0, 1);
                if (new int[] { 15, 0, 1 }.Contains(n))
                {
                    vertexs[n].Position.X = MathHelper.Lerp(0.5f, vertexs[n].Position.X, factor);
                }
                else if (new int[] { 7, 8, 9 }.Contains(n))
                {
                    vertexs[n].Position.X = MathHelper.Lerp(-0.5f, vertexs[n].Position.X, factor);

                }
                else if (new int[] { 3, 4, 5 }.Contains(n))
                {
                    vertexs[n].Position.Y = MathHelper.Lerp(0.5f, vertexs[n].Position.Y, factor);

                }
                else if (new int[] { 11, 12, 13 }.Contains(n))
                {
                    vertexs[n].Position.Y = MathHelper.Lerp(-0.5f, vertexs[n].Position.Y, factor);

                }
                vertexs[n].Position *= 32 * projectile.ai[0];
                vertexs[n].Position.Y *= -1;

                //vertexs[n].Position = vertexs[n].Position.RotatedBy(projectile.rotation);
                vertexs[n].Position += projectile.Center;
            }

            CustomVertexInfo[] tris = new CustomVertexInfo[48];
            for (int n = 0; n < 16; n++)
            {
                var index = indexs[n];
                tris[3 * n] = vertexs[(int)index.X];
                tris[3 * n + 1] = vertexs[(int)index.Y];
                tris[3 * n + 2] = vertexs[(int)index.Z];
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea"), projectile.Center - Main.screenPosition, null, new Color(255, 51, 51) * factor, projectile.timeLeft / 60f * MathHelper.TwoPi, VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea").Size() * .5f, factor.Lerp(0.75f, 0.25f), 0, 0);
            spriteBatch.Draw(VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea"), projectile.Center - Main.screenPosition, null, new Color(255, 51, 51) * MathHelper.Clamp(factor * 2, 0, 1), -projectile.timeLeft / 60f * MathHelper.TwoPi, VirtualDreamMod.GetTexture(ApePath + "StrawBerryArea").Size() * .5f, factor.Lerp(0.4f, 0.6f), 0, 0);
            //if (projectile.timeLeft > 60) 
            //{
            //    (Vector2, Vector2)[] lines = new (Vector2, Vector2)[2];
            //    var fac = (120 - projectile.timeLeft) / 60f;
            //    for (int n = 0; n < 2; n++)
            //    {
            //        lines[n].Item2 = projectile.Center;
            //        lines[n].Item1 = Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18);
            //    }
            //    spriteBatch.DrawEffectLine_StartAndEnd(lines, new Color(255, 51, 51) * fac, 1, 1, 32 * (1 - fac), 10, false);
            //}

            //if (projectile.timeLeft > 270 && projectile.frameCounter == 0)
            //{
            //    (Vector2, Vector2)[] lines = new (Vector2, Vector2)[2];
            //    var fac = (300 - projectile.timeLeft) / 30f;
            //    for (int n = 0; n < 2; n++)
            //    {
            //        //if (n == -1) 
            //        //{
            //        //    break;
            //        //}
            //        lines[n].Item2 = projectile.Center;
            //        //lines[n].Item1 = Vector2.Lerp(Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18), projectile.Center, fac);
            //        lines[n].Item1 = Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18);
            //    }
            //    spriteBatch.DrawEffectLine_StartAndEnd(lines, new Color(255, 51, 51) * fac, 1, 1, 32 * (1 - fac), 10, false);
            //}
            //if (projectile.timeLeft == 241)
            //{
            //    SoundEngine.PlaySound(SoundID.Item, projectile.Center, 12);
            //}
            //if (projectile.timeLeft > 240 && projectile.frameCounter == 1)
            //{
            //    (Vector2, Vector2)[] lines = new (Vector2, Vector2)[2];
            //    var fac = (255 - projectile.timeLeft) / 15f;
            //    for (int n = 0; n < 2; n++)
            //    {
            //        //if (n == -1) 
            //        //{
            //        //    break;
            //        //}
            //        lines[n].Item2 = projectile.Center;
            //        //lines[n].Item1 = Vector2.Lerp(Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18), projectile.Center, fac);
            //        lines[n].Item1 = Main.npc[(int)projectile.ai[1]].Center + new Vector2(-66 * (n == 0 ? -1 : 1), -18);
            //    }
            //    spriteBatch.DrawEffectLine_StartAndEnd(lines, new Color(255, 51, 51) * fac, 1, 1, 32 * (1 - fac), 10, false);
            //}
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new()
            {
                CullMode = CullMode.None,
                //FillMode = FillMode.WireFrame
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            LogSpiralLibraryMod.ShaderSwooshEffect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = LogSpiralLibraryMod.BaseTex[8].Value;
            Main.graphics.GraphicsDevice.Textures[1] = LogSpiralLibraryMod.AniTex[10].Value;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            LogSpiralLibraryMod.ShaderSwooshEffect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, tris, 0, 16);
            //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, new CustomVertexInfo[]
            //{
            //    vertexs[0],
            //    vertexs[1],
            //    vertexs[15]
            //}, 0, 1);
            //Main.NewText(vertexs[0].TexCoord, Color.Red);
            //Main.NewText(vertexs[1].TexCoord, Color.Cyan);
            //Main.NewText(vertexs[15].TexCoord, Color.Purple);

            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void AI()
        {
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.width = projectile.height = 8;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = 0.2f;
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("十字激光");
        }
    }
    public enum BigApeEyeMode
    {
        None,
        Normal,
        Laser
    }
    //public class BigApeSurfaceBgStyle : ModSurfaceBgStyle
    //{
    //    public override bool ChooseBgStyle()
    //    {
    //        return !Main.gameMenu && NPC.AnyNPCs(ModContent.NPCType<BigApe>());
    //    }

    //    // Use this to keep far Backgrounds like the mountains.
    //    public override void ModifyFarFades(float[] fades, float transitionSpeed)
    //    {
    //        for (int i = 0; i < fades.Length; i++)
    //        {
    //            if (i == Slot)
    //            {
    //                fades[i] += transitionSpeed;
    //                if (fades[i] > 1f)
    //                {
    //                    fades[i] = 1f;
    //                }
    //            }
    //            else
    //            {
    //                fades[i] -= transitionSpeed;
    //                if (fades[i] < 0f)
    //                {
    //                    fades[i] = 0f;
    //                }
    //            }
    //        }
    //    }

    //    public override int ChooseFarTexture()
    //    {
    //        return mod.GetBackgroundSlot("Backgrounds/BigApeSurfaceFar");
    //    }

    //    //private static int SurfaceFrameCounter;
    //    //private static int SurfaceFrame;
    //    public override int ChooseMiddleTexture()
    //    {
    //        //if (++SurfaceFrameCounter > 12)
    //        //{
    //        //    SurfaceFrame = (SurfaceFrame + 1) % 4;
    //        //    SurfaceFrameCounter = 0;
    //        //}
    //        //switch (SurfaceFrame)
    //        //{
    //        //    case 0:
    //        //        return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid0");
    //        //    case 1:
    //        //        return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid1");
    //        //    case 2:
    //        //        return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid2");
    //        //    case 3:
    //        //        return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid3");
    //        //    default:
    //        //        return -1;
    //        //}
    //        return mod.GetBackgroundSlot("Backgrounds/BigApeSurfaceMiddle_0");
    //    }

    //    public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    //    {
    //        return mod.GetBackgroundSlot("Backgrounds/BigApeSurfaceClose");
    //    }
    //}
    public class BigApeVectorField : BigApeProj
    {
        //Bug 2-2
        public override string Texture => "VirtualDream/" + ApePath + "strawberry_9";
        public Vector2 baseOfX;
        public Vector2 baseOfY;
        public Texture2D VectorTex => TextureAssets.Projectile[projectile.type].Value;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("大猿人向量场");
        }
        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.width = 1;
            projectile.friendly = false;
            projectile.height = 1;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
        }
        private Player targetPlayer
        {
            get
            {
                Vector2 cen = projectile.Center;
                Player target = null;
                float distanceMax = 4096f;
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
        }
        private static Vector2 GetVec(Vector2 vec, float length)
        {
            return Vector2.Normalize(vec) * MathHelper.Clamp(vec.Length(), 0, length);
        }
        public override void AI()
        {
            if (projectile.timeLeft > 180)
            {
                baseOfX = GetVec(targetPlayer.Center - projectile.Center, 256f);
                baseOfY = GetVec(targetPlayer.velocity * 16f, 256f);
            }
            else
            {
                //for (int n = 0; n < 3; n++)
                if (projectile.timeLeft % (Main.expertMode ? 1 : 2) == 0)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + Main.rand.NextVector2Unit() * Main.rand.Next(64, Main.rand.Next(512, 1024)), default, ModContent.ProjectileType<VectorFieldCone>(), projectile.damage, 0, projectile.owner, projectile.whoAmI);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var al = MathHelper.Clamp((300 - projectile.timeLeft) / 30f, 0, 1);
            spriteBatch.Draw(VectorTex, projectile.Center - Main.screenPosition, null, Color.White * al, baseOfX.ToRotation(), new Vector2(0, VectorTex.Height / 2f), new Vector2(baseOfX.Length(), (float)Math.Sqrt(baseOfX.Length() / 4) * 4) / VectorTex.Size(), 0, 0);
            spriteBatch.Draw(VectorTex, projectile.Center - Main.screenPosition, null, Color.White * al, baseOfY.ToRotation(), new Vector2(0, VectorTex.Height / 2f), new Vector2(baseOfY.Length(), (float)Math.Sqrt(baseOfY.Length() / 4) * 4) / VectorTex.Size(), 0, 0);
            for (int i = -10; i < 11; i++)
            {
                for (int j = -10; j < 11; j++)
                {
                    var v = new Vector2(64 * i, 64 * j);
                    var vd = Vector2.Normalize(v.ApplyMatrix(baseOfX, baseOfY)) * 32f;
                    spriteBatch.Draw(VectorTex, projectile.Center - Main.screenPosition + v, null, Color.White * 0.5f, vd.ToRotation(), new Vector2(0, VectorTex.Height / 2f), new Vector2(vd.Length(), (float)Math.Sqrt(vd.Length() / 4) * 4) / VectorTex.Size(), 0, 0);
                }
            }
            return false;
        }
    }
    public class VectorFieldCone : BigApeProj
    {
        //public Effect effect;
        //Bug 2-3
        public override string Texture => "VirtualDream/" + ApePath + "LightDagger";

        private Projectile owner => Main.projectile[(int)projectile.ai[0]];

        private BigApeVectorField tap => owner.ModProjectile as BigApeVectorField;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var effect = LogSpiralLibraryMod.ShaderSwooshEffect;
            if (effect == null)
            {
                return false;
            }

            {
                List<CustomVertexInfo> bars = [];

                // 把所有的点都生成出来，按照顺序
                for (int i = 1; i < projectile.oldPos.Length; ++i)
                {
                    if (projectile.oldPos[i] == Vector2.Zero)
                    {
                        break;
                    }
                    //spriteBatch.Draw(TextureAssets.MagicPixel.Value, projectile.oldPos[i] - Main.screenPosition,
                    //    new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);

                    //int width = 30;
                    var normalDir = projectile.oldPos[i - 1] - projectile.oldPos[i];
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

                    var factor = i / (float)projectile.oldPos.Length;
                    var color = Color.Lerp(Color.White, Color.Red, factor);
                    var f2 = MathHelper.Clamp(1f - Math.Abs(0.5f - factor) * 2f, 0f, 1f);
                    var w = f2 * MathHelper.Clamp((30 - Math.Abs(30 - projectile.timeLeft)) / 15f, 0, 1);
                    bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * 10 * f2, color, new Vector3((float)Math.Sqrt(factor), 1, w)));
                    bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * -10 * f2, color, new Vector3((float)Math.Sqrt(factor), 0, w)));
                }

                List<CustomVertexInfo> triangleList = [];

                if (bars.Count > 2)
                {

                    //// 按照顺序连接三角形
                    //triangleList.Add(bars[0]);
                    //var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(projectile.velocity) * 30, Color.White,
                    //	new Vector3(0, 0.5f, 1));
                    //triangleList.Add(bars[1]);
                    //triangleList.Add(vertex);
                    for (int i = 0; i < bars.Count - 2; i += 2)
                    {
                        triangleList.Add(bars[i]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 1]);

                        triangleList.Add(bars[i + 1]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 3]);
                    }


                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                    RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                    // 干掉注释掉就可以只显示三角形栅格
                    //RasterizerState rasterizerState = new RasterizerState();
                    //rasterizerState.CullMode = CullMode.None;
                    //rasterizerState.FillMode = FillMode.WireFrame;
                    //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                    var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                    // 把变换和所需信息丢给shader
                    effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                    effect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
                    //InfiniteNightmare.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(drawColor, 1f, 0.5f).ToVector4());
                    Main.graphics.GraphicsDevice.Textures[0] = VirtualDreamMod.GetTexture(ApePath + "Style_15");
                    Main.graphics.GraphicsDevice.Textures[1] = VirtualDreamMod.GetTexture(ApePath + "Style_9");
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    //Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;
                    //Main.graphics.GraphicsDevice.Textures[1] = TextureAssets.MagicPixel.Value;
                    //Main.graphics.GraphicsDevice.Textures[2] = TextureAssets.MagicPixel.Value;
                    /*if (isCyan)
    	{
    		InfiniteNightmare.CleverEffect.CurrentTechnique.Passes["Clever"].Apply();
    	}*/
                    effect.CurrentTechnique.Passes[0].Apply();


                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);

                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
            }
            return false;
        }
        public override void PostAI()
        {
            Vector2 value1 = Main.player[projectile.owner].position - Main.player[projectile.owner].oldPosition;
            for (int num31 = projectile.oldPos.Length - 1; num31 > 0; num31--)
            {
                projectile.oldPos[num31] = projectile.oldPos[num31 - 1];
                projectile.oldRot[num31] = projectile.oldRot[num31 - 1];
                projectile.oldSpriteDirection[num31] = projectile.oldSpriteDirection[num31 - 1];
                if (projectile.numUpdates == 0 && projectile.oldPos[num31] != Vector2.Zero)
                {
                    projectile.oldPos[num31] += value1;
                }
            }
            projectile.oldPos[0] = projectile.position;
        }
        public override void AI()
        {
            if (tap != null)
            {
                var v = projectile.Center - owner.Center;
                projectile.velocity = v.ApplyMatrix(tap.baseOfX, tap.baseOfY) / 1024f;
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 8f;
            }
            base.AI();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("向量场锥刺");
        }
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.width = 4;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.height = 4;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
        }
    }
}