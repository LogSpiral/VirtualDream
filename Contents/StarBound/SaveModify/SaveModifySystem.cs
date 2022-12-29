using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.IO;
using Terraria.ModLoader.IO;

namespace VirtualDream.Contents.StarBound.SaveModify
{
    public class SaveModifySystem : ModSystem
    {
        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("uniqueID", uniqueID);
            #region Player
            var player = Main.LocalPlayer;
            int IDCode = player.GetModPlayer<SaveModifyPlayer>().uniqueID;
            tag.Add("position" + IDCode, player.position);
            tag.Add("velocity" + IDCode, player.velocity);
            tag.Add("Life" + IDCode, player.statLife);
            tag.Add("Mana" + IDCode, player.statMana);
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
                //IOrderedEnumerable<WorldFileData> orderedEnumerable = 
                //new List<WorldFileData>(Main.WorldList)
                //.OrderByDescending(CanWorldBePlayed)
                //.ThenByDescending((WorldFileData x) => x.IsFavorite)
                //.ThenBy((WorldFileData x) => x.Name)
                //.ThenBy((WorldFileData x) => x.GetFileName());
                uniqueID = Main.rand.Next(int.MaxValue);
            }
            #region Player
            var player = Main.LocalPlayer;
            var IDCode = player.GetModPlayer<SaveModifyPlayer>().uniqueID;
            if (tag.TryGet("position" + IDCode, out Vector2 position))
                player.position = position;
            if (tag.TryGet("velocity" + IDCode, out Vector2 velocity))
                player.velocity = velocity;
            if (tag.TryGet("Life" + IDCode, out int life))
                player.statLife = life;
            if (tag.TryGet("Mana" + IDCode, out int mana))
                player.statMana = mana;
            #endregion
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
            tag.Add("uniqueID", uniqueID);
        }
        public override void LoadData(TagCompound tag)
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
    }
}
