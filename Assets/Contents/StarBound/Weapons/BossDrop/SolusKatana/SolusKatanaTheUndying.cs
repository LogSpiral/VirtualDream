//using Terraria.ModLoader;
//using Terraria;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;
//using Terraria.DataStructures;
//using System;

//namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana
//{
//    public class SolusKatanaTheUndying : ModItem
//    {
//        public override void SetStaticDefaults()
//        {

//            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(2, 6));
//            Tooltip.SetDefault("这么鬼畜(划掉)强大的日炎刀，肯定不是由阿斯拉诺克斯制造\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("日炎刀TheUndying");
//        }

//        public override void SetDefaults()
//        {
//            item.DamageType = DamageClass.Melee;
//            item.crit = 100;
//            item.width = 40;
//            item.width = 40;
//            item.useTime = 6;
//            item.rare = MyRareID.Tier3;
//            item.useAnimation = 6;
//            item.knockBack = 8;
//            item.useStyle = 1;
//            item.autoReuse = true;
//            item.value = 999000000;
//            item.noUseGraphic = true;
//            item.noMelee = true;
//            item.shootSpeed = 10f;
//            item.damage = 1000;
//            item.channel = true;
//            item.shoot = ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergyBlade>();

//        }
//        private static int Time = 0;
//        private static int Time1 = 0;

//        public override void MeleeEffects(Player player, Rectangle hitbox)
//        {
//            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, MyDustId.Fire, 0, 0, 100, Color.White, 1.0f);
//        }
//        public override void UseStyle(Player player, Rectangle rectangle)
//        {
//            Time++;
//            if (Time > 360)
//                Time = 0;
//            Time1++;
//            if (Time1 > 6)
//            {
//                Time1 = 0;
//                int n = 8;
//                float W = 3.1415926f / 180 * Time;
//                float X1 = (float)Math.Cos(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y1 = (float)Math.Sin(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X2 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y2 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X3 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y3 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X4 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y4 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X5 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y5 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X6 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y6 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X7 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y7 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float X8 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                float Y8 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//                Vector2 vec1 = Main.MouseWorld - (player.Center + new Vector2(X1, Y1));
//                vec1 = Vector2.Normalize(vec1);
//                Vector2 vec2 = Main.MouseWorld - (player.Center + new Vector2(X2, Y2));
//                vec2 = Vector2.Normalize(vec2);
//                Vector2 vec3 = Main.MouseWorld - (player.Center + new Vector2(X3, Y3));
//                vec3 = Vector2.Normalize(vec3);
//                Vector2 vec4 = Main.MouseWorld - (player.Center + new Vector2(X4, Y4));
//                vec4 = Vector2.Normalize(vec4);
//                Vector2 vec5 = Main.MouseWorld - (player.Center + new Vector2(X5, Y5));
//                vec5 = Vector2.Normalize(vec5);
//                Vector2 vec6 = Main.MouseWorld - (player.Center + new Vector2(X6, Y6));
//                vec6 = Vector2.Normalize(vec6);
//                Vector2 vec7 = Main.MouseWorld - (player.Center + new Vector2(X7, Y7));
//                vec7 = Vector2.Normalize(vec7);
//                Vector2 vec8 = Main.MouseWorld - (player.Center + new Vector2(X8, Y8));
//                vec8 = Vector2.Normalize(vec8);

//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X1, Y1), vec1 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X2, Y2), vec2 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X3, Y3), vec3 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X4, Y4), vec4 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X5, Y5), vec5 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X6, Y6), vec6 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X7, Y7), vec7 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + new Vector2(X8, Y8), vec8 * 16, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySworda>(), item.damage * 5 / 2, item.knockBack, player.whoAmI);
//            }
//        }
//        public override void HoldItem(Player player)
//        {
//            Time++;
//            if (Time > 360)
//                Time = 0;
//            int n = 8;
//            int d = MyDustId.Fire;
//            float W = 3.1415926f / 180 * Time;
//            float X1 = (float)Math.Cos(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y1 = (float)Math.Sin(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X2 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y2 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X3 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y3 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X4 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y4 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X5 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y5 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X6 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y6 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X7 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y7 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float X8 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y8 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float s = 2f;
//            for (int i = 0; i < 3; i++)
//            {
//                Dust dust1 = Dust.NewDustPerfect(player.Center + new Vector2(X1, Y1), d, new Vector2(0f, 0f), 0, Color.White, s);
//                Dust dust2 = Dust.NewDustPerfect(player.Center + new Vector2(X2, Y2), d, new Vector2(0, 0), 0, Color.White, s);
//                Dust dust3 = Dust.NewDustPerfect(player.Center + new Vector2(X3, Y3), d, new Vector2(0f, 0f), 0, Color.White, s);
//                Dust dust4 = Dust.NewDustPerfect(player.Center + new Vector2(X4, Y4), d, new Vector2(0, 0), 0, Color.White, s);
//                Dust dust5 = Dust.NewDustPerfect(player.Center + new Vector2(X5, Y5), d, new Vector2(0f, 0f), 0, Color.White, s);
//                Dust dust6 = Dust.NewDustPerfect(player.Center + new Vector2(X6, Y6), d, new Vector2(0f, 0f), 0, Color.White, s);
//                Dust dust7 = Dust.NewDustPerfect(player.Center + new Vector2(X7, Y7), d, new Vector2(0, 0), 0, Color.White, s);
//                Dust dust8 = Dust.NewDustPerfect(player.Center + new Vector2(X8, Y8), d, new Vector2(0f, 0f), 0, Color.White, s);
//                float v = 2f;
//                dust1.noGravity = true;
//                dust1.velocity *= v;
//                dust2.noGravity = true;
//                dust2.velocity *= v;
//                dust3.noGravity = true;
//                dust3.velocity *= v;
//                dust4.noGravity = true;
//                dust4.velocity *= v;
//                dust5.noGravity = true;
//                dust5.velocity *= v;
//                dust6.noGravity = true;
//                dust6.velocity *= v;
//                dust7.noGravity = true;
//                dust7.velocity *= v;
//                dust8.noGravity = true;
//                dust8.velocity *= v;
//            }
//        }
//        Item item => Item;
//        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
//        {
//            Vector2 vec = Main.MouseWorld - player.Center;
//            vec = Vector2.Normalize(vec);
//            for (int i = -10; i <= 10; i++)
//            {
//                Vector2 finalVec = vec * 64f + new Vector2(-vec.Y, vec.X) * 2.095f * i;
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), position, finalVec, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>(), damage, knockBack, player.whoAmI);
//                Projectile.NewProjectile(player.GetSource_ItemUse(item), position, finalVec, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergyBlade>(), damage, knockBack, player.whoAmI);
//            }
//            return true;
//        }
//    }
//}