using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//using VirtualDream.Tiles.StormZone;
//using VirtualDream.Items.Weapons.AprilFoolHammers;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Xna.Framework.Graphics;

using Terraria.UI.Chat;
using Terraria.Utilities;

using VirtualDream.Utils.BaseClasses;

namespace VirtualDream.Utils
{
    public static class DefaultColors
    {
        //废弃类，直接using static Microsoft.Xna.Framework.Color;就行...
        public static Color GetColor(uint value)
        {
            return new Color() { PackedValue = value };
        }
        public static Color Transparent => GetColor(0u);

        public static Color AliceBlue => GetColor(4294965488u);

        public static Color AntiqueWhite => GetColor(4292340730u);

        public static Color Aqua => GetColor(4294967040u);

        public static Color Aquamarine => GetColor(4292149119u);

        public static Color Azure => GetColor(4294967280u);

        public static Color Beige => GetColor(4292670965u);

        public static Color Bisque => GetColor(4291093759u);

        public static Color Black => GetColor(4278190080u);

        public static Color BlanchedAlmond => GetColor(4291685375u);

        public static Color Blue => GetColor(4294901760u);

        public static Color BlueViolet => GetColor(4293012362u);

        public static Color Brown => GetColor(4280953509u);

        public static Color BurlyWood => GetColor(4287084766u);

        public static Color CadetBlue => GetColor(4288716383u);

        public static Color Chartreuse => GetColor(4278255487u);

        public static Color Chocolate => GetColor(4280183250u);

        public static Color Coral => GetColor(4283465727u);

        public static Color CornflowerBlue => GetColor(4293760356u);

        public static Color Cornsilk => GetColor(4292671743u);

        public static Color Crimson => GetColor(4282127580u);

        public static Color Cyan => GetColor(4294967040u);

        public static Color DarkBlue => GetColor(4287299584u);

        public static Color DarkCyan => GetColor(4287335168u);

        public static Color DarkGoldenrod => GetColor(4278945464u);

        public static Color DarkGray => GetColor(4289309097u);

        public static Color DarkGreen => GetColor(4278215680u);

        public static Color DarkKhaki => GetColor(4285249469u);

        public static Color DarkMagenta => GetColor(4287299723u);

        public static Color DarkOliveGreen => GetColor(4281297749u);

        public static Color DarkOrange => GetColor(4278226175u);

        public static Color DarkOrchid => GetColor(4291572377u);

        public static Color DarkRed => GetColor(4278190219u);

        public static Color DarkSalmon => GetColor(4286224105u);

        public static Color DarkSeaGreen => GetColor(4287347855u);

        public static Color DarkSlateBlue => GetColor(4287315272u);

        public static Color DarkSlateGray => GetColor(4283387695u);

        public static Color DarkTurquoise => GetColor(4291939840u);

        public static Color DarkViolet => GetColor(4292018324u);

        public static Color DeepPink => GetColor(4287829247u);

        public static Color DeepSkyBlue => GetColor(4294950656u);

        public static Color DimGray => GetColor(4285098345u);

        public static Color DodgerBlue => GetColor(4294938654u);

        public static Color Firebrick => GetColor(4280427186u);

        public static Color FloralWhite => GetColor(4293982975u);

        public static Color ForestGreen => GetColor(4280453922u);

        public static Color Fuchsia => GetColor(4294902015u);

        public static Color Gainsboro => GetColor(4292664540u);

        public static Color GhostWhite => GetColor(4294965496u);

        public static Color Gold => GetColor(4278245375u);

        public static Color Goldenrod => GetColor(4280329690u);

        public static Color Gray => GetColor(4286611584u);

        public static Color Green => GetColor(4278222848u);

        public static Color GreenYellow => GetColor(4281335725u);

        public static Color Honeydew => GetColor(4293984240u);

        public static Color HotPink => GetColor(4290013695u);

        public static Color IndianRed => GetColor(4284243149u);

        public static Color Indigo => GetColor(4286709835u);

        public static Color Ivory => GetColor(4293984255u);

        public static Color Khaki => GetColor(4287424240u);

        public static Color Lavender => GetColor(4294633190u);

        public static Color LavenderBlush => GetColor(4294308095u);

        public static Color LawnGreen => GetColor(4278254716u);

        public static Color LemonChiffon => GetColor(4291689215u);

        public static Color LightBlue => GetColor(4293318829u);

        public static Color LightCoral => GetColor(4286611696u);

        public static Color LightCyan => GetColor(4294967264u);

        public static Color LightGoldenrodYellow => GetColor(4292016890u);

        public static Color LightGreen => GetColor(4287688336u);

        public static Color LightGray => GetColor(4292072403u);

        public static Color LightPink => GetColor(4290885375u);

        public static Color LightSalmon => GetColor(4286226687u);

        public static Color LightSeaGreen => GetColor(4289376800u);

        public static Color LightSkyBlue => GetColor(4294626951u);

        public static Color LightSlateGray => GetColor(4288252023u);

        public static Color LightSteelBlue => GetColor(4292789424u);

        public static Color LightYellow => GetColor(4292935679u);

        public static Color Lime => GetColor(4278255360u);

        public static Color LimeGreen => GetColor(4281519410u);

        public static Color Linen => GetColor(4293325050u);

        public static Color Magenta => GetColor(4294902015u);

        public static Color Maroon => GetColor(4278190208u);

        public static Color MediumAquamarine => GetColor(4289383782u);

        public static Color MediumBlue => GetColor(4291624960u);

        public static Color MediumOrchid => GetColor(4292040122u);

        public static Color MediumPurple => GetColor(4292571283u);

        public static Color MediumSeaGreen => GetColor(4285641532u);

        public static Color MediumSlateBlue => GetColor(4293814395u);

        public static Color MediumSpringGreen => GetColor(4288346624u);

        public static Color MediumTurquoise => GetColor(4291613000u);

        public static Color MediumVioletRed => GetColor(4286911943u);

        public static Color MidnightBlue => GetColor(4285536537u);

        public static Color MintCream => GetColor(4294639605u);

        public static Color MistyRose => GetColor(4292994303u);

        public static Color Moccasin => GetColor(4290110719u);

        public static Color NavajoWhite => GetColor(4289584895u);

        public static Color Navy => GetColor(4286578688u);

        public static Color OldLace => GetColor(4293326333u);

        public static Color Olive => GetColor(4278222976u);

        public static Color OliveDrab => GetColor(4280520299u);

        public static Color Orange => GetColor(4278232575u);

        public static Color OrangeRed => GetColor(4278207999u);

        public static Color Orchid => GetColor(4292243674u);

        public static Color PaleGoldenrod => GetColor(4289390830u);

        public static Color PaleGreen => GetColor(4288215960u);

        public static Color PaleTurquoise => GetColor(4293848751u);

        public static Color PaleVioletRed => GetColor(4287852763u);

        public static Color PapayaWhip => GetColor(4292210687u);

        public static Color PeachPuff => GetColor(4290370303u);

        public static Color Peru => GetColor(4282353101u);

        public static Color Pink => GetColor(4291543295u);

        public static Color Plum => GetColor(4292714717u);

        public static Color PowderBlue => GetColor(4293320880u);

        public static Color Purple => GetColor(4286578816u);

        public static Color Red => GetColor(4278190335u);

        public static Color RosyBrown => GetColor(4287598524u);

        public static Color RoyalBlue => GetColor(4292962625u);

        public static Color SaddleBrown => GetColor(4279453067u);

        public static Color Salmon => GetColor(4285694202u);

        public static Color SandyBrown => GetColor(4284523764u);

        public static Color SeaGreen => GetColor(4283927342u);

        public static Color SeaShell => GetColor(4293850623u);

        public static Color Sienna => GetColor(4281160352u);

        public static Color Silver => GetColor(4290822336u);

        public static Color SkyBlue => GetColor(4293643911u);

        public static Color SlateBlue => GetColor(4291648106u);

        public static Color SlateGray => GetColor(4287660144u);

        public static Color Snow => GetColor(4294638335u);

        public static Color SpringGreen => GetColor(4286578432u);

        public static Color SteelBlue => GetColor(4290019910u);

        public static Color Tan => GetColor(4287411410u);

        public static Color Teal => GetColor(4286611456u);

        public static Color Thistle => GetColor(4292394968u);

        public static Color Tomato => GetColor(4282868735u);

        public static Color Turquoise => GetColor(4291878976u);

        public static Color Violet => GetColor(4293821166u);

        public static Color Wheat => GetColor(4289978101u);

        public static Color White => GetColor(uint.MaxValue);

        public static Color WhiteSmoke => GetColor(4294309365u);

        public static Color Yellow => GetColor(4278255615u);

        public static Color YellowGreen => GetColor(4281519514u);
    }
    public static class IllusionRecipe
    {
        public static void SetResult(this Recipe recipe, ModItem modItem, int stack = 1) => recipe.ReplaceResult(modItem.Type, stack);
        public static void SetResult(this Recipe recipe, int type, int stack = 1) => recipe.ReplaceResult(type, stack);

        public static void AddRecipe(this Recipe recipe) => recipe.Register();
    }
    public static class IllusionBoundExtensionMethods
    {
        public static float Lerp(this float t, float from, float to, bool clamp = false)
        {
            if (clamp)
            {
                t = MathHelper.Clamp(t, 0, 1);
            }

            return (1 - t) * from + t * to;
        }
        public static Color GetColorFromTex(this Texture2D texture, Vector2 texcoord)
        {
            var w = texture.Width;
            var h = texture.Height;
            var cs = new Color[w * h];
            texture.GetData(cs);
            return cs[(int)(texcoord.X * w) + (int)(texcoord.Y * h) * w];
        }
        /// <summary>
        /// 阿汪超喜欢用的插值函数，获得一个先迅速增加再慢慢变小的插值
        /// </summary>
        /// <param name="value">丢进去的变量，取值范围一般是[0,maxTimeWhen]</param>
        /// <param name="maxTimeWhen">什么时候插值结束呢</param>
        /// <returns>自己画函数图像去，真的像是一个小山丘一样(</returns>
        public static float HillFactor2(this float value, float maxTimeWhen = 1)
        {
            //return Clamp((center - Math.Abs(center - value)) / center / whenGetMax, 0, 1);
            return (1 - (float)Math.Cos(MathHelper.TwoPi * Math.Sqrt(value / maxTimeWhen))) * 0.5f;
        }
        /// <summary>
        /// 阿汪超喜欢用的插值函数，获得一个先迅速增加再慢慢变小的插值
        /// </summary>
        /// <param name="value">丢进去的变量，取值范围一般是[0,maxTimeWhen]</param>
        /// <param name="maxTimeWhen">什么时候插值结束呢</param>
        /// <returns>自己画函数图像去，真的像是一个小山丘一样(</returns>
        public static float HillFactor(this float value, float maxTimeWhen = 1)
        {
            //return Clamp((center - Math.Abs(center - value)) / center / whenGetMax, 0, 1);
            return (float)Math.Sin(MathHelper.Pi * Math.Sqrt(value / maxTimeWhen));
        }
        /// <summary>
        /// 阿汪超喜欢用的插值函数，获得一个先上后下的插值
        /// </summary>
        /// <param name="value">丢进去的变量，取值范围一般是[0,2*center]</param>
        /// <param name="center">中间值，或者说最大值点</param>
        /// <param name="whenGetMax">决定丢进去的值与最大值的比值为多少时第一次达到最大值(1)，一般取(0,0.5f]</param>
        /// <returns>自己画函数图像去，不是三角形就是梯形(</returns>
        public static float SymmetricalFactor2(this float value, float center, float whenGetMax)
        {
            //return Clamp((center - Math.Abs(center - value)) / center / whenGetMax, 0, 1);
            return value.SymmetricalFactor(center, whenGetMax * center * 2);
        }

        /// <summary>
        /// 阿汪超喜欢用的插值函数，获得一个先上后下的插值
        /// </summary>
        /// <param name="value">丢进去的变量，取值范围一般是[0,2*center]</param>
		/// <param name="center">中间值，或者说最大值点</param>
		/// <param name="whenGetMax">决定丢进去的值为多少时第一次达到最大值(1)，一般取(0,center]</param>
		/// <returns>自己画函数图像去，不是三角形就是梯形(</returns>
        public static float SymmetricalFactor(this float value, float center, float whenGetMax)
        {
            return MathHelper.Clamp((center - Math.Abs(center - value)) / whenGetMax, 0, 1);
        }
        public static void UpdateArray<T>(this T[] array, T newValue, T defaultValue, bool when = true)
        {
            int length = array.Length;
            bool checkZero = true;
            for (int n = 0; n < length; n++)
            {
                checkZero &= array[n].GetHashCode() == default(T).GetHashCode();
            }
            if (checkZero)
            {
                for (int n = 0; n < length; n++)
                {
                    array[n] = defaultValue;
                }
            }
            else
            {
                if (when)
                {
                    for (int n = length - 1; n > 0; n--)
                    {
                        array[n] = array[n - 1];
                    }
                    array[0] = newValue;
                }

            }
        }
        public static void UpdateArray<T>(this T[] array, T newValue, bool when = true)
        {
            if (when)
            {
                for (int n = array.Length - 1; n > 0; n--)
                {
                    array[n] = array[n - 1];
                }
                array[0] = newValue;
            }
        }
        public static Color GetColor(this Player drawPlayer, Color color)
        {
            return Lighting.GetColor((drawPlayer.Center / 16).ToPoint().X, (drawPlayer.Center / 16).ToPoint().Y, color);
        }
        public static Color GetColor(this Player drawPlayer)
        {
            return Lighting.GetColor((drawPlayer.Center / 16).ToPoint().X, (drawPlayer.Center / 16).ToPoint().Y);
        }

