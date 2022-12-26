using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace VirtualDream.Contents.StarBound.SaveModify
{
    public class SaveModifySystem : ModSystem
    {
        public override void SaveWorldData(TagCompound tag)
        {
            #region Player

            #endregion
        }
        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.TryGet("uniqueID", out int id))
            {
                uniqueID = id;
            }
            else 
            {
                uniqueID = Main.rand.Next(int.MaxValue);
            }
        }

        public override void OnWorldLoad()
        {
            uniqueID = -1;
        }

        public override void OnWorldUnload()
        {
            uniqueID = -1;
        }
        internal int uniqueID;
    }
    public class SaveModifyPlayer : ModPlayer
    {
        internal int uniqueID = -1;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("position", Player.position);
            tag.Add("velocity", Player.velocity);
            tag.Add("Life", Player.statLife);
            tag.Add("Mana", Player.statMana);
            tag.Add("uniqueID", uniqueID);
        }
        public override void LoadData(TagCompound tag)
        {
            Player.position = tag.Get<Vector2>("position");
            Player.velocity = tag.Get<Vector2>("velocity");
            Player.statLife = tag.Get<int>("Life");
            Player.statMana = tag.Get<int>("Mana");
            uniqueID = tag.Get<int>("uniqueID");
        }
    }
}
