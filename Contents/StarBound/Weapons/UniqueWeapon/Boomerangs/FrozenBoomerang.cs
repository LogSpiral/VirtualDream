using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Boomerangs
{
    public class FrozenBoomerang : Chakrams.ChakramBaseItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("冷冻飞镖");
            // Tooltip.SetDefault("一个寒冷的飞镖与冰冻核心。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 270;
            item.rare = MyRareID.Tier2;
            item.shoot = ProjectileType<FrozenBoomerangProj>();
            item.height = item.width = 30;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient<NormalBoomerang>();
            recipe1.AddIngredient<Materials.CryonicExtract>(20);
            recipe1.AddIngredient<Materials.AncientEssence>(1500);
            recipe1.AddIngredient(ItemID.LunarBar, 10);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class FrozenBoomerangEX : FrozenBoomerang
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("冷冻飞镖EX");
            // Tooltip.SetDefault("一个寒冷的飞镖与冰冻核心。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        }
        public override WeaponState State => WeaponState.False_EX;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 420;
            item.rare = MyRareID.Tier3;
            item.width = item.height = 34;
            item.value *= 5;

        }
        public override bool Extra => true;
        public override void AddRecipes()
        {
        }
    }
    public class FrozenBoomerangProj : BoomerangBaseProj 
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            base.AI();
        }
    }
}
