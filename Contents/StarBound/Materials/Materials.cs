using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Materials
{
    public abstract class Materials : ModItem
    {
        public Item item => Item;
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 999;
            item.value = Item.buyPrice(gold: 1);
            item.rare = ItemRarityID.Red;
            SetMaterialValues();
        }
        //protected string ItemName;
        //protected string Description;
        public override void SetStaticDefaults()
        {
            string ItemName = "我抄居然被你发现材料的默认名了";
            string Description = "我抄居然被你发现材料的默认描述了";
            GetName(ref ItemName, ref Description);
            DisplayName.SetDefault(ItemName);
            Tooltip.SetDefault(Description);
        }
        public virtual void GetName(ref string ItemName, ref string Description)
        {

        }
        public virtual void SetMaterialValues()
        {
        }
    }
    public class LivingRoot : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "活根";
            Description = "一片被割断的进化植物物质。可以用于制作。";
        }
    }
    public class Leather : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "皮革";
            Description = "一块皮革。";
        }
    }
    public class HardenedCarapace : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "硬化甲壳";
            Description = "坚硬有机外壳的碎片。可以用于制作。";
        }
    }
    public class ErchiusCrystal : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "能源水晶";
            Description = "一种用于给FTL引擎供能的水晶，至今还未完全理解它的原理。\n在星界边境中连物质枪都对它无可奈何纯粹是因为呵呵鱼开了副本保护（";
        }
    }
    public class CryonicExtract : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "低温提取物";
            Description = "冰冷的有机化学品。可以用于制作。";
        }
    }
    public class PhaseMatter : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "月相物质";
            Description = "看起来几乎没有确定形态的物质。可以用于制作。";
        }
    }
    public class ScorchedCore : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "焦火之心";
            Description = "生物火系能力的源泉。可以用于制作。";
        }
    }
    public class SharpenedClaw : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "锋利的爪";
            Description = "锋利的怪物爪。可以用于制作。";
        }
    }
    public class StaticCell : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "静电细胞";
            Description = "一块充斥着电荷的细胞物质。可以用于制作。";
        }
    }
    public class StickOfRAM : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "内存条";
            Description = "一个典型的通用计算机芯片。可以用于制作。";
        }
    }
    public class VenomSample : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "毒液样本";
            Description = "挥发性有毒液体的样品。可以用于制作。";
        }
    }
    public class VioliumOre : Materials
    {
        public override void SetMaterialValues()
        {
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Swing;
            item.consumable = true;
            item.createTile = TileType<VioliumOreTile>();
        }
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "维奥合金矿";
            Description = "维奥合金矿,可用于冶炼。";
        }
    }
    public class FeroziumOre : Materials
    {
        public override void SetMaterialValues()
        {
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Swing;
            item.consumable = true;
            item.createTile = TileType<FeroziumOreTile>();
        }
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "菲洛合金矿";
            Description = "菲洛合金矿。可用于冶炼。";
        }
    }
    public class AegisaltOre : Materials
    {
        public override void SetMaterialValues()
        {
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Swing;
            item.consumable = true;
            item.createTile = TileType<AegisaltOreTile>();
        }
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "霓磷盐矿";
            Description = "这是霓磷盐。可用于冶炼。";
        }
    }
    public class SolariumOre : Materials
    {
        public override void SetMaterialValues()
        {
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Swing;
            item.consumable = true;
            item.createTile = TileType<SolariumOreTile>();
        }
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "日耀石矿";
            Description = "这就是日耀石。摸起来暖暖的。";
        }
    }
    public class RefinedViolium : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "精炼维奥合金";
            Description = "精炼维奥合金矿石可以在冰冻星系的行星上发现。\n在击败月球领主后也会无端地出现在泰拉瑞亚的地下......";
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<VioliumOre>(2);
            recipe.AddIngredient(ItemID.LunarOre, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class RefinedFerozium : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "精炼菲洛合金";
            Description = "精炼菲洛合金矿石可以在冰冻星系的行星上发现。\n在击败月球领主后也会无端地出现在泰拉瑞亚的地下......";
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FeroziumOre>(2);
            recipe.AddIngredient(ItemID.LunarOre, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class RefinedAegisalt : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "精炼霓磷盐";
            Description = "精炼霓磷盐矿可以在冰冻星系的行星上发现。\n在击败月球领主后也会无端地出现在泰拉瑞亚的地下......";
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AegisaltOre>(2);
            recipe.AddIngredient(ItemID.LunarOre, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class SolariumStar : Materials
    {
        public override void GetName(ref string ItemName, ref string Description)
        {
            ItemName = "精炼日耀石";
            Description = "精炼日耀石矿可以在灼热星系的行星上发现。\n在击败月球领主后也会无端地出现在泰拉瑞亚的地下......";
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<SolariumOre>(2);
            recipe.AddIngredient(ItemID.LunarOre, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class AncientEssence : ModItem
    {
        Item item => Item;
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.maxStack = 2147483647;
            item.rare = ItemRarityID.Purple;
            item.value = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("远古精华");
            Tooltip.SetDefault("有着极为强大的能量。");
        }
    }
}