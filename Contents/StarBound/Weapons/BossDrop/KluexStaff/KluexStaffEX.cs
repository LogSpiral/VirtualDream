//using System;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;

//namespace VirtualDream.Items.Weapons.BossDrop.KluexStaffS
//{
//    public class KluexStaffEX : ModItem
//    {
//        Item item => Item;

//        public override void SetStaticDefaults()
//        {
//            Tooltip.SetDefault("这根强大的法杖可以支持挥动着它的战士\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("克鲁西斯法杖EX");
//            Item.staff[item.type] = true;
//        }
//        private static float Time = 0;
//        private static float X = 0;
//        private static float Y = 0;
//        public int Time2;
//        public int Timex;
//        public int TimeR;
//        public override void SetDefaults()
//        {
//            item.damage = 500;
//            item.crit = 50;
//            item.DamageType = DamageClass.Magic;
//            item.mana = 30;
//            item.rare = MyRareID.Tier2;
//            item.width = 40;
//            item.height = 40;
//            item.useTime = 18;
//            item.useAnimation = 18;
//            item.knockBack = 6;
//            item.useStyle = 5;
//            item.noUseGraphic = true;
//            item.autoReuse = true;
//            item.noMelee = true;
//            item.shoot = ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexStaffEXA>();
//            item.channel = true;
//            item.shootSpeed = 1f;
//            item.UseSound = SoundID.Item13;
//        }
//        public override bool AltFunctionUse(Player player)
//        {
//            return true;
//        }
//        public override void HoldItem(Player player)
//        {
//            Time += MathHelper.Pi / 60;
//            X = (float)Math.Cos(Time);
//            Y = (float)Math.Sin(Time);
//            Dust dust1 = Dust.NewDustPerfect(Main.MouseWorld + new Vector2(X,Y) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust2 = Dust.NewDustPerfect(Main.MouseWorld - new Vector2(X, Y) * 32, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            Dust dust3 = Dust.NewDustPerfect(Main.MouseWorld, MyDustId.RedBubble, new Vector2(0, 0), 0, Color.White, 1f);
//            dust1.noGravity = true;
//            dust2.noGravity = true;
//            dust3.noGravity = true;
//        }
//        public override void UseStyle(Player player, Rectangle rectangle)
//        {
//            if (Main.mouseRight)
//            {
//                TimeR++;
//            }
//            else
//            {
//                if (TimeR >= 45)
//                {
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyZoneEX>(), 0, 0, player.whoAmI);
//                }
//                TimeR = 0;
//            }
//            if (player.channel)
//            {
//                Time2++;
//                Timex++;
//                if (Time2 >= 18 && Timex >= 45)
//                {
//                    Time2 = 0;
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld + new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld - new Vector2(X, Y) * 32, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20, 6, player.whoAmI);
//                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.KluexEnergyCrystal.KluexEnergyBall>(), player.GetWeaponDamage(item) / 20,6, player.whoAmI);
//                }
//            }
//            else
//            {
//                Timex = 0;
//                Time2 = 0;
//            }
//        }
//    }
//}