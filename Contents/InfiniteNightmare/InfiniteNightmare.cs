using System;
using System.Collections.Generic;

using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader.IO;

using VirtualDream.Contents.TouhouProject.NPCs.Fairy;

using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.InfiniteNightmare
{
    //public abstract class MyProjectile : InfiniteNightmareProjectile 
    //{
    //	public virtual string DefaultTexture => "";
    //	public sealed override string Texture => Mod.TextureExists(base.Texture) ? base.Texture : DefaultTexture;
    //   }
    public abstract class InfiniteNightmareItem : ModItem
    {
        public Item item => Item;
        public Mod mod => Mod;
    }
    public abstract class InfiniteNightmareProjectile : ModProjectile
    {
        public virtual bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return PreDraw(Main.spriteBatch, lightColor);
        }
        public Projectile projectile => Projectile;
        public Mod mod => Mod;
    }
    public class InfiniteNightmare : InfiniteNightmareItem
    {
        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 66;
            item.maxStack = 1;
            item.rare = ItemRarityID.Purple;
            item.value = 0;
            item.useTime = 60;
            item.useAnimation = 60;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Shoot;
            item.consumable = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddTile(TileID.DemonAltar);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.ShaderItemEffectInventory(spriteBatch, position, origin, IllusionBoundMod.GetTexture("Images/IMBellTex"), Main.hslToRgb(0.5f, 0.5f, 0.5f), scale);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.ShaderItemEffectInWorld(spriteBatch, IllusionBoundMod.GetTexture("Images/IMBellTex"), Main.hslToRgb(0.5f, 0.5f, 0.5f), rotation);
        }
        //public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //	CustomVertexInfo[] triangleArry = new CustomVertexInfo[6];
        //	RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
        //	//float offsetX = 32f / 1920 * (ItemID.Sets.ItemIconPulse[item.type] ? Main.essScale : 1f);
        //	//float offsetY = 32f / 1120 * (ItemID.Sets.ItemIconPulse[item.type] ? Main.essScale : 1f);
        //	float offset = 16f * (ItemID.Sets.ItemIconPulse[item.type] ? Main.essScale : 1f);
        //	Color c = Main.hslToRgb(0.5f, 0.5f, 0.5f);
        //	//triangleArry[0] = new CustomVertexInfo(Center - new Vector2(offsetX, offsetY), c, new Vector3(0, 0, 0));
        //	//triangleArry[1] = new CustomVertexInfo(Center + new Vector2(offsetX, -offsetY), c, new Vector3(1, 0, 0));
        //	//triangleArry[2] = new CustomVertexInfo(Center + new Vector2(offsetX, offsetY), c, new Vector3(1, 1, 0));
        //	//triangleArry[3] = triangleArry[2];
        //	//triangleArry[4] = new CustomVertexInfo(Center - new Vector2(offsetX, -offsetY), c, new Vector3(0, 1, 0));
        //	//triangleArry[5] = triangleArry[0];
        //	triangleArry[0] = new CustomVertexInfo(position + Main.screenPosition - new Vector2(offset, offset) + new Vector2(12, 12), c, new Vector3(0, 0, 0));
        //	triangleArry[1] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offset, -offset) + new Vector2(12, 12), c, new Vector3(1, 0, 0));
        //	triangleArry[2] = new CustomVertexInfo(position + Main.screenPosition + new Vector2(offset, offset) + new Vector2(12, 12), c, new Vector3(1, 1, 0));
        //	triangleArry[3] = triangleArry[2];
        //	triangleArry[4] = new CustomVertexInfo(position + Main.screenPosition - new Vector2(offset, -offset) + new Vector2(12, 12), c, new Vector3(0, 1, 0));
        //	triangleArry[5] = triangleArry[0];
        //	//triangleArry[0] = new CustomVertexInfo(position, c, new Vector3(0, 0, 0));
        //	//triangleArry[1] = new CustomVertexInfo(position + new Vector2(offset, 0), c, new Vector3(1, 0, 0));
        //	//triangleArry[2] = new CustomVertexInfo(position + new Vector2(offset, offset), c, new Vector3(1, 1, 0));
        //	//triangleArry[3] = triangleArry[2];
        //	//triangleArry[4] = new CustomVertexInfo(position - new Vector2(offset, 0), c, new Vector3(0, 1, 0));
        //	//triangleArry[5] = triangleArry[0];
        //	Effect effect = mod.GetEffect("Effects/InfiniteNightmareBell");
        //	var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
        //	var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
        //	effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
        //	effect.Parameters["uTime"].SetValue((float)IllusionBoundMod.ModTime / 60 % 1);
        //	Main.graphics.GraphicsDevice.Textures[0] = Main.itemTexture[item.type];
        //	Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/IMBellTex");
        //	Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
        //	Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
        //	effect.CurrentTechnique.Passes[0].Apply();
        //	Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleArry, 0, 2);
        //	Main.graphics.GraphicsDevice.RasterizerState = originalState;
        //	return false;
        //}
        //public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        //{
        //	CustomVertexInfo[] triangleArry = new CustomVertexInfo[6];
        //	RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
        //	//float offsetX = 32f / 1920 * (ItemID.Sets.ItemIconPulse[item.type] ? Main.essScale : 1f);
        //	//float offsetY = 32f / 1120 * (ItemID.Sets.ItemIconPulse[item.type] ? Main.essScale : 1f);
        //	float offsetX = 28f;
        //	float offsetY = 33f;
        //	Color c = Main.hslToRgb(0.5f, 0.5f, 0.5f);
        //	triangleArry[0] = new CustomVertexInfo(item.Center - new Vector2(offsetX, offsetY), c, new Vector3(0, 0, 0));
        //	triangleArry[1] = new CustomVertexInfo(item.Center + new Vector2(offsetX, -offsetY), c, new Vector3(1, 0, 0));
        //	triangleArry[2] = new CustomVertexInfo(item.Center + new Vector2(offsetX, offsetY), c, new Vector3(1, 1, 0));
        //	triangleArry[3] = triangleArry[2];
        //	triangleArry[4] = new CustomVertexInfo(item.Center - new Vector2(offsetX, -offsetY), c, new Vector3(0, 1, 0));
        //	triangleArry[5] = triangleArry[0];
        //	Effect effect = mod.GetEffect("Effects/InfiniteNightmareBell");
        //	var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
        //	var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
        //	effect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
        //	effect.Parameters["uTime"].SetValue((float)IllusionBoundMod.ModTime / 60);
        //	Main.graphics.GraphicsDevice.Textures[0] = Main.itemTexture[item.type];
        //	Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/IMBellTex");
        //	Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
        //	Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
        //	effect.CurrentTechnique.Passes[0].Apply();
        //	Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleArry, 0, 2);
        //	Main.graphics.GraphicsDevice.RasterizerState = originalState;
        //	return false;
        //}
        public override bool CanUseItem(Player player)
        {
            var V = player.GetModPlayer<InfiniteNightmarePlayer>();
            return !(V.InfiniteNightmareModeActive || V.PreInfiniteNightmareModeActive || V.ReallyInfiniteNightmareModeActive || V.PreReallyInfiniteNightmareModeActive);
        }
        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, default, ProjectileType<InfiniteNightmareHugeJadeBullet>(), 20000, 1, player.whoAmI, 0, 0.6f);
            player.GetModPlayer<InfiniteNightmarePlayer>().PreInfiniteNightmareModeActive = true;
            return base.UseItem(player);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("无间之钟");
            Tooltip.SetDefault("\"很眼熟，对吧？想必你的前世为了一时麻痹自己获得快乐而敲响了它，落入泰拉瑞亚这无间地狱之中。再度死亡也不会让你逃脱这幻世。\"\n\"......\"\n\"要再度敲响吗？虽然不会让你回到前世的世界，但至少在这里，你会好过很多。\"\n敲响之后各方面属性获得提升，但是死亡一次之后就会进入无间地狱模式。");
        }
    }
    public class InfiniteNightmareEX : InfiniteNightmareItem
    {
        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 66;
            item.maxStack = 1;
            item.rare = ItemRarityID.Purple;
            item.value = 0;
            item.useTime = 60;
            item.useAnimation = 60;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Shoot;
            item.consumable = true;

        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddTile(TileID.DemonAltar);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
        public override bool CanUseItem(Player player)
        {
            var V = player.GetModPlayer<InfiniteNightmarePlayer>();
            return V.InfiniteNightmareModeActive;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.ShaderItemEffectInventory(spriteBatch, position, origin, IllusionBoundMod.GetTexture("Images/IMBellTex"), Main.hslToRgb(0, 1, 0.75f), scale);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.ShaderItemEffectInWorld(spriteBatch, IllusionBoundMod.GetTexture("Images/IMBellTex"), Main.hslToRgb(0, 1, 0.75f), rotation);
        }
        //public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
        //	Vector2 realPos = position + new Vector2(10, 12);
        //	for (float n = 0; n < 256; n++)
        //	{
        //		spriteBatch.Draw(IllusionBoundMod.GetTexture("Items/Weapons/VoidGas"), realPos - new Vector2(0, (n + (int)Main.time) % 256 - 128), new Rectangle(0, (int)n, 256, 1), new Color(255, 153, 153) * MathHelper.Clamp((float)Math.Sin((n + Main.time) % 256 / 256 * MathHelper.Pi) + 0.3f, 0f, 1f), 0, new Vector2(128, 0), 1.5f, SpriteEffects.None, 0);
        //	}
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //}
        //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        //{
        //	Vector2 position = item.position + new Vector2(28, 33) - Main.screenPosition;
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
        //	for (float n = 0; n < 256; n++)
        //	{
        //		spriteBatch.Draw(IllusionBoundMod.GetTexture("Items/Weapons/VoidGas"), position - new Vector2(0, (n + (int)Main.time) % 256 - 128), new Rectangle(0, (int)n, 256, 1), new Color(255, 153, 153) * MathHelper.Clamp((float)Math.Sin((n + Main.time) % 256 / 256 * MathHelper.Pi) + 0.3f, 0f, 1f), 0, new Vector2(128, 0), 1.5f, SpriteEffects.None, 0);
        //	}
        //	spriteBatch.End();
        //	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //}
        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, default, ProjectileType<InfiniteNightmareHugeJadeBullet>(), 40000, 1, player.whoAmI);
            player.GetModPlayer<InfiniteNightmarePlayer>().PreReallyInfiniteNightmareModeActive = true;
            player.GetModPlayer<InfiniteNightmarePlayer>().InfiniteNightmareModeActive = false;
            return base.UseItem(player);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("无间之钟EX");
            Tooltip.SetDefault("\"一时敲钟一时爽，一直敲钟一直爽，对吗？看来你还是没有得到教训。\"\n\"在无间地狱中的世界过得如何？如果后悔了，敲响眼前这个不一样的吧。\"\n\"这一次，它会带你回到非无间地狱模式。\"\n\"但是，答应我，这一次，你不许死。\"\n解除无间地狱模式，如果再度死亡，会进入真·无间地狱。\n\"不过人本身就是为了眼前一时利益而牺牲长远利益的生物，你一定会再次敲响的吧。\"");
        }
    }
    public class InfiniteNightmareHugeJadeBullet : InfiniteNightmareProjectile
    {
        protected float Size = 1;
        protected float Alpha = 1;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (targetHitbox.Center.ToVector2() - projectile.Center).Length() <= (220 * Size / 2 + 16);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("光团");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 256;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center;
            projectile.ai[0]++;
            Size = -0.01f * projectile.ai[0] * (projectile.ai[0] - 60);
            Alpha = (float)Math.Sin(Math.Pow(projectile.ai[0] / 120 - 1, 2) * MathHelper.Pi);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Main.hslToRgb(projectile.ai[1], 1, 0.8f) * Alpha, 0, new Vector2(projectile.width / 2, projectile.height / 2), Size, SpriteEffects.None, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class HolyThing : InfiniteNightmareItem
    {
        public override void SetDefaults()
        {
            item.width = 13;
            item.height = 13;
            item.maxStack = 1;
            item.rare = ItemRarityID.Purple;
            item.value = 0;
            item.useTime = 60;
            item.useStyle = ItemUseStyleID.Shoot;
            item.useAnimation = 60;
        }
        public override bool CanUseItem(Player player)
        {
            var V = player.GetModPlayer<InfiniteNightmarePlayer>();
            return V.InfiniteNightmareModeActive || V.PreInfiniteNightmareModeActive || V.ReallyInfiniteNightmareModeActive || V.PreReallyInfiniteNightmareModeActive;
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<InfiniteNightmarePlayer>().PreInfiniteNightmareModeActive = false;
            player.GetModPlayer<InfiniteNightmarePlayer>().InfiniteNightmareModeActive = false;
            player.GetModPlayer<InfiniteNightmarePlayer>().PreReallyInfiniteNightmareModeActive = false;
            player.GetModPlayer<InfiniteNightmarePlayer>().ReallyInfiniteNightmareModeActive = false;
            for (int n = 0; n < 4; n++)
            {
                Dust.NewDustPerfect(player.Center, MyDustId.CyanBubble, new Vector2(1, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), 0, Color.White);
            }
            return base.UseItem(player);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("神圣之物");
            Tooltip.SetDefault("能让你逃出无间地狱的真正神器\n获得真正的幸福吧（\n屑⑨厨阿汪又拿⑨整活（");
        }
    }
    public class InfiniteNightmarePlayer : ModPlayer
    {
        public bool PreInfiniteNightmareModeActive;
        public bool InfiniteNightmareModeActive;
        public bool PreReallyInfiniteNightmareModeActive;
        public bool ReallyInfiniteNightmareModeActive;
        public int breatheCountInSpace;
        //public bool TooDarkBuffActive;
        //public bool TooDazzlingBuffActive;
        public static bool TooDarkBuffActive;
        public static bool TooDazzlingBuffActive;
        public bool TooQuietBuffActive;
        public bool TooSweetBuffActive;
        public bool[] ImmuneType = new bool[10];
        public bool[] VisuableBackPack = new bool[6];
        public bool hasGivenBell;
        public bool hasGivenBellEX;
        public Player player => Player;
        public override void ResetEffects()
        {
            //for (int n = 0; n < player.armor.Length; n++) 
            //{
            //	if (player.armor[n].type == ItemID.AncientGoldHelmet) 
            //	{
            //		Main.NewText(n);
            //	}
            //}
            if (PreInfiniteNightmareModeActive)
            {
                player.statLifeMax2 += player.statLifeMax2 / 5;
                player.statManaMax2 += player.statManaMax2 / 5;
                player.statDefense += 2;
                player.extraAccessorySlots++;
                player.maxMinions++;
                player.GetDamage(DamageClass.Generic) += 0.1f;
            }
            if (InfiniteNightmareModeActive)
            {
                player.maxMinions--;
                player.GetDamage(DamageClass.Generic) -= 0.05f;
                player.statLifeMax2 -= player.statLifeMax2 / 10;
                player.statManaMax2 -= player.statManaMax2 / 5;
            }
            if (ReallyInfiniteNightmareModeActive)
            {
                player.statLifeMax2 -= player.statLifeMax2 / 5;
                player.statManaMax2 -= player.statManaMax2 / 4;
                player.maxMinions -= 1;
                player.GetDamage(DamageClass.Generic) -= 0.15f;
            }
            if (TooDazzlingBuffActive)
            {
                IllusionBoundModSystem.ContrastGlassActive = true;
                player.statLifeMax2 -= player.statLifeMax2 / 20;
                player.statManaMax2 -= player.statManaMax2 / 20;
            }
            if (InfiniteNightmareModeActive || ReallyInfiniteNightmareModeActive)
            {
                if (player.breath == 0)
                {
                    player.KillMe(PlayerDeathReason.ByOther(1), 10.0, 0, false);
                }
            }
            TooDarkBuffActive = false;
            TooQuietBuffActive = false;
            TooSweetBuffActive = false;
            TooDazzlingBuffActive = false;
            if (InfiniteNightmareModeActive)
            {
                //InfiniteNightmareModeUpdate();
            }
            if (ImmuneType[1] && player.breath < player.breathMax)
            {
                player.breath += (Main.time % 15 < 1) ? 1 : 0;
            }
            for (int n = 0; n < 10; n++)
            {
                ImmuneType[n] = false;
            }
            for (int n = 0; n <= 5; n++)
            {
                VisuableBackPack[n] = false;
            }
            IllusionBoundMod.UnderGroundActive = false;
            //IllusionBoundMod.HarderActive = InfiniteNightmareModeActive || ReallyInfiniteNightmareModeActive;
            //IllusionBoundMod.IHarderActive = InfiniteNightmareModeActive;
        }
        public override void PostUpdate()
        {
            if (InfiniteNightmareModeActive)
            {
                InfiniteNightmareModeUpdate();
            }
            if (ReallyInfiniteNightmareModeActive)
            {
                ReallyInfiniteNightmareModeUpdate();
            }
        }
        private int SixteenF(float n)
        {
            return (int)(n / 16);
        }
        //private bool GetTopTile(Player player)
        //{
        //	bool flag1 = false;
        //	int n1 = 0;
        //	while (!flag1 && n1 <= 70)
        //	{
        //		Tile tile = Main.tile[SixteenF(player.position.X), SixteenF(player.position.Y) - n1];
        //		//bool flag = (tile != null) && (tile.type != 0);
        //		bool flag = tile.active();
        //		flag1 |= flag;
        //		n1++;
        //	}
        //	bool flag2 = false;
        //	int n2 = 0;
        //	while (!flag2 && n2 <= 70)
        //	{
        //		Tile tile = Main.tile[SixteenF(player.position.X) + 1, SixteenF(player.position.Y) - n2];
        //		bool flag = tile.active();
        //		flag2 |= flag;
        //		n2++;
        //	}
        //	return flag1 || flag2;
        //}
        //private bool IsInWater(Player player)
        //{
        //	int x = SixteenF(player.position.X);
        //	int y = SixteenF(player.position.Y);
        //	bool flag = false;
        //	for (int n = 0; n < 2; n++) 
        //	{
        //		for (int i = 0; i < 3; i++)
        //		{
        //			flag |= Main.tile[x + n, y + i].liquid == 224;
        //		}
        //	}
        //	return flag;
        //}
        private bool CheckFire()
        {
            int count = 0;
            for (int n = -9; n < 11; n++)
            {
                for (int i = -8; i < 11; i++)
                {
                    //Tile tile = Main.tile[SixteenF(player.position.X) + n, SixteenF(player.position.Y) + i];
                    //bool flag2 = (tile != null) && (tile.type != 0);
                    //flag |= flag2;
                    Tile tile = Main.tile[SixteenF(player.position.X) + n, SixteenF(player.position.Y) + i];
                    bool flag2 = tile.TileType == 4;
                    count += flag2 ? 1 : 0;
                }
            }
            return count >= 5;
        }
        private void ZoneDebuffUpdate()
        {
            if (player.ZoneSkyHeight)
            {
                if (!ImmuneType[1])
                {
                    player.AddBuff(BuffType<Hypoxia>(), 2);
                }
            }
            if (player.ZoneSnow)
            {
                if ((!ImmuneType[0]) && (!CheckFire()))
                {
                    player.AddBuff(BuffType<LowTemperature>(), 2);
                }
                return;
            }
            if (player.ZoneDesert)
            {
                if (!ImmuneType[4])
                {
                    player.AddBuff(BuffType<TooDry>(), 2);
                }
                return;
            }
            if (player.ZoneJungle)
            {
                if (!ImmuneType[3])
                {
                    player.AddBuff(BuffType<HotWithWet>(), 2);
                }
                return;
            }
            //if (player.GetModPlayer<IllusionBoundPlayer>().ZoneStorm)
            //{
            //	if (!ImmuneType[6])
            //	{
            //		player.AddBuff(BuffType<Radiation>(), 2);
            //	}
            //	return;
            //}
            if (player.ZoneUnderworldHeight)
            {
                if (!ImmuneType[2])
                {
                    player.AddBuff(BuffType<HighTemperature>(), 2);
                }
                return;
            }
            if (player.ZoneDungeon || player.zone4[5])
            {
                if (!ImmuneType[5])
                {
                    player.AddBuff(BuffType<TooDark>(), 2);
                }
                return;
            }
            if (player.Center.Y / 16 > Main.worldSurface)
            {
                IllusionBoundMod.UnderGroundActive = true;
                if (!ImmuneType[8])
                {
                    player.AddBuff(BuffType<TooQuiet>(), 2);
                }
                if (player.ZoneCorrupt || player.ZoneCrimson)
                {
                    if (player.statLife < player.statLifeMax2 && !ImmuneType[7])
                    {
                        player.AddBuff(BuffType<Infected>(), 2);
                    }
                }
                if (player.ZoneHallow)
                {
                    if (player.armor[0].type != ItemID.Sunglasses)
                    {
                        player.AddBuff(BuffType<TooDazzling>(), 2);
                    }
                }
                return;
            }
            //if (WorldGen.checkUnderground((int)player.Center.X / 16, (int)player.Center.Y / 16) && !player.ZoneUnderworldHeight)
            //{
            //	IllusionBoundMod.UnderGroundActive = true;
            //	if (!ImmuneType[8]) 
            //	{
            //		player.AddBuff(BuffType<TooQuiet>(), 2);
            //	}
            //	return;
            //}
            if (player.ZoneHallow)
            {
                if (player.armor[0].type != ItemID.Sunglasses)
                {
                    player.AddBuff(BuffType<TooDazzling>(), 2);
                }
                return;
            }
            if (player.ZoneBeach)
            {
                if (!ImmuneType[9])
                {
                    player.AddBuff(BuffType<EasyToBeRusty>(), 2);
                }
                return;
            }
            if (player.ZoneCorrupt || player.ZoneCrimson)
            {
                if (player.statLife < player.statLifeMax2 && !ImmuneType[7])
                {
                    player.AddBuff(BuffType<Infected>(), 2);
                }
                return;
            }
            player.AddBuff(BuffType<TheLastPieceOfPureLand>(), 2);
        }
        private void UniqueDebuffUpdate()
        {
            //if (Main.raining && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !GetTopTile(player)&&!player.HasBuff(156) &&!ImmuneType[3])
            //{
            //	player.AddBuff(BuffType<DoThingsSloppily>(), 1800);
            //}
            bool flag3 = false;
            foreach (var rain in Main.rain)
            {
                flag3 |= rain.active && player.Hitbox.Contains((rain.position + new Vector2(rain.scale, rain.scale)).ToPoint());
                if (flag3)
                {
                    break;
                }
            }
            //if (player.GetModPlayer<IllusionBoundPlayer>().ZoneStorm) 
            //{
            //	foreach (var rain in IllusionBoundMod.rain)
            //	{
            //		flag3 |= rain.active && player.Hitbox.Contains((rain.position + new Vector2(rain.scale, rain.scale)).ToPoint());
            //		if (flag3)
            //			break;
            //	}
            //}
            if (flag3 && !player.HasBuff(156) && !ImmuneType[3] && player.HeldItem.type != ItemID.Umbrella && player.armor[0].type != ItemID.UmbrellaHat)
            {
                player.AddBuff(BuffType<DoThingsSloppily>(), 1800);
            }
            if (player.wet && !player.lavaWet && !player.honeyWet && !player.HasBuff(156))
            {
                player.AddBuff(BuffType<DoThingsReallySloppily>(), 900);
            }
            if (player.lavaWet && !player.HasBuff(156) && !player.HasBuff(BuffID.ObsidianSkin))
            {
                player.AddBuff(BuffType<SwimInTheLava>(), 300);
            }
            if (player.honeyWet && !player.HasBuff(156))
            {
                player.AddBuff(BuffType<TooSweet>(), 1500);
            }
            if (!player.ZoneSnow && CheckFire() && !ImmuneType[2])
            {
                player.AddBuff(BuffType<TooWarm>(), 300);
            }
            {
                float MaxD = 6000f;
                NPC N = null;
                foreach (NPC n in Main.npc)
                {
                    if ((n.type == NPCID.LunarTowerSolar || n.type == NPCID.LunarTowerVortex || n.type == NPCID.LunarTowerNebula || n.type == NPCID.LunarTowerStardust) && n.active)
                    {
                        float D = (player.Center - n.Center).Length();
                        if (D < MaxD)
                        {
                            MaxD = D;
                            N = n;
                        }
                    }
                }
                if (N != null)
                {
                    if (!player.gravControl2)
                    {
                        player.AddBuff(BuffType<Gravitation>(), 2);
                    }
                }
            }
            bool flag1 = false;
            bool flag2 = false;
            int index1 = -1;
            int index2 = -1;
            for (int n = 0; n < 22; n++)
            {
                bool flag = (player.buffType[n] == BuffType<DoThingsReallySloppily>()) || (player.buffType[n] == BuffType<DoThingsSloppily>()) || (player.buffType[n] == BuffType<TooSweet>());
                flag1 |= player.buffType[n] == BuffType<SwimInTheLava>();
                flag2 |= flag;
                if (player.buffType[n] == BuffType<SwimInTheLava>())
                {
                    index1 = n;
                }
                if (flag)
                {
                    index2 = n;
                }
            }
            if (flag1 && flag2)
            {
                player.buffTime[index1] = 0;
                player.buffTime[index2] = 0;
                player.AddBuff(156, 300);
            }
        }
        private void InfiniteNightmareModeUpdate()
        {
            ZoneDebuffUpdate();
            UniqueDebuffUpdate();
        }
        private void ReallyInfiniteNightmareModeUpdate()
        {
            if (!ImmuneType[1])
            {
                player.AddBuff(BuffType<Hypoxia>(), 2);
            }
            if ((!ImmuneType[0]) && (!CheckFire()))
            {
                player.AddBuff(BuffType<LowTemperature>(), 2);
            }
            if (!ImmuneType[4])
            {
                player.AddBuff(BuffType<TooDry>(), 2);
            }
            if (!ImmuneType[3])
            {
                player.AddBuff(BuffType<HotWithWet>(), 2);
            }
            if (!ImmuneType[2])
            {
                player.AddBuff(BuffType<HighTemperature>(), 2);
            }
            if (!ImmuneType[5])
            {
                player.AddBuff(BuffType<TooDark>(), 2);
            }
            if (!ImmuneType[8])
            {
                player.AddBuff(BuffType<TooQuiet>(), 2);
            }
            if (player.armor[0].type != ItemID.Sunglasses)
            {
                player.AddBuff(BuffType<TooDazzling>(), 2);
            }
            if (!ImmuneType[9])
            {
                player.AddBuff(BuffType<EasyToBeRusty>(), 2);
            }
            if (player.statLife < player.statLifeMax2 && !ImmuneType[7])
            {
                player.AddBuff(BuffType<Infected>(), 2);
            }
            player.AddBuff(BuffType<TheLastPieceOfPureLand>(), 2);
            UniqueDebuffUpdate();
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            //var drawData1 = new DrawData(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Dark"), new Vector2(SixteenF(player.position.X), SixteenF(player.position.Y)) - Main.screenPosition, new Rectangle(0,0,16,16), new Color(255, 255, 255, 255), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //Main.playerDrawData.Add(drawData1);
            //if (TooDarkBuffActive) 
            //{
            //	//var drawData = new DrawData(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Dark"), player.Center - Main.screenPosition, null, new Color(153, 153, 153, 153), 0, new Vector2(960, 560), 1, SpriteEffects.None, 0);
            //	//var drawData = new DrawData(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Dark"), new Vector2(0, 0), null, new Color(51, 51, 51, 51), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //	//Main.playerDrawData.Add(drawData);

            //	Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Dark"), new Vector2(0, 0), null, new Color(153, 153, 153, 153) * (player.armorEffectDrawShadow ? 0.2f : 1f), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //}
            //if (TooDazzlingBuffActive)
            //{
            //	//var drawData = new DrawData(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Light"), new Vector2(0, 0), null, new Color(17, 17, 17, 17), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //	//Main.playerDrawData.Add(drawData);
            //	Main.spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/Light"), new Vector2(0, 0), null, new Color(51, 51, 51, 51) * (player.armorEffectDrawShadow ? 0.2f : 1f), 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //}
            if (TooQuietBuffActive)
            {
                //float x = (float)Main.time % 3600;
                float x = 1600;
                float num5 = MathHelper.Clamp((float)Math.Sin(x / 120f) * 2f, 0f, 1f);
                num5 *= 0.75f - x / 7200f;
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", Main.player[Main.myPlayer].position, new object[0]);
                }
                Filters.Scene["MoonLordShake"].GetShader().UseIntensity(num5);
            }
            for (int n = 5; n >= 0; n--)
            {
                if (VisuableBackPack[n])
                {
                    SpriteEffects spriteEffects;
                    if (player.gravDir == 1f)
                    {
                        if (player.direction == 1)
                        {
                            spriteEffects = SpriteEffects.None;
                        }
                        else
                        {
                            spriteEffects = SpriteEffects.FlipHorizontally;
                        }
                    }
                    else
                    {
                        if (player.direction == 1)
                        {
                            spriteEffects = SpriteEffects.FlipVertically;
                        }
                        else
                        {
                            spriteEffects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
                        }
                    }
                    Color color12 = player.GetImmuneAlphaPure(Lighting.GetColor((int)(player.position.X + player.width * 0.5) / 16, (int)(player.position.Y + player.height * 0.5) / 16, Color.White), 0);
                    var drawData = new DrawData(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/BackpackS"), new Vector2(0, player.gfxOffY) + new Vector2((int)(player.position.X - Main.screenPosition.X + player.width / 2 - 9 * player.direction) + -4 * (float)player.direction, (int)(player.position.Y - Main.screenPosition.Y + player.height / 2 + 2f * player.gravDir + -8 * player.gravDir)), new Rectangle(14 * n, 0, 14, 34), color12, player.bodyRotation, new Vector2(7, 17), 1f, spriteEffects, 0);
                    drawInfo.DrawDataCache.Add(drawData);
                    break;
                }
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (!hasGivenBell)
            {
                hasGivenBell = true;
                Item.NewItem(player.GetSource_Misc("PlayerDropItemCheck"), player.Center, ItemType<InfiniteNightmare>());
            }
            if (!hasGivenBellEX && InfiniteNightmareModeActive)
            {
                hasGivenBellEX = true;
                Item.NewItem(player.GetSource_Misc("PlayerDropItemCheck"), player.Center, ItemType<InfiniteNightmareEX>());
            }
            return true;
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (PreInfiniteNightmareModeActive)
            {
                PreInfiniteNightmareModeActive = false;
                InfiniteNightmareModeActive = true;
            }
            if (PreReallyInfiniteNightmareModeActive)
            {
                PreReallyInfiniteNightmareModeActive = false;
                ReallyInfiniteNightmareModeActive = true;
            }
        }
        public override void LoadData(TagCompound tag)
        {
            PreInfiniteNightmareModeActive = tag.GetBool("PreInfiniteNightmareModeActive");
            InfiniteNightmareModeActive = tag.GetBool("InfiniteNightmareModeActive");
            PreReallyInfiniteNightmareModeActive = tag.GetBool("PreReallyInfiniteNightmareModeActive");
            ReallyInfiniteNightmareModeActive = tag.GetBool("ReallyInfiniteNightmareModeActive");
            hasGivenBell = tag.GetBool("hasGivenBell");
            hasGivenBellEX = tag.GetBool("hasGivenBellEX");
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Set("PreInfiniteNightmareModeActive", PreInfiniteNightmareModeActive);
            tag.Set("InfiniteNightmareModeActive", InfiniteNightmareModeActive);
            tag.Set("PreReallyInfiniteNightmareModeActive", PreReallyInfiniteNightmareModeActive);
            tag.Set("ReallyInfiniteNightmareModeActive", ReallyInfiniteNightmareModeActive);
            tag.Set("hasGivenBell", hasGivenBell);
            tag.Set("hasGivenBellEX", hasGivenBellEX);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            double d = damage + player.statDefense * (Main.expertMode ? 0.75 : 0.5);
            if (PreInfiniteNightmareModeActive)
            {
                damage = (int)(d - player.statDefense);
            }
            if (InfiniteNightmareModeActive)
            {
                damage = (int)(d - (double)player.statDefense / 3);
            }
            if (ReallyInfiniteNightmareModeActive)
            {
                damage = (int)(d - (double)player.statDefense / 4);
            }
            return true;
        }
    }
    public class InfiniteNightmareNPC : GlobalNPC
    {
        public static int MultiplyK(int value, float k, bool negative = false)
        {
            if (negative)
            {
                return (int)(value * (1 - k));
            }

            return (int)(value * (1 + k));
        }
        public static float MultiplyK(float value, float k, bool negative = false)
        {
            if (negative)
            {
                return value * (1 - k);
            }

            return value * (1 + k);
        }
        private void SetValue(NPC npc, float k)
        {
            if (!npc.friendly)
            {
                npc.lifeMax = MultiplyK(npc.lifeMax, k);
                npc.defense = MultiplyK(npc.defense, k);
                npc.damage = MultiplyK(npc.damage, k);
                npc.knockBackResist = MultiplyK(npc.knockBackResist, k, true);
                npc.value = MultiplyK(npc.value, k);
            }
            if (npc.friendly)
            {
                npc.lifeMax = MultiplyK(npc.lifeMax, k, true);
                npc.defense = MultiplyK(npc.defense, k, true);
                npc.damage = MultiplyK(npc.damage, k, true);
                npc.knockBackResist = MultiplyK(npc.knockBackResist, k);
            }
        }
        public override void SetDefaults(NPC npc)
        {
            if (IllusionBoundMod.HarderActive)
            {
                if (IllusionBoundMod.IHarderActive)
                {
                    SetValue(npc, 0.3f);
                    return;
                }
                SetValue(npc, 0.5f);
            }
            //SetValue(npc, 0.5f);
        }
        private void ShootLaser(NPC npc)
        {
            Vector2 vec = new Vector2(Main.rand.NextFloat(-60, 60), -Main.rand.NextFloat(24, 30));
            int dmg = npc.damage;
            if (Main.expertMode)
            {
                dmg /= 3;
            }
            if (vec.X > 0)
            {
                Vector2 vec2 = new Vector2(Main.rand.NextFloat(-4, 0), Main.rand.NextFloat(3, 6));
                Projectile.NewProjectile(npc.GetSource_Death(), Main.player[npc.target].Center + vec * 16, vec2 * 2, ProjectileType<LaserBullet>(), dmg, 2, Main.myPlayer, 1 / 6f, 2);
            }
            if (vec.X <= 0)
            {
                Vector2 vec2 = new Vector2(Main.rand.NextFloat(0, 4), Main.rand.NextFloat(3, 6));
                Projectile.NewProjectile(npc.GetSource_Death(), Main.player[npc.target].Center + vec * 16, vec2 * 2, ProjectileType<LaserBullet>(), dmg, 2, Main.myPlayer, 1 / 6f, 2);
            }
            //Projectile.NewProjectile(Main.player[npc.target].Center + vec * 16, vec2.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)), ProjectileType<LaserBullet>(), npc.damage, 2, Main.myPlayer, 1 / 6f, 2);
        }
        public override void OnKill(NPC npc)
        {
            try
            {
                if (!IllusionBoundMod.HarderActive)
                {
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Main.NewText(Main.myPlayer);
                Main.NewText(Main.player.Length, Color.Red);
                return;
            }
            int dmg = npc.damage;
            if (Main.expertMode)
            {
                dmg /= 2;
            }
            Vector2 cen = (npc.target > 255 || npc.target < 0 || new Rectangle((int)Main.player[npc.target].Center.X - 960, (int)Main.player[npc.target].Center.Y - 560, 1920, 1120).Contains(npc.Center.ToPoint())) ? npc.Center : Main.player[npc.target].Center;
            if (npc.type == NPCID.KingSlime)
            {
                Vector2 vector2 = Main.player[npc.target].Center - cen;
                vector2.Normalize();
                vector2 = vector2.Length() < 1f ? Main.player[npc.target].velocity : vector2;
                Projectile.NewProjectile(npc.GetSource_Death(), cen, vector2 * 8, ProjectileType<LightJadeBullet>(), npc.damage, 5, Main.myPlayer, 3, 1);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                Vector2 vector2 = Main.player[npc.target].Center - cen;
                vector2.Normalize();
                vector2 = vector2.Length() < 1f ? Main.player[npc.target].velocity : vector2;
                for (int n = 0; n < 16; n++)
                {
                    Vector2 pos = cen + vector2.RotatedBy(MathHelper.TwoPi / 16 * n) * 128f;
                    Projectile.NewProjectile(npc.GetSource_Death(), pos, -vector2.RotatedBy(MathHelper.TwoPi / 16 * n) * 4, ProjectileType<PaperBullet>(), dmg, 2, Main.myPlayer, Main.rand.Next(3));
                    Projectile.NewProjectile(npc.GetSource_Death(), pos, vector2.RotatedBy(MathHelper.TwoPi / 16 * n + Math.PI / 3) * 4, ProjectileType<PaperBullet>(), dmg, 2, Main.myPlayer, Main.rand.Next(3), 1);
                    Projectile.NewProjectile(npc.GetSource_Death(), pos, vector2.RotatedBy(MathHelper.TwoPi / 16 * n - Math.PI / 3) * 4, ProjectileType<PaperBullet>(), dmg, 2, Main.myPlayer, Main.rand.Next(3), 1);
                    Projectile.NewProjectile(npc.GetSource_Death(), pos, vector2.RotatedBy(MathHelper.TwoPi / 16 * n) * 4, ProjectileType<PaperBullet>(), dmg, 2, Main.myPlayer, Main.rand.Next(3));
                    Projectile.NewProjectile(npc.GetSource_Death(), pos, -vector2.RotatedBy(MathHelper.TwoPi / 16 * n + Math.PI / 3) * 4, ProjectileType<PaperBullet>(), dmg, 2, Main.myPlayer, Main.rand.Next(3), 1);
                    Projectile.NewProjectile(npc.GetSource_Death(), pos, -vector2.RotatedBy(MathHelper.TwoPi / 16 * n - Math.PI / 3) * 4, ProjectileType<PaperBullet>(), dmg, 2, Main.myPlayer, Main.rand.Next(3), 1);
                }
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type >= NPCID.EaterofWorldsHead && npc.type <= NPCID.EaterofWorldsTail)
            {
                Vector2 vector2 = Main.player[npc.target].Center - npc.Center;
                vector2.Normalize();
                Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, vector2 * 8, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 2, 1);
            }
            //float r = Main.rand.NextFloat(0, 16f);
            if (npc.type == NPCID.Creeper)
            {
                for (int n = 0; n < 4; n++)
                {
                    ShootLaser(npc);
                }
                //Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-r, r), -Main.rand.NextFloat(12, 18)), ProjectileType<LaserBullet>(), npc.damage, 1, Main.myPlayer, 1 / 6f, 1);
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                for (int n = 0; n < 15; n++)
                {
                    ShootLaser(npc);
                }
            }
            if (npc.type == NPCID.DD2DarkMageT1)
            {
                Vector2 vector2 = ((npc.target < 0 || npc.target > 255) ? Main.screenPosition + new Vector2(960, 560) : Main.player[npc.target].Center) - npc.Center;
                for (int n = 0; n < 6; n++)
                {
                    vector2.Normalize();
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.Center + vector2.RotatedBy(MathHelper.TwoPi / 6 * n + MathHelper.TwoPi / 12) * 16, vector2.RotatedBy(-MathHelper.PiOver2 + MathHelper.TwoPi / 6 * n + MathHelper.TwoPi / 12) * 4, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 2, 1);
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.Center + vector2.RotatedBy(MathHelper.TwoPi / 6 * n) * 32, vector2.RotatedBy(MathHelper.PiOver2 + MathHelper.TwoPi / 6 * n) * 8, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 2, 3);
                }
            }
            if (npc.type == NPCID.DD2DarkMageT3)
            {
                Vector2 vector2 = ((npc.target < 0 || npc.target > 255) ? Main.screenPosition + new Vector2(960, 560) : Main.player[npc.target].Center) - npc.Center;
                for (int n = 0; n < 12; n++)
                {
                    vector2.Normalize();
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.Center + vector2.RotatedBy(MathHelper.TwoPi / 12 * n + MathHelper.TwoPi / 24) * 16, vector2.RotatedBy(-MathHelper.PiOver2 + MathHelper.TwoPi / 12 * n + MathHelper.TwoPi / 24) * 4, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 2, 1);
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.Center + vector2.RotatedBy(MathHelper.TwoPi / 12 * n) * 32, vector2.RotatedBy(MathHelper.PiOver2 + MathHelper.TwoPi / 12 * n) * 8, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 2, 3);
                }
            }
            if (npc.type == NPCID.QueenBee)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<HugeJadeBullet>(), dmg, 5, Main.myPlayer, 3, 1);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<SkeletronBullet>(), dmg, 5, Main.myPlayer);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<HugeJadeBullet>(), dmg, 5, Main.myPlayer, 1, 2);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.Spazmatism)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<HugeJadeBullet>(), dmg, 5, Main.myPlayer, 2, 3);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.Retinazer)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<HugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 4);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<SkeletronBulletEX>(), dmg, 5, Main.myPlayer);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.Probe)
            {
                Vector2 vector2 = Main.player[npc.target].Center - npc.Center;
                vector2.Normalize();
                Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, vector2 * 8, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 0, 2);
            }
            {           //if (npc.type >= NPCID.TheDestroyer && npc.type <= NPCID.TheDestroyerTail)
                        //{
                        //	Vector2 vector2 = Main.player[npc.target].Center - npc.Center;
                        //	vector2.Normalize();
                        //	Projectile.NewProjectile(npc.Center, vector2 * 8, ProjectileType<FireBallBullet>(), dmg, 5, Main.myPlayer, 0, 2);
                        //} 
            }
            if (npc.type == NPCID.Plantera)
            {
                for (int n = 0; n < 4; n++)
                {
                    int p = Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(480, 0).RotatedBy(MathHelper.TwoPi / 4 * n), default, ProjectileType<ButterFlyBullet>(), dmg, 5, Main.myPlayer, 2, 1);
                    ButterFlyBullet butterFlyBullet = (ButterFlyBullet)Main.projectile[p].ModProjectile;
                    butterFlyBullet.C = cen;
                    butterFlyBullet.r = MathHelper.TwoPi / 4 * n;
                }
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.Golem)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(256, 0).RotatedBy(MathHelper.Pi / 6), default, ProjectileType<ReallyHugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 1);
                Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(256, 0).RotatedBy(-MathHelper.Pi / 3), default, ProjectileType<ReallyHugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 1);
                Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(256, 0).RotatedBy(-2 * MathHelper.Pi / 3), default, ProjectileType<ReallyHugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 1);
                Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(256, 0).RotatedBy(-MathHelper.Pi - MathHelper.Pi / 6), default, ProjectileType<ReallyHugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 1);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.DukeFishron)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<ReallyHugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 2);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
                for (int i = 0; i < 18; i++)
                {
                    for (int n = 10; n < 45; n += 5)
                    {
                        int p = Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(n * 16, 0).RotatedBy(MathHelper.TwoPi / 18 * i), default, ProjectileType<HeartBullet>(), dmg, 5, Main.myPlayer, 3, 1);
                        HeartBullet heartBullet = (HeartBullet)Main.projectile[p].ModProjectile;
                        heartBullet.C = cen;
                    }
                    for (int n = 20; n < 84; n += 8)
                    {
                        int p = Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(n, 0).RotatedBy(MathHelper.TwoPi / 18 * i), default, ProjectileType<HeartBullet>(), dmg, 5, Main.myPlayer, 4, 2);
                        HeartBullet heartBullet = (HeartBullet)Main.projectile[p].ModProjectile;
                        heartBullet.C = cen;
                    }
                }
            }
            if (npc.type == NPCID.CultistBoss)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), cen, default, ProjectileType<ReallyHugeJadeBullet>(), dmg, 5, Main.myPlayer, 0, 3);
                SoundEngine.PlaySound(SoundID.Zombie104, npc.Center);
            }
            if (npc.type == NPCID.LunarTowerSolar)
            {
                for (int n = 0; n < 125; n++)
                {
                    Vector2 vector = new Vector2(Main.rand.NextFloat(4, 8), 6) * 1.5f;
                    Projectile.NewProjectile(npc.GetSource_Death(), cen + new Vector2(Main.rand.NextFloat(-960, 960) - 480, -Main.rand.NextFloat(400, 480)) - vector * 15 * (n / 5), vector, ProjectileType<DoubleStarBullet>(), 75, 2, Main.myPlayer, 6, 2);
                }
            }
            if (npc.type == NPCID.LunarTowerNebula)
            {
                //Vector2 vec1 = new Vector2(Main.rand.NextFloat(32, 80), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
                //Vector2 vec2 = new Vector2(Main.rand.NextFloat(32, 160), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
                //while (Math.Abs(vec1.X - vec2.X) < 80 || Math.Abs(vec1.Y - vec2.Y) < 96) 
                //{
                //	vec2 = new Vector2(Main.rand.NextFloat(32, 160), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi));
                //}
                //Main.NewText("基向量i为(" + vec1.X + "," + vec1.Y + ")", Color.Cyan, true);
                //Main.NewText("基向量j为(" + vec2.X + "," + vec2.Y + ")", Color.LawnGreen, true);
                //for (int n = -16; n < 16; n += 2) 
                //{
                //	for (int k = -16; k < 16; k += 2)
                //	{
                //		Projectile.NewProjectile(cen + vec1 * n + vec2 * k, new Vector2(2, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), ProjectileType<LightJadeBullet>(), 50, 2, Main.myPlayer, 2, 2);
                //	}
                //}
                for (int n = 0; n < 6; n++)
                {
                    Projectile.NewProjectile(npc.GetSource_Death(), cen, new Vector2(4, 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), ProjectileType<NebulaEnergyBall>(), 50, 2, Main.myPlayer);
                }
            }
            if (npc.type == NPCID.LunarTowerVortex)
            {
                for (int n = 0; n < 12; n++)
                {
                    Projectile.NewProjectile(npc.GetSource_Death(), cen, new Vector2(24, 0).RotatedBy((Main.player[npc.target].Center - npc.Center).ToRotation() + MathHelper.Pi / 6 * n), ProjectileType<LaserBullet>(), 50, 2, Main.myPlayer, 5f / 12f, 4);
                }
            }
            if (npc.type == NPCID.LunarTowerStardust)
            {
                //for (int n = 0; n < 5; n++) 
                //{
                //	int p = Projectile.NewProjectile(cen + new Vector2(256, 0).RotatedBy(MathHelper.TwoPi / 5 * n), default, ProjectileType<HugeStarBullet>(), 50, 2, Main.myPlayer, 4, 2);
                //	HugeStarBullet hugeStarBullet = (HugeStarBullet)Main.projectile[p].modProjectile;
                //	hugeStarBullet.sr = MathHelper.TwoPi / 5 * n;
                //	hugeStarBullet.C = cen;
                //}
                for (int n = 0; n < 16; n++)
                {
                    //for (int i = 0; i < 16; i++) 
                    //{
                    //	Projectile.NewProjectile(npc.Center + new Vector2(960, 0).RotatedBy((n + i / 16f - 8) * MathHelper.TwoPi / 16), new Vector2(i * (i - 16) / 32f - 1, 0).RotatedBy((n + i / 16f - 8) * MathHelper.TwoPi / 16), ProjectileType<StarBullet>(), 50, 2, Main.myPlayer, 6, 1);
                    //}
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(npc.GetSource_Death(), npc.Center - new Vector2(6f * i, 0).RotatedBy((n + i / 16f - 8) * MathHelper.TwoPi / 16), new Vector2(4f, 0).RotatedBy((n + i / 16f - 8) * MathHelper.TwoPi / 16), ProjectileType<StarBullet>(), 50, 2, Main.myPlayer, 6);
                        Projectile.NewProjectile(npc.GetSource_Death(), npc.Center - new Vector2(6f * i, 0).RotatedBy((n + i / 16f - 7.5f) * MathHelper.TwoPi / 16), new Vector2(4f, 0).RotatedBy((n + i / 16f - 7.5f) * MathHelper.TwoPi / 16), ProjectileType<StarBullet>(), 50, 2, Main.myPlayer, 8);
                    }
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, new Vector2(0, 1).RotatedBy((n - 8) * MathHelper.TwoPi / 16), ProjectileType<StarDustLaser>(), 50, 2, Main.myPlayer, 180 + n * 6, 1 / 2f);
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, new Vector2(0, 1).RotatedBy((n - 7.5f) * MathHelper.TwoPi / 16), ProjectileType<StarDustLaser>(), 50, 2, Main.myPlayer, 180 + (n + 15) * 6, 2 / 3f);
                }
            }
        }
    }
    public class SkeletronBullet : InfiniteNightmareProjectile
    {
        protected Player FindTargetPlayer()
        {
            Vector2 cen = projectile.Center;
            Player target = null;
            float distanceMax = 4096f;
            foreach (Player player in Main.player)
            {
                float currentDistance = Vector2.Distance(cen, player.Center);
                if (currentDistance < distanceMax)
                {
                    distanceMax = currentDistance;
                    target = player;
                }
            }
            return target;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            //return (Main.player[Main.myPlayer].Center - projectile.Center).Length() <= (scale / 2 + 16);
            return (targetHitbox.Center.ToVector2() - projectile.Center).Length() <= 36;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("骷髅王之首虚影");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 1;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Vector2 vec = FindTargetPlayer().Center - projectile.Center;
            vec.Normalize();
            projectile.velocity = new Vector2(6, 0).RotatedBy(vec.ToRotation());
            if (Main.time % 10 == 0)
            {
                if (Main.time % 20 == 0)
                {
                    for (float n = 0; n < 16; n++)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vec.RotatedBy(MathHelper.TwoPi / 16 * n + MathHelper.TwoPi / 32) * 2, ProjectileType<StarBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, Main.rand.Next(16), 1);
                    }
                    return;
                }
                for (float n = 0; n < 16; n++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vec.RotatedBy(MathHelper.TwoPi / 16 * n) * 2, ProjectileType<StarBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, Main.rand.Next(16), 1);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var bs = new BlendState
            {
                ColorDestinationBlend = Blend.Zero
            };
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White * (MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 300) + 600, 0, 120) / 120f), (float)Main.time / 60 * MathHelper.TwoPi, new Vector2(40, 51), 1, SpriteEffects.None, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class SkeletronBulletEX : InfiniteNightmareProjectile
    {
        protected Player FindTargetPlayer()
        {
            Vector2 cen = projectile.Center;
            Player target = null;
            float distanceMax = 4096f;
            foreach (Player player in Main.player)
            {
                float currentDistance = Vector2.Distance(cen, player.Center);
                if (currentDistance < distanceMax)
                {
                    distanceMax = currentDistance;
                    target = player;
                }
            }
            return target;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            //return (Main.player[Main.myPlayer].Center - projectile.Center).Length() <= (scale / 2 + 16);
            return (targetHitbox.Center.ToVector2() - projectile.Center).Length() <= 36;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("骷髅王之首虚影机械版");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 1;
            projectile.timeLeft = 900;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        //private float r;
        public override void AI()
        {
            Vector2 vec = FindTargetPlayer().Center - projectile.Center;
            vec.Normalize();
            projectile.velocity = new Vector2(6, 0).RotatedBy(vec.ToRotation());
            //if (projectile.timeLeft == 899) 
            //{
            //	//r = vec.ToRotation();
            //}
            if (Main.time % 10 == 0)
            {
                if (Main.time % 20 == 0)
                {
                    for (float n = 0; n < 16; n++)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vec.RotatedBy(MathHelper.TwoPi / 16 * n + MathHelper.TwoPi / 32) * 4, ProjectileType<ArrowBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 1, 1);
                    }
                    return;
                }
                for (float n = 0; n < 16; n++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vec.RotatedBy(MathHelper.TwoPi / 16 * n) * 4, ProjectileType<ArrowBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 1, 1);
                }
            }
            //if (Main.time % 3 == 0)
            //{
            //	r += MathHelper.TwoPi / 360;
            //	for (float n = 0; n < 8; n++)
            //	{
            //		Projectile.NewProjectile(projectile.Center, vec.RotatedBy(MathHelper.TwoPi / 8 * n + r) * 32, ProjectileType<ArrowBullet>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 1);
            //	}
            //}
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            float sign = projectile.velocity.X / Math.Abs(projectile.velocity.X);
            float k = 16 / (16 + Math.Abs(projectile.velocity.X));
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White * (MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 450) + 900, 0, 120) / 120f), MathHelper.PiOver2 * sign * (k - 1), new Vector2(41, 51), 1, SpriteEffects.None, 0);
            for (int n = 0; n < 8; n++)
            {
                spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/InfiniteNightmare/SkeletronBulletEXS"), projectile.Center + new Vector2(64, 0).RotatedBy((float)Main.time / 60 * MathHelper.TwoPi + MathHelper.TwoPi / 8 * n) - Main.screenPosition, null, Color.White * (MathHelper.Clamp(-2 * Math.Abs(projectile.timeLeft - 450) + 900, 0, 120) / 120f), (float)Main.time / 60 * MathHelper.TwoPi + MathHelper.TwoPi / 8 * n, new Vector2(14, 8), 1, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class NebulaEnergyBall : InfiniteNightmareProjectile
    {
        private Player FindTargetPlayer()
        {
            Vector2 cen = projectile.Center;
            Player target = null;
            float distanceMax = 4096f;
            foreach (Player player in Main.player)
            {
                float currentDistance = Vector2.Distance(cen, player.Center);
                if (currentDistance < distanceMax)
                {
                    distanceMax = currentDistance;
                    target = player;
                }
            }
            return target;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("星云光团");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.scale = 1f;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.light = 2f;
            projectile.aiStyle = -1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(TextureAssets.Projectile[ProjectileType<LightJadeBullet>()].Value, projectile.Center - Main.screenPosition, new Rectangle(64, 0, 32, 32), Color.White, (float)Main.time / 4, new Vector2(16, 16), 2f, SpriteEffects.None, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.Intersects(new Rectangle((int)projectile.Center.X - 16, (int)projectile.Center.Y - 16, 32, 32));
        }
        public override void AI()
        {
            Vector2 vec = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
            Dust.NewDustPerfect(projectile.position, MyDustId.GreenBubble, vec, 0, Color.White, 1f);
            projectile.velocity *= 1.02f;
            Player player = FindTargetPlayer();
            if (player != null)
            {
                Vector2 d = projectile.Center - player.Center;
                if (d.X + projectile.velocity.X >= 960)
                {
                    float r = 3;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + projectile.velocity + new Vector2(0, 16).RotatedBy(r * MathHelper.PiOver2), new Vector2(0, 0), ProjectileType<NebulaEnergyZone>(), projectile.damage, 0, projectile.owner, 0, r);
                    projectile.Kill();
                    SoundEngine.PlaySound(SoundID.Zombie104, projectile.Center);
                }
                else if (d.X + projectile.velocity.X <= -960)
                {
                    float r = 1;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + projectile.velocity + new Vector2(0, 16).RotatedBy(r * MathHelper.PiOver2), new Vector2(0, 0), ProjectileType<NebulaEnergyZone>(), projectile.damage, 0, projectile.owner, 0, r);
                    projectile.Kill();
                    SoundEngine.PlaySound(SoundID.Zombie104, projectile.Center);
                }
                if (d.Y + projectile.velocity.Y >= 560)
                {
                    float r = 0;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + projectile.velocity + new Vector2(0, 16).RotatedBy(r * MathHelper.PiOver2), new Vector2(0, 0), ProjectileType<NebulaEnergyZone>(), projectile.damage, 0, projectile.owner, 0, r);
                    projectile.Kill();
                    SoundEngine.PlaySound(SoundID.Zombie104, projectile.Center);
                }
                else if (d.Y + projectile.velocity.Y <= -560)
                {
                    // 
                    float r = 2;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + projectile.velocity + new Vector2(0, 16).RotatedBy(r * MathHelper.PiOver2), new Vector2(0, 0), ProjectileType<NebulaEnergyZone>(), projectile.damage, 0, projectile.owner, 0, r);
                    projectile.Kill();
                    SoundEngine.PlaySound(SoundID.Zombie104, projectile.Center);
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
        }
    }
    public class NebulaEnergyZone : InfiniteNightmareProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.scale = 1f;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.ignoreWater = true;
            projectile.alpha = 127;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.light = 0.5f;
            projectile.aiStyle = -1;
        }
        private float Time
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        private Vector2[] posG = new Vector2[121];
        private Vector2[] posP = new Vector2[121];
        private Vector2[] posH = new Vector2[100];
        private Vector2 GetVec(Vector2 vec)
        {
            switch ((int)projectile.ai[1] % 2)
            {
                case 0:
                    vec.X *= -1;
                    return vec;
                case 1:
                    vec.Y *= -1;
                    return vec;
            }
            return vec;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            List<CustomVertexInfo> bars1 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> bars2 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> bars3 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> bars4 = new List<CustomVertexInfo>();
            int sint = Math.Clamp((int)Time - 180, 1, 120);
            int eint = Math.Clamp((int)Time, 1, 120);
            for (int i = sint; i < eint; ++i)
            {
                var factor = ((float)i - sint) / ((float)eint - sint);
                var normalDir1 = posG[i - 1] - posG[i];
                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
                var normalDir2 = GetVec(posG[i - 1]) - GetVec(posG[i]);
                normalDir2 = Vector2.Normalize(new Vector2(-normalDir2.Y, normalDir2.X));
                var normalDir3 = posP[i - 1] - posP[i];
                normalDir3 = Vector2.Normalize(new Vector2(-normalDir3.Y, normalDir3.X));
                var normalDir4 = GetVec(posP[i - 1]) - GetVec(posP[i]);
                normalDir4 = Vector2.Normalize(new Vector2(-normalDir4.Y, normalDir4.X));
                //var color = Color.Lerp(Color.White, Color.Red, factor);
                //var w = MathHelper.Lerp(1f, 0.05f, factor);
                float w = (float)(1 - Math.Pow(2 * factor - 1, 4));
                bars1.Add(new CustomVertexInfo(posG[i] + projectile.Center + normalDir1 * 16, Color.White, new Vector3(factor, 1 / 16f, w)));
                bars1.Add(new CustomVertexInfo(posG[i] + projectile.Center + normalDir1 * -16, Color.White, new Vector3(factor, 0, w)));
                bars2.Add(new CustomVertexInfo(GetVec(posG[i]) + projectile.Center + normalDir2 * 16, Color.White, new Vector3(factor, 1 / 16f, w)));
                bars2.Add(new CustomVertexInfo(GetVec(posG[i]) + projectile.Center + normalDir2 * -16, Color.White, new Vector3(factor, 0, w)));
                bars3.Add(new CustomVertexInfo(posP[i] + projectile.Center + normalDir3 * 16, Color.White, new Vector3(factor, 1 / 16f, w)));
                bars3.Add(new CustomVertexInfo(posP[i] + projectile.Center + normalDir3 * -16, Color.White, new Vector3(factor, 0, w)));
                bars4.Add(new CustomVertexInfo(GetVec(posP[i]) + projectile.Center + normalDir4 * 16, Color.White, new Vector3(factor, 1 / 16f, w)));
                bars4.Add(new CustomVertexInfo(GetVec(posP[i]) + projectile.Center + normalDir4 * -16, Color.White, new Vector3(factor, 0, w)));
            }
            List<CustomVertexInfo> triangleList1 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> triangleList2 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> triangleList3 = new List<CustomVertexInfo>();
            List<CustomVertexInfo> triangleList4 = new List<CustomVertexInfo>();
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
                for (int i = 0; i < bars2.Count - 2; i += 2)
                {
                    triangleList2.Add(bars2[i]);
                    triangleList2.Add(bars2[i + 2]);
                    triangleList2.Add(bars2[i + 1]);
                    triangleList2.Add(bars2[i + 1]);
                    triangleList2.Add(bars2[i + 2]);
                    triangleList2.Add(bars2[i + 3]);
                }
                for (int i = 0; i < bars3.Count - 2; i += 2)
                {
                    triangleList3.Add(bars3[i]);
                    triangleList3.Add(bars3[i + 2]);
                    triangleList3.Add(bars3[i + 1]);
                    triangleList3.Add(bars3[i + 1]);
                    triangleList3.Add(bars3[i + 2]);
                    triangleList3.Add(bars3[i + 3]);
                }
                for (int i = 0; i < bars4.Count - 2; i += 2)
                {
                    triangleList4.Add(bars4[i]);
                    triangleList4.Add(bars4[i + 2]);
                    triangleList4.Add(bars4[i + 1]);
                    triangleList4.Add(bars4[i + 1]);
                    triangleList4.Add(bars4[i + 2]);
                    triangleList4.Add(bars4[i + 3]);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(5 / 6f, 1, 0.5f).ToVector4());
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList1.ToArray(), 0, triangleList1.Count / 3);
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList2.ToArray(), 0, triangleList2.Count / 3);
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList3.ToArray(), 0, triangleList3.Count / 3);
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList4.ToArray(), 0, triangleList4.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            List<CustomVertexInfo> barsh = new List<CustomVertexInfo>();
            if (Time < 180)
            {
                for (int i = 1; i < 100; i++)
                {
                    float width = (float)(70 * Math.Sin(MathHelper.Pi * Math.Sqrt(Time / 180)));
                    var normalDir = posH[i - 1] - posH[i];
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                    barsh.Add(new CustomVertexInfo(posH[i] + new Vector2(0, 48).RotatedBy(projectile.ai[1] * MathHelper.PiOver2) + normalDir * width, new Vector3((i / 100f - 0.5f) * 3 / 4 + 0.5f, 15 / 16f, 1)));
                    barsh.Add(new CustomVertexInfo(posH[i] + new Vector2(0, 48).RotatedBy(projectile.ai[1] * MathHelper.PiOver2) + normalDir * -width, new Vector3((i / 100f - 0.5f) * 3 / 4 + 0.5f, 1, 1)));
                }
            }
            List<CustomVertexInfo> triangleListh = new List<CustomVertexInfo>();
            if (Time < 180)
            {
                for (int i = 0; i < barsh.Count - 2; i += 2)
                {
                    triangleListh.Add(barsh[i]);
                    triangleListh.Add(barsh[i + 2]);
                    triangleListh.Add(barsh[i + 1]);
                    triangleListh.Add(barsh[i + 1]);
                    triangleListh.Add(barsh[i + 2]);
                    triangleListh.Add(barsh[i + 3]);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(5 / 6f, 1, 0.5f).ToVector4());
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");//IllusionBoundMod.LaserTex[(int)(Time / 4) % 4]
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleListh.ToArray(), 0, triangleListh.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.immuneTime = 15;
            target.immune = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            bool hiting = false;
            for (int n = 1; n < 121; n++)
            {
                Vector2 vec1 = GetVec(posG[n]);
                Vector2 vec2 = GetVec(posP[n]);
                hiting |= new Rectangle((int)(posG[n].X - 4 + projectile.Center.X), (int)(posG[n].Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox);
                hiting |= new Rectangle((int)(vec1.X - 4 + projectile.Center.X), (int)(vec1.Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox);
                hiting |= new Rectangle((int)(posP[n].X - 4 + projectile.Center.X), (int)(posP[n].Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox);
                hiting |= new Rectangle((int)(vec2.X - 4 + projectile.Center.X), (int)(vec2.Y - 4 + projectile.Center.Y), 8, 8).Intersects(targetHitbox);
            }
            if (Time < 180)
            {
                Rectangle rectangle;
                int width = (int)(70 * Math.Sin(MathHelper.Pi * Math.Sqrt(Time / 180)));
                if ((int)projectile.ai[1] % 2 == 0)
                {
                    rectangle = new Rectangle((int)projectile.Center.X - width / 2, (int)projectile.Center.Y - ((int)projectile.ai[1] == 0 ? 4800 : 0), width, 4800);
                }
                else
                {
                    rectangle = new Rectangle((int)projectile.Center.X - ((int)projectile.ai[1] == 3 ? 4800 : 0), (int)projectile.Center.Y - width / 2, 4800, width);
                }
                hiting |= rectangle.Intersects(targetHitbox);
            }
            return hiting;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense < 20 ? target.defense / 2 : 10;
        }
        public override void AI()
        {
            Time++;
            for (float n = 0; n < 121; n++)
            {
                float x = Math.Clamp(Time - n, 0, 180);
                posG[(int)n] = new Vector2(4 * (float)Math.Sin(Math.Sqrt(x + 10)), -x).RotatedBy(projectile.ai[1] * MathHelper.PiOver2) * 20f;
                //posG2[(int)n] = projectile.Center - new Vector2(4 * (float)Math.Sin(Math.Sqrt(x + 10)), +x) * 20f;
                posP[(int)n] = new Vector2(-8 * ((float)Math.Sin(0.05f * x) * 60 / x + (float)Math.Sqrt(0.05d * x) * 2 - 3), -x).RotatedBy(projectile.ai[1] * MathHelper.PiOver2) * 20f;
                //posP2[(int)n] = projectile.Center + new Vector2(8 * ((float)Math.Sin(0.05f * x) * 60 / x + (float)Math.Sqrt(0.05d * x) * 2 - 3), -x) * 20f;
                if (n % 5 == 0)
                {
                    Lighting.AddLight(posG[(int)n] + projectile.Center, new Vector3(107, 206, 107) / 255f);
                    Lighting.AddLight(GetVec(posG[(int)n]) + projectile.Center, new Vector3(107, 206, 107) / 255f);
                    Lighting.AddLight(posP[(int)n] + projectile.Center, new Vector3(206, 107, 206) / 255f);
                    Lighting.AddLight(GetVec(posP[(int)n]) + projectile.Center, new Vector3(206, 107, 206) / 255f);
                }
            }
            for (float n = 0; n < 100; n++)
            {
                if (Time == 1)
                {
                    posH[(int)n] = projectile.Center + new Vector2(0, -48 * n).RotatedBy(projectile.ai[1] * MathHelper.PiOver2);
                }

                if (n % 3 == 0 && Time < 180)
                {
                    Lighting.AddLight(posH[(int)n], new Vector3(206, 107, 206) / 255f);
                }
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("星云「杀意的百合」");
        }
    }
    public class VortexLightningIN : InfiniteNightmareProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("星旋闪电");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hostile = true;
            //this.hide = true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                int num958 = Player.FindClosest(projectile.Center, 0, 0);
                Vector2 vector118 = Main.player[num958].Center - projectile.Center;
                if (vector118 == Vector2.Zero)
                {
                    vector118 = Vector2.UnitY;
                }
                projectile.ai[1] = vector118.ToRotation();
                projectile.netUpdate = true;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] <= 50f)
            {
                if (Main.rand.NextBool(2))
                {
                    Vector2 vector121 = projectile.ai[1].ToRotationVector2();
                    Vector2 vector122 = vector121.RotatedBy(1.5707963705062866, default) * (Main.rand.NextBool(2)).ToDirectionInt() * Main.rand.Next(10, 21);
                    Vector2 value49 = vector121 * Main.rand.Next(-80, 81);
                    Vector2 vector123 = value49 - vector122;
                    vector123 /= 10f;
                    int num959 = 229;
                    Dust dust110 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, num959, 0f, 0f, 0, default, 1f)];
                    dust110.noGravity = true;
                    dust110.position = projectile.Center + vector122;
                    dust110.velocity = vector123;
                    dust110.scale = 0.5f + Main.rand.NextFloat();
                    dust110.fadeIn = 0.5f;
                    value49 = vector121 * Main.rand.Next(40, 121);
                    vector123 = value49 - vector122 / 2f;
                    vector123 /= 10f;
                    dust110 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, num959, 0f, 0f, 0, default, 1f)];
                    dust110.noGravity = true;
                    dust110.position = projectile.Center + vector122 / 2f;
                    dust110.velocity = vector123;
                    dust110.scale = 1f + Main.rand.NextFloat();
                    return;
                }
            }
            else if (projectile.ai[0] <= 90f)
            {
                projectile.scale = (projectile.ai[0] - 50f) / 40f;
                projectile.alpha = 255 - (int)(255f * projectile.scale);
                projectile.rotation -= 0.157079637f;
                Vector2 vector126 = projectile.ai[1].ToRotationVector2();
                Vector2 value50 = vector126.RotatedBy(1.5707963705062866, default) * (Main.rand.NextBool(2)).ToDirectionInt() * Main.rand.Next(10, 21);
                vector126 *= Main.rand.Next(-80, 81);
                Vector2 vector127 = vector126 - value50;
                vector127 /= 10f;
                int num960 = Terraria.Utils.SelectRandom(Main.rand, new int[]
                {
                                                                                                        229,
                                                                                                        229
                });
                Dust dust113 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, num960, 0f, 0f, 0, default, 1f)];
                dust113.noGravity = true;
                dust113.position = projectile.Center + value50;
                dust113.velocity = vector127;
                dust113.scale = 0.5f + Main.rand.NextFloat();
                dust113.fadeIn = 0.5f;
                if (projectile.ai[0] == 90f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vector128 = projectile.ai[1].ToRotationVector2() * 8f;
                    float ai2 = Main.rand.Next(80);
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X - vector128.X, projectile.Center.Y - vector128.Y, vector128.X, vector128.Y, ProjectileID.VortexLightning, projectile.damage, 1f, Main.myPlayer, projectile.ai[1], ai2);
                    return;
                }
            }
            else
            {
                if (projectile.ai[0] > 120f)
                {
                    projectile.scale = 1f - (projectile.ai[0] - 120f) / 60f;
                    projectile.alpha = 255 - (int)(255f * projectile.scale);
                    projectile.rotation -= 0.104719758f;
                    if (projectile.alpha >= 255)
                    {
                        projectile.Kill();
                    }
                    int num3;
                    for (int num963 = 0; num963 < 2; num963 = num3 + 1)
                    {
                        int num964 = Main.rand.Next(3);
                        if (num964 == 0)
                        {
                            Vector2 vector129 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * projectile.scale;
                            Dust dust114 = Main.dust[Dust.NewDust(projectile.Center - vector129 * 30f, 0, 0, 229, 0f, 0f, 0, default, 1f)];
                            dust114.noGravity = true;
                            dust114.position = projectile.Center - vector129 * Main.rand.Next(10, 21);
                            dust114.velocity = vector129.RotatedBy(1.5707963705062866, default) * 6f;
                            dust114.scale = 0.5f + Main.rand.NextFloat();
                            dust114.fadeIn = 0.5f;
                            dust114.customData = projectile.Center;
                        }
                        else if (num964 == 1)
                        {
                            Vector2 vector130 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * projectile.scale;
                            Dust dust115 = Main.dust[Dust.NewDust(projectile.Center - vector130 * 30f, 0, 0, 240, 0f, 0f, 0, default, 1f)];
                            dust115.noGravity = true;
                            dust115.position = projectile.Center - vector130 * 30f;
                            dust115.velocity = vector130.RotatedBy(-1.5707963705062866, default) * 3f;
                            dust115.scale = 0.5f + Main.rand.NextFloat();
                            dust115.fadeIn = 0.5f;
                            dust115.customData = projectile.Center;
                        }
                        num3 = num963;
                    }
                    return;
                }
                projectile.scale = 1f;
                projectile.alpha = 0;
                projectile.rotation -= 0.05235988f;
                if (Main.rand.NextBool(2))
                {
                    Vector2 vector131 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                    Dust dust116 = Main.dust[Dust.NewDust(projectile.Center - vector131 * 30f, 0, 0, DustID.Vortex, 0f, 0f, 0, default, 1f)];
                    dust116.noGravity = true;
                    dust116.position = projectile.Center - vector131 * Main.rand.Next(10, 21);
                    dust116.velocity = vector131.RotatedBy(1.5707963705062866, default) * 6f;
                    dust116.scale = 0.5f + Main.rand.NextFloat();
                    dust116.fadeIn = 0.5f;
                    dust116.customData = projectile.Center;
                    return;
                }
                Vector2 vector132 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust117 = Main.dust[Dust.NewDust(projectile.Center - vector132 * 30f, 0, 0, 240, 0f, 0f, 0, default, 1f)];
                dust117.noGravity = true;
                dust117.position = projectile.Center - vector132 * 30f;
                dust117.velocity = vector132.RotatedBy(-1.5707963705062866, default) * 3f;
                dust117.scale = 0.5f + Main.rand.NextFloat();
                dust117.fadeIn = 0.5f;
                dust117.customData = projectile.Center;
                return;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Color color25 = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            if (projectile.type == 681 && projectile.velocity.X > 0f)
            {
                spriteEffects ^= SpriteEffects.FlipHorizontally;
            }
            Vector2 vector60 = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D31 = TextureAssets.Projectile[projectile.type].Value;
            Color alpha4 = projectile.GetAlpha(color25);
            Vector2 origin8 = new Vector2(texture2D31.Width, texture2D31.Height) / 2f;
            Color color55 = alpha4 * 0.8f;
            color55.A /= 2;
            Color color56 = Color.Lerp(alpha4, Color.Black, 0.5f);
            color56.A = alpha4.A;
            float num278 = 0.95f + (projectile.rotation * 0.75f).ToRotationVector2().Y * 0.1f;
            color56 *= num278;
            float scale12 = 0.6f + projectile.scale * 0.6f * num278;
            SpriteBatch spriteBatch26 = Main.spriteBatch;
            Texture2D texture26 = TextureAssets.Extra[50].Value;
            Vector2 position31 = vector60;
            Rectangle? sourceRectangle2 = null;
            spriteBatch26.Draw(texture26, position31, sourceRectangle2, color56, -projectile.rotation + 0.35f, origin8, scale12, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
            SpriteBatch spriteBatch27 = Main.spriteBatch;
            Texture2D texture27 = TextureAssets.Extra[50].Value;
            Vector2 position32 = vector60;
            sourceRectangle2 = null;
            spriteBatch27.Draw(texture27, position32, sourceRectangle2, alpha4, -projectile.rotation, origin8, projectile.scale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
            SpriteBatch spriteBatch28 = Main.spriteBatch;
            Texture2D texture28 = texture2D31;
            Vector2 position33 = vector60;
            sourceRectangle2 = null;
            spriteBatch28.Draw(texture28, position33, sourceRectangle2, color55, -projectile.rotation * 0.7f, origin8, projectile.scale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
            SpriteBatch spriteBatch29 = Main.spriteBatch;
            Texture2D texture29 = TextureAssets.Extra[50].Value;
            Vector2 position34 = vector60;
            sourceRectangle2 = null;
            spriteBatch29.Draw(texture29, position34, sourceRectangle2, alpha4 * 0.8f, projectile.rotation * 0.5f, origin8, projectile.scale * 0.9f, spriteEffects, 0f);
            alpha4.A = 0;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class StarDustLaser : InfiniteNightmareProjectile
    {
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            return projectile.ai[0] <= 120 && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.velocity * 1440f + projectile.Center, 32 * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(projectile.ai[0] / 120f)), ref point);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("星尘光束");
        }
        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.aiStyle = -1;
            projectile.width = 1;
            projectile.height = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] > 180f)
            {
                return false;
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            for (int i = 0; i <= 1440; i += 10)
            {
                var factor = i / 1440f;
                var normalDir1 = -projectile.velocity;
                normalDir1 = Vector2.Normalize(new Vector2(-normalDir1.Y, normalDir1.X));
                if (projectile.ai[0] > 120f)
                {
                    bars.Add(new CustomVertexInfo(projectile.velocity * i + projectile.Center + normalDir1 * 4, Color.White, new Vector3((factor / 100f - 0.5f) * 3 / 4 + 0.5f, 1 / 16f, 0.25f)));
                    bars.Add(new CustomVertexInfo(projectile.velocity * i + projectile.Center + normalDir1 * -4, Color.White, new Vector3((factor / 100f - 0.5f) * 3 / 4 + 0.5f, 0, 0.25f)));
                }
                else
                {
                    bars.Add(new CustomVertexInfo(projectile.velocity * i + projectile.Center + normalDir1 * 16 * (float)Math.Pow(i + 1, 1 / 4f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.ai[0] / 120f)), Color.White, new Vector3((factor / 100f - 0.5f) * 3 / 4 + 0.5f, 1 / 16f, 2 * factor * (1 - factor) + 0.125f)));
                    bars.Add(new CustomVertexInfo(projectile.velocity * i + projectile.Center + normalDir1 * -16 * (float)Math.Pow(i + 1, 1 / 4f) * (float)Math.Sin(MathHelper.Pi * Math.Sqrt(1 - projectile.ai[0] / 120f)), Color.White, new Vector3((factor / 100f - 0.5f) * 3 / 4 + 0.5f, 0, 2 * factor * (1 - factor) + 0.125f)));
                }

            }
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            if (bars.Count > 2)
            {
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
                IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
                IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Main.hslToRgb(projectile.ai[1], 1, 0.5f).ToVector4());
                Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.GetTexture("Images/laser1");
                Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.GetTexture("Images/laser1");
                Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void AI()
        {
            projectile.ai[0]--;
            if (projectile.ai[0] <= 0)
            {
                projectile.Kill();
            }
        }
    }
    //buff类
    public class DoThingsSloppily : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("拖泥带水");
            Description.SetDefault("字面意思。\n移动速度降低，淋雨淋湿了做事效率会变低呢，小心感冒（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.velocity = new Vector2(player.velocity.X * 0.95f, player.velocity.Y);
            Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.Water, new Vector2(0, 0), 127, Color.White, 1f);
            d2.noGravity = true;
        }
    }
    public class DoThingsReallySloppily : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("拖泥带水Ⅱ");
            Description.SetDefault("字面意思。\n移动速度明显降低，这次是真的\"落汤\"鸡了，我欣赏你掉到水坑里挣扎的样子（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.velocity = new Vector2(player.velocity.X * 0.925f, player.velocity.Y);
            for (int n = 0; n < 3; n++)
            {
                Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.Water, new Vector2(0, 0), 127, Color.White, 1f);
                d2.noGravity = true;
            }
        }
    }
    public class Hypoxia : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("缺氧");
            Description.SetDefault("不及时补充氧气的话，不用多久就会暴毙哦。（坏笑");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.breath -= 3;
            player.breath -= (Main.time % 9 < 1) ? 2 : 0;
            //for (int n = 0; n < 3; n++)
            //{
            //	Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.WhiteClouds, new Vector2(0, 0), 127, Color.White, 1f);
            //	d2.noGravity = true;
            //}
        }
    }
    public class Radiation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("辐射");//雷云浮岛
            Description.SetDefault("生命和魔力降低\n为什么风暴之地的环境debuff是这个？感谢你的提问，我也想知道（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            if (player.manaRegen > 0)
            {
                player.manaRegen = 0;
            }
            player.manaRegenDelay = 60;
            player.manaRegenCount = 0;
            player.nebulaManaCounter = 0;
            player.lifeRegen -= 6;
            //player.manaRegen -= 8;
            //player.statLife -= (Main.time % 20 < 1) ? 1 : 0;
            player.statMana -= (Main.time % 15 < 1) ? 1 : 0;
            for (int n = 0; n < 3; n++)
            {
                Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.ElectricCyan, new Vector2(0, 0), 127, Color.White, 1f);
                d2.noGravity = true;
            }
        }
    }
    public class LowTemperature : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("低温");
            Description.SetDefault("你只能通过消耗魔力来维持体温了，当你魔力耗尽，你也就会感到一种霜冻般的绝望吧。（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            if (player.manaRegen > 0)
            {
                player.manaRegen = 0;
            }
            player.manaRegenCount = 0;
            player.manaRegenDelay = 60;
            player.nebulaManaCounter = 0;
            if (player.statMana > 0)
            {
                //player.manaRegen -= 12;
                player.statMana -= (Main.time % 10 < 1) ? 1 : 0;
            }
            else
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 8;
                //player.statLife -= (Main.time % 15 < 1) ? 1 : 0;
            }
            Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.Snow, new Vector2(0, 0), 127, Color.White, 1f);
            d2.noGravity = true;
        }
    }
    public class HighTemperature : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("高温");
            Description.SetDefault("并没有用魔力直接降温的手段哦，这样的温度能还原氧化铁了吧，钢铁就是这么炼成的。（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 8;
            //player.statLife -= (Main.time % 15 < 1) ? 1 : 0;
            Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.Fire, new Vector2(0, 0), 127, Color.White, 1f);
            d2.noGravity = true;
        }
    }
    public class TooDry : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("干旱");
            Description.SetDefault("口渴的你，手也软了吧。然而喝那么几壶水并不足以让整个沙漠解渴呢。（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            int n = 8;
            player.GetCritChance(DamageClass.Generic) -= n;
            player.GetDamage(DamageClass.Generic) -= 0.4f;
        }
    }
    public class HotWithWet : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("湿热");
            Description.SetDefault("潮湿与温热并存，你为此而焦躁不安，越是心急越是走不快呢，也没能集中注意力防备或回击怪物了。（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.statDefense -= 4;
            player.GetDamage(DamageClass.Generic) -= 0.1f;
            player.velocity = new Vector2(player.velocity.X * 0.95f, player.velocity.Y);
        }
    }
    public class TooDark : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("黑暗");
            Description.SetDefault("那些阴险的家伙隐匿在黑暗中，你看不见你前程的光明。（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            InfiniteNightmarePlayer.TooDarkBuffActive = true;
        }

    }
    public class DarkNPC : GlobalNPC
    {
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (npc.target == -1 || Main.gameMenu)
            {
                return drawColor;
            }
            Player player = Main.LocalPlayer;//Main.player[npc.target];
            if (!player.HasBuff(BuffType<TooDark>()))
            {
                return drawColor;
            }

            float distance = (npc.Center - player.Center).Length();
            float value = Math.Min(80 / distance, 1);
            if (distance > 640)
            {
                value = 0;
            }
            return drawColor * value;
        }
    }
    public class TooQuiet : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("寂静");
            Description.SetDefault("周围静的可怕，你有一种孤独的恐惧，甚至对于周围放下了防备。（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.GetModPlayer<InfiniteNightmarePlayer>().TooQuietBuffActive = true;
            player.statDefense -= 4;
        }
    }
    public class Gravitation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("引力");
            Description.SetDefault("你开始好奇眼前那天界塔的密度了，为什么它会有这么大引力。而且离得太远或太近引力都不大\n我知道了，那其实是天界塔碎片对你的吸引力（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            float MaxD = 4000f * 1.414213562373095f;
            NPC N = null;
            foreach (NPC n in Main.npc)
            {
                if ((n.type == NPCID.LunarTowerSolar || n.type == NPCID.LunarTowerVortex || n.type == NPCID.LunarTowerNebula || n.type == NPCID.LunarTowerStardust) && n.active)
                {
                    float D = (player.Center - n.Center).Length();
                    if (D < MaxD)
                    {
                        MaxD = D;
                        N = n;
                    }
                }
            }
            if (N != null)
            {
                Vector2 vec = N.Center - player.Center;
                float D = vec.Length();
                float unitV = (float)Math.Sin(MathHelper.Pi / 32000000 * D * D);
                vec.Normalize();
                if (unitV > 0)
                {
                    if (GetBottomTile(player))
                    {
                        player.Center += new Vector2(vec.X * unitV * 16, 0);
                    }
                    else
                    {
                        player.Center += vec * unitV * 16;
                    }
                }
            }
        }
        private int SixteenF(float n)
        {
            return (int)(n / 16);
        }
        private bool GetBottomTile(Player player)
        {
            bool flag1 = Main.tile[SixteenF(player.position.X), SixteenF(player.position.Y) + 3] != null;
            bool flag2 = Main.tile[SixteenF(player.position.X) + 1, SixteenF(player.position.Y) + 3] != null;
            return flag1 || flag2;
        }
    }
    public class SwimInTheLava : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("在岩浆中游泳");
            Description.SetDefault("淦哦老兄，为什么要做这种奇怪的尝试？你的皮肤又不是黑曜石做的，但岩浆里那些鱼是啊。子非鱼，安知鱼之热（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 120;
            //player.statLife -= (Main.time % 15 < 1) ? 1 : 0;
            Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.Fire, new Vector2(0, 0), 127, Color.White, 1f);
            d2.noGravity = true;
        }
    }
    public class TooSweet : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("过分甜蜜");
            Description.SetDefault("比秘封组还甜的蜂蜜令你不由得扭得像条蛆一样，就连敌对的弹幕也被蜂蜜吸引了！（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.velocity = new Vector2(player.velocity.X * 0.9f, player.velocity.Y);
            player.GetModPlayer<InfiniteNightmarePlayer>().TooSweetBuffActive = true;
            Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.YellowGems, new Vector2(0, 0), 127, Color.White, 1f);
            d2.noGravity = true;
        }
    }
    public class TooSweetProj : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            if (projectile.hostile)
            {
                Player player = null;
                float MaxD = 1024f;
                foreach (Player p in Main.player)
                {
                    if (p.active && p.GetModPlayer<InfiniteNightmarePlayer>().TooSweetBuffActive)
                    {
                        float currentDistance = Vector2.Distance(p.Center, projectile.Center);
                        if (currentDistance < MaxD)
                        {
                            MaxD = currentDistance;
                            player = p;
                        }
                    }
                }
                if (player != null)
                {
                    Vector2 targetVec = player.Center - projectile.Center;
                    targetVec.Normalize();
                    targetVec *= 20f / Math.Max((player.Center - projectile.Center).Length() / 64, 1);
                    projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
                }
            }
        }
    }
    public class TheLastPieceOfPureLand : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("最后一片净土");
            Description.SetDefault("纯粹地想占用你一个buff槽，气不气（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
        }
    }
    public class TooDazzling : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("刺眼");
            Description.SetDefault("那一刻，这片神圣的土地化为了光（\n暴击、生命上限、魔力上限降低了！");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            InfiniteNightmarePlayer.TooDazzlingBuffActive = true;
            int n = 2;
            player.GetCritChance(DamageClass.Generic) -= n;

        }
    }
    public class EasyToBeRusty : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("易生锈");
            Description.SetDefault("海边的金属是容易生锈的...保护好你身边的金属制品（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.statDefense -= 8;
        }
    }
    public class TooWarm : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("过于温暖");
            Description.SetDefault("有时候沉溺于温暖之中也不见得是好事（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.lifeRegenCount--;
        }
    }
    public class Infected : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("感染");
            Description.SetDefault("伤口不及时处理会恶化哦（");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 6;
            Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.PurpleBubble, new Vector2(0, 0), 127, Color.White, 1f);
            d2.noGravity = true;
        }
    }
    //饰品类
    public abstract class ImmuneAccS : InfiniteNightmareItem
    {
        protected string tooltip;
        protected string name;
        protected int index;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tooltip);
            DisplayName.SetDefault(name);
        }
        public override void SetDefaults()
        {
            item.rare = 2;
            item.accessory = true;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.defense = index == 6 ? 3 : 0;
        }
        public virtual void OtherSets(Player player)
        {

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<InfiniteNightmarePlayer>().ImmuneType[index] = true;
            if (index <= 5 && !hideVisual)
            {
                player.GetModPlayer<InfiniteNightmarePlayer>().VisuableBackPack[index] = true;
            }
            item.width = TextureAssets.Item[item.type].Width();
            item.height = TextureAssets.Item[item.type].Height();
            OtherSets(player);
        }
    }
    public class WarmBackpack : ImmuneAccS
    {
        public WarmBackpack()
        {
            name = "暖心背包升级模块";
            tooltip = "背后的原因令你暖心。";
            index = 0;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.CopperBarRG, 10);
            recipe1.AddRecipeGroup(IllusionBoundModSystem.TorchRG, 15);
            recipe1.AddRecipeGroup("Wood", 20);
            recipe1.AddIngredient(ItemID.Gel, 30);
            recipe1.AddTile(TileID.WorkBenches);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
        public override void OtherSets(Player player)
        {
            Lighting.AddLight(player.Center, Main.hslToRgb(0.15f, 0.75f, 0.5f).ToVector3());
        }
    }
    public class OxygenBackpack : ImmuneAccS
    {
        public OxygenBackpack()
        {
            name = "氧气背包升级模块";
            tooltip = "一株仙人掌足以产生临时需要的氧气吗？";
            index = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup("IronBar", 10);
            recipe1.AddRecipeGroup("Sand", 5);
            recipe1.AddIngredient(ItemID.Glass, 5);
            recipe1.AddIngredient(ItemID.WaterBucket);
            recipe1.AddIngredient(ItemID.Cactus, 10);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class CoolBackpack : ImmuneAccS
    {
        public CoolBackpack()
        {
            name = "降温背包升级模块";
            tooltip = "你需要冷静冷静。";
            index = 2;
        }
        public override void OtherSets(Player player)
        {
            player.manaRegen += 3;
            player.moveSpeed += 0.3f;
            player.maxRunSpeed += 0.6f;
            player.runAcceleration += 0.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.SilverBarRG, 10);
            recipe1.AddIngredient(ItemID.IceBlock, 20);
            recipe1.AddIngredient(ItemID.SnowBlock, 15);
            recipe1.AddIngredient(ItemID.Shiverthorn, 5);
            recipe1.AddTile(TileID.IceMachine);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class DryBackpack : ImmuneAccS
    {
        public DryBackpack()
        {
            name = "干燥背包升级模块";
            tooltip = "在雨林也能保持干爽什么的真是太好了。";
            index = 3;
        }
        public override void OtherSets(Player player)
        {
            player.lifeRegen += 3;
            player.moveSpeed += 0.2f;
            player.maxRunSpeed += 0.4f;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.SilverBarRG, 15);
            recipe1.AddIngredient(ItemID.Waterleaf, 5);
            recipe1.AddIngredient(ItemID.Blinkroot, 5);
            recipe1.AddTile(TileID.HeavyWorkBench);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class WetBackpack : ImmuneAccS
    {
        public WetBackpack()
        {
            name = "加湿背包升级模块";
            tooltip = "滋润你自己和你身边。";
            index = 4;
        }
        public override void OtherSets(Player player)
        {
            player.lifeRegen += 3;
            player.manaRegen += 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.GoldBarRG, 5);
            recipe1.AddIngredient(ItemID.BottledWater, 15);
            recipe1.AddIngredient(ItemID.WaterBucket, 3);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class LightBackpack : ImmuneAccS
    {
        public LightBackpack()
        {
            name = "光明背包升级模块";
            tooltip = "你逐渐看清一切。";
            index = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.GoldBarRG, 15);
            recipe1.AddIngredient(ItemID.Blinkroot, 10);
            recipe1.AddIngredient(ItemID.Daybloom, 10);
            recipe1.AddIngredient(ItemID.Moonglow, 10);
            recipe1.AddIngredient(ItemID.Sunflower, 5);
            recipe1.AddIngredient(ItemID.SunplateBlock, 25);
            recipe1.AddTile(TileID.SkyMill);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
        public override void OtherSets(Player player)
        {
            for (int n = 0; n < 3; n++)
            {
                Lighting.AddLight(player.Center + (MathHelper.TwoPi / 3 * n + (float)Main.time / 60 * MathHelper.TwoPi).ToRotationVector2() * 32, Main.hslToRgb(0.15f, 0.75f, 1f).ToVector3());

            }
        }
    }
    [AutoloadEquip(EquipType.Shield)]
    public class LeadShield : ImmuneAccS
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            name = "铅盾";
            tooltip = "这样的一面盾，竟然能有挡下辐射的神力！可谓中用不中看。";
            index = 6;
            item.defense = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup("IronBar", 20);
            recipe1.AddIngredient(ItemID.Deathweed, 10);
            recipe1.AddIngredient(ItemID.Obsidian, 20);
            recipe1.AddTile(TileID.DemonAltar);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    [AutoloadEquip(EquipType.Waist)]
    public class PureBottle : ImmuneAccS
    {
        public PureBottle()
        {
            name = "纯净水";
            tooltip = "洗涤并净化你的灵魂";
            index = 7;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.CopperBarRG, 5);
            recipe1.AddIngredient(ItemID.Blinkroot, 5);
            recipe1.AddIngredient(ItemID.BottledWater, 10);
            recipe1.AddIngredient(ItemID.PurificationPowder, 25);
            recipe1.AddTile(TileID.Bottles);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class NoisySlimeBox : ImmuneAccS
    {
        public NoisySlimeBox()
        {
            name = "噪音盒子";
            tooltip = "听上去噪音史莱姆缩到这个盒子里面去了\n在这个盒子的约束之下，它发出的声音也没有噪音那么烦人了，至少你不会在如底下般死寂的地方因没有什么声音而不安了。";
            index = 8;
        }
    }
    public class MetalPaint : ImmuneAccS
    {
        public MetalPaint()
        {
            name = "防锈涂层";
            tooltip = "它附在你的盔甲上，光明之下显出金色辉光。";
            index = 9;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(IllusionBoundModSystem.CopperBarRG, 10);
            recipe1.AddRecipeGroup("IronBar", 5);
            recipe1.AddIngredient(ItemID.EmptyBucket);
            recipe1.AddRecipeGroup(IllusionBoundModSystem.PaintRG, 25);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    //npc类
    public class NoisySlime : ModNPC
    {
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return IllusionBoundMod.UnderGroundActive ? 0.1f : 0;
        }

        private NPC npc => NPC;
        public override void SetDefaults()
        {
            npc.npcSlots = 1f;
            npc.width = 36;
            npc.height = 24;
            npc.aiStyle = -1;
            npc.damage = 24;
            npc.defense = 8;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.alpha = 60;
            npc.value = 150f;
            //npc.scale = 1.25f;
            npc.knockBackResist = 0.6f;
            npc.rarity = 1;
        }
        private float startHeight;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("噪音史莱姆");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void AI()
        {
            NoisySlimeAI();
        }
        private void NoiseAttack(Vector2 vector = default, int dmg = 0, float scale = 1)
        {
            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + vector, vector, ProjectileType<SlimeNoise>(), npc.damage + dmg, 1, Main.myPlayer, scale);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int num5;
            if (npc.life > 0)
            {
                int num259 = 0;
                while (num259 < damage / npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, npc.alpha, npc.color, 1f);
                    num5 = num259;
                    num259 = num5 + 1;
                }
                NoiseAttack(dmg: Main.rand.Next(4));
            }
            else
            {
                for (int num260 = 0; num260 < 50; num260 = num5 + 1)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, 2 * hitDirection, -2f, npc.alpha, npc.color, 1f);
                    num5 = num260;
                }
                for (int n = 0; n < 5; n++)
                {
                    NoiseAttack(new Vector2(1, 0).RotatedBy(MathHelper.TwoPi / 5 * n), Main.rand.Next(4));
                }
                NoiseAttack(dmg: Main.rand.Next(2, 5), scale: 2);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            int num2 = 0;
            if (npc.aiAction == 0)
            {
                if (npc.velocity.Y < 0f)
                {
                    num2 = 2;
                }
                else if (npc.velocity.Y > 0f)
                {
                    num2 = 3;
                }
                else if (npc.velocity.X != 0f)
                {
                    num2 = 1;
                }
                else
                {
                    num2 = 0;
                }
            }
            else if (npc.aiAction == 1)
            {
                num2 = 4;
            }
            npc.frameCounter += 1.0;
            if (num2 > 0)
            {
                npc.frameCounter += 1.0;
            }
            if (num2 == 4)
            {
                npc.frameCounter += 1.0;
            }
            if (npc.frameCounter >= 8.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
                //NoiseAttack();
            }
        }
        private bool StartRecordH;
        private float[] VY = new float[2];
        private void NoisySlimeAI()
        {
            bool flag = false;
            if (!StartRecordH)
            {
                StartRecordH = true;
                startHeight = npc.height;
            }
            VY[1] = VY[0];
            VY[0] = npc.velocity.Y;
            if (!Main.dayTime || npc.life != npc.lifeMax || npc.position.Y > Main.worldSurface * 16.0 || Main.slimeRain)
            {
                flag = true;
            }
            if (npc.ai[2] > 1f)
            {
                npc.ai[2] -= 1f;
            }
            if (npc.wet)
            {
                if (npc.collideY)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                    npc.ai[2] = 200f;
                }
                if (npc.velocity.Y > 0f)
                {
                    npc.ai[3] = npc.position.X;
                }
                if (npc.velocity.Y > 2f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                if (npc.ai[2] == 1f && flag)
                {
                    npc.TargetClosest(true);
                }
            }
            npc.aiAction = 0;
            if (npc.ai[2] == 0f)
            {
                npc.ai[0] = -100f;
                npc.ai[2] = 1f;
                npc.TargetClosest(true);
            }
            if (npc.velocity.Y == 0f)
            {
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + npc.direction);
                }
                if (npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                    npc.ai[2] = 200f;
                }
                npc.ai[3] = 0f;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (flag)
                {
                    npc.ai[0] += 1f;
                }
                npc.ai[0] += 4f;
                int num19 = 0;
                if (npc.ai[0] >= 0f)
                {
                    num19 = 1;
                }
                if (npc.ai[0] >= -1000f && npc.ai[0] <= -500f)
                {
                    num19 = 2;
                }
                if (npc.ai[0] >= -2000f && npc.ai[0] <= -1500f)
                {
                    num19 = 3;
                }
                if (num19 > 0)
                {
                    npc.netUpdate = true;
                    if (flag && npc.ai[2] == 1f)
                    {
                        npc.TargetClosest(true);
                    }
                    if (num19 == 3)
                    {
                        npc.velocity.Y = -8f;
                        npc.velocity.X = npc.velocity.X + 3 * npc.direction;
                        npc.ai[0] = -200f;
                        npc.ai[3] = npc.position.X;
                        //NoiseAttack(dmg: 5, scale: 2);
                    }
                    else
                    {
                        npc.velocity.Y = -6f;
                        npc.velocity.X = npc.velocity.X + 2 * npc.direction;
                        npc.ai[0] = -120f;
                        if (num19 == 1)
                        {
                            npc.ai[0] -= 1000f;
                        }
                        else
                        {
                            npc.ai[0] -= 2000f;
                        }
                        //NoiseAttack();
                    }
                }
                else if (npc.ai[0] >= -30f)
                {
                    npc.aiAction = 1;
                    return;
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if (npc.collideX && Math.Abs(npc.velocity.X) == 0.2f)
                {
                    npc.position.X = npc.position.X - 1.4f * npc.direction;
                }
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + npc.direction);
                }
                if ((npc.direction == -1 && npc.velocity.X < 0.01) || (npc.direction == 1 && npc.velocity.X > -0.01))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * npc.direction;
                    return;
                }
                npc.velocity.X = npc.velocity.X * 0.93f;
            }
            if (VY[0] > 0 && VY[1] < 0)
            {
                startHeight = npc.Center.Y;
            }
            if (npc.velocity.Y == 0 && npc.velocity.Y != npc.oldVelocity.Y)
            {
                if (Math.Abs(npc.Center.Y - startHeight) > 80)
                {
                    NoiseAttack(dmg: 5, scale: 2);
                }
                else if (Math.Abs(npc.Center.Y - startHeight) > 16)
                {
                    NoiseAttack();
                }
                startHeight = npc.Center.Y;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<NoisySlimeBox>(), 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 8));
        }
    }
    public class SlimeNoise : InfiniteNightmareProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("史莱姆噪音");
        }

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.DamageType = DamageClass.Magic;
            projectile.alpha = 0;
            projectile.ignoreWater = true;
            projectile.scale = 1f;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (projectile.Center - targetHitbox.Center.ToVector2()).Length() <= 64 * projectile.ai[1] * projectile.ai[0];
        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			float x1 = (float)Math.Sqrt(projectile.ai[1]);
			float x2 = 0;
			float x3 = 0;
			if (projectile.ai[1] - (1 / 3f) > 0)
			{
				x2 = (float)Math.Sqrt(projectile.ai[1] - (1 / 3f));
			}
			if (projectile.ai[1] - (2 / 3f) > 0)
			{
				x3 = (float)Math.Sqrt(projectile.ai[1] - (2 / 3f));
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
			if (projectile.ai[0] == 2) 
			{
				DrawNoise(spriteBatch, Color.Cyan, x1 * 2);
				DrawNoise(spriteBatch, Color.Cyan, x2 * 2);
				DrawNoise(spriteBatch, Color.Cyan, x3 * 2);
				//spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.Cyan, 0, new Vector2(64, 64),x * 2, SpriteEffects.None, 0);
				return false;
			}
			//spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, Color.White, 0, new Vector2(64, 64), x, SpriteEffects.None, 0);
			DrawNoise(spriteBatch, Color.White, x1);
			DrawNoise(spriteBatch, Color.White, x2);
			DrawNoise(spriteBatch, Color.White, x3);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}*/
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            float[] x = new float[3];
            for (int n = 0; n < 3; n++)
            {
                x[n] = 0;
                float i = projectile.ai[1] - (n / 3f);
                if (i > 0)
                {
                    x[n] = (float)Math.Sqrt(i);
                }
                if (projectile.ai[0] == 2)
                {
                    DrawNoise(spriteBatch, Color.Cyan with { A = 0 } * (projectile.timeLeft / 60f), x[n] * 2);
                }
                else
                {
                    DrawNoise(spriteBatch, Color.White with { A = 0 } * (projectile.timeLeft / 60f), x[n]);
                }
            }
            return false;
        }
        private void DrawNoise(SpriteBatch spriteBatch, Color color, float scale)
        {
            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, color, 0, new Vector2(64, 64), scale, SpriteEffects.None, 0);
        }
        //public override Color? GetAlpha(Color lightColor)
        //{
        //	if (projectile.ai[0] == 2) 
        //	{
        //		return Color.Cyan;
        //	}
        //	return Color.White;
        //}
        public override void AI()
        {
            projectile.ai[1] += 1 / 60f;
        }
    }
    public class FisherofSouls_EX : InfiniteNightmareItem
    {
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.ShaderItemEffectInventory(spriteBatch, position, origin, IllusionBoundMod.GetTexture("Images/IMBellTex"), Main.hslToRgb(0.5f, 0.5f, 0.5f), scale);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.ShaderItemEffectInWorld(spriteBatch, IllusionBoundMod.GetTexture("Images/IMBellTex"), Main.hslToRgb(0.5f, 0.5f, 0.5f), rotation);
        }

        //Bug 1 正则"Terraria/(?!Image) -> "Terraria/Images/
        public override string Texture => "Terraria/Images/Item_2293";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("无间「灵魂钓手」");
            Tooltip.SetDefault("用无间之钟的力量来捕获不止是鱼的灵魂罢！");
            //ItemID.Sets.CanFishInLava[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FisherofSouls);
            recipe.AddIngredient<InfiniteNightmare>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useAnimation = 8;
            item.useTime = 8;
            item.width = 24;
            item.height = 28;
            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileID.BobberFisherOfSouls;
            item.fishingPole = 20;
            item.shootSpeed = 13f;
            item.rare = 11;
        }
        public override void HoldItem(Player player)
        {
            player.accFishingLine = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int n = 0; n < 30; n++)
            {
                Projectile.NewProjectile(source, position, velocity + Main.rand.NextVector2Unit() * 16, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}