//using System;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;

//namespace VirtualDream.Items.Weapons.BossDrop.KluexStaffS
//{
//    public class KluexStaffPH : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Tooltip.SetDefault("这根强大的法杖可以支持挥动着它的战士\n 你知道......什么叫自机狙吗？！(已经被万恶的阿汪削弱(?)了)\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("克鲁西斯法杖PH");
//            Item.staff[item.type] = true;
//        }
//        private static float Time = 0;
//        private static float X = 0;
//        private static float Y = 0;
//        public int Time2;
//        public int Timex;
//        public int TimeR;
//        Item item => Item;

//        public override void SetDefaults()
//        {
//            item.damage = 800;
//            item.crit = 50;
//            item.DamageType = DamageClass.Magic;
//            item.width = 40;
//            item.height = 40;
//            item.useTime = 12;
//            item.useAnimation = 12;
//            item.rare = MyRareID.Tier3;
//            item.knockBack = 6;
//            item.noUseGraphic = true;
//            item.mana = 40;
//            item.useStyle = 5;
//            item.autoReuse = true;
//            item.noMelee = true;
//            item.shoot = ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexStaffPHA>();
//            item.channel = true;
//            item.shootSpeed = 1f;
//            item.UseSound = SoundID.Item13;
//        }
//        public override void HoldItem(Player player)
//        {
//            Time += 0.05f;
//            X = (float)Math.Cos(Time) * 5;
//            Y = (float)Math.Sin(Time) * 3;
//            Dust dust1 = Dust.NewDustPerfect(Main.MouseWorld + new Vector2(X, Y) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust2 = Dust.NewDustPerfect(Main.MouseWorld - new Vector2(X, Y) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust3 = Dust.NewDustPerfect(Main.MouseWorld + new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust4 = Dust.NewDustPerfect(Main.MouseWorld - new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust5 = Dust.NewDustPerfect(Main.MouseWorld, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust6 = Dust.NewDustPerfect(player.Center + new Vector2(X, Y).RotatedBy(Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust7 = Dust.NewDustPerfect(player.Center - new Vector2(X, Y).RotatedBy(Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust8 = Dust.NewDustPerfect(player.Center + new Vector2(X, Y).RotatedBy(MathHelper.PiOver2 + Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust9 = Dust.NewDustPerfect(player.Center - new Vector2(X, Y).RotatedBy(MathHelper.PiOver2 + Time) * 64, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            dust1.noGravity = true;
//            dust2.noGravity = true;
//            dust3.noGravity = true;
//            dust4.noGravity = true;
//            dust5.noGravity = true;
//            dust6.noGravity = true;
//            dust7.noGravity = true;
//            dust8.noGravity = true;
//            dust9.noGravity = true;
//        }
//        public override void UseStyle(Player player,Rectangle rectangle)
//        {
//            if (Main.mouseRight)
//            {
//                TimeR++;
//            }
//            else
//            {
//                if (TimeR >= 30)
//                {
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyZonePH>(), 0, 0, player.whoAmI);
//                }
//                TimeR = 0;
//            }
//            if (player.channel)
//            {
//                Time2++;
//                Timex++;
//                if ( Time2 >= 12 && Timex >= 30)
//                {
//                    Time2 = 0;
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld + new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld - new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld + new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld - new Vector2(X, Y).RotatedBy(MathHelper.PiOver2) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                }
//            }
//            else
//            {
//                Timex = 0;
//                Time2 = 0;
//            }
//        }
//        public override bool AltFunctionUse(Player player)
//        {
//            return true;
//        }
//    }
//}