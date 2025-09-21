using LogSpiralLibrary.CodeLibrary.DataStructures.SequenceStructures.Contents.Melee;
using LogSpiralLibrary.CodeLibrary.DataStructures.SequenceStructures.Contents.Melee.Core;
using LogSpiralLibrary.CodeLibrary.DataStructures.SequenceStructures.Contents.Melee.ExtendedMelee;
using LogSpiralLibrary.CodeLibrary.Utilties;
using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Protector
{
    public class BrokenBroadSword : MeleeSequenceItem<BrokenBroadSwordProj>
    {
        //public override void SetStaticDefaults()
        //{
        //    Tooltip.SetDefault("一把不错的剑，但是状态很差。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
        //    DisplayName.SetDefault("破裂的英雄之剑");
        //}
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.UseSound = SoundID.Item1;

            Item.crit = 6;
            Item.width = 71;
            Item.width = 71;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.knockBack = 10;
            Item.value = 10000;
            Item.rare = MyRareID.Tier1;
            Item.damage = 50;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }

    public class BrokenBroadSwordProj : MeleeSequenceProj
    {
        public override bool LabeledAsCompleted => true;

        public override void InitializeStandardInfo(StandardInfo standardInfo, VertexDrawStandardInfo vertexStandard)
        {
            standardInfo.standardColor = Color.Gray * .25f;
            standardInfo.itemType = ModContent.ItemType<BrokenBroadSword>();
            standardInfo.standardRotation = 0;
            standardInfo.standardOrigin = new Vector2(0.1f, 0.5f);

            vertexStandard.scaler = 105;
            vertexStandard.timeLeft = 15;
            vertexStandard.colorVec = new Vector3(0, 1, 0);
            vertexStandard.alphaFactor = 2f;
            base.InitializeStandardInfo(standardInfo, vertexStandard);
        }
    }

    public class BrokenBroadSwordRotatingStorm : StormInfo
    {
        public override string Category => "Protector";
    }
}