using ReLogic.Graphics;
using System;
using Terraria.DataStructures;

namespace VirtualDream
{
    public class VirtualDreamPlayer : ModPlayer
    {
        public static float screenShakeStrength;

        public int oldLifeMax;

        public float poisionLifeCostPerSecond;
        public float lifeCostCount;
        public override void PostUpdate()
        {
            oldLifeMax = Player.statLifeMax2;
        }
        public override void ResetEffects()
        {
            screenShakeStrength = 0;
            if (poisionLifeCostPerSecond > 0)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                lifeCostCount += oldLifeMax * poisionLifeCostPerSecond / 100f;
            }
            int lifeCost = (int)(lifeCostCount / 60);
            lifeCostCount -= lifeCost * 60;
            //while (lifeCostCount - 60 > 0)
            //{
            //    lifeCostCount -= 60;
            //    //Player.statLife -= 1;
            //    lifeCost++;
            //}
            //CombatText.NewText(Player.Hitbox, Color.Purple, Player.statDefense);
            if (lifeCost > 0) Player.HealEffect(-lifeCost);
            Player.statLife -= lifeCost;
            poisionLifeCostPerSecond = 0;
        }
        public override void ModifyScreenPosition()
        {
            if (screenShakeStrength > 0) Main.screenPosition += screenShakeStrength * Main.rand.NextFloat(0, 1) * Main.rand.NextVector2Unit();
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            //Main.spriteBatch.DrawString(FontAssets.MouseText.Value, (Player.skinColor, Player.eyeColor, Player.hairColor, Player.hair).ToString(), Player.Center + new Vector2(-128, -50) - Main.screenPosition, Color.White);
        }
    }


    //public class trydrawFlame : PlayerDrawLayer
    //{
    //    int k = 0;
    //    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    //    {

    //        return base.GetDefaultVisibility(drawInfo);
    //    }
    //    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);

    //    protected override void Draw(ref PlayerDrawSet drawInfo)
    //    {
    //    }
    //}
    //public class MyArray<T>
    //{
    //    public T[]? array;
    //    public T this[int index]
    //    {
    //        get
    //        {
    //            try
    //            {
    //                return array[index];
    //            }
    //            catch (Exception ex)
    //            {
    //                Main.NewText(ex.ToString() + "索引为：" + index);
    //                return default;
    //            }
    //        }
    //        set
    //        {
    //            try
    //            {
    //                array[index] = value;
    //            }
    //            catch (Exception ex)
    //            {
    //                Main.NewText(ex.ToString() + "索引为：" + index);
    //            }
    //        }
    //    }
    //}
    //public class drawFlameModplayer : ModPlayer
    //{
    //    public Dust[] d = new Dust[1000];
    //    Asset<Texture2D> texture;
    //    public int dFindex = 0;
    //    public int dFamount = 0;
    //    public int[] frameCounter;
    //    public int[] frame;
    //    MyArray<drawFlameInfo> dF = new MyArray<drawFlameInfo> { array = new drawFlameInfo[100] };


    //    public override void FrameEffects()
    //    {
    //        if (Main.myPlayer == Player.whoAmI)
    //        {

    //            for (int i = 0; i < 100; i++)
    //            {
    //                int index = i;
    //                if (index >= 100) index -= 100;
    //                if (dF[index] == null) dF[index] = new drawFlameInfo(Player.Center, IllusionBoundMod.GetTexture("升腾火焰"));
    //                if (dF[index] != null && dF[index].active)
    //                {
    //                    //Main.NewText(123456);
    //                    dF[index].渐变系数++;
    //                    dF[index].alpha = (int)MathHelper.Lerp(0, 255, (dF[index].渐变系数 * dF[index].渐变系数) / 3600f);
    //                    if (dF[index].随机火焰位置 == null)
    //                    {
    //                        dF[index].随机火焰位置 = new Vector2[dF[index].随机火焰数量];
    //                        dF[index].随机火焰大小 = new float[dF[index].随机火焰数量];
    //                        for (int k = 0; k < dF[index].随机火焰数量; k++)
    //                        {
    //                            dF[index].随机火焰位置[k] = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-18, 18));
    //                            dF[index].随机火焰大小[k] = Main.rand.NextFloat(1.6f, 2f);

    //                        }
    //                    }
    //                    if (dF[index].frameCounter++ % 6 == 0)
    //                    {
    //                        dF[index].frame += 1;
    //                        dF[index].frameCounter = 0;
    //                    }
    //                    if (dF[index].frame >= 16)
    //                    {
    //                        dF[index].frame = 0;
    //                        火焰消失(index);
    //                    }
    //                    if (dF[index].alpha > 240)
    //                    {
    //                        火焰消失(index);
    //                    }
    //                    //Main.NewText("a"+dF[index].随机火焰位置[i] + " " + dF[index].随机火焰大小[i]);
    //                    //Main.NewText(dFindex + " " + dFamount + " " + dF[index].Position);
    //                }
    //            }
    //        }

    //    }

    //    public void 创造火焰(Vector2 pos)
    //    {
    //        Main.NewText("开始" + dFindex + " " + dFamount);
    //        dF[dFindex + dFamount] = new drawFlameInfo(pos, IllusionBoundMod.GetTexture("升腾火焰"));
    //        dF[dFindex].active = true;
    //        dFamount++;
    //    }
    //    public void 火焰消失(int index)
    //    {
    //        dFindex++;
    //        if (dFindex >= 100) dFindex -= 100;
    //        dFamount--;
    //        dF[index].active = false;
    //        Main.NewText("消失" + dFindex + " " + dFamount);
    //    }
    //    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    //    {
    //        if (Main.myPlayer == Player.whoAmI)
    //        {
    //            //for (int i = dFindex; i < (dFindex + dFamount); i++)
    //            /*for (int i = 0; i < 100; i++)
    //            {
    //                int index = i;
    //                if (index >= 100) index -= 100;
    //                // + dF[i].随机火焰位置[index] + " " + dF[index].随机火焰大小[index]);

    //                if (dF[index].active && dF[index].alpha < 240)
    //                {
    //                    Main.spriteBatch.End();
    //                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap,
    //                        DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
    //                    //Main.NewText("绘制" + i+"数量："+ dF[i].随机火焰数量);
    //                    Main.NewText("b" + dF[i].随机火焰位置[index] + " " + dF[index].随机火焰大小[index]);
    //                    //Main.NewText("b");
    //                    绘制火焰( dF[i]);
    //                    Main.spriteBatch.End();
    //                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState,
    //                        DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
    //                }
    //            }*/
    //            for (int n = 0; n < dF.array.Length; n++) 
    //            {
    //                if (dF[n].active) 绘制火焰(dF[n]);
    //            }
    //        }
    //    }

    //    void 绘制火焰(drawFlameInfo df)
    //    {
    //        Vector2 origin = Terraria.Utils.Size(df.texture) / new Vector2(4f, 4f) * 0.5f;//除3相当于以图片中心为position.除以X,Y,此时为从上往下，从左往右的帧图
    //        int frameWidth = df.texture.Width / 4;//图片总高度除以长度，得到每张图的长度
    //        int frameHeight = df.texture.Height / 4;//图片总高度除以高度，得到每张图的高度
    //        int startX = frameWidth * ((df.frame) % 4);//每一帧的起始坐标X
    //        int startY = frameHeight * ((df.frame) / 4);//每一帧的起始坐标Y
    //        Rectangle sourceRectangle = new Rectangle(startX, startY, frameWidth, frameHeight);//坐标x，坐标y，图片长度，图片高度，得到一张完整图片

    //        Color c = new Color(255, 255, 255);
    //        c.A = (byte)(255f - df.alpha);
    //        //Main.NewText( "数量2：" + df.随机火焰数量);
    //        for (int i = 0; i < df.随机火焰数量; i++)
    //        {
    //            Vector2 randomPosition = df.随机火焰位置[i];
    //            float 逐渐减小 = MathHelper.Lerp(1, 0, df.alpha / 255f);
    //            Main.spriteBatch.Draw(df.texture, df.Position + randomPosition - Main.screenPosition, sourceRectangle, c, 0f, origin, df.随机火焰大小[i] * 逐渐减小, SpriteEffects.None, 0f);
    //        }

    //    }
    //    class drawFlameInfo
    //    {
    //        public Vector2 Position;
    //        public Color Color = new Color(255, 255, 255);
    //        public Texture2D texture;
    //        public int 渐变系数 = 0;
    //        public int 随机火焰数量 = Main.rand.Next(1, 6);
    //        public Vector2[] 随机火焰位置;
    //        public float[] 随机火焰大小;
    //        public float alpha = 0;
    //        public int frameCounter = 0;
    //        public int frame = 0;
    //        public bool active = false;
    //        public drawFlameInfo(Vector2 Position, Texture2D tex)
    //        {
    //            texture = tex;
    //            随机火焰位置 = null;
    //            随机火焰大小 = null;
    //            this.Position = Position;
    //        }
    //    };


    //}
}
