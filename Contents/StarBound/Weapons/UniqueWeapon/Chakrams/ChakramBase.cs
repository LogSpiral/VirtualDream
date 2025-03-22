using Terraria.ID;
using System;
using Terraria.DataStructures;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public abstract class ChakramBaseItem : StarboundWeaponBase
    {
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public Item item => Item;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("着蒸逝铬好冥自");
            // Tooltip.SetDefault("着蒸逝铬好苗竖");
        }
        public override void SetDefaults()
        {
            item.damage = 150;
            item.DamageType = DamageClass.Melee;
            item.rare = MyRareID.Tier2;
            item.value = Item.sellPrice(0, 10);
            item.width = 26;
            item.height = 26;
            item.useStyle = ItemUseStyleID.Swing;
            item.noMelee = true;
            item.useTime = 8;
            item.useAnimation = 8;
            item.knockBack = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.useTurn = true;
            item.noUseGraphic = true;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public virtual bool Extra => false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(GetSource_StarboundWeapon(), position, velocity, type, damage, knockback, player.whoAmI, player.altFunctionUse == 2 ? 1 : 0, Extra ? 1 : 0);
            return false;
        }
    }
    public abstract class ChakramBaseProjectile : ModProjectile, IStarboundWeaponProjectile
    {
        public Projectile projectile => Projectile;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("着蒸逝铬好冥自");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Melee;
            projectile.timeLeft = 180;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
        }
        public virtual bool hit => projectile.timeLeft <= 150;
        public bool Extra => projectile.ai[1] == 1;
        public bool SpecialAttack => projectile.ai[0] == 1;
        public override void AI()
        {
            projectile.rotation = (float)VirtualDreamSystem.ModTime;
            if (hit)
            {
                Player player = Main.player[projectile.owner];
                projectile.timeLeft = 2;
                if (Main.myPlayer == projectile.owner && hit)
                {
                    Rectangle rectangle = new((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value2 = new((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if (rectangle.Intersects(value2))
                    {
                        projectile.Kill();
                    }
                    Vector2 v = player.Center - projectile.Center;
                    v.Normalize();
                    projectile.velocity = v * 24;
                }
            }
            for (int n = projectile.oldPos.Length - 1; n > 0; n--)
            {
                projectile.oldPos[n] = projectile.oldPos[n - 1];
                projectile.oldRot[n] = projectile.oldRot[n - 1];
            }
            projectile.oldPos[0] = projectile.Center;
            projectile.oldRot[0] = projectile.rotation;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = 150;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[projectile.owner] = 3;
            projectile.timeLeft = 150;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float length = projectile.oldPos.Length;
            var tex = TextureAssets.Projectile[Type].Value;
            var rectangle = tex.Frame(2, 1, (int)Projectile.ai[1]);
            var origin = tex.Size() * new Vector2(.25f, .5f);
            for (int n = 0; n < length; n++)
            {
                var factor = (length - n) / length;
                Main.EntitySpriteDraw(tex, projectile.oldPos[n] - Main.screenPosition, rectangle, lightColor * factor, projectile.oldRot[n], origin, (float)Math.Sqrt(factor), 0, 0);
            }
            return false;
        }
    }
}
