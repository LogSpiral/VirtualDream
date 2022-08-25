using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
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
                Music = (byte)state < 7 ? musics.Item1 : musics.Item2;
            }
            visualPlayer.velocity = NPC.velocity;

            //Music = MusicID.Boss2;
            switch (state)
            {
                case AsraNoxState.开始:
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
                case AsraNoxState.陨日残阳:
                    {
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

                        var timer = (int)ai1;
                        int direct = timer >= 10 ? (timer - 10) / 20 % 2 : 0;

                        if (timer % 20 == 10)
                        {
                            if (timer <= 610)
                            {
                                ai2 = Main.rand.Next(-480, 480);
                                ai3 = ai1 >= 550 ? (-2 * ai2) : (Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 }));
                                ai4 = targetPlayer.Center.X;
                                ai5 = targetPlayer.Center.Y;
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

                        NPC.oldPos[0] = NPC.oldPosition;
                        //visualPlayer.itemRotation = 
                        break;
                    }
                case AsraNoxState.初源日炎:
                    {
                        if ((int)ai1 < 765)
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.25f);
                        else
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(ai5, ai6 + 400 - (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 2 / 15f);
                        NPC.damage = 0;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if ((int)ai1 % 10 == 0)
                        {
                            var value5 = targetPlayer.Center + targetPlayer.velocity * 20f + ((int)ai1 % 60 < ai1 / 13f ? default : Main.rand.NextVector2Unit() * Main.rand.NextFloat(Main.rand.NextFloat(0, 960), 960));

                            // Vector2 vector32 = value5 - NPC.Center;
                            Vector2 vector33 = Main.rand.NextVector2CircularEdge(1f, 1f);
                            float num78 = 1f;
                            int num79 = 1;
                            for (int num80 = 0; num80 < num79; num80++)
                            {
                                value5 += Main.rand.NextVector2Circular(24f, 24f);
                                //if (vector32.Length() > 700f)
                                //{
                                //    vector32 *= 700f / vector32.Length();
                                //    value5 = NPC.Center + vector32;
                                //}
                                float num81 = Terraria.Utils.GetLerpValue(0f, 6f, NPC.velocity.Length(), true) * 0.8f;
                                vector33 *= 1f - num81;
                                //vector33 += player.velocity * num81;
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
                                Vector2 position1 = value5 + value6;
                                var proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position1, vector34, ModContent.ProjectileType<SolusKatanaFractal>(), 45, 4, Main.myPlayer, num83);
                                if (proj.ModProjectile is SolusKatanaFractal solusKatanaFractal)
                                {
                                    solusKatanaFractal.drawPlayer = new Player();
                                    solusKatanaFractal.drawPlayer.CopyVisuals(visualPlayer);
                                }
                            }
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
                case AsraNoxState.日曜星流:
                    {
                        //240一次，三个阶段
                        //一次重劈(60帧)，一次下刺(120帧)，一次冲锋(20帧)
                        //流星雨，随着阶段而加强
                        //初源量减少
                        //冲刺生成的弹幕有所改变
                        int timer = (int)ai1;
                        int counter = timer % 240;
                        int stager = timer / 240;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        if (counter < 60)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X, targetPlayer.Center.Y - 400 + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.125f);
                        }
                        else if (counter < 180)
                        {

                        }
                        else 
                        {

                        }

                        ai1++;
                        if (ai1 >= 720)
                        {
                            SetAI(4);
                            break;
                        }

                        break;
                    }
                case AsraNoxState.星恒飞刃:
                    {
                        int timer = (int)ai1;
                        int counter = timer % 240;
                        int stager = timer / 240;
                        visualPlayer.direction = Math.Sign(targetPlayer.Center.X - NPC.Center.X);
                        NPC.Center = Vector2.Lerp(NPC.Center, new Vector2(targetPlayer.Center.X - 800 * visualPlayer.direction, targetPlayer.Center.Y - 400 * (float)Math.Sin(ai1 / 240 * MathHelper.TwoPi) + (float)Math.Sin(IllusionBoundModSystem.ModTime2 / 180f * MathHelper.TwoPi) * 32), 0.05f);//
                        if (counter < 180)
                        {
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
                        ai1++;
                        if (ai1 >= 720)
                        {
                            SetAI(4);

                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;
                        break;
                    }
                case AsraNoxState.太阳风暴:
                    {
                        ai1++;
                        if (ai1 >= 660)
                        {
                            SetAI(6);

                            break;
                        }

                        break;
                    }
                case AsraNoxState.破晓之光:
                    {
                        ai1++;
                        if (ai1 >= 780)
                        {
                            SetAI(7);

                            break;
                        }

                        break;
                    }
            }
        }
        public Player visualPlayer;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (state != 0)
            {
                var tex = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Meteor").Value;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                var fireRot = (NPC.position - NPC.oldPos[0]) == default ? -MathHelper.PiOver2 : (NPC.position - NPC.oldPos[0]).ToRotation().AngleLerp(-MathHelper.PiOver2, 0.25f);
                spriteBatch.Draw(tex, NPC.Center + new Vector2(6, 28f) - Main.screenPosition, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White, -MathHelper.Pi * 3 / 4 + fireRot, new Vector2(20, 64), new Vector2(0.25f), 0, 0);
                spriteBatch.Draw(tex, NPC.Center + new Vector2(-6, 28f) - Main.screenPosition, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White, -MathHelper.Pi * 3 / 4 + fireRot, new Vector2(20, 64), new Vector2(0.25f), 0, 0);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, visualPlayer, NPC.Center - new Vector2(10, 14), 0, default, 0, 1);
                if (state == AsraNoxState.陨日残阳)
                {
                    //var timer = (int)ai1;
                    float rotation = 0;
                    var proj = Main.projectile[(int)ai8];
                    if (proj.active && proj.type == ModContent.ProjectileType<SolusDash>()) rotation = proj.velocity.ToRotation();
                    spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center + new Vector2(0, 12f) - Main.screenPosition, null, Color.White, rotation + MathHelper.Pi / 4, new Vector2(12, 66), 1, 0, 0);

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    var fireCen = NPC.Center + new Vector2(0, 12f) - Main.screenPosition;
                    spriteBatch.Draw(tex, fireCen, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White with { A = 0 }, rotation - MathHelper.Pi * 3 / 4, new Vector2(20, 64), 0.75f, 0, 0);

                    var rot = (float)IllusionBoundModSystem.ModTime / 360f * MathHelper.TwoPi;
                    var sizeOffset = (float)Math.Sin(rot * 4) * 0.05f;
                    var alpha = .25f;
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot, new Vector2(36), new Vector2(1, 4) * (.75f + sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * (.75f + sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot + MathHelper.PiOver4, new Vector2(36), new Vector2(1, 4) * (.375f - sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.Orange with { A = 0 } * alpha, rot + MathHelper.PiOver4 * 3, new Vector2(36), new Vector2(1, 4) * (.375f - sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.White with { A = 0 } * alpha, rot, new Vector2(36), new Vector2(1, 4) * (.375f + sizeOffset), 0, 0);
                    Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, fireCen, null, Color.White with { A = 0 } * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * (.375f + sizeOffset), 0, 0);

                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            Main.spriteBatch.DrawString(FontAssets.MouseText.Value, state.ToString(), NPC.Center - Main.screenPosition + new Vector2(0, -50), Color.White);
            Main.spriteBatch.DrawString(FontAssets.MouseText.Value, ai1.ToString(), targetPlayer.Center - Main.screenPosition + new Vector2(0, -50), Color.White);

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
        日曜星流,//陨日残阳+初源日炎                                         -0:43
        星恒飞刃,//妖梦非符式奇偶狙+白莲二符收尾                             -0:55
        太阳风暴,//式神「蓝」(不是                                           -1:06
        破晓之光,//莱瓦汀                                                    -1:19

        //追灭状态下全程强风场，随着不同攻击模式改变风场，时符
        陨日残阳_追灭,//无双风神，但是全程预判冲刺
        初源日炎_追灭,//半定向冲刺
        日曜星流_追灭,//陨日残阳+初源日炎
        星恒飞刃_追灭,//妖梦非符式奇偶狙
        太阳风暴_追灭,//式神「蓝」
        破晓之光_追灭,//莱瓦汀，但是全程平移

        //70%血量进入，如果未低于70%则停留在追灭
        //风场停止，参考开始状态，弹幕难度提升，时间持久，随机性增加
        陨日残阳_随机,//无双风神，但是最后五击或者中途随机会预判玩家而后穿刺
        初源日炎_随机,//本体隐去，分身使用日炎风格初源峰巅，范围覆盖全屏，天降正义后生成一堆
        日曜星流_随机,//陨日残阳+初源日炎
        星恒飞刃_随机,//白莲二符，直线冲刺，冲刺频率逐渐增加，弹幕量逐渐降低
        太阳风暴_随机,//式神「蓝」，但是频率更高
        破晓之光_随机,//莱瓦汀,但是全程挥动+自机狙


        //最后30%血量  bgm先暂停后重新开始
        //风场重开且全程固定方向，世界右半部开始则向左......               阶段血量相关
        陨日残阳_后撤,//无双风神，但是是闪飞然后水平向弹幕
        初源日炎_后撤,//本体隐去，分身使用日炎风格初源峰巅，但是是纵向弹幕干扰移动
        日曜星流_后撤,//闪飞后 水平干扰纵向干扰兼具
        星恒飞刃_后撤,//妖梦非符式奇偶狙，但是增加散狙
        太阳风暴_后撤,//上下版边冲撞以发出大量滞留弹幕
        破晓之光_后撤,//参考月总大激光(x
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
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 60;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (drawPlayer != null)
            {
                float point = 0f;
                return projHitbox.Intersects(targetHitbox) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), drawPlayer.Center, drawPlayer.Center + projectile.oldRot[0].ToRotationVector2() * 70, 10, ref point);
            }
            return base.Colliding(projHitbox, targetHitbox);
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
                    projectile.Opacity = Terraria.Utils.GetLerpValue(0f, 12f, offset, true) * Terraria.Utils.GetLerpValue(timeToFly, timeToFly - 12f, offset, true);
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

                if ((int)projectile.ai[1] != 2 && (int)projectile.ai[1] != 3)
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
                foreach (var npc in Main.npc) 
                {
                    if (npc.active && npc.type == ModContent.NPCType<AsraNox>() && npc.ai[0] != 2 && npc.ai[0] != 3 && npc.ai[0] != 4) 
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
                if (flag || (int)projectile.ai[1] == 2 || (int)projectile.ai[1] == 3)
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
                    player.direction = (projectile.velocity.X > 0f) ? 1 : (-1);
                    player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * (float)player.direction, projectile.velocity.X * (float)player.direction);
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

}
