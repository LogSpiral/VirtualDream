using System;
using System.Collections.Generic;

using Terraria.ID;

using static Terraria.ModLoader.ModContent;
using static VirtualDream.Utils.IllusionBoundExtensionMethods;

namespace VirtualDream.Contents.TouhouProject.NPCs.Fairy //基类
{
//    public abstract class Fairies : ModNPC
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault(fairyName);
//            Main.npcFrameCount[npc.type] = 12;
//        }
//        public NPC npc => NPC;
//        protected virtual float scaleX => 1;
//        protected virtual float scaleY => 1;
//        protected float level;
//        protected virtual string fairyName => "妖精";
//        protected int dustType = MyDustId.GreenBubble;
//        private int frameGroup;
//        protected Vector2[] keyPoints = new Vector2[] { new Vector2(160 * 1.732f, -160), new Vector2(160, -160 * 1.732f), new Vector2(-160, -160 * 1.732f), new Vector2(-160 * 1.732f, -160) };
//        //private float velValue;
//        protected int shootCount;
//        protected int frameY;
//        protected int counter;
//        private Vector2 keyPoint;
//        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
//        {
//            SpawnDustOnHit();
//        }
//        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
//        {
//            SpawnDustOnHit();
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
//        {
//            return PreDraw(spriteBatch, drawColor);
//        }
//        public virtual bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            return true;
//        }

//        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
//        {
//            PostDraw(spriteBatch, drawColor);
//        }
//        public virtual void PostDraw(SpriteBatch spriteBatch, Color color)
//        {

//        }

//        public virtual void SpawnDustOnHit()
//        {
//            for (int n = 0; n < 5; n++)
//            {
//                Dust dust = Dust.NewDustPerfect(npc.Center, dustType, new Vector2(4, 0).RotateRandom(MathHelper.TwoPi));
//                dust.noGravity = true;
//            }
//        }
//        public override void OnKill()
//        {
//            SpawnDustWhenBeKilled();
//        }
//        public virtual void SpawnDustWhenBeKilled()
//        {
//            float k = Main.rand.NextFloat(0, MathHelper.TwoPi);
//            for (int n = 0; n < 30; n++)
//            {
//                Dust dust = Dust.NewDustPerfect(npc.Center, dustType, new Vector2(2 + (float)Math.Sin(MathHelper.TwoPi / 30 * n) * 8, 0).RotatedBy(MathHelper.TwoPi / 30 * n + k));
//                dust.noGravity = true;
//            }
//        }
//        public override void SetDefaults()
//        {
//            base.SetDefaults();
//            npc.width = (int)(32 * scaleX);
//            npc.height = (int)(32 * scaleY);
//            npc.knockBackResist = 0f;
//            npc.aiStyle = -1;
//            npc.damage = (int)(15 * (float)Math.Sqrt(level));
//            npc.noGravity = true;
//            npc.noTileCollide = false;
//            npc.defense = (int)(5 * level);
//            npc.lifeMax = (int)(60 * (float)Math.Pow(level, 4.0 / 3.0));
//            npc.value = 10 * (float)Math.Pow(level, 2);
//            npc.friendly = false;
//            keyPoint = keyPoints[Main.rand.Next(0, 3)];
//        }
//        protected Player FindTargetPlayer()
//        {
//            Vector2 cen = npc.Center;
//            Player target = null;
//            float distanceMax = 4096f;
//            foreach (Player player in Main.player)
//            {
//                float currentDistance = Vector2.Distance(cen, player.Center);
//                if (currentDistance < distanceMax)
//                {
//                    distanceMax = currentDistance;
//                    target = player;
//                }
//            }
//            if (target != null)
//            {
//                if (target.Center.X + target.width / 2 > npc.Center.X + npc.width / 2)
//                {
//                    npc.direction = -1;
//                }
//                else
//                {
//                    npc.direction = 1;
//                }
//            }
//            return target;
//        }
//        public override void AI()
//        {
//            ShootProjectile();
//            if (shootCount >= 12)
//            {
//                LeaveTarget();
//                return;
//            }
//            FindTarget(npc.height);
//        }
//        public virtual void LeaveTarget()
//        {
//            Vector2 vec = npc.Center - FindTargetPlayer().Center;
//            vec.Normalize();
//            npc.velocity = vec * 8;
//        }
//        public virtual void FindTarget(int frameHeight, int stay = 3)
//        {
//            Player player = FindTargetPlayer();
//            /*float distance = (player.Center + keyPoint - npc.Center).Length();
//if (distance >= 640)
//{
//    velValue += 0.2f;
//}
//else if (distance >= 480)
//{
//    velValue += 0.15f;
//}
//else if (distance >= 240)
//{
//    velValue += 0.1f;
//}
//else if (distance >= 80)
//{
//    velValue -= 0.1f;
//}
//else 
//{
//    velValue -= 0.2f;
//}*/

//            //velValue = velValue.ValueRange(0, 24);
//            //Vector2 vec = player.Center + keyPoint - npc.Center;
//            //float leng = vec.Length();
//            //vec.Normalize();
//            ////npc.velocity = vec * velValue;
//            //vec *= 20f;
//            //npc.velocity = (npc.velocity * 30f + vec) / 31f;
//            npc.velocity = (player.Center + keyPoint - npc.Center) / 16;
//            if (npc.velocity.Length() >= 16)
//            {
//                frameGroup = 2;
//            }
//            else if (npc.velocity.Length() >= 4)
//            {
//                frameGroup = 1;
//            }
//            else
//            {
//                frameGroup = 0;
//            }
//            frameY += ((int)Main.time % 4 == 0) ? 1 : 0;
//            if (frameGroup == 0)
//            {
//                frameY %= 5;
//                npc.frame.Y = frameY * frameHeight;
//            }
//            if (frameGroup == 1)
//            {
//                frameY %= 3;
//                npc.frame.Y = (frameY + 5) * frameHeight;
//            }
//            if (frameGroup == 2)
//            {
//                frameY %= 4;
//                npc.frame.Y = (frameY + 8) * frameHeight;
//            }
//            if (counter >= stay)
//            {
//                counter = 0;
//                keyPoint = keyPoints[Main.rand.Next(4)];
//            }
//        }
//        public virtual void ShootProjectile()
//        {
//            ShootProjectileCommonly(npc.Center, ProjectileType<PorridgeBullet>());
//        }
//        public void ShootProjectileCommonly(Vector2 pos, int type, float ai0 = 0, float ai1 = 0, int frequency = 60, float rangth = 480, float Vel = 16, int damage = 10)
//        {
//            Player player = FindTargetPlayer();
//            if ((player.Center - npc.Center).Length() <= rangth && (int)Main.time % frequency == 0)
//            {
//                //shootCount++;
//                Vector2 vec = player.Center - pos;
//                vec.Normalize();
//                Projectile.NewProjectile(npc.GetSource_FromAI(), pos, vec * Vel, type, damage, 0, Main.myPlayer, ai0, ai1);
//                Dust d = Dust.NewDustPerfect(pos, MyDustId.WhiteClouds, new Vector2(0, 0), 153, Color.White, 3f);
//                d.noGravity = true;
//            }
//        }
//        public override void FindFrame(int frameHeight)
//        {
//            npc.spriteDirection = npc.direction;
//        }
//    }
    public abstract class BulletProjectile : ModProjectile
    {
        protected virtual string projName => "弹幕";
        protected int scale = 16;
        protected float rotation;
        protected bool hasChangedValue;
        protected float Size = 1;
        protected float Alpha = 1;
        public Projectile projectile => Projectile;
        protected Player FindTargetPlayer()
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
        public virtual bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value
                , projectile.Center - Main.screenPosition
                , new Rectangle(scale * (int)projectile.ai[0]
                , 0, projectile.width, projectile.height)
                , Color.White with { A = 0 } * Alpha, projectile.velocity.ToRotation() + rotation
                , new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
            return false;
        }
        public virtual void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
        }
        public sealed override bool PreDraw(ref Color lightColor)
        {
            return PreDraw(Main.spriteBatch, lightColor);
        }
        public override void PostDraw(Color lightColor)
        {
            PostDraw(Main.spriteBatch, lightColor);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            //return (Main.player[Main.myPlayer].Center - projectile.Center).Length() <= (scale / 2 + 16);
            return (targetHitbox.Center.ToVector2() - projectile.Center).Length() <= (scale * Size / 2 + 16);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(projName);
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = scale;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        //protected BulletAI[] AIS = new BulletAI[2];
        //protected delegate void BulletAI();
        protected virtual void AI_1()
        {

        }
        protected virtual void AI_2()
        {

        }
        protected virtual void AI_3()
        {

        }
        protected virtual void AI_4()
        {

        }
        public override void AI()
        {
            //projectile.localAI[1] += 3;
            //if (255 - (int)projectile.localAI[1] > 0)
            //{
            //	projectile.alpha = 255 - (int)projectile.localAI[1];
            //}
            //else
            //{
            //	projectile.alpha = 0;
            //}
            //projectile.frame = (int)projectile.ai[0];
            switch ((int)projectile.ai[1])
            {
                case 1:
                    AI_1();
                    break;
                case 2:
                    AI_2();
                    break;
                case 3:
                    AI_3();
                    break;
                case 4:
                    AI_4();
                    break;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
        }
    }
}
//namespace VirtualDream.Contents.TouhouProject.NPCs.Fairy//妖精类
//{
//    public class GreenFairy : Fairies
//    {
//        public override void SetDefaults()
//        {
//            base.SetDefaults();
//            scaleX = 1;
//            scaleY = 1;
//            fairyName = "绿色妖精";
//            level = IllusionBoundMod.HarderActive ? 2 : 0 + Main.rand.NextFloat(1, 2f);
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //return spawnInfo.player.ZoneForest() ? 0.8f : 0.4f;
//            return 0;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(0, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//        }
//        public override void ShootProjectile()
//        {
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 20)
//            {
//                ShootProjectileCommonly(npc.Center, ProjectileType<PorridgeBullet>(), frequency: 1, damage: npc.damage / 2);
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class BlueFairy : Fairies
//    {
//        public BlueFairy()
//        {
//            scaleX = 1;
//            scaleY = 1;
//            fairyName = "蓝色妖精";
//            level = Main.rand.NextFloat(1.5f, 3f);
//            dustType = MyDustId.BlueMagic;
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (IllusionBoundMod.HarderActive) 
//            //{
//            //	return 0;
//            //}
//            //if (NPC.downedBoss1) 
//            //{
//            //	return Main.dayTime ? 0.2f : 0.8f;
//            //}
//            //return Main.dayTime ? 0f : 0.2f;
//            return 0;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(0, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//        }
//        public override void ShootProjectile()
//        {
//            if ((int)IllusionBoundMod.ModTime2 % 120 <= 15)
//            {
//                ShootProjectileCommonly(npc.Center, ProjectileType<PorridgeBullet>(), frequency: 3, rangth: 600, damage: npc.damage / 2);
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class DarkBlueFairy : Fairies
//    {
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (!IllusionBoundMod.HarderActive)
//            //{
//            //	return 0;
//            //}
//            //if (NPC.downedBoss1)
//            //{
//            //	return Main.dayTime ? 0.2f : 0.8f;
//            //}
//            //return Main.dayTime ? 0f : 0.2f;
//            return 0;
//        }
//        public DarkBlueFairy()
//        {
//            scaleX = 1;
//            scaleY = 1;
//            fairyName = "暗蓝色妖精";
//            level = Main.rand.NextFloat(1.5f, 3f) + 2;
//            dustType = MyDustId.BlueMagic;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//        }
//        public override void ShootProjectile()
//        {
//            if ((int)IllusionBoundMod.ModTime2 % 120 <= 20)
//            {
//                ShootProjectileCommonly(npc.Center, ProjectileType<DarkBullet>(), frequency: 2, rangth: 600, damage: npc.damage / 2, ai0: 5);
//                Player player = FindTargetPlayer();
//                if ((player.Center - npc.Center).Length() <= 600 && (int)IllusionBoundMod.ModTime2 % 2 == 0)
//                {
//                    Vector2 vec = player.Center - npc.Center;
//                    vec.Normalize();
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vec.RotatedBy(MathHelper.Pi / 12) * 16, ProjectileType<ConeBullet>(), npc.damage / 2, 0, Main.myPlayer, 5);
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vec.RotatedBy(-MathHelper.Pi / 12) * 16, ProjectileType<ConeBullet>(), npc.damage / 2, 0, Main.myPlayer, 5);
//                    Dust d = Dust.NewDustPerfect(npc.Center, MyDustId.BlackMaterial, new Vector2(0, 0), 153, Color.White, 3f);
//                    d.noGravity = true;
//                }
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class PurpleFairy : Fairies
//    {
//        public PurpleFairy()
//        {
//            scaleX = 1;
//            scaleY = 1;
//            fairyName = "紫色妖精";
//            level = IllusionBoundMod.HarderActive ? 3 : 0 + Main.rand.NextFloat(3f, 6f);
//            dustType = MyDustId.PinkBubble;
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //float chance = 0f;
//            //if (Main.hardMode && !Main.dayTime)
//            //{
//            //	chance += 0.4f;
//            //}
//            //else if(spawnInfo.player.ZoneCrimson) 
//            //{
//            //	chance += NPC.downedBoss2 ? 0.5f : 0 + 0.3f;
//            //}
//            //return chance;
//            return 0;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(0, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(0, 51, 51, 51), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / -180, new Vector2(32, 32), 2f, SpriteEffects.None, 0);
//        }
//        public override void ShootProjectile()
//        {
//            if ((int)IllusionBoundMod.ModTime2 % 120 <= 15)
//            {
//                ShootProjectileCommonly(npc.Center, ProjectileType<PorridgeBullet>(), frequency: 5, rangth: 720, damage: npc.damage / 2);
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 30)
//            {
//                for (int n = 0; n < 9; n++)
//                {
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, (MathHelper.TwoPi / 9 * n).ToRotationVector2() * 8, ProjectileType<LightJadeBullet>(), npc.damage / 2, 0, Main.myPlayer, 2);
//                }
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 90)
//            {
//                for (int n = 0; n < 18; n++)
//                {
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center - (MathHelper.TwoPi / 18 * n).ToRotationVector2() * 64, (MathHelper.TwoPi / 18 * n).ToRotationVector2() * 16, ProjectileType<LightJadeBullet>(), npc.damage / 2 + 1, 0, Main.myPlayer, 2);
//                }
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class RedFairy : Fairies
//    {
//        public RedFairy()
//        {
//            scaleX = 1;
//            scaleY = 1;
//            fairyName = "红色妖精";
//            level = Main.rand.NextFloat(5f, 7f);
//            dustType = MyDustId.RedBubble;
//            keyPoints = new Vector2[] { new Vector2(160, 0).RotatedBy(-MathHelper.TwoPi / 10), new Vector2(160, 0).RotatedBy(-2 * MathHelper.TwoPi / 10), new Vector2(160, 0).RotatedBy(-3 * MathHelper.TwoPi / 10), new Vector2(160, 0).RotatedBy(-4 * MathHelper.TwoPi / 10) };
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (Main.hardMode && Main.dayTime)
//            //{
//            //	return 0.4f;
//            //}
//            //else if (spawnInfo.player.ZoneUnderworldHeight) 
//            //{
//            //	return Main.hardMode ? 0.2f : 0f + 0.3f;
//            //}
//            return 0;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            ////Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(0, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //for (float r = 0; r <= MathHelper.TwoPi; r += MathHelper.Pi / 3)
//            //{
//            //    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + new Vector2(96, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi) - Main.screenPosition, null, new Color(1, 1, 1, 1) * 102f, r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi, new Vector2(32, 32), 1f, SpriteEffects.None, 0f);
//            //}
//        }

