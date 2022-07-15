using System;
//using static VirtualDream.Contents.StarBound.Weapons.BossDrop.UpgradeWeaponExtension;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;

using VirtualDream.Utils.BaseClasses;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.DragonheadPistol
{
    // 保证类名跟文件名一致，这样也方便查找
    public class DragonheadPistol : GlowItem
    {
        // 设置物品名字，描述的地方
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            // 这里可以写中文了ヾ(@^▽^@)ノ
            DisplayName.SetDefault("龙头手枪");

            // 物品的描述，加入换行符 '\n' 可以多行显示哦
            Tooltip.SetDefault("一个有急躁脾气的手枪，这里是龙。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        //private int Time;
        //private int Time1;
        //private int Dam;
        //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        //{
        //    if (Mod.HasAsset((Texture + "_Glow").Replace("VirtualDream/", "")))
        //        spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow", false), item.Center - Main.screenPosition, null, Color.White, rotation, IllusionBoundMod.GetTexture(Texture + "_Glow", false).Size() * .5f, scale, 0, 0);
        //}
        public Item item => Item;
        // 最最最重要的物品基本属性部分
        public override void SetDefaults()
        {
            item.damage = 50;
            item.knockBack = 0.25f;
            item.rare = MyRareID.Tier1;
            item.useStyle = 5;
            item.useAmmo = AmmoID.Bullet;
            item.DamageType = DamageClass.Ranged;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.width = 24;
            item.height = 24;
            item.crit = 6;
            item.noUseGraphic = true;
            item.scale = 1f;
            item.maxStack = 1;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<DragonHeadPistolProj>();
            item.shootSpeed = 1f;
            item.channel = true;
            item.autoReuse = true;
            item.useTime = 20;
            item.useAnimation = 20;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player) => false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, velocity, item.shoot, damage, knockback, player.whoAmI);
            return false;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        //{
        //    //Vector2 vec = Main.MouseWorld - player.Center;
        //    //Vector2 vec1 = new Vector2(32, 0);
        //    //if ((player.name == "sans." || player.name == "sans" || player.name == "Sans") && Time <= 1)
        //    //{
        //    //    Projectile.NewProjectile(source, player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonLaser>(), damage * 3 / 8, 0.5f, player.whoAmI);
        //    //}
        //    //if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //    //{
        //    //    Projectile.NewProjectile(source, player.Center, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.Dragonhead.DragonheadPistol>(), 0, 0, player.whoAmI);
        //    //}
        //    //Dam = damage;
        //    Projectile.NewProjectile(source, player.Center, velocity, type, damage, knockback, player.whoAmI);
        //    return false;
        //}
        public override bool AltFunctionUse(Player player)
        {
            //if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    Vector2 vec = Main.MouseWorld - player.Center;
        //    Vector2 vec1 = new Vector2(32, 0);
        //    if (Main.mouseRight)
        //    {
        //        Time1++;
        //    }
        //    else
        //    {
        //        Time1 = 0;
        //    }
        //    if (player.altFunctionUse == 2)
        //    {
        //        Time++;
        //        if (Time >= 2 && Time1 >= 30)
        //        {
        //            if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation()), vec1.RotatedBy(vec.ToRotation() + Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireCloud>(), Dam * 3 / 4, 0.5f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (player.channel)
        //        {
        //            Time++;
        //        }
        //        else
        //        {
        //            if (Time >= 30 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * 3, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 1 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBullet>(), Dam * (1 + Time / 15), 0.25f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //    }
        //}
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.PhoenixBlaster);
            recipe1.AddIngredient(ItemID.Flamethrower);
            recipe1.AddIngredient(ItemID.Bone, 100);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.AddIngredient(ItemID.LivingFireBlock, 150);
            recipe1.AddIngredient(ItemID.Ectoplasm, 25);
            recipe1.AddIngredient(ItemID.Ruby, 10);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class DragonheadPistolEX : DragonheadPistol
    {
        // 设置物品名字，描述的地方
        public override void SetStaticDefaults()
        {
            //base.SetStaticDefaults();

            // 这里可以写中文了ヾ(@^▽^@)ノ
            DisplayName.SetDefault("龙头手枪EX");

            // 物品的描述，加入换行符 '\n' 可以多行显示哦
            Tooltip.SetDefault("一个有急躁脾气的手枪，这里是龙。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void AddRecipes()
        {
        }
        // 最最最重要的物品基本属性部分
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 100;
            item.rare = MyRareID.Tier2;
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    Vector2 vec = Main.MouseWorld - player.Center;
        //    Vector2 vec1 = new Vector2(32, 0);
        //    if (Main.mouseRight)
        //    {
        //        Time1++;
        //    }
        //    else
        //    {
        //        Time1 = 0;
        //    }
        //    if (player.altFunctionUse == 2)
        //    {
        //        Time++;
        //        if (Time >= 2 && Time1 >= 24)
        //        {
        //            if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation()), vec1.RotatedBy(vec.ToRotation() + Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireCloudEX>(), Dam * 3 / 5, 0.5f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (player.channel)
        //        {
        //            Time++;
        //        }
        //        else
        //        {
        //            if (Time >= 24 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                int n = Main.rand.Next(10);
        //                if (n < 3)
        //                {
        //                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() + MathHelper.Pi * 3 / 4), vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() - MathHelper.Pi * 3 / 4), vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //                }
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * 3, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 1 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                int n = Main.rand.Next(10);
        //                if (n < 1 + Time / 12)
        //                {
        //                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() + MathHelper.Pi) / 2, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBullet>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //                }
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBullet>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //    }
        //}
    }
    public class DragonheadPistolOD : DragonheadPistol
    {
        // 设置物品名字，描述的地方
        public override void SetStaticDefaults()
        {
            //base.SetStaticDefaults();

            // 这里可以写中文了ヾ(@^▽^@)ノ
            DisplayName.SetDefault("龙头手枪OD");

            // 物品的描述，加入换行符 '\n' 可以多行显示哦
            Tooltip.SetDefault("一个有急躁脾气的手枪，这里是龙。\n看上去，这头龙开始暴走(OverDrive)了    [c/333333:龙和龙的体质不能一概而论(]\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void AddRecipes()
        {
        }
        // 最最最重要的物品基本属性部分
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 200;
            item.rare = MyRareID.Tier3;
        }
        public override void HoldItem(Player player)
        {
            int d = MyDustId.Fire;
            int r = 50;
            float W = (3.1415926f / 180) * (float)IllusionBoundModSystem.ModTime2;
            float X1 = (float)Math.Cos(6 * W) * 1 / (float)(Math.Cos(W)) * r;
            float Y1 = (float)Math.Sin(6 * W) * 1 / (float)(Math.Cos(W)) * r;
            float s = 2f;
            for (float i = 0; i <= MathHelper.TwoPi; i += MathHelper.Pi / 4)
            {
                Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(X1, Y1).RotatedBy(i), d, new Vector2(0f, 0f), 0, Color.White, s);
                float v = 1f;
                dust.noGravity = true;
                dust.velocity *= v;
            }
        }

        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    Vector2 vec = Main.MouseWorld - player.Center;
        //    Vector2 vec1 = new Vector2(32, 0);
        //    if (Main.mouseRight)
        //    {
        //        Time1++;
        //    }
        //    else
        //    {
        //        Time1 = 0;
        //    }
        //    if (player.altFunctionUse == 2)
        //    {
        //        Time++;
        //        if (Time >= 2 && Time1 >= 12)
        //        {
        //            if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation()), vec1.RotatedBy(vec.ToRotation() + Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireCloudOD>(), Dam * 3 / 10, 0.5f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (player.channel)
        //        {
        //            Time++;
        //        }
        //        else
        //        {
        //            if (Time >= 12 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                int n = Main.rand.Next(10);
        //                if (n < 5)
        //                {
        //                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() + MathHelper.Pi * 3 / 4), vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() - MathHelper.Pi * 3 / 4), vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //                }
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * 3, 5f, player.whoAmI);
        //            }
        //            else if (Time >= 1 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
        //            {
        //                int n = Main.rand.Next(10);
        //                if (n < 1 + Time / 6)
        //                {
        //                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() + MathHelper.Pi) / 2, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
        //                }
        //                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBullet>(), Dam * (1 + Time / 6), 0.25f, player.whoAmI);
        //            }
        //            Time = 0;
        //        }
        //    }
        //}
    }
    public class DragonHeadPistolProj : RangedHeldProjectile
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
            Projectile.friendly = Player.name == "Sans" && right && Factor > 0.5f;
            if (right && Charged && Player.name != "Sans")
            {
                if((int)Projectile.ai[0] % 30 == 0)
                SoundEngine.PlaySound(Terraria.ID.SoundID.Item74);
                if((int)Projectile.ai[0] % UpgradeValue(4, 3, 2) == 0)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), ShootCenter, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)) * 32f, ModContent.ProjectileType<DragonFireCloud>(), Player.GetWeaponDamage(Player.HeldItem) / 2, 0.5f, Player.whoAmI, this.UpgradeValue(0.9f, 0.925f, 0.95f));
            }
            if (Projectile.friendly && (int)Projectile.ai[0] % 20 == 0)
                SoundEngine.PlaySound(Terraria.ID.SoundID.Item15);
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 42 + new Vector2(Projectile.velocity.Y, -Projectile.velocity.X) * Player.direction * 7;
        public override void OnRelease(bool charged, bool left)
        {
            if (left)
            {
                var m = UpgradeValue(1, 2, 3);
                if (Charged)
                {
                    SoundEngine.PlaySound(Terraria.ID.SoundID.Item62);
                    for (int n = 0; n < m; n++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), ShootCenter + new Vector2(Projectile.velocity.Y, -Projectile.velocity.X) * 16f * (n - m * 0.5f), Projectile.velocity * 32f, ModContent.ProjectileType<DragonFireBall>(), Player.GetWeaponDamage(Player.HeldItem), 0.25f, Player.whoAmI, UpgradeValue(6, 8, 10));
                    }

                }
                else
                {
                    SoundEngine.PlaySound(Terraria.ID.SoundID.Item36);//36//38
                    for (int n = 0; n < m; n++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), ShootCenter + Projectile.velocity * (n * 8), Projectile.velocity * 32f, ModContent.ProjectileType<DragonFireBullet>(), (int)(Player.GetWeaponDamage(Player.HeldItem) * Factor), 0.25f, Player.whoAmI);
                    }
                }
                Player.PickAmmo(Player.HeldItem, out int _, out float _, out int _, out float _, out int _);
                Projectile.Kill();
            }
        }
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(30f, 24f, 16f), 0, 1);
            }
        }
        public T UpgradeValue<T>(T normal, T extra, T ultra, T defaultValue = default)
        {
            var type = Player.HeldItem.type;
            if (type == ModContent.ItemType<DragonheadPistol>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<DragonheadPistolEX>())
            {
                return extra;
            }

            if (type == ModContent.ItemType<DragonheadPistolOD>())
            {
                return ultra;
            }

            return defaultValue;
        }
        public override void PostDraw(Color lightColor)
        {
            if (Player.name == "Sans" && Factor > 0.5f && Player.controlUseTile)
            {
                var factor = 2 * (Factor - 0.5f);
                Main.spriteBatch.DrawQuadraticLaser_PassColorBar(ShootCenter, Vector2.Normalize(Projectile.velocity), 15, 1024 * factor, 256 * factor, 0.2f * factor, 4, 1);
            }
        }
        public override (int X, int Y) FrameMax => (3, 6);
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            frame = texture.Frame(FrameMax.X, FrameMax.Y, UpgradeValue(0, 1, 2), Factor < 1 ? (int)(Factor * 3) : (int)(IllusionBoundModSystem.ModTime / 4) % 3 + 3);
            origin = new Vector2(5, 24);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Player.name != "Sans" || Factor <= 0.5f)
            {
                return false;
            }

            float point = 0f;
            var factor = 2 * (Factor - 0.5f);
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), ShootCenter, ShootCenter + Projectile.velocity * factor * 1024f, 18 * factor, ref point);
        }
    }
    public class DragonFireCloud : OtherProjectiles.FlameCloud
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Daybreak, 60);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("龙息");
        }
    }
    public class DragonFireBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("龙火子弹");//
        }
        public Projectile projectile => Projectile;
        public override bool PreDraw(ref Color lightColor)
        {
            if (projectile.velocity != default && projectile.timeLeft == 179)
                projectile.rotation = projectile.velocity.ToRotation();
            Main.EntitySpriteDraw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, TextureAssets.Projectile[projectile.type].Value.Frame(1, 4, 0, (int)IllusionBoundModSystem.ModTime / 2 % 4), Color.White, projectile.rotation, TextureAssets.Projectile[projectile.type].Value.Size() * .5f * new Vector2(1, 0.25f), 1f, 0, 0);
            return false;
        }
        public override void SetDefaults()
        {
            projectile.scale = 1f;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.timeLeft = 180;
            projectile.penetrate = 1;
        }
        public override void AI()
        {
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                base.Kill(timeLeft);
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
                    MyDustId.Fire, 0, 0, 100, Color.White, 1.5f);
                d.noGravity = true;
                d.velocity *= 2;
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 150);
        }
    }
    public class DragonFireBall : DragonFireBullet
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("龙炎灭却弹");//龙火子弹
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Daybreak, 60);

        }
        public override void AI()
        {
            base.AI();
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.Fire, 0f, 0f, 100, Color.White, 2f);
            dust.noGravity = true;
        }
        public override bool PreKill(int timeLeft)
        {
            var rot = Main.rand.NextFloat(0, MathHelper.TwoPi);
            for (int n = 0; n < (int)projectile.ai[0]; n++)
            {
                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, (rot + n / projectile.ai[0] * MathHelper.TwoPi).ToRotationVector2() * 4f, ModContent.ProjectileType<DragonFireCloud>(), projectile.damage / 4, projectile.knockBack, projectile.owner, 0.95f);
            }
            SoundEngine.PlaySound(Terraria.ID.SoundID.Item74);

            projectile.hide = true;
            projectile.height = projectile.width = 160;

            return base.PreKill(timeLeft);
        }
    }
}