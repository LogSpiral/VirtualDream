using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana;
namespace VirtualDream.Contents.StarBound.NPCs.Bosses.AsraNox
{
    [AutoloadBossHead]
    public class AsraNox : ModNPC
    {
        public AsraNoxState state => (AsraNoxState)(byte)NPC.ai[0];
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
        public Player targetPlayer
        {
            get
            {
                Vector2 cen = NPC.Center;
                Player target = null;
                float distanceMax = float.MaxValue;
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
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 420)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 1;
                            break;
                        }
                        int counter = (int)NPC.ai[1];
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
                                        proj.timeLeft = 450 - (int)NPC.ai[1];
                                }
                            }
                        break;
                    }
                case AsraNoxState.陨日残阳:
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 660)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 2;
                            break;
                        }
                        const int timeMax = 20;
                        int counter = (int)NPC.ai[1] % timeMax;
                        int direct = (int)NPC.ai[1] / timeMax % 2;
                        if (counter == 0)
                        {
                            NPC.ai[2] = Main.rand.Next(-480, 480);
                            NPC.ai[3] = Main.rand.Next(0, 280) * Main.rand.Next(new int[] { -1, 1 });
                        }
                        NPC.Center = Vector2.Lerp(new Vector2(direct == 1 ? -1024 : 1024, 0), new Vector2(direct == 1 ? 1024 : -1024, NPC.ai[3]), (float)Math.Pow(counter / (timeMax - 1f), 3)) + targetPlayer.Center + new Vector2(0, 1) * NPC.ai[2];
                        visualPlayer.direction = direct == 1 ? 1 : -1;
                        visualPlayer.itemAnimation = 1;
                        visualPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, 0);
                        NPC.oldPos[0] = NPC.oldPosition;
                        //visualPlayer.itemRotation = 
                        break;
                    }
                case AsraNoxState.初源日炎:
                    {
                        NPC.Center = new Vector2(MathHelper.Lerp(NPC.Center.X, targetPlayer.Center.X, 0.25f), targetPlayer.Center.Y - 480);
                        NPC.damage = 0;
                        if ((int)NPC.ai[1] % 10 == 0)
                        {
                            var value5 = targetPlayer.Center + targetPlayer.velocity * 20f;

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




                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 780)
                        {
                            NPC.ai[1] = 0;
                            //NPC.ai[0] = 3;
                            break;
                        }
                        NPC.oldPos[0] = NPC.oldPosition;

                        break;
                    }
                case AsraNoxState.日曜星流:
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 720)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 4;
                            break;
                        }

                        break;
                    }
                case AsraNoxState.恒星飞刃:
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 720)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 5;
                            break;
                        }

                        break;
                    }
                case AsraNoxState.太阳风暴:
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 660)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 6;
                            break;
                        }

                        break;
                    }
                case AsraNoxState.破晓之光:
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= 780)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 7;
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
                    int direct = (int)NPC.ai[1] / 30 % 2;
                    float rotation = (new Vector2(direct == 1 ? 1024 : -1024, NPC.ai[3]) - new Vector2(direct == 1 ? -1024 : 1024, 0)).ToRotation();
                    spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/StarBound/NPCs/Bosses/AsraNox/SolusKatanaFractal"), NPC.Center + new Vector2(0, 12f) - Main.screenPosition, null, Color.White, rotation + MathHelper.Pi / 4, new Vector2(12, 66), 1, 0, 0);

                    spriteBatch.Draw(tex, NPC.Center + new Vector2(0, 12f) - Main.screenPosition, tex.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 4 % 4), Color.White, rotation - MathHelper.Pi * 3 / 4, new Vector2(20, 64), 0.75f, 0, 0);

                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            Main.spriteBatch.DrawString(FontAssets.MouseText.Value, state.ToString(), NPC.Center - Main.screenPosition + new Vector2(0, -50), Color.White);
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
        恒星飞刃,//妖梦非符式奇偶狙+白莲二符收尾                             -0:55
        太阳风暴,//式神「蓝」(不是                                           -1:06
        破晓之光,//莱瓦汀                                                    -1:19

        //追灭状态下全程强风场，随着不同攻击模式改变风场，时符
        陨日残阳_追灭,//无双风神，但是全程预判冲刺
        初源日炎_追灭,//半定向冲刺
        日曜星流_追灭,//陨日残阳+初源日炎
        恒星飞刃_追灭,//妖梦非符式奇偶狙
        太阳风暴_追灭,//式神「蓝」
        破晓之光_追灭,//莱瓦汀，但是全程平移

        //70%血量进入，如果未低于70%则停留在追灭
        //风场停止，参考开始状态，弹幕难度提升，时间持久，随机性增加
        陨日残阳_随机,//无双风神，但是最后五击或者中途随机会预判玩家而后穿刺
        初源日炎_随机,//本体隐去，分身使用日炎风格初源峰巅，范围覆盖全屏，天降正义后生成一堆
        日曜星流_随机,//陨日残阳+初源日炎
        恒星飞刃_随机,//白莲二符，直线冲刺，冲刺频率逐渐增加，弹幕量逐渐降低
        太阳风暴_随机,//式神「蓝」，但是频率更高
        破晓之光_随机,//莱瓦汀,但是全程挥动+自机狙


        //最后30%血量  bgm先暂停后重新开始
        //风场重开且全程固定方向，世界右半部开始则向左......               阶段血量相关
        陨日残阳_后撤,//无双风神，但是是闪飞然后水平向弹幕
        初源日炎_后撤,//本体隐去，分身使用日炎风格初源峰巅，但是是纵向弹幕干扰移动
        日曜星流_后撤,//闪飞后 水平干扰纵向干扰兼具
        恒星飞刃_后撤,//妖梦非符式奇偶狙，但是增加散狙
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
            float point = 0f;
            return projHitbox.Intersects(targetHitbox) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), drawPlayer.Center, drawPlayer.Center + projectile.oldRot[0].ToRotationVector2() * 70, 10, ref point);
        }
        public float BeginAlpha => projectile.localAI[0] >= 120 ? 1 : projectile.localAI[0] / 120f;
        public override void AI()
        {
            float num = 120f;
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 120)
            {
                float offset = projectile.localAI[0] - 120f;
                if (offset >= num)
                {
                    //projectile.Kill();
                    //return;
                    projectile.position -= projectile.velocity;
                    projectile.Opacity = 0;
                    if (offset >= 180)
                    {
                        projectile.Kill();
                        return;
                    }
                }
                else
                {
                    projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[0]);
                    projectile.Opacity = Terraria.Utils.GetLerpValue(0f, 12f, offset, true) * Terraria.Utils.GetLerpValue(num, num - 12f, offset, true);
                    projectile.direction = ((projectile.velocity.X > 0f) ? 1 : -1);
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
                for (int n = 59; n > 0; n--)
                {
                    projectile.oldPos[n] = projectile.oldPos[n - 1];
                    projectile.oldRot[n] = projectile.oldRot[n - 1];
                }
                projectile.oldPos[0] = projectile.Center - projectile.velocity.SafeNormalize(Vector2.Zero) * 42f;
                projectile.oldRot[0] = projectile.velocity.ToRotation() + projectile.ai[0] * (offset / num).Lerp(-180, 90, true);
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
            if (drawPlayer == null) drawPlayer = new Player();
            Player player = drawPlayer;
            if (player == null) { }
            Vector2 velocity = projectile.velocity;
            Vector2 position = projectile.Center;
            //Main.NewText(projectile.oldPos.Length);
            if (projectile.localAI[0] < 120)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                var alpha = BeginAlpha.HillFactor2();
                for (int n = 0; n < 150; n++)
                {
                    var fac = 1 - n / 150f;
                    Main.spriteBatch.DrawLine(position, velocity, Color.White * alpha * fac, 4 * fac, true, -Main.screenPosition);
                    Main.spriteBatch.DrawLine(position, velocity, Color.Orange * alpha * fac, 8 * fac, true, -Main.screenPosition);
                    position += velocity;
                    velocity = velocity.RotatedBy(projectile.ai[0]);// * (Main.rand.NextFloat(-15f, 15f))
                }
                var rot = (float)IllusionBoundModSystem.ModTime / 360f * MathHelper.TwoPi;
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot, new Vector2(36), new Vector2(1, 4) * .75f, 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * .75f, 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot + MathHelper.PiOver4, new Vector2(36), new Vector2(1, 4) * .375f, 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.Orange * alpha, rot + MathHelper.PiOver4 * 3, new Vector2(36), new Vector2(1, 4) * .375f, 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.White * alpha, rot, new Vector2(36), new Vector2(1, 4) * .375f, 0, 0);
                Main.spriteBatch.Draw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition, null, Color.White * alpha, rot + MathHelper.PiOver2, new Vector2(36), new Vector2(1, 4) * .375f, 0, 0);
                Main.spriteBatch.End();
            }

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
            player.direction = ((projectile.velocity.X > 0f) ? 1 : (-1));
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
        public void DrawSword()
        {
            SpriteEffects spriteEffects = projectile.ai[0] > 0 ? 0 : SpriteEffects.FlipHorizontally;
            Texture2D texture2D4 = TextureAssets.Projectile[projectile.type].Value;
            var color84 = Color.White * projectile.Opacity * 0.9f;
            color84.A /= 2;
            var origin = texture2D4.Size();
            origin *= spriteEffects == 0 ? new Vector2(0.1f, 0.9f) : new Vector2(0.9f, 0.9f);
            var rot = projectile.oldRot[0] + MathHelper.PiOver4;
            rot += projectile.ai[0] < 0 ? MathHelper.Pi / 2 : 0;
            Main.spriteBatch.Draw(texture2D4, projectile.oldPos[0] - Main.screenPosition, null, color84, rot, origin, 1, spriteEffects, 0);
        }
    }

}
