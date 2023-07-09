using NVorbis.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static TagCompound[] npcData = new TagCompound[200];
        public static TagCompound[] projectileData = new TagCompound[1000];
        public static Dictionary<int, Action<ModProjectile, TagCompound>> projectileSaver;
        public static Dictionary<int, Action<ModProjectile, TagCompound>> projectileLoader;
        public static void LoadNPC(NPC npc, TagCompound tag)
        {
            if (tag.TryGet("type", out int type))
            {
                npc.type = type;
                npc.SetDefaults(type);
            }
            else
            {
                var modName = tag.GetString("modName");
                var name = tag.GetString("typeName");
                if (ModContent.TryFind(modName, name, out ModNPC modNPC))
                {
                    npc.type = modNPC.Type;
                    npc.SetDefaults(modNPC.Type);
                }
            }
            npc.position = tag.Get<Vector2>("position");
            npc.velocity = tag.Get<Vector2>("velocity");
            npc.life = tag.GetInt("life");
            npc.damage = tag.GetInt("damage");
            npc.defense = tag.GetInt("defense");
            npc.friendly = tag.GetBool("friendly");
            npc.noGravity = tag.GetBool("noGravity");
            npc.noTileCollide = tag.GetBool("noTileCollide");
            npc.realLife = tag.GetInt("realLife");
            for (int n = 0; n < 4; n++)
                npc.ai[n] = tag.GetFloat("ai" + n);
            for (int n = 0; n < 4; n++)
                npc.localAI[n] = tag.GetFloat("localAI" + n);
            //npc.ai = tag.Get<float[]>("ai");
            //npc.localAI = tag.Get<float[]>("localAI");
            npc.active = true;
        }
        public static void LoadProjectile(Projectile proj, TagCompound tag)
        {
            if (tag.TryGet("type", out int type))
            {
                proj.type = type;
                proj.SetDefaults(type);
            }
            else
            {
                var modName = tag.GetString("modName");
                var name = tag.GetString("typeName");
                if (ModContent.TryFind(modName, name, out ModProjectile modProjectile))
                {
                    proj.type = modProjectile.Type;
                    proj.SetDefaults(modProjectile.Type);
                    if (!(projectileLoader == null || projectileLoader.Count == 0) && projectileLoader.TryGetValue(proj.type, out var func))
                    {
                        var modData = new TagCompound();
                        func.Invoke(proj.ModProjectile, modData);
                    }
                }
            }
            proj.position = tag.Get<Vector2>("position");
            proj.velocity = tag.Get<Vector2>("velocity");
            proj.timeLeft = tag.GetInt("timeLeft");
            proj.damage = tag.Get<int>("damage");
            proj.extraUpdates = tag.Get<int>("extraUpdates");
            proj.friendly = tag.Get<bool>("friendly");
            proj.hostile = tag.Get<bool>("hostile");
            proj.knockBack = tag.Get<float>("knockBack");
            proj.owner = tag.Get<int>("owner");
            proj.penetrate = tag.Get<int>("penetrate");
            proj.scale = tag.Get<float>("scale");
            proj.rotation = tag.Get<float>("rotation");
            proj.tileCollide = tag.Get<bool>("tileCollide");

            for (int n = 0; n < 2; n++)
                proj.ai[n] = tag.GetFloat("ai" + n);
            for (int n = 0; n < 2; n++)
                proj.localAI[n] = tag.GetFloat("localAI" + n);
            proj.active = true;
        }
        public static TagCompound NPCData(NPC npc)
        {
            var result = new TagCompound();
            //TODO 用了新的检测原版npc的方式
            //原先是判定type
            if (npc.ModNPC == null)
                result.Add("type", npc.type);
            else
            {
                result.Add("modName", npc.ModNPC.Mod.Name);
                result.Add("typeName", npc.ModNPC.Name);
                //var modData = new TagCompound();
                //npc.ModNPC.SaveData(modData);
                //result.Add("ModNPCData", modData);
            }
            result.Add("position", npc.position);
            result.Add("velocity", npc.velocity);
            result.Add("life", npc.life);
            result.Add("damage", npc.damage);
            result.Add("defense", npc.defense);
            result.Add("friendly", npc.friendly);
            result.Add("noGravity", npc.noGravity);
            result.Add("noTileCollide", npc.noTileCollide);
            result.Add("realLife", npc.realLife);

            for (int n = 0; n < 4; n++)
                result.Add("ai" + n, npc.ai[n]);
            for (int n = 0; n < 4; n++)
                result.Add("localAI" + n, npc.localAI[n]);
            return result;
        }
        public static TagCompound ProjectileData(Projectile projectile)
        {
            var result = new TagCompound();
            if (projectile.ModProjectile == null)
                result.Add("type", projectile.type);
            else
            {
                result.Add("modName", projectile.ModProjectile.Mod.Name);
                result.Add("typeName", projectile.ModProjectile.Name);
                if (!(projectileSaver == null || projectileSaver.Count == 0) && projectileSaver.TryGetValue(projectile.type, out var func))
                {
                    var modData = new TagCompound();
                    func.Invoke(projectile.ModProjectile, modData);
                    result.Add("ModProjectileData", modData);
                }

            }
            result.Add("position", projectile.position);
            result.Add("velocity", projectile.velocity);
            result.Add("timeLeft", projectile.timeLeft);
            result.Add("damage", projectile.damage);
            result.Add("extraUpdates", projectile.extraUpdates);
            result.Add("friendly", projectile.friendly);
            result.Add("hostile", projectile.hostile);
            result.Add("knockBack", projectile.knockBack);
            result.Add("owner", projectile.owner);
            result.Add("penetrate", projectile.penetrate);
            result.Add("scale", projectile.scale);
            result.Add("rotation", projectile.rotation);
            result.Add("tileCollide", projectile.tileCollide);

            for (int n = 0; n < 2; n++)
                result.Add("ai" + n, projectile.ai[n]);
            for (int n = 0; n < 2; n++)
                result.Add("localAI" + n, projectile.localAI[n]);
            return result;
        }
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
            int counter = 0;
            foreach (var npc in Main.npc)
            {
                if (npc.active)
                {
                    tag.Add("npc_" + counter, NPCData(npc));
                }
                counter++;
            }
            #endregion
            #region Projectile
            counter = 0;
            foreach (var proj in Main.projectile)
            {
                if (proj.active)
                {
                    tag.Add("proj_" + counter, ProjectileData(proj));
                }
                counter++;
            }
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
            #region NPC
            for (int n = 0; n < Main.maxNPCs; n++)
            {
                if (tag.TryGet("npc_" + n, out TagCompound data))
                {
                    npcData[n] = data;
                }
                else
                {
                    npcData[n] = null;
                }
            }
            #endregion
            #region Projectile
            for (int n = 0; n < Main.maxItems; n++)
            {
                if (tag.TryGet("proj_" + n, out TagCompound data))
                {
                    projectileData[n] = data;
                    //Console.WriteLine((n, data.Get<string>("name")));
                }
                else
                {
                    projectileData[n] = null;
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
                    for (int n = 0; n < Main.maxNPCs; n++)
                    {
                        if (SaveModifySystem.npcData[n] == null) continue;
                        SaveModifySystem.LoadNPC(Main.npc[n], SaveModifySystem.npcData[n]);
                    }
                    for (int n = 0; n < Main.maxProjectiles; n++)
                    {
                        if (SaveModifySystem.projectileData[n] == null) continue;
                        SaveModifySystem.LoadProjectile(Main.projectile[n], SaveModifySystem.projectileData[n]);
                    }
                    SaveModifySystem.itemData = new TagCompound[400];
                    SaveModifySystem.npcData = new TagCompound[200];
                    SaveModifySystem.projectileData = new TagCompound[1000];
                    SaveModifySystem.loadingData = false;
                }
                //Main.NewText(SaveModifySystem.PlrName);
            }


        }
        public override void OnEnterWorld()
        {

        }
    }
    //public class SaveModifyGlobalNPC : GlobalNPC
    //{
    //    static bool SaveIt<T>(TagCompound tag, object obj, string name)
    //    {
    //        if (obj is T value)
    //        {
    //            tag.Add(name, value);
    //            return true;
    //        }
    //        return false;
    //    }
    //    public override bool InstancePerEntity => true;
    //    public override void SaveData(NPC npc, TagCompound tag)
    //    {
    //        //var infos = npc.GetType().GetFields();
    //        //foreach (var info in infos)
    //        //{
    //        //    var obj = info.GetValue(npc);
    //        //    //_ =
    //        //    //SaveIt<int>(tag, obj, info.Name) ||
    //        //    //SaveIt<Vector2>(tag, obj, info.Name) ||
    //        //    //SaveIt<int[]>(tag, obj, info.Name) ||
    //        //    //SaveIt<float[]>(tag, obj, info.Name) ||
    //        //    //SaveIt<bool>(tag, obj, info.Name) ||
    //        //    //SaveIt<float>(tag, obj, info.Name) ||
    //        //    //SaveIt<double>(tag, obj, info.Name) ||
    //        //    //SaveIt<byte>(tag, obj, info.Name) ||
    //        //    //SaveIt<long>(tag, obj, info.Name) ||
    //        //    //SaveIt<string>(tag, obj, info.Name) ||
    //        //    //SaveIt<short>(tag, obj, info.Name);
    //        //    try
    //        //    {
    //        //        tag.Add(info.Name, obj);
    //        //    }
    //        //    catch
    //        //    {
    //        //    }
    //        //}
    //    }
    //    public override void LoadData(NPC npc, TagCompound tag)
    //    {
    //        //var infos = npc.GetType().GetFields();
    //        //foreach (var info in infos)
    //        //{
    //        //    if (tag.TryGet(info.Name, out object value))
    //        //    {
    //        //        info.SetValue(npc, value);
    //        //    }
    //        //}
    //        base.LoadData(npc, tag);
    //    }
    //}
}
