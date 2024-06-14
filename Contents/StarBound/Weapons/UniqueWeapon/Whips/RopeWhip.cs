using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Whips
{
    public class RopeWhip : WhipBase_Item
    {
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            type = ModContent.ProjectileType<RopeWhipProj>();
            damage = 150;
            item.rare = MyRareID.Tier2;
        }
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("比皮革更强，没有理由。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("绳鞭");
        }
        public Item item => Item;
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
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.Leather>(30);
            recipe1.AddIngredient<Materials.AncientEssence>(1800);
            recipe1.AddIngredient(ItemID.LunarBar, 15);
            recipe1.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class RopeWhipEX : RopeWhip
    {
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            base.WhipInfo(ref type, ref damage, ref knockBack, ref shootSpeed, ref animationTime);
            damage = 300;
            item.rare = MyRareID.Tier3;
        }
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("比皮革更强，没有理由。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("绳鞭EX");
        }
        public override void AddRecipes()
        {
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 21;
                item.useTime = 21;
            }
            else
            {
                item.useAnimation = 13;
                item.useTime = 13;
            }
            return true;
        }
    }
    public class RopeWhipProj : WhipBase_Projectile
    {
        public override void WhipSettings(ref int segments, ref float rangeMultiplier)
        {
            if (Player.altFunctionUse == 2) rangeMultiplier *= 1.5f;
            rangeMultiplier *= this.UpgradeValue(1.25f, 1.5f, 1f);
        }
        public override int DustType => MyDustId.Brown;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        //public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        //{
        //    //if (source is EntitySource_ItemUse eisource)
        //    //{
        //    //    if (eisource.Item.type == ModContent.ItemType<RopeWhip>()) return normal;
        //    //    if (eisource.Item.type == ModContent.ItemType<RopeWhipEX>()) return extra;
        //    //}

        //    if (sourceItemType == ModContent.ItemType<RopeWhip>()) return normal;
        //    if (sourceItemType == ModContent.ItemType<RopeWhipEX>()) return extra;
        //    if (Player.HeldItem.type == ModContent.ItemType<RopeWhip>()) return normal;
        //    if (Player.HeldItem.type == ModContent.ItemType<RopeWhipEX>()) return extra;
        //    Main.NewText($"Def拉你个大麻瓜 {sourceItemType} {Player.HeldItem.type} {ModContent.ItemType<RopeWhip>()} {ModContent.ItemType<RopeWhipEX>()}");

        //    return defaultValue;
        //}
    }
}