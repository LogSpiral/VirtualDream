using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Boomerangs
{
    public class Lunarang : Chakrams.ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("月镖");
            // Tooltip.SetDefault("那不是月亮。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 285;
            item.rare = MyRareID.Tier2;
            item.shoot = ProjectileType<LunarangProj>();
            item.height = item.width = 28;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient<NormalBoomerang>();
            recipe1.AddIngredient<Materials.PhaseMatter>(20);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class LunarangEX : Lunarang
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("月镖EX");
            // Tooltip.SetDefault("那不是月亮。\n碎月\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override WeaponState State => WeaponState.False_EX;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 435;
            item.rare = MyRareID.Tier3;
            item.value *= 5;
        }
        public override bool Extra => true;
        public override void AddRecipes()
        {
        }
    }
    public class LunarangProj : BoomerangBaseProj 
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            base.AI();
        }
    }
}
