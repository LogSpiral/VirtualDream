using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Boomerangs
{
    public class Boomerang : Chakrams.ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("飞镖");
            Tooltip.SetDefault("最先进的微推进器保证它总是能够返回。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.rare = MyRareID.Tier2;
            item.shoot = ProjectileType<BoomerangProj>();
            item.height = item.width = 30;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient<NormalBoomerang>();
            recipe1.AddIngredient<Materials.StickOfRAM>(20);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class BoomerangEX : Boomerang
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("飞镖EX");
            Tooltip.SetDefault("最先进的微推进器保证它总是能够返回。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override WeaponState State => WeaponState.False_EX;
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 425;
            item.rare = MyRareID.Tier3;
            item.value *= 5;
        }
        public override bool Extra => true;
        public override void AddRecipes()
        {
        }
    }
    public class BoomerangProj : BoomerangBaseProj 
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
