using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace VirtualDream.Contents.StarBound.TimeBackTracking
{
    public class TimeBackTrackingWorld : SubworldLibrary.Subworld
    {
        public override int Height => 1200;
        public override int Width => 2100;
        public override string Name => "TimeBackTracking";//时间回溯世界
        public override WorldGenConfiguration Config => base.Config;
        public override List<GenPass> Tasks => new List<GenPass>()
        {
            new Terraria.GameContent.Generation.PassLegacy("TimeBack!!",
                (progress,config)=>
                {
                    progress.Message = "我抄";
                    var genRand = WorldGen.genRand;
                    int offsetTarget = genRand.Next(0,30) * genRand.Next(new int[]{-1,1 });
                    int offset = 0;
                    int offsetTargetRock = genRand.Next(0,60) * genRand.Next(new int[]{-1,1 });
                    int offsetRock = 0;
                    for(int n = 0;n <Width;n++)
                    {
                        if(n % 100 == 0)
                        {
                            offset = offsetTarget;
                            offsetTarget = genRand.Next(0,30) * genRand.Next(new int[]{-1,1 });
                        }
                        if(n % 50 == 0)
                        {
                            offsetRock = offsetTargetRock;
                            offsetTargetRock = genRand.Next(0,60) * genRand.Next(new int[]{-1,1 });
                        }
                        int currentOffset = (int)MathHelper.SmoothStep(offset,offsetTarget,n % 100 / 100f);
                        for(int k =Height / 4 + currentOffset;k < Height / 8 * 3 + 60;k++)
                        {
                            //if(genRand.NextBool(5))continue;
                            var tile = Main.tile[n,k];
                            tile.TileType = (k == Height / 4 + currentOffset) ?Terraria.ID.TileID.Grass: Terraria.ID.TileID.Dirt;
                            tile.HasTile = true;
                        }
                        currentOffset = (int)MathHelper.SmoothStep(offsetRock,offsetTargetRock,n % 50 / 50f);
                        for(int k =Height / 8 * 3 + currentOffset;k < Height;k++)
                        {
                            //if(genRand.NextBool(5))continue;
                            var tile = Main.tile[n,k];
                            tile.TileType = Terraria.ID.TileID.Stone;
                            tile.HasTile = true;
                        }
                    }
                    offset= Main.spawnTileY;
                    offsetTarget = Main.spawnTileX;
                    while(Main.tile[offsetTarget,offset].HasTile)
                    {
                        offset--;
                    }
                    Main.spawnTileY = offset;





                //                    float num845 = (float)(Width *Height) * 0.002f;
                //for (int num846 = 0; (float)num846 < num845; num846++) {
                //    int num847 = genRand.Next(1, Width - 1);
                //    int num848 = genRand.Next((int)WorldGen.worldSurfaceLow, (int)WorldGen.worldSurfaceHigh);
                //    if (num848 >=Height)
                //        num848 =Height - 2;

                //    if (Main.tile[num847 - 1, num848].HasTile && Main.tile[num847 - 1, num848].TileType == 0 && Main.tile[num847 + 1, num848].HasTile && Main.tile[num847 + 1, num848].TileType == 0 && Main.tile[num847, num848 - 1].HasTile && Main.tile[num847, num848 - 1].TileType == 0 && Main.tile[num847, num848 + 1].HasTile && Main.tile[num847, num848 + 1].TileType == 0) {
                //       var tile = Main.tile[num847, num848];
                //        tile.HasTile = true;
                //        tile.TileType = 2;
                //    }

                //    num847 = genRand.Next(1, Width - 1);
                //    num848 = genRand.Next(0, (int)WorldGen.worldSurfaceLow);
                //    if (num848 >=Height)
                //        num848 =Height - 2;

                //    if (Main.tile[num847 - 1, num848].HasTile && Main.tile[num847 - 1, num848].TileType == 0 && Main.tile[num847 + 1, num848].HasTile && Main.tile[num847 + 1, num848].TileType == 0 && Main.tile[num847, num848 - 1].HasTile && Main.tile[num847, num848 - 1].TileType == 0 && Main.tile[num847, num848 + 1].HasTile && Main.tile[num847, num848 + 1].TileType == 0) {
                //       var tile = Main.tile[num847, num848];
                //        tile.HasTile = true;
                //        tile.TileType = 2;
                //    }
                //}
                }
                )
        };
        public override bool NoPlayerSaving => base.NoPlayerSaving;
        public override bool NormalUpdates => true;
        public override bool ShouldSave => base.ShouldSave;
        public int timeLeft;
        public override void DrawMenu(GameTime gameTime)
        {
            //Main.spriteBatch.Draw(TextureAssets.Item[Terraria.ID.ItemID.Zenith].Value, new Rectangle(240, 240, 240, 240), Color.White);
            base.DrawMenu(gameTime);
        }
        public override void DrawSetup(GameTime gameTime)
        {
            //Main.spriteBatch.Draw(TextureAssets.Item[Terraria.ID.ItemID.FirstFractal].Value, new Rectangle(240, 240, 240, 240), Color.White);
            base.DrawSetup(gameTime);
        }
        public override bool GetLight(Tile tile, int x, int y, ref FastRandom rand, ref Vector3 color)
        {
            //color = Main.hslToRgb(Main.rand.NextFloat(0, 1), 1f, 0.75f).ToVector3();
            return base.GetLight(tile, x, y, ref rand, ref color);
        }
        public override void OnEnter()
        {
            timeLeft = 86400;
            bool flag = Main.rand.NextBool(2);
            Main.time = flag ? Main.rand.Next(0, 54000) : Main.rand.Next(0, 32400);
            Main.dayTime = flag;

            base.OnEnter();
        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
    public class TimeBackTrackingBiome : ModBiome
    {
        // Select all the scenery
        //public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("ExampleMod/ExampleWaterStyle"); // Sets a water style for when inside this biome
        //public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("ExampleMod/ExampleSurfaceBackgroundStyle");
        //public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Crimson;

        // Select Music
        //public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");

        // Populate the Bestiary Filter
        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => BackgroundPath; // Re-uses Bestiary Background for Map Background

        // Use SetStaticDefaults to assign the display name
        public override void SetStaticDefaults()
        {
            // This translation is set in localization files
            // DisplayName.SetDefault("Example Surface");
        }

        // Calculate when the biome is active.
        public override bool IsBiomeActive(Player player)
        {
            //// First, we will use the exampleBlockCount from our added ModSystem for our first custom condition
            //bool b1 = ModContent.GetInstance<ExampleBiomeTileCount>().exampleBlockCount >= 40;

            //// Second, we will limit this biome to the inner horizontal third of the map as our second custom condition
            //bool b2 = Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;

            //// Finally, we will limit the height at which this biome can be active to above ground (ie sky and surface). Most (if not all) surface biomes will use this condition.
            //bool b3 = player.ZoneSkyHeight || player.ZoneOverworldHeight;
            //return b1 && b2 && b3;
            return SubworldLibrary.SubworldSystem.Current is TimeBackTrackingWorld;
        }
        public override void OnEnter(Player player)
        {
            SoundEngine.PlaySound(Terraria.ID.SoundID.Zombie104, Main.LocalPlayer.Center);

            base.OnEnter(player);
        }
        public override void OnLeave(Player player)
        {
            base.OnLeave(player);
        }
    }
}
