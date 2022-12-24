//using System;
//using Terraria.ModLoader;
//using Terraria.ID;
//using VirtualDream.Utils;
//using Terraria;
//using Microsoft.Xna.Framework;
//using Terraria.DataStructures;

//namespace VirtualDream.Items.Weapons.BossDrop.ErchiusEyeS
//{
//    public class ErchiusEyeDL : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("能源之眼DL");
//            Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n死亡激光(DeathLaser)\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//        }
//        private static float Time = 0;
//        Item item => Item;

//        public override void SetDefaults()
//        {
//            item.noMelee = true;
//            item.damage = 225;
//            item.DamageType = DamageClass.Magic;
//            item.channel = true; //Channel so that you can held the weapon [Important]
//            item.mana = 0;
//            item.rare = MyRareID.Tier3;
//            item.width = 28;
//            item.height = 30;
//            item.UseSound = SoundID.Item13;
//            item.useStyle = 5;
//            item.shootSpeed = 14f;
//            item.value = Item.sellPrice(silver: 3);
//        }
//        public override bool AltFunctionUse(Player player)
//        {
//            return true;
//        }
//        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                Vector2 vec = Main.MouseWorld - player.Center;
//                vec = Vector2.Normalize(vec);
//                for (float i = -3; i <= 3; i += 1)
//                {
//                    Vector2 finalVec = (vec.ToRotation()).ToRotationVector2() * 25f;
//                    Projectile.NewProjectile(source, position, finalVec, ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusCrystal>(), damage, knockBack, player.whoAmI);
//                }
//            }
//            else
//            {
//                Projectile.NewProjectile(source, position, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusLaserEX>(), damage * 3 / 2, knockBack, player.whoAmI);
//            }
//            return false;
//        }
//        public override void HoldItem(Player player)
//        {
//            Time += MathHelper.Pi / 60;
//            if (Time >= MathHelper.TwoPi)
//            {
//                Time = 0;
//            }
//            Dust dust = Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 256), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
//            Dust dust1 = Dust.NewDustPerfect(player.Center - new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 256), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
//            Dust dust2 = Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
//            Dust dust3 = Dust.NewDustPerfect(player.Center - new Vector2((float)Math.Cos(Time) * 256, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
//            Dust dust4 = Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(Time) * 16, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
//            Dust dust5 = Dust.NewDustPerfect(player.Center - new Vector2((float)Math.Cos(Time) * 16, (float)Math.Sin(Time) * 64), MyDustId.PinkBubble, new Vector2(0f, 0f), 0, Color.White, 1f);
//            dust.noGravity = true;
//            dust1.noGravity = true;
//            dust2.noGravity = true;
//            dust3.noGravity = true;
//            dust4.noGravity = true;
//            dust5.noGravity = true;
//        }
//        public override bool CanUseItem(Player player)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                item.damage = 225;
//                item.useTime = 19;
//                item.useAnimation = 19;
//                item.shoot = ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusCrystal>();
//            }
//            else
//            {
//                item.damage = 225;
//                item.useTime = 13;
//                item.useAnimation = 13;
//                item.shoot = ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusLaserEX>();
//            }
//            return base.CanUseItem(player);
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//}
