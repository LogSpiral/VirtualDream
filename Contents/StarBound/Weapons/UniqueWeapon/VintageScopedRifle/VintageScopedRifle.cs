using LogSpiralLibrary;
using Terraria.DataStructures;
using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.VintageScopedRifle
{
    public class VintageScopedRifle : StarboundWeaponBase
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("老当益壮，这把枪所打出的子弹能轻易击穿钢板。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("老式狙击步枪");
        }
        public Item item => Item;
        public override void SetDefaults()
        {
            item.damage = 300;
            item.noMelee = true;
            item.DamageType = DamageClass.Ranged;
            item.rare = MyRareID.Tier2;
            item.width = 68;
            item.height = 24;
            item.autoReuse = true;
            item.crit = 25;
            item.mana = 75;
            item.useTime = 60;
            item.noUseGraphic = true;
            item.useAmmo = AmmoID.Bullet;
            item.useStyle = ItemUseStyleID.Shoot;
            item.shootSpeed = 32f;
            item.useAnimation = 60;
            item.shoot = ModContent.ProjectileType<VintageScopedRifleProj>();
            item.value = Item.sellPrice(platinum: 3);
            item.knockBack = 7f;
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult) => reduce = player.ownedProjectileCounts[item.shoot] > 0 && player.controlUseTile ? reduce : 0;
        public override bool CanConsumeAmmo(Item ammo, Player player) => player.ownedProjectileCounts[item.shoot] > 0;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(GetSource_StarboundWeapon(), position, velocity, item.shoot, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SniperRifle);
            recipe.AddIngredient<Materials.SolariumStar>(15);
            recipe.AddIngredient<Materials.AncientEssence>(2500);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.ShroomiteBar, 25);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class VintageScopedRifleEX : VintageScopedRifle
    {
        public override void AddRecipes()
        {
        }
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("老当益壮，这把枪所打出的子弹能轻易击穿钢板。\n不对，不只是钢板，就是一米厚的夜明板也能轻易击穿。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("老式狙击步枪EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 500;
            item.rare = MyRareID.Tier3;
            item.useTime = 50;
            item.shootSpeed = 32f;
            item.useAnimation = 50;
            item.mana = 80;
            item.knockBack = 9f;
        }
    }
    public class VintageScopedRifleProj : RangedHeldProjectile,IStarboundWeaponProjectile
    {
        public override Vector2 HeldCenter => base.HeldCenter + Projectile.velocity * new Vector2(4, 8);// + Vector2.Normalize(Main.screenPosition - Player.Center) * 4 + new Vector2(0, 8)//Main.MouseWorld - Player.Center

        public override bool UseRight => true;
        //BossDropWeaponProj<DragonheadPistol, DragonheadPistolEX, DragonheadPistolOD>, IBossDropWeaponProj<DragonheadPistol, DragonheadPistolEX, DragonheadPistolOD>
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void OnCharging(bool left, bool right)
        {
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 50 + new Vector2(Projectile.velocity.Y, -Projectile.velocity.X) * Player.direction * 8;
        public override void OnRelease(bool charged, bool left)
        {
            if (controlState < 3 && Player.PickAmmo(((IStarboundWeaponProjectile)this).sourceItem, out int bulletType, out float speed, out int damage, out float knockBack, out int _))
            {
                if (charged || left)
                {
                    SoundEngine.PlaySound(SoundID.Item62);
                    var proj = Projectile.NewProjectileDirect(((IStarboundWeaponProjectile)this).weapon.GetSource_StarboundWeapon(), ShootCenter, Projectile.velocity * (32f + speed), left ? bulletType : ModContent.ProjectileType<PiercingBullet>(), Player.GetWeaponDamage(((IStarboundWeaponProjectile)this).sourceItem) + damage, knockBack + 4f, Player.whoAmI);
                    if (left) proj.extraUpdates += 2;
                    Projectile.timeLeft = 20;
                    controlState = 3;
                }
            }
        }
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / this.UpgradeValue(30f, 24f), 0, 1);
            }
        }
        public override void PostDraw(Color lightColor)
        {
            var shift = Projectile.ai[0] - this.UpgradeValue(30f, 24f);
            var factor = Factor * Factor;
            var factor_2 = controlState == 3 ? MathHelper.Clamp((Projectile.timeLeft - 12) / 8f, 0, 1) : 1;
            var length = 0f;
            var Vec = ShootCenter;
            var unit = Vector2.Normalize(Projectile.velocity);
            if (controlState == 1)
            {
                var tile = Main.tile[(int)Vec.X / 16, (int)Vec.Y / 16];
                while ((!tile.HasTile || !Main.tileSolid[tile.TileType]) && length < 1024)
                {
                    length += 8;
                    Vec = ShootCenter + unit * length;
                    tile = Main.tile[(int)Vec.X / 16, (int)Vec.Y / 16];
                }
            }
            else length = 1024;
            length = MathHelper.Clamp(length, 0, 1024 * factor);
            Vec = length * unit;
            Main.spriteBatch.DrawLine(ShootCenter, Vec, (controlState == 2 || controlState == 3 ? Color.Lerp(Color.Red, Color.White * .5f, MathHelper.Clamp(shift / 30f, 0, 1)) : Color.Red) with { A = 0 } * factor_2, 4, true, -Main.screenPosition);
            if (Charged && (controlState == 2 || controlState == 3))
            {
                int max = 1;
                if (shift > 30) max = 2;
                for (int n = 0; n < max; n++)
                {
                    var fac = (shift + 30 * n) % 60;
                    fac /= 60;
                    Main.spriteBatch.DrawLine(ShootCenter, Vec, Color.Lerp(Color.Red, Color.Transparent, (float)System.Math.Sqrt(fac)) with { A = 0 } * factor_2, 4 + 20 * fac, true, -Main.screenPosition);
                }
            }
        }
        public override (int X, int Y) FrameMax => (1, 2);
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            frame = texture.Frame(FrameMax.X, FrameMax.Y, 0, this.UpgradeValue(0, 1));
            if (controlState == 3)
            {
                var fac = (1 - Projectile.timeLeft / 20f).HillFactor2();
                rotation += -fac * fac * MathHelper.Pi / 6f * Player.direction;
                center += new Vector2(-6 * Player.direction, 0) * fac;
            }
            origin = new Vector2(20, 18);
        }
    }
    public class PiercingBullet : ModProjectile, IStarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("穿甲弹");
        }
        public Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
            projectile.width = 6;
            projectile.scale = 1f;
            projectile.height = 6;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.timeLeft = 180;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.light = .5f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            for (int n = 0; n < 30; n++)
            {
                Dust.NewDustPerfect(target.Center, MyDustId.RedBubble, projectile.velocity * .25f + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 4f), 0, Color.White, Main.rand.NextFloat(0, 1.5f)).noGravity = true;
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = oldVelocity;
            Collision.HitTiles(projectile.position, projectile.velocity * .25f, projectile.width, projectile.height);
            for (int n = 0; n < 3; n++)
            {
                Dust.NewDustPerfect(projectile.Center, MyDustId.RedBubble, projectile.velocity * .25f + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 4f), 0, Color.White, Main.rand.NextFloat(0, 1.5f)).noGravity = true;
            }
            return false;
        }
        public override void AI()
        {
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            projectile.velocity *= 1.02f;

        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0;
            return projHitbox.Intersects(targetHitbox) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * 2, 10, ref point);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            //var tex = LogSpiralLibraryMod.HeatMap[0];
            //Main.NewText(LogSpiralLibraryMod.HeatMap[0] == null ? "Null辣" : "好欸");
            spriteBatch.DrawShaderTail(projectile, LogSpiralLibraryMod.HeatMap[0].Value, LogSpiralLibraryMod.AniTex[0].Value, LogSpiralLibraryMod.BaseTex[12].Value, 40, new Vector2(projectile.width, projectile.height) * .5f, (1 - projectile.timeLeft / 180f).HillFactor2());
            spriteBatch.Draw(TextureAssets.Projectile[Type].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(10, 3), new Vector2(2, 1), 0, 0);
            return false;
        }
    }
}
