using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Whips
{
    public class IntestineWhip : WhipBase_Item
    {
        public Item item => Item;
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            type = ModContent.ProjectileType<IntestineWhipProj>();

            damage = 125;
            knockBack = 1f;
            item.rare = MyRareID.Tier2;
        }

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("如果真的以肠子作为鞭子，那可真够恶心的，还好这只是个高仿，攻击怪物时有类似于消化它们的能力(指吸血)。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("肠鞭");
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
                player.statLife += 1;
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
            recipe1.AddIngredient<Materials.LivingRoot>(30);
            recipe1.AddIngredient<Materials.AncientEssence>(1800);
            recipe1.AddIngredient(ItemID.LunarBar, 15);
            recipe1.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class IntestineWhipEX : IntestineWhip
    {
        public override void AddRecipes()
        {
        }
        public override WeaponState State => WeaponState.False_EX;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("如果真的以肠子作为鞭子，那可真够恶心的，还好这只是个高仿，攻击怪物时有类似于消化它们的能力(指吸血)。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            // DisplayName.SetDefault("肠鞭EX");
        }
        public override void WhipInfo(ref int type, ref int damage, ref float knockBack, ref float shootSpeed, ref int animationTime)
        {
            base.WhipInfo(ref type, ref damage, ref knockBack, ref shootSpeed, ref animationTime);
            damage = 250;
            Item.rare = MyRareID.Tier3;
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
    public class IntestineWhipProj : WhipBase_Projectile
    {
        public override void WhipSettings(ref int segments, ref float rangeMultiplier)
        {
            if (Player.altFunctionUse == 2) rangeMultiplier *= 1.5f;
            rangeMultiplier *= this.UpgradeValue(1.25f, 1.5f, 1f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var heal = (int)(hit.Damage / 20 * Main.rand.NextFloat(0.85f, 1.15f));
            var realValue = Player.statLife;
            Player.statLife += heal;
            Player.statLife = (int)MathHelper.Clamp(Player.statLife, 0, Player.statLifeMax2 + 0.1f);
            realValue = Player.statLife - realValue;
            if (realValue > 0)
                Player.HealEffect(realValue);
            base.OnHitNPC(target, hit, damageDone);
        }
        //public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        //{
        //    if (sourceItemType == ModContent.ItemType<IntestineWhip>()) return normal;
        //    if (sourceItemType == ModContent.ItemType<IntestineWhipEX>()) return extra;
        //    if (Player.HeldItem.type == ModContent.ItemType<IntestineWhip>()) return normal;
        //    if (Player.HeldItem.type == ModContent.ItemType<IntestineWhipEX>()) return extra;
        //    Main.NewText($"Def拉你个大麻瓜 {sourceItemType} {Player.HeldItem.type} {ModContent.ItemType<IntestineWhip>()} {ModContent.ItemType<IntestineWhipEX>()}");

        //    return defaultValue;
        //}
        public override int DustType => MyDustId.RedBlood;
    }
}