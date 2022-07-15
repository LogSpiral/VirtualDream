//using Terraria;
//using Terraria.ModLoader;
//using VirtualDream.Utils;
//namespace VirtualDream.Items.Weapons.BossDrop.IxodoomClawS
//{
//    public class IxodoomClawEX : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
//            DisplayName.SetDefault("死亡主宰爪EX");
//        }
//        Item item => Item;

//        public override void SetDefaults()
//        {
//            item.damage = 500;
//            item.crit = 100;
//            item.DamageType = DamageClass.Melee;
//            item.width = 40;
//            item.height = 40;
//            item.rare = MyRareID.Tier2;
//            item.useTime = 24;
//            item.useAnimation = 24;
//            item.knockBack = 6;
//            item.useStyle = 1;
//            item.autoReuse = true;
//        }
//        public override bool CanUseItem(Player player)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                item.useTime = 48;
//                item.useAnimation = 48;
//            }
//            else
//            {
//                item.useTime = 24;
//                item.useAnimation = 24;
//            }
//            return base.CanUseItem(player);
//        }
//        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
//        {
//            if (player.altFunctionUse == 2)
//            {
//                target.AddBuff(70, 1200);
//                target.AddBuff(ModContent.BuffType<Buffs.ToxicⅡ>(), 1200);
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