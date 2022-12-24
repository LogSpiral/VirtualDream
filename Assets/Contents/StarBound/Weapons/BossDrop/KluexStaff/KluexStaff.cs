using Terraria.ID;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.KluexStaff
{
    public class KluexStaff : StarboundWeaponBase
    {
        public override bool BossDrop => true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("克鲁西斯法杖");
            Tooltip.SetDefault("这根强大的法杖可以支持挥动着它的战士\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            //Item.staff[item.type] = true;
        }
        //public int Time;
        //public int Timex;
        //public int TimeR;
        //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        //{
        //    if (Mod.HasAsset((Texture + "_Glow").Replace("VirtualDream/", "")))
        //        spriteBatch.Draw(IllusionBoundMod.GetTexture(Texture + "_Glow", false), item.Center - Main.screenPosition, null, Color.White, rotation, IllusionBoundMod.GetTexture(Texture + "_Glow", false).Size() * .5f, scale, 0, 0);
        //}
        public Item item => Item;
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult) => reduce = player.ownedProjectileCounts[item.shoot] > 0 ? reduce : 0;

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public override void SetDefaults()
        {
            item.rare = MyRareID.Tier1;
            item.damage = 150;
            item.crit = 50;
            item.DamageType = DamageClass.Magic;
            item.width = 46;
            item.height = 84;
            item.mana = 25;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 6;
            item.useStyle = 5;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<KluexStaffProj>();
            item.noUseGraphic = true;
            item.noMelee = true;
            item.channel = true;
            item.shootSpeed = 1f;
            item.UseSound = SoundID.Item13;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    if (Main.mouseRight)
        //    {
        //        TimeR++;
        //    }
        //    else
        //    {
        //        if (TimeR >= 60)
        //        {
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyZone>(), 0, 0, player.whoAmI);
        //        }
        //        TimeR = 0;
        //    }
        //    if (player.channel)
        //    {
        //        Time++;
        //        Timex++;
        //        if (Time >= 24 && Timex >= 60)
        //        {
        //            Time = 0;
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //        }
        //    }
        //    else
        //    {
        //        Timex = 0;
        //        Time = 0;
        //    }
        //}
        public override void HoldItem(Player player)
        {
            Dust dust = Dust.NewDustPerfect(Main.MouseWorld, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            dust.noGravity = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.RubyStaff);
            recipe1.AddIngredient(ItemID.Ruby, 15);
            recipe1.AddIngredient(ItemID.HallowedBar, 30);
            recipe1.AddIngredient(ItemID.ShadowbeamStaff);
            recipe1.AddIngredient(ItemID.GoldBar, 15);
            recipe1.AddIngredient(ItemID.ManaCrystal, 5);
            recipe1.AddIngredient(ItemID.Ectoplasm, 30);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.RubyStaff);
            recipe2.AddIngredient(ItemID.Ruby, 15);
            recipe2.AddIngredient(ItemID.HallowedBar, 30);
            recipe2.AddIngredient(ItemID.SpectreStaff);
            recipe2.AddIngredient(ItemID.GoldBar, 15);
            recipe2.AddIngredient(ItemID.ManaCrystal, 5);
            recipe2.AddIngredient(ItemID.Ectoplasm, 30);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.RubyStaff);
            recipe3.AddIngredient(ItemID.Ruby, 15);
            recipe3.AddIngredient(ItemID.HallowedBar, 30);
            recipe3.AddIngredient(ItemID.InfernoFork);
            recipe3.AddIngredient(ItemID.GoldBar, 15);
            recipe3.AddIngredient(ItemID.ManaCrystal, 5);
            recipe3.AddIngredient(ItemID.Ectoplasm, 30);
            recipe3.SetResult(this);
            recipe3.AddRecipe();
        }
    }
    public class KluexStaffEX : KluexStaff
    {
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("这根强大的法杖可以支持挥动着它的战士\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("克鲁西斯法杖EX");
        }
        //private static float Time = 0;
        //private static float X = 0;
        //private static float Y = 0;
        //public int Time2;
        //public int Timex;
        //public int TimeR;
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 250;
            item.crit = 50;
            item.mana = 30;
            item.rare = MyRareID.Tier2;
            item.width = 46;
            item.height = 88;
        }
        public override void HoldItem(Player player)
        {
            //Time += MathHelper.Pi / 60;
            //X = (float)Math.Cos(Time);
            //Y = (float)Math.Sin(Time);
            var vec = ((float)IllusionBoundMod.ModTime2 * MathHelper.Pi / 60).ToRotationVector2() * 32;
            /*Dust dust1 = */
            Dust.NewDustPerfect(Main.MouseWorld + vec, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
            /*Dust dust2 = */
            Dust.NewDustPerfect(Main.MouseWorld - vec, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
            /*Dust dust3 = */
            Dust.NewDustPerfect(Main.MouseWorld, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
            //dust1.noGravity = true;
            //dust2.noGravity = true;
            //dust3.noGravity = true;
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    if (Main.mouseRight)
        //    {
        //        TimeR++;
        //    }
        //    else
        //    {
        //        if (TimeR >= 45)
        //        {
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyZoneEX>(), 0, 0, player.whoAmI);
        //        }
        //        TimeR = 0;
        //    }
        //    if (player.channel)
        //    {
        //        Time2++;
        //        Timex++;
        //        if (Time2 >= 18 && Timex >= 45)
        //        {
        //            Time2 = 0;
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld + new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld - new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //        }
        //    }
        //    else
        //    {
        //        Timex = 0;
        //        Time2 = 0;
        //    }
        //}
    }
    public class KluexStaffPH : KluexStaff
    {
        public override WeaponState State => WeaponState.False_UL;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("这根强大的法杖可以支持挥动着它的战士\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n纯粹幻象赤色水晶凝聚跃动着的能量，充斥着整根法杖，又聚焦于一点\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");//你知道......什么叫自机狙吗？！(已经被万恶的阿汪削弱(?)了)
            DisplayName.SetDefault("克鲁西斯法杖PH");
            //Item.staff[item.type] = true;
        }
        //public int Time2;
        //public int Timex;
        //public int TimeR;

        public override void SetDefaults()
        {

            base.SetDefaults();
            item.damage = 400;
            item.crit = 50;
            item.rare = MyRareID.Tier3;
            item.mana = 40;
            item.width = 46;
            item.height = 94;
        }
        public override void HoldItem(Player player)
        {
            var vec = ((float)IllusionBoundMod.ModTime2 * 0.05f).ToRotationVector2() * new Vector2(5, 3) * 32;
            for (int n = 0; n < 4; n++)
            {
                Dust.NewDustPerfect(Main.MouseWorld + vec, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
                vec = new Vector2(-vec.Y, vec.X);
            }
            //Dust dust1 = Dust.NewDustPerfect(Main.MouseWorld + new Vector2(X, Y) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust2 = Dust.NewDustPerfect(Main.MouseWorld - new Vector2(X, Y) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust3 = Dust.NewDustPerfect(Main.MouseWorld + new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust4 = Dust.NewDustPerfect(Main.MouseWorld - new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            /*Dust dust5 = */
            Dust.NewDustPerfect(Main.MouseWorld, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f).noGravity = true;
            //Dust dust6 = Dust.NewDustPerfect(player.Center + new Vector2(X, Y).RotatedBy(Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust7 = Dust.NewDustPerfect(player.Center - new Vector2(X, Y).RotatedBy(Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust8 = Dust.NewDustPerfect(player.Center + new Vector2(X, Y).RotatedBy(MathHelper.PiOver2 + Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //Dust dust9 = Dust.NewDustPerfect(player.Center - new Vector2(X, Y).RotatedBy(MathHelper.PiOver2 + Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
            //dust1.noGravity = true;
            //dust2.noGravity = true;
            //dust3.noGravity = true;
            //dust4.noGravity = true;
            //dust5.noGravity = true;
            //dust6.noGravity = true;
            //dust7.noGravity = true;
            //dust8.noGravity = true;
            //dust9.noGravity = true;
        }
        //public override void UseStyle(Player player, Rectangle rectangle)
        //{
        //    if (Main.mouseRight)
        //    {
        //        TimeR++;
        //    }
        //    else
        //    {
        //        if (TimeR >= 30)
        //        {
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyZonePH>(), 0, 0, player.whoAmI);
        //        }
        //        TimeR = 0;
        //    }
        //    if (player.channel)
        //    {
        //        Time2++;
        //        Timex++;
        //        if (Time2 >= 12 && Timex >= 30)
        //        {
        //            Time2 = 0;
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld + new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld - new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld + new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld - new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //            Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
        //        }
        //    }
        //    else
        //    {
        //        Timex = 0;
        //        Time2 = 0;
        //    }
        //}
    }
    public class KluexStaffProj : RangedHeldProjectile
    {
        //TODO 右键单点BUG修复

        //BossDropWeaponProj<DragonheadPistol, DragonheadPistolEX, DragonheadPistolOD>, IBossDropWeaponProj<DragonheadPistol, DragonheadPistolEX, DragonheadPistolOD>
        public int plasmaBallCount;
        public int plasmaBallMax;
        public override (int X, int Y) FrameMax => (20, 3);
        public override bool UseRight => true;
        public override void OnCharging(bool left, bool right)
        {
            if (left)
            {
                if (plasmaBallCount < UpgradeValue(60, 120, 180) && (int)Projectile.ai[0] % UpgradeValue(24, 18, 12) == 0)
                {
                    var type = ModContent.ProjectileType<KluexPlasmaBall>();
                    Vector2 vec;
                    switch (UpgradeValue(0, 1, 2))
                    {
                        case 0:
                            Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld, default, type, 0, 6, Player.whoAmI, plasmaBallCount);
                            break;
                        case 1:
                            vec = ((float)IllusionBoundModSystem.ModTime2).ToRotationVector2() * 32;
                            Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld + vec, new Vector2(0, 0), type, 0, 6, Player.whoAmI, plasmaBallCount);
                            Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld - vec, new Vector2(0, 0), type, 0, 6, Player.whoAmI, plasmaBallCount);
                            Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld, new Vector2(0, 0), type, 0, 6, Player.whoAmI, plasmaBallCount);
                            break;
                        case 2:
                            vec = ((float)IllusionBoundMod.ModTime2 * 0.05f).ToRotationVector2() * new Vector2(5, 3) * 32;
                            for (int n = 0; n < 4; n++)
                            {
                                Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld + vec, new Vector2(0, 0), type, 0, 6, Player.whoAmI, plasmaBallCount);
                                vec = new Vector2(-vec.Y, vec.X);
                            }
                            Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld, new Vector2(0, 0), type, 0, 6, Player.whoAmI, plasmaBallCount);
                            break;
                    }
                    plasmaBallCount++;
                    plasmaBallMax = plasmaBallCount;
                    SoundEngine.PlaySound(SoundID.Item15);

                }

            }
        }
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 42 + new Vector2(Projectile.velocity.Y, -Projectile.velocity.X * Player.direction) * 7;
        public override void OnRelease(bool charged, bool left)
        {
            if (left)
            {
                Projectile.timeLeft = 2;
                if (plasmaBallCount > 0)
                {
                    Projectile.ai[0]++;
                    if ((int)Projectile.ai[0] % UpgradeValue(10, 8, 6) == 0)
                    {
                        int count = 0;
                        foreach (var proj in Main.projectile)
                        {
                            if (proj.active && proj.type == ModContent.ProjectileType<KluexPlasmaBall>() && (int)proj.ai[0] == plasmaBallMax - plasmaBallCount)
                            {
                                proj.ai[1] = 1;

                                var vec = (Main.MouseWorld - proj.Center).SafeNormalize(Vector2.UnitX);
                                Projectile.NewProjectileDirect(proj.GetSource_FromThis(), proj.Center, vec * 32, ModContent.ProjectileType<KluexPlasmaCrystal>(), Player.GetWeaponDamage(sourceItem), 6, Player.whoAmI, plasmaBallCount).rotation = vec.ToRotation();

                                count++;
                            }
                        }
                        if (count > 0) SoundEngine.PlaySound(SoundID.Item68);
                        plasmaBallCount--;

                    }

                }
                //if (plasmaBallCount == 0)
                //{
                //    Projectile.ai[0] = 10;
                //    plasmaBallCount--;
                //}
                //if (plasmaBallCount < 0)
                //{
                //    Projectile.ai[0]--;
                //    if (Projectile.ai[0] == 0)
                //    {
                //        Projectile.Kill();
                //        foreach (var proj in Main.projectile)
                //        {
                //            if (proj.active && proj.type == ModContent.ProjectileType<KluexPlasmaBall>())
                //            {
                //                proj.ai[1] = 1;
                //                Projectile.NewProjectile(proj.GetSource_FromThis(), proj.Center, Vector2.Normalize(Main.MouseWorld - proj.Center) * 32, ModContent.ProjectileType<KluexPlasmaCrystal>(), Player.GetWeaponDamage(Player.HeldItem), 6, Player.whoAmI, plasmaBallCount);

                //            }
                //        }
                //    }


                //}
            }
            else
            {
                Projectile.timeLeft = 2;
                if (Charged)
                {
                    Projectile.NewProjectile(weapon.GetSource_StarboundWeapon(), Main.MouseWorld, default, ModContent.ProjectileType<KluexZone>(), 0, 0, Projectile.owner, UpgradeValue(1, 2, 3));
                    SoundEngine.PlaySound(SoundID.Item60, Projectile.Center);
                }

            }
            if (plasmaBallCount == 0)
            {
                Projectile.ai[0] = UpgradeValue(15, 12, 9) + 1;
                plasmaBallCount--;
            }
            if (plasmaBallCount < 0)
            {
                Projectile.ai[0]--;
                if (Projectile.ai[0] == 0)
                {
                    Projectile.Kill();
                    foreach (var proj in Main.projectile)
                    {
                        if (proj.active && proj.type == ModContent.ProjectileType<KluexPlasmaBall>())
                        {
                            proj.ai[1] = 1;
                            Projectile.NewProjectile(proj.GetSource_FromThis(), proj.Center, Vector2.Normalize(Main.MouseWorld - proj.Center) * 32, ModContent.ProjectileType<KluexPlasmaCrystal>(), Player.GetWeaponDamage(sourceItem), 6, Player.whoAmI, plasmaBallCount);

                        }
                    }
                }
            }
        }
        public override float Factor
        {
            get
            {
                return plasmaBallCount < 0 ? Projectile.ai[0] / UpgradeValue(15, 12, 9) : MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(60f, 48f, 36f), 0, 1);
            }
        }
        public override bool Charged => Projectile.ai[1] == 0 ? plasmaBallCount == 0 && base.Charged : base.Charged;
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            frame = texture.Frame(20, 3, plasmaBallCount < 0 ? (19 - (int)(Factor * 3)) : (Factor < 1 ? (int)(Factor * 12) : (int)(IllusionBoundModSystem.ModTime / 4) % 5 + 12), UpgradeValue(0, 1, 2));
            origin = new Vector2(29, 63) * .5f;
            scale = 2;
        }
    }
    public class KluexPlasmaBall : StarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("克鲁西斯等离子体球");
        }
        Projectile projectile => Projectile;
        public override void SetDefaults()
        {
            projectile.DamageType = DamageClass.Magic;
            projectile.timeLeft = 11;
            projectile.friendly = false;
            projectile.penetrate = -1;
            projectile.height = 34;
            projectile.width = 36;
            projectile.light = 1f;
        }

        public override void AI()
        {
            projectile.rotation = (Main.MouseWorld - projectile.Center).ToRotation();
            if (projectile.ai[1] == 0) projectile.timeLeft = 11;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (projectile.ai[1] == 0)//projectile.timeLeft > 10 || 
                Main.EntitySpriteDraw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, TextureAssets.Projectile[projectile.type].Value.Frame(1, 8, 0, (int)IllusionBoundModSystem.ModTime / 2 % 8), Color.White, projectile.rotation, TextureAssets.Projectile[projectile.type].Value.Size() * .5f * new Vector2(1, 0.125f), 2f, 0, 0);
            else
                Main.EntitySpriteDraw(IllusionBoundMod.GetTexture(Texture.Replace("KluexPlasmaBall", "KluexBall"), false), projectile.Center - Main.screenPosition, new Rectangle(0, (10 - projectile.timeLeft) / 2 * 78, 78, 78), Color.White, 0, new Vector2(39), 1f, 0, 0);

            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class KluexPlasmaCrystal : StarboundWeaponProjectile
    {
        Projectile projectile => Projectile;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            DisplayName.SetDefault("克鲁西斯能量水晶");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.light = 1f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
            //if (projectile.timeLeft == 299) projectile.rotation = projectile.velocity.ToRotation();
            for (int k = 1; k < projectile.oldPos.Length; k++)
            {
                Main.spriteBatch.Draw(projectileTexture, projectile.oldPos[k] - Main.screenPosition + new Vector2(8, 8), projectileTexture.Frame(1, 4, 0, ((int)IllusionBoundMod.ModTime / 2 + k) % 4, 0), new Color(255 - 28 * k, 0, 0, 0), projectile.rotation, new Vector2(14, 8), 2f - 0.2f * k, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(projectileTexture, projectile.Center - Main.screenPosition, projectileTexture.Frame(1, 4, 0, (int)IllusionBoundMod.ModTime / 2 % 4, 0), Color.White, projectile.rotation, new Vector2(14, 8), 2f, SpriteEffects.None, 0f);
            return false;
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
            for (int i = 0; i < 15; i++)
            {
                base.Kill(timeLeft);
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
                    MyDustId.RedBubble, 0, 0, 100, Color.White, 1.5f);
                d.noGravity = true;
                d.velocity *= 2;
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            }
            //SoundEngine.PlaySound(SoundID.Item9, projectile.position);
            SoundEngine.PlaySound(SoundID.Item27, projectile.Center);
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(24, 3600);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class KluexZone : StarboundWeaponProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("克鲁西斯与狂暴的境界");
        }
        Projectile projectile => Projectile;

        public override void SetDefaults()
        {
            projectile.width = 104;
            projectile.height = 104;
            projectile.scale = 1f;
            projectile.DamageType = DamageClass.Magic;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.light = 0.5f;
            projectile.aiStyle = -1;
            //projectile.alpha = 255;
            projectile.hide = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var frame = (int)IllusionBoundModSystem.ModTime / 4 % 12 + 4;
            if (projectile.timeLeft > 584) frame = (600 - projectile.timeLeft) / 4;
            if (projectile.timeLeft < 16) frame = (16 - projectile.timeLeft) / 4 + 15;

            Main.EntitySpriteDraw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, TextureAssets.Projectile[projectile.type].Value.Frame(4, 5, frame % 4, frame / 4), Color.White * .5f, 0, new Vector2(52), ((projectile.ai[0] - 1) / 2f).Lerp(1, 2) * 2, 0, 0);
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Main.instance.DrawCacheProjsOverWiresUI.Add(index);
        }
        //public float t;
        //public int n = 0;
        public override void AI()
        {
            //t += 0.5f;
            //if (t == 15 && n < 24)
            //{
            //    t = 3;
            //    n++;
            //}
            float distanceMax = 102f * ((projectile.ai[0] - 1) / 2f).Lerp(1, 2);
            foreach (Player player in Main.player)
            {
                if (Vector2.Distance(player.Center, projectile.Center) <= distanceMax)
                {
                    player.AddBuff(Mod.Find<ModBuff>("KluexRage" + (int)MathHelper.Clamp(projectile.ai[0], 1, 3)).Type, 60);
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
        }
    }
    public class KluexRage1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("克鲁西斯狂暴Ⅰ");
            Description.SetDefault("野蛮。");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.arrowDamage += 0.5f;
            player.bulletDamage += 0.5f;
            player.rocketDamage += 0.5f;
        }
    }
    public class KluexRage2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("克鲁西斯狂暴Ⅱ");
            Description.SetDefault("太野蛮了。");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 1f;
            player.arrowDamage += 1f;
            player.bulletDamage += 1f;
            player.rocketDamage += 1f;
        }
    }
    public class KluexRage3 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("克鲁西斯狂暴Ⅲ");
            Description.SetDefault("野蛮至极！");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 1.5f;
            player.arrowDamage += 1.5f;
            player.bulletDamage += 1.5f;
            player.rocketDamage += 1.5f;
        }
    }
}