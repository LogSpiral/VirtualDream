//using System;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;
//using Terraria.DataStructures;

//namespace VirtualDream.Items.Weapons.BossDrop.MiniknogLauncherS
//{
//	// 保证类名跟文件名一致，这样也方便查找
//	public class MiniknogLauncherLT : ModItem
//	{
//		// 设置物品名字，描述的地方
//		public override void SetStaticDefaults()
//		{
//			base.SetStaticDefaults();

//			// 这里可以写中文了ヾ(@^▽^@)ノ
//			DisplayName.SetDefault("科技发展部发射器LT");

//			// 物品的描述，加入换行符 '\n' 可以多行显示哦
//			Tooltip.SetDefault("微型导弹发射器，由科技发展部的顶级科学家开发。\n极限科技(LimitTechnology)\n你以为是大猿人用tr的奇妙科技造的？其实是由河童工程师改造的(\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//		}
//		private int Time;
//		private float TimeD;
//		private int Dam;

//		// 最最最重要的物品基本属性部分
//		public override void SetDefaults()
//		{
//			item.damage = 300;
//			item.knockBack = 0.25f;
//			item.rare = MyRareID.Tier3;
//			item.useStyle = 5;
//			item.useAmmo = AmmoID.Rocket;
//			item.DamageType = DamageClass.Ranged;
//			item.value = Item.sellPrice(0, 1, 0, 0);
//			item.width = 24;
//			item.height = 24;
//			item.noUseGraphic = true;
//			item.scale = 1f;
//			item.maxStack = 1;
//			item.noMelee = true;
//			item.shoot = ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogLauncherLT>();
//			item.shootSpeed = 1f;
//			item.channel = true;
//			item.autoReuse = true;
//			item.useTime = 8;
//			item.useAnimation = 8;
//		}
//		Item item => Item;

//		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//		{
//			Projectile.NewProjectile(source, player.Center, new Vector2(0, 0), type, 0, 0, player.whoAmI);
//			return false;
//		}
//		public override bool AltFunctionUse(Player player)
//		{
//			return true;
//		}
//		public override void UseStyle(Player player, Rectangle rectangle)
//		{
//			Vector2 vec = Main.MouseWorld - player.Center;
//			vec = Vector2.Normalize(vec) * 16;
//			if (player.altFunctionUse == 2)
//			{
//				Time++;
//				if(Time >= 8)
//				{
//					Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketG>(), Dam / 4 * 3, 5f, player.whoAmI);
//					Time = 0;
//				}
//			}
//			else
//			{
//			    if (player.channel)
//		    	{
//					Time++;
//				}
//				else
//				{
//					if(Time >= 32)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center,vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketE>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					else if (Time >= 24)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					else if (Time >= 16)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					else if (Time >= 8)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(),Dam,5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					Time = 0;
//				}
//				if(Time == 8)
//				{
//					Main.NewText("穿墙导弹填充完毕");
//				}
//				if (Time == 16)
//				{
//					Main.NewText("折射导弹填充完毕");
//				}
//				if (Time == 24)
//				{
//					Main.NewText("Buff导弹填充完毕");
//				}
//				if (Time == 32)
//				{
//					Main.NewText("爆炸导弹填充完毕");
//					Main.NewText("[c/00FFFF:导弹填充完毕！]");
//				}
//			}
//		}
//		public override void HoldItem(Player player)
//		{
//			float a = 256f;
//			TimeD += MathHelper.Pi / 180;
//			float X1 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 0) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float X2 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 1) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float X3 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 2) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float X4 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 3) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float X5 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 4) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float X6 = (float)Math.Cos(TimeD + MathHelper.TwoPi / 6 * 5) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float Y1 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 0) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float Y2 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 1) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float Y3 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 2) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float Y4 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 3) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float Y5 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 4) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			float Y6 = (float)Math.Sin(TimeD + MathHelper.TwoPi / 6 * 5) * (float)(Math.Sin(6 * TimeD) + 0.5f);
//			Dust dust1 = Dust.NewDustPerfect(player.Center + new Vector2(X1, Y1) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
//			Dust dust2 = Dust.NewDustPerfect(player.Center + new Vector2(X2, Y2) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
//			Dust dust3 = Dust.NewDustPerfect(player.Center + new Vector2(X3, Y3) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
//			Dust dust4 = Dust.NewDustPerfect(player.Center + new Vector2(X4, Y4) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
//			Dust dust5 = Dust.NewDustPerfect(player.Center + new Vector2(X5, Y5) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
//			Dust dust6 = Dust.NewDustPerfect(player.Center + new Vector2(X6, Y6) * a, MyDustId.CyanBubble, new Vector2(0, 0), 0, Color.White, 1f);
//			dust1.noGravity = true;
//			dust2.noGravity = true;
//			dust3.noGravity = true;
//			dust4.noGravity = true;
//			dust5.noGravity = true;
//			dust6.noGravity = true;
//		}
//	}
//}