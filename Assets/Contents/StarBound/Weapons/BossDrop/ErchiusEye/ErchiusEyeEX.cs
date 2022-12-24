//using Terraria.ModLoader;
//using Terraria.ID;
//using VirtualDream.Utils;
//using Terraria;
//using Microsoft.Xna.Framework;
//using Terraria.DataStructures;

//namespace VirtualDream.Items.Weapons.BossDrop.ErchiusEyeS
//{
//	public class ErchiusEyeEX : ModItem
//	{
//		public override void SetStaticDefaults() {
//			DisplayName.SetDefault("能源之眼EX");
//			Tooltip.SetDefault("能源恐怖的眼睛，专注于你的敌人。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//		}
//		Item item => Item;

//		public override void SetDefaults() {
//			item.noMelee = true;
//			item.damage = 75;
//			item.DamageType = DamageClass.Magic;
//			item.channel = true; //Channel so that you can held the weapon [Important]
//			item.mana = 0;
//			item.rare = MyRareID.Tier2;
//			item.width = 28;
//			item.height = 30;
//			item.UseSound = SoundID.Item13;
//			item.useStyle = 5;
//			item.shootSpeed = 14f;
//			item.value = Item.sellPrice(silver: 3);
//		}
//		public override bool AltFunctionUse(Player player)
//		{
//			return true;
//		}
//        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
//		{
//			if (player.altFunctionUse == 2)
//			{
//				Vector2 vec = Main.MouseWorld - player.Center;
//				vec = Vector2.Normalize(vec);
//				for (float i = -MathHelper.Pi / 12; i <= MathHelper.Pi / 12; i += MathHelper.Pi / 24)
//				{
//					Vector2 finalVec = (vec.ToRotation()).ToRotationVector2() * 25f;
//					Projectile.NewProjectile(source, position, finalVec, ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusCrystal>(), damage, knockBack, player.whoAmI);
//				}
//			}
//			else
//			{
//				Projectile.NewProjectile(source, position, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusLaserEX>(), damage * 3 / 2, knockBack, player.whoAmI);
//			}
//			return false;
//		}
//		public override bool CanUseItem(Player player)
//        {
//            if (player.altFunctionUse == 2)
//            {
//				item.damage = 75;
//				item.useTime = 24;
//				item.useAnimation = 24;
//				item.shoot = ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusCrystal>();
//            }
//            else
//            {
//				item.damage = 75;
//				item.useTime = 16;
//				item.useAnimation = 16;
//				item.shoot = ModContent.ProjectileType<Projectiles.ErchiusCrystal.ErchiusLaserEX>();
//			}
//            return base.CanUseItem(player);
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//}
