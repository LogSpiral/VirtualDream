using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Boomerangs
{
    public class MoltenBoomerang : Chakrams.ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("火焰飞镖");
            // Tooltip.SetDefault("一个炎热的飞镖与熔火之心。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 325;
            item.rare = MyRareID.Tier2;
            item.shoot = ProjectileType<MoltenBoomerangProj>();
            item.height = item.width = 30;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient<NormalBoomerang>();
            recipe1.AddIngredient<Materials.ScorchedCore>(20);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class MoltenBoomerangEX : MoltenBoomerang
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("火焰飞镖EX");
            // Tooltip.SetDefault("一个炎热的飞镖与熔火之心。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 475;
            item.rare = MyRareID.Tier3;
            item.value *= 5;
        }
        public override WeaponState State => WeaponState.False_EX;

        public override bool Extra => true;
        public override void AddRecipes()
        {
        }
    }
    public class MoltenBoomerangProj : BoomerangBaseProj 
    {
        public override void AI()
        {
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