        private static void InsertSort(float[] arr)
        {
            // 检查数据合法性
            if (arr == null)
            {
                return;
            }
            for (int i = 1; i < arr.Length; i++)
            {
                float tmp = arr[i];
                int j;
                for (j = i - 1; j >= 0; j--)
                {
                    //如果比tmp大把值往后移动一位
                    if (arr[j] > tmp)
                    {
                        arr[j + 1] = arr[j];
                    }
                    else
                    {
                        break;
                    }
                }
                arr[j + 1] = tmp;
            }
        }
        public static void Reverse<T>(this T[] values)
        {
            var backup = values.CloneArray();
            for (int n = 0; n < values.Length; n++)
            {
                values[n] = backup[values.Length - n - 1];
            }
        }
        public static Vector2[] ClockwiseSorting(this Vector2[] vectors)
        {
            var result = new Vector2[vectors.Length];
            float? value = null;
            //Vector2 vec = default;
            int index = -1;
            for (int n = 0; n < vectors.Length; n++)
            {
                if (value == null || vectors[n].X < value)
                {
                    value = vectors[n].X;
                    //vec = vectors[n];
                    index = n;
                }
            }
            result[0] = vectors[index];
            Dictionary<float, Vector2> myDic = new Dictionary<float, Vector2>();
            for (int n = 0; n < vectors.Length; n++)
            {
                if (n != index)
                {
                    myDic.Add(Vector2.Dot(new Vector2(0, 1), vectors[n] - result[0]) / (vectors[n] - result[0]).Length(), vectors[n]);
                }
            }
            var myArray = myDic.Keys.ToArray();
            InsertSort(myArray);
            myArray.Reverse();
            for (int n = 0; n < myArray.Length; n++)
            {
                result[n + 1] = myDic[myArray[n]];
            }
            return result;
        }
        public static void DrawOutSide(this SpriteBatch spriteBatch, Vector2[] vectors, float light = 1)
        {
            #region Outside
            //FileStream fileStream = new FileStream(@"D:\\TestTesseract.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            float left = Main.screenPosition.X;
            float right = Main.screenPosition.X + 1920;
            float bottom = Main.screenPosition.Y;
            float top = Main.screenPosition.Y + 1120;

            //binaryWriter.Write("PreEdge");
            var edgePointsNative = vectors.CloneArray();//.EdgePoints()
            //for (int n = 0; n < edgePointsNative.Length; n++)
            //{
            //    //Main.NewText((edgePoints[n], n));
            //    spriteBatch.DrawString(Main.fontMouseText, (edgePointsNative[n], n).ToString(), new Vector2(800, 300 + 24 * n), Color.Red);
            //}
            //binaryWriter.Write("PostEdge");
            //var lep = new LoopArray<Vector2>(edgePointsNative);
            //for (int n = 0; n < lep.Length; n++)
            //{
            //    spriteBatch.DrawLine(lep[n], lep[n + 1], Color.Red, 32, false, -Main.screenPosition);
            //}

            //binaryWriter.Write("PreVertex");

            Vector2[] targetPoints = GetVertexPoints(ref edgePointsNative);//edgePoints.GetVertexPoints();
            //binaryWriter.Write("PostVertex");

            if (targetPoints == null)
            {
                return;
            }

            if (targetPoints[0].X - 200 < left)
            {
                left = targetPoints[0].X - 200;
            }

            if (targetPoints[1].Y + 200 > top)
            {
                top = targetPoints[1].Y + 200;
            }

            if (targetPoints[2].X + 200 > right)
            {
                right = targetPoints[2].X + 200;
            }

            if (targetPoints[3].Y - 200 < bottom)
            {
                bottom = targetPoints[3].Y - 200;
            }

            //CustomVertexInfo[] vertexs = new CustomVertexInfo[edgePoints.Length + 4];
            //LoopArray<CustomVertexInfo> vertexs = new LoopArray<CustomVertexInfo>(new CustomVertexInfo[edgePointsNative.Length + 4]);
            LoopArray<CustomVertexInfo> vertexs = new LoopArray<CustomVertexInfo>(new CustomVertexInfo[edgePointsNative.Length]);
            LoopArray<CustomVertexInfo> vertexs2 = new LoopArray<CustomVertexInfo>(new CustomVertexInfo[4]);

            List<CustomVertexInfo> vertexInfos = new List<CustomVertexInfo>();
            //binaryWriter.Write("PreTri");

            var l = edgePointsNative.Length;
            for (int n = 0; n < l; n++)
            {
                vertexs[n] = edgePointsNative[n].VertexInScreen(Color.Cyan, light);
            }
            vertexs2[0] = new Vector2(left, top).VertexInScreen(Color.Cyan, light);
            vertexs2[1] = new Vector2(right, top).VertexInScreen(Color.Cyan, light);
            vertexs2[2] = new Vector2(right, bottom).VertexInScreen(Color.Cyan, light);
            vertexs2[3] = new Vector2(left, bottom).VertexInScreen(Color.Cyan, light);
            var connecttingVertex = vertexs2[3];
            List<int> indexList = new List<int>();

            for (int n = 0; n < l; n++)
            {
                int index = -1;
                for (int i = 0; i < 4; i++)
                {
                    if (targetPoints[i] == edgePointsNative[n] && !indexList.Contains(i))
                    {
                        index = i;
                        indexList.Add(i);
                        break;
                    }
                }
                if (index == -1)
                {
                    vertexInfos.Add(vertexs[n - 1]);
                    vertexInfos.Add(vertexs[n]);
                    vertexInfos.Add(connecttingVertex);
                }
                else
                {
                    //vertexInfos.Add(connecttingVertex);
                    //vertexInfos.Add(vertexs[n]);
                    //connecttingVertex = vertexs[l + index];
                    //vertexInfos.Add(connecttingVertex);
                    vertexInfos.Add(vertexs[n - 1]);
                    vertexInfos.Add(vertexs[n]);
                    vertexInfos.Add(connecttingVertex);

                    vertexInfos.Add(connecttingVertex);
                    vertexInfos.Add(vertexs[n]);
                    connecttingVertex = vertexs2[index];
                    vertexInfos.Add(connecttingVertex);
                }
            }
            //binaryWriter.Write("PostTri");

            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            Effect effect = IllusionBoundMod.TextureEffect;//IllusionBoundMod.ShaderSwoosh
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(0);
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Backgrounds/StarSky_0");//IllusionBoundMod.MaskColor[4]
            //Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.MaskColor[4];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            //Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            effect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexInfos.ToArray(), 0, vertexInfos.Count / 3);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            //binaryWriter.Flush();
            //binaryWriter.Close();
            //fileStream.Close();
            #endregion
        }
        public static CustomVertexInfo VertexInScreen(this Vector2 vec, Color color, float light = 1)
        {
            return new CustomVertexInfo(vec, color, new Vector3((vec.X - Main.screenPosition.X) / 1920f, (vec.Y - Main.screenPosition.Y) / 1120f, light));
        }
        public static Vector2[] GetVertexPoints(ref Vector2[] points)
        {
            //Vector2[] result = new Vector2[4];
            LoopArray<Vector2> result = new LoopArray<Vector2>(new Vector2[4]);
            float left = float.MaxValue, bottom = float.MaxValue;
            float right = float.MinValue, top = float.MinValue;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //foreach (var vec in points)
            //{
            //    if (vec.X < left)
            //    {
            //        left = vec.X;
            //        result[0] = vec;
            //    }
            //    if (vec.Y > top)
            //    {
            //        top = vec.Y;
            //        result[1] = vec;
            //    }
            //    if (vec.X > right)
            //    {
            //        right = vec.X;
            //        result[2] = vec;
            //    }
            //    if (vec.Y < bottom)
            //    {
            //        bottom = vec.Y;
            //        result[3] = vec;
            //    }
            //}

            for (int n = 0; n < points.Length; n++)
            {
                if (sw.ElapsedTicks >= 10000)
                {
                    return null;
                }

                var vec = points[n];
                //if (!result.array.Contains(vec)) 
                //{
                //    if (vec.X < left)
                //    {
                //        left = vec.X;
                //        result[0] = vec;
                //    }
                //    if (vec.Y > top)
                //    {
                //        top = vec.Y;
                //        result[1] = vec;
                //    }
                //    if (vec.X > right)
                //    {
                //        right = vec.X;
                //        result[2] = vec;
                //    }
                //    if (vec.Y < bottom)
                //    {
                //        bottom = vec.Y;
                //        result[3] = vec;
                //    }
                //}
                if (vec.X < left)
                {
                    left = vec.X;
                    result[0] = vec;
                }
                if (vec.Y > top)
                {
                    top = vec.Y;
                    result[1] = vec;
                }
                if (vec.X > right)
                {
                    right = vec.X;
                    result[2] = vec;
                }
                if (vec.Y < bottom)
                {
                    bottom = vec.Y;
                    result[3] = vec;
                }
            }
            //if ((result[0] == result[1] && result[2] == result[3]) || (result[2] == result[1] && result[0] == result[3])) 
            //{
            //    return null;
            //}
            List<Vector2> newPoints = points.ToList();

            for (int n = 0; n < 4; n++)
            {
                if (sw.ElapsedTicks >= 10000)
                {
                    return null;
                }

                if (result[n] == result[n + 1])
                {
                    for (int i = 0; i < newPoints.Count; i++)
                    {
                        if (result[n] == newPoints[i])
                        {
                            newPoints.Insert(i, result[n]);
                            break;
                        }

                    }
                    //var points2 = new Vector2[points.Length + 1];
                    //int offset = 0;
                    //for (int i = 0; i < points.Length; i++)
                    //{
                    //    points2[i + offset] = points[i];
                    //    if (points[i] == result[n])
                    //    {
                    //        offset++;
                    //        points2[i + offset] = points[i];
                    //        //break;
                    //    }
                    //}
                    //points = points2;

                    //for (int k = 0; k < points.Length; k++)
                    //{
                    //    Main.NewText(points[k]);
                    //}
                }
            }
            points = newPoints.ToArray();
            //for (int k = 0; k < points.Length; k++)
            //{
            //    Main.NewText(points[k]);
            //}
            return result;
        }
        public static Vector2[] GetVertexPoints(this Vector2[] points)
        {
            Vector2[] result = new Vector2[4];
            float left = float.MaxValue, bottom = float.MaxValue;
            float right = float.MinValue, top = float.MinValue;
            foreach (var vec in points)
            {
                if (vec.X < left)
                {
                    left = vec.X;
                    result[0] = vec;
                }
                if (vec.Y > top)
                {
                    top = vec.Y;
                    result[1] = vec;
                }
                if (vec.X > right)
                {
                    right = vec.X;
                    result[2] = vec;
                }
                if (vec.Y < bottom)
                {
                    bottom = vec.Y;
                    result[3] = vec;
                }
            }
            return result;
        }
        public static T[] DifferenceSet<T>(this T[] A, IEnumerable<T> B)
        {
            List<T> result = new List<T>();
            for (int n = 0; n < A.Length; n++)
            {
                T item = A[n];
                if (!B.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }
        public static Vector2[] EdgePoints(this Vector2[] vecs, out Vector2 left)
        {
            if (vecs.Length < 3)
            {
                throw new ArgumentException("兄啊，三个点都没有，计算个锤子的凸包");
            }

            int index = -1;
            float? leftcoord = null;
            for (int n = 0; n < vecs.Length; n++)
            {
                if (leftcoord == null || leftcoord > vecs[n].X)
                {
                    leftcoord = vecs[n].X;
                    index = n;
                }
                //leftcoord = (leftcoord == null || leftcoord > vecs[n].X) ? vecs[n].X : leftcoord;
                //index = n;
            }
            var vec = vecs[index];
            left = vec;
            List<Vector2> result = new List<Vector2>() { vec };
            do
            {
                Vector2 dir = new Vector2(0, -1);
                float value = -20000;
                foreach (var v in vecs)//.DifferenceSet(result)
                {
                    if (v != vec)
                    {
                        Vector2 _dir = v - vec;
                        float dot = Vector2.Dot(_dir, dir) / _dir.Length();
                        if (dot > value)
                        {
                            value = dot;
                            dir = _dir;
                            vec = v;
                        }
                    }
                }
                if (vec != result[0])
                {
                    result.Add(vec);
                }
            }
            while (vec == result[0]);
            return result.ToArray();
        }
        //public static Vector2[] EdgePoints(this Vector2[] vecs)
        //{
        //    if (vecs.Length < 3) throw new ArgumentException("兄啊，三个点都没有，计算个锤子的凸包");
        //    int index = -1;
        //    float? leftcoord = null;
        //    for (int n = 0; n < vecs.Length; n++)
        //    {
        //        if (leftcoord == null || leftcoord > vecs[n].X)
        //        {
        //            leftcoord = vecs[n].X;
        //            index = n;
        //        }
        //        //leftcoord = (leftcoord == null || leftcoord > vecs[n].X) ? vecs[n].X : leftcoord;
        //        //index = n;
        //    }
        //    var vec = vecs[index];
        //    List<Vector2> result = new List<Vector2>() { vec };
        //    do
        //    {
        //        Vector2 dir = new Vector2(0, -1);
        //        float value = -20000;
        //        foreach (var v in vecs)//.DifferenceSet(result)
        //        {
        //            if (v != vec)
        //            {
        //                Vector2 _dir = v - vec;
        //                float dot = Vector2.Dot(_dir, dir) / _dir.Length();
        //                if (dot > value)
        //                {
        //                    value = dot;
        //                    dir = _dir;
        //                    vec = v;
        //                }
        //            }
        //        }
        //        if (vec != result[0])
        //            result.Add(vec);
        //    }
        //    while (vec == result[0]);
        //    return result.ToArray();
        //}
        public static T[] DelRepeatData<T>(this T[] array)
        {
            return array.GroupBy(p => p).Select(p => p.Key).ToArray();
        }
        public static List<Vector2> CalcConvexHull(this List<Vector2> list)
        {
            List<Vector2> resPoint = new List<Vector2>();
            //查找最小坐标点
            int minIndex = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Y < list[minIndex].Y)
                {
                    minIndex = i;
                }
            }
            Vector2 minPoint = list[minIndex];
            resPoint.Add(list[minIndex]);
            list.RemoveAt(minIndex);
            //坐标点排序
            list.Sort(
                delegate (Vector2 p1, Vector2 p2)
                {
                    Vector2 baseVec;
                    baseVec.X = 1;
                    baseVec.Y = 0;

                    Vector2 p1Vec;
                    p1Vec.X = p1.X - minPoint.X;
                    p1Vec.Y = p1.Y - minPoint.Y;

                    Vector2 p2Vec;
                    p2Vec.X = p2.X - minPoint.X;
                    p2Vec.Y = p2.Y - minPoint.Y;

                    double up1 = p1Vec.X * baseVec.X;
                    double down1 = Math.Sqrt(p1Vec.X * p1Vec.X + p1Vec.Y * p1Vec.Y);

                    double up2 = p2Vec.X * baseVec.X;
                    double down2 = Math.Sqrt(p2Vec.X * p2Vec.X + p2Vec.Y * p2Vec.Y);


                    double cosP1 = up1 / down1;
                    double cosP2 = up2 / down2;

                    if (cosP1 > cosP2)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                );
            resPoint.Add(list[0]);
            resPoint.Add(list[1]);
            for (int i = 2; i < list.Count; i++)
            {
                Vector2 basePt = resPoint[resPoint.Count - 2];
                Vector2 v1;
                v1.X = list[i - 1].X - basePt.X;
                v1.Y = list[i - 1].Y - basePt.Y;

                Vector2 v2;
                v2.X = list[i].X - basePt.X;
                v2.Y = list[i].Y - basePt.Y;

                if (v1.X * v2.Y - v1.Y * v2.X < 0)
                {
                    resPoint.RemoveAt(resPoint.Count - 1);
                    while (true)
                    {
                        Vector2 basePt2 = resPoint[resPoint.Count - 2];
                        Vector2 v12;
                        v12.X = resPoint[resPoint.Count - 1].X - basePt2.X;
                        v12.Y = resPoint[resPoint.Count - 1].Y - basePt2.Y;
                        Vector2 v22;
                        v22.X = list[i].X - basePt2.X;
                        v22.Y = list[i].Y - basePt2.Y;
                        if (v12.X * v22.Y - v12.Y * v22.X < 0)
                        {
                            resPoint.RemoveAt(resPoint.Count - 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    resPoint.Add(list[i]);
                }
                else
                {
                    resPoint.Add(list[i]);
                }
            }
            return resPoint;
        }
        public static Vector2[] EdgePoints(this Vector2[] vecs)
        {
            if (vecs.Length < 3)
            {
                throw new ArgumentException("兄啊，三个点都没有，计算个锤子的凸包");
            }

            try
            {
                int index = -1;
                float? leftcoord = null;
                for (int n = 0; n < vecs.Length; n++)
                {
                    if (leftcoord == null || leftcoord > vecs[n].X)
                    {
                        leftcoord = vecs[n].X;
                        index = n;
                    }
                    //leftcoord = (leftcoord == null || leftcoord > vecs[n].X) ? vecs[n].X : leftcoord;
                    //index = n;
                }
                var vec = vecs[index];
                List<Vector2> result = new List<Vector2>() { vec };
                int count = 0;
                do
                {
                    //foreach (var v in vecs.DifferenceSet(new Vector2[] { vec }))//.DifferenceSet(result)
                    //{
                    //    bool flag = true;
                    //    foreach (var v2 in vecs.DifferenceSet(new Vector2[] { vec, v }))
                    //    {
                    //        flag &= (v - vec).CrossLength(v2 - vec) < 0;
                    //    }
                    //    if (flag) result.Add(v);
                    //}
                    var set1 = vecs.DifferenceSet(new Vector2[] { vec });
                    for (int n = 0; n < set1.Length; n++)
                    {
                        bool flag = true;
                        var set2 = vecs.DifferenceSet(new Vector2[] { vec, set1[n] });
                        for (int i = 0; i < set2.Length; i++)
                        {
                            try
                            {
                                flag &= (set1[n] - vec).CrossLength(set2[i] - vec) <= 0;
                            }
                            catch
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            vec = set1[n];
                            if (vec != result[0])
                            {
                                result.Add(vec);
                            }
                        }
                    }
                    count++;
                    if (count > 100)
                    {
                        throw new Exception("我抄发生什么事了" + result.Count);
                    }
                    //if (vec != result[0])
                    //    result.Add(vec);
                }
                while (vec != result[0]);
                return result.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static void RotatedBy(this Vector2[] vecs, float rotation, Vector2 center = default)
        {
            for (int n = 0; n < vecs.Length; n++)
            {
                vecs[n] = vecs[n].RotatedBy(rotation, center);
            }
        }
        public static T[] CloneArray<T>(this T[] ts)
        {
            T[] myArray = new T[ts.Length];
            for (int n = 0; n < ts.Length; n++)
            {
                myArray[n] = ts[n];
            }
            return myArray;
        }
        public static void MulY(this Vector2[] vecs, float sclar)
        {
            for (int n = 0; n < vecs.Length; n++)
            {
                vecs[n].Y *= sclar;
            }
        }
        public static void MulX(this Vector2[] vecs, float sclar)
        {
            for (int n = 0; n < vecs.Length; n++)
            {
                vecs[n].X *= sclar;
            }
        }
        public static void Mul(this Vector2[] vecs, float sclar)
        {
            vecs.MulX(sclar);
            vecs.MulY(sclar);
        }
        public static void Mul(this Vector2[] vecs, Vector2 sclar)
        {
            vecs.MulX(sclar.X);
            vecs.MulY(sclar.Y);
        }
        public static void DrawHammer(this SpriteBatch spriteBatch, IHammerProj hammerProj, Texture2D glowTex, Color glowColor, Rectangle? glowFrame)
        {
            Vector2 origin = hammerProj.DrawOrigin;
            float rotation = hammerProj.Rotation;
            switch (hammerProj.flip)
            {
                case SpriteEffects.FlipHorizontally:
                    origin.X = hammerProj.projTex.Size().X / hammerProj.FrameMax.X - origin.X;
                    rotation += MathHelper.PiOver2;

                    break;
                case SpriteEffects.FlipVertically:
                    origin.Y = hammerProj.projTex.Size().Y / hammerProj.FrameMax.Y - origin.Y;
                    break;
            }
            spriteBatch.Draw(hammerProj.projTex, hammerProj.projCenter - Main.screenPosition, hammerProj.frame, hammerProj.color, rotation, origin, hammerProj.scale, hammerProj.flip, 0);
            spriteBatch.Draw(glowTex, hammerProj.projCenter - Main.screenPosition, glowFrame ?? hammerProj.frame, glowColor, rotation, origin, hammerProj.scale, hammerProj.flip, 0);
        }
        public static void DrawHammer(this SpriteBatch spriteBatch, IHammerProj hammerProj)
        {
            Vector2 origin = hammerProj.DrawOrigin;
            float rotation = hammerProj.Rotation;

            switch (hammerProj.flip)
            {
                case SpriteEffects.FlipHorizontally:
                    origin.X = hammerProj.projTex.Size().X / hammerProj.FrameMax.X - origin.X;
                    rotation += MathHelper.PiOver2;

                    break;
                case SpriteEffects.FlipVertically:
                    origin.Y = hammerProj.projTex.Size().Y / hammerProj.FrameMax.Y - origin.Y;
                    break;
            }
            spriteBatch.Draw(hammerProj.projTex, hammerProj.projCenter - Main.screenPosition, hammerProj.frame, hammerProj.color, rotation, origin, hammerProj.scale, hammerProj.flip, 0);
        }
        public static float TreatDegree(this Player player, NPC target)
        {
            if (!target.active)
            {
                return 0;
            }

            float locationTreat = Vector2.Dot(player.Center - target.Center, target.velocity - player.velocity) / (target.Center - player.Center).LengthSquared();
            float baseDataTreat = (target.life * target.defense / 10) * (1 / target.width / target.height / target.scale) + target.damage * (1 - 1 / target.width / target.height / target.scale);
            Main.NewText(new Vector2(locationTreat, baseDataTreat));
            return locationTreat + baseDataTreat;
        }
        //public static void Transform(this Tile[,] orig, Func<Vector2, Vector2> transform)
        //{
        //	var newTiles = new Tile[Main.maxTilesX, Main.maxTilesY];
        //	var types = new ushort[Main.maxTilesX, Main.maxTilesY];
        //	var counts = new ushort[Main.maxTilesX, Main.maxTilesY];
        //	for (int i = 0; i < Main.maxTilesX; i++)
        //	{
        //		for (int j = 0; j < Main.maxTilesY; j++)
        //		{
        //			newTiles[i, j] = new Tile();
        //		}
        //	}
        //	for (int i = 0; i < Main.maxTilesX; i++)
        //	{
        //		for (int j = 0; j < Main.maxTilesY; j++)
        //		{
        //			var vec = transform(new Vector2(i, j));
        //			if (vec.X >= 0 && vec.X < Main.maxTilesX && vec.Y >= 0 && vec.Y < Main.maxTilesY)
        //			{
        //				newTiles[(int)vec.X, (int)vec.Y] = Main.tile[i, j];
        //				types[(int)vec.X, (int)vec.Y] += Main.tile[i, j].type;
        //				counts[(int)vec.X, (int)vec.Y]++;
        //			}
        //		}
        //	}
        //	for (int i = 0; i < Main.maxTilesX; i++)
        //	{
        //		for (int j = 0; j < Main.maxTilesY; j++)
        //		{
        //			if (counts[i, j] > 0)
        //			{
        //				newTiles[i, j].type = (ushort)(types[i, j] / counts[i, j]);
        //				Main.tile[i, j] = newTiles[i, j];
        //			}
        //		}
        //	}
        //	//return Main.tile;
        //}
        //   public static void Transform(this Tile[,] orig, Matrix transform, Vector2 center)
        //   {
        //       var newTiles = new Tile[Main.maxTilesX, Main.maxTilesY];
        //       var types = new ushort[Main.maxTilesX, Main.maxTilesY];
        //       var counts = new ushort[Main.maxTilesX, Main.maxTilesY];
        //       for (int i = 0; i < Main.maxTilesX; i++)
        //       {
        //           for (int j = 0; j < Main.maxTilesY; j++)
        //           {
        //               newTiles[i, j] = new Tile();
        //           }
        //       }
        //       for (int i = 0; i < Main.maxTilesX; i++)
        //       {
        //           for (int j = 0; j < Main.maxTilesY; j++)
        //           {
        //var vec = (new Vector2(i, j) - center).ApplyMatrix(transform) + center;
        //               if (vec.X >= 0 && vec.X < Main.maxTilesX && vec.Y >= 0 && vec.Y < Main.maxTilesY)
        //               {
        //                   newTiles[(int)vec.X, (int)vec.Y] = Main.tile[i, j];
        //                   types[(int)vec.X, (int)vec.Y] += Main.tile[i, j].type;
        //                   counts[(int)vec.X, (int)vec.Y]++;
        //               }
        //           }
        //       }
        //       for (int i = 0; i < Main.maxTilesX; i++)
        //       {
        //           for (int j = 0; j < Main.maxTilesY; j++)
        //           {
        //if (counts[i, j] > 0)
        //{
        //	newTiles[i, j].type = (ushort)(types[i, j] / counts[i, j]);
        //	//Main.tile[i, j] = newTiles[i, j];
        //	Main.tile[i, j].SetValue(newTiles[i, j]);
        //}
        //else 
        //{
        //	Main.tile[i, j].type = 0;
        //	Main.tile[i, j].active(false);
        //}
        //           }
        //       }
        //   }
        public static BlendState GetBlendState(this (Blend sourceBlend, Blend destinationBlend) value)
        {
            return new BlendState()
            {
                ColorSourceBlend = value.sourceBlend,
                AlphaSourceBlend = value.sourceBlend,
                ColorDestinationBlend = value.destinationBlend,
                AlphaDestinationBlend = value.destinationBlend
            };
        }
        public static BlendState GetBlendState(Blend sourceBlend, Blend destinationBlend)
        {
            return new BlendState() { ColorSourceBlend = sourceBlend, AlphaSourceBlend = sourceBlend, ColorDestinationBlend = destinationBlend, AlphaDestinationBlend = destinationBlend };
        }
        //public static void TransformMain_Tile(Matrix transform, Vector2 center = default)
        //{
        //    var newTiles = new Tile[Main.maxTilesX, Main.maxTilesY];
        //    for (int i = 0; i < Main.maxTilesX; i++)
        //    {
        //        for (int j = 0; j < Main.maxTilesY; j++)
        //        {
        //            newTiles[i, j] = new Tile();
        //            newTiles[i, j].SetValue(Main.tile[i, j]);
        //            Main.tile[i, j].SetValue(0, 0, 0, 0, 0, 0, 0, 0, 0);
        //            Main.tile[i, j].active(false);
        //        }
        //    }
        //    for (int i = 0; i < Main.maxTilesX; i++)
        //    {
        //        for (int j = 0; j < Main.maxTilesY; j++)
        //        {
        //            var vec = ((new Vector2(i, j) - center).ApplyMatrix(transform) + center).ToPoint();
        //            if (vec.X >= 0 && vec.X < Main.maxTilesX && vec.Y >= 0 && vec.Y < Main.maxTilesY)
        //            {
        //                Main.tile[vec.X, vec.Y].SetValue(newTiles[i, j]);
        //            }
        //        }
        //    }
        //}
        //public static void TransformMain_Tile(Func<Vector2, Vector2> transform, Vector2 center = default)
        //{
        //    var newTiles = new Tile[Main.maxTilesX, Main.maxTilesY];
        //    for (int i = 0; i < Main.maxTilesX; i++)
        //    {
        //        for (int j = 0; j < Main.maxTilesY; j++)
        //        {
        //            newTiles[i, j] = new Tile();
        //            newTiles[i, j].SetValue(Main.tile[i, j]);
        //            Main.tile[i, j].SetValue(0, 0, 0, 0, 0, 0, 0, 0, 0);
        //            Main.tile[i, j].active(false);
        //        }
        //    }
        //    for (int i = 0; i < Main.maxTilesX; i++)
        //    {
        //        for (int j = 0; j < Main.maxTilesY; j++)
        //        {
        //            var vec = (transform(new Vector2(i, j) - center) + center).ToPoint();
        //            if (vec.X >= 0 && vec.X < Main.maxTilesX && vec.Y >= 0 && vec.Y < Main.maxTilesY)
        //            {
        //                Main.tile[vec.X, vec.Y].SetValue(newTiles[i, j]);
        //            }
        //        }
        //    }
        //}
        //public static void Transform(this Tile[,] orig, Matrix transform, Vector2 center = default)
        //{
        //	var xMax = orig.GetLength(0);
        //	var yMax = orig.GetLength(1);
        //	var newTiles = new Tile[xMax, yMax];
        //	for (int i = 0; i < xMax; i++)
        //	{
        //		for (int j = 0; j < yMax; j++)
        //		{
        //			newTiles[i, j] = new Tile();
        //			newTiles[i, j].SetValue(orig[i, j]);
        //			orig[i, j].SetValue(0, 0, 0, 0, 0, 0, 0, 0, 0);
        //			orig[i, j].active(false);
        //		}
        //	}
        //	for (int i = 0; i < xMax; i++)
        //	{
        //		for (int j = 0; j < yMax; j++)
        //		{
        //			var vec = ((new Vector2(i, j) - center).ApplyMatrix(transform) + center).ToPoint();
        //			if (vec.X >= 0 && vec.X < xMax && vec.Y >= 0 && vec.Y < yMax)
        //			{
        //				orig[vec.X, vec.Y].SetValue(newTiles[i, j]);
        //			}
        //		}
        //	}
        //}
        //public static void Transform(this Tile[,] orig, Func<Vector2,Vector2> transform, Vector2 center = default)
        //{
        //	var xMax = orig.GetLength(0);
        //	var yMax = orig.GetLength(1);
        //	var newTiles = new Tile[xMax, yMax];
        //	for (int i = 0; i < xMax; i++)
        //	{
        //		for (int j = 0; j < yMax; j++)
        //		{
        //			newTiles[i, j] = new Tile();
        //			newTiles[i, j].SetValue(orig[i, j]);
        //			orig[i, j].SetValue(0, 0, 0, 0, 0, 0, 0, 0, 0);
        //			orig[i, j].active(false);
        //		}
        //	}
        //	for (int i = 0; i < xMax; i++)
        //	{
        //		for (int j = 0; j < yMax; j++)
        //		{
        //			var vec = (transform(new Vector2(i, j) - center) + center).ToPoint();
        //			if (vec.X >= 0 && vec.X < xMax && vec.Y >= 0 && vec.Y < yMax)
        //			{
        //				orig[vec.X, vec.Y].SetValue(newTiles[i, j]);
        //			}
        //		}
        //	}
        //}
        public static double GaussianRandom(this UnifiedRandom random, double mu, double sigma)
        {
            double u = -2 * Math.Log(random.NextDouble());
            double v = 2 * Math.PI * random.NextDouble();
            return Math.Sqrt(u) * Math.Cos(v) * sigma + mu;
        }
        public static Matrix CreateRotationTransform(this Vector3 director, float rotation)
        {
            //var (s, c) = System.MathF.SinCos(rotation);
            var s = (float)Math.Sin(rotation);
            var c = (float)Math.Cos(rotation);
            var x = director.X;
            var y = director.Y;
            var z = director.Z;
            return new Matrix
            (
                x * x * (1 - c) + c, x * y * (1 - c) - z * s, x * z * (1 - c) + y * s, 0,
                x * y * (1 - c) + z * s, y * y * (1 - c) + c, y * z * (1 - c) - x * s, 0,
                x * z * (1 - c) - y * s, y * z * (1 - c) + x * s, z * z * (1 - c) + c, 0,
                0, 0, 0, 1
            );
        }
        public static void DrawQuadraticLaser_PassColorBar(this SpriteBatch spriteBatch, Vector2 start, Vector2 unit, int ColorBarIndex = 15, float length = 3200, float width = 512, float shakeRadMax = 0, float light = 4, int styleIndex = 10, bool timeOffset = false, float maxFactor = 0.5f, bool autoAdditive = true, (float x1, float y1, float x2, float y2) texcoord = default)
        {
            Effect effect = IllusionBoundMod.GetEffect("Effects/EightTrigramsFurnaceEffect");
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
            if (shakeRadMax > 0)
            {
                unit = unit.RotatedBy(Main.rand.NextFloat(-shakeRadMax, shakeRadMax));
            }

            Vector2 unit2 = new Vector2(-unit.Y, unit.X);
            if (texcoord == default) texcoord = (0, 0, 1, 1);
            bars1.Add(new CustomVertexInfo(start + unit2 * width, new Vector3(texcoord.x1, texcoord.y1, light)));
            bars1.Add(new CustomVertexInfo(start - unit2 * width, new Vector3(texcoord.x1, texcoord.y2, light)));
            bars1.Add(new CustomVertexInfo(start + unit2 * width + length * unit, new Vector3(texcoord.x2, texcoord.y1, 0)));
            bars1.Add(new CustomVertexInfo(start - unit2 * width + length * unit, new Vector3(texcoord.x2, texcoord.y2, 0)));
            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
            if (bars1.Count > 2)
            {
                for (int i = 0; i < bars1.Count - 2; i += 2)
                {
                    triangleList1.Add(bars1[i]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 3]);
                }
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                effect.Parameters["maxFactor"].SetValue(maxFactor);
                effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.HeatMap[ColorBarIndex];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                if (timeOffset)
                {
                    effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect_ColorBar_TimeOffset"].Apply();
                }
                else
                {
                    effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect_ColorBar"].Apply();
                }

                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone,null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static void DrawQuadraticLaser_PassColorBar(this SpriteBatch spriteBatch, (Vector2 start, Vector2 unit)[] startAndUnits, int ColorBarIndex = 15, float length = 3200, float width = 512, float shakeRadMax = 0, float light = 4, int styleIndex = 10, bool timeOffset = false, float maxFactor = 0.5f, bool autoAdditive = true)
        {
            Effect effect = IllusionBoundMod.GetEffect("Effects/EightTrigramsFurnaceEffect");
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["maxFactor"].SetValue(maxFactor);
            effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
            Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.HeatMap[ColorBarIndex];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            if (timeOffset)
            {
                effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect_ColorBar_TimeOffset"].Apply();
            }
            else
            {
                effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect_ColorBar"].Apply();
            }

            foreach (var (start, _unit) in startAndUnits)
            {
                List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
                var unit = _unit;
                if (shakeRadMax > 0)
                {
                    unit = unit.RotatedBy(Main.rand.NextFloat(-shakeRadMax, shakeRadMax));
                }

                Vector2 unit2 = new Vector2(-unit.Y, unit.X);
                bars1.Add(new CustomVertexInfo(start + unit2 * width, new Vector3(0, 0, light)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width, new Vector3(0, 1, light)));
                bars1.Add(new CustomVertexInfo(start + unit2 * width + length * unit, new Vector3(1, 0, 0)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width + length * unit, new Vector3(1, 1, 0)));
                List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
                if (bars1.Count > 2)
                {
                    for (int i = 0; i < bars1.Count - 2; i += 2)
                    {
                        triangleList1.Add(bars1[i]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 3]);
                    }
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                }
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static void DrawQuadraticLaser_PassNormal(this SpriteBatch spriteBatch, Vector2 start, Vector2 unit, Color color, float length = 3200, float width = 512, float shakeRadMax = 0, float light = 4, int styleIndex = 10, float maxFactor = 0.5f, bool autoAdditive = true)
        {
            Effect effect = IllusionBoundMod.GetEffect("Effects/EightTrigramsFurnaceEffect");
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
            if (shakeRadMax > 0)
            {
                unit = unit.RotatedBy(Main.rand.NextFloat(-shakeRadMax, shakeRadMax));
            }

            Vector2 unit2 = new Vector2(-unit.Y, unit.X);
            bars1.Add(new CustomVertexInfo(start + unit2 * width, color, new Vector3(0, 0, light)));
            bars1.Add(new CustomVertexInfo(start - unit2 * width, color, new Vector3(0, 1, light)));
            bars1.Add(new CustomVertexInfo(start + unit2 * width + length * unit, color, new Vector3(1, 0, 0)));
            bars1.Add(new CustomVertexInfo(start - unit2 * width + length * unit, color, new Vector3(1, 1, 0)));
            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
            if (bars1.Count > 2)
            {
                for (int i = 0; i < bars1.Count - 2; i += 2)
                {
                    triangleList1.Add(bars1[i]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 3]);
                }
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                effect.Parameters["maxFactor"].SetValue(maxFactor);
                effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect"].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static void DrawQuadraticLaser_PassNormal(this SpriteBatch spriteBatch, (Vector2 start, Vector2 unit)[] startAndUnits, Color color, float length = 3200, float width = 512, float shakeRadMax = 0, float light = 4, int styleIndex = 10, float maxFactor = 0.5f, bool autoAdditive = true)
        {
            Effect effect = IllusionBoundMod.GetEffect("Effects/EightTrigramsFurnaceEffect");
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["maxFactor"].SetValue(maxFactor);
            effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            effect.CurrentTechnique.Passes["EightTrigramsFurnaceEffect"].Apply();
            foreach (var (start, _unit) in startAndUnits)
            {
                List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
                var unit = _unit;
                if (shakeRadMax > 0)
                {
                    unit = unit.RotatedBy(Main.rand.NextFloat(-shakeRadMax, shakeRadMax));
                }

                Vector2 unit2 = new Vector2(-unit.Y, unit.X);
                bars1.Add(new CustomVertexInfo(start + unit2 * width, color, new Vector3(0, 0, light)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width, color, new Vector3(0, 1, light)));
                bars1.Add(new CustomVertexInfo(start + unit2 * width + length * unit, color, new Vector3(1, 0, 0)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width + length * unit, color, new Vector3(1, 1, 0)));
                List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
                if (bars1.Count > 2)
                {
                    for (int i = 0; i < bars1.Count - 2; i += 2)
                    {
                        triangleList1.Add(bars1[i]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 3]);
                    }
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                }
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static void DrawEffectLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 _unit, Color color, float startLight = 1, float endLight = 0, float length = 3200, float width = 512, float shakeRadMax = 0, int styleIndex = 10, float maxFactor = 0.5f, bool autoAdditive = true)
        {
            try
            {
                Effect effect = IllusionBoundMod.ShaderSwoosh;
                if (effect == null)
                {
                    return;
                }

                if (autoAdditive)
                {
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                effect.Parameters["maxFactor"].SetValue(maxFactor);
                effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                effect.CurrentTechnique.Passes[0].Apply();
                List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
                var unit = _unit;
                if (shakeRadMax > 0)
                {
                    unit = unit.RotatedBy(Main.rand.NextFloat(-shakeRadMax, shakeRadMax));
                }

                Vector2 unit2 = new Vector2(-unit.Y, unit.X);
                bars1.Add(new CustomVertexInfo(start + unit2 * width, color, new Vector3(0, 0, startLight)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width, color, new Vector3(0, 1, startLight)));
                bars1.Add(new CustomVertexInfo(start + unit2 * width + length * unit, color, new Vector3(1, 0, endLight)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width + length * unit, color, new Vector3(1, 1, endLight)));
                List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
                if (bars1.Count > 2)
                {
                    for (int i = 0; i < bars1.Count - 2; i += 2)
                    {
                        triangleList1.Add(bars1[i]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 3]);
                    }
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                }
                if (autoAdditive)
                {
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
            }
            catch (Exception e)
            {
                Main.NewText(e);
            }
        }
        public static void DrawEffectLine(this SpriteBatch spriteBatch, (Vector2 start, Vector2 unit)[] startAndUnits, Color color, float startLight = 1, float endLight = 0, float length = 3200, float width = 512, float shakeRadMax = 0, int styleIndex = 10, float maxFactor = 0.5f, bool autoAdditive = true)
        {
            Effect effect = IllusionBoundMod.ShaderSwoosh;
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["maxFactor"].SetValue(maxFactor);
            effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            effect.CurrentTechnique.Passes[0].Apply();
            foreach (var (start, _unit) in startAndUnits)
            {
                List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
                var unit = _unit;
                if (shakeRadMax > 0)
                {
                    unit = unit.RotatedBy(Main.rand.NextFloat(-shakeRadMax, shakeRadMax));
                }

                Vector2 unit2 = new Vector2(-unit.Y, unit.X);
                bars1.Add(new CustomVertexInfo(start + unit2 * width, color, new Vector3(0, 0, startLight)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width, color, new Vector3(0, 1, startLight)));
                bars1.Add(new CustomVertexInfo(start + unit2 * width + length * unit, color, new Vector3(1, 0, endLight)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width + length * unit, color, new Vector3(1, 1, endLight)));
                List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
                if (bars1.Count > 2)
                {
                    for (int i = 0; i < bars1.Count - 2; i += 2)
                    {
                        triangleList1.Add(bars1[i]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 3]);
                    }
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                }
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static void DrawEffectLine_StartAndEnd(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float startLight = 1, float endLight = 0, float width = 512, int styleIndex = 10, bool autoAdditive = true)
        {
            Effect effect = IllusionBoundMod.ShaderSwoosh;
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            effect.CurrentTechnique.Passes[1].Apply();
            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
            var unit = Vector2.Normalize(end - start);
            //unit.Normalize();
            Vector2 unit2 = new Vector2(-unit.Y, unit.X);
            bars1.Add(new CustomVertexInfo(start + unit2 * width, color, new Vector3(0, 0, startLight)));
            bars1.Add(new CustomVertexInfo(start - unit2 * width, color, new Vector3(0, 1, startLight)));
            bars1.Add(new CustomVertexInfo(end + unit2 * width, color, new Vector3(1, 0, endLight)));
            bars1.Add(new CustomVertexInfo(end - unit2 * width, color, new Vector3(1, 1, endLight)));
            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
            if (bars1.Count > 2)
            {
                for (int i = 0; i < bars1.Count - 2; i += 2)
                {
                    triangleList1.Add(bars1[i]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 1]);
                    triangleList1.Add(bars1[i + 2]);
                    triangleList1.Add(bars1[i + 3]);
                }
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static void DrawEffectLine_StartAndEnd(this SpriteBatch spriteBatch, (Vector2 start, Vector2 end)[] startAndEnds, Color color, float startLight = 1, float endLight = 0, float width = 512, int styleIndex = 10, bool autoAdditive = true)
        {
            Effect effect = IllusionBoundMod.ShaderSwoosh;
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[styleIndex];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            effect.CurrentTechnique.Passes[1].Apply();
            foreach (var (start, end) in startAndEnds)
            {
                List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
                var unit = Vector2.Normalize(end - start);
                //unit.Normalize();
                Vector2 unit2 = new Vector2(-unit.Y, unit.X);
                bars1.Add(new CustomVertexInfo(start + unit2 * width, color, new Vector3(0, 0, startLight)));
                bars1.Add(new CustomVertexInfo(start - unit2 * width, color, new Vector3(0, 1, startLight)));
                bars1.Add(new CustomVertexInfo(end + unit2 * width, color, new Vector3(1, 0, endLight)));
                bars1.Add(new CustomVertexInfo(end - unit2 * width, color, new Vector3(1, 1, endLight)));
                List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
                if (bars1.Count > 2)
                {
                    for (int i = 0; i < bars1.Count - 2; i += 2)
                    {
                        triangleList1.Add(bars1[i]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 1]);
                        triangleList1.Add(bars1[i + 2]);
                        triangleList1.Add(bars1[i + 3]);
                    }
                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                }
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public static int NextPow(this UnifiedRandom rand, int min, int max, int times, bool aMax = false)
        {
            for (int n = 0; n < times - 1; n++)
            {
                if (aMax)
                {
                    max = rand.Next(min, max);
                }
                else
                {
                    min = rand.Next(min, max);
                }
            }
            return rand.Next(min, max);
        }
        //public static void ForeachFunc<T>(this List<T> array, ActionFunc<T> action)
        //{
        //	for (int n = 0; n < array.Count; n++)
        //	{
        //		action.Invoke(array[n]);
        //	}
        //}
        public static Vector2 ApplyMatrix(this Vector2 v, float a, float b, float c, float d)
        {
            //(a b) (x)
            //(c d) (y)
            return new Vector2(v.X * a + v.Y + b, v.X * c + v.Y * d);
        }
        public static bool PointHit(this Rectangle target, Func<float, Vector2> vectorFunc, int times = 25)
        {
            if (vectorFunc == null)
            {
                return false;
            }

            for (int n = 0; n < times; n++)
            {
                var p = vectorFunc.Invoke(n / (times - 1f)).ToPoint();
                if (target.Contains(p.X, p.Y))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool RectangleHit(this Rectangle target, Func<float, Vector2> vectorFunc, Point size, int times = 25)
        {
            if (vectorFunc == null)
            {
                return false;
            }

            for (int n = 0; n < times; n++)
            {
                if (vectorFunc.Invoke(n / (times - 1f)).RectangleHit(target, size))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool RectangleHit(this Func<float, Vector2> vectorFunc, Rectangle target, int width = 4, int height = 4, int times = 25)
        {
            if (vectorFunc == null)
            {
                return false;
            }

            for (int n = 0; n < times; n++)
            {
                if (vectorFunc.Invoke(n / (times - 1f)).RectangleHit(target, width, height))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool RectangleHit(this Vector2 vector, Rectangle target, Point size)
        {
            return vector.RectangleHit(target, size.X, size.Y);
        }
        public static bool RectangleHit(this Vector2 vector, Rectangle target, int width = 4, int height = 4)
        {
            return target.Intersects(new Rectangle((int)vector.X - width / 2, (int)vector.Y - height / 2, width, height));
        }
        public static Matrix Create3DRotation(this float theta, DirOf3DRotation dir)
        {
            theta *= (int)dir % 2 == 0 ? 1 : -1;
            var c = (float)Math.Cos(theta);
            var s = (float)Math.Sin(theta);
            var matrix = Matrix.Identity;
            switch ((int)dir / 2)
            {
                case 0: matrix = new Matrix(1, 0, 0, 0, 0, c, -s, 0, 0, s, c, 0, 0, 0, 0, 0); break;
                case 1: matrix = new Matrix(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, 0, 0, 0, 0); break;
                case 2: matrix = new Matrix(c, -s, 0, 0, s, c, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0); break;
            };
            return matrix;
        }
        //     public static Matrix Create3DRotation(this float theta, DirOf3DRotation dir)
        //     {
        //         theta *= (int)dir % 2 == 0 ? 1 : -1;
        //         var c = (float)Math.Cos(theta);
        //         var s = (float)Math.Sin(theta);
        //         return  switch ((int)dir / 2)
        //{
        //             0 => new Matrix(1, 0, 0, 0, 0, c, -s, 0, 0, s, c, 0, 0, 0, 0, 0),
        //             1 => new Matrix(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, 0, 0, 0, 0),
        //             2 => new Matrix(c, -s, 0, 0, s, c, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0),
        //             _ => default,
        //         };
        //     }

        public static Vector2 ApplyMatrix(this Vector2 v, Matrix matrix)
        {
            return new Vector2(
                v.X * matrix.M11 + v.Y * matrix.M12,
                v.X * matrix.M21 + v.Y * matrix.M22
                );
        }
        public static Vector3 ApplyMatrix(this Vector3 v, Matrix matrix)
        {
            return new Vector3(
                v.X * matrix.M11 + v.Y * matrix.M12 + v.Z * matrix.M13,
                v.X * matrix.M21 + v.Y * matrix.M22 + v.Z * matrix.M23,
                v.X * matrix.M31 + v.Y * matrix.M32 + v.Z * matrix.M33
                );
        }
        public static Vector4 ApplyMatrix(this Vector4 v, Matrix matrix)
        {
            return new Vector4(
                v.X * matrix.M11 + v.Y * matrix.M12 + v.Z * matrix.M13 + v.W * matrix.M14,
                v.X * matrix.M21 + v.Y * matrix.M22 + v.Z * matrix.M23 + v.W * matrix.M24,
                v.X * matrix.M31 + v.Y * matrix.M32 + v.Z * matrix.M33 + v.W * matrix.M34,
                v.X * matrix.M41 + v.Y * matrix.M42 + v.Z * matrix.M43 + v.W * matrix.M44
                );
        }
        //public static void Draw3DPlane(this SpriteBatch spriteBatch, Effect effect, Texture2D baseTex, Texture2D aniTex, VertexTriangle3_RigidList loti, string pass = default)
        //{
        //	if (loti.tris == null) return;
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
        //	RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
        //	RasterizerState rasterizerState = new RasterizerState
        //	{
        //		CullMode = CullMode.None
        //	};
        //	Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
        //	var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
        //	var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
        //	effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
        //	effect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
        //	Main.graphics.GraphicsDevice.Textures[0] = baseTex;
        //	Main.graphics.GraphicsDevice.Textures[1] = aniTex;
        //	Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
        //	Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
        //	if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
        //	Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, loti.ToVertexInfo(), 0, loti.Length);
        //	Main.graphics.GraphicsDevice.RasterizerState = originalState;
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //}
        public static void Draw3DPlane(this SpriteBatch spriteBatch, Effect effect, Texture2D baseTex, Texture2D aniTex, VertexTriangle3List loti, string pass = default)
        {
            if (loti.tris == null)
            {
                return;
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = baseTex;
            Main.graphics.GraphicsDevice.Textures[1] = aniTex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, loti.ToVertexInfo(), 0, loti.Length);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void Draw3DPlane(this SpriteBatch spriteBatch, Effect effect, Texture2D baseTex, Texture2D aniTex, string pass = default, params VertexTriangle3[] tris)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = baseTex;
            Main.graphics.GraphicsDevice.Textures[1] = aniTex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, VertexTriangle3.ToVertexInfo(tris), 0, tris.Length);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void DrawPlane(this SpriteBatch spriteBatch, Effect effect, Texture2D baseTex, Texture2D aniTex, VertexTriangleList vttl, string pass = default)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = baseTex;
            Main.graphics.GraphicsDevice.Textures[1] = aniTex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vttl.ToVertexInfo(), 0, vttl.Length);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void DrawPlane(this SpriteBatch spriteBatch, Effect effect, Texture2D baseTex, Texture2D aniTex, string pass = default, params VertexTriangle[] tris)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            effect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
            Main.graphics.GraphicsDevice.Textures[0] = baseTex;
            Main.graphics.GraphicsDevice.Textures[1] = aniTex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, VertexTriangle.ToVertexInfo(tris), 0, tris.Length);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void DrawPath(this SpriteBatch spriteBatch, Vector2[] vectorFunc, Func<float, Color> colorFunc, Effect effect, Texture2D baseTex, Texture2D aniTex, Vector2 offest = default, float width = 16, float kOfX = 1, bool looped = false, Func<float, float> factorFunc = null, Func<float, float> widthFunc = null, Func<float, float> lightFunc = null, Func<float> timeFunc = null, string pass = default, Action<Vector2, int> doSth = null, bool alwaysDoSth = false)
        {
            if (vectorFunc == null || colorFunc == null || effect == null || vectorFunc.Length < 3)
            {
                return;
            }
            var counts = vectorFunc.Length;
            var bars = new CustomVertexInfo[counts * 2];
            for (int n = 0; n < counts; n++)
            {
                vectorFunc[n] += offest;
                if ((!Main.gamePaused || alwaysDoSth) && doSth != null)
                {
                    doSth.Invoke(vectorFunc[n], n);
                }
            }
            for (int i = 0; i < counts; ++i)
            {
                var normalDir = i == 0 ? (looped ? vectorFunc[0] - vectorFunc[counts - 1] : vectorFunc[1] - vectorFunc[0]) : vectorFunc[i] - vectorFunc[i - 1];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                var factor = 1 - i / (counts - 1f);
                if (factorFunc != null)
                {
                    factor = factorFunc.Invoke(factor);
                }
                var color = colorFunc.Invoke(factor);
                var w = widthFunc == null ? width : widthFunc.Invoke(factor);
                var l = lightFunc == null ? factor : lightFunc.Invoke(factor);
                bars[2 * i] = (new CustomVertexInfo(vectorFunc[i] + normalDir * w, color, new Vector3(factor * kOfX, 1, l)));
                bars[2 * i + 1] = (new CustomVertexInfo(vectorFunc[i] + normalDir * -w, color, new Vector3(factor * kOfX, 0, l)));
            }
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            for (int i = 0; i < bars.Length - 2; i += 2)
            {
                triangleList.Add(bars[i]);
                triangleList.Add(bars[i + 2]);
                triangleList.Add(bars[i + 1]);
                triangleList.Add(bars[i + 1]);
                triangleList.Add(bars[i + 2]);
                triangleList.Add(bars[i + 3]);
            }
            if (triangleList.Count > 2)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
                RasterizerState rasterizerState = new RasterizerState
                {
                    CullMode = CullMode.None
                };
                Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                effect.Parameters["uTime"].SetValue(timeFunc == null ? -(float)Main.time * 0.03f : timeFunc.Invoke());
                Main.graphics.GraphicsDevice.Textures[0] = baseTex;
                Main.graphics.GraphicsDevice.Textures[1] = aniTex;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }

        }
        public static void DrawPath(this SpriteBatch spriteBatch, Func<float, Vector2> vectorFunc, Func<float, Color> colorFunc, Effect effect, Texture2D baseTex, Texture2D aniTex, Vector2 offest = default, int counts = 25, float min = 0, float max = 1, float width = 16, float kOfX = 1, bool looped = false, Func<float, float> factorFunc = null, Func<float, float> widthFunc = null, Func<float, float> lightFunc = null, Func<float> timeFunc = null, string pass = default, Action<Vector2, float> doSth = null, bool alwaysDoSth = false, bool autoAdditive = true, int[] skipPoint = null)
        {
            if (vectorFunc == null || colorFunc == null || effect == null || counts < 3)
            {
                return;
            }
            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            var positions = new Vector2[counts];
            var bars = new CustomVertexInfo[counts * 2];
            for (int n = 0; n < counts; n++)
            {
                var factor = (float)n / (counts - 1);
                if (factorFunc != null)
                {
                    factor = factorFunc.Invoke(factor);
                }
                var lerp = factor * min + (1 - factor) * max;
                var position = vectorFunc.Invoke(lerp) + offest;
                positions[n] = position;
                if ((!Main.gamePaused || alwaysDoSth) && doSth != null)
                {
                    doSth.Invoke(position, factor);
                }
            }
            for (int i = 0; i < counts; ++i)
            {
                if (positions[i] == Vector2.Zero)
                {
                    break;
                }

                var normalDir = i == 0 ? (looped ? positions[0] - positions[counts - 1] : positions[1] - positions[0]) : positions[i] - positions[i - 1];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                var factor = i / (counts - 1f);
                if (factorFunc != null)
                {
                    factor = factorFunc.Invoke(factor);
                }
                var color = colorFunc.Invoke(factor);
                var w = widthFunc == null ? width : widthFunc.Invoke(factor);
                var l = lightFunc == null ? factor : lightFunc.Invoke(factor);
                bars[2 * i] = (new CustomVertexInfo(positions[i] + normalDir * w, color, new Vector3(factor * kOfX, 1, l)));
                bars[2 * i + 1] = (new CustomVertexInfo(positions[i] + normalDir * -w, color, new Vector3(factor * kOfX, 0, l)));
            }
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            for (int i = 0; i < bars.Length - 2; i += 2)
            {
                if (skipPoint != null && skipPoint.Contains(i)) i += 2;
                triangleList.Add(bars[i]);
                triangleList.Add(bars[i + 2]);
                triangleList.Add(bars[i + 1]);
                triangleList.Add(bars[i + 1]);
                triangleList.Add(bars[i + 2]);
                triangleList.Add(bars[i + 3]);
            }
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
            if (triangleList.Count > 2)
            {
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                effect.Parameters["uTime"].SetValue(timeFunc == null ? -(float)Main.time * 0.03f : timeFunc.Invoke());
                Main.graphics.GraphicsDevice.Textures[0] = baseTex;
                Main.graphics.GraphicsDevice.Textures[1] = aniTex;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                if (pass != null) { effect.CurrentTechnique.Passes[pass].Apply(); } else { effect.CurrentTechnique.Passes[0].Apply(); }
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
            }

            Main.graphics.GraphicsDevice.RasterizerState = originalState;

            if (autoAdditive)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }

        }
        public static Vector2 ApplyMatrix(this Vector2 v, Vector2 i, Vector2 j)
        {
            return new Vector2(v.X * i.X + v.Y * j.X, v.X * i.Y + v.Y * j.Y);
        }
        public static void ResetArray<T>(this T[] array)
        {
            for (int n = 0; n < array.Length; n++)
            {
                array[n] = default;
            }
        }
        public static Vector2[] EasierVec2Array(params float[] v)
        {
            var len = v.Length;
            if (len < 1)
            {
                return null;
            }

            var l = new List<Vector2>();
            for (int n = 0; n < len / 2; n++)
            {
                float y = 2 * n + 1 == len ? 0 : v[2 * n + 1];
                l.Add(new Vector2(v[2 * n], y));
            }
            return l.ToArray();
        }
        public static Vector2 Projectile(this Vector3 vector, float height, Vector2 center = default)
        {
            return (new Vector2(vector.X, vector.Y) - center) * height / (height - vector.Z) + center;
        }
        public static Vector3 Projectile(this Vector4 vector, float height, Vector3 center = default)
        {
            return (new Vector3(vector.X, vector.Y, vector.Z) - center) * height / (height - vector.W) + center;
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float width = 4f, bool offset = false, Vector2 drawOffset = default)
        {
            if (offset)
            {
                end += start;
            }

            spriteBatch.Draw(TextureAssets.MagicPixel.Value, (start + end) * .5f + drawOffset, new Rectangle(0, 0, 1, 1), color, (end - start).ToRotation(), new Vector2(.5f, .5f), new Vector2((start - end).Length(), width), 0, 0);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector3 start, Vector3 end, Color color, float height, float width = 4f, bool offset = false, Vector2 drawOffset = default, Vector2 projCenter = default)
        {
            if (offset)
            {
                end += start;
            }

            var s = start.Projectile(height, projCenter);
            var e = end.Projectile(height, projCenter);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, (s + e) * .5f + drawOffset, new Rectangle(0, 0, 1, 1), color, (e - s).ToRotation(), new Vector2(.5f, .5f), new Vector2((s - e).Length(), width), 0, 0);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector4 start,
            Vector4 end, Color color, float heightZ, float heightW, float width = 4f,
            bool offset = false, Vector2 drawOffset = default, Vector2 projCenter = default)
        {
            if (offset)
            {
                end += start;
            }

            var s = start.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter);
            var e = end.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, (s + e) * .5f + drawOffset,
                new Rectangle(0, 0, 1, 1), color, (e - s).ToRotation(),
                new Vector2(.5f, .5f), new Vector2((s - e).Length(), width), 0, 0);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector3 start, Vector3 end, Color color, float height, out Vector2 s, out Vector2 e, float width = 4f, bool offset = false, Vector2 drawOffset = default, Vector2 projCenter = default)
        {
            if (offset)
            {
                end += start;
            }

            s = start.Projectile(height, projCenter) + drawOffset;
            e = end.Projectile(height, projCenter) + drawOffset;
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, (s + e) * .5f, new Rectangle(0, 0, 1, 1), color, (e - s).ToRotation(), new Vector2(.5f, .5f), new Vector2((s - e).Length(), width), 0, 0);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector4 start, Vector4 end, Color color, float heightZ, float heightW, out Vector2 s, out Vector2 e, float width = 4f, bool offset = false, Vector2 drawOffset = default, Vector2 projCenter = default)
        {
            if (offset)
            {
                end += start;
            }

            s = start.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter) + drawOffset;
            e = end.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter) + drawOffset;
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, (s + e) * .5f, new Rectangle(0, 0, 1, 1), color, (e - s).ToRotation(), new Vector2(.5f, .5f), new Vector2((s - e).Length(), width), 0, 0);
        }
        public static void ForeachFunc<T>(this T[] array, Action<T> action)
        {
            for (int n = 0; n < array.Length; n++)
            {
                action.Invoke(array[n]);
            }
        }
        public static void ForeachFunc<T>(this T[] array, Action<T, int> action)
        {
            for (int n = 0; n < array.Length; n++)
            {
                action.Invoke(array[n], n);
            }
        }
        //public static void ForeachFunc<T>(this T[] array, RefFunction<T> action)
        //{
        //    for (int n = 0; n < array.Length; n++)
        //    {
        //        action.Invoke(ref array[n], n);
        //    }
        //}
        public static List<T> CopyList<T>(this List<T> l)
        {
            var l1 = new List<T>();
            //l.ForEach()
            //ForeachFunc(l, (T v) => { l1.Add(v); });
            l.ForEach((T v) => { l1.Add(v); });
            return l1;
        }
        private static List<int> FuncForHBC_1(this List<int> l1)
        {
            List<int> l = l1.CopyList();
            //ForeachFunc(l, (int v) => { v = 4 - v; });
            //l.ForEach((int v) => { v = 4 - v; });
            for (int n = 0; n < l.Count; n++)
            {
                //l[n] = 4 - l[n];
                l[n]++;
                l[n] %= 4;
                if (l[n] % 2 == 0)
                {
                    l[n] = (l[n] + 2) % 4;
                }
                l[n] += 2;
                l[n] %= 4;
            }
            return l;
        }
        private static List<int> FuncForHBC_2(this List<int> l1)
        {
            List<int> l = l1.CopyList();
            //ForeachFunc(l, (int v) => { v = (v + 1) % 2 + v / 2 * 2; });
            //l.ForEach((int v) => { v = (v + 1) % 2 + v / 2 * 2; });
            for (int n = 0; n < l.Count; n++)
            {
                //l[n] = (l[n] + 1) % 2 + l[n] / 2 * 2;
                l[n]++;
                l[n] %= 4;
                if (l[n] % 2 == 0)
                {
                    l[n] = (l[n] + 2) % 4;
                }
            }
            return l;
        }
        private static void Add<T>(this List<T> l1, List<T> l2)
        {
            l2.ForEach((T v) => { l1.Add(v); });
        }
        public static List<int> HBCIndex(int t = 1)
        {
            if (t < 1)
            {
                return null;
            }
            else if (t == 1)
            {
                return new List<int>() { 0, 1, 2 };
            }
            else
            {
                var l = HBCIndex(t - 1);
                var ml = new List<int>
                {
                    l.FuncForHBC_2(),
                    0,
                    l,
                    1,
                    l,
                    2,
                    l.FuncForHBC_1()
                };
                return ml;
            }
        }
        public static List<Vector2> HBCPoint(this List<int> index)
        {
            var l = new List<Vector2>() { default };
            for (int n = 0; n < index.Count; n++)
            {
                Vector2 vec;
                switch (index[n])
                {
                    case 0:
                        {
                            vec = new Vector2(0, 1);
                            break;
                        }
                    case 1:
                        {
                            vec = new Vector2(1, 0);
                            break;
                        }
                    case 2:
                        {
                            vec = new Vector2(0, -1);
                            break;
                        }
                    case 3:
                        {
                            vec = new Vector2(-1, 0);
                            break;
                        }
                    default:
                        {
                            vec = new Vector2(0, 1);
                            break;
                        }
                }
                l.Add(vec + l[n]);
            }
            //index.ForEach((int v) =>
            //{
            //	switch (v)
            //	{
            //		case 0:
            //			{
            //				l.Add(new Vector2(0, 1));
            //				break;
            //			}
            //		case 1:
            //			{
            //				l.Add(new Vector2(1, 0));
            //				break;
            //			}
            //		case 2:
            //			{
            //				l.Add(new Vector2(0, -1));
            //				break;
            //			}
            //		case 3:
            //			{
            //				l.Add(new Vector2(-1, 0));
            //				break;
            //			}
            //		default:
            //			{
            //				l.Add(new Vector2(0, 1));
            //				break;
            //			}
            //	}
            //});
            //ForeachFunc(index, (int v) =>
            // {
            //	 switch (v)
            //	 {
            //		 case 0:
            //			 {
            //				 l.Add(new Vector2(0, 1));
            //				 break;
            //			 }
            //		 case 1:
            //			 {
            //				 l.Add(new Vector2(1, 0));
            //				 break;
            //			 }
            //		 case 2:
            //			 {
            //				 l.Add(new Vector2(0, -1));
            //				 break;
            //			 }
            //		 case 3:
            //			 {
            //				 l.Add(new Vector2(-1, 0));
            //				 break;
            //			 }
            //		 default:
            //			 {
            //				 l.Add(new Vector2(0, 1));
            //				 break;
            //			 }
            //	 }
            // });
            return l;
        }
        public static Color GetLerpValue(this float factor, params Color[] values)
        {
            if (factor <= 0)
            {
                return values[0];
            }
            else if (factor >= 1)
            {
                return values[values.Length - 1];
            }
            else
            {
                int c = values.Length - 1;
                int tier = (int)(c * factor);
                return Color.Lerp(values[tier], values[tier + 1], c * factor % 1);
            }
        }
        public static Vector4 GetLerpValue(this float factor, params Vector4[] values)
        {
            if (factor <= 0)
            {
                return values[0];
            }
            else if (factor >= 1)
            {
                return values[values.Length - 1];
            }
            else
            {
                int c = values.Length - 1;
                int tier = (int)(c * factor);
                return Vector4.Lerp(values[tier], values[tier + 1], c * factor % 1);
            }
        }
        public static Vector2 GetLerpValue_Loop(this float factor, params Vector2[] values)
        {
            if (factor <= 0 || factor >= 1)
            {
                return values[0];
            }
            else
            {
                int c = values.Length;
                int tier = (int)(c * factor);
                return Vector2.Lerp(values[tier], values[(tier + 1) == c ? 0 : (tier + 1)], c * factor % 1);
            }
        }
        public static Vector4 GetLerpValue_Loop(this float factor, params Vector4[] values)
        {
            if (factor <= 0 || factor >= 1)
            {
                return values[0];
            }
            else
            {
                int c = values.Length;
                int tier = (int)(c * factor);
                return Vector4.Lerp(values[tier], values[(tier + 1) == c ? 0 : (tier + 1)], c * factor % 1);
            }
        }
        public static Vector2 GetLerpValue(this float factor, params Vector2[] values)
        {
            if (factor <= 0)
            {
                return values[0];
            }
            else if (factor >= 1)
            {
                return values[values.Length - 1];
            }
            else
            {
                int c = values.Length - 1;
                int tier = (int)(c * factor);
                return Vector2.Lerp(values[tier], values[tier + 1], c * factor % 1);
            }
        }
        /// <summary>
        /// 0到1从正方形的左下角到正方形右下角，n阶伪希尔伯特曲线轨迹（这个适合用来求单个点
        /// </summary>
        /// <param name="fac">0到1的一个浮点数</param>
        /// <param name="t">阶数，至少且默认为1</param>
        /// <returns></returns>
        public static Vector2 HBCFacFunc(this float fac, int t = 1)
        {
            if (t < 1)
            {
                return default;
            }

            return fac.GetLerpValue(HBCPoint(HBCIndex(t)).ToArray());
        }
        public static FactorFunction<Vector2> CatmullromFactor(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4)
        {
            Vector2 VF(float fac)
            {
                return Vector2.CatmullRom(value1, value2, value3, value4, fac);
            }
            return VF;
        }
        public static Vector2[] CatMullRomCurve(this Vector2[] vecs, int extraLength)
        {
            int l = vecs.Length;
            extraLength += l;
            Vector2[] scVecs = new Vector2[extraLength];
            for (int n = 0; n < extraLength; n++)
            {
                float t = n / (float)extraLength;
                float k = (l - 1) * t;
                int i = (int)k;
                float vk = k % 1;
                if (i == 0)
                {
                    scVecs[n] = Vector2.CatmullRom(2 * vecs[0] - vecs[1], vecs[0], vecs[1], vecs[2], vk);
                }
                else if (i == l - 2)
                {
                    scVecs[n] = Vector2.CatmullRom(vecs[l - 3], vecs[l - 2], vecs[l - 1], 2 * vecs[l - 1] - vecs[l - 2], vk);
                }
                else
                {
                    scVecs[n] = Vector2.CatmullRom(vecs[i - 1], vecs[i], vecs[i + 1], vecs[i + 2], vk);
                }
            }
            return scVecs;
        }
        public static Vector2[] CatMullRomCurve(this Vector2[] vecs, int extraLength, (int start, int end) range)
        {
            if (range.start >= range.end)
            {
                throw new Exception("你丫的找茬是吧，起点下标(start)必须小于终点下标(end)");
            }

            var (s, e) = range;
            int l = e - s;
            extraLength += l;
            Vector2[] scVecs = new Vector2[extraLength];
            for (int n = 0; n < extraLength; n++)
            {
                float t = n / (float)extraLength;
                float k = (l - 1) * t;
                int i = (int)k;
                float vk = k % 1;
                if (i == 0)
                {
                    scVecs[n] = Vector2.CatmullRom(2 * vecs[s] - vecs[1 + s], vecs[s], vecs[1 + s], vecs[2 + s], vk);
                }
                else if (i == l - 2)
                {
                    scVecs[n] = Vector2.CatmullRom(vecs[l - 3 + s], vecs[l - 2 + s], vecs[l - 1 + s], 2 * vecs[l - 1 + s] - vecs[l - 2 + s], vk);
                }
                else
                {
                    scVecs[n] = Vector2.CatmullRom(vecs[i - 1 + s], vecs[i + s], vecs[i + 1 + s], vecs[i + 2 + s], vk);
                }
            }
            return scVecs;
        }
        public static Vector2[] CatMullRomCurve2(this Vector2[] vecs, int extraLength)
        {
            int l = vecs.Length;
            extraLength += l;
            FactorFunction<Vector2>[] vFs = new FactorFunction<Vector2>[l - 1];
            for (int n = 1; n < l - 2; n++)
            {
                vFs[n] = CatmullromFactor(vecs[n - 1], vecs[n], vecs[n + 1], vecs[n + 2]);
            }
            vFs[0] = CatmullromFactor(2 * vecs[0] - vecs[1], vecs[0], vecs[1], vecs[2]);
            vFs[l - 2] = CatmullromFactor(vecs[l - 3], vecs[l - 2], vecs[l - 1], 2 * vecs[l - 1] - vecs[l - 2]);
            Vector2[] scVecs = new Vector2[extraLength];
            for (int n = 0; n < extraLength; n++)
            {
                float t = n / (float)extraLength;
                float k = (l - 1) * t;
                int i = (int)k;
                float vk = k % 1;
                scVecs[n] = vFs[i].Invoke(vk);
            }
            return scVecs;
        }
        public static bool ZoneForest(this Player player)
        {
            if (player.ZoneSkyHeight)
            {
                return false;
            }
            if (player.ZoneSnow)
            {
                return false;
            }
            if (player.ZoneDesert)
            {
                return false;
            }
            if (player.ZoneJungle)
            {
                return false;
            }
            //if (player.GetModPlayer<IllusionBoundPlayer>().ZoneStorm)
            //{
            //    return false;
            //}
            if (player.ZoneUnderworldHeight)
            {
                return false;
            }
            if (player.ZoneDungeon || player.zone4[5])
            {
                return false;
            }
            if (player.ZoneHallow)
            {
                return false;
            }
            if (player.ZoneBeach)
            {
                return false;
            }
            if (player.ZoneCorrupt || player.ZoneCrimson)
            {
                return false;
            }
            return true;
        }
        public static Vector2 CatmullRomCurve(this Vector2[] pos, double t)
        {
            //这个b没实现，是复制了贝塞尔曲线的代码，下次补上（x

            int n = pos.Length - 1;
            if (n == 0)
            {
                return Vector2.Zero;
            }

            if (t == 0)
            {
                return pos[0];
            }
            Vector2 p = Vector2.Zero;
            for (int i = 0; i <= n; i++)
            {
                p += pos[i] * Combination(i, n) * (float)(Math.Pow(1 - t, n - i) * Math.Pow(t, i));
            }
            return p;
        }
        public static void ShaderItemEffectInWorld(this Item item, SpriteBatch spriteBatch, Texture2D effectTex, Color c, float rotation, float light = 2)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            CustomVertexInfo[] triangleArry = new CustomVertexInfo[6];
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            //Color c = Main.hslToRgb((float)IllusionBoundMod.ModTime / 60 % 1, 1f, 0.75f);

            Vector2 scale = TextureAssets.Item[item.type].Value.Size();
            //triangleArry[0] = new CustomVertexInfo(item.position, c, new Vector3(0, 0, light));
            //triangleArry[1] = new CustomVertexInfo(item.position + new Vector2(scale.X, 0), c, new Vector3(1, 0, light));
            //triangleArry[2] = new CustomVertexInfo(item.position + scale, c, new Vector3(1, 1, light));
            //triangleArry[3] = triangleArry[2];
            //triangleArry[4] = new CustomVertexInfo(item.position + new Vector2(0, scale.Y), c, new Vector3(0, 1, light));
            //triangleArry[5] = triangleArry[0];
            scale *= 0.5f;
            triangleArry[0] = new CustomVertexInfo(item.Center - scale.RotatedBy(rotation), c, new Vector3(0, 0, light));
            triangleArry[1] = new CustomVertexInfo(item.Center + new Vector2(scale.X, -scale.Y).RotatedBy(rotation), c, new Vector3(1, 0, light));
            triangleArry[2] = new CustomVertexInfo(item.Center + scale.RotatedBy(rotation), c, new Vector3(1, 1, light));
            triangleArry[3] = triangleArry[2];
            triangleArry[4] = new CustomVertexInfo(item.Center - new Vector2(scale.X, -scale.Y).RotatedBy(rotation), c, new Vector3(0, 1, light));
            triangleArry[5] = triangleArry[0];
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            IllusionBoundMod.IMBellEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            IllusionBoundMod.IMBellEffect.Parameters["uTime"].SetValue((float)IllusionBoundMod.ModTime2 / 60);//(float)IllusionBoundMod.ModTime / 60
            Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.Item[item.type].Value;
            Main.graphics.GraphicsDevice.Textures[1] = effectTex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            IllusionBoundMod.IMBellEffect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleArry, 0, 2);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void ShaderItemEffectInventory(this Item item, SpriteBatch spriteBatch, Vector2 position, Vector2 origin, Texture2D effectTex, Color c, float Scale, float light = 2)
        {
            #region MyRegion
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            //CustomVertexInfo[] triangleArry = new CustomVertexInfo[6];
            //RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            //Texture2D texture2D = TextureAssets.Item[item.type].Value;
            //float offsetX = texture2D.Width * Scale;
            //float offsetY = texture2D.Height * Scale;
            ////Color c = Main.hslToRgb(0f, 1f, 0.75f);
            ////triangleArry[0] = new CustomVertexInfo(position + Main.screenPosition - new Vector2(offsetX, offsetY) + origin, c, new Vector3(0, 0, light));
            ////triangleArry[1] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, -offsetY) + origin, c, new Vector3(1, 0, light));
            ////triangleArry[2] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, offsetY) + origin, c, new Vector3(1, 1, light));
            ////triangleArry[3] = triangleArry[2];
            ////triangleArry[4] = new CustomVertexInfo(position + Main.screenPosition - new Vector2(offsetX, -offsetY) + origin, c, new Vector3(0, 1, light));
            ////triangleArry[5] = triangleArry[0];
            ////Vector2 offset = ItemID.Sets.ItemIconPulse[item.type] ? default : new Vector2(-2, -2);
            //position -= origin * Scale;
            //triangleArry[0] = new CustomVertexInfo(position + Main.screenPosition, c, new Vector3(0, 0, light));
            //triangleArry[1] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, 0), c, new Vector3(1, 0, light));
            //triangleArry[2] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, offsetY), c, new Vector3(1, 1, light));
            //triangleArry[3] = triangleArry[2];
            //triangleArry[4] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(0, offsetY), c, new Vector3(0, 1, light));
            //triangleArry[5] = triangleArry[0];
            //var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            //var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            //IllusionBoundMod.IMBellEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            //IllusionBoundMod.IMBellEffect.Parameters["uTime"].SetValue((float)IllusionBoundMod.ModTime / 60 % 1);
            //Main.graphics.GraphicsDevice.Textures[0] = texture2D;
            //Main.graphics.GraphicsDevice.Textures[1] = effectTex;
            //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            //IllusionBoundMod.IMBellEffect.CurrentTechnique.Passes[0].Apply();
            //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleArry, 0, 2);
            //Main.graphics.GraphicsDevice.RasterizerState = originalState;
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            #endregion
            
            if (IllusionBoundMod.IMBellEffect == null) return;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            CustomVertexInfo[] triangleArry = new CustomVertexInfo[6];
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            Texture2D texture2D = TextureAssets.Item[item.type].Value;
            float offsetX = texture2D.Width * Scale;
            float offsetY = texture2D.Height * Scale;
            //Color c = Main.hslToRgb(0f, 1f, 0.75f);
            //triangleArry[0] = new CustomVertexInfo(position + Main.screenPosition - new Vector2(offsetX, offsetY) + origin, c, new Vector3(0, 0, light));
            //triangleArry[1] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, -offsetY) + origin, c, new Vector3(1, 0, light));
            //triangleArry[2] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, offsetY) + origin, c, new Vector3(1, 1, light));
            //triangleArry[3] = triangleArry[2];
            //triangleArry[4] = new CustomVertexInfo(position + Main.screenPosition - new Vector2(offsetX, -offsetY) + origin, c, new Vector3(0, 1, light));
            //triangleArry[5] = triangleArry[0];
            //Vector2 offset = ItemID.Sets.ItemIconPulse[item.type] ? default : new Vector2(-2, -2);
            position -= origin * Scale;
            triangleArry[0] = new CustomVertexInfo(position + Main.screenPosition, c, new Vector3(0, 0, light));
            triangleArry[1] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, 0), c, new Vector3(1, 0, light));
            triangleArry[2] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offsetX, offsetY), c, new Vector3(1, 1, light));
            triangleArry[3] = triangleArry[2];
            triangleArry[4] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(0, offsetY), c, new Vector3(0, 1, light));
            triangleArry[5] = triangleArry[0];
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            IllusionBoundMod.IMBellEffect.Parameters["uTransform"].SetValue(model * projection);
            IllusionBoundMod.IMBellEffect.Parameters["uTime"].SetValue((float)IllusionBoundModSystem.ModTime / 60f % 1);
            Main.graphics.GraphicsDevice.Textures[0] = texture2D;
            Main.graphics.GraphicsDevice.Textures[1] = effectTex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            IllusionBoundMod.IMBellEffect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleArry, 0, 2);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }
        //public static void SetValue(this Tile tile, byte bh1, byte bh2, byte bh3, short fx, short fy, byte lqu, ushort sh, ushort type, ushort wall)
        //{
        //    tile.active(true);
        //    tile.bTileHeader = bh1;
        //    tile.bTileHeader2 = bh2;
        //    tile.bTileHeader3 = bh3;
        //    tile.frameX = fx;
        //    tile.frameY = fy;
        //    tile.liquid = lqu;
        //    tile.sTileHeader = sh;
        //    tile.type = type;
        //    tile.wall = wall;
        //}
        //public static void SetValue(this Tile tile, Tile tar)
        //{
        //    tile.active(tar.active());
        //    tile.bTileHeader = tar.bTileHeader;
        //    tile.bTileHeader2 = tar.bTileHeader2;
        //    tile.bTileHeader3 = tar.bTileHeader3;
        //    tile.frameX = tar.frameX;
        //    tile.frameY = tar.frameY;
        //    tile.liquid = tar.liquid;
        //    tile.sTileHeader = tar.sTileHeader;
        //    tile.type = tar.type;
        //    tile.wall = tar.wall;
        //}
        //激光判定，start为起点，end为终点，width为宽度，hitbox为碰撞目标
        //用于bool? colliding
        public static bool LineCheck(Vector2 start, Vector2 end, float width, Rectangle hitbox)
        {
            Vector2 v = end - start;
            v.Normalize();
            v = new Vector2(v.Y, -v.X);
            Triangle t1 = new Triangle(start + v * width, start - v * width, end + v * width);
            Triangle t2 = new Triangle(end + v * width, end - v * width, start - v * width);
            Triangle t3 = new Triangle(new Vector2(hitbox.X, hitbox.Y),
                new Vector2(hitbox.X, hitbox.Y + hitbox.Height),
                new Vector2(hitbox.X + hitbox.Width, hitbox.Y + hitbox.Width));
            Triangle t4 = new Triangle(new Vector2(hitbox.X, hitbox.Y),
                new Vector2(hitbox.X + hitbox.Width, hitbox.Y),
                new Vector2(hitbox.X + hitbox.Width, hitbox.Y + hitbox.Width));
            if (Triangle.Intersect(t1, t3))
            {
                return true;
            }
            if (Triangle.Intersect(t1, t4))
            {
                return true;
            }
            if (Triangle.Intersect(t2, t3))
            {
                return true;
            }
            if (Triangle.Intersect(t2, t4))
            {
                return true;
            }
            return false;
        }

        //三角类
        private class Triangle
        {
            private Vector2 vertex1;
            private Vector2 vertex2;
            private Vector2 vertex3;
            private Vector2 center;
            private Line_Segment line1, line2, line3;
            public Triangle(Vector2 v1, Vector2 v2, Vector2 v3)
            {
                vertex1 = v1;
                vertex2 = v2;
                vertex3 = v3;
                Reset();
            }
            public Vector2 Vertex1
            {
                get
                {
                    return vertex1;
                }
                set
                {
                    vertex1 = value;
                    Reset();
                }
            }
            public Vector2 Vertex2
            {
                get
                {
                    return vertex2;
                }
                set
                {
                    vertex2 = value;
                    Reset();
                }
            }
            public Vector2 Vertex3
            {
                get
                {
                    return vertex3;
                }
                set
                {
                    vertex3 = value;
                    Reset();
                }
            }
            public Vector2 Center
            {
                get
                {
                    return center;
                }
            }
            public static bool Intersect(Triangle triangle1, Triangle triangle2)
            {
                if (Line_Segment.Intersect(triangle1.line1, triangle2.line1))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line1, triangle2.line2))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line1, triangle2.line3))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line2, triangle2.line1))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line2, triangle2.line2))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line2, triangle2.line3))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line3, triangle2.line1))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line3, triangle2.line2))
                {
                    return true;
                }
                if (Line_Segment.Intersect(triangle1.line3, triangle2.line3))
                {
                    return true;
                }
                if (Point_In_Triangle(triangle1.Vertex1, triangle2) || Point_In_Triangle(triangle1.Vertex2, triangle2) || Point_In_Triangle(triangle1.Vertex3, triangle2))
                {
                    return true;
                }
                if (Point_In_Triangle(triangle2.Vertex1, triangle1) || Point_In_Triangle(triangle2.Vertex2, triangle1) || Point_In_Triangle(triangle2.Vertex3, triangle1))
                {
                    return true;
                }
                return false;
            }
            public static bool Point_In_Triangle(Vector2 point, Triangle triangle)
            {
                bool flag1, flag2, flag3;
                flag1 = Line_Segment.Is_Same_Side(point, triangle.Center, triangle.line1);
                flag2 = Line_Segment.Is_Same_Side(point, triangle.Center, triangle.line2);
                flag3 = Line_Segment.Is_Same_Side(point, triangle.Center, triangle.line3);
                if (flag1 && flag2 && flag3)
                {
                    return true;
                }
                return false;
            }
            private void Reset()
            {
                ResetCenter();
                ResetLine();
            }
            private void ResetCenter()
            {
                center = (vertex1 + vertex2 + vertex3) / 3;
            }
            private void ResetLine()
            {
                line1 = new Line_Segment(vertex1, vertex2);
                line2 = new Line_Segment(vertex2, vertex3);
                line3 = new Line_Segment(vertex3, vertex1);
            }
        }

        //线段类
        private class Line_Segment
        {
            private Vector2 startpos, endpos;
            private float a, b, c;
            public Line_Segment(Vector2 start, Vector2 end)
            {
                startpos = start;
                endpos = end;
                ABC();
            }
            public Vector2 StartPos
            {
                get
                {
                    return startpos;
                }
                set
                {
                    startpos = value;
                    ABC();
                }
            }
            public Vector2 EndPos
            {
                get
                {
                    return endpos;
                }
                set
                {
                    endpos = value;
                    ABC();
                }
            }
            public static bool Intersect(Line_Segment line1, Line_Segment line2)
            {
                float x = (line2.B * line1.C - line1.B * line2.C) / (line1.B * line2.A - line2.B * line1.A);
                bool flag1 = Math.Min(line1.StartPos.X, line1.EndPos.X) < x;
                bool flag2 = Math.Max(line1.startpos.X, line1.EndPos.X) > x;
                bool flag3 = Math.Min(line2.StartPos.X, line2.EndPos.X) < x;
                bool flag4 = Math.Max(line2.StartPos.X, line2.EndPos.X) > x;
                if (flag1 && flag2 && flag3 && flag4)
                {
                    return true;
                }
                return false;
            }
            public static bool Is_Same_Side(Vector2 pos1, Vector2 pos2, Line_Segment line)
            {
                return (line.A * pos1.X + line.B * pos1.Y + line.C) * (line.A * pos2.X + line.B * pos2.Y + line.C) > 0;
            }
            public float A
            {
                get
                {
                    return a;
                }
            }
            public float B
            {
                get
                {
                    return b;
                }
            }
            public float C
            {
                get
                {
                    return c;
                }
            }
            private void ABC()
            {
                a = endpos.Y - startpos.Y;
                b = startpos.X - endpos.X;
                c = endpos.X * startpos.Y - endpos.Y * startpos.X;
            }
        }
        //
        //public static void ConvertToStorm(int startX, int endX, int startY, int endY)
        //{
        //    for (int i = startX; i <= endX; i++)
        //    {
        //        for (int j = startY; j <= endY; j++)
        //        {
        //            ConvertToStorm(i, j, true);
        //        }
        //    }
        //}

        //// Token: 0x06000149 RID: 329 RVA: 0x0004FB64 File Offset: 0x0004DD64
        //public static void ConvertToStorm(int x, int y, bool tileframe = true)
        //{
        //    //Mod mod = ModLoader.GetMod("IllusionBoundMod");
        //    if (WorldGen.InWorld(x, y, 1))
        //    {
        //        int type = (int)Main.tile[x, y].type;
        //        int wall = (int)Main.tile[x, y].wall;
        //        if (Main.tile[x, y] != null)
        //        {
        //            if (WallID.Sets.Conversion.Grass[wall])
        //            {
        //                Main.tile[x, y].wall = (ushort)WallType<StormGrassWall>();
        //            }
        //            else if (WallID.Sets.Conversion.HardenedSand[wall])
        //            {
        //                Main.tile[x, y].wall = (ushort)WallType<HardenedStormSandWall>();
        //            }
        //            else if (WallID.Sets.Conversion.Sandstone[wall])
        //            {
        //                Main.tile[x, y].wall = (ushort)WallType<StormSandStoneWall>();
        //            }
        //            else if (WallID.Sets.Conversion.Stone[wall])
        //            {
        //                Main.tile[x, y].wall = (ushort)WallType<StormStoneWall>();
        //            }
        //            else
        //            {
        //                if (wall <= 16)
        //                {
        //                    if (wall != 2 && wall != 16)
        //                    {
        //                        goto IL_156;
        //                    }
        //                }
        //                else if (wall != 59)
        //                {
        //                    if (wall == 71)
        //                    {
        //                        Main.tile[x, y].wall = (ushort)WallType<StormIceWall>();
        //                        goto IL_156;
        //                    }
        //                    if (wall - 196 > 3)
        //                    {
        //                        goto IL_156;
        //                    }
        //                }
        //                Main.tile[x, y].wall = (ushort)WallType<StormDirtWall>();
        //            }
        //        IL_156:
        //            if (TileID.Sets.Conversion.Grass[type] && !TileID.Sets.GrassSpecial[type])
        //            {
        //                Main.tile[x, y].type = (ushort)TileType<StormGrassTile>();
        //            }
        //            else if (TileID.Sets.Conversion.Stone[type] || Main.tileMoss[type])
        //            {
        //                Main.tile[x, y].type = (ushort)TileType<StormStoneTile>();
        //            }
        //            else if (TileID.Sets.Conversion.Sand[type])
        //            {
        //                Main.tile[x, y].type = (ushort)TileType<StormSandTile>();
        //            }
        //            else if (TileID.Sets.Conversion.HardenedSand[type])
        //            {
        //                Main.tile[x, y].type = (ushort)TileType<HardenedStormSandTile>();
        //            }
        //            else if (TileID.Sets.Conversion.Sandstone[type])
        //            {
        //                Main.tile[x, y].type = (ushort)TileType<StormSandStoneTile>();
        //            }
        //            else if (TileID.Sets.Conversion.Ice[type])
        //            {
        //                Main.tile[x, y].type = (ushort)TileType<StormIceTile>();
        //            }
        //            else
        //            {
        //                Tile tile = Main.tile[x, y];
        //                if (type <= 52)
        //                {
        //                    if (type != 0)
        //                    {
        //                        if (type == 52)
        //                        {
        //                            Main.tile[x, y].type = (ushort)TileType<StormVines>();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Main.tile[x, y].type = (ushort)TileType<StormDirtTile>();
        //                    }
        //                }
        //                else if (type != 165)
        //                {
        //                    switch (type)
        //                    {
        //                        case 185:
        //                            if (tile.frameY == 18)
        //                            {
        //                                ushort type2;
        //                                if (tile.frameX >= 1476 && tile.frameX <= 1674)
        //                                {
        //                                    type2 = (ushort)TileType<StormDesertMediumPiles>();
        //                                }
        //                                else if (tile.frameX <= 558 || (tile.frameX >= 1368 && tile.frameX <= 1458))
        //                                {
        //                                    type2 = (ushort)TileType<StormStoneMediumPiles>();
        //                                }
        //                                else
        //                                {
        //                                    if (tile.frameX < 900 || tile.frameX > 1098)
        //                                    {
        //                                        break;
        //                                    }
        //                                    type2 = (ushort)TileType<StormIceMediumPiles>();
        //                                }
        //                                int num = x;
        //                                if (tile.frameX % 36 != 0)
        //                                {
        //                                    num--;
        //                                }
        //                                if (Main.tile[num, y] != null)
        //                                {
        //                                    Main.tile[num, y].type = type2;
        //                                }
        //                                if (Main.tile[num + 1, y] != null)
        //                                {
        //                                    Main.tile[num + 1, y].type = type2;
        //                                }
        //                                while (Main.tile[num, y].frameX >= 216)
        //                                {
        //                                    if (Main.tile[num, y] != null)
        //                                    {
        //                                        Tile tile2 = Main.tile[num, y];
        //                                        tile2.frameX -= 216;
        //                                    }
        //                                    if (Main.tile[num + 1, y] != null)
        //                                    {
        //                                        Tile tile3 = Main.tile[num + 1, y];
        //                                        tile3.frameX -= 216;
        //                                    }
        //                                }
        //                            }
        //                            else if (tile.frameY == 0)
        //                            {
        //                                ushort type3;
        //                                if (tile.frameX >= 972 && tile.frameX <= 1062)
        //                                {
        //                                    type3 = (ushort)TileType<StormDesertSmallPiles>();
        //                                }
        //                                else if (tile.frameX <= 486)
        //                                {
        //                                    type3 = (ushort)TileType<StormStoneSmallPiles>();
        //                                }
        //                                else
        //                                {
        //                                    if (tile.frameX < 648 || tile.frameX > 846)
        //                                    {
        //                                        break;
        //                                    }
        //                                    type3 = (ushort)TileType<StormIceSmallPiles>();
        //                                }
        //                                Main.tile[x, y].type = type3;
        //                                while (Main.tile[x, y].frameX >= 108)
        //                                {
        //                                    Tile tile4 = Main.tile[x, y];
        //                                    tile4.frameX -= 108;
        //                                }
        //                            }
        //                            break;
        //                        case 186:
        //                            if (tile.frameX <= 1170)
        //                            {
        //                                RecursiveReplaceToStorm(186, (ushort)TileType<StormStoneLargePiles>(), x, y, 324, 0, 1170, 0, 18);
        //                            }
        //                            if (tile.frameX >= 1728)
        //                            {
        //                                RecursiveReplaceToStorm(186, (ushort)TileType<StormStoneLargePiles>(), x, y, 324, 1728, 1872, 0, 18);
        //                            }
        //                            if (tile.frameX >= 1404 && tile.frameX <= 1710)
        //                            {
        //                                RecursiveReplaceToStorm(186, (ushort)TileType<StormIceLargePiles>(), x, y, 324, 1404, 1710, 0, 18);
        //                            }
        //                            break;
        //                        case 187:
        //                            if (tile.frameX >= 1566 && tile.frameY < 36)
        //                            {
        //                                RecursiveReplaceToStorm(187, (ushort)TileType<StormDesertLargePiles>(), x, y, 324, 1566, 1872, 0, 18);
        //                            }
        //                            if (tile.frameX >= 756 && tile.frameX <= 900)
        //                            {
        //                                RecursiveReplaceToStorm(187, (ushort)TileType<StormStoneLargePiles>(), x, y, 324, 756, 900, 0, 18);
        //                            }
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    int num2 = (tile.frameY <= 54) ? ((tile.frameY % 36 == 0) ? y : (y - 1)) : y;
        //                    bool flag = tile.frameY <= 54;
        //                    bool flag2 = tile.frameY <= 18 || tile.frameY == 72;
        //                    ushort type4;
        //                    if (tile.frameX >= 378 && tile.frameX <= 414)
        //                    {
        //                        type4 = (ushort)TileType<StormDesertStalactite>();
        //                    }
        //                    else if ((tile.frameX >= 54 && tile.frameX <= 90) || (tile.frameX >= 216 && tile.frameX <= 360))
        //                    {
        //                        type4 = (ushort)TileType<StormStoneStalactite>();
        //                    }
        //                    else
        //                    {
        //                        if (tile.frameX > 36)
        //                        {
        //                            goto IL_856;
        //                        }
        //                        type4 = (ushort)TileType<StormIceStalactite>();
        //                    }
        //                    if (Main.tile[x, num2] != null)
        //                    {
        //                        Main.tile[x, num2].type = type4;
        //                    }
        //                    if (flag && Main.tile[x, num2 + 1] != null)
        //                    {
        //                        Main.tile[x, num2 + 1].type = type4;
        //                    }
        //                    while (Main.tile[x, num2].frameX >= 54)
        //                    {
        //                        if (Main.tile[x, num2] != null)
        //                        {
        //                            Tile tile5 = Main.tile[x, num2];
        //                            tile5.frameX -= 54;
        //                        }
        //                        if (flag && Main.tile[x, num2 + 1] != null)
        //                        {
        //                            Tile tile6 = Main.tile[x, num2 + 1];
        //                            tile6.frameX -= 54;
        //                        }
        //                    }
        //                    if (flag2)
        //                    {
        //                        ConvertToStorm(x, num2 - 1, true);
        //                    }
        //                    else if (flag)
        //                    {
        //                        ConvertToStorm(x, num2 + 2, true);
        //                    }
        //                    else
        //                    {
        //                        ConvertToStorm(x, num2 + 1, true);
        //                    }
        //                }
        //            }
        //        IL_856:
        //            if (tileframe)
        //            {
        //                if (Main.netMode == 0)
        //                {
        //                    WorldGen.SquareTileFrame(x, y, true);
        //                    return;
        //                }
        //                if (Main.netMode == 2)
        //                {
        //                    NetMessage.SendTileSquare(-1, x, y, 1, 0);
        //                }
        //            }
        //        }
        //    }
        //}
        //public static void RecursiveReplaceToStorm(ushort checkType, ushort replaceType, int x, int y, int replaceTextureWidth, int minFrameX = 0, int maxFrameX = 2147483647, int minFrameY = 0, int maxFrameY = 2147483647)
        //{
        //    Tile tile = Main.tile[x, y];
        //    if (tile == null || !tile.active() || tile.type != checkType || (int)tile.frameX < minFrameX || (int)tile.frameX > maxFrameX || (int)tile.frameY < minFrameY || (int)tile.frameY > maxFrameY)
        //    {
        //        return;
        //    }
        //    Main.tile[x, y].type = replaceType;
        //    while ((int)Main.tile[x, y].frameX >= replaceTextureWidth)
        //    {
        //        Tile tile2 = Main.tile[x, y];
        //        tile2.frameX -= (short)replaceTextureWidth;
        //    }
        //    if (Main.tile[x - 1, y] != null)
        //    {
        //        RecursiveReplaceToStorm(checkType, replaceType, x - 1, y, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
        //    }
        //    if (Main.tile[x + 1, y] != null)
        //    {
        //        RecursiveReplaceToStorm(checkType, replaceType, x + 1, y, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
        //    }
        //    if (Main.tile[x, y - 1] != null)
        //    {
        //        RecursiveReplaceToStorm(checkType, replaceType, x, y - 1, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
        //    }
        //    if (Main.tile[x, y + 1] != null)
        //    {
        //        RecursiveReplaceToStorm(checkType, replaceType, x, y + 1, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
        //    }
        //}
        //// Token: 0x0600014A RID: 330 RVA: 0x000503EC File Offset: 0x0004E5EC
        //public static void RecursiveReplaceFromStorm(ushort checkType, ushort replaceType, int x, int y, int addFrameX, int addFrameY)
        //{
        //    Tile tile = Main.tile[x, y];
        //    if (tile == null || !tile.active() || tile.type != checkType)
        //    {
        //        return;
        //    }
        //    Main.tile[x, y].type = replaceType;
        //    Tile tile2 = Main.tile[x, y];
        //    tile2.frameX += (short)addFrameX;
        //    Tile tile3 = Main.tile[x, y];
        //    tile3.frameY += (short)addFrameY;
        //    if (Main.tile[x - 1, y] != null)
        //    {
        //        RecursiveReplaceFromStorm(checkType, replaceType, x - 1, y, addFrameX, addFrameY);
        //    }
        //    if (Main.tile[x + 1, y] != null)
        //    {
        //        RecursiveReplaceFromStorm(checkType, replaceType, x + 1, y, addFrameX, addFrameY);
        //    }
        //    if (Main.tile[x, y - 1] != null)
        //    {
        //        RecursiveReplaceFromStorm(checkType, replaceType, x, y - 1, addFrameX, addFrameY);
        //    }
        //    if (Main.tile[x, y + 1] != null)
        //    {
        //        RecursiveReplaceFromStorm(checkType, replaceType, x, y + 1, addFrameX, addFrameY);
        //    }
        //}
        //public static void ReplaceStormStalactite(ushort checkType, ushort replaceType, ushort replaceOriginTile, int x, int y, int addFrameX, int addFrameY)
        //{
        //    Tile tile = Main.tile[x, y];
        //    int num = (tile.frameY <= 54) ? ((tile.frameY % 36 == 0) ? y : (y - 1)) : y;
        //    bool flag = tile.frameY <= 54;
        //    int num2 = (tile.frameY <= 18 || tile.frameY == 72) ? (num - 1) : (flag ? (num + 2) : (y + 1));
        //    if (Main.tile[x, num++] != null)
        //    {
        //        Main.tile[x, num++].type = replaceType;
        //    }
        //    if (flag && Main.tile[x, num] != null)
        //    {
        //        Main.tile[x, num].type = replaceType;
        //    }
        //    if (Main.tile[x, num2] != null)
        //    {
        //        Main.tile[x, num2].type = replaceOriginTile;
        //    }
        //}
        //public static void ConvertFromStorm(int x, int y, ConvertType convert)
        //{
        //    Tile tile = Main.tile[x, y];
        //    int type = (int)tile.type;
        //    int wall = (int)tile.wall;
        //    if (WorldGen.InWorld(x, y, 1))
        //    {
        //        if (Main.tile[x, y] != null)
        //        {
        //            if (wall == WallType<StormDirtWall>())
        //            {
        //                Main.tile[x, y].wall = 2;
        //            }
        //            else if (wall == WallType<StormGrassWall>())
        //            {
        //                switch (convert)
        //                {
        //                    case ConvertType.Pure:
        //                        Main.tile[x, y].wall = 63;
        //                        break;
        //                    case ConvertType.Corrupt:
        //                        Main.tile[x, y].wall = 69;
        //                        break;
        //                    case ConvertType.Crimson:
        //                        Main.tile[x, y].wall = 81;
        //                        break;
        //                    case ConvertType.Hallow:
        //                        Main.tile[x, y].wall = 70;
        //                        break;
        //                }
        //            }
        //            else if (wall == WallType<StormIceWall>())
        //            {
        //                Main.tile[x, y].wall = 71;
        //            }
        //            else if (wall == WallType<StormStoneWall>())
        //            {
        //                switch (convert)
        //                {
        //                    case ConvertType.Pure:
        //                        Main.tile[x, y].wall = 1;
        //                        break;
        //                    case ConvertType.Corrupt:
        //                        Main.tile[x, y].wall = 3;
        //                        break;
        //                    case ConvertType.Crimson:
        //                        Main.tile[x, y].wall = 83;
        //                        break;
        //                    case ConvertType.Hallow:
        //                        Main.tile[x, y].wall = 28;
        //                        break;
        //                }
        //            }
        //        }
        //        if (Main.tile[x, y] != null)
        //        {
        //            if (type == TileType<StormDirtTile>())
        //            {
        //                tile.type = 0;
        //            }
        //            else if (type == TileType<StormGrassTile>())
        //            {
        //                SetTileFromConvert(x, y, convert, 23, 199, 109, 2);
        //            }
        //            else if (type == TileType<StormStoneTile>())
        //            {
        //                SetTileFromConvert(x, y, convert, 25, 203, 117, 1);
        //            }
        //            else if (type == TileType<StormSandTile>())
        //            {
        //                SetTileFromConvert(x, y, convert, 112, 234, 116, 53);
        //            }
        //            else if (type == TileType<StormSandStoneTile>())
        //            {
        //                SetTileFromConvert(x, y, convert, 400, 401, 403, 396);
        //            }
        //            else if (type == TileType<HardenedStormSandTile>())
        //            {
        //                SetTileFromConvert(x, y, convert, 398, 399, 402, 397);
        //            }
        //            else if (type == TileType<StormIceTile>())
        //            {
        //                SetTileFromConvert(x, y, convert, 163, 200, 164, 161);
        //            }
        //            else if (type == TileType<StormVines>())
        //            {
        //                SetTileFromConvert(x, y, convert, ushort.MaxValue, 205, 115, 52);
        //            }
        //            //else if (type == instance.TileType("AstralShortPlants"))
        //            //{
        //            //	SetTileFromConvert(x, y, convert, 24, ushort.MaxValue, 110, 3);
        //            //}
        //            //else if (type == instance.TileType("AstralTallPlants"))
        //            //{
        //            //	SetTileFromConvert(x, y, convert, ushort.MaxValue, ushort.MaxValue, 113, 73);
        //            //}
        //            else if (type == TileType<StormStoneLargePiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 186, x, y, 378, 0);
        //            }
        //            else if (type == TileType<StormStoneMediumPiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 185, x, y, 0, 18);
        //            }
        //            else if (type == TileType<StormStoneSmallPiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 185, x, y, 0, 0);
        //            }
        //            else if (type == TileType<StormDesertLargePiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 187, x, y, 1566, 0);
        //            }
        //            else if (type == TileType<StormDesertMediumPiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 185, x, y, 1476, 18);
        //            }
        //            else if (type == TileType<StormDesertSmallPiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 185, x, y, 972, 0);
        //            }
        //            else if (type == TileType<StormIceLargePiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 186, x, y, 1404, 0);
        //            }
        //            else if (type == TileType<StormIceMediumPiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 185, x, y, 900, 18);
        //            }
        //            else if (type == TileType<StormIceSmallPiles>())
        //            {
        //                RecursiveReplaceFromStorm((ushort)type, 185, x, y, 648, 0);
        //            }
        //            else if (type == TileType<StormStoneStalactite>())
        //            {
        //                ushort replaceOriginTile = 1;
        //                int addFrameX = 54;
        //                switch (convert)
        //                {
        //                    case ConvertType.Corrupt:
        //                        replaceOriginTile = 25;
        //                        addFrameX = 324;
        //                        break;
        //                    case ConvertType.Crimson:
        //                        replaceOriginTile = 203;
        //                        addFrameX = 270;
        //                        break;
        //                    case ConvertType.Hallow:
        //                        replaceOriginTile = 117;
        //                        addFrameX = 216;
        //                        break;
        //                }
        //                ReplaceStormStalactite((ushort)type, 165, replaceOriginTile, x, y, addFrameX, 0);
        //            }
        //            else if (type == TileType<StormDesertStalactite>())
        //            {
        //                ushort replaceOriginTile2 = 396;
        //                int addFrameX2 = 378;
        //                switch (convert)
        //                {
        //                    case ConvertType.Corrupt:
        //                        replaceOriginTile2 = 400;
        //                        addFrameX2 = 324;
        //                        break;
        //                    case ConvertType.Crimson:
        //                        replaceOriginTile2 = 401;
        //                        addFrameX2 = 270;
        //                        break;
        //                    case ConvertType.Hallow:
        //                        replaceOriginTile2 = 403;
        //                        addFrameX2 = 216;
        //                        break;
        //                }
        //                ReplaceStormStalactite((ushort)type, 165, replaceOriginTile2, x, y, addFrameX2, 0);
        //            }
        //            else if (type == TileType<StormIceStalactite>())
        //            {
        //                ReplaceStormStalactite((ushort)type, 165, 161, x, y, 0, 0);
        //            }
        //            if (TileID.Sets.Conversion.Grass[type] || type == 0)
        //            {
        //                WorldGen.SquareTileFrame(x, y, true);
        //            }
        //        }
        //    }
        //}
        //public static void SetTileFromConvert(int x, int y, ConvertType convert, ushort corrupt, ushort crimson, ushort hallow, ushort pure)
        //{
        //    switch (convert)
        //    {
        //        case ConvertType.Pure:
        //            if (pure != 65535)
        //            {
        //                Main.tile[x, y].type = pure;
        //                WorldGen.SquareTileFrame(x, y, true);
        //            }
        //            break;
        //        case ConvertType.Corrupt:
        //            if (corrupt != 65535)
        //            {
        //                Main.tile[x, y].type = corrupt;
        //                WorldGen.SquareTileFrame(x, y, true);
        //                return;
        //            }
        //            break;
        //        case ConvertType.Crimson:
        //            if (crimson != 65535)
        //            {
        //                Main.tile[x, y].type = crimson;
        //                WorldGen.SquareTileFrame(x, y, true);
        //                return;
        //            }
        //            break;
        //        case ConvertType.Hallow:
        //            if (hallow != 65535)
        //            {
        //                Main.tile[x, y].type = hallow;
        //                WorldGen.SquareTileFrame(x, y, true);
        //                return;
        //            }
        //            break;
        //        default:
        //            return;
        //    }
        //}
        public static Vector2 RandVec(this int l)
        {
            return new Vector2(Main.rand.NextFloat(0, l), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
        }
        public static Vector2 RandVec(this float l)
        {
            return new Vector2(Main.rand.NextFloat(0, l), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
        }
        public static void DrawProjShadow(this SpriteBatch spriteBatch, Projectile projectile, Color lightColor)
        {
            Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
            int frameHeight = projectileTexture.Height / Main.projFrames[projectile.type];
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width, projectile.height) / 2 + new Vector2(1f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.5f) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(projectileTexture, drawPos, new Rectangle(0, frameHeight * projectile.frame, projectileTexture.Width, frameHeight), color, projectile.rotation, new Vector2(TextureAssets.Projectile[projectile.type].Value.Width * 0.5f, frameHeight * 0.5f), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
            }
        }
        public static void ProjFrameChanger(this Projectile projectile, int frames, int time)
        {
            Main.projFrames[projectile.type] = frames;
            projectile.frame += (int)IllusionBoundMod.ModTime % time == 0 ? 1 : 0;
            projectile.frame %= frames;
        }
        //public static void BlockRunner(int i, int j, double strength, int steps, ushort type, bool air = false, bool rand = false)
        //{
        //    //就是取消了某些替换条件的OreRunner
        //    double num = strength;
        //    float num2 = (float)steps;
        //    Vector2 vector;
        //    vector.X = (float)i;
        //    vector.Y = (float)j;
        //    Vector2 vector2;
        //    vector2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
        //    vector2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
        //    while (num > 0.0 && num2 > 0f)
        //    {
        //        if (vector.Y < 0f && num2 > 0f && type == 59)
        //        {
        //            num2 = 0f;
        //        }
        //        num = strength * (double)(num2 / (float)steps);
        //        num2 -= 1f;
        //        int num3 = (int)((double)vector.X - num * 0.5);
        //        int num4 = (int)((double)vector.X + num * 0.5);
        //        int num5 = (int)((double)vector.Y - num * 0.5);
        //        int num6 = (int)((double)vector.Y + num * 0.5);
        //        if (num3 < 0)
        //        {
        //            num3 = 0;
        //        }
        //        if (num4 > Main.maxTilesX)
        //        {
        //            num4 = Main.maxTilesX;
        //        }
        //        if (num5 < 0)
        //        {
        //            num5 = 0;
        //        }
        //        if (num6 > Main.maxTilesY)
        //        {
        //            num6 = Main.maxTilesY;
        //        }
        //        bool flag = WorldGen.genRand.Next(3) == 0;
        //        for (int k = num3; k < num4; k++)
        //        {
        //            for (int l = num5; l < num6; l++)
        //            {
        //                if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015 + (rand ? WorldGen.genRand.Next((int)strength / -6, (int)strength / 6) : 0)) && Main.tile[k, l].active())
        //                {
        //                    if (air)
        //                    {
        //                        Main.tile[k, l].active(false);
        //                        if (flag)
        //                        {
        //                            Main.tile[k, l].liquid = byte.MaxValue;
        //                            Main.tile[k, l].lava(false);
        //                        }
        //                    }
        //                    Main.tile[k, l].type = type;
        //                    WorldGen.SquareTileFrame(k, l, true);
        //                    if (Main.netMode == 2)
        //                    {
        //                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
        //                    }
        //                }
        //            }
        //        }
        //        vector += vector2;
        //        vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
        //        if (vector2.X > 1f)
        //        {
        //            vector2.X = 1f;
        //        }
        //        if (vector2.X < -1f)
        //        {
        //            vector2.X = -1f;
        //        }
        //    }
        //}
        public static bool ContainsValue(this int[] values, int value)
        {
            foreach (var v in values)
            {
                if (v == value)
                {
                    return true;
                }
            }
            return false;
        }
        public static void LinerDust(Vector2 vec1, Vector2 vec2, int type = MyDustId.Fire, float step = 2)
        {
            for (float n = 0; n <= (vec1 - vec2).Length(); n += step)
            {
                Dust.NewDustPerfect(Vector2.Lerp(vec1, vec2, n / (vec1 - vec2).Length()), type, default, newColor: Color.White).noGravity = true;
            }
        }
        public static void LinerDust(Vector3 vec1, Vector3 vec2, float height, Vector2 projCenter = default, Vector2 drawOffset = default, int type = MyDustId.Fire, float step = 2)
        {
            var v1 = vec1.Projectile(height, projCenter);
            var v2 = vec2.Projectile(height, projCenter);
            for (float n = 0; n <= (v1 - v2).Length(); n += step)
            {
                Dust.NewDustPerfect(Vector2.Lerp(v1, v2, n / (v1 - v2).Length()) + drawOffset, type, default, newColor: Color.White).noGravity = true;
            }
        }
        public static void LinerDust(Vector4 vec1, Vector4 vec2, float heightZ, float heightW, Vector2 drawOffset = default, Vector2 projCenter = default, int type = MyDustId.Fire, float step = 2)
        {
            var v1 = vec1.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter);
            var v2 = vec2.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter);
            for (float n = 0; n <= (v1 - v2).Length(); n += step)
            {
                Dust.NewDustPerfect(Vector2.Lerp(v1, v2, n / (v1 - v2).Length()) + drawOffset, type, default, newColor: Color.White).noGravity = true;
            }
        }
        public static void LinerDust(Vector4 vec1, Vector4 vec2, float heightZ, float heightW, Action<Dust> action, Vector2 drawOffset = default, Vector2 projCenter = default, int type = MyDustId.Fire, float step = 2)
        {
            var v1 = vec1.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter);
            var v2 = vec2.Projectile(heightW, new Vector3(projCenter, 0)).Projectile(heightZ, projCenter);
            for (float n = 0; n <= (v1 - v2).Length(); n += step)
            {
                var d = Dust.NewDustPerfect(Vector2.Lerp(v1, v2, n / (v1 - v2).Length()) + drawOffset, type, default, newColor: Color.White);
                action?.Invoke(d);
            }
        }
        public static void GetBoundPoints(Vector2 vs, ref Vector2 ve, out Vector2 start, out Vector2 end)
        {
            if ((vs - ve).Length() < 1f)
            {
                ve = vs + new Vector2(32, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
            }
            if (vs.X - ve.X == 0)
            {
                start = new Vector2(vs.X, vs.Y > ve.Y ? -560 : 560);
                end = new Vector2(vs.X, vs.Y > ve.Y ? 560 : -560);
                return;
            }
            if (vs.Y - ve.Y == 0)
            {
                start = new Vector2(vs.X > ve.X ? -960 : 960, vs.Y);
                end = new Vector2(vs.X > ve.X ? 960 : -960, vs.Y);
                return;
            }
            float k = (vs.Y - ve.Y) / (vs.X - ve.X);
            float b = vs.Y - vs.X * (vs.Y - ve.Y) / (vs.X - ve.X);
            Vector2?[] vec1 = new Vector2?[] { new Vector2(-960, k * -960 + b), new Vector2(960, k * 960 + b), new Vector2((-560 - b) / k, -560), new Vector2((560 - b) / k, 560) };
            for (int n = 0; n < 4; n++)
            {
                if (n < 2)
                {
                    if (vec1[n].Value.Y > 560 || vec1[n].Value.Y < -560)
                    {
                        vec1[n] = null;
                    }
                }
                else
                {
                    if (vec1[n].Value.X > 960 || vec1[n].Value.X < -960)
                    {
                        vec1[n] = null;
                    }
                }
            }
            Vector2[] vecs2 = new Vector2[2];
            for (int i = 0; i < 2; i++)
            {
                for (int n = 0; n < 4; n++)
                {
                    if (vec1[n].HasValue)
                    {
                        vecs2[i] = vec1[n].Value;
                        vec1[n] = null;
                        break;
                    }
                }
            }
            if (vs.X > ve.X)
            {
                start = vecs2[0].X > vecs2[1].X ? vecs2[1] : vecs2[0];
                end = vecs2[0].X > vecs2[1].X ? vecs2[0] : vecs2[1];
            }
            else
            {
                start = vecs2[0].X > vecs2[1].X ? vecs2[0] : vecs2[1];
                end = vecs2[0].X > vecs2[1].X ? vecs2[1] : vecs2[0];
            }
        }
        public static float GetRad(Vector2 vec)
        {
            //废弃
            return (new Vector2(-vec.X, vec.Y)).ToRotation() + MathHelper.Pi;
        }
        public static void GetBoundPoints_Wrong(Vector2 vs, Vector2 ve, out Vector2 start, out Vector2 end)
        {
            //废弃
            Vector2 vOfRightDown = new Vector2(60, 35) * 16 - vs;
            Vector2 vOfRightUp = new Vector2(60, -35) * 16 - vs;
            Vector2 vOfLeftUp = new Vector2(-60, -35) * 16 - vs;
            Vector2 vOfLeftDown = new Vector2(-60, 35) * 16 - vs;
            Vector2 vOfTarget = ve - vs;
            float rOfRightDown = GetRad(vOfRightDown);
            float rOfRightUp = GetRad(vOfRightUp) - rOfRightDown;
            float rOfLeftUp = GetRad(vOfLeftUp) - rOfRightDown;
            float rOfLeftDown = GetRad(vOfLeftDown) - rOfRightDown;
            float rOftarget = GetRad(vOfTarget) - rOfRightDown;
            if (rOftarget >= rOfLeftDown)
            {
                start = new Vector2((560 - ve.X) * (vs.X - ve.X) / (vs.Y - ve.Y) + vs.X, 560);
                end = new Vector2((-560 - ve.X) * (vs.X - ve.X) / (vs.Y - ve.Y) + vs.X, -560);

            }
            else if (rOftarget >= rOfLeftUp)
            {
                start = new Vector2(-960, (vs.Y - ve.Y) / (vs.X - ve.X) * (-960 - vs.X) + vs.Y);
                end = new Vector2(960, (vs.Y - ve.Y) / (vs.X - ve.X) * (960 - vs.X) + vs.Y);
            }
            else if (rOftarget >= rOfRightUp)
            {
                start = new Vector2((-560 - ve.X) * (vs.X - ve.X) / (vs.Y - ve.Y) + vs.X, -560);
                end = new Vector2((560 - ve.X) * (vs.X - ve.X) / (vs.Y - ve.Y) + vs.X, 560);
            }
            else
            {
                start = new Vector2(960, (vs.Y - ve.Y) / (vs.X - ve.X) * (960 - vs.X) + vs.Y);
                end = new Vector2(-960, (vs.Y - ve.Y) / (vs.X - ve.X) * (-960 - vs.X) + vs.Y);
            }
            float l = (start - end).Length();
            if (l > (float)Math.Sqrt(772) * 80)
            {
                Main.NewText(start, Color.Red);
                Main.NewText(end, Color.Cyan);
                Main.NewText(vs, Color.White);
                Main.NewText(ve, Color.Green);
            }
        }
        public static Color ToColor(this Vector4 vec)
        {
            return new Color((int)vec.X, (int)vec.Y, (int)vec.Z, (int)vec.W);
        }
        public static Color ToColor(this Vector3 vec)
        {
            return new Color((int)vec.X, (int)vec.Y, (int)vec.Z);
        }
        public static double GaussianRandom(double mu, double sigma, UnifiedRandom random = default)
        {
            double u = -2 * Math.Log(random.NextDouble());
            double v = 2 * Math.PI * random.NextDouble();
            return Math.Sqrt(u) * Math.Cos(v) * sigma + mu;
        }
        public static float CrossLength(this Vector2 O, Vector2 A)
        {
            return O.X * A.Y - O.Y * A.X;
        }
        public static bool InTriangle(this Vector2 O, Vector2 A, Vector2 B, Vector2 C)
        {
            Vector2 v1 = O - A;
            Vector2 v2 = O - B;
            Vector2 v3 = O - C;
            Vector2 v4 = B - A;
            Vector2 v5 = C - B;
            Vector2 v6 = A - C;
            bool flag1 = v1.CrossLength(v4) >= 0 && v2.CrossLength(v5) >= 0 && v3.CrossLength(v6) >= 0;
            //bool flag2 = O.CrossLength(A) <= 0 && O.CrossLength(B) <= 0 && O.CrossLength(C) <= 0;
            return flag1;
        }
        //public static Vector2 RotationMartix(this Vector2 vector, float r)
        //{
        //	return new Vector2(vector.X * (float)Math.Cos(r) - vector.Y * (float)Math.Sin(r), vector.X * (float)Math.Sin(r) + vector.Y * (float)Math.Cos(r));
        //}
        public static Vector2 MultiplyMartix(this Vector2 vector, float a, float b, float c, float d)
        {
            return new Vector2(vector.X * a + vector.Y * b, vector.X * c + vector.Y * d);
        }
        /// <summary>
        /// 求阶乘
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Factorial(this int n)
        {
            if (n == 0)
            {
                return 1;
            }
            return n * (n - 1).Factorial();
        }
        /// <summary>
        /// 求组合
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int Combination(int m, int n)
        {
            return n.Factorial() / (m.Factorial() * (n - m).Factorial());
        }
        /// <summary>
        /// 求t对应的贝塞尔曲线坐标
        /// </summary>
        /// <param name="pos">节点</param>
        /// <param name="t">时间</param>
        /// <returns></returns>
        public static Vector2 BesselCurve(this Vector2[] pos, double t)
        {
            int n = pos.Length - 1;
            if (n == 0)
            {
                return Vector2.Zero;
            }

            if (t == 0)
            {
                return pos[0];
            }
            Vector2 p = Vector2.Zero;
            for (int i = 0; i <= n; i++)
            {
                p += pos[i] * Combination(i, n) * (float)(Math.Pow(1 - t, n - i) * Math.Pow(t, i));
            }
            return p;
        }
        public static Vector2[] BesselCurve(this Vector2[] pos, int length)
        {
            Vector2[] curvePoses = new Vector2[length];
            for (int n = 0; n < length; n++)
            {
                curvePoses[n] = BesselCurve(pos, n / (length - 1f));
            }
            return curvePoses;
        }
        public static float ODEStarTimer;
        public delegate bool TileActionAttempt(int x, int y);
        private static bool PlotLine(int x0, int y0, int x1, int y1, TileActionAttempt plot, bool jump = true)
        {
            if (x0 == x1 && y0 == y1)
            {
                return plot(x0, y0);
            }
            bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (flag)
            {
                Terraria.Utils.Swap(ref x0, ref y0);
                Terraria.Utils.Swap(ref x1, ref y1);
            }
            int num = Math.Abs(x1 - x0);
            int num2 = Math.Abs(y1 - y0);
            int num3 = num / 2;
            int num4 = y0;
            int num5 = (x0 < x1) ? 1 : -1;
            int num6 = (y0 < y1) ? 1 : -1;
            for (int num7 = x0; num7 != x1; num7 += num5)
            {
                if (flag)
                {
                    if (!plot(num4, num7))
                    {
                        return false;
                    }
                }
                else if (!plot(num7, num4))
                {
                    return false;
                }
                num3 -= num2;
                if (num3 < 0)
                {
                    num4 += num6;
                    if (!jump)
                    {
                        if (flag)
                        {
                            if (!plot(num4, num7))
                            {
                                return false;
                            }
                        }
                        else if (!plot(num7, num4))
                        {
                            return false;
                        }
                    }
                    num3 += num;
                }
            }
            return true;
        }
        public static bool PlotTileLine(Vector2 start, Vector2 end, float width, TileActionAttempt plot)
        {
            float scaleFactor = width / 2f;
            Vector2 value = end - start;
            Vector2 vector = value / value.Length();
            Vector2 value2 = new Vector2(-vector.Y, vector.X) * scaleFactor;
            Point point = (start - value2).ToTileCoordinates();
            Point point2 = (start + value2).ToTileCoordinates();
            Point point3 = start.ToTileCoordinates();
            Point point4 = end.ToTileCoordinates();
            Point lineMinOffset = new Point(point.X - point3.X, point.Y - point3.Y);
            Point lineMaxOffset = new Point(point2.X - point3.X, point2.Y - point3.Y);
            return PlotLine(point3.X, point3.Y, point4.X, point4.Y, (int x, int y) => PlotLine(x + lineMinOffset.X, y + lineMinOffset.Y, x + lineMaxOffset.X, y + lineMaxOffset.Y, plot, false), true);
        }
        public static void DrawProj_PiercingStarlight(Texture2D value2, Projectile proj, int Type)
        {
            //1落英之刺 2八分咲 3赤铜之锋 4最废矿物 5同志短剑 6达瓦里氏短剑冲刺 7星流 8银辉星流
            int num = 3;
            int num2 = 2;
            Vector2 value = proj.Center - proj.rotation.ToRotationVector2() * num2;
            for (int i = 0; i < 1; i++)
            {
                float num3 = Main.rand.NextFloat();
                float scale = GetLerpValue(0f, 0.3f, num3, true) * GetLerpValue(1f, 0.5f, num3, true);
                Color color = proj.GetAlpha(Color.White) * scale;
                //Color color = Color.White;
                Vector2 origin = value2.Size() / 2f;
                float num4 = Main.rand.NextFloatDirection();
                float scaleFactor = 8f + MathHelper.Lerp(0f, 20f, num3) + Main.rand.NextFloat() * 6f;
                float num5 = proj.rotation + num4 * 6.28318548f * 0.04f;
                float num6 = num5 + 0.7853982f;
                Vector2 position = value + num5.ToRotationVector2() * scaleFactor + Main.rand.NextVector2Circular(8f, 8f) - Main.screenPosition;
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (proj.rotation < -1.57079637f || proj.rotation > 1.57079637f)
                {
                    num6 += 1.57079637f;
                    spriteEffects |= SpriteEffects.FlipHorizontally;
                }
                Main.spriteBatch.Draw(value2, position, null, color, num6 + MathHelper.Pi, origin, 1f, spriteEffects, 0f);
            }
            for (int j = 0; j < num; j++)
            {
                float num7 = Main.rand.NextFloat();
                float num8 = GetLerpValue(0f, 0.3f, num7, true) * GetLerpValue(1f, 0.5f, num7, true);
                float amount = GetLerpValue(0f, 0.3f, num7, true) * GetLerpValue(1f, 0.5f, num7, true);
                float scaleFactor2 = MathHelper.Lerp(0.6f, 1f, amount);
                //Color fairyQueenWeaponsColor = proj.GetFairyQueenWeaponsColor(0.25f, 0f, new float?((Main.rand.NextFloat() * 0.33f + Main.GlobalTimeWrappedHourly) % 1f));
                Color fairyQueenWeaponsColor = new Color(255, 153, 255);
                if (Type == 2)
                {
                    fairyQueenWeaponsColor = Color.Lerp(new Color(255, 153, 255), new Color(249, 52, 243), (float)Math.Sin(MathHelper.Pi / 60 * Main.time) / 2 + 0.5f);
                }
                if (Type == 3)
                {
                    fairyQueenWeaponsColor = new Color(235, 166, 135);
                }
                if (Type == 4)
                {
                    fairyQueenWeaponsColor = Color.Lerp(new Color(235, 166, 135), new Color(205, 134, 71), (float)Math.Sin(MathHelper.Pi / 60 * Main.time) / 2 + 0.5f);
                }
                if (Type == 5)
                {
                    fairyQueenWeaponsColor = Color.Lerp(new Color(255, 251, 133), new Color(255, 128, 128), (float)Math.Sin(MathHelper.Pi / 60 * Main.time) / 2 + 0.5f);
                }
                if (Type == 6)
                {
                    fairyQueenWeaponsColor = Color.Lerp(new Color(255, 0, 0), new Color(255, 255, 0), (float)Math.Sin(MathHelper.Pi / 30 * Main.time) / 2 + 0.5f);
                }
                if (Type == 7)
                {
                    if (ODEStarTimer >= 3f)
                    {
                        fairyQueenWeaponsColor = Color.Lerp(new Color(254, 158, 35), new Color(0, 174, 238), ODEStarTimer - 3);
                    }
                    else if (ODEStarTimer >= 2f)
                    {
                        fairyQueenWeaponsColor = Color.Lerp(new Color(254, 126, 229), new Color(254, 158, 35), ODEStarTimer - 2);
                    }
                    else if (ODEStarTimer >= 1f)
                    {
                        fairyQueenWeaponsColor = Color.Lerp(new Color(34, 221, 151), new Color(254, 126, 229), ODEStarTimer - 1);
                    }
                    else if (ODEStarTimer >= 0f)
                    {
                        fairyQueenWeaponsColor = Color.Lerp(new Color(0, 174, 238), new Color(34, 221, 151), ODEStarTimer);
                    }
                }
                if (Type == 8)
                {
                    fairyQueenWeaponsColor = Color.White;
                }
                Texture2D value3 = TextureAssets.Projectile[proj.type].Value;
                Color color2 = fairyQueenWeaponsColor;
                color2 *= num8 * 0.5f;
                Vector2 origin2 = value3.Size() / 2f;
                Color value4 = Color.White * num8;
                value4.A /= 2;
                Color color3 = value4 * 0.5f;
                float num9 = 1f;
                float num10 = Main.rand.NextFloat() * 2f;
                float num11 = Main.rand.NextFloatDirection();
                Vector2 vector = new Vector2(2.8f + num10, 1f) * num9 * scaleFactor2;
                //new Vector2(1.5f + num10 * 0.5f, 1f) * num9 * scaleFactor2;
                int num12 = 50;
                Vector2 value5 = proj.rotation.ToRotationVector2() * ((j >= 1) ? 56 : 0);
                float num13 = 0.03f - j * 0.012f;
                float scaleFactor3 = 30f + MathHelper.Lerp(0f, num12, num7) + num10 * 16f;
                float num14 = proj.rotation + num11 * 6.28318548f * num13;
                float rotation = num14;
                color2 *= num9;
                color3 *= num9;
                SpriteEffects effects = SpriteEffects.None;
                if (Type == 1)
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 * 0.2f + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2, rotation, origin2, vector * 0.5f, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3, rotation, origin2, vector * 0.6f * 0.5f, effects, 0f);
                }
                else if (Type == 2)
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 * 0.6f + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2, rotation, origin2, vector * 0.8f, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3, rotation, origin2, vector * 0.6f * 0.8f, effects, 0f);
                }
                else if (Type == 3)
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 * -0.8f + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2, rotation, origin2, vector * 0.5f, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3, rotation, origin2, vector * 0.6f * 0.5f, effects, 0f);
                }
                else if (Type == 4)
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 * -1.6f + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2, rotation, origin2, vector * 0.8f, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3, rotation, origin2, vector * 0.6f * 0.8f, effects, 0f);
                }
                else if (Type == 6)
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 * -2f + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2, rotation, origin2, vector, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3, rotation, origin2, vector * 0.6f, effects, 0f);
                }
                else if (Type == 6)
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2 * 1.5f, rotation, origin2, vector, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3 * 1.5f, rotation, origin2, vector * 0.6f, effects, 0f);
                }
                else
                {
                    Vector2 position2 = value + num14.ToRotationVector2() * scaleFactor3 + Main.rand.NextVector2Circular(20f, 20f) + value5 - Main.screenPosition;
                    Main.spriteBatch.Draw(value3, position2, null, color2, rotation, origin2, vector, effects, 0f);
                    Main.spriteBatch.Draw(value3, position2, null, color3, rotation, origin2, vector * 0.6f, effects, 0f);
                }
            }
        }
        public static Vector2 RotatedRelativePoint(this Player player, Vector2 pos, bool reverseRotation = false, bool addGfxOffY = true)
        {
            float num = reverseRotation ? (-player.fullRotation) : player.fullRotation;
            Vector2 vector = player.Bottom + new Vector2(0f, player.gfxOffY);
            int num2 = player.mount.PlayerOffset / 2 + 4;
            Vector2 value = new Vector2(0f, (float)(-(float)num2)) + new Vector2(0f, num2).RotatedBy((double)num, default);
            if (addGfxOffY)
            {
                pos.Y += player.gfxOffY;
            }
            pos = vector + (pos - vector).RotatedBy((double)num, default) + value;
            return pos;
        }
        public static int MultiplyK(this int value, float k, bool negative = false)
        {
            if (negative)
            {
                return (int)(value * (1 - k));
            }

            return (int)(value * (1 + k));
        }
        public static float MultiplyK(this float value, float k, bool negative = false)
        {
            if (negative)
            {
                return value * (1 - k);
            }

            return value * (1 + k);
        }
        /// <summary>
        /// 判断鼠标在不在某矩形
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Width"></param>
        /// <param name="Hegith"></param>
        /// <returns></returns>
        public static bool MouseInRectangle(int X, int Y, int Width, int Hegith, int OFFXLeft = 0, int OFFYTop = 0)
        {
            Vector2 mountedCenter = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            if (new Rectangle((int)mountedCenter.X, (int)mountedCenter.Y, 0, 0).Intersects(new Rectangle((int)(X + Main.screenPosition.X - OFFXLeft), (int)(Y + Main.screenPosition.Y - OFFYTop), Width, Hegith)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static CustomVertexInfo[] TailVertexFromProj(this Projectile projectile, Vector2 Offset = default, float Width = 30, float alpha = 1, bool VeloTri = false)
        {
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            int indexMax = -1;
            for (int n = 0; n < projectile.oldPos.Length; n++) if (projectile.oldPos[n] == Vector2.Zero) { indexMax = n; break; }
            //if(!Main.gamePaused)
            //Main.NewText(projectile.oldPos[0]);
            if (indexMax == -1) indexMax = 10;
            Offset += projectile.velocity;
            for (int i = 1; i < indexMax; ++i)
            {
                if (projectile.oldPos[i] == Vector2.Zero)
                {
                    break;
                }
                var normalDir = projectile.oldPos[i - 1] - projectile.oldPos[i];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                var factor = i / (float)indexMax;
                var w = 1 - factor;
                bars.Add(new CustomVertexInfo(projectile.oldPos[i] + Offset + normalDir * Width, Color.Purple * w, new Vector3((float)Math.Sqrt(factor), 1, alpha * .6f)));//w * 
                bars.Add(new CustomVertexInfo(projectile.oldPos[i] + Offset + normalDir * -Width, Color.Purple * w, new Vector3((float)Math.Sqrt(factor), 0, alpha * .6f)));//w * 
            }
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            if (bars.Count > 2)
            {
                if (VeloTri)
                {
                    triangleList.Add(bars[0]);
                    var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(projectile.velocity) * 30, Color.White,
                        new Vector3(0, 0.5f, alpha));
                    triangleList.Add(bars[1]);
                    triangleList.Add(vertex);
                }

                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }
            }
            return triangleList.ToArray();
        }
        public static void DrawShaderTail(this SpriteBatch spriteBatch, Projectile projectile, Texture2D heatMap, Texture2D aniTex, Texture2D baseTex, float Width = 30, Vector2 Offset = default, float alpha = 1, bool VeloTri = false, bool additive = false)
        {
            var triangleList = projectile.TailVertexFromProj(Offset, Width, alpha, VeloTri);
            if (triangleList.Length < 3) return;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            //var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            //var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            //IllusionBoundMod.DefaultEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            //IllusionBoundMod.DefaultEffect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);
            //Main.graphics.GraphicsDevice.Textures[0] = heatMap;
            //Main.graphics.GraphicsDevice.Textures[1] = baseTex;
            //Main.graphics.GraphicsDevice.Textures[2] = aniTex;
            //Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            //Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            //Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            //IllusionBoundMod.DefaultEffect.CurrentTechnique.Passes[0].Apply();
            //Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0, triangleList.Length / 3);
            //Main.graphics.GraphicsDevice.RasterizerState = originalState;
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            IllusionBoundMod.ShaderSwooshEX.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);

            IllusionBoundMod.ShaderSwooshEX.Parameters["uLighter"].SetValue(0);
            IllusionBoundMod.ShaderSwooshEX.Parameters["uTime"].SetValue(0);//-(float)Main.time * 0.06f
            IllusionBoundMod.ShaderSwooshEX.Parameters["checkAir"].SetValue(false);
            IllusionBoundMod.ShaderSwooshEX.Parameters["airFactor"].SetValue(1);
            IllusionBoundMod.ShaderSwooshEX.Parameters["gather"].SetValue(false);

            Main.graphics.GraphicsDevice.Textures[0] = baseTex;
            Main.graphics.GraphicsDevice.Textures[1] = aniTex;
            Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.Textures[3] = heatMap;

            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[3] = SamplerState.PointWrap;

            IllusionBoundMod.ShaderSwooshEX.CurrentTechnique.Passes[2].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0, triangleList.Length / 3);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, additive ? BlendState.Additive : BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        //不好用，用新的
        public static void DrawShaderTail(SpriteBatch spriteBatch, Projectile projectile, ShaderTailTexture shaderTail = ShaderTailTexture.Fire, ShaderTailStyle tailStyle = ShaderTailStyle.Dust, float Width = 30, ShaderTailMainStyle shaderTailMainStyle = ShaderTailMainStyle.MiddleLine, Vector2 Offset = default, float alpha = 1, bool additive = false)
        {
            //这里有几个我自己定义的枚举类型
            //ShaderTailTexture这个对应的是颜色
            //tailStyle这个是弹幕的动态亮度贴图（？
            //shaderTailMainStyle这个是弹幕的静态亮度贴图（？
            //它们分别对应uImage0 uImage2 uImage1
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();

            // 把所有的点都生成出来，按照顺序
            for (int i = 1; i < projectile.oldPos.Length; ++i)
            {
                if (projectile.oldPos[i] == Vector2.Zero)
                {
                    break;
                }
                //spriteBatch.Draw(TextureAssets.MagicPixel.Value, projectile.oldPos[i] - Main.screenPosition,
                //    new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);

                //int width = 30;
                var normalDir = projectile.oldPos[i - 1] - projectile.oldPos[i];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

                var factor = i / (float)projectile.oldPos.Length;
                var color = Color.Lerp(Color.White, Color.Red, factor);//后来发现底下那些if似乎没用（
                if (shaderTail == ShaderTailTexture.Frozen)
                {
                    color = Color.Lerp(Color.White, Color.Blue, factor);
                }
                if (shaderTail == ShaderTailTexture.Yellow)
                {
                    color = Color.Lerp(Color.White, Color.Yellow, factor);
                }
                if (shaderTail == ShaderTailTexture.White)
                {
                    color = Color.Lerp(Color.Black, Color.White, factor);
                }
                var w = 1 - factor;
                bars.Add(new CustomVertexInfo(projectile.oldPos[i] + Offset + normalDir * Width, color, new Vector3((float)Math.Sqrt(factor), 1, w * alpha)));//这里还是在截图画图吧
                bars.Add(new CustomVertexInfo(projectile.oldPos[i] + Offset + normalDir * -Width, color, new Vector3((float)Math.Sqrt(factor), 0, w * alpha)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();//这里是三角形的顶点

            if (bars.Count > 2)
            {

                // 按照顺序连接三角形
                triangleList.Add(bars[0]);//等腰直角三角形的底角1的顶点
                var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(projectile.velocity) * 30, Color.White,
                    new Vector3(0, 0.5f, alpha));
                triangleList.Add(bars[1]);//底角2的顶点
                triangleList.Add(vertex);//顶角顶点
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }//每次消耗两个点生成新三角形


                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                // 干掉注释掉就可以只显示三角形栅格
                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));//这个矩阵没仔细看，应该是负责把图像丢到三角形栅格中

                // 把变换和所需信息丢给shader
                IllusionBoundMod.DefaultEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                IllusionBoundMod.DefaultEffect.Parameters["uTime"].SetValue(-(float)IllusionBoundMod.ModTime * 0.03f);//会动的那个贴图的横向偏移量(就是这个才能让它动起来Main.time
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.HeatMap[(int)shaderTail];
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.BaseTexes[(int)shaderTailMainStyle];
                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[(int)tailStyle];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                //Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;
                //Main.graphics.GraphicsDevice.Textures[1] = TextureAssets.MagicPixel.Value;
                //Main.graphics.GraphicsDevice.Textures[2] = TextureAssets.MagicPixel.Value;
                /*if (isCyan)
				{
					IllusionBoundMod.CleverEffect.CurrentTechnique.Passes["Clever"].Apply();
				}*/
                IllusionBoundMod.DefaultEffect.CurrentTechnique.Passes[0].Apply();


                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);//连接三角形顶点

                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, additive ? BlendState.Additive : BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        /// <summary>
        /// 绘制鼠标在某矩形下的悬浮字
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="color1">内色</param>
        /// <param name="color2">边框色</param>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <param name="Width">矩形宽</param>
        /// <param name="Hegiht">矩形高</param>
        public static void DrawMouseTextOnRectangle(string text, Color color1, Color color2, int X, int Y, int Width, int Hegiht)
        {
            Vector2 mountedCenter = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            if (new Rectangle((int)mountedCenter.X, (int)mountedCenter.Y, 0, 0).Intersects(new Rectangle((int)(X + Main.screenPosition.X), (int)(Y + Main.screenPosition.Y), Width, Hegiht)))
            {
                string name = text;
                Vector2 worldPos = new Vector2(mountedCenter.X + 15, mountedCenter.Y + 15);
                Vector2 size = FontAssets.MouseText.Value.MeasureString(name);
                Vector2 texPos = worldPos + new Vector2(-size.X * 0.5f, name.Length) - Main.screenPosition;
                Terraria.Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, name, texPos.X, texPos.Y, color1, color2, Vector2.Zero);
            }

        }
        public static Vector2 MoveTowards(this Vector2 currentPosition, Vector2 targetPosition, float maxAmountAllowedToMove)
        {
            Vector2 v = targetPosition - currentPosition;
            if (v.Length() < maxAmountAllowedToMove)
            {
                return targetPosition;
            }
            return currentPosition + v.SafeNormalize(Vector2.Zero) * maxAmountAllowedToMove;
        }
        public static void DrawStorm(Color c1, Projectile projectile, float MaxValue)
        {
            float num252 = 15f;
            float num253 = 15f;
            float num254 = projectile.ai[0];
            float scale8 = MathHelper.Clamp(num254 / 30f, 0f, 1f);
            if (num254 > MaxValue - 60f)
            {
                scale8 = MathHelper.Lerp(1f, 0f, (num254 - (MaxValue - 60f)) / 60f);
            }
            Point point6 = projectile.Center.ToTileCoordinates();
            Collision.ExpandVertically(point6.X, point6.Y, out int num255, out int num256, (int)num252, (int)num253);
            int num43 = num255;
            num255 = num43 + 1;
            num43 = num256;
            num256 = num43 - 1;
            float num257 = 0.2f;
            Vector2 vector50 = new Vector2(point6.X, num255) * 16f + new Vector2(8f);
            Vector2 vector51 = new Vector2(point6.X, num256) * 16f + new Vector2(8f);
            Vector2.Lerp(vector50, vector51, 0.5f);
            Vector2 vector52 = new Vector2(0f, vector51.Y - vector50.Y);
            vector52.X = vector52.Y * num257;
            _ = new Vector2(vector50.X - vector52.X / 2f, vector50.Y);
            Texture2D texture2D29 = TextureAssets.Projectile[projectile.type].Value;
            Rectangle rectangle14 = texture2D29.Frame(1, 1, 0, 0);
            Vector2 origin6 = rectangle14.Size() / 2f;
            float num258 = -0.06283186f * num254;
            Vector2 unitY3 = Vector2.UnitY;
            double radians7 = (double)(num254 * 0.1f);
            Vector2 center = default;
            Vector2 vector53 = unitY3.RotatedBy(radians7, center);
            float num259 = 0f;
            float num260 = 5.1f;
            float xValue = projectile.velocity.X > 0 ? -16 : 0;
            for (float num261 = (int)vector51.Y; num261 > (int)vector50.Y; num261 -= num260)
            {
                num259 += num260;
                float num262 = num259 / vector52.Y;
                float num263 = num259 * 6.28318548f / -20f;
                float num264 = num262 - 0.15f;
                Vector2 spinningpoint5 = vector53;
                double radians8 = (double)num263;
                center = default;
                Vector2 vector54 = spinningpoint5.RotatedBy(radians8, center);
                Vector2 vector55 = new Vector2(0f, num262 + 1f);
                vector55.X = vector55.Y * num257;
                Color color49 = Color.Lerp(Color.Transparent, c1, num262 * 2f);
                if (num262 > 0.5f)
                {
                    color49 = Color.Lerp(Color.Transparent, c1, 2f - num262 * 2f);
                }
                color49.A = (byte)(color49.A * 0.5f);
                color49 *= scale8;
                vector54 *= vector55 * 100f;
                vector54.Y = 0f;
                vector54.X = 0f;
                vector54 += new Vector2(vector51.X, num261) - Main.screenPosition;
                Main.spriteBatch.Draw(texture2D29, vector54 + new Vector2(projectile.Center.X % 16 + xValue, projectile.Center.Y % 16), new Microsoft.Xna.Framework.Rectangle?(rectangle14), color49, num258 + num263, origin6, 1f + num264, SpriteEffects.None, 0f);
            }
        }
        public static void DrawWind(Color c1, Color c2, Projectile projectile, float MaxValue)
        {
            float num266 = projectile.ai[0];
            float scale9 = MathHelper.Clamp(num266 / 30f, 0f, 1f);
            if (num266 > MaxValue - 60f)
            {
                scale9 = MathHelper.Lerp(1f, 0f, (num266 - (MaxValue - 60f)) / 60f);
            }
            float num267 = 0.2f;
            Vector2 top = projectile.Top;
            Vector2 bottom = projectile.Bottom;
            Vector2.Lerp(top, bottom, 0.5f);
            Vector2 vector56 = new Vector2(0f, bottom.Y - top.Y);
            vector56.X = vector56.Y * num267;
            _ = new Vector2(top.X - vector56.X / 2f, top.Y);
            Texture2D texture2D30 = TextureAssets.Projectile[projectile.type].Value;
            Rectangle rectangle15 = texture2D30.Frame(1, 1, 0, 0);
            Vector2 origin7 = rectangle15.Size() / 2f;
            float num268 = -0.157079637f * num266 * ((projectile.velocity.X > 0f) ? -1 : 1);
            SpriteEffects effects2 = (projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            bool flag25 = projectile.velocity.X > 0f;
            Vector2 unitY4 = Vector2.UnitY;
            double radians9 = (double)(num266 * 0.14f);
            Vector2 center = default;
            Vector2 vector57 = unitY4.RotatedBy(radians9, center);
            float num269 = 0f;
            float num270 = 5.01f + num266 / 150f * -0.9f;
            float xValue = projectile.velocity.X > 0f ? -1 : 1;
            if (num270 < 4.11f)
            {
                num270 = 4.11f;
            }
            float num271 = num266 % 60f;
            if (num271 < 30f)
            {
                //c2 *= Terraria.Utils.InverseLerp(22f, 30f, num271, true);
            }
            else
            {
                //c2 *= Terraria.Utils.InverseLerp(38f, 30f, num271, true);
            }
            bool flag26 = c2 != Color.Transparent;
            for (float num272 = (int)bottom.Y; num272 > (int)top.Y; num272 -= num270)
            {
                num269 += num270;
                float num273 = num269 / vector56.Y;
                float num274 = num269 * 6.28318548f / -20f;
                if (flag25)
                {
                    num274 *= -1f;
                }
                float num275 = num273 - 0.35f;
                Vector2 spinningpoint6 = vector57;
                double radians10 = (double)num274;
                center = default;
                Vector2 vector58 = spinningpoint6.RotatedBy(radians10, center);
                Vector2 vector59 = new Vector2(0f, num273 + 1f);
                vector59.X = vector59.Y * num267;
                Color color51 = Color.Lerp(Color.Transparent, c1, num273 * 2f);
                if (num273 > 0.5f)
                {
                    color51 = Color.Lerp(Color.Transparent, c1, 2f - num273 * 2f);
                }
                color51.A = (byte)(color51.A * 0.5f);
                color51 *= scale9;
                vector58 *= vector59 * 100f;
                vector58.Y = 0f;
                vector58.X = 0f;
                vector58 += new Vector2(bottom.X, num272) - Main.screenPosition;
                if (flag26)
                {
                    Color color52 = Color.Lerp(Color.Transparent, c2, num273 * 2f);
                    if (num273 > 0.5f)
                    {
                        color52 = Color.Lerp(Color.Transparent, c2, 2f - num273 * 2f);
                    }
                    color52.A = (byte)(color52.A * 0.5f);
                    color52 *= scale9;
                    Main.spriteBatch.Draw(texture2D30, vector58 + new Vector2(16 * xValue, 0), new Microsoft.Xna.Framework.Rectangle?(rectangle15), color52, num268 + num274, origin7, (1f + num275) * 0.8f, effects2, 0f);
                }
                Main.spriteBatch.Draw(texture2D30, vector58 + new Vector2(16 * xValue, 0), new Microsoft.Xna.Framework.Rectangle?(rectangle15), color51, num268 + num274, origin7, 1f + num275, effects2, 0f);
            }
        }
        public static Rectangle CenteredRectangle(Vector2 center, Vector2 size)
        {
            return new Rectangle((int)(center.X - size.X / 2f), (int)(center.Y - size.Y / 2f), (int)size.X, (int)size.Y);
        }
        public static float GetLerpValue(float from, float to, float t, bool clamped = false)
        {
            if (clamped)
            {
                if (from < to)
                {
                    if (t < from)
                    {
                        return 0f;
                    }
                    if (t > to)
                    {
                        return 1f;
                    }
                }
                else
                {
                    if (t < to)
                    {
                        return 1f;
                    }
                    if (t > from)
                    {
                        return 0f;
                    }
                }
            }
            return (t - from) / (to - from);
        }
        //public static void DrawSwanSongLight(Texture2D texture2D, Projectile projectile, Player player, SpriteBatch spriteBatch, bool Dashing = false)
        //{
        //    IllusionBoundPlayer illusionBoundPlayer = player.GetModPlayer<IllusionBoundPlayer>();
        //    Vector2 vector = player.velocity;
        //    float num2 = vector.Length();
        //    if (num2 == 0f)
        //    {
        //        vector = Vector2.UnitY;
        //    }
        //    else
        //    {
        //        vector *= 5f / num2;
        //    }
        //    float num4 = 60f;
        //    float num5 = GetLerpValue(num4, num4 * 0.8333333f, projectile.localAI[0], true);
        //    num5 *= GetLerpValue(0f, 15f, projectile.localAI[0], true);
        //    Color value = Main.hslToRgb(Main.rgbToHsl(new Color(240, 139, 78)).X, 1f, 0.5f) * num5;
        //    var spinningpoint = new Vector2(0f, -2f);
        //    float num6 = GetLerpValue(num4, num4 * 0.6666667f, projectile.localAI[0], true);
        //    num6 *= GetLerpValue(0f, 20f, projectile.localAI[0], true);
        //    var num = -0.3f * (1f - num6);
        //    num += -1f * GetLerpValue(15f, 0f, projectile.localAI[0], true);
        //    num *= 1;
        //    Vector2 value2 = projectile.Center + vector;
        //    float num7 = (float)Main.time / 60f;
        //    Vector2 value6 = value2 - player.velocity;
        //    Color color2 = value;
        //    color2.A = 0;
        //    SpriteEffects spriteEffects;
        //    if (player.gravDir == 1f)
        //    {
        //        if (player.direction == 1)
        //        {
        //            spriteEffects = SpriteEffects.None;
        //        }
        //        else
        //        {
        //            spriteEffects = SpriteEffects.FlipHorizontally;
        //        }
        //    }
        //    else
        //    {
        //        if (player.direction == 1)
        //        {
        //            spriteEffects = SpriteEffects.FlipVertically;
        //        }
        //        else
        //        {
        //            spriteEffects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
        //        }
        //    }
        //    float c = 2.09439516f / 2;
        //    for (int n = 0; n < 3; n++)
        //    {
        //        if (Dashing)
        //        {
        //            spriteBatch.Draw(texture2D, projectile.Center + new Vector2(32, 0).RotatedBy(projectile.ai[0]) - Main.screenPosition + spinningpoint.RotatedBy((double)(6.28318548f * num7 + c * (n - 1))), null, color2, projectile.ai[0] + MathHelper.PiOver2, new Vector2(texture2D.Width / 2, texture2D.Height / 2), 1.2f + 0.2f * (n - 1) + num, spriteEffects, 0);
        //        }
        //        else
        //        {
        //            spriteBatch.Draw(texture2D, projectile.Center + new Vector2(32 * player.direction, 8) - Main.screenPosition + spinningpoint.RotatedBy((double)(6.28318548f * num7 + c * (n - 1))), null, color2, -MathHelper.PiOver2 + MathHelper.PiOver2 * player.direction + MathHelper.PiOver2, new Vector2(texture2D.Width / 2, texture2D.Height / 2), (1.2f + 0.2f * (n - 1) + num) * (illusionBoundPlayer.swanSongKatanaTimer.ValueRange(60, 115) - 60) / 55, spriteEffects, 0);
        //        }
        //    }
        //}
        public static Vector2 GetPlayerArmPosition(Projectile proj)
        {
            Player player = Main.player[proj.owner];
            Vector2 vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector.X = player.bodyFrame.Width - vector.X;
            }
            if (player.gravDir != 1f)
            {
                vector.Y = player.bodyFrame.Height - vector.Y;
            }
            vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            return player.RotatedRelativePoint(player.MountedCenter - new Vector2(20f, 42f) / 2f + vector + Vector2.UnitY * player.gfxOffY, false);
        }
        private static ConcurrentDictionary<string, ITagHandler> _handlers = new ConcurrentDictionary<string, ITagHandler>();
        private static ITagHandler GetHandler(string tagName)
        {
            string key = tagName.ToLower();
            if (_handlers.ContainsKey(key))
            {
                return _handlers[key];
            }
            return null;
        }
        public static List<TextSnippet> ParseMessage(string text, Color baseColor)
        {
            MatchCollection matchCollection = ChatManager.Regexes.Format.Matches(text);
            List<TextSnippet> list = new List<TextSnippet>();
            int num = 0;
            foreach (object obj in matchCollection)
            {
                Match match = (Match)obj;
                if (match.Index > num)
                {
                    list.Add(new TextSnippet(text.Substring(num, match.Index - num), baseColor, 1f));
                }
                num = match.Index + match.Length;
                string value = match.Groups["tag"].Value;
                string value2 = match.Groups["text"].Value;
                string value3 = match.Groups["options"].Value;
                ITagHandler handler = GetHandler(value);
                if (handler != null)
                {
                    list.Add(handler.Parse(value2, baseColor, value3));
                    list[list.Count - 1].TextOriginal = match.ToString();
                }
                else
                {
                    list.Add(new TextSnippet(value2, baseColor, 1f));
                }
            }
            if (text.Length > num)
            {
                list.Add(new TextSnippet(text[num..], baseColor, 1f));
            }
            return list;
        }
        public static float ValueRange(this float var, float Min = 0f, float Max = 1f)
        {
            if (var > Max)
            {
                var = Max;
            }
            if (var < Min)
            {
                var = Min;
            }
            return var;
        }
        public static int ValueRange(this int var, int Min = 0, int Max = 1)
        {
            if (var > Max)
            {
                var = Max;
            }
            if (var < Min)
            {
                var = Min;
            }
            return var;
        }
        //public static float ValueRange(this ref float var, float Min, float Max)
        //{
        //	if (var > Max)
        //	{
        //		var = Max;
        //	}
        //	if (var < Min)
        //	{
        //		var = Min;
        //	}
        //	return var;
        //}
        //public static int ValueRange(this ref int var, int Min, int Max)
        //{
        //	if (var > Max)
        //	{
        //		var = Max;
        //	}
        //	if (var < Min)
        //	{
        //		var = Min;
        //	}
        //	return var;
        //}
        /*public static void EntitySpriteDraw(Texture2D texture, Vector2 position, Microsoft.Xna.Framework.Rectangle? sourceRectangle, Microsoft.Xna.Framework.Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, int worthless)
		{
			EntitySpriteDraw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale), effects, worthless);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x003601C8 File Offset: 0x0035E3C8
		public static void EntitySpriteDraw(Texture2D texture, Vector2 position, Microsoft.Xna.Framework.Rectangle? sourceRectangle, Microsoft.Xna.Framework.Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, int worthless)
		{
			if (Main.CurrentDrawnEntityShader > 0)
			{
				DrawData value = new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects, worthless);
				GameShaders.Armor.Apply(Main.CurrentDrawnEntityShader, Main.CurrentDrawnEntity, new DrawData?(value));
				value.Draw(Main.spriteBatch);
				return;
			}
			Main.spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, (float)worthless);
		}*/
        //public static void PTWST(Item item, Player player)
        //{
        //    //没用的函数（？
        //    bool flag19 = player.position.X / 16f - (float)Player.tileRangeX - (float)item.tileBoost <= (float)Player.tileTargetX && (player.position.X + (float)player.width) / 16f + (float)Player.tileRangeX + (float)item.tileBoost - 1f >= (float)Player.tileTargetX && player.position.Y / 16f - (float)Player.tileRangeY - (float)item.tileBoost <= (float)Player.tileTargetY && (player.position.Y + (float)player.height) / 16f + (float)Player.tileRangeY + (float)item.tileBoost - 2f >= (float)Player.tileTargetY;
        //    if (player.noBuilding)
        //    {
        //        flag19 = false;
        //    }
        //    if (flag19)
        //    {
        //        int num242 = 0;
        //        bool flag20 = true;
        //        if (!Main.GamepadDisableCursorItemIcon)
        //        {
        //            player.showItemIcon = true;
        //            Main.ItemIconCacheUpdate(item.type);
        //        }
        //        if (player.toolTime == 0 && player.itemAnimation > 0 && player.controlUseItem && (!Main.tile[Player.tileTargetX, Player.tileTargetY].active() || (!Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && Main.tile[Player.tileTargetX, Player.tileTargetY].type != 314 && Main.tile[Player.tileTargetX, Player.tileTargetY].type != 424 && Main.tile[Player.tileTargetX, Player.tileTargetY].type != 442 && Main.tile[Player.tileTargetX, Player.tileTargetY].type != 351)))
        //        {
        //            player.poundRelease = false;
        //        }
        //        if (Main.tile[Player.tileTargetX, Player.tileTargetY].active())
        //        {
        //            if ((item.pick > 0 && !Main.tileAxe[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && !Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]) || (item.axe > 0 && Main.tileAxe[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]) || (item.hammer > 0 && Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
        //            {
        //                flag20 = false;
        //            }
        //            if (player.toolTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
        //            {
        //                int tileId = player.hitTile.HitObject(Player.tileTargetX, Player.tileTargetY, 1);
        //                if (Main.tileNoFail[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
        //                {
        //                    num242 = 100;
        //                }
        //                if (Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
        //                {
        //                    flag20 = false;
        //                }
        //                else if (Main.tileAxe[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
        //                {
        //                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 80)
        //                    {
        //                        num242 += item.axe * 3;
        //                    }
        //                    else
        //                    {
        //                        TileLoader.MineDamage(item.axe, ref num242);
        //                    }
        //                    if (item.axe > 0)
        //                    {
        //                        AchievementsHelper.CurrentlyMining = true;
        //                        if (!WorldGen.CanKillTile(Player.tileTargetX, Player.tileTargetY))
        //                        {
        //                            num242 = 0;
        //                        }
        //                        if (player.hitTile.AddDamage(tileId, num242, true) >= 100)
        //                        {
        //                            player.hitTile.Clear(tileId);
        //                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
        //                            if (Main.netMode == 1)
        //                            {
        //                                NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0, 0, 0);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
        //                            if (Main.netMode == 1)
        //                            {
        //                                NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0, 0, 0);
        //                            }
        //                        }
        //                        if (num242 != 0)
        //                        {
        //                            player.hitTile.Prune();
        //                        }
        //                        player.itemTime = (int)((float)item.useTime / PlayerHooks.TotalUseTimeMultiplier(player, item));
        //                        AchievementsHelper.CurrentlyMining = false;
        //                    }
        //                }
        //                else if (item.pick > 0)
        //                {
        //                    player.PickTile(Player.tileTargetX, Player.tileTargetY, item.pick);
        //                    player.itemTime = (int)((float)item.useTime * player.pickSpeed / PlayerHooks.TotalUseTimeMultiplier(player, item));
        //                }
        //                if (item.pick > 0)
        //                {
        //                    player.itemTime = (int)((float)item.useTime * player.pickSpeed / PlayerHooks.TotalUseTimeMultiplier(player, item));
        //                }
        //                player.poundRelease = false;
        //            }
        //        }
        //        if (player.releaseUseItem)
        //        {
        //            player.poundRelease = true;
        //        }
        //        int num264 = Player.tileTargetX;
        //        int num265 = Player.tileTargetY;
        //        bool flag25 = true;
        //        if (Main.tile[num264, num265].wall > 0)
        //        {
        //            if (!Main.wallHouse[(int)Main.tile[num264, num265].wall])
        //            {
        //                int num2;
        //                for (int num266 = num264 - 1; num266 < num264 + 2; num266 = num2 + 1)
        //                {
        //                    for (int num267 = num265 - 1; num267 < num265 + 2; num267 = num2 + 1)
        //                    {
        //                        if (Main.tile[num266, num267].wall != Main.tile[num264, num265].wall)
        //                        {
        //                            flag25 = false;
        //                            break;
        //                        }
        //                        num2 = num267;
        //                    }
        //                    num2 = num266;
        //                }
        //            }
        //            else
        //            {
        //                flag25 = false;
        //            }
        //        }
        //        if (flag25 && !Main.tile[num264, num265].active())
        //        {
        //            int num268 = -1;
        //            if ((double)(((float)Main.mouseX + Main.screenPosition.X) / 16f) < Math.Round((double)(((float)Main.mouseX + Main.screenPosition.X) / 16f)))
        //            {
        //                num268 = 0;
        //            }
        //            int num269 = -1;
        //            if ((double)(((float)Main.mouseY + Main.screenPosition.Y) / 16f) < Math.Round((double)(((float)Main.mouseY + Main.screenPosition.Y) / 16f)))
        //            {
        //                num269 = 0;
        //            }
        //            int num2;
        //            for (int num270 = Player.tileTargetX + num268; num270 <= Player.tileTargetX + num268 + 1; num270 = num2 + 1)
        //            {
        //                for (int num271 = Player.tileTargetY + num269; num271 <= Player.tileTargetY + num269 + 1; num271 = num2 + 1)
        //                {
        //                    if (flag25)
        //                    {
        //                        num264 = num270;
        //                        num265 = num271;
        //                        if (Main.tile[num264, num265].wall > 0)
        //                        {
        //                            if (!Main.wallHouse[(int)Main.tile[num264, num265].wall])
        //                            {
        //                                for (int num272 = num264 - 1; num272 < num264 + 2; num272 = num2 + 1)
        //                                {
        //                                    for (int num273 = num265 - 1; num273 < num265 + 2; num273 = num2 + 1)
        //                                    {
        //                                        if (Main.tile[num272, num273].wall != Main.tile[num264, num265].wall)
        //                                        {
        //                                            flag25 = false;
        //                                            break;
        //                                        }
        //                                        num2 = num273;
        //                                    }
        //                                    num2 = num272;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                flag25 = false;
        //                            }
        //                        }
        //                    }
        //                    num2 = num271;
        //                }
        //                num2 = num270;
        //            }
        //        }
        //        if (flag20 && Main.tile[num264, num265].wall > 0 && (!Main.tile[num264, num265].active() || num264 != Player.tileTargetX || num265 != Player.tileTargetY || (!Main.tileHammer[(int)Main.tile[num264, num265].type] && !player.poundRelease)) && player.toolTime == 0 && player.itemAnimation > 0 && player.controlUseItem && item.hammer > 0)
        //        {
        //            bool flag26 = true;
        //            if (!Main.wallHouse[(int)Main.tile[num264, num265].wall])
        //            {
        //                flag26 = false;
        //                int num2;
        //                for (int num274 = num264 - 1; num274 < num264 + 2; num274 = num2 + 1)
        //                {
        //                    for (int num275 = num265 - 1; num275 < num265 + 2; num275 = num2 + 1)
        //                    {
        //                        if (Main.tile[num274, num275].wall == 0 || Main.wallHouse[(int)Main.tile[num274, num275].wall])
        //                        {
        //                            flag26 = true;
        //                            break;
        //                        }
        //                        num2 = num275;
        //                    }
        //                    num2 = num274;
        //                }
        //            }
        //            if (flag26)
        //            {
        //                int tileId2 = player.hitTile.HitObject(num264, num265, 2);
        //                num242 = (int)((float)item.hammer * 1.5f);
        //                if (player.hitTile.AddDamage(tileId2, num242, true) >= 100)
        //                {
        //                    player.hitTile.Clear(tileId2);
        //                    WorldGen.KillWall(num264, num265, false);
        //                    if (Main.netMode == 1)
        //                    {
        //                        NetMessage.SendData(17, -1, -1, null, 2, (float)num264, (float)num265, 0f, 0, 0, 0);
        //                    }
        //                }
        //                else
        //                {
        //                    WorldGen.KillWall(num264, num265, true);
        //                    if (Main.netMode == 1)
        //                    {
        //                        NetMessage.SendData(17, -1, -1, null, 2, (float)num264, (float)num265, 1f, 0, 0, 0);
        //                    }
        //                }
        //                if (num242 != 0)
        //                {
        //                    player.hitTile.Prune();
        //                }
        //                player.itemTime = (int)((float)item.useTime / PlayerHooks.TotalUseTimeMultiplier(player, item)) / 2;
        //            }
        //        }
        //    }
        //}
        //public static void PickTileWithSpecialTool(this IllusionBoundPlayer player, int x, int y, int pickPower, bool UsingMatterManipulator = true)
        //{
        //    int num = 0;
        //    int tileId = player.hitTileWithSpecialTool.HitObject(x, y, 1);
        //    Tile tile = Main.tile[x, y];
        //    if (Main.tileNoFail[(int)tile.type])
        //    {
        //        num = 100;
        //    }
        //    int pickPowerQuarter = pickPower / (UsingMatterManipulator ? 16 : 8);
        //    //num += pickPower / 17;
        //    if (Main.tileDungeon[(int)tile.type] || tile.type == 25 || tile.type == 58 || tile.type == 117 || tile.type == 203)
        //    {
        //        num += pickPowerQuarter / 2;
        //    }
        //    else if (tile.type == 48 || tile.type == 232)
        //    {
        //        num += pickPowerQuarter / 4;
        //    }
        //    else if (tile.type == 226)
        //    {
        //        num += pickPowerQuarter / 4;
        //    }
        //    else if (tile.type == 107 || tile.type == 221)
        //    {
        //        num += pickPowerQuarter / 2;
        //    }
        //    else if (tile.type == 108 || tile.type == 222)
        //    {
        //        num += pickPowerQuarter / 3;
        //    }
        //    else if (tile.type == 111 || tile.type == 223)
        //    {
        //        num += pickPowerQuarter / 4;
        //    }
        //    else if (tile.type == 211)
        //    {
        //        num += pickPowerQuarter / 5;
        //    }
        //    else
        //    {
        //        TileLoader.MineDamage(pickPowerQuarter, ref num);
        //    }
        //    if (tile.type == 165 || Main.tileRope[(int)tile.type] || tile.type == 199 || Main.tileMoss[(int)tile.type])
        //    {
        //        num = 100;
        //    }
        //    if (tile.type == 128 || tile.type == 269)
        //    {
        //        if (tile.frameX == 18 || tile.frameX == 54)
        //        {
        //            x--;
        //            tile = Main.tile[x, y];
        //            player.hitTileWithSpecialTool.UpdatePosition(tileId, x, y);
        //        }
        //        if (tile.frameX >= 100)
        //        {
        //            num = 0;
        //            Main.blockMouse = true;
        //        }
        //    }
        //    if (tile.type == 334)
        //    {
        //        if (tile.frameY == 0)
        //        {
        //            y++;
        //            tile = Main.tile[x, y];
        //            player.hitTileWithSpecialTool.UpdatePosition(tileId, x, y);
        //        }
        //        if (tile.frameY == 36)
        //        {
        //            y--;
        //            tile = Main.tile[x, y];
        //            player.hitTileWithSpecialTool.UpdatePosition(tileId, x, y);
        //        }
        //        int i = (int)tile.frameX;
        //        bool flag = i >= 5000;
        //        bool flag2 = false;
        //        if (!flag)
        //        {
        //            int num2 = i / 18;
        //            num2 %= 3;
        //            x -= num2;
        //            tile = Main.tile[x, y];
        //            if (tile.frameX >= 5000)
        //            {
        //                flag = true;
        //            }
        //        }
        //        if (flag)
        //        {
        //            i = (int)tile.frameX;
        //            int num3 = 0;
        //            while (i >= 5000)
        //            {
        //                i -= 5000;
        //                num3++;
        //            }
        //            if (num3 != 0)
        //            {
        //                flag2 = true;
        //            }
        //        }
        //        if (flag2)
        //        {
        //            num = 0;
        //            Main.blockMouse = true;
        //        }
        //    }
        //    if (!WorldGen.CanKillTile(x, y))
        //    {
        //        num = 0;
        //    }
        //    if (player.hitTileWithSpecialTool.AddDamage(tileId, num, true) >= 100)
        //    {
        //        AchievementsHelper.CurrentlyMining = true;
        //        player.hitTileWithSpecialTool.Clear(tileId);
        //        if (Main.netMode == 1 && Main.tileContainer[(int)Main.tile[x, y].type])
        //        {
        //            WorldGen.KillTile(x, y, true, false, false);
        //            NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 1f, 0, 0, 0);
        //            if (Main.tile[x, y].type == 21 || (Main.tile[x, y].type >= 470 && TileID.Sets.BasicChest[(int)Main.tile[x, y].type]))
        //            {
        //                NetMessage.SendData(34, -1, -1, null, 1, (float)x, (float)y, 0f, 0, 0, 0);
        //            }
        //            if (Main.tile[x, y].type == 467)
        //            {
        //                NetMessage.SendData(34, -1, -1, null, 5, (float)x, (float)y, 0f, 0, 0, 0);
        //            }
        //            if (TileLoader.IsDresser((int)Main.tile[x, y].type))
        //            {
        //                NetMessage.SendData(34, -1, -1, null, 3, (float)x, (float)y, 0f, 0, 0, 0);
        //            }
        //            if (Main.tile[x, y].type >= 470 && TileID.Sets.BasicChest[(int)Main.tile[x, y].type])
        //            {
        //                NetMessage.SendData(34, -1, -1, null, 101, (float)x, (float)y, 0f, 0, (int)Main.tile[x, y].type, 0);
        //            }
        //            if (Main.tile[x, y].type >= 470 && TileLoader.IsDresser((int)Main.tile[x, y].type))
        //            {
        //                NetMessage.SendData(34, -1, -1, null, 103, (float)x, (float)y, 0f, 0, (int)Main.tile[x, y].type, 0);
        //            }
        //        }
        //        else
        //        {
        //            int num4 = y;
        //            bool flag3 = Main.tile[x, num4].active();
        //            WorldGen.KillTile(x, num4, false, false, false);
        //            if (flag3 && !Main.tile[x, num4].active())
        //            {
        //                AchievementsHelper.HandleMining();
        //            }
        //            if (Main.netMode == 1)
        //            {
        //                NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)num4, 0f, 0, 0, 0);
        //            }
        //        }
        //        AchievementsHelper.CurrentlyMining = false;
        //    }
        //    else
        //    {
        //        WorldGen.KillTile(x, y, true, false, false);
        //        if (Main.netMode == 1)
        //        {
        //            NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 1f, 0, 0, 0);
        //        }
        //    }
        //    //if (num != 0)
        //    //{
        //    //	player.hitTileWithSpecialTool.Prune();
        //    //}
        //}
    }
}
