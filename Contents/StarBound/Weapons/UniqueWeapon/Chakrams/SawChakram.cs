using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class SawChakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("锯环刃");
            Tooltip.SetDefault("遵循圣律的伐木僧们最喜欢的武器。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.rare = MyRareID.Tier2;
            item.shoot = ProjectileType<SawChakramProj>();
            item.height = item.width = 34;

        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.SharpenedClaw>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.SharpenedClaw>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
    public class SawChakramEX : SawChakram
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("锯环刃EX");
            Tooltip.SetDefault("遵循圣律的伐木僧们最喜欢的武器。\n就用这个锯下那最没用的珍珠木吧。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value *= 5;
            item.damage = 400;
            item.rare = MyRareID.Tier3;
        }
        public override void AddRecipes()
        {
        }
    }
    public class SawChakramProj : ChakramBaseProjectile 
    {
        public override void AI()
        {
            base.AI();
        }
    }
}
