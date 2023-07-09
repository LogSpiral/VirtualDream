using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using VirtualDream.Utils;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Diagnostics;
using VirtualDream.Contents.TouhouProject.Items.Weapons.PhilosopherStone;
using LogSpiralLibrary;

namespace VirtualDream.Contents.TouhouProject.Items.Weapons
{
    public class EightTrigramsFurnace : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("极限状态下甚至能夷平一座山。");//“发射生命解离彩虹”
            // DisplayName.SetDefault("八卦炉");//终极棱镜
        }
        Item item => Item;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.ShaderItemEffectInventory(spriteBatch, position, origin, LogSpiralLibraryMod.Misc[0].Value, Main.hslToRgb((float)VirtualDreamMod.ModTime / 300f % 1, 1, 0.75f), scale);
            //SpriteBatchDraw.Draw(spriteBatch, IllusionBoundMod.GetTexture("Images/IMBellTex"),
            //    new SpriteBatchDraw.CustomVertexInfo4(
            //        new CustomVertexInfo(new Vector2(960, 540), Color.White, new Vector3(0, 0, 0)),
            //        new CustomVertexInfo(new Vector2(1000, 540), Color.White, new Vector3(1, 0, 0)),
            //        new CustomVertexInfo(new Vector2(960, 600), Color.White, new Vector3(0, 1, 0)),
            //        new CustomVertexInfo(new Vector2(1000, 600), Color.White, new Vector3(1, 1, 0))
            //        ));
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.ShaderItemEffectInWorld(spriteBatch, LogSpiralLibraryMod.Misc[0].Value, Main.hslToRgb((float)VirtualDreamMod.ModTime / 300f % 1, 1, 0.75f), rotation);
        }
        public override void SetDefaults()
        {
            item.damage = 300;
            item.knockBack = 0;
            item.useStyle = ItemUseStyleID.Shoot;
            item.useAnimation = 12;
            item.useTime = 12;
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            //item.rare = MyRareID.TouhouProject;
            item.rare = 10;
            item.mana = 10;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.channel = true;
            item.DamageType = DamageClass.Magic;
            item.shoot = ProjectileType<EightTrigramsFurnaceProj>();
        }
        public override void OnConsumeMana(Player player, int manaConsumed)
        {
            player.statMana += manaConsumed;
            base.OnConsumeMana(player, manaConsumed);
        }
        //public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        //{
        //    reduce = 0;
        //}
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
    public class EightTrigramsFurnaceProj : GemProj1
    {
        //public override string Texture => "IllusionBoundMod/NPCs/BigApe/StrawBerryArea";
        //public override string Texture => "IllusionBoundMod/Images/MagicZone/MagicZone_2";
        //public static void CleanTile(Tile tile)
        //{
        //    tile.active(false);
        //    tile.type = 0;
        //    tile.wall = 0;
        //    tile.liquid = 0;
        //}
        public override void OtherThings()
        {
            //for (int i = 0; i < Main.maxTilesX; i++)
            //{
            //    for (int j = 0; j < Main.maxTilesY; j++)
            //    {
            //        Main.tile[i, j].active(false);
            //        Main.tile[i, j].liquid = 0;
            //        Main.tile[i, j].wall = 0;
            //    }
            //}
            ////foreach (var t in Main.tile)
            ////{
            ////    CleanTile(t);
            ////}
            //if (projectile.ai[0] < 180) return;
            //var u = new Vector2(-projectile.velocity.Y, projectile.velocity.X);
            //for (int i = -8; i < 9; i++)
            //{
            //    for (int n = 0; n < 400; n++)
            //    {
            //        Point v = ((player.Center + projectile.velocity * 16 * n + u * 16 * i) / 16).ToPoint();
            //        var t = Main.tile[v.X, v.Y];
            //        //if (t.IsActive) 
            //        //{
            //        //	t.type = TileID.Glass;
            //        //	t.wall = WallID.Glass;
            //        //}
            //        t.LiquidAmount = 0;
            //        t.WallType = 0;
            //        player.PickTile(v.X, v.Y, 10000);
            //    }
            //}
        }
        public override bool UseMana => projectile.ai[0] >= 180 && (int)projectile.ai[0] % 10 == 0;
        public override Color MainColor => projectile != null ? Main.hslToRgb(projectile.ai[0] / 120f % 1, 1f, 0.75f) : Main.OurFavoriteColor;//
        public override void DrawExtraVertex()
        {
            //CustomVertexInfo[] _vertexInfos = new CustomVertexInfo[4];
            //_vertexInfos[0] = new CustomVertexInfo(default, MainColor, new Vector3(0, 0, 0));
            //_vertexInfos[1] = new CustomVertexInfo(default, MainColor, new Vector3(1, 0, 0));
            //_vertexInfos[2] = new CustomVertexInfo(default, MainColor, new Vector3(1, 1, 0));
            //_vertexInfos[3] = new CustomVertexInfo(default, MainColor, new Vector3(0, 1, 0));
            //for (int n = 0; n < 4; n++)
            //{
            //	_vertexInfos[n].Position = GetVec(new Vector3(vertexInfos[n].TexCoord.X - 0.5f, vertexInfos[n].TexCoord.Y - 0.5f, 0) * Size * 1.5f, 96, true) + player.Center;
            //	_vertexInfos[n].TexCoord.Z = Light;
            //}
            //CustomVertexInfo[] vertexInfos1 = new[] { _vertexInfos[0], _vertexInfos[1], _vertexInfos[2], _vertexInfos[2], _vertexInfos[3], _vertexInfos[0] };
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, new[] { vertexInfos[4], vertexInfos[5], vertexInfos[6], vertexInfos[6], vertexInfos[7], vertexInfos[4] }, 0, 2);
        }
        public override void UpdateVertex()
        {
            //Main.NewText(counter);
            //counter = 0;
            for (int n = 4; n < 8; n++)
            {
                vertexInfos[n].Position = GetVec(new Vector3(vertexInfos[n].TexCoord.X - 0.5f, vertexInfos[n].TexCoord.Y - 0.5f, 0) * Size * 1.5f, 96, true) + player.Center;
                vertexInfos[n].TexCoord.Z = Light;
                if (MainColor != vertexInfos[n].Color)
                {
                    vertexInfos[n].Color = MainColor;
                }
            }
            //Main.NewText(MainColor, MainColor);
            base.UpdateVertex();
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            //spriteBatch.Draw3DPlane(IllusionBoundMod.ShaderSwoosh, TextureAssets.MagicPixel.Value, TextureAssets.MagicPixel.Value, new VertexTriangle3List());

            if (projectile.ai[0] < 120) return;
            //if (projectile.ai[0] >= 180 &&!Main.gamePaused && (int)Main.time % 10 == 0 && !Main.player[projectile.owner].CheckMana(player.inventory[player.selectedItem].mana, true)) 
            //{
            //    projectile.Kill();
            //    return;
            //}
            var factor = MathHelper.Clamp(projectile.ai[0] - 180, 0, 60) / 60f;
            var fac = MathHelper.Clamp(projectile.ai[0] - 120, 0, 60) / 60f;
            //var stopWatch = new Stopwatch();
            //stopWatch.Start();
            //spriteBatch.DrawQuadraticLaser_PassNormal((vertexInfos[0].Position + vertexInfos[2].Position) * 0.5f
            //    , projectile.velocity, MainColor, 3200 * fac, 4.47213f * 16 * 4 * factor * 4 + 16f
            //    , Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48) * factor, (projectile.ai[0] >= 180 ? MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1) : fac) * 4, 10);
            spriteBatch.DrawQuadraticLaser_PassColorBar((vertexInfos[0].Position + vertexInfos[2].Position) * 0.5f
    , projectile.velocity, LogSpiralLibraryMod.HeatMap[13].Value, LogSpiralLibraryMod.AniTex[10].Value, 1920 * fac, (4.47213f * 16 * 4 * factor * 4 + 16f) * .4f
    , Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48) * factor, (projectile.ai[0] >= 180 ? MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1) : fac) * 4, true);
            //stopWatch.Stop();
            //Main.NewText(stopWatch.ElapsedTicks);
        }
        //public int counter;
        //public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    //SpriteBatch spriteBatch = Main.spriteBatch;
        //    spriteBatch.DrawString(Main.fontMouseText, "", default, default);
        //    //counter++;
        //    if (projectile.ai[0] < 120)
        //    {
        //        return;
        //    }
        //    Effect effect = mod.GetEffect("Effects/EightTrigramsFurnaceEffect");
        //    spriteBatch.End();
        //    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
        //    List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
        //    Vector2 start = (vertexInfos[0].Position + vertexInfos[2].Position) * 0.5f;
        //    float factor = MathHelper.Clamp(projectile.ai[0] - 180, 0, 60) / 60f;
        //    Vector2 unit = projectile.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48) * factor);
        //    //Vector2 unit = projectile.velocity;
        //    Vector2 unit2 = new Vector2(-unit.Y, unit.X);
        //    //Main.screenPosition += Main.rand.NextVector2Unit() * 2;

        //    //float a = 1 / 4f;
        //    //float b = 3 / 4f;
        //    //if (projectile.ai[0] < 180)
        //    //         {
        //    //	float fac = MathHelper.Clamp(projectile.ai[0] - 120, 0, 60) / 60f;
        //    //	bars1.Add(new CustomVertexInfo(start + unit2 * 16, MainColor, new Vector3(0, a, fac)));
        //    //	bars1.Add(new CustomVertexInfo(start - unit2 * 16, MainColor, new Vector3(0, b, fac)));
        //    //	bars1.Add(new CustomVertexInfo(start + unit2 * 16 + fac * 1600 * unit, MainColor, new Vector3(1, a, fac)));
        //    //	bars1.Add(new CustomVertexInfo(start - unit2 * 16 + fac * 1600 * unit, MainColor, new Vector3(1, b, fac)));
        //    //}
        //    //         else
        //    //         {
        //    //             for (int n = 0; n <= 100; n++)
        //    //             {
        //    //		float height = (float)Math.Sqrt(n / 5f) * 16 * 4 * factor * 4 + 16f;
        //    //		bars1.Add(new CustomVertexInfo(start + unit2 * height + 16 * unit * n, MainColor, new Vector3(n / 100f, a, MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1))));
        //    //		bars1.Add(new CustomVertexInfo(start - unit2 * height + 16 * unit * n, MainColor, new Vector3(n / 100f, b, MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1))));
        //    //	}
        //    //         }
        //    float height = 4.47213f * 16 * 4 * factor * 4 + 16f;
        //    float fac = MathHelper.Clamp(projectile.ai[0] - 120, 0, 60) / 60f;
        //    var l = fac;
        //    if (projectile.ai[0] >= 180) l = MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1);
        //    l *= 4;
        //    bars1.Add(new CustomVertexInfo(start + unit2 * height, MainColor, new Vector3(0, 0, l)));
        //    bars1.Add(new CustomVertexInfo(start - unit2 * height, MainColor, new Vector3(0, 1, l)));
        //    bars1.Add(new CustomVertexInfo(start + unit2 * height + fac * 3200 * unit, MainColor, new Vector3(1, 0, 0)));
        //    bars1.Add(new CustomVertexInfo(start - unit2 * height + fac * 3200 * unit, MainColor, new Vector3(1, 1, 0)));
        //    List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
        //    if (bars1.Count > 2)
        //    {
        //        for (int i = 0; i < bars1.Count - 2; i += 2)
        //        {
        //            triangleList1.Add(bars1[i]);
        //            triangleList1.Add(bars1[i + 2]);
        //            triangleList1.Add(bars1[i + 1]);
        //            triangleList1.Add(bars1[i + 1]);
        //            triangleList1.Add(bars1[i + 2]);
        //            triangleList1.Add(bars1[i + 3]);
        //        }
        //        RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
        //        var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
        //        var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
        //        //Effect effect = projectile.ai[0] < 180 ? InfiniteNightmare.ColorEffect : effect;
        //        effect.Parameters["uTransform"].SetValue(model * projection);
        //        effect.Parameters["maxFactor"].SetValue(0.5f);
        //        effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);// - Main.GameUpdateCount * 0.03f
        //        Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.MaskColor[6];
        //        Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.MaskColor[10];//IllusionBoundMod.LaserTex[(int)projectile.ai[0] % 4];
        //        Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.MainColor[23];
        //        Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
        //        Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
        //        Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
        //        effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect_ColorBar"].Apply();//_TimeOffset
        //        Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
        //        Main.graphics.GraphicsDevice.RasterizerState = originalState;
        //        spriteBatch.End();
        //        spriteBatch.Begin();
        //    }

        //}

        //      public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        //      {
        //	if (projectile.ai[0] < 120)
        //	{
        //		return;
        //	}
        //	spriteBatch.End();
        //          spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
        //          List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
        //          Vector2 start = (vertexInfos[0].Position + vertexInfos[2].Position) * 0.5f;
        //	float factor = MathHelper.Clamp(projectile.ai[0] - 180, 0, 60) / 60f;
        //	Vector2 unit = projectile.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48) * factor);
        //          Vector2 unit2 = new Vector2(-unit.Y, unit.X);
        //	//float a = 1 / 4f;
        //	//float b = 3 / 4f;
        //	float a = 0;
        //	float b = 1;
        //	if (projectile.ai[0] < 180)
        //          {
        //		float fac = MathHelper.Clamp(projectile.ai[0] - 120, 0, 60) / 60f;
        //		bars1.Add(new CustomVertexInfo(start + unit2 * 16, MainColor, new Vector3(0, a, fac)));
        //		bars1.Add(new CustomVertexInfo(start - unit2 * 16, MainColor, new Vector3(0, b, fac)));
        //		bars1.Add(new CustomVertexInfo(start + unit2 * 16 + fac * 1600 * unit, MainColor, new Vector3(1, a, fac)));
        //		bars1.Add(new CustomVertexInfo(start - unit2 * 16 + fac * 1600 * unit, MainColor, new Vector3(1, b, fac)));
        //	}
        //          else
        //          {
        //              for (int n = 0; n <= 100; n++)
        //              {
        //			float height = (float)Math.Sqrt(n / 5f) * 16 * 4 * factor * 4 + 16f;
        //			bars1.Add(new CustomVertexInfo(start + unit2 * height + 16 * unit * n, MainColor, new Vector3(n / 100f, a, MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1))));
        //			bars1.Add(new CustomVertexInfo(start - unit2 * height + 16 * unit * n, MainColor, new Vector3(n / 100f, b, MathHelper.Clamp(projectile.timeLeft / 12f, 0, 1))));
        //		}
        //          }
        //	List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
        //	if (bars1.Count > 2) 
        //	{
        //		for (int i = 0; i < bars1.Count - 2; i += 2)
        //		{
        //			triangleList1.Add(bars1[i]);
        //			triangleList1.Add(bars1[i + 2]);
        //			triangleList1.Add(bars1[i + 1]);
        //			triangleList1.Add(bars1[i + 1]);
        //			triangleList1.Add(bars1[i + 2]);
        //			triangleList1.Add(bars1[i + 3]);
        //		}
        //		RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
        //		var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
        //		var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
        //		Effect effect = IllusionBoundMod.ShaderSwoosh;
        //		effect.Parameters["uTransform"].SetValue(model * projection);
        //		effect.Parameters["uTime"].SetValue(0);//-(float)IllusionBoundMod.ModTime * 0.03f
        //		Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.MaskColor[6];
        //		Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.MaskColor[1];//IllusionBoundMod.LaserTex[(int)projectile.ai[0] % 4];
        //		Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
        //		Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
        //		effect.CurrentTechnique.Passes[0].Apply();
        //		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
        //		Main.graphics.GraphicsDevice.RasterizerState = originalState;
        //		spriteBatch.End();
        //		spriteBatch.Begin();
        //	}

        //}
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] <= 180)
            {
                return false;
            }
            //for (int n = -1; n <= 1; n++)
            //{
            //	float point = 0f;
            //	Vector2 cen = (vertexInfos[0].Position + vertexInfos[2].Position) * 0.5f;
            //	if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), cen, (Beta + MathHelper.Pi / 24 * n).ToRotationVector2() * 1600f + cen, 96 * (float)Math.Sqrt(MathHelper.Clamp(projectile.ai[0] - 180, 0, 60) / 60f), ref point))
            //		return true;
            //}
            float point = 0f;
            Vector2 cen = (vertexInfos[0].Position + vertexInfos[2].Position) * 0.5f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), cen, Beta.ToRotationVector2() * 3200f + cen, 96 * (float)Math.Sqrt(MathHelper.Clamp(projectile.ai[0] - 180, 0, 60) / 60f), ref point);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[projectile.owner] = 0;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.friendly = true;
            vertexInfos = new CustomVertexInfo[8];
            vertexInfos[0] = new CustomVertexInfo(default, MainColor, new Vector3(0, 0, 0));
            vertexInfos[1] = new CustomVertexInfo(default, MainColor, new Vector3(1, 0, 0));
            vertexInfos[2] = new CustomVertexInfo(default, MainColor, new Vector3(1, 1, 0));
            vertexInfos[3] = new CustomVertexInfo(default, MainColor, new Vector3(0, 1, 0));
            vertexInfos[4] = new CustomVertexInfo(default, MainColor, new Vector3(0, 0, 0));
            vertexInfos[5] = new CustomVertexInfo(default, MainColor, new Vector3(1, 0, 0));
            vertexInfos[6] = new CustomVertexInfo(default, MainColor, new Vector3(1, 1, 0));
            vertexInfos[7] = new CustomVertexInfo(default, MainColor, new Vector3(0, 1, 0));
        }
    }
}
