using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using VirtualDream.Utils;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using VirtualDream.Contents.StarBound.Materials;
using System;
using System.Linq;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.AdaptableCrossbow
{
    public class AdaptableCrossbow : GlowItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("右键切换弹药类型。共七种弹药：普通箭、爆炸箭、寒霜箭、带电箭、剧毒箭、引力箭以及斥力箭。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("万用型十字弩");
        }
        public Item item => Item;
        public override void SetDefaults()
        {
            item.damage = 150;
            item.DamageType = DamageClass.Ranged;
            item.width = 22;
            item.height = 58;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = ItemUseStyleID.Shoot;
            item.noMelee = true;
            item.value = 10000;
            item.rare = MyRareID.Tier2;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0.25f;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.crit = 6;
            item.noUseGraphic = true;
            item.shoot = ModContent.ProjectileType<AdaptableCrossbowProj>();
            item.shootSpeed = 1f;
            item.channel = true;
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (CanUseItem(player)) { Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, default, item.shoot, player.GetWeaponDamage(item), player.GetWeaponKnockback(item), player.whoAmI); }
            base.HoldStyle(player, heldItemFrame);
        }
        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        //{
        //    Projectile.NewProjectile(source, player.Center, Vector2.Normalize(velocity) * 64, projtype, damage, knockback, player.whoAmI);
        //    return false;
        //}
        //public override Vector2? HoldoutOffset()
        //{
        //    return new Vector2(-6, 2);
        //}
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    if (type == 1)
        //    {
        //        dusttype = MyDustId.MuddyBrown;
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType1>();
        //    }
        //    if (type == 2)
        //    {
        //        dusttype = MyDustId.OrangeFx;
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType2>();
        //    }
        //    if (type == 3)
        //    {
        //        dusttype = MyDustId.PurpleFx;
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType3>();
        //    }
        //    if (type == 4)
        //    {
        //        dusttype = ModContent.DustType<Dusts.Electrified>();
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType4>();
        //    }
        //    if (type == 5)
        //    {
        //        dusttype = MyDustId.GreenFx;
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType5>();
        //    }
        //    if (type == 6)
        //    {
        //        dusttype = ModContent.DustType<Dusts.Electrified>();
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType6>();
        //    }
        //    if (type == 7)
        //    {
        //        dusttype = ModContent.DustType<Dusts.Electrified>();
        //        projtype = ModContent.ProjectileType<Projectiles.CrossBow.ArrowType7>();
        //    }
        //    if (player.altFunctionUse == 2)
        //    {
        //        item.shoot = ProjectileID.None;
        //        item.useTime = 27;
        //        item.useAnimation = 27;
        //        float rotat = (Main.MouseWorld - player.Center).ToRotation() + MathHelper.Pi;
        //        for (int n = 1; n < 8; n++)
        //        {
        //            if (rotat >= MathHelper.TwoPi / 7 * (n - 1) && rotat < MathHelper.TwoPi / 7 * n)
        //            {
        //                type = n;
        //            }
        //        }
        //        if (type != 6 && type != 7)
        //        {
        //            for (int d = 0; d < (Main.MouseWorld - player.Center).Length(); d += 5)
        //            {
        //                Dust dust = Dust.NewDustPerfect(player.Center + (Main.MouseWorld - player.Center) / (Main.MouseWorld - player.Center).Length() * d, dusttype, new Vector2(0, 0), 0, Color.White, 1f);
        //                dust.noGravity = true;
        //            }
        //        }
        //        else
        //        {
        //            for (int d = 0; d < (Main.MouseWorld - player.Center).Length(); d += 5)
        //            {
        //                if (d % 10 == 0)
        //                {
        //                    Dust dust = Dust.NewDustPerfect(player.Center + (Main.MouseWorld - player.Center) / (Main.MouseWorld - player.Center).Length() * d, dusttype, new Vector2(0, 0), 0, Color.White, 1f);
        //                    dust.noGravity = true;
        //                }
        //                else if (type == 6)
        //                {
        //                    Dust dust = Dust.NewDustPerfect(player.Center + (Main.MouseWorld - player.Center) / (Main.MouseWorld - player.Center).Length() * d, MyDustId.BlackMaterial, new Vector2(0, 0), 0, Color.White, 1f);
        //                    dust.noGravity = true;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (type == 1)
        //        {
        //            item.useTime = 18;
        //            item.useAnimation = 18;
        //        }
        //        if (type == 2)
        //        {
        //            item.useTime = 36;
        //            item.useAnimation = 36;
        //        }
        //        if (type >= 3 && type <= 7)
        //        {
        //            item.useTime = 27;
        //            item.useAnimation = 27;
        //        }
        //        item.shoot = ProjectileID.WoodenArrowFriendly;
        //    }
        //}
        public override bool CanConsumeAmmo(Item ammo, Player player) => false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, velocity, item.shoot, damage, knockback, player.whoAmI);
            return false;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<LivingRoot>(15);
            recipe.AddIngredient<Leather>(15);
            recipe.AddIngredient<HardenedCarapace>(15);
            recipe.AddIngredient<ErchiusCrystal>(15);
            recipe.AddIngredient<CryonicExtract>(15);
            recipe.AddIngredient<PhaseMatter>(15);
            recipe.AddIngredient<ScorchedCore>(15);
            recipe.AddIngredient<SharpenedClaw>(15);
            recipe.AddIngredient<StaticCell>(15);
            recipe.AddIngredient<StickOfRAM>(15);
            recipe.AddIngredient<VenomSample>(15);
            recipe.AddIngredient<AncientEssence>(2500);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class AdaptableCrossbowEX : AdaptableCrossbow
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("右键切换弹药类型。共七种弹药：普通箭、爆炸箭、寒霜箭、带电箭、剧毒箭、引力箭以及斥力箭。\n所有尝试过最后两种特殊箭矢的人都表示那会是他们最喜欢的箭矢......除了有点反胃，以及会影响其他人。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("万用型十字弩EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 350;
            item.useTime = 21;
            item.useAnimation = 21;
            item.rare = MyRareID.Tier3;
        }
    }
    public class AdaptableCrossbowProj : RangedHeldProjectile
    {
        public override Vector2 HeldCenter => base.HeldCenter + Projectile.velocity * new Vector2(4, 8);// + Projectile.velocity * new Vector2(4, 8)
        public override bool UseRight => true;
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void UpdatePlayer()
        {
            Player.ChangeDir(Projectile.direction);
            Player.heldProj = Projectile.whoAmI;
            Player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            Player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, Player.itemRotation - MathHelper.PiOver2 - (Player.direction == -1 ? MathHelper.Pi : 0));
            if ((int)Projectile.ai[0] != 0)
            {
                Player.itemTime = 2;
                Player.itemAnimation = 2;
                Projectile.ai[0]--;
            }
            Projectile.timeLeft = 2;
            Projectile.velocity = Terraria.Utils.SafeNormalize(Main.MouseWorld - HeldCenter, Vector2.One);
            Projectile.rotation = Projectile.velocity.ToRotation();
            var array = new Vector2[] { new Vector2(1, 2) };
            array[0].X++;
        }
        public override bool Charging => ((UseLeft && Player.controlUseItem) || (UseRight && Player.controlUseTile));
        public override void OnCharging(bool left, bool right)
        {
            Projectile.ai[0]--;
            if ((int)Projectile.ai[0] == 0)
            {
                if (right)
                {
                    Projectile.frameCounter++;
                    Projectile.frameCounter %= 7;
                    Projectile.ai[0] = UpgradeValue(15, 10);
                    SoundEngine.PlaySound(new SoundStyle("VirtualDream/Assets/Sound/shotgun_reload_clip3"));
                }
                else if (left)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), ShootCenter, Projectile.velocity * 32, ModContent.ProjectileType<CrossBowArrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.frameCounter, UpgradeValue(0, 1));
                    Projectile.ai[0] = UpgradeValue(27, 21);
                    //SoundEngine.PlaySound(SoundID.Item17);
                    SoundEngine.PlaySound(new SoundStyle("VirtualDream/Assets/Sound/crossbow1"));

                }

            }
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 46 + new Vector2(Projectile.velocity.Y, -Projectile.velocity.X) * Player.direction * 12;
        public override void OnRelease(bool charged, bool left)
        {
            if (UpgradeValue(false, false, true)) { Projectile.Kill(); }
        }
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(30f, 24f, 16f), 0, 1);
            }
        }
        public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        {
            var type = Player.HeldItem.type;
            if (type == ModContent.ItemType<AdaptableCrossbow>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<AdaptableCrossbowEX>())
            {
                return extra;
            }
            return defaultValue;
        }
        public override void PostDraw(Color lightColor)
        {
            //Main.NewText(lightColor);
            //if (Player.name == "Sans" && Factor > 0.5f && Player.controlUseTile)
            //{
            //    var factor = 2 * (Factor - 0.5f);
            //    Main.spriteBatch.DrawQuadraticLaser_PassColorBar(ShootCenter, Vector2.Normalize(Projectile.velocity), 15, 1024 * factor, 256 * factor, 0.2f * factor, 4, 1);
            //}
        }
        public override (int X, int Y) FrameMax => (7, 2);
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            var left = (int)Projectile.ai[1] == 1;
            var factor = 1 - Projectile.ai[0] / (left ? UpgradeValue(27, 21) : UpgradeValue(15, 10));
            factor = factor.HillFactor2(1);
            //if (left)
            //{

            //}
            center = center + (left ? new Vector2(-6 * Player.direction, 0) : new Vector2(-12 * Player.direction, 12)) * factor;
            rotation += -factor * MathHelper.Pi / 6f * Player.direction * (left ? .5f : 1f);
            frame = texture.Frame(FrameMax.X, FrameMax.Y, Projectile.frameCounter, UpgradeValue(0, 1));
            origin = new Vector2(12, 14);
        }
    }
    public class CrossBowArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("万用十字弩之矢");
        }
        Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 5;
            projectile.light = 0.5f;
            projectile.timeLeft = 300;
            projectile.aiStyle = -1;
        }
        public override void AI()
        {
            if (projectile.frameCounter != 1 && projectile.ai[0] < 7)
            {
                projectile.velocity = projectile.velocity + new Vector2(0, 0.2f);
            }
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            switch ((int)projectile.ai[0])
            {
                case 1:
                    {
                        Dust.NewDustPerfect(projectile.Center, MyDustId.Fire, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
                        break;
                    }
                case 2:
                    {
                        Dust.NewDustPerfect(projectile.Center, MyDustId.PurpleFx, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
                        break;
                    }
                case 3:
                    {
                        if (projectile.timeLeft % 2 == 0)
                            ElectricTriangle.NewElectricTriangle(projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 32), Main.rand.NextFloat(0, MathHelper.TwoPi), 16, default, 15, 30);
                        break;
                    }
                case 4:
                    {
                        if (projectile.frameCounter != 1)
                        {
                            Dust.NewDustPerfect(projectile.Center, MyDustId.GreenFXPowder, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
                        }
                        break;
                    }
                case 5:
                case 6:
                    {
                        if (projectile.frameCounter != 1)
                        {
                            projectile.timeLeft = 60;
                            projectile.velocity = projectile.velocity + new Vector2(0, 0.2f);
                            Dust.NewDustPerfect(projectile.Center, MyDustId.BlackMaterial, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
                        }
                        else
                        {
                            projectile.damage = 0;
                            for (int n = 0; n < 4; n++)
                            {
                                Dust.NewDustPerfect(projectile.Center, MyDustId.BlackMaterial, new Vector2(4, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), 0, Color.White, Main.rand.NextFloat(1f, 2f)).noGravity = true;
                            }
                            foreach (NPC npc in Main.npc)
                            {
                                if (npc.type != NPCID.TargetDummy)
                                {
                                    Vector2 v = projectile.Center - npc.Center;
                                    v.Normalize();
                                    if ((projectile.Center - npc.Center).Length() != 0 && !(projectile.timeLeft < 5))
                                    {
                                        npc.velocity += v * 160 / (projectile.Center - npc.Center).Length();
                                    }
                                }
                            }
                            foreach (Player player in Main.player)
                            {
                                Vector2 v = projectile.Center - player.Center;
                                v.Normalize();
                                if ((projectile.Center - player.Center).Length() != 0 && !(projectile.timeLeft < 5))
                                {
                                    player.velocity += v * 160 / (projectile.Center - player.Center).Length() * ((int)projectile.ai[0] == 5 ? 1 : -1);
                                }
                            }
                        }

                        break;
                    }
                case 7:
                    {
                        projectile.friendly = projectile.frameCounter == 0;
                        break;
                    }
            }
        }
        public override void Kill(int timeLeft)
        {
            var type = (int)projectile.ai[0];
            switch (type)
            {
                case 1:
                    var proj = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, default, projectile.type, projectile.damage, 8, projectile.owner, 7);
                    proj.timeLeft = 35;
                    proj.width = proj.width = 160;
                    proj.penetrate = -1;
                    proj.Center = projectile.Center;
                    proj.rotation = projectile.rotation;
                    SoundEngine.PlaySound(Terraria.ID.SoundID.Item62);
                    break;
                case 2:
                    {
                        SoundEngine.PlaySound(SoundID.Item27, projectile.position);
                        for (int num431 = 4; num431 < 31; num431++)
                        {
                            float num432 = projectile.oldVelocity.X * (30f / (float)num431);
                            float num433 = projectile.oldVelocity.Y * (30f / (float)num431);
                            int num434 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num432, projectile.oldPosition.Y - num433), 8, 8, 197, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
                            Main.dust[num434].noGravity = true;
                            Dust dust = Main.dust[num434];
                            dust.velocity *= 0.5f;
                        }
                        break;
                    }
                case 3:
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit53, projectile.position);
                        for (int n = -3; n < 4; n++)
                        {
                            ElectricTriangle.NewElectricTriangle(projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(0, 32), Main.rand.NextFloat(0, MathHelper.TwoPi), 16, default, 15, 30);
                        }
                        //for (int num431 = 4; num431 < 31; num431++)
                        //{
                        //    float num432 = projectile.oldVelocity.X * (30f / (float)num431);
                        //    float num433 = projectile.oldVelocity.Y * (30f / (float)num431);
                        //    int num434 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num432, projectile.oldPosition.Y - num433), 8, 8, 197, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
                        //    Main.dust[num434].noGravity = true;
                        //    Dust dust = Main.dust[num434];
                        //    dust.velocity *= 0.5f;
                        //}
                        break;
                    }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.ai[0] > 0 && projectile.ai[0] < 4)
            {
                projectile.Kill();
            }
            else
            {
                projectile.Center += projectile.velocity;
                projectile.velocity = Vector2.Zero;
                projectile.damage = 0;
                projectile.frameCounter = 1;
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (projectile.ai[0] > 8) return false;
            //Main.EntitySpriteDraw(TextureAssets.Projectile[612].Value, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(26), 1f, 0, 0);
            if ((int)projectile.ai[0] == 7)
            {
                var fac = 1 - projectile.timeLeft / 35f;
                fac = fac.HillFactor2(1);
                //Main.EntitySpriteDraw(TextureAssets.Projectile[612].Value, projectile.Center - Main.screenPosition, new Rectangle(0, 208 - projectile.timeLeft / 2 * 52, 52, 52), Color.White * .5f * fac, projectile.rotation, new Vector2(26), new Vector2(1, 2) * 2, 0, 0);//new Rectangle(0, projectile.timeLeft / 2, 52, 52)
                //if(!TextureAssets.Projectile[687].IsLoaded)
                //Main.instance.LoadProjectile(687);
                Main.EntitySpriteDraw(IllusionBoundMod.GetTexture(Texture.Replace("CrossBowArrow", "ExplosionEffect"),false), projectile.Center - Main.screenPosition, new Rectangle(0, 588 - projectile.timeLeft / 5 * 98, 98, 98), Color.White * fac, projectile.rotation, new Vector2(49), 2f, 0, 0);//new Rectangle(0, projectile.timeLeft / 2, 52, 52)
    
                //Main.NewText("yeeeeee");
             }
            else
            {
                Texture2D texture2D = (int)projectile.ai[1] == 1 ? TextureAssets.Projectile[projectile.type].Value : IllusionBoundMod.GetTexture(Texture + "_Origin", false);
                Main.EntitySpriteDraw(texture2D, projectile.Center - Main.screenPosition, texture2D.Frame((int)projectile.ai[1] == 1 ? 1 : 2, 7, (int)projectile.ai[1] == 1 ? 0 : ((int)IllusionBoundMod.ModTime / 2 % 2), (int)projectile.ai[0]), (int)projectile.ai[0] == 2 || (int)projectile.ai[0] == 3 ? Color.White : lightColor, projectile.rotation, texture2D.Size() * .5f / new Vector2((int)projectile.ai[1] == 1 ? 1 : 2, 7), 1f, 0, 0);
            }

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var type = (int)projectile.ai[0];
            if (type == 5 || type == 6)
            {
                projectile.velocity = default;
                projectile.frameCounter++;
                projectile.damage = 0;
            }
            if (type == 1) Projectile.Kill();
            else
                target.immune[projectile.owner] = 5;
            if (type == 7) projectile.frameCounter++;

            switch (type)
            {
                case 1:
                case 7:
                    target.AddBuff(24, 180);
                    break;
            }
        }
    }
}
