using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ID;
using VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana;
using static VirtualDream.Utils.VirtualDreamDrawMethods;
namespace VirtualDream.Contents.StarBound.NPCs.Bosses.AsraNox
{
    [AutoloadBossHead]
    public class AsraNox : ModNPC
    {
        public AsraNoxState state => (AsraNoxState)(byte)ai0;
        public (int, int) musics;
        public static int solusEnergyShard;
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 56;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.defense = 75;
            NPC.lifeMax = 75000;
            NPC.value = 10000f;
            NPC.friendly = false;
            NPC.boss = true;
            //NPC.noGravity = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            if (ModLoader.TryGetMod("VirtualDreamMusic", out Mod music))
            {
                musics.Item1 = MusicLoader.GetMusicSlot(music, "Assets/Music/JudasKiss(full)");
                musics.Item2 = MusicLoader.GetMusicSlot(music, "Assets/Music/JudasKiss_Part2");
            }
            Music = MusicID.Boss2;
            visualPlayer = new Player();
            visualPlayer.armor[1] = new Item(ItemID.HallowedPlateMail);
            visualPlayer.armor[2] = new Item(ItemID.NebulaLeggings);
            visualPlayer.armor[3] = new Item(ItemID.HeroShield);
            visualPlayer.armor[4] = new Item(ItemID.PrinceCape);
            visualPlayer.armor[5] = new Item(ItemID.LeinforsWings);
            visualPlayer.dye[1] = new Item(ItemID.ReflectiveSilverDye);
            visualPlayer.dye[3] = new Item(ItemID.PurpleDye);
            visualPlayer.dye[4] = new Item(ItemID.PurpleDye);
            //visualPlayer.inventory[0] = new Item(ItemID.TerraBlade);
            //visualPlayer.itemAnimationMax = 15;
            //visualPlayer.itemAnimation = 5;
            visualPlayer.skinColor = new Color(255, 125, 90, 255);
            visualPlayer.eyeColor = new Color(38, 38, 38, 255);
            visualPlayer.hairColor = new Color(38, 38, 38, 255);
            visualPlayer.hair = 85;
            visualPlayer.isFirstFractalAfterImage = true;
            visualPlayer.ResetEffects();
            visualPlayer.ResetVisibleAccessories();
            visualPlayer.UpdateDyes();
            visualPlayer.DisplayDollUpdate();
            visualPlayer.UpdateSocialShadow();
            visualPlayer.PlayerFrame();