//        public override void ShootProjectile()
//        {
//            npc.ai[0]++;
//            if ((int)IllusionBoundMod.ModTime2 % 40 == 0)
//            {
//                for (float r = 0; r <= MathHelper.TwoPi; r += MathHelper.Pi / 3)
//                {
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(96, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi), new Vector2(32, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72)), ProjectileType<MiddleJadeBullet>(), npc.damage, 1, Main.myPlayer, Main.rand.Next(16), 1);
//                }
//            }
//            for (float r = 0; r <= MathHelper.TwoPi; r += MathHelper.Pi / 3)
//            {
//                Dust d = Dust.NewDustPerfect(npc.Center + new Vector2(96, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi), MyDustId.CyanBubble, new Vector2(0, 0), 153, Color.White, 1.5f);
//                d.noGravity = true;
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 120 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class DarkRedFairy : Fairies
//    {
//        public DarkRedFairy()
//        {
//            scaleX = 1;
//            scaleY = 1;
//            fairyName = "暗红色妖精";
//            level = Main.rand.NextFloat(5f, 7f) + 3;
//            dustType = MyDustId.RedBubble;
//            keyPoints = new Vector2[] { new Vector2(160, 0).RotatedBy(-MathHelper.TwoPi / 10), new Vector2(160, 0).RotatedBy(-2 * MathHelper.TwoPi / 10), new Vector2(160, 0).RotatedBy(-3 * MathHelper.TwoPi / 10), new Vector2(160, 0).RotatedBy(-4 * MathHelper.TwoPi / 10) };
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            ////Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(0, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //for (float r = 0; r <= MathHelper.TwoPi; r += MathHelper.Pi / 3)
//            //{
//            //    spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + new Vector2(96, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi) - Main.screenPosition, null, new Color(1, 1, 1, 1) * 153f, r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi, new Vector2(32, 32), 1f, SpriteEffects.None, 0f);
//            //}
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (Main.hardMode && Main.dayTime)
//            //{
//            //	return 0.4f;
//            //}
//            //else if (spawnInfo.player.ZoneUnderworldHeight)
//            //{
//            //	return Main.hardMode ? 0.2f : 0f + 0.3f;
//            //}
//            return 0;
//        }
//        public override void ShootProjectile()
//        {
//            npc.ai[0]++;
//            if ((int)IllusionBoundMod.ModTime2 % 20 == 0)
//            {
//                for (float r = 0; r <= MathHelper.TwoPi; r += MathHelper.Pi / 3)
//                {
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(96, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi), new Vector2(32, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72)), ProjectileType<MiddleJadeBullet>(), npc.damage, 1, Main.myPlayer, Main.rand.Next(16), 1);
//                }
//            }
//            for (float r = 0; r <= MathHelper.TwoPi; r += MathHelper.Pi / 3)
//            {
//                Dust d = Dust.NewDustPerfect(npc.Center + new Vector2(96, 0).RotatedBy(r + ((npc.ai[0] - 240) / 60 * 72) / 180 * MathHelper.Pi), MyDustId.RedBubble, new Vector2(0, 0), 153, Color.White, 1.5f);
//                d.noGravity = true;
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 60 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class CyanFairy : Fairies
//    {
//        public CyanFairy()
//        {
//            scaleX = 1.5f;
//            scaleY = 1.5f;
//            fairyName = "青色妖精";
//            level = IllusionBoundMod.HarderActive ? 3 : 0 + Main.rand.NextFloat(6f, 9f);
//            dustType = MyDustId.CyanBubble;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center - Main.screenPosition, null, new Color(51, 51, 51, 51), (float)IllusionBoundMod.ModTime2 * -MathHelper.TwoPi / 180, new Vector2(32, 32), 3f, SpriteEffects.None, 0);
//            //for (int n = 0; n < 4; n++)
//            //{
//            //    //Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + new Vector2(64, 0).RotatedBy(IllusionBoundMod.ModTime2 / 60 + MathHelper.TwoPi / 4 * n) - Main.screenPosition, null, new Color(0, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //}
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (Main.hardMode && spawnInfo.player.ZoneHoly)
//            //{
//            //	return 0.6f;
//            //}
//            return 0;
//        }
//        public override void ShootProjectile()
//        {
//            if (IllusionBoundMod.ModTime2 % 60 < 20)
//            {
//                for (int n = 0; n < 4; n++)
//                {
//                    ShootProjectileCommonly(npc.Center + new Vector2(64, 0).RotatedBy(IllusionBoundMod.ModTime2 / 60 + MathHelper.TwoPi / 4 * n), ProjectileType<CrystalBullet>(), frequency: 2, rangth: 800, ai0: Main.rand.Next(16), damage: npc.damage / 2);
//                }

