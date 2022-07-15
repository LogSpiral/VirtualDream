//using Terraria;
//using Terraria.ModLoader;
//using IllusionBoundMod.Utils;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using Terraria.DataStructures;

//namespace IllusionBoundMod.Items.Weapons.BossDrop.DragonheadPistolS
//{
//	// 保证类名跟文件名一致，这样也方便查找
//	public class DragonheadPistolEX : ModItem
//	{
//		// 设置物品名字，描述的地方
//		public override void SetStaticDefaults()
//		{
//			base.SetStaticDefaults();

//			// 这里可以写中文了ヾ(@^▽^@)ノ
//			DisplayName.SetDefault("龙头手枪EX");

//			// 物品的描述，加入换行符 '\n' 可以多行显示哦
//			Tooltip.SetDefault("一个有急躁脾气的手枪，这里是龙。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//		}
//		private int Time;
//		private int Time1;
//		private int Dam;
//		Item item => Item;

//		// 最最最重要的物品基本属性部分
//		public override void SetDefaults()
//		{
//			item.damage = 100;
//			item.knockBack = 0.25f;
//			item.rare = MyRareID.Tier2;
//			item.useStyle = 5;
//			item.crit = 6;
//			item.useAmmo = AmmoID.Bullet;
//			item.DamageType = DamageClass.Ranged;
//			item.value = Item.sellPrice(0, 1, 0, 0);
//			item.width = 24;
//			item.height = 24;
//			item.noUseGraphic = true;
//			item.scale = 1f;
//			item.maxStack = 1;
//			item.noMelee = true;
//			item.shoot = ModContent.ProjectileType<Projectiles.Dragonhead.DragonheadPistolEX>();
//			item.shootSpeed = 1f;
//			item.channel = true;
//			item.autoReuse = true;
//			item.useTime = 20;
//			item.useAnimation = 20;
//		}
//		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//		{
//			Vector2 vec = Main.MouseWorld - player.Center;
//			Vector2 vec1 = new Vector2(32, 0);
//			if ((player.name == "sans." || player.name == "sans" || player.name == "Sans") && Time <= 1)
//			{
//				Projectile.NewProjectile(source, player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonLaserEX>(), damage * 3 / 10, 0.5f, player.whoAmI);
//			}
//			if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
//			{
//				Projectile.NewProjectile(source, player.Center, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.Dragonhead.DragonheadPistolEX>(), 0, 0, player.whoAmI);
//			}
//			Dam = damage;
//			return false;
//		}
//		public override bool AltFunctionUse(Player player)
//		{
//			if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
//			{
//				return true;
//			}
//			else
//			{
//				return false;
//			}
//		}
//		public override void UseStyle(Player player, Rectangle rectangle)
//		{
//			Vector2 vec = Main.MouseWorld - player.Center;
//			Vector2 vec1 = new Vector2(32, 0);
//			if(Main.mouseRight)
//			{
//				Time1++;
//			}
//			else
//			{
//				Time1 = 0;
//			}
//			if (player.altFunctionUse == 2)
//			{
//				Time++;
//				if(Time >= 2 && Time1>=24)
//				{
//					if (player.name != "sans." && player.name != "sans" && player.name != "Sans")
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation()), vec1.RotatedBy(vec.ToRotation() + Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireCloudEX>(), Dam * 3 / 5, 0.5f, player.whoAmI);
//					}
//					Time = 0;
//				}
//			}
//			else
//			{
//				if(player.channel)
//				{
//					Time++;
//				}
//				else
//				{
//					if (Time >= 24 && player.name != "sans."&&player.name != "sans"&&player.name != "Sans")
//					{
//						int n = Main.rand.Next(10);
//						if (n < 3)
//						{
//							Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() + MathHelper.Pi * 3 / 4), vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
//							Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() - MathHelper.Pi * 3 / 4), vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
//						}
//						Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBall>(), Dam * 3, 5f, player.whoAmI);
//					}
//					else if (Time >= 1 && player.name != "sans." && player.name != "sans" && player.name != "Sans")
//					{
//						int n = Main.rand.Next(10);
//						if(n < 1 + Time / 12)
//						{
//							Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center + vec1.RotatedBy(vec.ToRotation() + MathHelper.Pi) / 2, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBullet>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
//						}
//						Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, vec1.RotatedBy(vec.ToRotation()), ModContent.ProjectileType<Projectiles.Dragonhead.DragonFireBullet>(), Dam * (1 + Time / 12), 0.25f, player.whoAmI);
//					}
//					Time = 0;
//				}
//			}
//		}
//	}
//}