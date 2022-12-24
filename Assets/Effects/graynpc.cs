/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using IllusionBoundMod.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace IllusionBoundMod.Effects
{
	// This ModNPC serves as an example of a complete AI example.
	public class graynpc : GlobalNPC
	{
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            IllusionBoundMod.grayEffect.CurrentTechnique.Passes["Test"].Apply();
            return true;
        }
    }
}*/