//            }
//            if ((int)IllusionBoundMod.ModTime2 % 60 == 0)
//            {
//                shootCount++;
//                counter++;
//            }
//        }
//    }
//    public class WhiteFairy : Fairies
//    {
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (NPC.downedMoonlord && !NPC.AnyNPCs(npc.type))
//            //{
//            //	return 0.2f;
//            //}
//            return 0;
//        }
//        public WhiteFairy()
//        {
//            scaleX = 2f;
//            scaleY = 2f;
//            fairyName = "白色妖精";
//            level = Main.rand.NextFloat(12f, 16f);
//            dustType = MyDustId.WhiteClouds;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Vector2[] Vec = new Vector2[] { new Vector2(128, 0).RotatedBy(-MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-2 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-3 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-4 * MathHelper.TwoPi / 10) };
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[0] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[1] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[2] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[3] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[0] - Main.screenPosition, null, new Color(51, 51, 51, 51), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[1] - Main.screenPosition, null, new Color(51, 51, 51, 51), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[2] - Main.screenPosition, null, new Color(51, 51, 51, 51), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa"), npc.Center + Vec[3] - Main.screenPosition, null, new Color(51, 51, 51, 51), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/LightArea"), npc.Center - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)(IllusionBoundMod.ModTime2 / 60 * MathHelper.Pi), new Vector2(180, 180), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/LightArea"), npc.Center - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)(-IllusionBoundMod.ModTime2 / 60 * MathHelper.Pi), new Vector2(180, 180), 1f, SpriteEffects.None, 0);
//        }
//        private int timer;
//        public override void AI()
//        {
//            Vector2[] Vec = new Vector2[] { new Vector2(128, 0).RotatedBy(-MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-2 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-3 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-4 * MathHelper.TwoPi / 10) };
//            Player player = FindTargetPlayer();
//            if (npc.ai[2] == 4)
//            {
//                Vector2 vec = npc.Center - player.Center;
//                vec.Normalize();
//                npc.velocity = vec * 8;
//                if ((npc.Center - player.Center).Length() < 320)
//                {
//                    timer++;
//                }
//                if (timer == 60)
//                {
//                    npc.ai[2] = 0;
//                    timer = 0;
//                    npc.velocity = default;
//                }
//                return;
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 3 == 0 && npc.ai[0] != 64)
//            {
//                float x = npc.ai[0] - 1;
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[0] + npc.Center, new Vector2(4, 0).RotatedBy(x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer, npc.ai[3]);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[1] + npc.Center, new Vector2(-4, 0).RotatedBy(x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer, npc.ai[3]);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[2] + npc.Center, new Vector2(4, 0).RotatedBy(-x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer, npc.ai[3]);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[3] + npc.Center, new Vector2(-4, 0).RotatedBy(-x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer, npc.ai[3]);
//                for (int n = 0; n < 4; n++)
//                {
//                    Dust d = Dust.NewDustPerfect(Vec[n] + npc.Center, dustType, default, 153, Color.White, 3f);
//                    d.noGravity = true;
//                }
//            }
//            if ((npc.Center - player.Center).Length() >= 640 || npc.ai[1] == 120)
//            {
//                npc.ai[1] = 0;
//                npc.ai[0] = 0;
//                npc.ai[3] = Main.rand.Next(8);
//                npc.Center = player.Center + new Vector2(0, -320);
//                for (int n = 0; n < 30; n++)
//                {
//                    Vector2 vector2 = new Vector2(64, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
//                    Dust d = Dust.NewDustPerfect(npc.Center + vector2, dustType, vector2 * -0.125f, 0, Color.White, 3f);
//                    d.noGravity = true;
//                }
//                npc.ai[2]++;
//            }
//            npc.ai[0] += (((int)IllusionBoundMod.ModTime2 % 3 == 0) && npc.ai[0] != 64) ? 1 : 0;
//            if (npc.ai[0] == 64)
//            {
//                npc.ai[1]++;
//                if ((int)IllusionBoundMod.ModTime2 % 20 == 0)
//                {
//                    for (int n = 0; n < 4; n++)
//                    {
//                        Vector2 vector2 = player.Center - (Vec[n] + npc.Center);
//                        vector2.Normalize();
//                        Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[n] + npc.Center, vector2 * 8, ProjectileType<HugeJadeBullet>(), npc.damage, 1, Main.myPlayer, npc.ai[3] % 4);
//                    }
//                }
//            }
//            frameY += ((int)IllusionBoundMod.ModTime2 % 4 == 0) ? 1 : 0;
//            frameY %= 5;
//            npc.frame.Y = frameY * npc.height;
//        }

