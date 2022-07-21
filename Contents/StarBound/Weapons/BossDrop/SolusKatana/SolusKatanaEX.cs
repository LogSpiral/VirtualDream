//using Terraria.ModLoader;
//using Terraria.ID;
//using Terraria;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;
//using Terraria.DataStructures;

//namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana
//{
//    public class SolusKatanaEX : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Tooltip.SetDefault("日光注入剑中，由阿斯拉诺克斯制造\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("日炎刀EX");
//        }
//        //public override void UseStyle(Player player)
//        //{
//        //    if (item.noUseGraphic || !item.melee || item.damage == 0 || item.useStyle != 1)
//        //    {
//        //        return;
//        //    }
//        //    ShaderSwooshEffectPlayer ssep = player.GetModPlayer<ShaderSwooshEffectPlayer>();
//        //    if (player.itemAnimation == player.itemAnimationMax - 1)
//        //    {
//        //        ssep.playerOldPos = new Vector2[player.itemAnimationMax - 1];
//        //    }
//        //    ssep.playerOldPos[player.itemAnimationMax - player.itemAnimation - 1] = player.Center;
//        //    if (player.itemAnimation == 1)
//        //    {
//        //        ssep.NewSwoosh(1 / 12f, item, ShaderSwooshEffectPlayer.ShaderSwooshStyle.LightBlade);
//        //    }
//        //}
//        public override void SetDefaults()
//        {
//            item.damage = 225;
//            item.DamageType = DamageClass.Melee;
//            item.width = 40;
//            item.rare = MyRareID.Tier2;
//            item.width = 40;
//            item.useTime = 13;
//            item.useAnimation = 13;
//            item.knockBack = 8;
//            item.useStyle = 1;
//            item.autoReuse = true;
//            item.value = 666000000;
//        }

//        public override void MeleeEffects(Player player, Rectangle hitbox)
//        {
//            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, MyDustId.Fire, 0, 0, 100, Color.White, 1.0f);
//        }
//        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//        {
//            Vector2 vec = Main.MouseWorld - player.Center;
//            vec = Vector2.Normalize(vec);
//            for (float i = -MathHelper.Pi / 12; i <= MathHelper.Pi / 12; i += MathHelper.Pi / 24)
//            {
//                Vector2 finalVec = (vec.ToRotation() + i).ToRotationVector2() * 72f;
//                Projectile.NewProjectile(source, position, finalVec, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>(), damage, knockback, player.whoAmI);
//            }
//            return false;
//        }
//        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
//        {
//            target.AddBuff(BuffID.OnFire, 300);
//            target.AddBuff(BuffID.Daybreak, 300);
//        }
//        Item item => Item;

//        public override bool CanUseItem(Player player)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                item.shoot = ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>();
//                item.shootSpeed = 10f;
//                item.mana = 40;
//                item.useTime = 39;
//                item.useAnimation = 39;
//            }
//            else
//            {
//                item.useTime = 13;
//                item.mana = 0;
//                item.shoot = 0;
//                item.useAnimation = 13;
//            }
//            return base.CanUseItem(player);
//        }
//        public override bool AltFunctionUse(Player player)
//        {
//            return true;
//        }

//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//        public override void AddRecipes()
//        {
//            Recipe recipe1 = CreateRecipe();
//            recipe1.AddIngredient<SolusKatana>();
//            recipe1.AddIngredient<Materials.AncientEssence>(3000);
//            recipe1.AddTile(TileID.LunarCraftingStation);
//            recipe1.SetResult(this);
//            recipe1.AddRecipe();
//        }
//    }
//}