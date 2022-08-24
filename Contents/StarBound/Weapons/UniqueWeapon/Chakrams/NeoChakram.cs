using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class NeoChakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("新型环刃");
            Tooltip.SetDefault("毁灭性的能量正发出噼啪声。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 225;
            item.shoot = ProjectileType<NeoChakramProj>();
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.StickOfRAM>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.StickOfRAM>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
    public class NeoChakramEX : NeoChakram
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("环刃EX");
            Tooltip.SetDefault("毁灭性的能量正发出噼啪声。\n锁定目标，光束打击！\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 350;
            item.rare = MyRareID.Tier3;
            item.value *= 5;

        }
        public override void AddRecipes()
        {
        }
    }
    public class NeoChakramProj : ChakramBaseProjectile 
    {
        public override void AI()
        {
            base.AI();
        }
    }
}
