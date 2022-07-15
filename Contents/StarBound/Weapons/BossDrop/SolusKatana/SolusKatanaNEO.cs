//using Terraria.ModLoader;
//using Terraria.ID;
//using Terraria;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;
//using System;
//using Terraria.DataStructures;

//namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.SolusKatana
//{
//    public class SolusKatanaNEO : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Tooltip.SetDefault("二次强化的日炎刀，是由阿斯拉诺克斯制造的吗？\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("日炎刀NEO");
//        }
//        private int time;
//        public override void SetDefaults()
//        {
//            item.damage = 500;
//            item.DamageType = DamageClass.Melee;
//            item.width = 40;
//            item.width = 40;
//            item.rare = MyRareID.Tier3;
//            item.useTime = 10;
//            item.useAnimation = 10;
//            item.knockBack = 8;
//            item.useStyle = 1;
//            item.autoReuse = true;
//            item.value = 999000000;
//            item.shootSpeed = 10f;
//            item.shoot = ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>();
//        }
//        Item item => Item;

//        private int Time = 0;
//        public override void MeleeEffects(Player player, Rectangle hitbox)
//        {
//            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, MyDustId.Fire, 0, 0, 100, Color.White, 1.0f);
//        }
//        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                Vector2 vec = Main.MouseWorld - player.Center;
//                vec = Vector2.Normalize(vec);
//                for (float i = -MathHelper.Pi / 12; i <= MathHelper.Pi / 12; i += MathHelper.Pi / 48)
//                {
//                    Vector2 finalVec = (vec.ToRotation() + i).ToRotationVector2() * 72f;
//                    Projectile.NewProjectile(source, position, finalVec, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>(), damage, knockback, player.whoAmI);
//                }
//            }
//            return false;
//        }
//        public override void UseStyle(Player player, Rectangle rectangle)
//        {
//            //if (item.noUseGraphic || !item.melee || item.damage == 0 || item.useStyle != 1)
//            //{
//            //    return;
//            //}
//            //ShaderSwooshEffectPlayer ssep = player.GetModPlayer<ShaderSwooshEffectPlayer>();
//            //if (player.itemAnimation == player.itemAnimationMax - 1)
//            //{
//            //    ssep.playerOldPos = new Vector2[player.itemAnimationMax - 1];
//            //}
//            //ssep.playerOldPos[player.itemAnimationMax - player.itemAnimation - 1] = player.Center;
//            //if (player.itemAnimation == 1)
//            //{
//            //    ssep.NewSwoosh(1 / 12f, item, ShaderSwooshEffectPlayer.ShaderSwooshStyle.LightBlade);
//            //}
//            if (player.altFunctionUse != 2)
//            {
//                time++;
//                if (time == 20)
//                {
//                    Vector2 vec = Main.MouseWorld - player.Center;
//                    vec = Vector2.Normalize(vec);
//                    for (float i = -MathHelper.Pi / 12; i <= MathHelper.Pi / 12; i += MathHelper.Pi / 12)
//                    {
//                        Vector2 finalVec = (vec.ToRotation() + i).ToRotationVector2() * 72f;
//                        Projectile.NewProjectile(player.GetSource_ItemUse(item), player.position, finalVec, ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>(), player.GetWeaponDamage(item), 8, player.whoAmI);
//                    }
//                    time = 0;
//                }
//            }
//        }
//        public override bool AltFunctionUse(Player player)
//        {
//            return true;
//        }
//        public override bool CanUseItem(Player player)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                item.shoot = ModContent.ProjectileType<Projectiles.SolarEnergySword.SolarEnergySword>();
//                item.shootSpeed = 10f;
//                item.mana = 50;
//                item.useTime = 30;
//                item.useAnimation = 30;
//            }
//            else
//            {
//                item.useTime = 10;
//                item.mana = 0;
//                item.shoot = 0;
//                item.useAnimation = 10;
//            }
//            return base.CanUseItem(player);
//        }
//        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
//        {
//            target.AddBuff(BuffID.OnFire, 450);
//            target.AddBuff(BuffID.Daybreak, 450);
//        }
//        public override void HoldItem(Player player)
//        {
//            Time++;
//            if (Time > 360)
//                Time = 0;
//            int n = 8;
//            int d = MyDustId.Fire;
//            float W = 3.1415926f / 180 * Time;
//            float X = (float)Math.Cos(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            float Y = (float)Math.Sin(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) + n) * 25;
//            for (float rad = 0; rad < MathHelper.TwoPi; rad += MathHelper.PiOver2)
//            {
//                Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(X, Y).RotatedBy(rad), d, new Vector2(0f, 0f), 0, Color.White, 2f);
//                dust.noGravity = true;
//            }

//        }
//    }
//}