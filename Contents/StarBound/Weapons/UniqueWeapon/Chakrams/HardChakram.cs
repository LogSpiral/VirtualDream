using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class HardChakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("硬环刃");
            Tooltip.SetDefault("坚固，沉重，而残忍地有效。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.shoot = ProjectileType<HardChakramProj>();
            item.height = item.width = 34;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.HardenedCarapace>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.HardenedCarapace>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
    public class HardChakramEX : HardChakram
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("硬环刃EX");
            Tooltip.SetDefault("坚固，沉重，而残忍地有效。\n沉重到能够轻易地击飞对方。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 400;
            item.rare = MyRareID.Tier3;
            item.value *= 5;

        }
        public override void AddRecipes()
        {
        }
    }
    public class HardChakramProj : ChakramBaseProjectile 
    {
        public override void AI()
        {
            base.AI();
        }
    }
}
