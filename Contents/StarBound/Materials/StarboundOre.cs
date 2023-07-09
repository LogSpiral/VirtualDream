using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.StarBound.Materials
{
    public abstract class StarboundOre : ModTile
    {
        //protected int type;
        //protected string typeName;
        //protected Color color;
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // 此互动程序将受spelunker高亮显示的影响
            Main.tileOreFinderPriority[Type] = 1000; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
            Main.tileShine2[Type] = true; // 稍微修改绘图颜色。
            Main.tileShine[Type] = 975; // 瓷砖上经常会有细小的灰尘。较大的不太常见s
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            var name = CreateMapEntryName();

            int type = 0;
            string typeName = "着真逝个好物块冥";
            Color color = Color.White;
            GetInfo(ref type, ref typeName, ref color);
            //name.SetDefault(typeName);
            //name = new Terraria.Localization.LocalizedText(typeName);
            AddMapEntry(color, name);
            DustType = 84;
            //TODO 矿石名字
            //ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = type;
            HitSound = SoundID.Tink;
            //SoundStyle = 1;
            MineResist = 6f;
            MinPick = 255;
        }
        public virtual void GetInfo(ref int type, ref string typeName, ref Color color)
        {

        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            Tile tile = Main.tile[i, j];
            spriteBatch.Draw(VirtualDreamMod.GetTexture("Contents/StarBound/Materials/" + GetType().Name + "_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), Color.White * VirtualDreamMod.GlowLight * 2f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
    public class VioliumOreTile : StarboundOre
    {
        public override void GetInfo(ref int type, ref string typeName, ref Color color)
        {
            type = ItemType<VioliumOre>();
            typeName = "维奥合金矿";
            color = new Color(187, 71, 255);
        }
    }
    public class FeroziumOreTile : StarboundOre
    {
        public override void GetInfo(ref int type, ref string typeName, ref Color color)
        {
            type = ItemType<FeroziumOre>();
            typeName = "菲洛合金矿";
            color = new Color(83, 172, 177);
        }
    }
    public class AegisaltOreTile : StarboundOre
    {
        public override void GetInfo(ref int type, ref string typeName, ref Color color)
        {
            type = ItemType<AegisaltOre>();
            typeName = "霓磷盐矿";
            color = new Color(136, 168, 38);
        }
    }
    public class SolariumOreTile : StarboundOre
    {
        public override void GetInfo(ref int type, ref string typeName, ref Color color)
        {
            type = ItemType<SolariumOre>();
            typeName = "日耀石矿";
            color = new Color(210, 128, 0);
        }
    }
    public class MoonLordOreSpawner : GlobalNPC
    {
        public static void SpawnOre(int type, double pinlv, float depth, float depthMax, bool hell = false, int insteadType = 0)
        {

            for (int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * pinlv); i++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                //int y = WorldGen.genRand.Next((int)((Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0), Main.maxTilesY - 200);
                int fy = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
                int y = WorldGen.genRand.Next(fy, Main.maxTilesY - 200);
                //WorldGen.TileRunner(tilesX, tilesY, Main.rand.Next(3, 6), Main.rand.Next(1, 4), mod.TileType("OrangeSkin"), false, 0f, 0f, false, true);
                /*if (hell)
				{
					Tile tile = Framing.GetTileSafely(tilesX, tilesY);
					y = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY + 200);
					tilesY = WorldGen.genRand.Next((int)(y * depth), (int)(y * depthMax));
					if (tile.active() && tile.type == insteadType)
					{
						WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(3, 7), (ushort)type);
					}
				}
				else 
				{*/
                WorldGen.OreRunner(x, y, WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(3, 7), (ushort)type);
                //}
            }

        }
        /*public static void SpawnSolariumOre() 
		{
			int? x = null;
			int? y = null;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int k = Main.maxTilesY - 200; k < Main.maxTilesY; k++)
				{
					int r = WorldGen.genRand.Next(50);
					if (r == 0 && Main.tile[i, k].type == TileID.Ash) 
					{
						Main.tile[i, k].type = (ushort)TileType<SolariumOreTile>();
						x = i;
						y = k;
					}
					if (r < 5 && Main.tile[i, k].type == 57)
					{
						Main.tile[i, k].type = (ushort)TileType<SolariumOreTile>();
						x = i;
						y = k;
					}
					if (x != null && y != null)
					{
						for (int u = -1; u < 1; u++) 
						{
							for (int v = -1; u < 1; v++)
							{
								int r2 = Main.rand.Next(2);
								if (r2 == 0) 
								{
									Main.tile[(int)x + i,(int)y + k].type = (ushort)TileType<SolariumOreTile>();
								}
							}
						}
					}
				}
			}

		}*/
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.MoonLordCore && !NPC.downedMoonlord)
            {
                SpawnOre(TileType<VioliumOreTile>(), 2E-05, 0.3f, 0.9f);
                SpawnOre(TileType<FeroziumOreTile>(), 2E-05, 0.3f, 0.9f);
                SpawnOre(TileType<AegisaltOreTile>(), 2E-05, 0.3f, 0.9f);
                for (int num11 = 0; num11 < (int)(Main.maxTilesX * Main.maxTilesY * 0.0002); num11++)
                {
                    WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY - 140, Main.maxTilesY), WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(3, 7), TileType<SolariumOreTile>(), false, 0f, 0f, false, true);
                }
            }
        }
    }
}