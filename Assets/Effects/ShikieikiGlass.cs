//using System;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using IllusionBoundMod.Utils;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria.Graphics.Shaders;

//namespace IllusionBoundMod.Effects
//{
//    // This ModNPC serves as an example of a complete AI example.
//    public class ShikieikiScreenShaderData : ScreenShaderData
//    {
//        public ShikieikiScreenShaderData(string passName) : base(passName)
//        {
//        }
//        public ShikieikiScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
//        {
//        }
//        public override void Apply()
//        {
//            Shader.Parameters["lightConst"].SetValue(IllusionBoundMod.lightConst);
//            base.Apply();
//        }
//    }
//}