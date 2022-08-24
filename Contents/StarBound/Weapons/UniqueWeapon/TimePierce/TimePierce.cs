using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.TimePierce
{
    public class TimePierce : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("斩断那呼啸的时之风。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("刺穿时间(未完成)");
        }
        public Item item => Item;
        public override void SetDefaults()
        {
            item.damage = 175;
            item.DamageType = DamageClass.Melee;
            item.width = 40;
            item.height = 68;
            item.rare = MyRareID.Tier2;
            item.useTime = 13;
            item.useAnimation = 13;
            item.knockBack = 8;
            item.useStyle = ItemUseStyleID.Swing;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 10);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.GetGlobalNPC<TimePierceStopNPC>().stopCount = (int)(damage * Main.rand.NextFloat(0.85f, 1.15f) * .2f);
            if (player.altFunctionUse == 2)
            {
                foreach (var npc in Main.npc)
                {
                    if (npc.active) npc.GetGlobalNPC<TimePierceStopNPC>().stopCount = (int)(damage * Main.rand.NextFloat(0.85f, 1.15f) * .2f);
                }
            }
            //player.GetModPlayer<TimePierceStopPlayer>().stopCount += (int)(damage * Main.rand.NextFloat(0.85f, 1.15f) * .2f);
        }
        public override bool AltFunctionUse(Player player) => true;

        //public override bool AltFunctionUse(Player player)
        //{
        //    return player.ownedProjectileCounts[ProjectileType<TimePierceProj>()] < 1;
        //}
    }
    public class TimePierceEX : TimePierce
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("斩断那呼啸的时之风。\n但朝花也只能于夕拾。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("刺穿时间EX");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 325;
            item.width = 67;
            item.rare = MyRareID.Tier3;
            item.useTime = 10;
            item.useAnimation = 10;
            item.value = Item.sellPrice(0, 50);
        }
        public override void AddRecipes()
        {
        }
    }
    //public class TimePierceProj : VertexHammerProj
    //{
    //    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    //    {
    //        target.GetGlobalNPC<TimePierceStopNPC>().stopCount += (int)(damage * Main.rand.NextFloat(0.85f, 1.15f) * .2f);
    //    }
    //    public override void OnHitPlayer(Player target, int damage, bool crit)
    //    {
    //        target.GetModPlayer<TimePierceStopPlayer>().stopCount += (int)(damage * Main.rand.NextFloat(0.85f, 1.15f) * .2f);
    //    }
    //}
    public class TimePierceStopNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int stopCount;
        public bool NotStop => stopCount <= 0;

        public override bool PreAI(NPC npc)
        {
            stopCount--;
            if (NotStop) return true;
            else
            {
                npc.velocity = default;
                return false;
            }
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot) => NotStop;
        public override bool? CanHitNPC(NPC npc, NPC target) => NotStop;
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NotStop) return;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NotStop) return true;
            if (ColorChange == null) return true;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.instance.GraphicsDevice.Textures[1] = IllusionBoundMod.AniTexes[4];
            ColorChange.Parameters["uTime"].SetValue(-0.03f * (float)IllusionBoundModSystem.ModTime);
            ColorChange.CurrentTechnique.Passes[0].Apply();
            return true;
        }
        Effect colorChange;
        Effect ColorChange => colorChange ??= Request<Effect>("VirtualDream/Effects/ColorChange").Value;
    }
    public class TimePierceStopPlayer : ModPlayer
    {
        public int stopCount;
        public bool NotStop => stopCount <= 0;
        public override bool PreItemCheck()
        {
            stopCount--;
            if (NotStop) return true;
            else
            {
                Player.itemAnimation = 0;
                return false;
            }
        }
        public override bool? CanAutoReuseItem(Item item) => NotStop;
        public override bool CanBuyItem(NPC vendor, Item[] shopInventory, Item item) => NotStop;
        public override bool? CanCatchNPC(NPC target, Item item) => NotStop;
        public override bool CanConsumeAmmo(Item weapon, Item ammo) => NotStop;
        public override bool? CanConsumeBait(Item bait) => NotStop;
        //public override bool? CanHitNPC(Item item, NPC target) => NotStop;
        //public override bool CanHitPvp(Item item, Player target) => NotStop;
        public override bool CanSellItem(NPC vendor, Item[] shopInventory, Item item) => NotStop;
        public override bool CanShoot(Item item) => NotStop;
        public override bool CanUseItem(Item item) => NotStop;
        public override void PreUpdateMovement()
        {
            if (!NotStop)
            {
                if (System.Math.Abs(Player.velocity.Y) > 0.01f) Player.velocity.Y = 0;
                Player.velocity.X = 0;
            }
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (NotStop || Main.gameMenu) return;
            var vec = new Vector3(0.6f, 0.4f, 0.8f);
            vec = Vector3.Dot(vec, new Vector3(r, g, b)) / vec.LengthSquared() * vec;
            r = vec.X;
            g = vec.Y;
            b = vec.Z;
        }
    }
}