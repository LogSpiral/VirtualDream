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
//    public class TestScreenShaderData : ScreenShaderData
//    {
//        public TestScreenShaderData(string passName) : base(passName)
//        {
//        }
//        public TestScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
//        {
//        }

//        public override void Apply()
//        {
//            Shader.Parameters["mousePos"].SetValue(new Vector2((Main.MouseWorld - Main.screenPosition).X / Main.screenWidth, (Main.MouseWorld - Main.screenPosition).Y / Main.screenHeight));
//            base.Apply();
//        }
//    }
//}