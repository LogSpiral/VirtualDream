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
        public struct PlrData
        {
            public Vector2 position;
            public Vector2 velocity;
            public int life;
            public int mana;
            public TagCompound ToTag()
            {
                return new TagCompound() { ["position"] = position, ["velocity"] = velocity, ["life"] = life, ["mana"] = mana };
            }
            public PlrData(TagCompound tag)
            {
                position = tag.Get<Vector2>("position");
                velocity = tag.Get<Vector2>("velocity");
                life = tag.Get<int>("life");
                mana = tag.Get<int>("mana");
            }
        }
        public static Dictionary<int, PlrData> plrSaver = new Dictionary<int, PlrData>();
        public static bool loadingData;
        public static TagCompound[] itemData = new TagCompound[400];
        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("uniqueID", uniqueID);
            #region Player
            var plr = Main.LocalPlayer;
            var IDCode = plr.GetModPlayer<SaveModifyPlayer>().uniqueID;
            var data = new PlrData() with { position = plr.position, velocity = plr.velocity, life = plr.statLife, mana = plr.statMana };
            foreach (var key in plrSaver.Keys)
            {
                tag.Add("plrdata" + key, (key == IDCode ? data : plrSaver[key]).ToTag());
            }
            if (!plrSaver.ContainsKey(IDCode))
            {
                tag.Add("plrdata" + IDCode, data.ToTag());
            }
            #endregion
            #region Item
            for (int n = 0; n < Main.maxItems; n++)
            {
                var item = Main.item[n];
                if (item.active)
                {
                    var itemData = ItemIO.Save(item);
                    itemData.Add("position", item.position);
                    tag.Add("item_" + n, itemData);
                    //Console.WriteLine((n, item.Name));
                }
            }
            #endregion
            #region NPC
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
            plrSaver.Clear();
            foreach (var file in Main.PlayerList)
            {
                int IDCode = file.Player.GetModPlayer<SaveModifyPlayer>().uniqueID;
                if (tag.TryGet("plrdata" + IDCode, out TagCompound data))
                {
                    plrSaver.Add(IDCode, new PlrData(data));
                }
            }
            loadingData = true;
            #endregion
            #region Item
            for (int n = 0; n < Main.maxItems; n++)
            {
                if (tag.TryGet("item_" + n, out TagCompound data))
                {
                    itemData[n] = data;
                    //Console.WriteLine((n, data.Get<string>("name")));
                }
                else 
                {
                    itemData[n] = null;
                }
            }
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
            if (uniqueID == -1) uniqueID = Main.rand.Next(int.MaxValue);
        }
        public override void ResetEffects()
        {
            if (Player.whoAmI >= 0 && Player.whoAmI < 256 && !Main.gameMenu)
            {
                if (SaveModifySystem.loadingData)
                {
                    if (SaveModifySystem.plrSaver.TryGetValue(uniqueID, out SaveModifySystem.PlrData data))
                    {
                        Player.position = data.position;
                        Player.velocity = data.velocity;
                        Player.statLife = data.life;
                        Player.statMana = data.mana;
                    }
                    for (int n = 0; n < 400; n++)
                    {
                        if (SaveModifySystem.itemData[n] == null) continue;
                        ItemIO.Load(Main.item[n], SaveModifySystem.itemData[n]);
                        Main.item[n].active = true;
                        Main.item[n].position = SaveModifySystem.itemData[n].Get<Vector2>("position");
                        //Console.WriteLine((n, Main.item[n].Name, Main.item[n].active));
                    }
                    SaveModifySystem.loadingData = false;
                }
                //Main.NewText(SaveModifySystem.PlrName);
            }


        }
        public override void OnEnterWorld(Player player)
        {

        }
    }
}
