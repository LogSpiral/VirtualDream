using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace VirtualDream.Contents.StarBound.TimeBackTracking
{
    public class WindOfTime : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("这是一个能扭曲时空的怀表\n没有人知道这货究竟是怎么工作的，而你也不知道自己是怎么鬼使神差造出这玩意的");//\"堆积一切又风化一切的时之风，孕育一切又埋没一切的星之河\"
            DisplayName.SetDefault("时之风");
        }
        //public override string Texture => "Terraria/Images/Item_" + Terraria.ID.ItemID.PlatinumWatch;
        public int chargeCount;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var tip = new TooltipLine(Mod, "Charge", $"目前已经充能了{chargeCount} / 8");
            tip.OverrideColor = Main.hslToRgb(0.75f, 0.75f + MathF.Sin((float)IllusionBoundModSystem.ModTime / 120f * MathHelper.Pi) * .25f, .5f);
            tooltips.Add(tip);
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.value = -1;
            Item.useTime = Item.useAnimation = 60;
            Item.useStyle = Terraria.ID.ItemUseStyleID.HoldUp;
            Item.holdStyle = Terraria.ID.ItemHoldStyleID.HoldHeavy;
            Item.accessory = true;
            Item.UseSound = new SoundStyle("VirtualDream/Assets/Sound/shotgun_reload_clip3");
        }
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == 1) 
            {
                chargeCount = 0;
                if (SubworldLibrary.SubworldSystem.Current is TimeBackTrackingWorld world)
                {
                    world.timeLeft += 129600;
                }
                else
                {
                    SubworldLibrary.SubworldSystem.Enter<TimeBackTrackingWorld>();
                }
            }
            //Main.NewText("!!");
            return base.UseItem(player);
        }
        public override bool CanUseItem(Player player)
        {
            return chargeCount == 8 || player.itemAnimation > 0;//
        }
        public override void UpdateInventory(Player player)
        {
            if ((int)Main.time == 0 && Main.dayTime && chargeCount < 8)
            {
                chargeCount++;
            }
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if ((int)Main.time == 0 && Main.dayTime && chargeCount < 8)
            {
                chargeCount++;
            }
        }
        public override void LoadData(TagCompound tag)
        {
            chargeCount = tag.GetInt("chargeCount");
            base.LoadData(tag);
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("chargeCount", chargeCount);
            base.SaveData(tag);
        }
    }
    //public class WindOfTimeStand : ModItem
    //{

    //}
    //public class WindOfTimeStandTile : ModTile
    //{

    //}
}
