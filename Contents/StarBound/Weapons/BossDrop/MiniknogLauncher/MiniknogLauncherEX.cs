//using Terraria;
//using Terraria.ModLoader;
//using VirtualDream.Utils;
//using Terraria.ID;
//using Terraria.DataStructures;

//using Microsoft.Xna.Framework;

//namespace VirtualDream.Items.Weapons.BossDrop.MiniknogLauncherS
//{
//	// 保证类名跟文件名一致，这样也方便查找
//	public class MiniknogLauncherEX : ModItem
//	{
//		// 设置物品名字，描述的地方
//		public override void SetStaticDefaults()
//		{
//			base.SetStaticDefaults();

//			// 这里可以写中文了ヾ(@^▽^@)ノ
//			DisplayName.SetDefault("科技发展部发射器EX");

//			// 物品的描述，加入换行符 '\n' 可以多行显示哦
//			Tooltip.SetDefault("微型导弹发射器，由科技发展部的顶级科学家开发。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//		}
//		private int Time;
//		private int Dam;
//		Item item => Item;

//		// 最最最重要的物品基本属性部分
//		public override void SetDefaults()
//		{
//			item.damage = 150;
//			item.knockBack = 0.25f;
//			item.rare = MyRareID.Tier2;
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
//			item.shoot = ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogLauncherEX>();
//			item.shootSpeed = 1f;
//			item.channel = true;
//			item.autoReuse = true;
//			item.useTime = 16;
//			item.useAnimation = 16;
//		}
//		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//		{
//			Projectile.NewProjectile(source, player.Center, new Vector2(0, 0), type, 0, 0, player.whoAmI);
//			return false;
//		}
//		public override bool AltFunctionUse(Player player)
//		{
//			return true;
//		}
//		public override void UseStyle(Player player,Rectangle rectangle)
//		{
//			Vector2 vec = Main.MouseWorld - player.Center;
//			vec = Vector2.Normalize(vec) * 16;
//			if (player.altFunctionUse == 2)
//			{
//				Time++;
//				if(Time >= 16)
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
//					if(Time >= 64)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center,vec, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketE>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 18).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 9).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 6).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					else if (Time >= 48)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketB>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36 * 5).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					else if (Time >= 32)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketR>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 12).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					else if (Time >= 16)
//					{
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() - MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(),Dam,5f, player.whoAmI);
//						Projectile.NewProjectile(player.GetSource_ItemUse(item),player.Center, (vec.ToRotation() + MathHelper.Pi / 36).ToRotationVector2() * 16, ModContent.ProjectileType<Projectiles.MiniknogLauncher.MiniknogRocketP>(), Dam, 5f, player.whoAmI);
//					}
//					Time = 0;
//				}
//				if (Time % 16 == 0)
//				{
//					var rect = player.Hitbox;
//					rect.Offset(0, -64);
//					if (Time == 16)
//					{
//						Main.combatText[CombatText.NewText(rect, Color.Cyan, "穿墙导弹填充完毕", true)].velocity.Y = -1;
//					}
//					if (Time == 32)
//					{
//						Main.combatText[CombatText.NewText(rect, Color.Cyan, "折射导弹填充完毕", true)].velocity.Y = -1;
//					}
//					if (Time == 48)
//					{
//						Main.combatText[CombatText.NewText(rect, Color.Cyan, "Buff导弹填充完毕", true)].velocity.Y = -1;
//					}
//					if (Time == 64)
//					{
//						Main.combatText[CombatText.NewText(rect, Color.Cyan, "爆炸导弹填充完毕", true)].velocity.Y = -1;
//						rect.Offset(0, -32);
//						Main.combatText[CombatText.NewText(rect, Color.Blue, "所有导弹填充完毕！", true)].velocity.Y = -2;
//					}
//				}
//			}
//		}
//	}
//}