//    }
//    public class BlackFairy : Fairies
//    {
//        public BlackFairy()
//        {
//            scaleX = 2f;
//            scaleY = 2f;
//            fairyName = "黑色妖精";
//            level = Main.rand.NextFloat(12f, 16f) + 4;
//            dustType = MyDustId.BlackMaterial;
//        }
//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            //if (NPC.downedMoonlord && !NPC.AnyNPCs(npc.type))
//            //{
//            //	return 0.3f;
//            //}
//            return 0;
//        }
//        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
//        {
//            //Vector2[] Vec = new Vector2[] { new Vector2(128, 0).RotatedBy(-MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-2 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-3 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-4 * MathHelper.TwoPi / 10) };
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[0] - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[1] - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[2] - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[3] - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 120, new Vector2(32, 32), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[0] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[1] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[2] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("NPCs/MagicDa1"), npc.Center + Vec[3] - Main.screenPosition, null, new Color(102, 102, 102, 102), (float)-IllusionBoundMod.ModTime2 * MathHelper.TwoPi / 180, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("Items/Weapons/VoidArea"), npc.Center - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)(IllusionBoundMod.ModTime2 / 60 * MathHelper.Pi), new Vector2(180, 180), 1f, SpriteEffects.None, 0);
//            //spriteBatch.Draw(IllusionBoundMod.GetTexture("Items/Weapons/VoidArea"), npc.Center - Main.screenPosition, null, new Color(153, 153, 153, 153), (float)(-IllusionBoundMod.ModTime2 / 60 * MathHelper.Pi), new Vector2(180, 180), 1f, SpriteEffects.None, 0);
//        }
//        private int timer;
//        public override void AI()
//        {
//            Vector2[] Vec = new Vector2[] { new Vector2(128, 0).RotatedBy(-MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-2 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-3 * MathHelper.TwoPi / 10), new Vector2(128, 0).RotatedBy(-4 * MathHelper.TwoPi / 10) };
//            Player player = FindTargetPlayer();
//            if (npc.ai[2] == 6)
//            {
//                Vector2 vec = npc.Center - player.Center;
//                vec.Normalize();
//                npc.velocity = vec * 8;
//                if ((npc.Center - player.Center).Length() < 320)
//                {
//                    timer++;
//                }
//                if (timer == 30)
//                {
//                    npc.ai[2] = 0;
//                    timer = 0;
//                    npc.velocity = default;
//                }
//                return;
//            }
//            if ((int)IllusionBoundMod.ModTime2 % 3 == 0 && npc.ai[0] != 64)
//            {
//                float x = npc.ai[0] - 1;
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[0] + npc.Center, new Vector2(6, 0).RotatedBy(x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[1] + npc.Center, new Vector2(-6, 0).RotatedBy(x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[2] + npc.Center, new Vector2(6, 0).RotatedBy(-x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[3] + npc.Center, new Vector2(-6, 0).RotatedBy(-x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[0] + npc.Center, new Vector2(-6, 0).RotatedBy(x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[1] + npc.Center, new Vector2(6, 0).RotatedBy(x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[2] + npc.Center, new Vector2(-6, 0).RotatedBy(-x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[3] + npc.Center, new Vector2(6, 0).RotatedBy(-x * MathHelper.TwoPi / 32f), ProjectileType<ButterFlyBullet>(), npc.damage, 1, Main.myPlayer);
//                for (int n = 0; n < 4; n++)
//                {
//                    Dust d = Dust.NewDustPerfect(Vec[n] + npc.Center, dustType, default, 153, Color.White, 3f);
//                    d.noGravity = true;
//                }
//            }
//            if ((npc.Center - player.Center).Length() >= 640 || npc.ai[1] == 120)
//            {
//                npc.ai[1] = 0;
//                npc.ai[0] = 0;
//                npc.Center = player.Center + new Vector2(0, -320);
//                for (int n = 0; n < 30; n++)
//                {
//                    Vector2 vector2 = new Vector2(64, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
//                    Dust d = Dust.NewDustPerfect(npc.Center + vector2, dustType, vector2 * -0.25f, 0, Color.White, 3f);
//                    d.noGravity = true;
//                }
//                npc.ai[2]++;
//            }
//            npc.ai[0] += (((int)IllusionBoundMod.ModTime2 % 3 == 0) && npc.ai[0] != 64) ? 1 : 0;
//            if (npc.ai[0] == 64)
//            {
//                npc.ai[1]++;
//                if ((int)IllusionBoundMod.ModTime2 % 5 == 0)
//                {
//                    for (int n = 0; n < 4; n++)
//                    {
//                        Vector2 vector2 = player.Center - (Vec[n] + npc.Center);
//                        vector2.Normalize();
//                        if ((int)IllusionBoundMod.ModTime2 % 10 == 0)
//                        {
//                            Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[n] + npc.Center, vector2 * 12, ProjectileType<HugeJadeBullet>(), npc.damage, 1, Main.myPlayer);
//                        }
//                        Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[n] + npc.Center, vector2.RotatedBy(MathHelper.Pi / 12) * 18, ProjectileType<DartsBullet>(), npc.damage, 1, Main.myPlayer, 15);
//                        Projectile.NewProjectile(npc.GetSource_FromAI(), Vec[n] + npc.Center, vector2.RotatedBy(-MathHelper.Pi / 12) * 18, ProjectileType<DartsBullet>(), npc.damage, 1, Main.myPlayer, 15);
//                    }
//                }
//            }
//            frameY += ((int)IllusionBoundMod.ModTime2 % 4 == 0) ? 1 : 0;
//            frameY %= 5;
//            npc.frame.Y = frameY * npc.height;
//        }

//    }
//}
namespace VirtualDream.Contents.TouhouProject.NPCs.Fairy//弹幕类
{
    public class PorridgeBullet : BulletProjectile
    {
        protected override string projName => "米弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
    }
    public class CrystalBullet : BulletProjectile
    {
        protected override string projName => "晶弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                projectile.timeLeft = 450;
                hasChangedValue = true;
            }
            if (projectile.timeLeft > 150)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.Pi / 72 * (projectile.timeLeft - 150) / 300f);
            }
        }
    }
    public class ButterFlyBullet : BulletProjectile
    {
        protected override string projName => "蝶弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
        public Vector2 C;
        public float r;
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 720;
                Size = 2;
            }
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 360) + 720, 0, 120) / 120f;
            r += MathHelper.TwoPi / 360;
            Vector2 target = C + new Vector2(480, 0).RotatedBy(r);
            projectile.velocity = target - projectile.Center;
            if ((int)Main.time % 10 == 0 && Alpha >= 0.9f)
            {
                for (int n = 0; n < 4; n++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(8, 0).RotatedBy(MathHelper.Pi / 2 * n - r * 2), ProjectileType<DoubleStarBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 5, 1);
                }
            }
        }
    }
    public class HugeJadeBullet : BulletProjectile
    {
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            bool flag = (targetHitbox.Center.ToVector2() - projectile.Center).Length() <= (scale * Size / 2 + 16);
            if ((int)projectile.ai[1] != 4 && Alpha > 0.2f)
            {
                return flag;
            }

            float point = 0f;
            bool flag2 = Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + new Vector2(2048, 0).RotatedBy(r), 64, ref point) || flag;
            return flag2;
            //Vector2 A = new Vector2(64, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center;
            //Vector2 B = new Vector2(64, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center;
            //Vector2 C = new Vector2(64, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r);
            //Vector2 D = new Vector2(64, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r);
            //bool flag2 = targetHitbox.Center.ToVector2().InTriangle(A, B, C) || targetHitbox.Center.ToVector2().InTriangle(D, B, C) || flag;
            //if (flag2) 
            //{
            //	Main.NewText("aywc,zshd");
            //}
            //return flag2;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if ((int)projectile.ai[1] != 4)
            {
                return;
            }
            //Vector2[] Points = new Vector2[6];
            //Points[0] = new Vector2(32, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center;
            //Points[1] = new Vector2(32, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center;
            //Points[2] = new Vector2(32, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r);
            //Points[3] = new Vector2(32, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r);
            //Points[4] = new Vector2(32, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r);
            //Points[5] = new Vector2(32, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center;
            CustomVertexInfo[] Points = new CustomVertexInfo[6];
            Points[0] = new CustomVertexInfo(new Vector2(64, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center, new Vector3(0, 0, Alpha / 2));
            Points[1] = new CustomVertexInfo(new Vector2(64, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center, new Vector3(0, 1, Alpha / 2));
            Points[2] = new CustomVertexInfo(new Vector2(64, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r), new Vector3(1, 0, Alpha / 2));
            Points[3] = new CustomVertexInfo(new Vector2(64, 0).RotatedBy(r + MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r), new Vector3(1, 0, Alpha / 2));
            Points[4] = new CustomVertexInfo(new Vector2(64, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center + new Vector2(2048, 0).RotatedBy(r), new Vector3(1, 1, Alpha / 2));
            Points[5] = new CustomVertexInfo(new Vector2(64, 0).RotatedBy(r - MathHelper.PiOver2) + projectile.Center, new Vector3(0, 1, Alpha / 2));
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            //IllusionBoundMod.LaserEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            //IllusionBoundMod.LaserEffect.Parameters["MainColor"].SetValue(new Vector4(1, 0, 0, 1));
            IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
            IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Color.Red.ToVector4());
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.LaserTex[(projectile.timeLeft / 2) % 4];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.LaserTex[(projectile.timeLeft / 2) % 4];
            Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            //IllusionBoundMod.LaserEffect.CurrentTechnique.Passes[0].Apply();
            IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Points, 0, 2);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }

        protected override string projName => "大玉";
        public override void SetDefaults()
        {
            base.SetDefaults();
            scale = 64;
        }
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                projectile.hostile = false;
                hasChangedValue = true;
                projectile.timeLeft = 150;
                Size = 10;
                //for (float n = 0; n < 6; n += 0.5f) 
                //{
                //	for (float i = 1; i < 4; i += 0.5f) 
                //	{
                //		int k = Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center + new Vector2(64 * i, 0).RotatedBy(MathHelper.Pi / 3 * n + (float)Math.Sin(MathHelper.Pi / 6 * (i - 1))), new Vector2(32 * 1 / i, 0).RotatedBy(MathHelper.Pi / 3 * n + (float)Math.Sin(MathHelper.Pi / 6 * i)), ProjectileType<ScaleBullet>(), projectile.damage, projectile.knockBack, projectile.owner, 11, 1);
                //		ScaleBullet scaleBullet = (ScaleBullet)Main.projectile[k].modProjectile;
                //		scaleBullet.C = projectile.Center;
                //	}
                //}
                //for (float n = 0; n < 36; n++) 
                //{
                //	int k = Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center,new Vector2((n % 6 + 1) / 3f * 8f,0).RotatedBy((MathHelper.TwoPi / 36 * n) * (MathHelper.TwoPi / 36 * n)), ProjectileType<ScaleBullet>(), projectile.damage, projectile.knockBack, projectile.owner, 11, 1);
                //	ScaleBullet scaleBullet = (ScaleBullet)Main.projectile[k].modProjectile;
                //	scaleBullet.C = projectile.Center;
                //}
                Player player = FindTargetPlayer();
                for (float n = 0; n < 6; n++)
                {
                    for (float i = 0; i < 36; i++)
                    {
                        int k = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + new Vector2(96, 0).RotatedBy((player.Center - projectile.Center).ToRotation() + MathHelper.TwoPi / 6 * n), new Vector2((i % 6 + 3) / 6f * 16f, 0).RotatedBy(MathHelper.TwoPi / 36 * i), ProjectileType<ScaleBullet>(), projectile.damage, projectile.knockBack, projectile.owner, 11, 1);
                        ScaleBullet scaleBullet = (ScaleBullet)Main.projectile[k].ModProjectile;
                        scaleBullet.C = projectile.Center;
                    }
                }
            }
            Alpha = projectile.timeLeft / 150f;
        }
        private float r;
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                projectile.hostile = false;
                hasChangedValue = true;
                Size = 5;
                projectile.timeLeft = 119;
                Alpha = MathHelper.Clamp(-Math.Abs(projectile.timeLeft - 59) + 119, 0, 12) / 12f;
            }
            r += MathHelper.Pi / 60;
            //for (float n = 0; n < 6; n++) 
            //{
            //	Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center, new Vector2(8, 0).RotatedBy(r * r * MathHelper.Pi / 60 * 64f + MathHelper.Pi / 3 * n), ProjectileType<RingBullet>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
            //}
            for (float n = 0; n < 2; n++)
            {
                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(8, 0).RotatedBy(r * r * MathHelper.Pi / 60 * 64f + MathHelper.Pi * n), ProjectileType<RingBullet>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
            }
        }
        protected override void AI_3()
        {
            if (!hasChangedValue)
            {
                projectile.hostile = false;
                hasChangedValue = true;
                Size = 3;
                //projectile.timeLeft = 192;
                projectile.timeLeft = 202;
                r = (FindTargetPlayer().Center - projectile.Center).ToRotation() - MathHelper.Pi / 8;
            }
            Alpha = MathHelper.Clamp(-Math.Abs(projectile.timeLeft - 101) + 202, 0, 20) / 20f;
            if (projectile.timeLeft % 10 == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int n = -2; n < 2; n++)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(2, n).RotatedBy(MathHelper.PiOver2 * i + r), ProjectileType<PaperBullet>(), projectile.damage, projectile.knockBack, projectile.owner, 10, 2);
                    }
                }
                //r += MathHelper.Pi / 72
                r += MathHelper.Pi / 72 * 4;
            }
        }
        protected override void AI_4()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                Size = 3;
                projectile.timeLeft = 600;
            }
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 300) + 600, 0, 120) / 120f;
            if ((int)Main.time % 60 == 0)
            {
                float r1 = Main.rand.NextFloat(0, MathHelper.TwoPi);
                for (int n = 0; n < 16; n++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(8, 0).RotatedBy(r1 + MathHelper.TwoPi / 16 * n), ProjectileType<HugeStarBullet>(), projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(8), 1);
                }
            }
            r = ((projectile.Center - FindTargetPlayer().Center).ToRotation() + MathHelper.Pi) * 0.01f + r * 0.99f;
        }
    }
    public class DartsBullet : BulletProjectile
    {
        protected override string projName => "镖弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
    }
    public class ConeBullet : BulletProjectile
    {
        protected override string projName => "锥弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                Size = 2f;
                hasChangedValue = true;
            }
            projectile.velocity += Vector2.Normalize(projectile.velocity) / 16f;
        }
    }
    public class DarkBullet : BulletProjectile
    {
        protected override string projName => "暗弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
    }
    public class MiddleJadeBullet : BulletProjectile
    {
        protected override string projName => "中玉";
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 29;
            }
            projectile.velocity *= 0.8f;
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
        }
        public override void Kill(int timeLeft)
        {
            if ((int)projectile.ai[1] == 1)
            {
                Player target = null;
                float distanceMax = 1280f;
                foreach (Player player in Main.player)
                {
                    float currentDistance = Vector2.Distance(projectile.Center, player.Center);
                    if (currentDistance < distanceMax)
                    {
                        distanceMax = currentDistance;
                        target = player;
                    }
                }
                if (target != null)
                {
                    Vector2 targetVec = target.Center - projectile.Center;
                    targetVec.Normalize();
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, targetVec * 32, ProjectileType<PorridgeBullet>(), projectile.damage, 1, Main.myPlayer, projectile.ai[0]);
                }
            }
        }
    }
    public class LightJadeBullet : BulletProjectile
    {
        private bool AI_1HasHit;
        protected override string projName => "光玉";
        public override void SetDefaults()
        {
            base.SetDefaults();
            scale = 32;
        }
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                Size = 3;
                projectile.timeLeft = 119;
            }
            projectile.velocity *= 1.02f;
            Player player = FindTargetPlayer();
            if (player != null)
            {
                Vector2 d = projectile.Center - FindTargetPlayer().Center;
                float v = 48f;
                if (d.X + projectile.velocity.X >= 960)
                {
                    projectile.Center = new Vector2(player.Center.X + 960, projectile.Center.Y);
                    projectile.velocity = default;
                    if (!AI_1HasHit)
                    {
                        AI_1HasHit = true;
                        for (float n = -17f; n <= 17f; n += 0.5f)
                        {
                            Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(960, 32 * n), new Vector2(-Main.rand.NextFloat(1 / 20f, (17f - Math.Abs(n)) / 17 + 1 / 20f) * v, 0), ProjectileType<DropBullet>(), projectile.damage / 3, projectile.knockBack / 2, projectile.owner, Main.rand.Next(5, 8), 1);
                        }
                    }
                }
                else if (d.X + projectile.velocity.X <= -960)
                {
                    projectile.Center = new Vector2(player.Center.X - 960, projectile.Center.Y);
                    projectile.velocity = default;
                    if (!AI_1HasHit)
                    {
                        AI_1HasHit = true;
                        for (float n = -17f; n <= 17f; n += 0.5f)
                        {
                            Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(-960, 32 * n), new Vector2(Main.rand.NextFloat(1 / 20f, (17f - Math.Abs(n)) / 17 + 1 / 20f) * v, 0), ProjectileType<DropBullet>(), projectile.damage / 3, projectile.knockBack / 2, projectile.owner, Main.rand.Next(5, 8), 1);
                        }
                    }
                }
                if (d.Y + projectile.velocity.Y >= 560)
                {
                    v = 24f;
                    projectile.Center = new Vector2(projectile.Center.X, player.Center.Y + 560);
                    projectile.velocity = default;
                    if (!AI_1HasHit)
                    {
                        AI_1HasHit = true;
                        for (float n = -29.5f; n <= 29.5f; n += 0.5f)
                        {
                            Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(32 * n, 560), new Vector2(0, -Main.rand.NextFloat(1 / 20f, (29.5f - Math.Abs(n)) / 29.5f + 1 / 20f) * v), ProjectileType<DropBullet>(), projectile.damage / 3, projectile.knockBack / 2, projectile.owner, Main.rand.Next(5, 8), 1);
                        }
                    }
                }
                else if (d.Y + projectile.velocity.Y <= -560)
                {
                    v = 24f;
                    projectile.Center = new Vector2(projectile.Center.X, player.Center.Y - 560);
                    projectile.velocity = default;
                    if (!AI_1HasHit)
                    {
                        AI_1HasHit = true;
                        for (float n = -29.5f; n <= 29.5f; n += 0.5f)
                        {
                            Projectile.NewProjectile(projectile.GetSource_FromThis(), player.Center + new Vector2(32 * n, -560), new Vector2(0, Main.rand.NextFloat(1 / 20f, (29.5f - Math.Abs(n)) / 29.5f + 1 / 20f) * v), ProjectileType<DropBullet>(), projectile.damage / 3, projectile.knockBack / 2, projectile.owner, Main.rand.Next(5, 8), 1);
                        }
                    }
                }
            }

        }
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 300;
                projectile.hostile = false;
            }
            Alpha = MathHelper.Clamp(-4 * Math.Abs(projectile.timeLeft - 150) + 600, 0, 120) / 120f;
            if (Alpha >= 1f)
            {
                projectile.velocity *= 1.01f;
                projectile.hostile = true;
            }
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
        }
        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //	spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (int)projectile.ai[0], 0, projectile.width, projectile.height), Color.White * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
        //	if ((int)projectile.ai[1] == 2)
        //	{
        //		DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.Nebula, ShaderTailStyle.Dust);
        //	}
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //	return false;
        //}
    }
    public class DropBullet : BulletProjectile
    {
        protected override string projName => "水滴弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
        private float VX;
        private float VY;
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                float v = 0.2f;
                if (projectile.velocity.X > 0)
                {
                    VX = -v;
                }
                if (projectile.velocity.X < 0)
                {
                    VX = v;
                }
                if (projectile.velocity.Y > 0)
                {
                    VY = -v;
                }
                if (projectile.velocity.Y < 0)
                {
                    VY = v;
                }
            }
            projectile.velocity += new Vector2(VX, VY);
        }
    }
    public class PaperBullet : BulletProjectile
    {
        protected override string projName => "札弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
        protected override void AI_1()
        {
            Player player = FindTargetPlayer();
            float distance = (projectile.Center - player.Center).Length();
            float value = Math.Min(32 / distance, 1);
            if (distance > 320)
            {
                value = 0;
            }
            Alpha = value;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if ((int)projectile.ai[1] == 2)
            {
                target.AddBuff(BuffID.CursedInferno, 150);
            }
        }
        protected override void AI_2()
        {
            //AI_1();
            Dust dust = Dust.NewDustPerfect(projectile.Center, MyDustId.CursedFire, default, 0, Color.White * Alpha, Main.rand.NextFloat(0.5f, 2f));
            dust.noGravity = true;
        }
    }
    public class FireBallBullet : BulletProjectile
    {
        protected override string projName => "火弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if ((int)projectile.ai[1] == 1)
            {
                spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (projectile.frame + 4 * (int)projectile.ai[0]), 0, projectile.width, projectile.height), Color.White with { A = 0 } * Alpha, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (projectile.frame + 4 * (int)projectile.ai[0]), 0, projectile.width, projectile.height), Color.White with { A = 0 } * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
            }
            return false;
        }
        public override void AI()
        {
            projectile.frame += (int)Main.time % 4 == 0 ? 1 : 0;
            projectile.frame %= 4;
            switch ((int)projectile.ai[1])
            {
                case 1:
                    AI_1();
                    break;
                case 2:
                    AI_2();
                    break;
                case 3:
                    AI_3();
                    break;
                case 4:
                    AI_4();
                    break;
            }
        }
        private float timer = 120f;
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 360;
                timer = 120f;
            }
            timer--;
            Player player = FindTargetPlayer();
            float sign = projectile.velocity.X / Math.Abs(projectile.velocity.X);
            float k = 8 / (8 + Math.Abs(projectile.velocity.X));
            projectile.rotation = MathHelper.PiOver2 * sign * (k - 1) + MathHelper.Pi;
            if (player != null)
            {
                Vector2 targetVec = player.Center - projectile.Center;
                float l = targetVec.Length();
                targetVec.Normalize();
                targetVec *= MathHelper.Clamp(timer, 0, 120f) / 36 * MathHelper.Clamp(l, 0, 64) / 64;
                projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
            }
        }
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 480;
                timer = 240f;
            }
            timer--;
            Player player = FindTargetPlayer();
            float sign = projectile.velocity.X / Math.Abs(projectile.velocity.X);
            float k = 16 / (16 + Math.Abs(projectile.velocity.X));
            projectile.rotation = MathHelper.PiOver2 * sign * (k - 1) + MathHelper.Pi;
            if (player != null)
            {
                Vector2 targetVec = player.Center - projectile.Center;
                float l = targetVec.Length();
                targetVec.Normalize();
                targetVec *= MathHelper.Clamp(timer, 0, 240f) / 24 * MathHelper.Clamp(l, 0, 48) / 48;
                projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
            }
            if (timer < 0 && (timer + 240) % 60 == 0)
            {
                //for (int n = 0; n < 5; n++) 
                //{

                //}
                Vector2 vec = player.Center - projectile.Center;
                vec.Normalize();
                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vec * 16, ProjectileType<LaserBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 0, 3);
            }
        }
        protected override void AI_3()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 480;
                timer = 240f;
            }
            timer--;
            Player player = FindTargetPlayer();
            float sign = projectile.velocity.X / Math.Abs(projectile.velocity.X);
            float k = 16 / (16 + Math.Abs(projectile.velocity.X));
            projectile.rotation = MathHelper.PiOver2 * sign * (k - 1) + MathHelper.Pi;
            if (player != null)
            {
                Vector2 targetVec = player.Center - projectile.Center;
                float l = targetVec.Length();
                targetVec.Normalize();
                targetVec *= MathHelper.Clamp(timer, 0, 240f) / 24 * MathHelper.Clamp(l, 0, 48) / 48;
                projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
            }
        }
    }
    public class LaserBullet : BulletProjectile
    {
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            bool flag = false;
            for (int n = 1; n < projectile.oldPos.Length; n++)
            {
                Point vec = projectile.oldPos[n].ToPoint();
                flag |= targetHitbox.Intersects(new Rectangle(vec.X, vec.Y, 8, 8));
            }
            return flag;
        }
        protected override string projName => "激光";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            for (int i = 1; i < projectile.oldPos.Length; ++i)
            {
                if (projectile.oldPos[i] == Vector2.Zero)
                {
                    break;
                }

                var normalDir = projectile.oldPos[i - 1] - projectile.oldPos[i];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                var factor = i / (float)projectile.oldPos.Length;
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var w = MathHelper.Lerp(1f, 0.05f, factor);
                //bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * 32, color, new Vector3((float)Math.Sqrt(factor), 1, w)));
                //bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * -32, color, new Vector3((float)Math.Sqrt(factor), 0, w)));
                bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * 32, color, new Vector3(factor, 1, w)));
                bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * -32, color, new Vector3(factor, 0, w)));
            }
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            if (bars.Count > 2)
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
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                float hue = projectile.ai[0];
                while (hue < 0)
                {
                    hue++;
                }
                while (hue > 1)
                {
                    hue--;
                }
                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(hue, 1, 0.5f).ToVector4());
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.LaserTex[projectile.frame];
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.LaserTex[projectile.frame];
                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void PostAI()
        {
            for (int num31 = projectile.oldPos.Length - 1; num31 > 0; num31--)
            {
                projectile.oldPos[num31] = projectile.oldPos[num31 - 1];
            }
            projectile.oldPos[0] = projectile.position + projectile.velocity;
        }
        public override void AI()
        {
            projectile.frame += (int)Main.time % 4 == 0 ? 1 : 0;
            projectile.frame %= 4;
            switch ((int)projectile.ai[1])
            {
                case 1:
                    AI_1();
                    break;
                case 2:
                    AI_2();
                    break;
                case 3:
                    AI_3();
                    break;
                case 4:
                    AI_4();
                    break;
            }
        }
        public override void Kill(int timeLeft)
        {
            if ((int)projectile.ai[1] == 1 || (int)projectile.ai[1] == 2)
            {
                for (int n = 0; n < projectile.oldPos.Length; n++)
                {
                    Dust d = Dust.NewDustPerfect(projectile.oldPos[n], MyDustId.YellowFx, default, 153, Color.White, 3f);
                    d.noGravity = true;
                    if (projectile.velocity.X > 0)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.oldPos[n], projectile.velocity + new Vector2(-2, 4), ProjectileType<CrystalBullet>(), projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(12, 14));
                    }
                    else
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.oldPos[n], projectile.velocity + new Vector2(2, 4), ProjectileType<CrystalBullet>(), projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(12, 14));
                    }
                }
            }
            if ((int)projectile.ai[1] == 3)
            {
                for (int n = 0; n < projectile.oldPos.Length; n++)
                {
                    Dust d = Dust.NewDustPerfect(projectile.oldPos[n], MyDustId.RedBubble, default, 153, Color.White, 3f);
                    d.noGravity = true;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.oldPos[n], projectile.velocity, ProjectileType<ConeBullet>(), projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(0, 2));
                }
            }
        }
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 90;
            }
            projectile.velocity = projectile.velocity + new Vector2(0, 0.3f);
        }
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 30;
            }
        }
        protected override void AI_3()
        {
            //projectile.timeLeft = 2;
            //projectile.hostile = false;
            //projectile.friendly = true;
            //if (Main.mouseRight) 
            //{
            //	projectile.Kill();
            //}
            //projectile.velocity = (Main.MouseWorld - projectile.Center) / 16;
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 30;
                ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            }
        }
        protected override void AI_4()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            }
            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.Pi / 6 / (60 - projectile.timeLeft * 0.2f));
            if (projectile.timeLeft % 45 == 0)
            {
                //ProjectileID.VortexVortexLightning

                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, default, ProjectileType<InfiniteNightmare.VortexLightningIN>(), projectile.damage, 1f, Main.myPlayer);

                //Point point = projectile.Center.ToTileCoordinates();
                //Point point2 = FindTargetPlayer().Center.ToTileCoordinates();
                //Vector2 vector3 = FindTargetPlayer().Center - projectile.Center;
                //int num87 = 20;
                //int num88 = 3;
                //int num89 = 7;
                //int num90 = 2;
                //int num91 = 0;
                //int num5 = 20;
                //bool flag2 = false;
                //if (vector3.Length() > 2000f)
                //{
                //	flag2 = true;
                //}
                //while (!flag2)
                //{
                //	if (num91 >= 100)
                //	{
                //		break;
                //	}
                //	num5 = num91;
                //	num91 = num5 + 1;
                //	int num92 = Main.rand.Next(point2.X - num87, point2.X + num87 + 1);
                //	int num93 = Main.rand.Next(point2.Y - num87, point2.Y - Math.Abs(num92 - point2.X) + 1);
                //	if ((num93 < point2.Y - num89 || num93 > point2.Y + num89 || num92 < point2.X - num89 || num92 > point2.X + num89) && (num93 < point.Y - num88 || num93 > point.Y + num88 || num92 < point.X - num88 || num92 > point.X + num88) && !Main.tile[num92, num93].nactive())
                //	{
                //		bool flag3 = true;
                //		if (flag3 && Main.tile[num92, num93].lava())
                //		{
                //			flag3 = false;
                //		}
                //		if (flag3 && Collision.SolidTiles(num92 - num90, num92 + num90, num93 - num90, num93 + num90))
                //		{
                //			flag3 = false;
                //		}
                //		if (flag3 && !Collision.CanHitLine(projectile.Center, 0, 0, FindTargetPlayer().Center, 0, 0))
                //		{
                //			flag3 = false;
                //		}
                //		if (flag3)
                //		{

                //			Main.NewText(0);
                //			break;
                //		}
                //	}
                //}
            }
        }
    }
    public class ScaleBullet : BulletProjectile
    {
        protected override string projName => "鳞弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
        }
        public Vector2 C;
        private int coolDown;
        private int timer;
        protected override void AI_1()
        {
            coolDown--;
            Vector2 Vec = projectile.Center + projectile.velocity - C;
            if (Vec.Length() > 320 && coolDown <= 0 && timer < 3)
            {
                coolDown = 5;
                float k1 = projectile.velocity.Y / projectile.velocity.X;
                float k2 = -Vec.X / Vec.Y;
                float r = new Vector2(1, (k2 - k1) / (1 + k2 * k1)).ToRotation();
                //projectile.velocity = projectile.velocity.RotationMartix(r).RotationMartix(r);
                projectile.velocity = projectile.velocity.RotatedBy(2 * r);
                timer++;
            }
        }
    }
    public class StarBullet : BulletProjectile
    {
        protected override string projName => "星弹";
        protected override void AI_1()
        {
            //rotation = (float)Main.time / 60 * MathHelper.TwoPi;
            rotation += projectile.velocity.Length();
            projectile.velocity *= 1.01f;
        }
        protected override void AI_2()
        {
            rotation += projectile.velocity.Length();
            if (projectile.timeLeft > 240)
            {
                projectile.velocity *= 0.975f;
            }
            else if (projectile.timeLeft == 240)
            {
                projectile.velocity = Vector2.Normalize(FindTargetPlayer().Center - projectile.Center) * 4;
            }
            else
            {
                if (projectile.velocity.LengthSquared() < 64)
                {
                    projectile.velocity *= 1.05f;
                }
                else if (projectile.velocity.LengthSquared() > 64)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * 8;
                }
            }
        }
    }
    public class RingBullet : BulletProjectile
    {
        protected override string projName => "环玉";
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if ((int)projectile.ai[1] == 1)
            {
                spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/TouhouProject/NPCs/Fairy/RingBulletD"), projectile.Center - Main.screenPosition, null, Color.White * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
                return false;
            }
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (int)projectile.ai[0], 0, projectile.width, projectile.height), Color.White with { A = 0 } * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
            return false;
        }
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                projectile.hostile = false;
                hasChangedValue = true;
                projectile.timeLeft = 479;
            }
            Player player = FindTargetPlayer();
            if (player != null)
            {
                Vector2 d = projectile.Center - FindTargetPlayer().Center;
                if (d.X + projectile.velocity.X >= 960)
                {
                    projectile.Center = new Vector2(projectile.Center.X + 960, player.Center.Y);
                    projectile.ai[0] = 15;
                    projectile.ai[1] = 0;
                    projectile.hostile = true;
                    projectile.velocity = new Vector2(-projectile.velocity.X, projectile.velocity.Y);
                }
                else if (d.X + projectile.velocity.X <= -960)
                {
                    projectile.Center = new Vector2(projectile.Center.X - 960, player.Center.Y);
                    projectile.ai[0] = 15;
                    projectile.ai[1] = 0;
                    projectile.hostile = true;
                    projectile.velocity = new Vector2(-projectile.velocity.X, projectile.velocity.Y);
                }
                if (d.Y + projectile.velocity.Y >= 560)
                {
                    projectile.Center = new Vector2(projectile.Center.X, player.Center.Y + 560);
                    projectile.ai[0] = 15;
                    projectile.ai[1] = 0;
                    projectile.hostile = true;
                    projectile.velocity = new Vector2(projectile.velocity.X, -projectile.velocity.Y);
                }
                else if (d.Y + projectile.velocity.Y <= -560)
                {
                    projectile.Center = new Vector2(projectile.Center.X, player.Center.Y - 560);
                    projectile.ai[0] = 15;
                    projectile.ai[1] = 0;
                    projectile.hostile = true;
                    projectile.velocity = new Vector2(projectile.velocity.X, -projectile.velocity.Y);
                }
            }
        }
    }
    public class HugeStarBullet : BulletProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            scale = 32;
        }
        protected override string projName => "大星弹";
        protected override void AI_1()
        {
            //rotation = (float)Main.time / 60 * MathHelper.TwoPi;
            rotation += projectile.velocity.Length();
        }
        public Vector2 C;
        public float r;
        public float sr;
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 200;
            }
            Vector2 tar = new Vector2((float)Math.Sin(5 * r) + 1, 0).RotatedBy(3 * r + sr) * 256 + C;
            projectile.velocity = tar - projectile.Center;
            r += MathHelper.Pi / 100;
            if (projectile.timeLeft % 20 == 0)
            {
                for (int n = 0; n < 5; n++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(16, 0).RotatedBy(3 * r + MathHelper.TwoPi / 5 * n + MathHelper.PiOver2 + sr), ProjectileType<StarBullet>(), projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(5, 9), 1);
                }
            }
        }
    }
    public class ArrowBullet : BulletProjectile
    {
        protected override string projName => "箭弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
        protected override void AI_1()
        {
            //rotation += projectile.velocity.Length();
            projectile.velocity *= 1.02f;
        }
    }
    public class DoubleStarBullet : BulletProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
        protected override string projName => "复星弹";
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                Size = 0.5f;
            }
            rotation += projectile.velocity.Length();
        }
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                ProjectileID.Sets.TrailingMode[projectile.type] = 0;
                ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
                projectile.timeLeft = 600;
            }
            rotation += projectile.velocity.LengthSquared();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (int)projectile.ai[0], 0, projectile.width, projectile.height), Color.White with { A = 0 } * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
            if ((int)projectile.ai[1] == 2)
            {
                VirtualDreamDrawMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.Solar, ShaderTailStyle.Light);
            }
            return false;
        }
    }
    public class ReallyHugeJadeBullet : BulletProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 256;
        }
        protected override string projName => "巨型大玉";
        private int r;
        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //	if ((int)projectile.ai[1] == 2)
        //	{
        //		spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (int)projectile.ai[0], 0, projectile.width, projectile.height), Color.Cyan * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
        //	}
        //	else 
        //	{
        //		spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, new Rectangle(scale * (int)projectile.ai[0], 0, projectile.width, projectile.height), Color.White * Alpha, projectile.velocity.ToRotation() + rotation, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
        //	}
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //	return false;
        //}
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 240;
                Size = 0.5f;
            }
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 120) + 240, 0, 120) / 120f;
            if (projectile.timeLeft % 20 == 19)
            {
                float v = 8;
                //r += MathHelper.Pi / 12;
                r += 2;
                r %= 14;
                float fv = 1 + 1 / (r + 1) * 2;
                for (int rad = 0; rad < 6; rad++)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(n * v * fv / 3, 0).RotatedBy(r * MathHelper.Pi / 7 + rad * MathHelper.Pi / 3), ProjectileType<EllipseBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, Main.rand.NextBool(2) ? 6 : 7);
                    }
                }
            }
        }
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 420;
            }
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 210) + 420 + 24, 0, 120) / 120f;
        }
        protected override void AI_3()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 600;
            }
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 300) + 600, 0, 120) / 120f;
            if ((int)Main.time % 6 == 0)
            {
                for (int n = 0; n < 6; n++)
                {
                    Vector2 vec = new Vector2((float)Math.Sin(0.3 * r * MathHelper.Pi / 3)).RotatedBy(MathHelper.TwoPi / 6 * n + r * MathHelper.Pi / 5);
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + 64f * vec, -4 * vec, ProjectileType<KnifeBullet>(), projectile.damage, projectile.knockBack, projectile.owner, n + 1);
                }
                r++;
            }
        }
    }
    public class EllipseBullet : BulletProjectile
    {
        protected override string projName => "椭弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
    }
    public class HeartBullet : BulletProjectile
    {
        protected override string projName => "心弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
        public Vector2 C;
        //public Vector2 PC;
        //private double t;
        protected override void AI_1()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 420;
                //projectile.extraUpdates = 3;
            }
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 210) + 420 + 12, 0, 120) / 120f;
            Vector2 vector2 = projectile.Center - C;
            projectile.velocity = new Vector2(-vector2.X - vector2.Y, vector2.X) / 60f;
        }
        //protected override void AI_1()
        //{
        //	if (!hasChangedValue)
        //	{
        //		hasChangedValue = true;
        //		projectile.timeLeft = 120;
        //	}
        //	t += 0.01;
        //	double theta = 0.86602540378443864676372317075294 * t;
        //	double E = Math.Exp(-t / 2);
        //	float k1 = (float)(E * (Math.Cos(theta) - 0.57735026918962576450914878050196 * Math.Sin(theta)));
        //	float k2 = (float)(-0.57735026918962576450914878050196 * E * Math.Sin(theta));
        //	float k3 = -k2;
        //	float k4 = (float)(E * (Math.Cos(theta) + 0.57735026918962576450914878050196 * Math.Sin(theta)));
        //	var targetPos = C + new Vector2(k1 * PC.X + k2 * PC.Y, k3 * PC.X + k4 * PC.Y).RotatedBy(Main.time / 60 * MathHelper.Pi) * 64f;
        //	projectile.velocity = (targetPos - projectile.Center);
        //	Main.NewText(1, Color.Red, true);
        //}
        protected override void AI_2()
        {
            if (!hasChangedValue)
            {
                hasChangedValue = true;
                projectile.timeLeft = 420;
                //projectile.extraUpdates = 3;
            }
            //t -= 0.01;
            //double theta = 0.86602540378443864676372317075294 * t;
            //double E = Math.Exp(-t / 2);
            //float k1 = (float)(E * (Math.Cos(theta) - 0.57735026918962576450914878050196 * Math.Sin(theta)));
            //float k2 = (float)(-0.57735026918962576450914878050196 * E * Math.Sin(theta));
            //float k3 = -k2;
            //float k4 = (float)(E * (Math.Cos(theta) + 0.57735026918962576450914878050196 * Math.Sin(theta)));
            //var targetPos = C + new Vector2(k1 * PC.X + k2 * PC.Y, k3 * PC.X + k4 * PC.Y).RotatedBy(-Main.time / 60 * MathHelper.Pi) * 64f;
            //projectile.velocity = (targetPos - projectile.Center);
            Alpha = MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 210) + 420 + 12, 0, 120) / 120f;
            Vector2 vector2 = projectile.Center - C;
            projectile.velocity = new Vector2(+vector2.X + vector2.Y, -vector2.X) / 60f;
        }
    }
    public class KnifeBullet : BulletProjectile
    {
        protected override string projName => "刀弹";
        public override void SetDefaults()
        {
            base.SetDefaults();
            rotation = MathHelper.PiOver2;
            scale = 32;
        }
    }
}