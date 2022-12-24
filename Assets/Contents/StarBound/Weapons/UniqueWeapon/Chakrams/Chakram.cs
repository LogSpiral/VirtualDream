using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Chakrams
{
    public class Chakram : ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("环刃");
            Tooltip.SetDefault("环刃大师能抓住它而不伤及手指。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 275;
            item.shoot = ProjectileType<ChakramProj>();
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.Leather>(25);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.AddIngredient(ItemID.TitaniumBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<Materials.Leather>(25);
            recipe2.AddIngredient<Materials.AncientEssence>(1500);
            recipe2.AddIngredient(ItemID.LunarBar, 10);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
    public class ChakramEX : Chakram
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("环刃EX");
            Tooltip.SetDefault("环刃大师能抓住它而不伤及手指。\n更加猛烈地切割万物吧，包括使用者自身。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 400;
            item.rare = MyRareID.Tier3;
            item.value *= 5;
            item.height = item.width = 30;
        }
        public override WeaponState State => WeaponState.False_EX;

        public override void AddRecipes()
        {
        }
        public override bool Extra => true;
    }
    public class ChakramProj : ChakramBaseProjectile 
    {
        public override void AI()
        {
            base.AI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
