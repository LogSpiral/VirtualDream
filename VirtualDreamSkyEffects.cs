using VirtualDream.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace VirtualDream
{
    //public class BigApeSky_ : CustomSky
    //{
    //    // Token: 0x06000B84 RID: 2948 RVA: 0x00004165 File Offset: 0x00002365
    //    public override void Deactivate(params object[] args)
    //    {
    //        this.skyActive = false;
    //    }

    //    // Token: 0x06000B85 RID: 2949 RVA: 0x00004165 File Offset: 0x00002365
    //    public override void Reset()
    //    {
    //        this.skyActive = false;
    //    }

    //    // Token: 0x06000B86 RID: 2950 RVA: 0x0000416E File Offset: 0x0000236E
    //    public override bool IsActive()
    //    {
    //        return this.skyActive || this.opacity > 0f;
    //    }

    //    // Token: 0x06000B87 RID: 2951 RVA: 0x00004187 File Offset: 0x00002387
    //    public override void Activate(Vector2 position, params object[] args)
    //    {
    //        this.skyActive = true;
    //    }

    //    // Token: 0x06000B88 RID: 2952 RVA: 0x00083C24 File Offset: 0x00081E24
    //    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    //    {
    //        if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
    //        {
    //            spriteBatch.Draw(IllusionBoundMod.GetTexture("Backgrounds/WhiteSky"), new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Lerp(new Color(0, 255, 255), new Color(0, 0, 255), (float)Math.Sin(IllusionBoundMod.ModTime / 180f * MathHelper.TwoPi) * .5f + .5f) * this.opacity);//Main.bgColor//Color.White
    //            #region 你瞅啥
    //            //spriteBatch.End();
    //            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
    //            //CustomVertexInfo[] triangleArry = new CustomVertexInfo[6];
    //            //RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
    //            //Color c = new Color(127, 127, 127);
    //            //float light = (1.5f - StormPlantLight).ValueRange();
    //            //triangleArry[0] = new CustomVertexInfo(Main.screenPosition, c, new Vector3(0, 0, light));
    //            //triangleArry[1] = new CustomVertexInfo(Main.screenPosition + new Vector2(Main.screenWidth, 0), c, new Vector3(0, 0.5f, light));
    //            //triangleArry[2] = new CustomVertexInfo(Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight), c, new Vector3(1, 0.5f, light));
    //            //triangleArry[3] = triangleArry[2];
    //            //triangleArry[4] = new CustomVertexInfo(Main.screenPosition + new Vector2(0, Main.screenHeight), c, new Vector3(1, 0, light));
    //            //triangleArry[5] = triangleArry[0];
    //            //var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
    //            //var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
    //            //IllusionBoundMod.IMBellEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
    //            //IllusionBoundMod.IMBellEffect.Parameters["uTime"].SetValue((float)IllusionBoundMod.ModTime / 300);
    //            //Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.MaskColor[6];
    //            //Main.graphics.GraphicsDevice.Textures[1] = GetTexture("IllusionBoundMod/Backgrounds/StormSky");
    //            //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
    //            //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
    //            //IllusionBoundMod.IMBellEffect.CurrentTechnique.Passes[0].Apply();
    //            //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleArry, 0, 2);
    //            //Main.graphics.GraphicsDevice.RasterizerState = originalState;
    //            //spriteBatch.End();
    //            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
    //            ////double w = IllusionBoundMod.ModTime % 1 / 300f;
    //            //if (Main.netMode != 2)
    //            //{
    //            //	//for (int i = 0; i < Main.star.Length; i++)
    //            //	//{
    //            //	//	Star star = Main.star[i];
    //            //	//	if (star != null)
    //            //	//	{
    //            //	//		Texture2D texture2D = Main.starTexture[star.type];
    //            //	//		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
    //            //	//		int num = (int)((double)(-(double)Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
    //            //	//		float num2 = star.position.X * ((float)Main.screenWidth / 800f);
    //            //	//		float num3 = star.position.Y * ((float)Main.screenHeight / 600f);
    //            //	//		Vector2 position = new Vector2(num2 + vector.X, num3 + vector.Y + (float)num);
    //            //	//		spriteBatch.Draw(texture2D, position, new Rectangle?(new Rectangle(0, 0, texture2D.Width, texture2D.Height)), Color.White * star.twinkle * 0.952f * this.opacity, star.rotation, vector, star.scale * star.twinkle, SpriteEffects.None, 0f);
    //            //	//	}
    //            //	//}
    //            //	Microsoft.Xna.Framework.Rectangle[] array = new Microsoft.Xna.Framework.Rectangle[6];
    //            //	for (int i = 0; i < array.Length; i++)
    //            //	{
    //            //		array[i] = new Microsoft.Xna.Framework.Rectangle(i * 4, 0, 2, 40);
    //            //	}
    //            //	for (int j = 0; j < IllusionBoundMod.rain.Length; j++)
    //            //	{
    //            //		StormRain rain = IllusionBoundMod.rain[j];
    //            //		if (rain != null)
    //            //		{
    //            //			Main.spriteBatch.Draw(Main.rainTexture, rain.position - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(array[(int)rain.type]), (Lighting.GetColor((int)(rain.position.X + 4f) >> 4, (int)(rain.position.Y + 4f) >> 4).ToVector3() * 0.85f + new Vector3(255, 255, 255) * rain.highLight).ToColor(), rain.rotation, Vector2.Zero, rain.scale, SpriteEffects.None, 0f);
    //            //			rain.Update();
    //            //		}
    //            //	}
    //            //}
    //            #endregion

    //        }
    //    }

    //    // Token: 0x06000B89 RID: 2953 RVA: 0x00083DB8 File Offset: 0x00081FB8
    //    public override void Update(GameTime gameTime)
    //    {
    //        if (this.skyActive && this.opacity < 1f)
    //        {
    //            this.opacity += 0.02f;
    //            return;
    //        }
    //        if (!this.skyActive && this.opacity > 0f)
    //        {
    //            this.opacity -= 0.02f;
    //        }
    //    }

    //    // Token: 0x06000B8A RID: 2954 RVA: 0x00004190 File Offset: 0x00002390
    //    public override float GetCloudAlpha()
    //    {
    //        return (1f - this.opacity) * 0.97f + 0.03f;
    //    }

    //    // Token: 0x04000368 RID: 872
    //    private bool skyActive;

    //    // Token: 0x04000369 RID: 873
    //    private float opacity;
    //}

    public class SkyEffectPlayer : ModPlayer
    {
        public override void Load()
        {
            SkyManager.Instance["VirtualDream:ErchiusHorrorSky"] = new ErchiusHorrorSky();
            SkyManager.Instance["VirtualDream:BigApeSky"] = new BigApeSky();
        }
        public override void ResetEffects()
        {
            //for (int n = 0; n < 200; n++) if (Main.npc[n].type == Terraria.ID.NPCID.WallofFlesh) Main.npc[n].active = false;
            if (!ErchiusHorrorSky.SkyActive && NPC.AnyNPCs(ModContent.NPCType<Contents.StarBound.NPCs.Bosses.ErchiusHorror.ErchiusHorror>()))
            {
                SkyManager.Instance.Activate("VirtualDream:ErchiusHorrorSky");
                ErchiusHorrorSky.SkyActive = true;
            }
            if (ErchiusHorrorSky.SkyActive && !NPC.AnyNPCs(ModContent.NPCType<Contents.StarBound.NPCs.Bosses.ErchiusHorror.ErchiusHorror>()))
            {
                SkyManager.Instance.Deactivate("VirtualDream:ErchiusHorrorSky");
                ErchiusHorrorSky.SkyActive = false;
            }
            if (!BigApeSky.SkyActive && NPC.AnyNPCs(ModContent.NPCType<Contents.StarBound.NPCs.Bosses.BigApe.BigApe>()))
            {
                SkyManager.Instance.Activate("VirtualDream:BigApeSky");
                BigApeSky.SkyActive = true;
            }
            if (BigApeSky.SkyActive && !NPC.AnyNPCs(ModContent.NPCType<Contents.StarBound.NPCs.Bosses.BigApe.BigApe>()))
            {
                SkyManager.Instance.Deactivate("VirtualDream:BigApeSky");
                BigApeSky.SkyActive = false;
            }
        }
    }
    public class BigApeSky : CustomSky
    {
        // Token: 0x060024DD RID: 9437 RVA: 0x0048067C File Offset: 0x0047E87C
        public override void OnLoad()
        {
            //this._planetTexture = TextureManager.Load("Images/Misc/VortexSky/Planet");
            //this._bgTexture = TextureManager.Load("Images/Misc/VortexSky/Background");
            //this._boltTexture = TextureManager.Load("Images/Misc/VortexSky/Bolt");
            //this._flashTexture = TextureManager.Load("Images/Misc/VortexSky/Flash");
            this._planetTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/VortexSky/Planet", false);
            this._bgTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/VortexSky/Background", false);
            this._boltTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/VortexSky/Bolt", false);
            this._flashTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/VortexSky/Flash", false);
        }

        // Token: 0x060024DE RID: 9438 RVA: 0x004806CC File Offset: 0x0047E8CC
        public override void Update(GameTime gameTime)
        {
            if (this._isActive)
            {
                this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
            }
            else
            {
                this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
            }
            if (this._ticksUntilNextBolt <= 0)
            {
                this._ticksUntilNextBolt = this._random.Next(1, 5);
                int num = 0;
                while (this._bolts[num].IsAlive && num != this._bolts.Length - 1)
                {
                    num++;
                }
                this._bolts[num].IsAlive = true;
                this._bolts[num].Position.X = this._random.NextFloat() * ((float)Main.maxTilesX * 16f + 4000f) - 2000f;
                this._bolts[num].Position.Y = this._random.NextFloat() * 500f;
                this._bolts[num].Depth = this._random.NextFloat() * 8f + 2f;
                this._bolts[num].Life = 30;
            }
            this._ticksUntilNextBolt--;
            for (int i = 0; i < this._bolts.Length; i++)
            {
                if (this._bolts[i].IsAlive)
                {
                    Bolt[] bolts = this._bolts;
                    int num2 = i;
                    bolts[num2].Life = bolts[num2].Life - 1;
                    if (this._bolts[i].Life <= 0)
                    {
                        this._bolts[i].IsAlive = false;
                    }
                }
            }
        }

        // Token: 0x060024DF RID: 9439 RVA: 0x00480890 File Offset: 0x0047EA90
        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, this._fadeOpacity * 0.5f));
        }

        // Token: 0x060024E0 RID: 9440 RVA: 0x004808C4 File Offset: 0x0047EAC4
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(IllusionBoundMod.GetTexture("Backgrounds/WhiteSky"), new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Lerp(new Color(0, 255, 255), new Color(0, 0, 255), (float)Math.Sin(IllusionBoundMod.ModTime / 180f * MathHelper.TwoPi) * .5f + .5f) * _fadeOpacity);//Main.bgColor//Color.White

                //spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
                //spriteBatch.Draw(this._bgTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f) * this._fadeOpacity);
                //            Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
                //Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
                //spriteBatch.Draw(this._planetTexture, value + new Vector2(-200f, -200f) + value2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width >> 1), (float)(this._planetTexture.Height >> 1)), 1f, SpriteEffects.None, 1f);
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < this._bolts.Length; i++)
            {
                if (this._bolts[i].IsAlive && this._bolts[i].Depth > minDepth && this._bolts[i].Depth < maxDepth)
                {
                    Vector2 vector = new Vector2(1f / this._bolts[i].Depth, 0.9f / this._bolts[i].Depth);
                    Vector2 vector2 = (this._bolts[i].Position - value3) * vector + value3 - Main.screenPosition;
                    if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
                    {
                        Texture2D texture = this._boltTexture;
                        int life = this._bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = this._flashTexture;
                        }
                        float scale2 = (float)life / 30f;
                        spriteBatch.Draw(texture, vector2, null, Color.White * scale * scale2 * this._fadeOpacity, 0f, Vector2.Zero, vector.X * 5f, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        // Token: 0x060024E1 RID: 9441 RVA: 0x0001A072 File Offset: 0x00018272
        public override float GetCloudAlpha()
        {
            return (1f - this._fadeOpacity) * 0.3f + 0.7f;
        }

        // Token: 0x060024E2 RID: 9442 RVA: 0x00480C30 File Offset: 0x0047EE30
        public override void Activate(Vector2 position, params object[] args)
        {
            this._fadeOpacity = 0.002f;
            this._isActive = true;
            this._bolts = new Bolt[500];
            for (int i = 0; i < this._bolts.Length; i++)
            {
                this._bolts[i].IsAlive = false;
            }
        }

        // Token: 0x060024E3 RID: 9443 RVA: 0x0001A08C File Offset: 0x0001828C
        public override void Deactivate(params object[] args)
        {
            this._isActive = false;
        }

        // Token: 0x060024E4 RID: 9444 RVA: 0x0001A08C File Offset: 0x0001828C
        public override void Reset()
        {
            this._isActive = false;
        }

        // Token: 0x060024E5 RID: 9445 RVA: 0x0001A095 File Offset: 0x00018295
        public override bool IsActive()
        {
            return this._isActive || this._fadeOpacity > 0.001f;
        }

        // Token: 0x040040AB RID: 16555
        private UnifiedRandom _random = new UnifiedRandom();

        // Token: 0x040040AC RID: 16556
        private Texture2D _planetTexture;

        // Token: 0x040040AD RID: 16557
        private Texture2D _bgTexture;

        // Token: 0x040040AE RID: 16558
        private Texture2D _boltTexture;

        // Token: 0x040040AF RID: 16559
        private Texture2D _flashTexture;

        // Token: 0x040040B0 RID: 16560
        private bool _isActive;

        // Token: 0x040040B1 RID: 16561
        private int _ticksUntilNextBolt;

        // Token: 0x040040B2 RID: 16562
        private float _fadeOpacity;

        // Token: 0x040040B3 RID: 16563
        private Bolt[] _bolts;

        // Token: 0x02000423 RID: 1059
        private struct Bolt
        {
            // Token: 0x040040B4 RID: 16564
            public Vector2 Position;

            // Token: 0x040040B5 RID: 16565
            public float Depth;

            // Token: 0x040040B6 RID: 16566
            public int Life;

            // Token: 0x040040B7 RID: 16567
            public bool IsAlive;
        }
        public static bool SkyActive;
    }
    public class ErchiusHorrorSky : CustomSky
    {
        public static bool SkyActive;

        public override void OnLoad()
        {
            this._planetTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/NebulaSky/Planet", false);
            this._bgTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/NebulaSky/Background", false);
            this._beamTexture = IllusionBoundMod.GetTexture("Terraria/Images/Misc/NebulaSky/Beam", false);
            this._rockTextures = new Texture2D[3];
            for (int i = 0; i < this._rockTextures.Length; i++)
            {
                this._rockTextures[i] = IllusionBoundMod.GetTexture("Terraria/Images/Misc/NebulaSky/Rock_" + i, false);
            }
        }

        // Token: 0x06002497 RID: 9367 RVA: 0x0047E294 File Offset: 0x0047C494
        public override void Update(GameTime gameTime)
        {
            if (this._isActive)
            {
                this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
                return;
            }
            this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
        }

        // Token: 0x06002498 RID: 9368 RVA: 0x0047E2E4 File Offset: 0x0047C4E4
        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, this._fadeOpacity * 0.5f));
        }

        // Token: 0x06002499 RID: 9369 RVA: 0x0047E318 File Offset: 0x0047C518
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(IllusionBoundMod.GetTexture("Backgrounds/WhiteSky"), new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Lerp(Color.Purple, Color.Pink, (float)Math.Sin(IllusionBoundMod.ModTime / 180f * MathHelper.TwoPi) * .5f + .5f) * _fadeOpacity);//Main.bgColor//Color.White

                //spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
                //spriteBatch.Draw(this._bgTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * this._fadeOpacity));
                //Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
                //Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
                //spriteBatch.Draw(this._planetTexture, value + new Vector2(-200f, -200f) + value2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width >> 1), (float)(this._planetTexture.Height >> 1)), 1f, SpriteEffects.None, 1f);
            }
            int num = -1;
            int num2 = 0;
            for (int i = 0; i < this._pillars.Length; i++)
            {
                float depth = this._pillars[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            for (int j = num; j < num2; j++)
            {
                Vector2 vector = new Vector2(1f / this._pillars[j].Depth, 0.9f / this._pillars[j].Depth);
                Vector2 vector2 = this._pillars[j].Position;
                vector2 = (vector2 - value3) * vector + value3 - Main.screenPosition;
                if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
                {
                    float num3 = vector.X * 450f;
                    spriteBatch.Draw(this._beamTexture, vector2, null, Color.White * 0.2f * scale * this._fadeOpacity, 0f, Vector2.Zero, new Vector2(num3 / 70f, num3 / 45f), SpriteEffects.None, 0f);
                    int num4 = 0;
                    for (float num5 = 0f; num5 <= 1f; num5 += 0.03f)
                    {
                        float num6 = 1f - (num5 + Main.GlobalTimeWrappedHourly * 0.02f + (float)Math.Sin((double)((float)j))) % 1f;
                        spriteBatch.Draw(this._rockTextures[num4], vector2 + new Vector2((float)Math.Sin((double)(num5 * 1582f)) * (num3 * 0.5f) + num3 * 0.5f, num6 * 2000f), null, Color.White * num6 * scale * this._fadeOpacity, num6 * 20f, new Vector2((float)(this._rockTextures[num4].Width >> 1), (float)(this._rockTextures[num4].Height >> 1)), 0.9f, SpriteEffects.None, 0f);
                        num4 = (num4 + 1) % this._rockTextures.Length;
                    }
                }
            }
        }

        // Token: 0x0600249A RID: 9370 RVA: 0x00019D3B File Offset: 0x00017F3B
        public override float GetCloudAlpha()
        {
            return (1f - this._fadeOpacity) * 0.3f + 0.7f;
        }

        // Token: 0x0600249B RID: 9371 RVA: 0x0047E764 File Offset: 0x0047C964
        public override void Activate(Vector2 position, params object[] args)
        {
            this._fadeOpacity = 0.002f;
            this._isActive = true;
            this._pillars = new LightPillar[40];
            for (int i = 0; i < this._pillars.Length; i++)
            {
                this._pillars[i].Position.X = (float)i / (float)this._pillars.Length * ((float)Main.maxTilesX * 16f + 20000f) + this._random.NextFloat() * 40f - 20f - 20000f;
                this._pillars[i].Position.Y = this._random.NextFloat() * 200f - 2000f;
                this._pillars[i].Depth = this._random.NextFloat() * 8f + 7f;
            }
            Array.Sort<LightPillar>(this._pillars, new Comparison<LightPillar>(this.SortMethod));
        }

        // Token: 0x0600249C RID: 9372 RVA: 0x00019D55 File Offset: 0x00017F55
        private int SortMethod(LightPillar pillar1, LightPillar pillar2)
        {
            return pillar2.Depth.CompareTo(pillar1.Depth);
        }

        // Token: 0x0600249D RID: 9373 RVA: 0x00019D69 File Offset: 0x00017F69
        public override void Deactivate(params object[] args)
        {
            this._isActive = false;
        }

        // Token: 0x0600249E RID: 9374 RVA: 0x00019D69 File Offset: 0x00017F69
        public override void Reset()
        {
            this._isActive = false;
        }

        // Token: 0x0600249F RID: 9375 RVA: 0x00019D72 File Offset: 0x00017F72
        public override bool IsActive()
        {
            return this._isActive || this._fadeOpacity > 0.001f;
        }

        // Token: 0x04004060 RID: 16480
        private LightPillar[] _pillars;

        // Token: 0x04004061 RID: 16481
        private UnifiedRandom _random = new UnifiedRandom();

        // Token: 0x04004062 RID: 16482
        private Texture2D _planetTexture;

        // Token: 0x04004063 RID: 16483
        private Texture2D _bgTexture;

        // Token: 0x04004064 RID: 16484
        private Texture2D _beamTexture;

        // Token: 0x04004065 RID: 16485
        private Texture2D[] _rockTextures;

        // Token: 0x04004066 RID: 16486
        private bool _isActive;

        // Token: 0x04004067 RID: 16487
        private float _fadeOpacity;

        // Token: 0x02000418 RID: 1048
        private struct LightPillar
        {
            // Token: 0x04004068 RID: 16488
            public Vector2 Position;

            // Token: 0x04004069 RID: 16489
            public float Depth;
        }
    }
}
