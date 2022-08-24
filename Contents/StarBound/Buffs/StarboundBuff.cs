using Terraria.ID;

namespace VirtualDream.Contents.StarBound.Buffs
{
    public class ToxicⅠ : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("剧毒Ⅰ");
            Description.SetDefault("你的生命正在以百分比的形式下降。这意味着你的血量和防御再怎么高也没有意义。幸运的是，怪物和boss也是这样。祝你好运。HELL TO YOU");
        }

        // 注意这里我们选择的是对Player生效的Update，另一个是对NPC生效的Update
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.GetModPlayer<VirtualDreamPlayer>().poisionLifeCostPerSecond++;
            //player.GetModPlayer<IllusionBoundPlayer>().ToxicLev[0] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.velocity *= 0.975f;
            npc.life -= ((npc.lifeMax / 100) * 2) / 120;
            npc.checkDead();
            base.Update(npc, ref buffIndex);
        }
    }
    public class ToxicⅡ : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("剧毒Ⅱ");
            Description.SetDefault("你的生命正在以百分比的形式下降。这意味着你的血量和防御再怎么高也没有意义。幸运的是，怪物和boss也是这样。祝你好运。HELL TO YOU");
        }

        // 注意这里我们选择的是对Player生效的Update，另一个是对NPC生效的Update
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.GetModPlayer<VirtualDreamPlayer>().poisionLifeCostPerSecond += 2;

            //player.GetModPlayer<IllusionBoundPlayer>().ToxicLev[1] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.life -= ((npc.lifeMax / 100) * 4) / 120;
            npc.checkDead();
            npc.velocity *= 0.95f;
            base.Update(npc, ref buffIndex);
        }
    }
    public class ToxicⅢ : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("剧毒Ⅲ");
            Description.SetDefault("你的生命正在以百分比的形式下降。这意味着你的血量和防御再怎么高也没有意义。幸运的是，怪物和boss也是这样。祝你好运。HELL TO YOU");
        }

        // 注意这里我们选择的是对Player生效的Update，另一个是对NPC生效的Update
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.GetModPlayer<VirtualDreamPlayer>().poisionLifeCostPerSecond += 3;

            //player.GetModPlayer<IllusionBoundPlayer>().ToxicLev[2] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.velocity *= 0.9f;
            npc.life -= ((npc.lifeMax / 100) * 6) / 120;
            npc.checkDead();
            base.Update(npc, ref buffIndex);
        }
    }
    public class Frozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("霜冻之矢");
            Description.SetDefault("刺骨凌冽的寒风");
        }

        // 注意这里我们选择的是对Player生效的Update，另一个是对NPC生效的Update
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.velocity *= 0.9f;
            for (int n = 0; n < 4; n++)
            {
                Dust d = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.PurpleFx, new Vector2(0, 0), 0, Color.White, 1f);
                d.noGravity = true;
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0.95f;
            for (int n = 0; n < 4; n++)
            {
                Dust d = Dust.NewDustPerfect(npc.Center + new Vector2(Main.rand.NextFloat(-64, 64), 0).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), MyDustId.PurpleFx, new Vector2(0, 0), 0, Color.White, 1f);
                d.noGravity = true;
            }
        }
    }
    public class Electrified : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("青电之矢");
            Description.SetDefault("这是触电一般的感觉...别和其他人挨得太近了。");
        }

        // 注意这里我们选择的是对Player生效的Update，另一个是对NPC生效的Update
        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            player.velocity *= 0.9f;
            if (Main.rand.NextBool(5))
            {
                ElectricTriangle.NewElectricTriangle(player.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(48), Main.rand.NextFloat(0, MathHelper.TwoPi), Main.rand.NextFloat(12, 24));
            }
            foreach (Player player1 in Main.player)
            {
                if (player1.hostile && player1.active && player1.whoAmI != player.whoAmI && (player.Center - player1.Center).Length() < 160f)
                {
                    if ((int)Main.GameUpdateCount % 12 == 0)
                    {
                        player1.statLife -= 5;
                        ElectricTriangle.NewElectricTriangle(player.Center, Main.rand.NextFloat(0, MathHelper.TwoPi), Main.rand.NextFloat(12, 24), (player1.Center - player.Center) / 16f);
                    }
                }
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0.95f;
            if (Main.rand.NextBool(5))
            {
                ElectricTriangle.NewElectricTriangle(npc.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(48), Main.rand.NextFloat(0, MathHelper.TwoPi), Main.rand.NextFloat(12, 24));
            }
            foreach (NPC npc1 in Main.npc)
            {
                if (npc1.active && !npc1.friendly && npc1.type != NPCID.TargetDummy && npc1.whoAmI != npc.whoAmI && (npc.Center - npc1.Center).Length() < 160f)
                {
                    if ((int)Main.GameUpdateCount % 3 == 0)
                    {
                        npc1.life -= 5;
                        ElectricTriangle.NewElectricTriangle(npc.Center, Main.rand.NextFloat(0, MathHelper.TwoPi), Main.rand.NextFloat(12, 24), (npc1.Center - npc.Center) / 16f);
                        npc1.checkDead();
                    }
                }
            }
        }
    }
    public class BuffColorNPC : GlobalNPC
    {
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<ToxicⅠ>()))
            {
                drawColor = Color.Lerp(drawColor, Color.Green, 0.5f) * (drawColor.R / 255f);
            }
            if (npc.HasBuff(ModContent.BuffType<ToxicⅡ>()))
            {
                drawColor = Color.Lerp(drawColor, Color.Purple, 0.5f) * (drawColor.R / 255f);
            }
            if (npc.HasBuff(ModContent.BuffType<ToxicⅢ>()))
            {
                drawColor = Color.Lerp(drawColor, Color.Lerp(Color.Green, Color.Purple, 0.5f), 0.5f) * (drawColor.R / 255f);
            }
            if (npc.HasBuff(ModContent.BuffType<Frozen>()))
            {
                drawColor = Color.Lerp(drawColor, Color.Cyan, 0.5f) * (drawColor.R / 255f);
            }
            if (npc.HasBuff(ModContent.BuffType<Electrified>()))
            {
                //drawColor = new Color(0.3f,0.6f,0.9f) * drawColor.R;
                //Main.NewText(Color.Lerp(Color.Cyan, Color.Purple, 0.5f));
                drawColor = Color.Lerp(drawColor, Color.Lerp(Color.Cyan, Color.Purple, 0.5f), 0.5f) * (drawColor.R / 255f);
            }
        }
    }
}
