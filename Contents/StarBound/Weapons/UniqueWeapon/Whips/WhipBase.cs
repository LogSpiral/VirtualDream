using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using System;
using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Whips
{
    public abstract class WhipBase_Item : StarboundWeaponBase
    {
        public abstract void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime);

        public override void SetDefaults()
        {
            int type = 841;
            int damage = 50;
            float knockBack = 1f;
            float shootSpeed = 4f;
            int animationTime = 30;
            WhipInfo(ref type, ref damage, ref knockBack, ref shootSpeed, ref animationTime);
            Item.DefaultToWhip(type, damage, knockBack, shootSpeed, animationTime);
        }
    }

    public abstract class WhipBase_Projectile : ModProjectile, IStarboundWeaponProjectile
    {
        //public IEntitySource source;
        //public int sourceItemType => weapon.Item.type;
        public Player Player => Main.player[projectile.owner];

        public Projectile projectile => Projectile;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
            // DisplayName.SetDefault("鞭");
        }

        public virtual void WhipSettings(ref int segments, ref float rangeMultiplier)
        { }

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();
            projectile.aiStyle = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            projectile.DrawWhip();
            return false;
        }

        public override void AI()
        {
            if (projectile.ai[2] == 0)
            {
                WhipSettings(ref Projectile.WhipSettings.Segments, ref Projectile.WhipSettings.RangeMultiplier);
                Projectile.WhipSettings.Segments = (int)(Projectile.WhipSettings.Segments * Projectile.WhipSettings.RangeMultiplier);
                projectile.ai[2] = 1;
            }
            Player player = Main.player[Projectile.owner];
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
            Projectile.ai[0] += 1f;
            Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
            Projectile.Center = Main.GetPlayerArmPosition(Projectile) + Projectile.velocity * (Projectile.ai[0] - 1f);
            Projectile.spriteDirection = ((!(Vector2.Dot(Projectile.velocity, Vector2.UnitX) < 0f)) ? 1 : (-1));
            if (Projectile.ai[0] >= timeToFlyOut || player.itemAnimation == 0)
            {
                Projectile.Kill();
                return;
            }

            player.heldProj = Projectile.whoAmI;

            // causes projectile to be duped with #2351 because tML makes reuse start at itemAnimation == 1
            // could fix the calculation to `player.itemAnimationMax - (int)((ai[0] - 1) / MaxUpdates);` but the code doesn't do anything useful (even in vanilla)
            // player.itemAnimation = player.itemAnimationMax - (int)(ai[0] / MaxUpdates);
            // player.itemTime = player.itemAnimation;
            if (Projectile.ai[0] == (int)(timeToFlyOut / 2f))
            {
                Projectile.WhipPointsForCollision.Clear();
                Projectile.FillWhipControlPoints(Projectile, Projectile.WhipPointsForCollision);
                Vector2 position = Projectile.WhipPointsForCollision[Projectile.WhipPointsForCollision.Count - 1];
                SoundEngine.PlaySound(SoundID.Item153, position);
            }
            float t3 = Projectile.ai[0] / timeToFlyOut;
            float num5 = Terraria.Utils.GetLerpValue(0.1f, 0.7f, t3, clamped: true) * Terraria.Utils.GetLerpValue(0.9f, 0.7f, t3, clamped: true);
            if (num5 > 0.1f)
            {
                Projectile.WhipPointsForCollision.Clear();
                Projectile.FillWhipControlPoints(Projectile, Projectile.WhipPointsForCollision);
                AI_Other(num5);
            }
        }

        public virtual int DustType => MyDustId.BlackFlakes;

        public virtual void AI_Other(float factor)
        {
            if (Main.rand.NextFloat() < factor / 2f)
            {
                Rectangle r4 = Terraria.Utils.CenteredRectangle(Projectile.WhipPointsForCollision[Projectile.WhipPointsForCollision.Count - 1], new Vector2(30f, 30f));
                int num6 = Dust.NewDust(r4.TopLeft(), r4.Width, r4.Height, DustType, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num6].noGravity = true;
                Main.dust[num6].velocity.X /= 2f;
                Main.dust[num6].velocity.Y /= 2f;
            }
        }
    }
}