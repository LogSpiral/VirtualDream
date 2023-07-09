using Terraria;
using Terraria.ID;
using VirtualDream.Contents.StarBound.Buffs;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Whips
{
    public class VineWhip : WhipBase_Item
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("以附有巨毒的长藤作为鞭子。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("藤鞭");
        }
        public Item item => Item;
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            type = ModContent.ProjectileType<VineWhipProj>();
            damage = 125;
            item.rare = MyRareID.Tier2;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 45;
                item.useTime = 45;
            }
            else
            {
                item.useAnimation = 30;
                item.useTime = 30;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.VenomSample>(30);
            recipe1.AddIngredient<Materials.AncientEssence>(1800);
            recipe1.AddIngredient(ItemID.LunarBar, 15);
            recipe1.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class VineWhipEX : VineWhip
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("以附有巨毒的长藤作为鞭子。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("藤鞭EX");
        }
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            base.WhipInfo(ref type, ref damage, ref knockBack, ref shootSpeed, ref animationTime);
            damage = 250;
            item.rare = MyRareID.Tier3;
        }
        public override WeaponState State => WeaponState.False_EX;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 30;
                item.useTime = 30;
            }
            else
            {
                item.useAnimation = 20;
                item.useTime = 20;
            }
            return true;
        }
    }
    public class VineWhipProj : WhipBase_Projectile
    {
        public override void WhipSettings(ref int segments, ref float rangeMultiplier)
        {
            if (Player.altFunctionUse == 2) rangeMultiplier *= 1.5f;
            rangeMultiplier *= this.UpgradeValue(1.25f, 1.5f, 1f);
        }
        public override int DustType => MyDustId.GreenGrass;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ToxicⅠ>(), 180);
            base.OnHitNPC(target, hit, damageDone);
        }
        //public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        //{
        //    //if (source is EntitySource_ItemUse eisource)
        //    //{
        //    //    if (eisource.Item.type == ModContent.ItemType<VineWhip>()) return normal;
        //    //    if (eisource.Item.type == ModContent.ItemType<VineWhipEX>()) return extra;
        //    //}

        //    if (sourceItemType == ModContent.ItemType<VineWhip>()) return normal;
        //    if (sourceItemType == ModContent.ItemType<VineWhipEX>()) return extra;
        //    if (Player.HeldItem.type == ModContent.ItemType<VineWhip>()) return normal;
        //    if (Player.HeldItem.type == ModContent.ItemType<VineWhipEX>()) return extra;
        //    Main.NewText($"Def拉你个大麻瓜 {sourceItemType} {Player.HeldItem.type} {ModContent.ItemType<VineWhip>()} {ModContent.ItemType<VineWhipEX>()}");
        //    return defaultValue;
        //}
    }
}