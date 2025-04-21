using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using VirtualDream.Utils;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;
using LogSpiralLibrary.CodeLibrary.DataStructures.SequenceStructures.Contents.Melee;
using LogSpiralLibrary.CodeLibrary.DataStructures.SequenceStructures.Core;
using LogSpiralLibrary.CodeLibrary.DataStructures.Drawing;
using LogSpiralLibrary.CodeLibrary.DataStructures.SequenceStructures.Contents.Melee.ExtendedMelee;

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

        //public override string Texture => base.Texture.Replace("Proj", "");
        public override StandardInfo StandardInfo => base.StandardInfo with
        {
            standardColor = Color.Gray * .25f,
            itemType = ModContent.ItemType<BrokenBroadSword>(),
            vertexStandard = new()
            {
                active = true,
                scaler = 105,
                timeLeft = 15,
                colorVec = new(0, 1, 0),
                alphaFactor = 2f
            },
            standardRotation = 0,
            standardOrigin = new Vector2(0.1f,0.5f)
        };
    }
    public class BrokenBroadSwordRotatingStorm : StormInfo
    {
        public override string Category => "Protector";
    }
}