            solusEnergyShard = ModContent.ProjectileType<SolusEnergyShard>();
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("阿斯拉诺克斯");
        }
        void SetAI(float ai_0 = 0, float ai_1 = 0, float ai_2 = 0, float ai_3 = 0, float ai_4 = 0, float ai_5 = 0, float ai_6 = 0, float ai_7 = 0, float ai_8 = 0)
        {
            ai0 = ai_0;
            ai1 = ai_1;
            ai2 = ai_2;
            ai3 = ai_3;
            ai4 = ai_4;
            ai5 = ai_5;
            ai6 = ai_6;
            ai7 = ai_7;
            ai8 = ai_8;
        }
        public Player targetPlayer
        {
            get
            {
                Vector2 cen = NPC.Center;
                Player target = null;
                float distanceMax = float.MaxValue;
                foreach (Player player in Main.player)
                {
                    if (!player.active || player.dead) continue;
                    float currentDistance = Vector2.Distance(cen, player.Center);
                    if (currentDistance < distanceMax)
                    {
                        distanceMax = currentDistance;
                        target = player;
                    }
                }
                if (target == null)
                    target = Main.LocalPlayer;
                return target;
            }
        }
        public float ai0
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public float ai1
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public float ai2
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public float ai3
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public float ai4;
        public float ai5;
        public float ai6;
        public float ai7;
        public float ai8;
        private void SpawnFractal(Vector2 targetPosition, Vector2 offset = default, Vector2? dir = null)
        {
            //var value5 = targetPlayer.Center + targetPlayer.velocity * 20f + ((int)ai1 % 60 < ai1 / 13f ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960));
            // Vector2 vector32 = value5 - NPC.Center;
            Vector2 vector33 = dir ?? Main.rand.NextVector2CircularEdge(1f, 1f);
            float num78 = 1f;
            int num79 = 1;
            for (int num80 = 0; num80 < num79; num80++)
            {
                targetPosition += Main.rand.NextVector2Circular(24f, 24f);
                //if (vector32.Length() > 700f)
                //{
                //    vector32 *= 700f / vector32.Length();
                //    value5 = NPC.Center + vector32;
                //}
                float num81 = Terraria.Utils.GetLerpValue(0f, 6f, NPC.velocity.Length(), true) * 0.8f;
                vector33 *= 1f - num81;
                vector33 += offset * num81;//player.velocity * num81
                vector33 = vector33.SafeNormalize(Vector2.UnitX);

                float num82 = 120f;
                float num83 = Main.rand.NextFloatDirection() * 3.14159274f * (1f / num82) * 0.5f * num78;
                float num84 = num82 / 2f;
                float scaleFactor3 = 12f + Main.rand.NextFloat() * 2f;
                Vector2 vector34 = vector33 * scaleFactor3;
                Vector2 vector35 = new Vector2(0f, 0f);
                Vector2 vector36 = vector34;
                int num85 = 0;
                while (num85 < num84)
                {
                    vector35 += vector36;
                    vector36 = vector36.RotatedBy(num83, default);
                    num85++;
                }
                Vector2 value6 = -vector35;
                Vector2 position1 = targetPosition + value6;
                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position1, vector34, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, num83);
                if (proj.ModProjectile is SolusKatanaFractal solusKatanaFractal)
                {
                    solusKatanaFractal.drawPlayer = new Player();
                    solusKatanaFractal.drawPlayer.CopyVisuals(visualPlayer);
                }
            }
        }

        //private float ai(int index)
        //{
        //    index = (int)MathHelper.Clamp(index, 0, 7);
        //    if (index < 4) return NPC.ai[index];
        //    return index switch
        //    {
        //        4 => ai4,
        //        5 => ai5,
        //        6 => ai6,
        //        7 => ai7,
        //        _ => 0
        //    };
        //}
        public override void AI()
        {
            if (musics.Item1 != 0)
            {
                Music = (byte)state < 7 || (byte)state > 18 ? musics.Item1 : musics.Item2;
            }
            visualPlayer.velocity = NPC.velocity;
            NPC.dontTakeDamage = state == 0 ||
    (NPC.life < NPC.lifeMax * 7 / 10 && state < AsraNoxState.陨日残阳_随机) ||
    (NPC.life < NPC.lifeMax / 4 && state < AsraNoxState.陨日残阳_后撤) ||
    (NPC.life < NPC.lifeMax / 5 && state < AsraNoxState.初源日炎_后撤) ||
    (NPC.life < NPC.lifeMax * 3 / 20 && state < AsraNoxState.星恒飞刃_后撤) ||
    (NPC.life < NPC.lifeMax / 10 && state < AsraNoxState.日曜星流_后撤) ||
    (NPC.life < NPC.lifeMax / 20 && state < AsraNoxState.太阳风暴_后撤);
            //Music = MusicID.Boss2;
            switch (state)
            {
                #region 开始
                case AsraNoxState.开始://TODO 开始
                    {
                        ai1++;
                        if (ai1 >= 420)
                        {
                            SetAI(1);
                            break;
                        }
                        int counter = (int)ai1;
                        if (counter <= 360)
                            if (counter % (24 - counter / 30) == 0)
                            {
                                for (int n = 0; n < 4; n++)
                                {
                                    if (Main.rand.NextBool(4)) continue;
                                    var flag = Main.rand.NextBool(3);
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560)), new Vector2(Main.rand.NextFloat(4, 8), 6) * 3f, solusEnergyShard, 35, 0, Main.myPlayer, flag ? 0 : 2, Main.rand.NextFloat(Main.rand.NextFloat(0.925f, 1f), 1f));
                                    proj.friendly = false;
                                    proj.hostile = true;
                                    if (counter >= 270 || Main.rand.NextBool(4))
                                        proj.timeLeft = 450 - (int)ai1;
                                }
                            }
                        break;
                    }
                case AsraNoxState.陨日残阳://TODO 陨日残阳
                    {
                        var timer = (int)ai1;
                        int direct = timer >= 10 ? (timer - 10) / 20 % 2 : 0;
                        if (timer == 0)
                        {
                            ai2 = Main.rand.Next(-480, 480);
                            ai3 = ai1 >= 550 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                            ai4 = targetPlayer.Center.X;
                            ai5 = targetPlayer.Center.Y;
                        }

                        if (timer % 20 == 10)
                        {
                            if (timer <= 610)
                            {
                                if (timer != 10)
                                {
                                    ai2 = Main.rand.Next(-480, 480);
                                    ai3 = ai1 >= 550 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                                    ai4 = targetPlayer.Center.X;
                                    ai5 = targetPlayer.Center.Y;
                                }
                                ai7 = ai6;
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);
                            }
                            else if (timer == 630) ai7 = ai6;
                        }

                        if (timer >= 40)
                        {

                            const int timeMax = 20;
                            if (timer % timeMax == 0)
                            {
                                ai8 = ai7;
                            }
                            int counter = (timer - 40) % timeMax;
                            var projectile = Main.projectile[(int)ai8];
                            NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow(counter / (timeMax - 1f), 3);
                            projectile.Center = NPC.Center + new Vector2(0, 12);
                            visualPlayer.direction = Math.Sign(projectile.velocity.X);
                            visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);

                            if (timer % 2 == 0)
                            {
                                var unit = projectile.velocity.SafeNormalize(default);
                                unit = unit.RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6 + MathHelper.PiOver2);
                                for (int n = 0; n < 2; n++)
                                {
                                    unit = -unit;
                                    if (!Main.rand.NextBool(3)) continue;
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;
                                }
                                if (Main.rand.NextBool((int)MathHelper.Clamp((targetPlayer.Center - NPC.Center).Length() / 16, 3, 64)))
                                {
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;
                                }

                            }
                        }
                        if (timer < 40)
                        {
                            var targetVec = timer < 30 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai7].Center;
                            NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                        }

                        ai1++;
                        if (ai1 >= 660)
                        {
                            SetAI(2);//2
                            break;
                        }
                        #region 旧冲刺，效果拉胯
                        //const int timeMax = 20;
                        //int counter = (int)ai1 % timeMax;
                        //int direct = (int)ai1 / timeMax % 2;
                        //if (counter == 0)
                        //{
                        //    ai2 = Main.rand.Next(-480, 480);
                        //    ai3 = ai1 >= 580 ? -ai2 : Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 });
                        //    ai4 = targetPlayer.Center.X;
                        //    ai5 = targetPlayer.Center.Y;
                        //    ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2, new Vector2(direct == 1 ? 2048 : -2048, ai3) / 20f, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 1);
                        //    Main.projectile[(int)ai6].extraUpdates = 0;
                        //    if (Main.projectile[(int)ai6].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                        //}
                        //NPC.Center = Vector2.Lerp(new Vector2(direct == 1 ? -1024 : 1024, 0), new Vector2(direct == 1 ? 1024 : -1024, ai3), (float)Math.Pow(counter / (timeMax - 1f), 3)) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                        //Main.projectile[(int)ai6].Center = NPC.Center + Main.projectile[(int)ai6].velocity.SafeNormalize(default) * 42;

                        //if (counter % 5 == 0)
                        //{
                        //    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, 3, 1.05f);
                        //    proj.friendly = false;
                        //    proj.hostile = true;
                        //}
                        //visualPlayer.direction = direct == 1 ? 1 : -1;
                        //visualPlayer.itemAnimation = 1;
                        //visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, 0);
                        #endregion


                        NPC.oldPos[0] = NPC.oldPosition;
                        //visualPlayer.itemRotation = 
                        break;
                    }
                case AsraNoxState.初源日炎://TODO 初源日炎
                    {
                        if ((int)ai1 < 765)
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                        else
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(ai5, ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);
                        NPC.damage = 0;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((int)ai1 % 10 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f + ((int)ai1 % 60 < ai1 / 13f ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960)));
                        }
                        var counter = (int)ai1 % 120;
                        if (counter == 0)
                        {
                            ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                            Main.projectile[(int)ai4].extraUpdates = 0;
                            if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            if ((int)ai1 == 720)
                            {
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                            }
                        }
                        var projectile = Main.projectile[(int)ai4];
                        for (int n = 59; n > 3; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 4];
                            projectile.oldRot[n] = projectile.oldRot[n - 4];
                        }
                        for (int n = 1; n < 4; n++)
                        {
                            projectile.oldPos[n] = projectile.oldPos[0];
                            projectile.oldRot[n] = projectile.oldRot[0];
                        }
                        if (counter <= 60)
                        {
                            //projectile.oldPos[0] = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            //if ((int)ai1 < 720)
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //else
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //if (visualPlayer.direction == -1) projectile.oldRot[0] = MathHelper.Pi - projectile.oldRot[0];
                            Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                if ((int)ai1 < 720)
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                else
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                            }
                        }
                        ai1++;

                        if (counter == 60 || (int)ai1 == 780)
                        {
                            int max = (int)ai1 / 120 + 4;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }

                        if (ai1 >= 780)
                        {
                            SetAI(3);
                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;

                        //if (counter <= 60 && counter >= 45 && counter % 3 == 0 && (int)ai1 < 720)
                        //{
                        //    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, 3, 1.05f);
                        //    proj.friendly = false;
                        //    proj.hostile = true;
                        //}

                        break;
                    }
                case AsraNoxState.星恒飞刃://TODO 星恒飞刃
                    {
                        int timer = (int)ai1;
                        //前180帧妖梦非符同款攻击
                        //后60帧如下安排
                        //20帧移动，40帧发射弹幕，最后10帧隐去

                        //这里是新安排
                        //前3*180帧妖梦非符同款攻击
                        //最后180帧中60帧移动，120帧发射弹幕
                        if (timer < 540)
                        {
                            int counter = timer % 180;
                            int stager = timer / 180;
                            if (counter % 60 == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter % 60 <= 30)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                                if (counter % 60 > 21)
                                {
                                    var factor = counter % 60 - 21f;
                                    factor /= 8f;
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(0.6f * MathHelper.Pi * (2 * factor - 1));//(MathHelper.Lerp(-MathHelper.Pi * .35f, MathHelper.Pi * .85f, factor) - MathHelper.Pi / 6).ToRotationVector2()
                                                                                                                                                            //unit *= new Vector2(visualPlayer.direction, 1);
                                    for (int n = 0; n < stager + counter / 60 + 1; n++)
                                    {
                                        var shootCenter = NPC.Center + 192 * unit;//.RotatedBy(n / (stager + 1f) * MathHelper.Pi * .15f)
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), shootCenter, (targetPlayer.Center - shootCenter).SafeNormalize(default).RotatedBy(MathHelper.Pi / 12f * (n - 0.5f * (stager + counter / 60))) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                        //proj.timeLeft = 31;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (timer == 540)
                            {
                                ai5 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai5].extraUpdates = 0;
                                if (Main.projectile[(int)ai5].ModProjectile is SolusKatanaFractal solusKatanaFractal_0)
                                {
                                    solusKatanaFractal_0.drawPlayer = new Player();
                                    solusKatanaFractal_0.drawPlayer.CopyVisuals(visualPlayer);
                                }
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai6].extraUpdates = 0;
                                if (Main.projectile[(int)ai6].ModProjectile is SolusKatanaFractal solusKatanaFractal_1)
                                {
                                    solusKatanaFractal_1.drawPlayer = new Player();
                                    solusKatanaFractal_1.drawPlayer.CopyVisuals(visualPlayer);
                                }
                                ai7 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai7].extraUpdates = 0;
                                if (Main.projectile[(int)ai7].ModProjectile is SolusKatanaFractal solusKatanaFractal_2)
                                {
                                    solusKatanaFractal_2.drawPlayer = new Player();
                                    solusKatanaFractal_2.drawPlayer.CopyVisuals(visualPlayer);
                                }
                                ai8 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai8].extraUpdates = 0;
                                if (Main.projectile[(int)ai8].ModProjectile is SolusKatanaFractal solusKatanaFractal_3)
                                {
                                    solusKatanaFractal_3.drawPlayer = new Player();
                                    solusKatanaFractal_3.drawPlayer.CopyVisuals(visualPlayer);
                                }
                            }
                            if (timer < 600)
                            {
                                var target = targetPlayer.Center + (MathHelper.Pi / 6).ToRotationVector2() * 400;
                                var projectile = Main.projectile[(int)ai5];
                                var count = 0;
                            mylabel:
                                projectile.Center += projectile.velocity;
                                Vector2 targetVec = target - projectile.Center;
                                targetVec.Normalize();
                                targetVec *= 40f;
                                projectile.velocity = (projectile.velocity * 15f + targetVec * 2) / 17f;
                                if (projectile.ModProjectile is SolusKatanaFractal solusKatanaFractal) solusKatanaFractal.drawPlayer.direction = Math.Sign(targetPlayer.Center.X - projectile.Center.X);//
                                count++;
                                if (count < 4)
                                {
                                    switch (count)
                                    {
                                        case 1: projectile = Main.projectile[(int)ai6]; target = targetPlayer.Center + (MathHelper.Pi / 6 * 5).ToRotationVector2() * 400; break;
                                        case 2: projectile = Main.projectile[(int)ai7]; target = targetPlayer.Center + (-MathHelper.Pi / 4).ToRotationVector2() * 400; break;
                                        case 3: projectile = Main.projectile[(int)ai8]; target = targetPlayer.Center + (-MathHelper.Pi / 4 * 3).ToRotationVector2() * 400; break;
                                    }
                                    goto mylabel;
                                }
                            }
                            if (timer == 600)
                            {
                                var target = targetPlayer.Center + (MathHelper.Pi / 6).ToRotationVector2() * 400;
                                var projectile = Main.projectile[(int)ai5];
                                var count = 0;
                            mylabel:
                                projectile.Center = target;
                                projectile.velocity = default;
                                count++;
                                if (count < 4)
                                {
                                    switch (count)
                                    {
                                        case 1: projectile = Main.projectile[(int)ai6]; target = targetPlayer.Center + (MathHelper.Pi / 6 * 5).ToRotationVector2() * 400; break;
                                        case 2: projectile = Main.projectile[(int)ai7]; target = targetPlayer.Center + (-MathHelper.Pi / 4).ToRotationVector2() * 400; break;
                                        case 3: projectile = Main.projectile[(int)ai8]; target = targetPlayer.Center + (-MathHelper.Pi / 4 * 3).ToRotationVector2() * 400; break;
                                    }
                                    goto mylabel;
                                }
                            }
                            var _projectile = Main.projectile[(int)ai5];
                            var _count = 0;
                        _mylabel:
                            for (int n = 59; n > 3; n--)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                                _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[0];
                                _projectile.oldRot[n] = _projectile.oldRot[0];
                            }
                            Vector2 currentVec = _projectile.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                _projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                _projectile.oldRot[n] = (timer - n * .25f) * -MathHelper.Pi / 20f + 19 * MathHelper.PiOver2;
                                if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                            }
                            _count++;
                            if (_count < 4)
                            {
                                switch (_count)
                                {
                                    case 1: _projectile = Main.projectile[(int)ai6]; break;
                                    case 2: _projectile = Main.projectile[(int)ai7]; break;
                                    case 3: _projectile = Main.projectile[(int)ai8]; break;
                                }
                                goto _mylabel;
                            }
                            if (timer >= 600 && timer % 3 == 0)
                            {
                                var projectile = Main.projectile[(int)ai5];
                                var count = 0;
                            mylabel:
                                if (Main.rand.Next(10) < 7)
                                {
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;
                                }

                                count++;
                                if (count < 4)
                                {
                                    switch (count)
                                    {
                                        case 1: projectile = Main.projectile[(int)ai6]; break;
                                        case 2: projectile = Main.projectile[(int)ai7]; break;
                                        case 3: projectile = Main.projectile[(int)ai8]; break;
                                    }
                                    goto mylabel;
                                }
                            }
                        }

                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 800 * visualPlayer.direction, targetPlayer.Center.Y - 400 * (float)Math.Sin(ai1 / 240 * MathHelper.TwoPi) + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.05f);//
                        #region 放弃的
                        //if (counter < 180)
                        //{
                        //    if (counter % 60 == 0)
                        //    {
                        //        ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                        //        Main.projectile[(int)ai4].extraUpdates = 0;
                        //        if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                        //    }
                        //    var projectile = Main.projectile[(int)ai4];
                        //    for (int n = 59; n > 3; n--)
                        //    {
                        //        projectile.oldPos[n] = projectile.oldPos[n - 4];
                        //        projectile.oldRot[n] = projectile.oldRot[n - 4];
                        //    }
                        //    for (int n = 1; n < 4; n++)
                        //    {
                        //        projectile.oldPos[n] = projectile.oldPos[0];
                        //        projectile.oldRot[n] = projectile.oldRot[0];
                        //    }
                        //    if (counter % 60 <= 30)
                        //    {
                        //        Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                        //        for (int n = 0; n < 4; n++)
                        //        {
                        //            projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                        //            projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                        //            if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                        //        }
                        //        if (counter % 60 > 21)
                        //        {
                        //            var factor = counter % 60 - 21f;
                        //            factor /= 8f;
                        //            var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(0.6f * MathHelper.Pi * (2 * factor - 1));//(MathHelper.Lerp(-MathHelper.Pi * .35f, MathHelper.Pi * .85f, factor) - MathHelper.Pi / 6).ToRotationVector2()
                        //            //unit *= new Vector2(visualPlayer.direction, 1);
                        //            for (int n = 0; n < stager + counter / 60 + 1; n++)
                        //            {
                        //                var shootCenter = NPC.Center + 192 * unit;//.RotatedBy(n / (stager + 1f) * MathHelper.Pi * .15f)
                        //                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), shootCenter, (targetPlayer.Center - shootCenter).SafeNormalize(default).RotatedBy(MathHelper.Pi / 12f * (n - 0.5f * (stager + counter / 60))) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                        //                proj.friendly = false;
                        //                proj.hostile = true;
                        //                //proj.timeLeft = 31;
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (counter == 180)
                        //    {
                        //        ai5 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                        //        Main.projectile[(int)ai5].extraUpdates = 0;
                        //        if (Main.projectile[(int)ai5].ModProjectile is SolusKatanaFractal solusKatanaFractal_0)
                        //        {
                        //            solusKatanaFractal_0.drawPlayer = new Player();
                        //            solusKatanaFractal_0.drawPlayer.CopyVisuals(visualPlayer);
                        //        }
                        //        ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                        //        Main.projectile[(int)ai6].extraUpdates = 0;
                        //        if (Main.projectile[(int)ai6].ModProjectile is SolusKatanaFractal solusKatanaFractal_1)
                        //        {
                        //            solusKatanaFractal_1.drawPlayer = new Player();
                        //            solusKatanaFractal_1.drawPlayer.CopyVisuals(visualPlayer);
                        //        }
                        //        ai7 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                        //        Main.projectile[(int)ai7].extraUpdates = 0;
                        //        if (Main.projectile[(int)ai7].ModProjectile is SolusKatanaFractal solusKatanaFractal_2)
                        //        {
                        //            solusKatanaFractal_2.drawPlayer = new Player();
                        //            solusKatanaFractal_2.drawPlayer.CopyVisuals(visualPlayer);
                        //        }
                        //        ai8 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                        //        Main.projectile[(int)ai8].extraUpdates = 0;
                        //        if (Main.projectile[(int)ai8].ModProjectile is SolusKatanaFractal solusKatanaFractal_3)
                        //        {
                        //            solusKatanaFractal_3.drawPlayer = new Player();
                        //            solusKatanaFractal_3.drawPlayer.CopyVisuals(visualPlayer);
                        //        }
                        //    }
                        //    if (counter < 200)
                        //    {
                        //        var target = targetPlayer.Center + (MathHelper.Pi / 6).ToRotationVector2() * 400;
                        //        var projectile = Main.projectile[(int)ai5];
                        //        var count = 0;
                        //    mylabel:
                        //        projectile.Center += projectile.velocity;
                        //        Vector2 targetVec = target - projectile.Center;
                        //        targetVec.Normalize();
                        //        targetVec *= 20f;
                        //        projectile.velocity = (projectile.velocity * 60f + targetVec) / 61f;
                        //        count++;
                        //        if (count < 4)
                        //        {
                        //            switch (count)
                        //            {
                        //                case 1: projectile = Main.projectile[(int)ai6]; target = targetPlayer.Center + (MathHelper.Pi / 6 * 5).ToRotationVector2() * 400; break;
                        //                case 2: projectile = Main.projectile[(int)ai7]; target = targetPlayer.Center + (-MathHelper.Pi / 4).ToRotationVector2() * 400; break;
                        //                case 3: projectile = Main.projectile[(int)ai8]; target = targetPlayer.Center + (-MathHelper.Pi / 4 * 3).ToRotationVector2() * 400; break;
                        //            }
                        //            goto mylabel;
                        //        }
                        //    }
                        //    if (counter == 200) 
                        //    {
                        //        var target = targetPlayer.Center + (MathHelper.Pi / 6).ToRotationVector2() * 400;
                        //        var projectile = Main.projectile[(int)ai5];
                        //        var count = 0;
                        //    mylabel:
                        //        projectile.Center = target;
                        //        projectile.velocity = default;
                        //        count++;
                        //        if (count < 4)
                        //        {
                        //            switch (count)
                        //            {
                        //                case 1: projectile = Main.projectile[(int)ai6]; target = targetPlayer.Center + (MathHelper.Pi / 6 * 5).ToRotationVector2() * 400; break;
                        //                case 2: projectile = Main.projectile[(int)ai7]; target = targetPlayer.Center + (-MathHelper.Pi / 4).ToRotationVector2() * 400; break;
                        //                case 3: projectile = Main.projectile[(int)ai8]; target = targetPlayer.Center + (-MathHelper.Pi / 4 * 3).ToRotationVector2() * 400; break;
                        //            }
                        //            goto mylabel;
                        //        }
                        //    }
                        //    var _projectile = Main.projectile[(int)ai5];
                        //    var _count = 0;
                        //_mylabel:
                        //    for (int n = 59; n > 3; n--)
                        //    {
                        //        _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                        //        _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                        //    }
                        //    for (int n = 1; n < 4; n++)
                        //    {
                        //        _projectile.oldPos[n] = _projectile.oldPos[0];
                        //        _projectile.oldRot[n] = _projectile.oldRot[0];
                        //    }
                        //    Vector2 currentVec = _projectile.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                        //    for (int n = 0; n < 4; n++)
                        //    {
                        //        _projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                        //        _projectile.oldRot[n] = counter * MathHelper.Pi / 20f - 19 * MathHelper.PiOver2;
                        //        if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                        //    }
                        //    _count++;
                        //    if (_count < 4)
                        //    {
                        //        switch (_count)
                        //        {
                        //            case 1: _projectile = Main.projectile[(int)ai6]; break;
                        //            case 2: _projectile = Main.projectile[(int)ai7]; break;
                        //            case 3: _projectile = Main.projectile[(int)ai8]; break;
                        //        }
                        //        goto _mylabel;
                        //    }
                        //    if (counter >= 200)
                        //    {
                        //    //    var projectile = Main.projectile[(int)ai5];
                        //    //    var count = 0;
                        //    //mylabel:
                        //    //    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                        //    //    proj.friendly = false;
                        //    //    proj.hostile = true;
                        //    //    count++;
                        //    //    if (count < 4)
                        //    //    {
                        //    //        switch (count)
                        //    //        {
                        //    //            case 1: projectile = Main.projectile[(int)ai6]; break;
                        //    //            case 2: projectile = Main.projectile[(int)ai7]; break;
                        //    //            case 3: projectile = Main.projectile[(int)ai8]; break;
                        //    //        }
                        //    //        goto mylabel;
                        //    //    }
                        //    }

                        //}
                        #endregion

                        ai1++;
                        if (ai1 >= 720)
                        {
                            SetAI(4);

                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.日曜星流://TODO 日曜星流
                    {
                        //240一次，三个阶段
                        //一次重劈(120帧)，一次下刺(60帧)，一次冲锋(20帧)
                        //流星雨，随着阶段而加强
                        //初源量减少
                        //冲刺生成的弹幕有所改变
                        int timer = (int)ai1;
                        int counter = timer % 240;
                        int stager = timer / 240;

                        #region 流星生成
                        if (timer % (36 - stager * 4) == 0)
                        {
                            for (int n = 0; n < 4; n++)
                            {
                                if (Main.rand.NextBool(4)) continue;
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560)), new Vector2(Main.rand.NextFloat(4, 8), 6).SafeNormalize(default) * 3f, solusEnergyShard, 35, 0, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), Main.rand.NextFloat(Main.rand.NextFloat(1f, 1.05f), 1.05f));
                                proj.friendly = false;
                                proj.hostile = true;
                            }
                        }
                        #endregion

                        #region 初源生成
                        if (timer % 20 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f + (Main.rand.NextBool(5 - stager) ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960)));
                        }
                        #endregion


                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if (counter < 120)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.125f);

                            if (counter == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter <= 60)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }
                            if (counter == 60)
                            {
                                int max = stager + 3;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                        }
                        else if (counter < 180)
                        {
                            if (counter < 165)
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                            else
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(ai5, ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);

                            if (counter == 120)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                                //Main.NewText((ai5, ai6));


                            }
                            if (counter >= 120)
                            {
                                var projectile = Main.projectile[(int)ai4];
                                for (int n = 59; n > 3; n--)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[n - 4];
                                    projectile.oldRot[n] = projectile.oldRot[n - 4];
                                }
                                for (int n = 1; n < 4; n++)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[0];
                                    projectile.oldRot[n] = projectile.oldRot[0];
                                }
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 120 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }

                        }
                        else
                        {
                            int direct = visualPlayer.direction == -1 ? 0 : 1;
                            if (counter == 180)
                            {
                                ai2 = Main.rand.Next(-480, 480);
                                ai3 = stager == 2 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;

                                int max = stager + 4;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                            if (counter == 190)
                            {
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);

                            }
                            if (counter < 220)
                            {
                                var targetVec = counter < 190 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai6].Center;
                                NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                            }
                            if (counter >= 220)
                            {
                                const int timeMax = 20;
                                var projectile = Main.projectile[(int)ai6];
                                NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow((counter - 220f) / (timeMax - 1f), 3);
                                projectile.Center = NPC.Center + new Vector2(0, 12);
                                visualPlayer.direction = Math.Sign(projectile.velocity.X);
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                //if (timer % 2 == 0)
                                //{
                                //    var unit = projectile.velocity.SafeNormalize(default);
                                //    unit = unit.RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6 + MathHelper.PiOver2);
                                //    for (int n = 0; n < 2; n++)
                                //    {
                                //        unit = -unit;
                                //        if (!Main.rand.NextBool(3)) continue;
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }
                                //    if (Main.rand.NextBool((int)MathHelper.Clamp((targetPlayer.Center - NPC.Center).Length() / 16, 3, 64)))
                                //    {
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }

                                //}
                                if (timer % 2 == 0)
                                {
                                    int max = stager + 6;
                                    for (int n = 0; n < max; n++)
                                    {
                                        var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj1.friendly = false;
                                            proj1.hostile = true;
                                        }
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj2.friendly = false;
                                            proj2.hostile = true;
                                        }
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj3.friendly = false;
                                            proj3.hostile = true;
                                        }
                                    }
                                }
                            }
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        ai1++;
                        if (ai1 >= 720)
                        {
                            SetAI(5);
                            break;
                        }
                        break;
                    }
                case AsraNoxState.太阳风暴://TODO 太阳风暴
                    {
                        //ai0状态 ai1计时 ai2 ai3控制发射弹幕 ai4 ai5记录玩家坐标 ai6 ai7记录起始位置 ai8记录旋转中心位置
                        var timer = (int)ai1;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((timer - 30) % 60 == 0 && timer != 30)
                        {
                            int max = (timer - 30) / 120 + 3;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if (timer < 30)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 400 * visualPlayer.direction, targetPlayer.Center.Y), 0.05f);//
                        }
                        else if (timer < 630)
                        {
                            timer -= 30;
                            var counter = timer % 60;
                            if (counter == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai2].extraUpdates = 0;
                                ai3 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai3].extraUpdates = 0;
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;
                                ai6 = NPC.Center.X;
                                ai7 = NPC.Center.Y;
                                ai8 = Main.rand.NextFloat(-2f, 2f);
                                var leng = (targetPlayer.Center - NPC.Center).Length();
                                if (leng * Math.Abs(ai8) > 600)
                                {
                                    ai8 = (600 - Main.rand.NextFloat(-50, 50)) / leng * Math.Sign(ai8);
                                }
                            }
                            var rotationCenter = new Vector2(ai6 - ai4, ai7 - ai5);
                            rotationCenter = new Vector2(-rotationCenter.Y, rotationCenter.X) * ai8 + new Vector2(ai4 + ai6, ai5 + ai7) * .5f;
                            var toStart = new Vector2(ai6, ai7) - rotationCenter;
                            var toTarget = new Vector2(ai4, ai5) - rotationCenter;
                            var cross = toStart.CrossLength(toTarget);
                            //NPC.Center = MathHelper.Lerp(toStart.ToRotation(), toTarget.ToRotation(), (float)Math.Pow(counter / 60f, 2) * 2).ToRotationVector2() * toStart.Length() + rotationCenter;
                            //NPC.Center = toStart.RotatedBy(((cross > 0 ? MathHelper.TwoPi : 0) + new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation()) * (float)Math.Pow(counter / 60f, 2) * 2) + rotationCenter;

                            var t = 1 - (float)Math.Cos(counter / 60f * MathHelper.Pi);// (float)Math.Pow(counter / 60f, 2) * 2
                            if (cross < 0)
                            {
                                var dummy = toStart;
                                toStart = toTarget;
                                toTarget = dummy;
                                t = 1 - t;
                                cross *= -1;
                            }
                            NPC.Center = toStart.RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation() * t) + rotationCenter;

                            //NPC.Center = (cross < 0 ? toTarget : toStart).RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), Math.Abs(cross)).ToRotation() * (cross < 0 ? (1 - t) : t)) + rotationCenter;
                            //前置方法:圆心O 起点P 终点T，逆时针生成一段圆弧
                            //p = (P-O).RotatedBy(new Vector2(Vector2.Dot(P-O,T-O),(P-O).CrossLength(T-O)).ToRotation*t)+O
                            //生成劣弧则是检测叉积为负就交换PT，保持时间原点不变就再反向一下t,

                            var _projectile = Main.projectile[(int)ai2];
                            var _count = 0;
                        _mylabel:
                            for (int n = 59; n > 3; n--)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                                _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[0];
                                _projectile.oldRot[n] = _projectile.oldRot[0];
                            }
                            Vector2 currentVec = NPC.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                _projectile.oldPos[n] = Vector2.Lerp(currentVec, _projectile.oldPos[4], n * .25f);//
                                var rotation = -timer + n * .25f;
                                _projectile.oldRot[n] = MathHelper.Pi / 10f * rotation - 3 * (float)Math.Sin(MathHelper.Pi / 30f * rotation) + _count * MathHelper.Pi;
                                //if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                            }
                            _projectile.Center = _projectile.oldPos[0];
                            _count++;
                            if (_count < 2)
                            {
                                switch (_count)
                                {
                                    case 1: _projectile = Main.projectile[(int)ai3]; break;
                                }
                                goto _mylabel;
                            }

                            if (timer % 5 == 0)
                            {
                                var projectile = Main.projectile[(int)ai2];
                                for (int n = 0; n < 2; n++)
                                {
                                    if (Main.rand.Next(10) < 7)
                                    {
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center, (projectile.oldRot[0] + n * MathHelper.Pi).ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                    }
                                }
                            }


                        }

                        ai1++;

                        if (ai1 >= 660)
                        {
                            SetAI(6);
                            break;
                        }

                        break;
                    }
                case AsraNoxState.破晓之光://TODO 破晓之光
                    {
                        //分三个阶段
                        //前两个240秒，最后一个300秒
                        //转动一圈
                        //持剑平移
                        //杀意百合
                        //ai1计时 ai2记弹幕 ai3记录方向
                        var timer = (int)ai1;
                        if (timer < 480)
                        {
                            if (timer == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusLevatine>(), 60, 8, Main.myPlayer, -MathHelper.PiOver2);
                                ai3 = Math.Sign(new Vector2(0, -1).CrossLength(targetPlayer.Center - NPC.Center));
                                var _projectile = Main.projectile[(int)ai2];
                                for (int n = _projectile.oldPos.Length - 1; n >= 0; n--)
                                {
                                    _projectile.oldPos[n] = NPC.Center;
                                    _projectile.oldRot[n] = _projectile.ai[0];
                                }
                            }
                            if (timer == 240)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusLevatine>(), 60, 8, Main.myPlayer, MathHelper.PiOver2);
                                ai3 = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                                var _projectile = Main.projectile[(int)ai2];
                                for (int n = _projectile.oldPos.Length - 1; n >= 0; n--)
                                {
                                    _projectile.oldPos[n] = NPC.Center;
                                    _projectile.oldRot[n] = _projectile.ai[0];
                                }
                            }
                            var projectile = Main.projectile[(int)ai2];
                            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 1];
                                projectile.oldRot[n] = projectile.oldRot[n - 1];
                            }
                            if (timer < 240)
                            {
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi);
                                visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);

                                if (timer >= 30)
                                {
                                    var factor = timer - 30f;
                                    factor /= 210f;
                                    factor *= factor;
                                    projectile.oldRot[0] = projectile.ai[0] = factor * MathHelper.TwoPi * ai3 - MathHelper.PiOver2;
                                    visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.oldRot[0] - MathHelper.PiOver2);
                                    visualPlayer.direction = Math.Sign(Math.Cos(projectile.ai[0]));
                                }
                                else
                                {
                                    NPC.Center = Vector2.Lerp(NPC.Center, targetPlayer.Center + new Vector2(-visualPlayer.direction * 400, 320), 0.1f);
                                }
                                projectile.Center = projectile.oldPos[0] = NPC.Center + projectile.ai[0].ToRotationVector2() * 20 * new Vector2(.5f, 1) + new Vector2(0, 12);

                            }
                            else
                            {
                                visualPlayer.direction = (int)ai3;
                                if (timer >= 270)
                                {
                                    NPC.Center += new Vector2((timer - 270f).SymmetricalFactor(105, 60) * 12 * ai3, 0);
                                    NPC.Center = Vector2.Lerp(NPC.Center, NPC.Center with { Y = targetPlayer.Center.Y - 400 }, 0.05f);
                                    projectile.oldRot[0] = projectile.ai[0] = MathHelper.PiOver2;
                                    visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.PiOver2 * visualPlayer.direction);

                                    if ((targetPlayer.Center.X - NPC.Center.X) * ai3 < 0 && timer % 4 == 0)
                                    {
                                        #region 初源生成
                                        SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    var rot = visualPlayer.compositeFrontArm.rotation;
                                    visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Terraria.Utils.AngleLerp(rot, -MathHelper.PiOver2 * visualPlayer.direction, 0.05f));
                                    NPC.Center = Vector2.Lerp(NPC.Center, targetPlayer.Center + new Vector2(-visualPlayer.direction * 256, -200), 0.1f);

                                }
                                projectile.Center = projectile.oldPos[0] = NPC.Center + new Vector2(visualPlayer.direction * 6, 16);

                            }
                            if (timer % 24 == 0 && timer % 240 > 30)
                            {
                                var unit = projectile.oldRot[0].ToRotationVector2();
                                var normal = new Vector2(-unit.Y, unit.X) * ai3 * (timer > 240 ? -1 : 1);
                                for (int n = 0; n < 12; n++)
                                {
                                    if (!Main.rand.NextBool(3))
                                    {
                                        var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center + unit * Main.rand.NextFloat(0, 1200), normal * 8f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj1.friendly = false;
                                        proj1.hostile = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);

                            if (timer % 60 == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;

                            }
                            if (timer % 60 == 30)
                            {
                                for (int n = 0; n < 3; n++)
                                {
                                    var targetPos = targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, -320) + 640 * n, 540);
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPos - NPC.Center) / 15, solusEnergyShard, 45, 4, Main.myPlayer, 7, 1f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;
                                    proj1.timeLeft = 46;
                                }
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (timer % 60 <= 30)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }

                            //if (timer % 60 == 0)
                            //{
                            //    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-960, 960), 540), default, ModContent.ProjectileType<SolusUltraLaser>(), 80, 8, Main.myPlayer, 0, 0);
                            //}
                        }

                        ai1++;
                        if (ai1 >= 780)
                        {
                            SetAI(7);//todo 这货完成了要切换回7

                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                #endregion

                #region 追灭
                case AsraNoxState.陨日残阳_追灭://TODO 陨日残阳_追灭
                    {
                        var timer = (int)ai1;
                        int direct = AsraNoxSky.windDirection;
                        if (timer == 0)
                        {
                            ai2 = Main.rand.Next(-480, 480);
                            ai3 = -2 * ai2;
                            ai4 = targetPlayer.Center.X;
                            ai5 = targetPlayer.Center.Y;
                        }
                        if (timer % 20 == 10)
                        {
                            if (timer <= 610)
                            {
                                if (timer != 10)
                                {
                                    ai2 = Main.rand.Next(-480, 480);
                                    ai3 = -2 * ai2;
                                    ai4 = targetPlayer.Center.X;
                                    ai5 = targetPlayer.Center.Y;
                                }
                                ai7 = ai6;
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);
                            }
                            else if (timer == 630) ai7 = ai6;
                        }
                        if (timer >= 40)
                        {
                            const int timeMax = 20;
                            if (timer % timeMax == 0)
                            {
                                ai8 = ai7;
                            }
                            int counter = (timer - 40) % timeMax;
                            var projectile = Main.projectile[(int)ai8];
                            NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow(counter / (timeMax - 1f), 3);
                            projectile.Center = NPC.Center + new Vector2(0, 12);
                            visualPlayer.direction = Math.Sign(projectile.velocity.X);
                            visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            if (timer % 12 == 0)
                            {
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 12f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.02f);
                                proj.friendly = false;
                                proj.hostile = true;
                            }
                        }
                        if (timer < 40)
                        {
                            var targetVec = timer < 30 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai7].Center;
                            NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                        }
                        ai1++;
                        if (ai1 >= 660)
                        {
                            SetAI(8);
                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.初源日炎_追灭://TODO 初源日炎_追灭
                    {
                        if ((int)ai1 < 765)
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 600 * (AsraNoxSky.windDirection), targetPlayer.Center.Y + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                        else
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 400 * (AsraNoxSky.windDirection), ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);
                        NPC.damage = 0;
                        visualPlayer.direction = AsraNoxSky.windDirection;
                        if ((int)ai1 % 10 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f + ((int)ai1 % 60 < ai1 / 13f ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960)), targetPlayer.velocity);
                        }
                        var counter = (int)ai1 % 120;
                        if (counter == 0)
                        {
                            ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                            Main.projectile[(int)ai4].extraUpdates = 0;
                            if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            if ((int)ai1 == 720)
                            {
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                            }
                        }
                        var projectile = Main.projectile[(int)ai4];
                        for (int n = 59; n > 3; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 4];
                            projectile.oldRot[n] = projectile.oldRot[n - 4];
                        }
                        for (int n = 1; n < 4; n++)
                        {
                            projectile.oldPos[n] = projectile.oldPos[0];
                            projectile.oldRot[n] = projectile.oldRot[0];
                        }
                        if (counter <= 60)
                        {
                            //projectile.oldPos[0] = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            //if ((int)ai1 < 720)
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //else
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //if (visualPlayer.direction == -1) projectile.oldRot[0] = MathHelper.Pi - projectile.oldRot[0];
                            Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                if ((int)ai1 < 720)
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                else
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                            }
                        }
                        ai1++;

                        if (counter == 60 || (int)ai1 == 780)
                        {
                            int max = (int)ai1 / 120 + 4;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }

                        if (ai1 >= 780)
                        {
                            SetAI(9);
                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;

                        //if (counter <= 60 && counter >= 45 && counter % 3 == 0 && (int)ai1 < 720)
                        //{
                        //    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, 3, 1.05f);
                        //    proj.friendly = false;
                        //    proj.hostile = true;
                        //}

                        break;
                    }
                case AsraNoxState.星恒飞刃_追灭://TODO 星恒飞刃_追灭
                    {
                        int timer = (int)ai1;
                        //前180帧妖梦非符同款攻击
                        //后60帧如下安排
                        //20帧移动，40帧发射弹幕，最后10帧隐去

                        //这里是新安排
                        //前3*180帧妖梦非符同款攻击
                        //最后180帧中60帧移动，120帧发射弹幕
                        int counter = timer % 180;
                        int stager = timer / 180;
                        if (counter % 60 == 0)
                        {
                            ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                            Main.projectile[(int)ai4].extraUpdates = 0;
                            if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                        }
                        var projectile = Main.projectile[(int)ai4];
                        for (int n = 59; n > 3; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 4];
                            projectile.oldRot[n] = projectile.oldRot[n - 4];
                        }
                        for (int n = 1; n < 4; n++)
                        {
                            projectile.oldPos[n] = projectile.oldPos[0];
                            projectile.oldRot[n] = projectile.oldRot[0];
                        }
                        if (counter % 60 <= 30)
                        {
                            Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                            }
                            if (counter % 60 > 21)
                            {
                                var factor = counter % 60 - 21f;
                                factor /= 8f;
                                var unit = (targetPlayer.Center + targetPlayer.velocity * 30 - NPC.Center).SafeNormalize(default).RotatedBy(0.6f * MathHelper.Pi * (2 * factor - 1));//(MathHelper.Lerp(-MathHelper.Pi * .35f, MathHelper.Pi * .85f, factor) - MathHelper.Pi / 6).ToRotationVector2()
                                                                                                                                                                                     //unit *= new Vector2(visualPlayer.direction, 1);
                                for (int n = 0; n < stager + counter / 60 + 1; n++)
                                {
                                    var shootCenter = NPC.Center + 192 * unit;//.RotatedBy(n / (stager + 1f) * MathHelper.Pi * .15f)
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), shootCenter, (targetPlayer.Center + targetPlayer.velocity * 30 - shootCenter).SafeNormalize(default).RotatedBy(MathHelper.Pi / 12f * (n - 0.5f * (stager + counter / 60))) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;
                                    //proj.timeLeft = 31;
                                }
                            }
                        }

                        visualPlayer.direction = AsraNoxSky.windDirection;
                        NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 800 * visualPlayer.direction, targetPlayer.Center.Y - 400 * (float)Math.Sin(ai1 / 240 * MathHelper.TwoPi) + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.05f);//
                        ai1++;
                        if (ai1 >= 720)
                        {
                            SetAI(10);
                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.日曜星流_追灭://TODO 日曜星流_追灭
                    {
                        //240一次，三个阶段
                        //一次重劈(120帧)，一次下刺(60帧)，一次冲锋(20帧)
                        //流星雨，随着阶段而加强
                        //初源量减少
                        //冲刺生成的弹幕有所改变
                        int timer = (int)ai1;
                        int counter = timer % 240;
                        int stager = timer / 240;

                        #region 流星生成
                        if (timer % (36 - stager * 4) == 0)
                        {
                            for (int n = 0; n < 4; n++)
                            {
                                if (Main.rand.NextBool(4)) continue;
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560)), new Vector2(Main.rand.NextFloat(8, 12) * (AsraNoxSky.windDirection), 6).SafeNormalize(default) * 3f, solusEnergyShard, 35, 0, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), Main.rand.NextFloat(Main.rand.NextFloat(1f, 1.05f), 1.05f));
                                proj.friendly = false;
                                proj.hostile = true;
                            }
                        }
                        #endregion

                        #region 初源生成
                        if (timer % 20 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f + (Main.rand.NextBool(5 - stager) ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960)), targetPlayer.velocity);
                        }
                        #endregion


                        visualPlayer.direction = AsraNoxSky.windDirection;
                        if (counter < 120)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 600 * (AsraNoxSky.windDirection), targetPlayer.Center.Y + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.125f);

                            if (counter == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter <= 60)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }
                            if (counter == 60)
                            {
                                int max = stager + 3;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                        }
                        else if (counter < 180)
                        {
                            if (counter < 165)
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 600 * (AsraNoxSky.windDirection), targetPlayer.Center.Y + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                            else
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 400 * (AsraNoxSky.windDirection), ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);

                            if (counter == 120)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                                //Main.NewText((ai5, ai6));


                            }
                            if (counter >= 120)
                            {
                                var projectile = Main.projectile[(int)ai4];
                                for (int n = 59; n > 3; n--)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[n - 4];
                                    projectile.oldRot[n] = projectile.oldRot[n - 4];
                                }
                                for (int n = 1; n < 4; n++)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[0];
                                    projectile.oldRot[n] = projectile.oldRot[0];
                                }
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 120 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }

                        }
                        else
                        {
                            int direct = AsraNoxSky.windDirection;
                            if (counter == 180)
                            {
                                ai2 = Main.rand.Next(-480, 480);
                                ai3 = -2 * ai2;
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;

                                int max = stager + 4;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                            if (counter == 190)
                            {
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);

                            }
                            if (counter < 220)
                            {
                                var targetVec = counter < 190 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai6].Center;
                                NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                            }
                            if (counter >= 220)
                            {
                                const int timeMax = 20;
                                var projectile = Main.projectile[(int)ai6];
                                NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow((counter - 220f) / (timeMax - 1f), 3);
                                projectile.Center = NPC.Center + new Vector2(0, 12);
                                visualPlayer.direction = Math.Sign(projectile.velocity.X);
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                //if (timer % 2 == 0)
                                //{
                                //    var unit = projectile.velocity.SafeNormalize(default);
                                //    unit = unit.RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6 + MathHelper.PiOver2);
                                //    for (int n = 0; n < 2; n++)
                                //    {
                                //        unit = -unit;
                                //        if (!Main.rand.NextBool(3)) continue;
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }
                                //    if (Main.rand.NextBool((int)MathHelper.Clamp((targetPlayer.Center - NPC.Center).Length() / 16, 3, 64)))
                                //    {
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }

                                //}
                                if (timer % 2 == 0)
                                {
                                    int max = stager + 6;
                                    for (int n = 0; n < max; n++)
                                    {
                                        var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj1.friendly = false;
                                            proj1.hostile = true;
                                        }
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj2.friendly = false;
                                            proj2.hostile = true;
                                        }
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj3.friendly = false;
                                            proj3.hostile = true;
                                        }
                                    }
                                }
                            }
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        ai1++;
                        if (ai1 >= 720)
                        {
                            SetAI(11);
                            break;
                        }
                        break;
                    }
                case AsraNoxState.太阳风暴_追灭://TODO 太阳风暴_追灭
                    {
                        //ai0状态 ai1计时 ai2 ai3控制发射弹幕 ai4 ai5记录玩家坐标 ai6 ai7记录起始位置 ai8记录旋转中心位置
                        var timer = (int)ai1;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((timer - 30) % 60 == 0 && timer != 30)
                        {
                            int max = (timer - 30) / 120 + 3;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if (timer < 30)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 400 * visualPlayer.direction, targetPlayer.Center.Y), 0.05f);//
                        }
                        else if (timer < 630)
                        {
                            timer -= 30;
                            var counter = timer % 60;
                            if (counter == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai2].extraUpdates = 0;
                                ai3 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai3].extraUpdates = 0;
                                ai4 = targetPlayer.Center.X + targetPlayer.velocity.X * 20;
                                ai5 = targetPlayer.Center.Y + targetPlayer.velocity.Y * 20;
                                ai6 = NPC.Center.X;
                                ai7 = NPC.Center.Y;
                                ai8 = Main.rand.NextFloat(-2f, 2f);
                                var leng = (targetPlayer.Center - NPC.Center).Length();
                                if (leng * Math.Abs(ai8) > 600)
                                {
                                    ai8 = (600 - Main.rand.NextFloat(-50, 50)) / leng * Math.Sign(ai8);
                                }
                            }
                            var rotationCenter = new Vector2(ai6 - ai4, ai7 - ai5);
                            rotationCenter = new Vector2(-rotationCenter.Y, rotationCenter.X) * ai8 + new Vector2(ai4 + ai6, ai5 + ai7) * .5f;
                            var toStart = new Vector2(ai6, ai7) - rotationCenter;
                            var toTarget = new Vector2(ai4, ai5) - rotationCenter;
                            var cross = toStart.CrossLength(toTarget);
                            //NPC.Center = MathHelper.Lerp(toStart.ToRotation(), toTarget.ToRotation(), (float)Math.Pow(counter / 60f, 2) * 2).ToRotationVector2() * toStart.Length() + rotationCenter;
                            //NPC.Center = toStart.RotatedBy(((cross > 0 ? MathHelper.TwoPi : 0) + new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation()) * (float)Math.Pow(counter / 60f, 2) * 2) + rotationCenter;

                            var t = 1 - (float)Math.Cos(counter / 60f * MathHelper.Pi);// (float)Math.Pow(counter / 60f, 2) * 2
                            //t /= 2f;
                            t *= .75f;
                            if (cross < 0)
                            {
                                var dummy = toStart;
                                toStart = toTarget;
                                toTarget = dummy;
                                t = 1 - t;
                                cross *= -1;
                            }
                            NPC.Center = toStart.RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation() * t) + rotationCenter;

                            //NPC.Center = (cross < 0 ? toTarget : toStart).RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), Math.Abs(cross)).ToRotation() * (cross < 0 ? (1 - t) : t)) + rotationCenter;
                            //前置方法:圆心O 起点P 终点T，逆时针生成一段圆弧
                            //p = (P-O).RotatedBy(new Vector2(Vector2.Dot(P-O,T-O),(P-O).CrossLength(T-O)).ToRotation*t)+O
                            //生成劣弧则是检测叉积为负就交换PT，保持时间原点不变就再反向一下t,

                            var _projectile = Main.projectile[(int)ai2];
                            var _count = 0;
                        _mylabel:
                            for (int n = 59; n > 3; n--)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                                _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[0];
                                _projectile.oldRot[n] = _projectile.oldRot[0];
                            }
                            Vector2 currentVec = NPC.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                _projectile.oldPos[n] = Vector2.Lerp(currentVec, _projectile.oldPos[4], n * .25f);//
                                var rotation = -timer + n * .25f;
                                _projectile.oldRot[n] = MathHelper.Pi / 10f * rotation - 3 * (float)Math.Sin(MathHelper.Pi / 30f * rotation) + _count * MathHelper.Pi;
                                //if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                            }
                            _projectile.Center = _projectile.oldPos[0];
                            _count++;
                            if (_count < 2)
                            {
                                switch (_count)
                                {
                                    case 1: _projectile = Main.projectile[(int)ai3]; break;
                                }
                                goto _mylabel;
                            }

                            if (timer % 5 == 0)
                            {
                                var projectile = Main.projectile[(int)ai2];
                                for (int n = 0; n < 2; n++)
                                {
                                    if (Main.rand.Next(10) < 7)
                                    {
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center, (projectile.oldRot[0] + n * MathHelper.Pi).ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                    }
                                }
                            }


                        }

                        ai1++;

                        if (ai1 >= 660)
                        {
                            SetAI(12);
                            break;
                        }

                        break;
                    }
                case AsraNoxState.破晓之光_追灭://TODO 破晓之光_追灭
                    {
                        var timer = (int)ai1 - 60;
                        if (timer == -60)
                        {
                            for (int n = 0; n < 7; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / 6f));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if (timer >= 0)
                        {
                            if (timer % 240 == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusLevatine>(), 60, 8, Main.myPlayer, MathHelper.PiOver2);
                                ai3 = AsraNoxSky.windDirection;
                                var _projectile = Main.projectile[(int)ai2];
                                for (int n = _projectile.oldPos.Length - 1; n >= 0; n--)
                                {
                                    _projectile.oldPos[n] = NPC.Center;
                                    _projectile.oldRot[n] = _projectile.ai[0];
                                }
                            }
                            var projectile = Main.projectile[(int)ai2];
                            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 1];
                                projectile.oldRot[n] = projectile.oldRot[n - 1];
                            }
                            visualPlayer.direction = (int)ai3;
                            if (timer % 240 >= 30)
                            {
                                NPC.Center += new Vector2((timer - (30f + timer / 240 * 240)).SymmetricalFactor(105, 60) * 16 * ai3, 0);
                                NPC.Center = Vector2.Lerp(NPC.Center, NPC.Center with { Y = targetPlayer.Center.Y - 400 }, 0.05f);
                                projectile.oldRot[0] = projectile.ai[0] = MathHelper.PiOver2;
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.PiOver2 * visualPlayer.direction);

                                if ((targetPlayer.Center.X - NPC.Center.X) * ai3 < 0 && timer % 4 == 0)
                                {
                                    #region 初源生成
                                    SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f, targetPlayer.velocity);
                                    #endregion
                                }
                            }
                            else
                            {
                                var rot = visualPlayer.compositeFrontArm.rotation;
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Terraria.Utils.AngleLerp(rot, -MathHelper.PiOver2 * visualPlayer.direction, 0.05f));
                                NPC.Center = Vector2.Lerp(NPC.Center, targetPlayer.Center + new Vector2(-visualPlayer.direction * 256, -200), 0.1f);


                            }
                            projectile.Center = projectile.oldPos[0] = NPC.Center + new Vector2(visualPlayer.direction * 6, 16);
                            if (timer % 24 == 0 && timer % 240 > 30)
                            {
                                var unit = projectile.oldRot[0].ToRotationVector2();
                                var normal = new Vector2(-unit.Y, unit.X) * ai3 * -1;
                                for (int n = 0; n < 12; n++)
                                {
                                    if (!Main.rand.NextBool(3))
                                    {
                                        var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center + unit * Main.rand.NextFloat(0, 1200), normal * 8f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj1.friendly = false;
                                        proj1.hostile = true;
                                    }
                                }
                            }
                        }


                        ai1++;

                        if (ai1 >= 780)
                        {
                            SetAI(NPC.life < NPC.lifeMax * 7 / 10 ? Main.rand.Next(13, 18) : Main.rand.Next(7, 12));//
                            break;
                        }

                        break;
                    }
                #endregion

                #region 随机
                case AsraNoxState.陨日残阳_随机://TODO 陨日残阳_随机
                    {
                        if (ai1 >= 660)
                        {
                            if (NPC.life < NPC.lifeMax * 3 / 10)
                            {
                                PrepareToEscape(660);
                            }
                            else
                            {
                                SetAI(Main.rand.Next(14, 19));
                            }
                            break;
                        }
                        var timer = (int)ai1;
                        int direct = timer >= 10 ? (timer - 10) / 20 % 2 : 0;
                        if (timer == 0)
                        {
                            ai2 = Main.rand.Next(-480, 480);
                            ai3 = ai1 >= 550 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                            ai4 = targetPlayer.Center.X;
                            ai5 = targetPlayer.Center.Y;
                        }
                        if (timer % 20 == 10)
                        {
                            if (timer <= 610)
                            {
                                if (timer != 10)
                                {
                                    ai2 = Main.rand.Next(-480, 480);
                                    ai3 = (ai1 >= 510 || Main.rand.NextBool(4)) ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                                    ai4 = targetPlayer.Center.X + targetPlayer.velocity.X * 30;
                                    ai5 = targetPlayer.Center.Y + targetPlayer.velocity.Y * 30;
                                }
                                ai7 = ai6;
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);
                            }
                            else if (timer == 630) ai7 = ai6;
                        }
                        if (timer >= 40)
                        {
                            const int timeMax = 20;
                            if (timer % timeMax == 0)
                            {
                                ai8 = ai7;
                            }
                            int counter = (timer - 40) % timeMax;
                            var projectile = Main.projectile[(int)ai8];
                            NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow(counter / (timeMax - 1f), 3);
                            projectile.Center = NPC.Center + new Vector2(0, 12);
                            visualPlayer.direction = Math.Sign(projectile.velocity.X);
                            visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            if (timer % 2 == 0)
                            {
                                var unit = projectile.velocity.SafeNormalize(default);
                                unit = unit.RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6 + MathHelper.PiOver2);
                                for (int n = 0; n < 2; n++)
                                {
                                    unit = -unit;
                                    if (!Main.rand.NextBool(3)) continue;
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;
                                }
                                if (Main.rand.NextBool((int)MathHelper.Clamp((targetPlayer.Center - NPC.Center).Length() / 16, 3, 64)))
                                {
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;
                                }

                            }
                        }
                        if (timer < 40)
                        {
                            var targetVec = timer < 30 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai7].Center;
                            NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                        }
                        ai1++;

                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.初源日炎_随机://TODO 初源日炎_随机
                    {
                        if (ai1 >= 780)
                        {
                            if (NPC.life < NPC.lifeMax * 3 / 10)
                            {
                                PrepareToEscape(780);
                            }
                            else
                            {
                                SetAI(Main.rand.NextBool(2) ? 13 : Main.rand.Next(15, 19));
                            }
                            break;
                        }

                        if ((int)ai1 < 765)
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                        else
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(ai5, ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);
                        NPC.damage = 0;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((int)ai1 % 10 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 1) * new Vector2(960, 540));
                        }
                        var counter = (int)ai1 % 120;
                        if (counter == 0)
                        {
                            ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                            Main.projectile[(int)ai4].extraUpdates = 0;
                            if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            if ((int)ai1 == 720)
                            {
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                            }
                        }
                        var projectile = Main.projectile[(int)ai4];
                        for (int n = 59; n > 3; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 4];
                            projectile.oldRot[n] = projectile.oldRot[n - 4];
                        }
                        for (int n = 1; n < 4; n++)
                        {
                            projectile.oldPos[n] = projectile.oldPos[0];
                            projectile.oldRot[n] = projectile.oldRot[0];
                        }
                        if (counter <= 60)
                        {
                            //projectile.oldPos[0] = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            //if ((int)ai1 < 720)
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //else
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //if (visualPlayer.direction == -1) projectile.oldRot[0] = MathHelper.Pi - projectile.oldRot[0];
                            Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                if ((int)ai1 < 720)
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                else
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                            }
                        }

                        if (counter == 60)
                        {
                            int max = (int)ai1 / 120 + 4;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if ((int)ai1 == 779)
                        {
                            for (int n = 0; n < 16; n++)
                            {
                                if (Main.rand.NextBool(3))
                                    SpawnFractal(NPC.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 64));
                                if (!Main.rand.NextBool(3))
                                    SpawnFractal(targetPlayer.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 1), 1) * new Vector2(960, 540));
                            }
                        }
                        ai1++;


                        NPC.oldPos[0] = NPC.oldPosition;

                        //if (counter <= 60 && counter >= 45 && counter % 3 == 0 && (int)ai1 < 720)
                        //{
                        //    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, 3, 1.05f);
                        //    proj.friendly = false;
                        //    proj.hostile = true;
                        //}

                        break;
                    }
                case AsraNoxState.星恒飞刃_随机://TODO 星恒飞刃_随机
                    {
                        if (ai1 >= 720)
                        {
                            if (NPC.life < NPC.lifeMax * 3 / 10)
                            {
                                PrepareToEscape(720);
                            }
                            else
                            {
                                SetAI(Main.rand.NextBool(2) ? Main.rand.Next(13, 15) : Main.rand.Next(16, 19));
                            }
                            break;
                        }
                        int timer = (int)ai1;
                        if (timer < 360)
                        {
                            int counter = timer % 180;
                            int stager = timer / 180 + 1;
                            if (counter % 60 == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter % 60 <= 30)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                                if (counter % 60 > 21)
                                {
                                    var factor = counter % 60 - 21f;
                                    factor /= 8f;
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(0.6f * MathHelper.Pi * (2 * factor - 1));//(MathHelper.Lerp(-MathHelper.Pi * .35f, MathHelper.Pi * .85f, factor) - MathHelper.Pi / 6).ToRotationVector2()
                                                                                                                                                            //unit *= new Vector2(visualPlayer.direction, 1);
                                    for (int n = 0; n < stager + counter / 60 + 1; n++)
                                    {
                                        var shootCenter = NPC.Center + 192 * unit;//.RotatedBy(n / (stager + 1f) * MathHelper.Pi * .15f)
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), shootCenter, (targetPlayer.Center - shootCenter).SafeNormalize(default).RotatedBy(MathHelper.Pi / 12f * (n - 0.5f * (stager + counter / 60))) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                        //proj.timeLeft = 31;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (timer % 180 == 0)
                            {
                                ai5 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai5].extraUpdates = 0;
                                if (Main.projectile[(int)ai5].ModProjectile is SolusKatanaFractal solusKatanaFractal_0)
                                {
                                    solusKatanaFractal_0.drawPlayer = new Player();
                                    solusKatanaFractal_0.drawPlayer.CopyVisuals(visualPlayer);
                                }
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai6].extraUpdates = 0;
                                if (Main.projectile[(int)ai6].ModProjectile is SolusKatanaFractal solusKatanaFractal_1)
                                {
                                    solusKatanaFractal_1.drawPlayer = new Player();
                                    solusKatanaFractal_1.drawPlayer.CopyVisuals(visualPlayer);
                                }
                                ai7 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai7].extraUpdates = 0;
                                if (Main.projectile[(int)ai7].ModProjectile is SolusKatanaFractal solusKatanaFractal_2)
                                {
                                    solusKatanaFractal_2.drawPlayer = new Player();
                                    solusKatanaFractal_2.drawPlayer.CopyVisuals(visualPlayer);
                                }
                                ai8 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 4);
                                Main.projectile[(int)ai8].extraUpdates = 0;
                                if (Main.projectile[(int)ai8].ModProjectile is SolusKatanaFractal solusKatanaFractal_3)
                                {
                                    solusKatanaFractal_3.drawPlayer = new Player();
                                    solusKatanaFractal_3.drawPlayer.CopyVisuals(visualPlayer);
                                }
                            }
                            if (timer % 180 < 60)
                            {
                                var target = targetPlayer.Center + (-MathHelper.Pi / 6).ToRotationVector2() * 400;
                                var projectile = Main.projectile[(int)ai5];
                                var count = 0;
                            mylabel:
                                projectile.Center += projectile.velocity;
                                Vector2 targetVec = target - projectile.Center;
                                targetVec.Normalize();
                                targetVec *= 40f;
                                projectile.velocity = (projectile.velocity * 15f + targetVec * 2) / 17f;
                                if (projectile.ModProjectile is SolusKatanaFractal solusKatanaFractal) solusKatanaFractal.drawPlayer.direction = Math.Sign(targetPlayer.Center.X - projectile.Center.X);//
                                count++;
                                if (count < 4)
                                {
                                    switch (count)
                                    {
                                        case 1: projectile = Main.projectile[(int)ai6]; target = targetPlayer.Center + (-MathHelper.Pi / 6 * 5).ToRotationVector2() * 400; break;
                                        case 2: projectile = Main.projectile[(int)ai7]; target = targetPlayer.Center + (-MathHelper.Pi / 4).ToRotationVector2() * 400; break;
                                        case 3: projectile = Main.projectile[(int)ai8]; target = targetPlayer.Center + (-MathHelper.Pi / 4 * 3).ToRotationVector2() * 400; break;
                                    }
                                    goto mylabel;
                                }
                            }
                            if (timer % 180 == 60)
                            {
                                var target = targetPlayer.Center + (-MathHelper.Pi / 6).ToRotationVector2() * 400;
                                var projectile = Main.projectile[(int)ai5];
                                var count = 0;
                            mylabel:
                                projectile.Center = target;
                                projectile.velocity = default;
                                count++;
                                if (count < 4)
                                {
                                    switch (count)
                                    {
                                        case 1: projectile = Main.projectile[(int)ai6]; target = targetPlayer.Center + (-MathHelper.Pi / 6 * 5).ToRotationVector2() * 400; break;
                                        case 2: projectile = Main.projectile[(int)ai7]; target = targetPlayer.Center + (-MathHelper.Pi / 4).ToRotationVector2() * 400; break;
                                        case 3: projectile = Main.projectile[(int)ai8]; target = targetPlayer.Center + (-MathHelper.Pi / 4 * 3).ToRotationVector2() * 400; break;
                                    }
                                    goto mylabel;
                                }
                            }
                            var _projectile = Main.projectile[(int)ai5];
                            var _count = 0;
                        _mylabel:
                            for (int n = 59; n > 3; n--)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                                _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[0];
                                _projectile.oldRot[n] = _projectile.oldRot[0];
                            }
                            Vector2 currentVec = _projectile.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                _projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                _projectile.oldRot[n] = (timer - n * .25f) * -MathHelper.Pi / 20f + 19 * MathHelper.PiOver2;
                                if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                            }
                            _count++;
                            if (_count < 4)
                            {
                                switch (_count)
                                {
                                    case 1: _projectile = Main.projectile[(int)ai6]; break;
                                    case 2: _projectile = Main.projectile[(int)ai7]; break;
                                    case 3: _projectile = Main.projectile[(int)ai8]; break;
                                }
                                goto _mylabel;
                            }
                            if (timer >= 420)
                            {
                                if (timer % 3 == 0)
                                {
                                    var projectile = Main.projectile[(int)ai5];
                                    var count = 0;
                                mylabel:
                                    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj.friendly = false;
                                    proj.hostile = true;

                                    count++;
                                    if (count < 4)
                                    {
                                        switch (count)
                                        {
                                            case 1: projectile = Main.projectile[(int)ai6]; break;
                                            case 2: projectile = Main.projectile[(int)ai7]; break;
                                            case 3: projectile = Main.projectile[(int)ai8]; break;
                                        }
                                        goto mylabel;
                                    }
                                }

                            }
                            else if (timer >= 600)
                            {

                            }
                        }

                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 800 * visualPlayer.direction, targetPlayer.Center.Y - 400 * (float)Math.Sin(ai1 / 240 * MathHelper.TwoPi) + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.05f);//
                        ai1++;
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.日曜星流_随机://TODO 日曜星流_随机
                    {
                        if (ai1 >= 720)
                        {
                            if (NPC.life < NPC.lifeMax * 3 / 10)
                            {
                                PrepareToEscape(720);
                            }
                            else
                            {
                                SetAI(Main.rand.NextBool(2) ? Main.rand.Next(13, 16) : Main.rand.Next(17, 19));
                            }
                            break;
                        }
                        //240一次，三个阶段
                        //一次重劈(120帧)，一次下刺(60帧)，一次冲锋(20帧)
                        //流星雨，随着阶段而加强
                        //初源量减少
                        //冲刺生成的弹幕有所改变
                        int timer = (int)ai1;
                        int counter = timer % 240;
                        int stager = timer / 240;

                        #region 流星生成
                        if (timer % (36 - stager * 4) == 0)
                        {
                            for (int n = 0; n < 5; n++)
                            {
                                if (Main.rand.NextBool(5)) continue;
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560)), new Vector2(Main.rand.NextFloat(4, 8), 6).SafeNormalize(default) * 3f, solusEnergyShard, 35, 0, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), Main.rand.NextFloat(Main.rand.NextFloat(1f, 1.05f), 1.05f));
                                proj.friendly = false;
                                proj.hostile = true;
                            }
                        }
                        #endregion

                        #region 初源生成
                        if (timer % 15 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 1) * new Vector2(960, 540));
                        }
                        #endregion


                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if (counter < 120)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.125f);

                            if (counter == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter <= 60)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }
                            if (counter == 60)
                            {
                                int max = stager + 3;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                        }
                        else if (counter < 180)
                        {
                            if (counter < 165)
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                            else
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(ai5, ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);

                            if (counter == 120)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                                //Main.NewText((ai5, ai6));


                            }
                            if (counter >= 120)
                            {
                                var projectile = Main.projectile[(int)ai4];
                                for (int n = 59; n > 3; n--)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[n - 4];
                                    projectile.oldRot[n] = projectile.oldRot[n - 4];
                                }
                                for (int n = 1; n < 4; n++)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[0];
                                    projectile.oldRot[n] = projectile.oldRot[0];
                                }
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 120 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }

                        }
                        else
                        {
                            int direct = visualPlayer.direction == -1 ? 0 : 1;
                            if (counter == 180)
                            {
                                ai2 = Main.rand.Next(-480, 480);
                                ai3 = stager != 0 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;

                                //for (int n = 0; n < 12 + stager * 2; n++)
                                //{
                                //    if (Main.rand.NextBool(3))
                                //        SpawnFractal(NPC.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 64));
                                //    if (!Main.rand.NextBool(3))
                                //        SpawnFractal(targetPlayer.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 1), 1) * new Vector2(960, 540));
                                //}
                                int max = stager + 4;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 256 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 256 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                            if (counter == 190)
                            {
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);

                            }
                            if (counter < 220)
                            {
                                var targetVec = counter < 190 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai6].Center;
                                NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                            }
                            if (counter >= 220)
                            {
                                const int timeMax = 20;
                                var projectile = Main.projectile[(int)ai6];
                                NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow((counter - 220f) / (timeMax - 1f), 3);
                                projectile.Center = NPC.Center + new Vector2(0, 12);
                                visualPlayer.direction = Math.Sign(projectile.velocity.X);
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                //if (timer % 2 == 0)
                                //{
                                //    var unit = projectile.velocity.SafeNormalize(default);
                                //    unit = unit.RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6 + MathHelper.PiOver2);
                                //    for (int n = 0; n < 2; n++)
                                //    {
                                //        unit = -unit;
                                //        if (!Main.rand.NextBool(3)) continue;
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }
                                //    if (Main.rand.NextBool((int)MathHelper.Clamp((targetPlayer.Center - NPC.Center).Length() / 16, 3, 64)))
                                //    {
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }

                                //}
                                if (timer % 2 == 0)
                                {
                                    int max = stager + 6;
                                    for (int n = 0; n < max; n++)
                                    {
                                        var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                        if (Main.rand.NextBool(3))
                                        {
                                            var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj1.friendly = false;
                                            proj1.hostile = true;
                                        }
                                        if (Main.rand.NextBool(3))
                                        {
                                            var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj2.friendly = false;
                                            proj2.hostile = true;
                                        }
                                        if (Main.rand.NextBool(3))
                                        {
                                            var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj3.friendly = false;
                                            proj3.hostile = true;
                                        }
                                    }
                                }
                            }
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        ai1++;
                        break;
                    }
                case AsraNoxState.太阳风暴_随机://TODO 太阳风暴_随机
                    {
                        if (ai1 >= 660)
                        {
                            if (NPC.life < NPC.lifeMax * 3 / 10)
                            {
                                PrepareToEscape(660);
                            }
                            else
                            {
                                SetAI(Main.rand.NextBool(2) ? Main.rand.Next(13, 17) : 18);
                            }
                            break;
                        }
                        //ai0状态 ai1计时 ai2 ai3控制发射弹幕 ai4 ai5记录玩家坐标 ai6 ai7记录起始位置 ai8记录旋转中心位置
                        var timer = (int)ai1;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((timer - 30) % 60 == 0 && timer != 30)
                        {
                            int max = (timer - 30) / 120 + 3;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if (timer < 30)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 400 * visualPlayer.direction, targetPlayer.Center.Y), 0.05f);//
                        }
                        else if (timer < 630)
                        {
                            timer -= 30;
                            var counter = timer % 60;
                            if (counter == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai2].extraUpdates = 0;
                                ai3 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai3].extraUpdates = 0;
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;
                                ai6 = NPC.Center.X;
                                ai7 = NPC.Center.Y;
                                ai8 = Main.rand.NextFloat(-2f, 2f);
                                var leng = (targetPlayer.Center - NPC.Center).Length();
                                if (leng * Math.Abs(ai8) > 600)
                                {
                                    ai8 = (600 - Main.rand.NextFloat(-50, 50)) / leng * Math.Sign(ai8);
                                }
                            }
                            var rotationCenter = new Vector2(ai6 - ai4, ai7 - ai5);
                            rotationCenter = new Vector2(-rotationCenter.Y, rotationCenter.X) * ai8 + new Vector2(ai4 + ai6, ai5 + ai7) * .5f;
                            var toStart = new Vector2(ai6, ai7) - rotationCenter;
                            var toTarget = new Vector2(ai4, ai5) - rotationCenter;
                            var cross = toStart.CrossLength(toTarget);
                            //NPC.Center = MathHelper.Lerp(toStart.ToRotation(), toTarget.ToRotation(), (float)Math.Pow(counter / 60f, 2) * 2).ToRotationVector2() * toStart.Length() + rotationCenter;
                            //NPC.Center = toStart.RotatedBy(((cross > 0 ? MathHelper.TwoPi : 0) + new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation()) * (float)Math.Pow(counter / 60f, 2) * 2) + rotationCenter;

                            var t = 1 - (float)Math.Cos(counter / 60f * MathHelper.Pi);// (float)Math.Pow(counter / 60f, 2) * 2
                            //t *= 1.5f;
                            t *= 3 / 4f;
                            if (cross < 0)
                            {
                                var dummy = toStart;
                                toStart = toTarget;
                                toTarget = dummy;
                                t = 1 - t;
                                cross *= -1;
                            }
                            NPC.Center = toStart.RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation() * t) + rotationCenter;

                            //NPC.Center = (cross < 0 ? toTarget : toStart).RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), Math.Abs(cross)).ToRotation() * (cross < 0 ? (1 - t) : t)) + rotationCenter;
                            //前置方法:圆心O 起点P 终点T，逆时针生成一段圆弧
                            //p = (P-O).RotatedBy(new Vector2(Vector2.Dot(P-O,T-O),(P-O).CrossLength(T-O)).ToRotation*t)+O
                            //生成劣弧则是检测叉积为负就交换PT，保持时间原点不变就再反向一下t,

                            var _projectile = Main.projectile[(int)ai2];
                            var _count = 0;
                        _mylabel:
                            for (int n = 59; n > 3; n--)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                                _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[0];
                                _projectile.oldRot[n] = _projectile.oldRot[0];
                            }
                            Vector2 currentVec = NPC.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                _projectile.oldPos[n] = Vector2.Lerp(currentVec, _projectile.oldPos[4], n * .25f);//
                                var rotation = -timer + n * .25f;
                                _projectile.oldRot[n] = MathHelper.Pi / 10f * rotation - 3 * (float)Math.Sin(MathHelper.Pi / 30f * rotation) + _count * MathHelper.Pi;
                                //if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                            }
                            _projectile.Center = _projectile.oldPos[0];
                            _count++;
                            if (_count < 2)
                            {
                                switch (_count)
                                {
                                    case 1: _projectile = Main.projectile[(int)ai3]; break;
                                }
                                goto _mylabel;
                            }

                            if (timer % 4 == 0)
                            {
                                var projectile = Main.projectile[(int)ai2];
                                for (int n = 0; n < 2; n++)
                                {
                                    if (Main.rand.Next(10) < 7)
                                    {
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center, (projectile.oldRot[0] + n * MathHelper.Pi).ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                    }
                                }
                            }


                        }

                        ai1++;

                        break;
                    }
                case AsraNoxState.破晓之光_随机://TODO 破晓之光_随机
                    {
                        if (ai1 >= 780)
                        {
                            if (NPC.life < NPC.lifeMax * 3 / 10)
                            {
                                PrepareToEscape(780);
                            }
                            else
                            {
                                SetAI(Main.rand.Next(13, 18));
                            }
                            break;
                        }
                        //分三个阶段
                        //前两个240秒，最后一个300秒
                        //转动一圈
                        //持剑平移
                        //杀意百合
                        //ai1计时 ai2记弹幕 ai3记录方向
                        var timer = (int)ai1 - 60;
                        if (timer == -60)
                        {
                            for (int n = 0; n < 7; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / 6f));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if (timer % 240 == 0)
                        {
                            ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusLevatine>(), 60, 8, Main.myPlayer, -MathHelper.PiOver2);
                            ai3 = Math.Sign(new Vector2(0, -1).CrossLength(targetPlayer.Center - NPC.Center));
                            var _projectile = Main.projectile[(int)ai2];
                            for (int n = _projectile.oldPos.Length - 1; n >= 0; n--)
                            {
                                _projectile.oldPos[n] = NPC.Center;
                                _projectile.oldRot[n] = _projectile.ai[0];
                            }
                        }
                        var projectile = Main.projectile[(int)ai2];
                        for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 1];
                            projectile.oldRot[n] = projectile.oldRot[n - 1];
                        }
                        visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi);
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);

                        if (timer % 240 >= 30)
                        {
                            var factor = (timer - 30f) % 240;
                            factor /= 210f;
                            factor *= factor;
                            projectile.oldRot[0] = projectile.ai[0] = factor * MathHelper.TwoPi * ai3 - MathHelper.PiOver2;
                            visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.oldRot[0] - MathHelper.PiOver2);
                            visualPlayer.direction = Math.Sign(Math.Cos(projectile.ai[0]));
                        }
                        else
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, targetPlayer.Center + new Vector2(-visualPlayer.direction * 400, 320), 0.1f);
                        }
                        projectile.Center = projectile.oldPos[0] = NPC.Center + projectile.ai[0].ToRotationVector2() * 20 * new Vector2(.5f, 1) + new Vector2(0, 12);

                        if (timer % 24 == 0 && timer % 240 > 30)
                        {
                            var unit = projectile.oldRot[0].ToRotationVector2();
                            var normal = new Vector2(-unit.Y, unit.X) * ai3 * (timer > 240 ? -1 : 1);
                            for (int n = 0; n < 12; n++)
                            {
                                if (!Main.rand.NextBool(3))
                                {
                                    var start = projectile.Center + unit * Main.rand.NextFloat(0, 1200);
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), start, (Main.rand.NextBool(3) ? (targetPlayer.Center - start).SafeNormalize(default) : normal) * 8f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;
                                }
                            }
                            if (Main.rand.NextBool(8))
                            {
                                var targetPos = targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960), 540);
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPos - NPC.Center) / 15, solusEnergyShard, 45, 4, Main.myPlayer, 7, 1f);
                                proj1.friendly = false;
                                proj1.hostile = true;
                                proj1.timeLeft = 46;
                            }
                        }

                        ai1++;
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                #endregion

                #region 后撤
                case AsraNoxState.陨日残阳_后撤://TODO 陨日残阳_后撤
                    {
                        if (ai1 >= 660)
                        {
                            var flag = NPC.life < NPC.lifeMax / 4;
                            TakeARest(660, flag ? 20 : 19);
                            if (flag && ai1 > 780) NPC.life = NPC.lifeMax / 4;
                            break;
                        }
                        var timer = (int)ai1;
                        int direct = -AsraNoxSky.windDirection;
                        if (timer == 0)
                        {
                            ai2 = Main.rand.Next(-480, 480);
                            ai3 = -2 * ai2;
                            ai4 = targetPlayer.Center.X;
                            ai5 = targetPlayer.Center.Y;
                        }
                        if (timer % 20 == 10)
                        {
                            if (timer <= 610)
                            {
                                if (timer != 10)
                                {
                                    ai2 = Main.rand.Next(-480, 480);
                                    ai3 = -2 * ai2;
                                    ai4 = targetPlayer.Center.X;
                                    ai5 = targetPlayer.Center.Y;
                                }
                                ai7 = ai6;
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);
                            }
                            else if (timer == 630) ai7 = ai6;
                        }
                        if (timer >= 40)
                        {
                            const int timeMax = 20;
                            if (timer % timeMax == 0)
                            {
                                ai8 = ai7;
                            }
                            int counter = (timer - 40) % timeMax;
                            var projectile = Main.projectile[(int)ai8];
                            NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow(counter / (timeMax - 1f), 3);
                            projectile.Center = NPC.Center + new Vector2(0, 12);
                            visualPlayer.direction = Math.Sign(projectile.velocity.X);
                            visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                            if (timer % 12 == 0)
                            {
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 12f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.02f);
                                proj.friendly = false;
                                proj.hostile = true;
                            }
                        }
                        if (timer < 40)
                        {
                            var targetVec = timer < 30 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai7].Center;
                            NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                        }
                        ai1++;

                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.初源日炎_后撤://TODO 初源日炎_后撤
                    {
                        if (ai1 >= 780)
                        {
                            var flag = NPC.life < NPC.lifeMax / 5;
                            TakeARest(780, flag ? 21 : 20);
                            if (flag && ai1 > 900) NPC.life = NPC.lifeMax / 5;
                            break;
                        }
                        if ((int)ai1 < 765)
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 600 * (AsraNoxSky.windDirection), targetPlayer.Center.Y + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                        else
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 400 * (AsraNoxSky.windDirection), ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);
                        NPC.damage = 0;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((int)ai1 % 10 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f + ((int)ai1 % 60 < ai1 / 13f ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960)), default, (Main.rand.NextFloat(-MathHelper.Pi / 24, MathHelper.Pi / 24) + (Main.rand.NextBool(2) ? -MathHelper.PiOver2 : MathHelper.PiOver2)).ToRotationVector2());
                        }
                        var counter = (int)ai1 % 120;
                        if (counter == 0)
                        {
                            ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                            Main.projectile[(int)ai4].extraUpdates = 0;
                            if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            if ((int)ai1 == 720)
                            {
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                            }
                        }
                        var projectile = Main.projectile[(int)ai4];
                        for (int n = 59; n > 3; n--)
                        {
                            projectile.oldPos[n] = projectile.oldPos[n - 4];
                            projectile.oldRot[n] = projectile.oldRot[n - 4];
                        }
                        for (int n = 1; n < 4; n++)
                        {
                            projectile.oldPos[n] = projectile.oldPos[0];
                            projectile.oldRot[n] = projectile.oldRot[0];
                        }
                        if (counter <= 60)
                        {
                            //projectile.oldPos[0] = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            //if ((int)ai1 < 720)
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //else
                            //    projectile.oldRot[0] = (1 - (1 - MathHelper.Clamp(ai1 % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .75f) - MathHelper.Pi / 6;
                            //if (visualPlayer.direction == -1) projectile.oldRot[0] = MathHelper.Pi - projectile.oldRot[0];
                            Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                if ((int)ai1 < 720)
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                else
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                            }
                        }
                        ai1++;

                        if (counter == 60 || (int)ai1 == 780)
                        {
                            int max = (int)ai1 / 120 + 4;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }


                        NPC.oldPos[0] = NPC.oldPosition;

                        //if (counter <= 60 && counter >= 45 && counter % 3 == 0 && (int)ai1 < 720)
                        //{
                        //    var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, projectile.oldRot[0].ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, 3, 1.05f);
                        //    proj.friendly = false;
                        //    proj.hostile = true;
                        //}

                        break;
                    }
                case AsraNoxState.星恒飞刃_后撤://TODO 星恒飞刃_后撤
                    {
                        if (ai1 >= 720)
                        {
                            var flag = NPC.life < NPC.lifeMax * 3 / 20;
                            TakeARest(720, flag ? 22 : 21);
                            if (flag && ai1 > 840) NPC.life = NPC.lifeMax * 3 / 20;
                            break;
                        }
                        int timer = (int)ai1;
                        //前180帧妖梦非符同款攻击
                        //后60帧如下安排
                        //20帧移动，40帧发射弹幕，最后10帧隐去

                        //这里是新安排
                        //前3*180帧妖梦非符同款攻击
                        //最后180帧中60帧移动，120帧发射弹幕
                        int counter = timer % 180;
                        int stager = timer / 180;
                        if (counter % 60 == 0)
                        {
                            ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                            Main.projectile[(int)ai4].extraUpdates = 0;
                            if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                        }
                        var projectile = Main.projectile[(int)ai4];
                        if (projectile.type == ModContent.ProjectileType<SolusKatanaFractal>())
                        {
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter % 60 <= 30)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                                if (counter % 60 > 21)
                                {
                                    var factor = counter % 60 - 21f;
                                    factor /= 8f;
                                    var unit = (targetPlayer.Center + targetPlayer.velocity * 60 - NPC.Center).SafeNormalize(default).RotatedBy(0.6f * MathHelper.Pi * (2 * factor - 1));//(MathHelper.Lerp(-MathHelper.Pi * .35f, MathHelper.Pi * .85f, factor) - MathHelper.Pi / 6).ToRotationVector2()
                                                                                                                                                                                         //unit *= new Vector2(visualPlayer.direction, 1);
                                    for (int n = 0; n < stager + counter / 60 + 1; n++)
                                    {
                                        var shootCenter = NPC.Center + 192 * unit;//.RotatedBy(n / (stager + 1f) * MathHelper.Pi * .15f)
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), shootCenter, (targetPlayer.Center + targetPlayer.velocity * 30 - shootCenter).SafeNormalize(default).RotatedBy(MathHelper.Pi / 12f * (n - 0.5f * (stager + counter / 60))) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                        //proj.timeLeft = 31;
                                    }
                                }
                            }
                        }


                        visualPlayer.direction = AsraNoxSky.windDirection;
                        NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 600 * visualPlayer.direction, targetPlayer.Center.Y - 400 * (float)Math.Sin(ai1 / 240 * MathHelper.TwoPi) + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.05f);//
                        ai1++;

                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.日曜星流_后撤://TODO 日曜星流_后撤
                    {
                        if (ai1 >= 720)
                        {
                            var flag = NPC.life < NPC.lifeMax / 10;
                            TakeARest(720, flag ? 23 : 22);
                            if (flag && ai1 > 840) NPC.life = NPC.lifeMax / 10;
                            break;
                        }
                        //240一次，三个阶段
                        //一次重劈(120帧)，一次下刺(60帧)，一次冲锋(20帧)
                        //流星雨，随着阶段而加强
                        //初源量减少
                        //冲刺生成的弹幕有所改变
                        int timer = (int)ai1;
                        int counter = timer % 240;
                        int stager = timer / 240;

                        #region 流星生成
                        if (timer % (36 - stager * 4) == 0)
                        {
                            for (int n = 0; n < 4; n++)
                            {
                                if (Main.rand.NextBool(4)) continue;
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), targetPlayer.Center + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(480, 560)), new Vector2(Main.rand.NextFloat(4, 8), 6).SafeNormalize(default) * 3f, solusEnergyShard, 35, 0, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), Main.rand.NextFloat(Main.rand.NextFloat(1f, 1.05f), 1.05f));
                                proj.friendly = false;
                                proj.hostile = true;
                            }
                        }
                        #endregion

                        #region 初源生成
                        if (timer % 20 == 0)
                        {
                            SpawnFractal(targetPlayer.Center + targetPlayer.velocity * 20f + ((int)ai1 % 60 < ai1 / 13f ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960)), default, (Main.rand.NextFloat(-MathHelper.Pi / 24, MathHelper.Pi / 24) + (Main.rand.NextBool(2) ? -MathHelper.PiOver2 : MathHelper.PiOver2)).ToRotationVector2());
                        }
                        #endregion


                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if (counter < 120)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 600 * (AsraNoxSky.windDirection), targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.125f);

                            if (counter == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                            }
                            var projectile = Main.projectile[(int)ai4];
                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            if (counter <= 60)
                            {
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }
                            if (counter == 60)
                            {
                                int max = stager + 3;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                        }
                        else if (counter < 180)
                        {
                            if (counter < 165)
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 600 * (AsraNoxSky.windDirection), targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                            else
                                NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 400 * (AsraNoxSky.windDirection), ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);

                            if (counter == 120)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;
                                ai5 = targetPlayer.Center.X;
                                ai6 = targetPlayer.Center.Y;
                                //Main.NewText((ai5, ai6));


                            }
                            if (counter >= 120)
                            {
                                var projectile = Main.projectile[(int)ai4];
                                for (int n = 59; n > 3; n--)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[n - 4];
                                    projectile.oldRot[n] = projectile.oldRot[n - 4];
                                }
                                for (int n = 1; n < 4; n++)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[0];
                                    projectile.oldRot[n] = projectile.oldRot[0];
                                }
                                Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                for (int n = 0; n < 4; n++)
                                {
                                    projectile.oldPos[n] = Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                    projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((counter - 120 - 0.25f * n) % 120 / 60f, 0, 1)).HillFactor2()).Lerp(MathHelper.Pi * 0.25f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                    if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                }
                            }

                        }
                        else
                        {
                            int direct = visualPlayer.direction == -1 ? 0 : 1;
                            if (counter == 180)
                            {
                                ai2 = Main.rand.Next(-480, 480);
                                ai3 = stager == 2 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;

                                int max = stager + 4;
                                for (int n = 0; n < max; n++)
                                {
                                    var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;

                                    var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj2.friendly = false;
                                    proj2.hostile = true;

                                    var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                    proj3.friendly = false;
                                    proj3.hostile = true;
                                }
                            }
                            if (counter == 190)
                            {
                                var start = new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2;
                                ai6 = Projectile.NewProjectile(NPC.GetSource_FromAI(), start, new Vector2(direct == 1 ? 2048 : -2048, ai3), ModContent.ProjectileType<SolusDash>(), 45, 4, Main.myPlayer, start.X, start.Y);

                            }
                            if (counter < 220)
                            {
                                var targetVec = counter < 190 ? new Vector2(direct == 1 ? -1024 : 1024, 0) + new Vector2(ai4, ai5) + new Vector2(0, 1) * ai2 : Main.projectile[(int)ai6].Center;
                                NPC.Center = Vector2.Lerp(NPC.Center, targetVec, 0.05f);
                            }
                            if (counter >= 220)
                            {
                                const int timeMax = 20;
                                var projectile = Main.projectile[(int)ai6];
                                NPC.Center = new Vector2(projectile.ai[0], projectile.ai[1]) + projectile.velocity * (float)Math.Pow((counter - 220f) / (timeMax - 1f), 3);
                                projectile.Center = NPC.Center + new Vector2(0, 12);
                                visualPlayer.direction = Math.Sign(projectile.velocity.X);
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                visualPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, projectile.velocity.ToRotation() - MathHelper.PiOver2);
                                //if (timer % 2 == 0)
                                //{
                                //    var unit = projectile.velocity.SafeNormalize(default);
                                //    unit = unit.RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 6 + MathHelper.PiOver2);
                                //    for (int n = 0; n < 2; n++)
                                //    {
                                //        unit = -unit;
                                //        if (!Main.rand.NextBool(3)) continue;
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }
                                //    if (Main.rand.NextBool((int)MathHelper.Clamp((targetPlayer.Center - NPC.Center).Length() / 16, 3, 64)))
                                //    {
                                //        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(Main.rand.NextFloat(-1, 1) * MathHelper.Pi / 32) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                //        proj.friendly = false;
                                //        proj.hostile = true;
                                //    }

                                //}
                                if (timer % 2 == 0)
                                {
                                    int max = stager + 6;
                                    for (int n = 0; n < max; n++)
                                    {
                                        var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj1.friendly = false;
                                            proj1.hostile = true;
                                        }
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj2.friendly = false;
                                            proj2.hostile = true;
                                        }
                                        if (Main.rand.NextBool(4))
                                        {
                                            var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                            proj3.friendly = false;
                                            proj3.hostile = true;
                                        }
                                    }
                                }
                            }
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        ai1++;

                        break;
                    }
                case AsraNoxState.太阳风暴_后撤://TODO 太阳风暴_后撤
                    {
                        if (ai1 >= 660)
                        {
                            var flag = NPC.life < NPC.lifeMax / 20;
                            TakeARest(660, flag ? 24 : 23);
                            if (flag && ai1 > 780) NPC.life = NPC.lifeMax / 20;
                            break;
                        }
                        //ai0状态 ai1计时 ai2 ai3控制发射弹幕 ai4 ai5记录玩家坐标 ai6 ai7记录起始位置 ai8记录旋转中心位置
                        var timer = (int)ai1;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((timer - 30) % 60 == 0 && timer != 30)
                        {
                            int max = (timer - 30) / 120 + 5;
                            for (int n = 0; n < max; n++)
                            {
                                var unit = (targetPlayer.Center - NPC.Center).SafeNormalize(default).RotatedBy(MathHelper.Lerp(-MathHelper.Pi / 3, MathHelper.Pi / 3, n / (max - 1f)));
                                var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, unit * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj1.friendly = false;
                                proj1.hostile = true;

                                var proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(unit.Y, -unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj2.friendly = false;
                                proj2.hostile = true;

                                var proj3 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + 128 * unit, new Vector2(-unit.Y, unit.X) * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 0, 2, 3, 3 }), 1.05f);
                                proj3.friendly = false;
                                proj3.hostile = true;
                            }
                        }
                        if (timer < 30)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 400 * visualPlayer.direction, targetPlayer.Center.Y), 0.05f);//
                        }
                        else if (timer < 630)
                        {
                            timer -= 30;
                            var counter = timer % 60;
                            if (counter == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai2].extraUpdates = 0;
                                ai3 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 2);
                                Main.projectile[(int)ai3].extraUpdates = 0;
                                ai4 = targetPlayer.Center.X + AsraNoxSky.windDirection * 960 + targetPlayer.velocity.X * 30;
                                ai5 = targetPlayer.Center.Y + Main.rand.NextFloat(-540, 540) + targetPlayer.velocity.Y * 30;
                                ai6 = NPC.Center.X;
                                ai7 = NPC.Center.Y;
                                ai8 = Main.rand.NextFloat(-2f, 2f);
                                var leng = (targetPlayer.Center - NPC.Center).Length();
                                if (leng * Math.Abs(ai8) > 600)
                                {
                                    ai8 = (600 - Main.rand.NextFloat(-50, 50)) / leng * Math.Sign(ai8);
                                }
                            }
                            var rotationCenter = new Vector2(ai6 - ai4, ai7 - ai5);
                            rotationCenter = new Vector2(-rotationCenter.Y, rotationCenter.X) * ai8 + new Vector2(ai4 + ai6, ai5 + ai7) * .5f;
                            var toStart = new Vector2(ai6, ai7) - rotationCenter;
                            var toTarget = new Vector2(ai4, ai5) - rotationCenter;
                            var cross = toStart.CrossLength(toTarget);
                            //NPC.Center = MathHelper.Lerp(toStart.ToRotation(), toTarget.ToRotation(), (float)Math.Pow(counter / 60f, 2) * 2).ToRotationVector2() * toStart.Length() + rotationCenter;
                            //NPC.Center = toStart.RotatedBy(((cross > 0 ? MathHelper.TwoPi : 0) + new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation()) * (float)Math.Pow(counter / 60f, 2) * 2) + rotationCenter;

                            var t = 1 - (float)Math.Cos(counter / 60f * MathHelper.Pi);// (float)Math.Pow(counter / 60f, 2) * 2
                            t *= .5f;
                            if (cross < 0)
                            {
                                var dummy = toStart;
                                toStart = toTarget;
                                toTarget = dummy;
                                t = 1 - t;
                                cross *= -1;
                            }
                            NPC.Center = toStart.RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), cross).ToRotation() * t) + rotationCenter;

                            //NPC.Center = (cross < 0 ? toTarget : toStart).RotatedBy(new Vector2(Vector2.Dot(toStart, toTarget), Math.Abs(cross)).ToRotation() * (cross < 0 ? (1 - t) : t)) + rotationCenter;
                            //前置方法:圆心O 起点P 终点T，逆时针生成一段圆弧
                            //p = (P-O).RotatedBy(new Vector2(Vector2.Dot(P-O,T-O),(P-O).CrossLength(T-O)).ToRotation*t)+O
                            //生成劣弧则是检测叉积为负就交换PT，保持时间原点不变就再反向一下t,

                            var _projectile = Main.projectile[(int)ai2];
                            var _count = 0;
                        _mylabel:
                            for (int n = 59; n > 3; n--)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[n - 4];
                                _projectile.oldRot[n] = _projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                _projectile.oldPos[n] = _projectile.oldPos[0];
                                _projectile.oldRot[n] = _projectile.oldRot[0];
                            }
                            Vector2 currentVec = NPC.Center - _projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                            for (int n = 0; n < 4; n++)
                            {
                                _projectile.oldPos[n] = Vector2.Lerp(currentVec, _projectile.oldPos[4], n * .25f);//
                                var rotation = -timer + n * .25f;
                                _projectile.oldRot[n] = MathHelper.Pi / 10f * rotation - 3 * (float)Math.Sin(MathHelper.Pi / 30f * rotation) + _count * MathHelper.Pi;
                                //if (_count % 2 == 1) _projectile.oldRot[n] = MathHelper.Pi - _projectile.oldRot[n];
                            }
                            _projectile.Center = _projectile.oldPos[0];
                            _count++;
                            if (_count < 2)
                            {
                                switch (_count)
                                {
                                    case 1: _projectile = Main.projectile[(int)ai3]; break;
                                }
                                goto _mylabel;
                            }

                            if (timer % 4 == 0)
                            {
                                var projectile = Main.projectile[(int)ai2];
                                for (int n = 0; n < 2; n++)
                                {
                                    if (Main.rand.Next(10) < 7)
                                    {
                                        var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center, (projectile.oldRot[0] + n * MathHelper.Pi).ToRotationVector2() * 5f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                                        proj.friendly = false;
                                        proj.hostile = true;
                                    }
                                }
                            }


                        }

                        ai1++;



                        break;
                    }
                case AsraNoxState.破晓之光_后撤://TODO 破晓之光_后撤
                    {
                        if (ai1 >= 780)
                        {
                            TakeARest(780, 24);
                            break;
                        }
                        //分三个阶段
                        //前两个240秒，最后一个300秒
                        //转动一圈
                        //持剑平移
                        //杀意百合
                        //ai1计时 ai2记弹幕 ai3记录方向
                        var timer = (int)ai1;
                        if (timer < 480)
                        {
                            if (timer % 240 == 0)
                            {
                                ai2 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusLevatine>(), 60, 8, Main.myPlayer, MathHelper.PiOver2 * (1 + AsraNoxSky.windDirection));
                                ai3 = Math.Sign(timer - 239) * AsraNoxSky.windDirection;
                                var _projectile = Main.projectile[(int)ai2];
                                for (int n = _projectile.oldPos.Length - 1; n >= 0; n--)
                                {
                                    _projectile.oldPos[n] = NPC.Center;
                                    _projectile.oldRot[n] = _projectile.ai[0];
                                }
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;
                            }
                            var projectile = Main.projectile[(int)ai2];
                            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 1];
                                projectile.oldRot[n] = projectile.oldRot[n - 1];
                            }

                            visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi);
                            visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);

                            if (timer >= 30)
                            {
                                var factor = timer % 240 - 30f;
                                factor /= 210f;
                                factor *= factor;
                                projectile.oldRot[0] = projectile.ai[0] = factor * MathHelper.TwoPi / 8f * ai3 + (MathHelper.PiOver2 * (1 + AsraNoxSky.windDirection));
                                visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.oldRot[0] - MathHelper.PiOver2);
                                visualPlayer.direction = Math.Sign(Math.Cos(projectile.ai[0]));
                            }
                            NPC.Center = Vector2.Lerp(NPC.Center, Vector2.Lerp(targetPlayer.Center, new Vector2(ai4, ai5), MathHelper.Clamp(timer / 150f, 0, 0.2f)) + new Vector2(AsraNoxSky.windDirection * 960, 480 * ai3 * AsraNoxSky.windDirection), 0.1f);
                            projectile.Center = projectile.oldPos[0] = NPC.Center + projectile.ai[0].ToRotationVector2() * 20 * new Vector2(.5f, 1) + new Vector2(0, 12);
                            //if (timer % 24 == 0 && timer % 240 > 30)
                            //{
                            //    var unit = projectile.oldRot[0].ToRotationVector2();
                            //    var normal = new Vector2(-unit.Y, unit.X) * ai3 * (timer > 240 ? -1 : 1);
                            //    for (int n = 0; n < 12; n++)
                            //    {
                            //        if (!Main.rand.NextBool(3))
                            //        {
                            //            var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projectile.Center + unit * Main.rand.NextFloat(0, 1200), normal * 8f, solusEnergyShard, 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                            //            proj1.friendly = false;
                            //            proj1.hostile = true;
                            //        }
                            //    }
                            //}
                        }
                        else
                        {
                            visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X + 1200 * (AsraNoxSky.windDirection), targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.05f);

                            if (timer % 60 == 0)
                            {
                                ai4 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, default, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, 0, 3);
                                Main.projectile[(int)ai4].extraUpdates = 0;
                                if (Main.projectile[(int)ai4].ModProjectile is SolusKatanaFractal skf) skf.drawPlayer = visualPlayer;

                            }
                            if (timer % 60 == 30)
                            {
                                for (int n = 0; n < 3; n++)
                                {
                                    var targetPos = new Vector2(NPC.Center.X - Main.rand.Next(0, Main.rand.Next(0, 1920)) * AsraNoxSky.windDirection, targetPlayer.Center.Y + 540);
                                    var proj1 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (targetPos - NPC.Center) / 15, solusEnergyShard, 45, 4, Main.myPlayer, 7, 1f);
                                    proj1.friendly = false;
                                    proj1.hostile = true;
                                    proj1.timeLeft = 46;
                                }
                            }
                            var projectile = Main.projectile[(int)ai4];
                            if (projectile.type == ModContent.ProjectileType<SolusKatanaFractal>())
                            {
                                for (int n = 59; n > 3; n--)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[n - 4];
                                    projectile.oldRot[n] = projectile.oldRot[n - 4];
                                }
                                for (int n = 1; n < 4; n++)
                                {
                                    projectile.oldPos[n] = projectile.oldPos[0];
                                    projectile.oldRot[n] = projectile.oldRot[0];
                                }
                                if (timer % 60 <= 30)
                                {
                                    Vector2 currentVec = NPC.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                                    for (int n = 0; n < 4; n++)
                                    {
                                        projectile.oldPos[n] = currentVec;//Vector2.Lerp(currentVec, projectile.oldPos[4], n * .25f);
                                        projectile.oldRot[n] = (1 - (1 - MathHelper.Clamp((ai1 - 0.25f * n) % 60 / 30f, 0, 1)).HillFactor2()).Lerp(-MathHelper.Pi * 0.75f, MathHelper.Pi * .875f) - MathHelper.Pi / 6;
                                        if (visualPlayer.direction == -1) projectile.oldRot[n] = MathHelper.Pi - projectile.oldRot[n];
                                    }
                                }
                            }


                            //if (timer % 60 == 0)
                            //{
                            //    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-960, 960), 540), default, ModContent.ProjectileType<SolusUltraLaser>(), 80, 8, Main.myPlayer, 0, 0);
                            //}
                        }
                        ai1++;


                        break;
                    }
                    #endregion

            }


        }
        private void PrepareToEscape(int starter)
        {
            //放弃了，本来想让音乐淡出然后重新播放的，但是我整不出
            //ai1++;
            //var timer = (int)ai1 - starter;
            ////if (timer == 1)
            ////{
            ////    ai2 = Main.musicVolume;
            ////}
            ////Music = 0;
            //NPC.dontTakeDamage = true;
            //if (timer > 120)
            //{
            //    SetAI(19);
            //    //Main.musicVolume = ai2;
            //}
            ////else
            ////{
            ////    Main.musicVolume = MathHelper.Clamp(1 - timer / 60f, 0, 1) * ai2;
            ////}
            //
            SetAI(19);

        }
        private void TakeARest(int starter, int stateToSet)
        {
            ai1++;
            var timer = (int)ai1 - starter;
            if (timer < 105)
            {
                NPC.Center = Vector2.Lerp(NPC.Center, targetPlayer.Center + new Vector2(960 * AsraNoxSky.windDirection, -400), 0.05f);
            }
            else
            {
                AsraNoxSky.windToLeft = targetPlayer.Center.X - NPC.Center.X > 0;
            }
            if (timer > 120)
            {
                SetAI(stateToSet);
            }
        }
        public Player visualPlayer;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (state != 0)
            {
                var tex = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Meteor").Value;
                if (state == AsraNoxState.陨日残阳) visualPlayer.firstFractalAfterImageOpacity = MathHelper.Clamp(ai1 / 40f, 0, 1);
                var alpha = visualPlayer.firstFractalAfterImageOpacity;

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

                var timer = (int)ai1;
                var nState = (byte)state;
                if (nState != 5)
                {
                    var fireRot = (NPC.position - NPC.oldPos[0]) == default ? -MathHelper.PiOver2 : (NPC.position - NPC.oldPos[0]).ToRotation().AngleLerp(-MathHelper.PiOver2, 0.25f);
                    spriteBatch.Draw(tex, NPC.Center + new Vector2(6, 28f) - Main.screenPosition, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White with { A = 0 } * alpha, -MathHelper.Pi * 3 / 4 + fireRot, new Vector2(20, 64), new Vector2(0.25f), 0, 0);
                    spriteBatch.Draw(tex, NPC.Center + new Vector2(-6, 28f) - Main.screenPosition, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White with { A = 0 } * alpha, -MathHelper.Pi * 3 / 4 + fireRot, new Vector2(20, 64), new Vector2(0.25f), 0, 0);
                    Main.PlayerRenderer.DrawPlayer(Main.Camera, visualPlayer, NPC.Center - new Vector2(10, 14), 0, default, 0, 1);
                }
                else
                {
                    if (timer < 30 || timer > 630)
                        spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/ballAction_Real"), NPC.Center - Main.screenPosition, new Rectangle((330 - Math.Abs(timer - 330)) / 2 * 90, 0, 90, 86), drawColor, 0, new Vector2(45, 43), 1f + (330 - Math.Abs(timer - 330f)) / 30f, visualPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : 0, 0);
                    else
                    {
                        var rotation = Main.projectile[(int)ai2].oldRot[0];
                        spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusStormBall"), NPC.Center - Main.screenPosition, null, drawColor, rotation, new Vector2(10), 2f, visualPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : 0, 0);
                        spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusStormBall_Glow"), NPC.Center - Main.screenPosition, null, Color.White, rotation, new Vector2(10), 2f, visualPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : 0, 0);
                    }
                }
                if (nState % 6 == 1 || ((nState % 6 == 4) && timer % 240 >= 180))
                {
                    float rotation = 0;
                    if (nState % 6 == 1)
                    {
                        Projectile proj = null;
                        if (timer < 10)
                        {
                            int direct = timer >= 10 ? (timer - 10) / 20 % 2 : 0;
                            rotation = new Vector2(direct == 1 ? 2048 : -2048, ai3).ToRotation();
                        }
                        else if (timer < 30)
                        {
                            proj = Main.projectile[(int)ai6];
                        }
                        else if (timer < 40)
                        {
                            proj = Main.projectile[(int)ai7];
                        }
                        else
                        {
                            proj = Main.projectile[(int)ai8];
                        }
                        if (proj != null && proj.active && proj.type == ModContent.ProjectileType<SolusDash>()) rotation = proj.velocity.ToRotation();
                    }
                    if (nState % 6 == 4)
                    {
                        if (timer % 240 <= 190) rotation = new Vector2(2048 * visualPlayer.direction, ai3).ToRotation();
                        else rotation = Main.projectile[(int)ai6].velocity.ToRotation();
                        alpha = (ai1 % 240 - 180).SymmetricalFactor(30, 10);
                    }
                    spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center + new Vector2(0, 12f) - Main.screenPosition, null, Color.White * alpha, rotation + MathHelper.Pi / 4, new Vector2(12, 66), 1, 0, 0);

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    var fireCen = NPC.Center + new Vector2(0, 12f) - Main.screenPosition;
                    spriteBatch.Draw(tex, fireCen, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White with { A = 0 } * alpha, rotation - MathHelper.Pi * 3 / 4, new Vector2(20, 64), 0.75f, 0, 0);

                    var rot = (float)IllusionBoundModSystem.ModTime / 360f * MathHelper.TwoPi;
                    var sizeOffset = (float)Math.Sin(rot * 4) * 0.05f;
                    alpha *= .25f;
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot, new Vector2(36), new Vector2(1, 4) * (.75f + sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * (.75f + sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot + MathHelper.PiOver4, new Vector2(36), new Vector2(1, 4) * (.375f - sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot + MathHelper.PiOver4 * 3, new Vector2(36), new Vector2(1, 4) * (.375f - sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.White with { A = 0 } * alpha, rot, new Vector2(36), new Vector2(1, 4) * (.375f + sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.White with { A = 0 } * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * (.375f + sizeOffset), 0, 0);

                }
                if (state == AsraNoxState.破晓之光)
                {
                    if (timer < 480)
                    {
                        if (timer < 240)
                        {
                            var rotation = Main.projectile[(int)ai2].oldRot[0];
                            alpha = ai1.SymmetricalFactor(120, 30);
                            spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center - Main.screenPosition + rotation.ToRotationVector2() * 20 * new Vector2(.5f, 1) + new Vector2(0, 12), null, Color.White * alpha, rotation + MathHelper.Pi / 4, new Vector2(12, 66), 1, 0, 0);
                        }
                        else
                        {
                            alpha = (ai1 - 240).SymmetricalFactor(120, 30);
                            spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center - Main.screenPosition + new Vector2(visualPlayer.direction * 6, 16), null, Color.White * alpha, MathHelper.Pi * 3 / 4, new Vector2(12, 66), 1, 0, 0);
                        }
                    }
                }
                if (state == AsraNoxState.破晓之光_追灭 && timer > 60)
                {
                    alpha = ((timer - 60f) % 240).SymmetricalFactor(120, 30);
                    spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center - Main.screenPosition + new Vector2(visualPlayer.direction * 6, 16), null, Color.White * alpha, MathHelper.Pi * 3 / 4, new Vector2(12, 66), 1, 0, 0);
                }
                if (state == AsraNoxState.破晓之光_随机 && timer > 60)
                {
                    var rotation = Main.projectile[(int)ai2].oldRot[0];
                    alpha = ((timer - 60f) % 240).SymmetricalFactor(120, 30);
                    spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center - Main.screenPosition + rotation.ToRotationVector2() * 20 * new Vector2(.5f, 1) + new Vector2(0, 12), null, Color.White * alpha, rotation + MathHelper.Pi / 4, new Vector2(12, 66), 1, 0, 0);
                }
                if (state == AsraNoxState.破晓之光_后撤 && timer < 480)
                {
                    var rotation = Main.projectile[(int)ai2].oldRot[0];
                    alpha = (timer % 240f).SymmetricalFactor(120, 30);
                    spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center - Main.screenPosition + rotation.ToRotationVector2() * 20 * new Vector2(.5f, 1) + new Vector2(0, 12), null, Color.White * alpha, rotation + MathHelper.Pi / 4, new Vector2(12, 66), 1, 0, 0);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            //Main.spriteBatch.DrawString(FontAssets.MouseText.Value, state.ToString(), NPC.Center - Main.screenPosition + new Vector2(0, -50), Color.White);
            Main.spriteBatch.DrawString(FontAssets.MouseText.Value, ai1.ToString(), targetPlayer.Center - Main.screenPosition + new Vector2(0, -50), Color.White);
            //for (int n = 0; n < 16; n++)
            //{
            //    var vec = (MathHelper.Pi / 8 * n).ToRotationVector2() * 256;
            //    Main.spriteBatch.DrawString(FontAssets.MouseText.Value, vec.ToRotation().ToString(), targetPlayer.Center + vec - Main.screenPosition, Color.White);

            //}
            //Main.spriteBatch.DrawString(FontAssets.MouseText.Value, (Music, musics).ToString(), NPC.Center - Main.screenPosition + new Vector2(0, -80), Color.White);

            return false;
        }
    }
    public enum AsraNoxState
    {
        //专家以上难度全程流星雨(不是

        //时符
        开始,//弹幕量逐渐提升，而后本体出现                                  -0:07
        陨日残阳,//无双风神，但是最后三击会预判玩家而后穿刺                  -0:18
        初源日炎,//本体隐去，分身使用日炎风格初源峰巅，最后本体天降正义(不是 -0:31
        星恒飞刃,//妖梦非符式奇偶狙+白莲二符收尾                             -0:43
        日曜星流,//陨日残阳+初源日炎                                         -0:55
        太阳风暴,//式神「蓝」(不是                                           -1:06
        破晓之光,//莱瓦汀+杀意的百合                                         -1:19

        //追灭状态下全程强风场，随着不同攻击模式改变风场，半时符
        陨日残阳_追灭,//无双风神，但是全程预判冲刺
        初源日炎_追灭,//半定向冲刺
        星恒飞刃_追灭,//妖梦非符式奇偶狙
        日曜星流_追灭,//陨日残阳+初源日炎
        太阳风暴_追灭,//式神「蓝」
        破晓之光_追灭,//莱瓦汀，但是全程平移

        //70%血量进入，如果未低于70%则停留在追灭
        //风场停止，参考开始状态，弹幕难度提升，时间持久，随机性增加
        陨日残阳_随机,//无双风神，但是最后五击或者中途随机会预判玩家而后穿刺
        初源日炎_随机,//本体隐去，分身使用日炎风格初源峰巅，范围覆盖全屏，天降正义后生成一堆
        星恒飞刃_随机,//妖梦非符式奇偶狙+白莲二符
        日曜星流_随机,//陨日残阳+初源日炎
        太阳风暴_随机,//式神「蓝」，但是频率更高
        破晓之光_随机,//莱瓦汀,但是全程挥动+自机狙


        //最后30%血量  bgm先暂停后重新开始
        //风场重开且全程固定方向，世界右半部开始则向左......                        阶段血量相关
        陨日残阳_后撤,//无双风神，但是是闪飞然后水平向弹幕                          30-25
        初源日炎_后撤,//本体隐去，分身使用日炎风格初源峰巅，但是是纵向弹幕干扰移动  25-20
        星恒飞刃_后撤,//妖梦非符式奇偶狙，但是增加散狙                              20-15
        日曜星流_后撤,//闪飞后 水平干扰纵向干扰兼具                                 15-10
        太阳风暴_后撤,//上下版边冲撞以发出大量滞留弹幕                              10-05
        破晓之光_后撤,//参考月总大激光(x                                            05-00
    }
    //TODO 阿斯拉诺克斯
    //8.22 完成大致结构设计
    //8.23 计划完成各招数大致设计
    //8.24 计划完成初始阶段
    //8.25 计划完成追灭阶段
    //8.26 计划完成随机阶段
    //8.27 计划完成后撤阶段
    //8.28 最后的优化修改
    public class SolusDash : ModProjectile
    {
        Projectile projectile => Projectile;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            return projHitbox.Intersects(targetHitbox) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity.SafeNormalize(default) * 70, 10, ref point);

        }
        public override string Texture => base.Texture.Replace("Dash", "KatanaFractal");
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.hide = true;
            projectile.DamageType = DamageClass.Melee;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 80;
            ProjectileID.Sets.DrawScreenCheckFluff[projectile.type] = 3000;
        }
        public override bool ShouldUpdatePosition() => false;
        public override bool PreDraw(ref Color lightColor)
        {
            //bool flag = false;
            const float length = 2203f;
            if (projectile.timeLeft > 50)
            {
                Main.spriteBatch.DrawEffectLine(projectile.Center, projectile.velocity.SafeNormalize(default), Color.Orange * (80f - projectile.timeLeft).HillFactor2(30), 1, 0, length, 30, 1);
            }
            else
            {
                Effect effect = IllusionBoundMod.GetEffect("Effects/EightTrigramsFurnaceEffect");
                //List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
                CustomVertexInfo[] bars1 = new CustomVertexInfo[5];

                var unit = -projectile.velocity.SafeNormalize(default);
                //if (unit == default) Main.NewText("DEF辣你个大傻瓜");
                //Main.NewText("我在绘制啊");
                //flag = true;
                var width = MathHelper.Clamp(projectile.timeLeft / 30f, 0, 1) * 96;
                var wf = 0.25f;
                Vector2 unit2 = new Vector2(-unit.Y, unit.X);
                var start = projectile.Center;
                (float x1, float y1, float x2, float y2) = (0, 0, 1, 1);
                bars1[0] = new CustomVertexInfo(start + unit2 * width, new Vector3(x1, y1, 4));
                bars1[1] = new CustomVertexInfo(start - unit2 * width, new Vector3(x1, y2, 4));
                bars1[2] = new CustomVertexInfo(start + length * unit + unit2 * width * wf, new Vector3(x2, y1, 0));
                bars1[3] = new CustomVertexInfo(start + length * unit - unit2 * width * wf, new Vector3(x2, y2, 0));
                bars1[4] = new CustomVertexInfo(start + length * unit, new Vector3(x2, (y2 + y1) * .5f, 0));


                //List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
                if (bars1.Length > 2)
                {
                    //for (int i = 0; i < bars1.Count - 2; i += 2)
                    //{
                    //    triangleList1.Add(bars1[i]);
                    //    triangleList1.Add(bars1[i + 2]);
                    //    triangleList1.Add(bars1[i + 1]);
                    //    triangleList1.Add(bars1[i + 1]);
                    //    triangleList1.Add(bars1[i + 2]);
                    //    triangleList1.Add(bars1[i + 3]);
                    //}
                    //for (int n = 0; n < 6; n++)
                    //    Main.spriteBatch.DrawLine(triangleList1[n].Position, triangleList1[(n + 1) % 6].Position, Main.hslToRgb(new Vector3(n / 6f, 1, 0.5f)), 4, false, -Main.screenPosition);
                    CustomVertexInfo[] bars2 = new CustomVertexInfo[9];
                    bars2[0] = bars1[0];
                    bars2[1] = bars1[2];
                    bars2[2] = bars1[4];
                    bars2[3] = bars1[0];
                    bars2[4] = bars1[1];
                    bars2[5] = bars1[4];
                    bars2[6] = bars1[1];
                    bars2[7] = bars1[3];
                    bars2[8] = bars1[4];

                    RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                    Main.graphics.GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };
                    var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                    effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                    effect.Parameters["maxFactor"].SetValue(0f);
                    effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
                    Main.graphics.GraphicsDevice.BlendState = BlendState.Additive;
                    Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
                    Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[10];
                    Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.HeatMap[24];
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                    effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect_ColorBar"].Apply();
                    //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, bars2, 0, 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                }
            }
            //Main.spriteBatch.DrawLine(projectile.Center + projectile.velocity * .5f - projectile.velocity.SafeNormalize(default) * 16, projectile.velocity.SafeNormalize(default) * 16, flag ? Color.Cyan : Color.Purple, 16, true, -Main.screenPosition);
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }
    }
    public class SolusKatanaFractal : ModProjectile
    {
        Projectile projectile => Projectile;
        public Player drawPlayer;
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.DamageType = DamageClass.Melee;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.manualDirectionChange = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 114514;
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 60;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            if (drawPlayer != null)
            {
                return projHitbox.Intersects(targetHitbox) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), drawPlayer.Center, drawPlayer.Center + projectile.oldRot[0].ToRotationVector2() * 70, 10, ref point);
            }
            return projHitbox.Intersects(targetHitbox) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.oldPos[0], projectile.oldPos[0] + projectile.oldRot[0].ToRotationVector2() * 70, 10, ref point);
        }
        public float BeginAlpha
        {
            get
            {
                GetInfos(out float _, out float timeToLate, out float _);
                timeToLate = (int)projectile.ai[1] == 1 ? 60 : timeToLate;
                return projectile.localAI[0] >= timeToLate ? 1 : projectile.localAI[0] / timeToLate;
            }
        }

        public void GetInfos(out float timeToFly, out float timeToLate, out float rotator)
        {
            switch ((int)projectile.ai[1])
            {
                case 0:
                default:
                    timeToFly = timeToLate = 120;
                    rotator = projectile.ai[0];
                    break;
                case 1:
                    timeToFly = 20;
                    timeToLate = 0;
                    rotator = 0;
                    break;
                case 2:
                    timeToFly = 60;
                    timeToLate = 0;
                    rotator = 0;
                    break;
                case 3:
                    timeToFly = 30;
                    timeToLate = 0;
                    rotator = 0;
                    break;
                case 4:
                    timeToFly = 120;
                    timeToLate = 60;
                    rotator = 0;
                    break;
            }
        }
        public override void AI()
        {
            GetInfos(out float timeToFly, out float timeToLate, out float rotator);
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= timeToLate)
            {
                float offset = projectile.localAI[0] - timeToLate;
                if (offset >= timeToFly)
                {
                    //projectile.Kill();
                    //return;
                    projectile.position -= projectile.velocity;
                    projectile.Opacity = 0;
                    if (offset >= timeToFly + 60)
                    {
                        projectile.Kill();
                        return;
                    }
                }
                else
                {
                    projectile.velocity = projectile.velocity.RotatedBy(rotator);
                    projectile.Opacity = (int)projectile.ai[1] == 4 ? MathHelper.Clamp(projectile.localAI[0].SymmetricalFactor(90, 30), 0, 1) : Terraria.Utils.GetLerpValue(0f, 12f, offset, true) * Terraria.Utils.GetLerpValue(timeToFly, timeToFly - 12f, offset, true);
                    projectile.direction = (projectile.velocity.X > 0f) ? 1 : -1;
                    projectile.spriteDirection = projectile.direction;
                    projectile.rotation = 0.7853982f * projectile.spriteDirection + projectile.velocity.ToRotation();
                    if (projectile.spriteDirection == -1)
                    {
                        projectile.rotation += 3.14159274f;
                    }
                    if (offset > 7f)
                    {
                        if (Main.rand.NextBool(15))
                        {
                            Dust dust = Dust.NewDustPerfect(projectile.Center, MyDustId.CyanBubble, null, 100, Color.Lerp(Main.hslToRgb(0.1f, 1f, 0.5f), Color.White, Main.rand.NextFloat() * 0.3f), 1f);
                            dust.scale = 0.7f;
                            dust.noGravity = true;
                            dust.velocity *= 0.5f;
                            dust.velocity += projectile.velocity * 2f;
                        }
                    }
                }

                if ((int)projectile.ai[1] != 2 && (int)projectile.ai[1] != 3 && (int)projectile.ai[1] != 4)
                {
                    for (int n = 59; n > 0; n--)
                    {
                        projectile.oldPos[n] = projectile.oldPos[n - 1];
                        projectile.oldRot[n] = projectile.oldRot[n - 1];
                    }
                    projectile.oldPos[0] = projectile.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                    if ((int)projectile.ai[1] == 1) rotator = MathHelper.Pi / 192;
                    projectile.oldRot[0] = projectile.velocity.ToRotation() + rotator * (offset / timeToFly).Lerp(-180, 90, true);
                }
                else
                    foreach (var npc in Main.npc)
                    {
                        if (
                            npc.active && npc.type == ModContent.NPCType<AsraNox>() &&
                            npc.ai[0] != 2 &&
                            (npc.ai[0] != 3 || (npc.ai[1] < 540 && (int)projectile.ai[1] == 4) || (npc.ai[1] <= 60 && (int)projectile.ai[1] == 2)) &&
                            (npc.ai[0] != 4 || npc.ai[1] % 240 >= 180) &&
                            npc.ai[0] != 5 &&
                            npc.ai[0] != 6 &&

                            npc.ai[0] != 8 &&
                            npc.ai[0] != 9 &&
                            (npc.ai[0] != 10 || npc.ai[1] % 240 >= 180) &&
                            npc.ai[0] != 11 &&

                            npc.ai[0] != 14 &&
                            npc.ai[0] != 15 &&
                            (npc.ai[0] != 16 || npc.ai[1] % 240 >= 180) &&
                            npc.ai[0] != 17 &&

                            npc.ai[0] != 20 &&
                            npc.ai[0] != 21 &&
                            (npc.ai[0] != 22 || npc.ai[1] % 240 >= 180) &&
                            npc.ai[0] != 23
                            )
                        {

                            for (int n = 59; n > 3; n--)
                            {
                                projectile.oldPos[n] = projectile.oldPos[n - 4];
                                projectile.oldRot[n] = projectile.oldRot[n - 4];
                            }
                            for (int n = 1; n < 4; n++)
                            {
                                projectile.oldPos[n] = projectile.oldPos[0];
                                projectile.oldRot[n] = projectile.oldRot[0];
                            }
                            //Main.NewText("!!");
                            break;
                        }
                    }
            }
            else
            {
                projectile.position -= projectile.velocity;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //Main.spriteBatch.DrawLine(projectile.Center, Main.LocalPlayer.Center, Color.Orange * .5f, 8, false, -Main.screenPosition);
            return false;
        }

        public void DrawOthers()
        {
            //if (drawPlayer == null) drawPlayer = new Player();
            Player player = drawPlayer;
            if (player == null) { }
            Vector2 velocity = projectile.velocity;
            Vector2 position = projectile.Center;
            //Main.NewText(projectile.oldPos.Length);
            GetInfos(out float timeToFly, out float timeToLate, out float rotator);
            bool flag = (int)projectile.ai[1] == 1;
            timeToLate = flag ? 60 : timeToLate;
            timeToLate = (int)projectile.ai[1] == 4 ? 0 : timeToLate;
            if (projectile.localAI[0] < timeToLate)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                var alpha = BeginAlpha.HillFactor2();
                if (flag) timeToFly = 10;
                for (int n = 0; n < (timeToFly + 30); n++)
                {
                    var fac = 1 - n / (timeToFly + 30f);
                    Main.spriteBatch.DrawLine(position, velocity, Color.White * alpha * fac, 4 * fac, true, -Main.screenPosition);
                    Main.spriteBatch.DrawLine(position, velocity, Color.Orange * alpha * fac, 8 * fac, true, -Main.screenPosition);
                    position += velocity;
                    velocity = velocity.RotatedBy(rotator);// * (Main.rand.NextFloat(-15f, 15f))
                }
                var rot = (float)IllusionBoundModSystem.ModTime / 360f * MathHelper.TwoPi;
                var sizeOffset = (float)Math.Sin(rot * 4) * 0.05f;
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot, new Vector2(36), new Vector2(1, 4) * (.75f + sizeOffset), 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * (.75f + sizeOffset), 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot + MathHelper.PiOver4, new Vector2(36), new Vector2(1, 4) * (.375f - sizeOffset), 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot + MathHelper.PiOver4 * 3, new Vector2(36), new Vector2(1, 4) * (.375f - sizeOffset), 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.White * alpha, rot, new Vector2(36), new Vector2(1, 4) * (.375f + sizeOffset), 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.White * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * (.375f + sizeOffset), 0, 0);
                Main.spriteBatch.End();
            }
            if (player != null)
            {
                if (flag || (int)projectile.ai[1] == 2 || (int)projectile.ai[1] == 3)//|| (int)projectile.ai[1] == 4
                {
                    player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.oldRot[0] - MathHelper.PiOver2);
                }
                else
                {
                    player.isFirstFractalAfterImage = true;
                    player.firstFractalAfterImageOpacity = projectile.Opacity;
                    player.ResetEffects();
                    player.ResetVisibleAccessories();
                    player.UpdateDyes();
                    player.DisplayDollUpdate();
                    player.UpdateSocialShadow();
                    player.itemAnimationMax = 60;
                    player.itemAnimation = (int)projectile.localAI[0];
                    player.itemRotation = projectile.velocity.ToRotation();
                    player.Center = projectile.oldPos[0];
                    if ((int)projectile.ai[1] != 4)
                    {
                        player.direction = (projectile.velocity.X > 0f) ? 1 : (-1);
                        player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * (float)player.direction, projectile.velocity.X * (float)player.direction);
                    }
                    player.velocity.Y = 0.01f;
                    player.wingFrame = 2;
                    player.PlayerFrame();
                    player.socialIgnoreLight = true;
                    player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.oldRot[0] - MathHelper.PiOver2);
                    try
                    {
                        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                        Main.PlayerRenderer.DrawPlayer(Main.Camera, player, player.position, 0f, player.fullRotationOrigin, 0.5f);
                        Main.spriteBatch.End();

                    }
                    catch
                    {
                    }
                }

            }
        }
        public void DrawSword()
        {
            SpriteEffects spriteEffects = projectile.ai[0] > 0 ? 0 : SpriteEffects.FlipHorizontally;
            Texture2D texture2D4 = TextureAssets.Projectile[projectile.type].Value;
            var color84 = Color.White * projectile.Opacity * 0.9f;
            color84.A /= 2;
            var origin = texture2D4.Size();
            origin *= spriteEffects == 0 ? new Vector2(0.1f, 0.9f) : new Vector2(0.9f, 0.9f);
            var rot = projectile.oldRot[0] + MathHelper.PiOver4;
            rot += projectile.ai[0] <= 0 ? MathHelper.Pi / 2 : 0;
            Main.spriteBatch.Draw(texture2D4, projectile.oldPos[0] - Main.screenPosition, null, color84, rot, origin, 1, spriteEffects, 0);
        }
    }
    public class SolusLevatine : ModProjectile
    {
        public override string Texture => base.Texture.Replace("Levatine", "KatanaFractal");

        public Projectile projectile => Projectile;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.timeLeft > 300)
            {
                return false;
            }
            float point = 0f;
            Vector2 u = projectile.ai[0].ToRotationVector2();
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + u * 16f, u * 3200f + projectile.Center, 120 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.timeLeft / 300f)), ref point);
        }

        private VertexTriangle3List loti;
        private Vector3 GetVec(Vector3 v, Vector3 size, float r) => (size * v).ApplyMatrix(projectile.ai[0].Create3DRotation(DirOf3DRotation.z_Axis_P) * (r * (float)IllusionBoundMod.ModTime / 300f * MathHelper.TwoPi).Create3DRotation(DirOf3DRotation.x_Axis_P));//Main.time
        public void UpdateTris(float factor)
        {
            var size = new Vector3(32, 96 * factor, 96 * factor);
            loti.offset = projectile.Center;
            if (loti.tris == null)
            {
                NewTris(2000);
            }
            var vel = 1;
            for (int n = 0; n < 2; n++)
            {
                loti.tris[2 * n].positions[0] = GetVec(new Vector3(1, 1, -1), size, vel);
                loti.tris[2 * n + 1].positions[0] = GetVec(new Vector3(1, -1, 1), size, vel);
                loti.tris[2 * n].positions[1] = loti.tris[2 * n + 1].positions[2] = GetVec(new Vector3(1, 1, 1), size, vel);
                loti.tris[2 * n].positions[2] = loti.tris[2 * n + 1].positions[1] = GetVec(new Vector3(1, -1, -1), size, vel);
                size *= new Vector3(2f, 1.5f, 1.5f);
                vel -= 3;
            }
            for (int n = 0; n < 4; n++)
            {
                for (int i = 0; i < 3; i++)
                {
                    loti.tris[n].vertexs[i].Z = factor;
                }
            }
        }
        public void NewTris(float height)
        {
            VertexTriangle3[] tris = new VertexTriangle3[4];
            for (int n = 0; n < 4; n++)
            {
                tris[n] = new VertexTriangle3(default, default, default, default, default, default);
            }
            for (int n = 0; n < 2; n++)
            {
                tris[2 * n].vertexs[0] = new Vector3(0, 0, 1);
                tris[2 * n + 1].vertexs[0] = new Vector3(1, 1, 1);
                tris[2 * n].vertexs[1] = tris[2 * n + 1].vertexs[2] = new Vector3(0, 1, 1);
                tris[2 * n].vertexs[2] = tris[2 * n + 1].vertexs[1] = new Vector3(1, 0, 1);
            }
            for (int n = 0; n < 4; n++)
            {
                for (int i = 0; i < 3; i++)
                {
                    tris[n].colors[i] = new Color(240, 139, 78);//Main.hslToRgb(0.8f, 0.75f, 0.75f)
                }
            }
            loti.tris = tris;
            loti.height = height;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (projectile.timeLeft > 195)
            {
                var timer = (240 - projectile.timeLeft) % 15 / 15f;
                //spriteBatch.Draw(IllusionBoundMod.GetTexture(BigApe.BigApeTools.ApePath + "StrawBerryArea"), projectile.Center + u * 12 - Main.screenPosition, null, Color.Lerp(Color.Orange, Color.White, timer) with { A = 0 } * timer.HillFactor2(1), 0, new Vector2(99), projectile.timeLeft < 210 ? MathHelper.Lerp(0, 24, timer * timer) : MathHelper.Lerp(16, 0, (float)Math.Sqrt(timer)), 0, 0);
                var u = projectile.ai[0].ToRotationVector2();
                Texture2D tex = IllusionBoundMod.GetTexture("Terraria/Images/Misc/Perlin", false);
                int v = 600;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Terraria.DataStructures.DrawData data = new Terraria.DataStructures.DrawData(tex, projectile.Center + u * 12 - Main.screenPosition, new Rectangle(0, 0, 2 * v, 2 * v), Color.Lerp(Color.Orange, Color.White, timer) with { A = 0 }, 0, new Vector2(v), (projectile.timeLeft < 210 ? MathHelper.Lerp(0, 4, timer * timer) : MathHelper.Lerp(2, 0, (float)Math.Sqrt(timer))) * new Vector2(1.5f, 1), 0, 0);
                Terraria.Graphics.Shaders.GameShaders.Misc["ForceField"].UseColor(new Vector3(timer.HillFactor2(1)));//
                Terraria.Graphics.Shaders.GameShaders.Misc["ForceField"].Apply(data);
                data.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return false;
        }
        public void DrawLaser()
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            var factor = projectile.timeLeft <= 210 ? (210f - projectile.timeLeft).HillFactor2(210) : MathHelper.Clamp(240 - projectile.timeLeft, 0, 30) / 30f * 0.5f;
            var width = projectile.timeLeft <= 210 ? 800f * factor : 16f;//4.47213f * 16 * 4 * factor * 4 + 16f//1600f * factor
            var u = projectile.ai[0].ToRotationVector2();
            //spriteBatch.DrawQuadraticLaser_PassNormal(projectile.Center + u * 12, u, new Color(240, 139, 78), 1600 * MathHelper.Clamp(240 - projectile.timeLeft, 0, 30) / 30f, width, styleIndex: 10);// * factor

            spriteBatch.DrawQuadraticLaser_PassColorBar(projectile.Center + u * 12, u, 15, 1600 * MathHelper.Clamp(240 - projectile.timeLeft, 0, 30) / 30f, width, styleIndex: 10);
            UpdateTris(factor);
            spriteBatch.Draw3DPlane(IllusionBoundMod.GetEffect("Effects/ShaderSwooshEffect"), IllusionBoundMod.GetTexture(BigApe.BigApeTools.ApePath + "StrawBerryArea"), IllusionBoundMod.AniTexes[6], loti);
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("日炎「破晓之光」");
            ProjectileID.Sets.TrailCacheLength[Type] = 60;
        }
        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.aiStyle = -1;
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 240;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 210)
            {
                SoundEngine.PlaySound(SoundID.Zombie104, projectile.Center);
            }
        }
    }
    public class SolusUltraLaser : ModProjectile
    {
        public override string Texture => base.Texture.Replace("UltraLaser", "KatanaFractal");

        Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.scale = 1f;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Melee;
            projectile.ignoreWater = true;
            projectile.alpha = 127;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.light = 0.5f;
            projectile.aiStyle = -1;
        }
        private float Time
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        private Vector2[] posG = new Vector2[120];
        private Vector2[] posP = new Vector2[120];
        private Vector2 GetVec(Vector2 vec)
        {
            switch ((int)projectile.ai[1] % 2)
            {
                case 0:
                    vec.X *= -1;
                    return vec;
                case 1:
                    vec.Y *= -1;
                    return vec;
            }
            return vec;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Effect effect = IllusionBoundMod.ShaderSwoosh;
            if (effect == null)
            {
                return false;
            }
            SpriteBatch spriteBatch = Main.spriteBatch;
            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> bars4 = new List<CustomVertexInfo>();
            int sint = IllusionBoundExtensionMethods.ValueRange((int)Time * 4 - 120, 1, 120);
            int eint = IllusionBoundExtensionMethods.ValueRange((int)Time * 4, 1, 120);
            var _orange = Color.Lerp(Color.Orange, Color.OrangeRed, .5f);
            for (int i = sint; i < eint; ++i)
            {
                var factor = ((float)i - sint) / ((float)eint - sint);
                var normalDir1 = posG[i - 1] - posG[i];
                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
                var normalDir2 = GetVec(posG[i - 1]) - GetVec(posG[i]);
                normalDir2 = Vector2.Normalize(new Vector2(-normalDir2.Y, normalDir2.X));
                var normalDir3 = posP[i - 1] - posP[i];
                normalDir3 = Vector2.Normalize(new Vector2(-normalDir3.Y, normalDir3.X));
                var normalDir4 = GetVec(posP[i - 1]) - GetVec(posP[i]);
                normalDir4 = Vector2.Normalize(new Vector2(-normalDir4.Y, normalDir4.X));
                //var color = Color.Lerp(Color.White, Color.Red, factor);
                //var w = MathHelper.Lerp(1f, 0.05f, factor);
                float w = (float)(1 - Math.Pow(2 * factor - 1, 4));
                bars1.Add(new CustomVertexInfo(posG[i] + projectile.Center + normalDir1 * 32, Color.Orange, new Vector3(factor, 1, w)));
                bars1.Add(new CustomVertexInfo(posG[i] + projectile.Center + normalDir1 * -32, Color.Orange, new Vector3(factor, 0, w)));
                bars2.Add(new CustomVertexInfo(GetVec(posG[i]) + projectile.Center + normalDir2 * 32, Color.Orange, new Vector3(factor, 1, w)));
                bars2.Add(new CustomVertexInfo(GetVec(posG[i]) + projectile.Center + normalDir2 * -32, Color.Orange, new Vector3(factor, 0, w)));
                bars3.Add(new CustomVertexInfo(posP[i] + projectile.Center + normalDir3 * 32, _orange, new Vector3(factor, 1, w)));
                bars3.Add(new CustomVertexInfo(posP[i] + projectile.Center + normalDir3 * -32, _orange, new Vector3(factor, 0, w)));
                bars4.Add(new CustomVertexInfo(GetVec(posP[i]) + projectile.Center + normalDir4 * 32, _orange, new Vector3(factor, 1, w)));
                bars4.Add(new CustomVertexInfo(GetVec(posP[i]) + projectile.Center + normalDir4 * -32, _orange, new Vector3(factor, 0, w)));
            }
            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
            if (bars1.Count > 2)
            {
                for (int i = 0; i < bars1.Count - 2; i += 2)
                {
                    triangleList1.Add(bars1[i]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 3]);
                }
                for (int i = 0; i < bars2.Count - 2; i += 2)
                {
                    triangleList1.Add(bars2[i]);
                    triangleList1.Add(bars2[i + 2]);
                    triangleList1.Add(bars2[i + 1]);
                    triangleList1.Add(bars2[i + 1]);
                    triangleList1.Add(bars2[i + 2]);
                    triangleList1.Add(bars2[i + 3]);
                }
                for (int i = 0; i < bars3.Count - 2; i += 2)
                {
                    triangleList1.Add(bars3[i]);
                    triangleList1.Add(bars3[i + 2]);
                    triangleList1.Add(bars3[i + 1]);
                    triangleList1.Add(bars3[i + 1]);
                    triangleList1.Add(bars3[i + 2]);
                    triangleList1.Add(bars3[i + 3]);
                }
                for (int i = 0; i < bars4.Count - 2; i += 2)
                {
                    triangleList1.Add(bars4[i]);
                    triangleList1.Add(bars4[i + 2]);
                    triangleList1.Add(bars4[i + 1]);
                    triangleList1.Add(bars4[i + 1]);
                    triangleList1.Add(bars4[i + 2]);
                    triangleList1.Add(bars4[i + 3]);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                effect.Parameters["uTime"].SetValue(-(float)IllusionBoundModSystem.ModTime * 0.06f);
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[1];
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
                effect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                var factor = (float)Math.Pow(MathHelper.Clamp((60 - projectile.timeLeft) / 30f, 0, 1), 2);

                Main.spriteBatch.DrawQuadraticLaser_PassColorBar(projectile.Center, ((projectile.ai[1] - 1) * MathHelper.PiOver2).ToRotationVector2(), 15, 2400 * factor, 128 * (60f - projectile.timeLeft).HillFactor2(60), texcoord: (0, 0, factor, 1));


                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.immuneTime = 10;
            target.immune = true;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = 0; i < 30; i++)
            {
                var n = i * 4;
                Vector2 vec1 = GetVec(posG[n]);
                Vector2 vec2 = GetVec(posP[n]);
                if (new Rectangle((int)(posG[n].X - 4 + projectile.Center.X), (int)(posG[n].Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox)) return true;
                if (new Rectangle((int)(vec1.X - 4 + projectile.Center.X), (int)(vec1.Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox)) return true;
                if (new Rectangle((int)(posP[n].X - 4 + projectile.Center.X), (int)(posP[n].Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox)) return true;
                if (new Rectangle((int)(vec2.X - 4 + projectile.Center.X), (int)(vec2.Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox)) return true;
            }
            if (Time < 180)
            {
                Rectangle rectangle;
                int width = (int)(70 * Math.Sin(MathHelper.Pi * Math.Sqrt(Time / 180)));
                if ((int)projectile.ai[1] % 2 == 0)
                {
                    rectangle = new Rectangle((int)projectile.Center.X - width / 2, (int)projectile.Center.Y - ((int)projectile.ai[1] == 0 ? 4800 : 0), width, 4800);
                }
                else
                {
                    rectangle = new Rectangle((int)projectile.Center.X - ((int)projectile.ai[1] == 3 ? 4800 : 0), (int)projectile.Center.Y - width / 2, 4800, width);
                }
                if (rectangle.Intersects(targetHitbox)) return true;
            }
            return false;
        }
        public override void AI()
        {
            Time++;
            for (int n = 119; n > 3; n--)
            {
                posG[n] = posG[n - 4];
                posP[n] = posP[n - 4];
            }
            if (projectile.timeLeft % 12 == 0)
            {
                var unit = ((projectile.ai[1] - 1) * MathHelper.PiOver2).ToRotationVector2();
                var normal = new Vector2(-unit.Y, unit.X);
                //for (int n = 0; n < 4; n++)
                //{
                //    if (Main.rand.NextBool(2))
                //    {
                //        normal *= Main.rand.Next(new int[] { -1, 1 });
                //        var proj1 = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center + unit * Main.rand.NextFloat(0, 1200), normal * 8f, ModContent.ProjectileType<SolusEnergyShard>(), 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                //        proj1.friendly = false;
                //        proj1.hostile = true;
                //    }
                //}
                normal *= Main.rand.Next(new int[] { -1, 1 });
                var proj1 = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center + unit * Main.rand.NextFloat(0, 1200), normal * 8f, ModContent.ProjectileType<SolusEnergyShard>(), 45, 4, Main.myPlayer, Main.rand.Next(new int[] { 4, 5, 6, 6 }), 1.05f);
                proj1.friendly = false;
                proj1.hostile = true;
            }
            if (projectile.timeLeft > 30)
            {
                for (int n = 0; n < 4; n++)
                {
                    float x = (Time - .25f * n).ValueRange(0, 30) * 6;
                    posG[n] = new Vector2(4 * (float)Math.Sin(Math.Sqrt(x + 10)), -x).RotatedBy(projectile.ai[1] * MathHelper.PiOver2) * 20f;
                    posP[n] = new Vector2(-8 * ((float)Math.Sin(0.05f * x) * 60 / x + (float)Math.Sqrt(0.05d * x) * 2 - 3), -x).RotatedBy(projectile.ai[1] * MathHelper.PiOver2) * 20f;
                }
            }
            else
                for (int n = 1; n < 4; n++)
                {
                    posG[n] = posG[0];
                    posP[n] = posP[0];
                }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("日炎「杀意的百合」");
        }
    }
}
