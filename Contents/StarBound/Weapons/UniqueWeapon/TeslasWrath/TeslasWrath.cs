using LogSpiralLibrary.CodeLibrary.Utilties;
using LogSpiralLibrary.CodeLibrary.Utilties.BaseClasses;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using Terraria.DataStructures;
using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.TeslasWrath
{
    public class TeslasWrath : StarboundWeaponBase
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("感受交流电狙击枪的怒火吧！\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("特斯拉之怒");
        }

        public Item item => Item;

        public override void SetDefaults()
        {
            item.damage = 225;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.DamageType = DamageClass.Ranged;
            item.rare = MyRareID.Tier2;
            item.width = 68;
            item.height = 28;
            item.autoReuse = true;
            item.crit = 25;
            item.mana = 0;
            item.useTime = 32;
            item.useAmmo = AmmoID.Bullet;
            item.useStyle = ItemUseStyleID.Shoot;
            item.shootSpeed = 32f;
            item.useAnimation = 32;
            item.shoot = ModContent.ProjectileType<TeslasWrathProj>();
            item.value = Item.sellPrice(0, 10);
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
            recipe.AddIngredient<Materials.StaticCell>(30);
            recipe.AddIngredient<Materials.AncientEssence>(2500);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.ShroomiteBar, 25);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class TeslasWrathEX : TeslasWrath
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("感受交流电狙击枪的怒火吧！\n杨永信之力！\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("特斯拉之怒EX");
        }

        public override WeaponState State => WeaponState.False_EX;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 400;
            item.rare = MyRareID.Tier3;
            item.useTime = 24;
            item.useAnimation = 24;
            item.value = Item.sellPrice(0, 50);
        }

        public override void AddRecipes()
        {
        }
    }

    public class TeslasWrathProj : RangedHeldProjectile, IStarboundWeaponProjectile
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
            if (right)
            {
                NPC target = null;
                float distance = 1024;
                foreach (var npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                    {
                        float currentDistance = (npc.Center - HeldCenter).Length();
                        if (currentDistance < distance)
                        {
                            distance = currentDistance;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {
                    Projectile.velocity = Terraria.Utils.SafeNormalize(target.Center - HeldCenter, Vector2.One);
                    Projectile.rotation = Projectile.velocity.ToRotation();
                }
            }
        }

        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 62 + new Vector2(Projectile.velocity.Y, -Projectile.velocity.X) * Player.direction * 8;

        public override void OnRelease(bool charged, bool left)
        {
            if (controlState < 3 && Player.PickAmmo(((IStarboundWeaponProjectile)this).sourceItem, out int _, out float speed, out int damage, out float knockBack, out int _))
            {
                if (charged || left)
                {
                    SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
                    Projectile.NewProjectileDirect(((IStarboundWeaponProjectile)this).weapon.GetSource_StarboundWeapon(), ShootCenter, Projectile.velocity * (32f + speed), ModContent.ProjectileType<TeslasWrathBullet>(), Player.GetWeaponDamage(((IStarboundWeaponProjectile)this).sourceItem) + damage, knockBack + 4f, Player.whoAmI).extraUpdates *= left ? 1 : 2;
                    Projectile.timeLeft = 20;
                    controlState = 3;
                }
            }
        }

        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / this.UpgradeValue(24f, 18f), 0, 1);
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
                var tile = Framing.GetTileSafely((int)Vec.X / 16, (int)Vec.Y / 16);
                while ((!tile.HasTile || !Main.tileSolid[tile.TileType]) && length < 1024)
                {
                    length += 8;
                    Vec = ShootCenter + unit * length;
                    tile = Framing.GetTileSafely((int)Vec.X / 16, (int)Vec.Y / 16);
                }
            }
            else length = 1024;
            length = MathHelper.Clamp(length, 0, 1024 * factor);
            Vec = length * unit;
            Main.spriteBatch.DrawLine(ShootCenter, Vec, (controlState == 2 || controlState == 3 ? Color.Lerp(Color.Purple, Color.Cyan * .5f, MathHelper.Clamp(shift / 30f, 0, 1)) : Color.Purple) with { A = 0 } * factor_2, 4, true, -Main.screenPosition);
            if (Charged && (controlState == 2 || controlState == 3))
            {
                int max = 1;
                if (shift > 30) max = 2;
                for (int n = 0; n < max; n++)
                {
                    var fac = (shift + 30 * n) % 60;
                    fac /= 60;
                    Main.spriteBatch.DrawLine(ShootCenter, Vec, Color.Lerp(Color.Purple, Color.Transparent, (float)System.Math.Sqrt(fac)) with { A = 0 } * factor_2, 4 + 20 * fac, true, -Main.screenPosition);
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
            origin = new Vector2(28, 16);
        }
    }

    public class TeslasWrathBullet : ModProjectile, IStarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("特斯拉之怒子弹");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 7;
            projectile.light = 0.5f;
            projectile.timeLeft = 300;
            projectile.aiStyle = -1;
            projectile.extraUpdates = 3;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit53, projectile.position);
            for (int n = -3; n < 4; n++)
            {
                ElectricTriangle.NewElectricTriangle(projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 32), Main.rand.NextFloat(0, MathHelper.TwoPi), 16, default, 15, 30);
                Dust.NewDustPerfect(projectile.Center, MyDustId.BlackFlakes, Main.rand.NextVector2Unit() * 3 + projectile.velocity * .25f);
            }
        }

        public override void AI()
        {
            if ((int)VirtualDreamMod.ModTime2 % 4 == 0)
                ElectricTriangle.NewElectricTriangle(projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 32), Main.rand.NextFloat(0, MathHelper.TwoPi), 16, default, 15, 30);
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            for (int n = 0; n < 4; n++)
            {
                bool flag = n == (int)VirtualDreamMod.ModTime % 4;
                Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, projectile.Center - Main.screenPosition, new Rectangle(0, 16 * n, 64, 16), (flag ? Color.White : Color.Purple * .25f) with { A = 0 }, projectile.rotation, new Vector2(32, 8), (flag ? 1f : Main.rand.NextFloat(0.5f, 0.9f)) * new Vector2(3, 1), 0, 0);
            }
            return false;
        }

        private Projectile projectile => Projectile;
    }
}