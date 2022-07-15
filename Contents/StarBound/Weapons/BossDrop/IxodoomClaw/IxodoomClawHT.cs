//using System;
//using Terraria;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;
//using VirtualDream.Utils;

//namespace VirtualDream.Items.Weapons.BossDrop.IxodoomClawS
//{
//    public class IxodoomClawHT : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n它的刀刃上附着着高度的剧毒\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("死亡主宰爪HT");
//        }
//        Item item => Item;

//        public override void SetDefaults()
//        {
//            item.damage = 700;
//            item.crit = 100;
//            item.DamageType = DamageClass.Melee;
//            item.width = 40;
//            item.rare = MyRareID.Tier3;
//            item.height = 40;
//            item.useTime = 18;
//            item.useAnimation = 18;
//            item.knockBack = 6;
//            item.useStyle = 1;
//            item.autoReuse = true;
//        }
//        public override bool CanUseItem(Player player)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                item.useTime = 36;
//                item.useAnimation = 36;
//            }
//            else
//            {
//                item.useTime = 18;
//                item.useAnimation = 18;
//            }
//            return base.CanUseItem(player);
//        }
//        private static int Time = 0;
//        public override void HoldItem(Player player)
//        {
//            Time++;
//            if (Time > 360)
//                Time = 0;
//            //int n = 8;
//            int d = MyDustId.PinkBubble;
//            float W = (3.1415926f / 180) * Time;
//            for (int n = 0; n < 8; n++) 
//            {
//                Dust dust1 = Terraria.Dust.NewDustPerfect(player.Center + (0.5f * 7 * W + MathHelper.TwoPi * n / 8).ToRotationVector2() * (float)(Math.Tan(4 * W) / Math.Sin(4 * W) + 8) * 25, d, new Vector2(0f, 0f), 0, Color.White, 1.5f);
//                dust1.noGravity = true;
//            }
//            //float X1 = (float)Math.Cos(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y1 = (float)Math.Sin(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X2 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W)/ Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y2 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X3 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y3 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X4 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y4 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X5 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y5 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X6 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y6 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X7 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y7 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float X8 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float Y8 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
//            //float s = 1.5f;
//            //float v = 2f;
//            //for (int i = 0; i < 3; i++)
//            //{
//            //    Dust dust1 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X1, Y1), d, new Vector2(0f, 0f), 0, Color.White, s);
//            //    Dust dust2 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X2, Y2), d, new Vector2(0, 0), 0, Color.White, s);
//            //    Dust dust3 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X3, Y3), d, new Vector2(0f, 0f), 0, Color.White, s);
//            //    Dust dust4 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X4, Y4), d, new Vector2(0, 0), 0, Color.White, s);
//            //    Dust dust5 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X5, Y5), d, new Vector2(0f, 0f), 0, Color.White, s);
//            //    Dust dust6 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X6, Y6), d, new Vector2(0f, 0f), 0, Color.White, s);
//            //    Dust dust7 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X7, Y7), d, new Vector2(0, 0), 0, Color.White, s);
//            //    Dust dust8 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X8, Y8), d, new Vector2(0f, 0f), 0, Color.White, s);
//            //    dust1.noGravity = true;
//            //    dust1.velocity *= v;
//            //    dust2.noGravity = true;
//            //    dust2.velocity *= v;
//            //    dust3.noGravity = true;
//            //    dust3.velocity *= v;
//            //    dust4.noGravity = true;
//            //    dust4.velocity *= v;
//            //    dust5.noGravity = true;
//            //    dust5.velocity *= v;
//            //    dust6.noGravity = true;
//            //    dust6.velocity *= v;
//            //    dust7.noGravity = true;
//            //    dust7.velocity *= v;
//            //    dust8.noGravity = true;
//            //    dust8.velocity *= v;
//            //}
//        }
//        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                target.AddBuff(20, 1800);
//                target.AddBuff(70, 1800);
//                target.AddBuff(ModContent.BuffType<Buffs.ToxicⅢ>(), 1800);
//            }
//            else
//            {
//                target.AddBuff(0, 0);
//            }
//        }

//        public override bool AltFunctionUse(Player player)
//        {
//            return true;
//        }
//    }
//}