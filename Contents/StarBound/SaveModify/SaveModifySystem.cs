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
        public static Vector2 positionLoader;
        public static Vector2 velocityLoader;
        public static int lifeLoader;
        public static int manaLoader;
        public static bool loadingData;
        public static string PlrName;
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
            PlrName = player.name;
            if (tag.TryGet("position" + IDCode, out Vector2 position)) 
            { 
                positionLoader = position; 
                loadingData = true; 
            }
               
            if (tag.TryGet("velocity" + IDCode, out Vector2 velocity))
                velocityLoader= velocity;
            if (tag.TryGet("Life" + IDCode, out int life))
                lifeLoader = life;
            if (tag.TryGet("Mana" + IDCode, out int mana))
                manaLoader = mana;
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
        public override void ResetEffects()
        {
            if (SaveModifySystem.loadingData)
            {
                Player.position = SaveModifySystem.positionLoader;
                Player.velocity = SaveModifySystem.velocityLoader;
                Player.statLife = SaveModifySystem.lifeLoader;
                Player.statMana = SaveModifySystem.manaLoader;
                //SaveModifySystem.loadingData = false;
            }
            Main.NewText("你是一个一个一个");
            Main.NewText(SaveModifySystem.PlrName);

        }
        public override void OnEnterWorld(Player player)
        {

        }
    }
}
