using LogSpiralLibrary;
using Terraria;
using VirtualDream.Contents.StarBound.Weapons;

namespace VirtualDream.Contents.StarBound.OtherProjectiles
{
    public class FlameCloud : ModProjectile, IStarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烈焰");
        }

        private Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Melee;
            projectile.ignoreWater = true;
            projectile.timeLeft = 60;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.light = 0.5f;
        }
        public override void AI()
        {
            projectile.velocity *= projectile.ai[0];
            //projectile.alpha = (int)((1 - ((float)projectile.timeLeft).HillFactor(60)) * 255);
            if (projectile.timeLeft % 5 == 4 && projectile.frame < 11)
            {
                projectile.frame++;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft--;
            projectile.velocity = oldVelocity * .975f;
            projectile.friendly = false;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (projectile.velocity != default && projectile.timeLeft == 59)// 
                projectile.rotation = projectile.velocity.ToRotation();
            var fac = (60f - projectile.timeLeft).HillFactor2(60);
            var color = Color.White with { A = 0 } * fac;
            //Main.NewText((fac, color));
            for (int n = 0; n < 4; n++)
                Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, projectile.Center - Main.screenPosition + (Main.rand.NextFloat() + n * MathHelper.PiOver2).ToRotationVector2() * Main.rand.NextFloat(0, 8), TextureAssets.Projectile[projectile.type].Value.Frame(1, 12, 0, projectile.frame), color * .33f, projectile.rotation, TextureAssets.Projectile[projectile.type].Value.Size() * .5f / new Vector2(1f, 12f), 2f, 0, 0);//with { A = 0 } * (60f - projectile.timeLeft).HillFactor2(1)
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[projectile.owner] = 5;
            target.AddBuff(Terraria.ID.BuffID.OnFire, 600);
            base.OnHitNPC(target, hit, damageDone);
        }
        //public override bool PreDraw(ref Color lightColor)
        //{

        //    return false;
        //}
        public override Color? GetAlpha(Color lightColor) => new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
    }
}
