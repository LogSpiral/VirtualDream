using System;

using Terraria.ID;

using VirtualDream.Utils.BaseClasses;

namespace VirtualDream.Contents.StarBound.Weapons.BossDrop.IxodoomClaw
{
    public class IxodoomClaw : GlowItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("死亡主宰爪");
        }
        public Item item => Item;

        public override void SetDefaults()
        {
            item.damage = 200;
            item.crit = 21;
            item.DamageType = DamageClass.Melee;
            item.width = 88;
            item.height = 84;
            item.rare = MyRareID.Tier1;
            item.useTime = 30;
            item.useAnimation = 30;
            item.knockBack = 6;
            item.useStyle = 1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<IxodoomClawProj>();
            item.shootSpeed = 1f;
            item.noUseGraphic = true;
            item.noMelee = true;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[item.shoot] < 1;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.SpiderFang, 50);
            recipe1.AddIngredient(ItemID.Bone, 25);
            recipe1.AddIngredient(ItemID.VialofVenom, 50);
            recipe1.AddIngredient(ItemID.Stinger, 30);
            recipe1.AddIngredient(ItemID.StyngerBolt, 30);
            recipe1.AddIngredient(ItemID.Ectoplasm, 15);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class IxodoomClawEX : IxodoomClaw
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n 它在接受了远古精华的纯化后，拥有了更为强大的纯粹的力量。\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("死亡主宰爪EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 300;
            item.crit = 32;
            item.rare = MyRareID.Tier2;
        }
    }
    public class IxodoomClawHT : IxodoomClaw
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("强大的死亡主宰的断腿。这可以作为一个强大的武器。\n它的刀刃上附着着高度的剧毒\n[c/ff0000:温馨提示:不要对高血量怪物使用右键技能，怪物死了，你的电脑也卡死了（]\n此物品魔改自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("死亡主宰爪HT");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 450;
            item.crit = 50;
            item.rare = MyRareID.Tier3;
        }
        //private static int Time = 0;
        public override void HoldItem(Player player)
        {
            //Time++;
            //if (Time > 360)
            //    Time = 0;
            //int n = 8;
            int d = MyDustId.PinkBubble;
            float W = (3.1415926f / 180) * (float)IllusionBoundModSystem.ModTime2;
            for (int n = 0; n < 8; n++)
            {
                Dust dust1 = Terraria.Dust.NewDustPerfect(player.Center + (0.5f * 7 * W + MathHelper.TwoPi * n / 8).ToRotationVector2() * (float)(Math.Tan(4 * W) / Math.Sin(4 * W) + 8) * 25, d, new Vector2(0f, 0f), 0, Color.White, 1.5f);
                dust1.noGravity = true;
            }
            //float X1 = (float)Math.Cos(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y1 = (float)Math.Sin(0.5f * 7 * W) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X2 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W)/ Math.Sin(0.5f * n * W) + n) * 25;
            //float Y2 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 2 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X3 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y3 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 4 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X4 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y4 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 6 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X5 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y5 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 8 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X6 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y6 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 10 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X7 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y7 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 12 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float X8 = (float)Math.Cos(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float Y8 = (float)Math.Sin(0.5f * 7 * (W + 3.1415926f * 14 / n)) * (float)(Math.Tan(0.5f * n * W) / Math.Sin(0.5f * n * W) + n) * 25;
            //float s = 1.5f;
            //float v = 2f;
            //for (int i = 0; i < 3; i++)
            //{
            //    Dust dust1 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X1, Y1), d, new Vector2(0f, 0f), 0, Color.White, s);
            //    Dust dust2 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X2, Y2), d, new Vector2(0, 0), 0, Color.White, s);
            //    Dust dust3 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X3, Y3), d, new Vector2(0f, 0f), 0, Color.White, s);
            //    Dust dust4 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X4, Y4), d, new Vector2(0, 0), 0, Color.White, s);
            //    Dust dust5 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X5, Y5), d, new Vector2(0f, 0f), 0, Color.White, s);
            //    Dust dust6 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X6, Y6), d, new Vector2(0f, 0f), 0, Color.White, s);
            //    Dust dust7 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X7, Y7), d, new Vector2(0, 0), 0, Color.White, s);
            //    Dust dust8 = Terraria.Dust.NewDustPerfect(player.Center + new Vector2(X8, Y8), d, new Vector2(0f, 0f), 0, Color.White, s);
            //    dust1.noGravity = true;
            //    dust1.velocity *= v;
            //    dust2.noGravity = true;
            //    dust2.velocity *= v;
            //    dust3.noGravity = true;
            //    dust3.velocity *= v;
            //    dust4.noGravity = true;
            //    dust4.velocity *= v;
            //    dust5.noGravity = true;
            //    dust5.velocity *= v;
            //    dust6.noGravity = true;
            //    dust6.velocity *= v;
            //    dust7.noGravity = true;
            //    dust7.velocity *= v;
            //    dust8.noGravity = true;
            //    dust8.velocity *= v;
            //}
        }
    }
    public class IxodoomClawProj : VertexHammerProj
    {
        public override string HammerName => base.HammerName;
        public override float MaxTime => (controlState == 2 ? 2f : 1f) * UpgradeValue(30, 24, 18);
        public override float factor => base.factor;
        public override Vector2 CollidingSize => base.CollidingSize * 2;
        //public override Vector2 projCenter => base.projCenter + new Vector2(Player.direction * 16, -16);
        public override Vector2 CollidingCenter => base.CollidingCenter;//new Vector2(projTex.Size().X / 3 - 16, 16)
        public override Vector2 DrawOrigin => base.DrawOrigin + new Vector2(-12, 12);
        public override Color color => base.color;
        public override Color VertexColor(float time) => default;
        public override float MaxTimeLeft => (controlState == 2 ? 0.75f : 1f) * UpgradeValue(10, 8, 7);
        public override float Rotation => base.Rotation;
        
        public override bool UseRight => true;
        public override (int X, int Y) FrameMax => (3, 1);
        public override void Kill(int timeLeft)
        {
            int max = (int)(30 * factor);
            var vec = (CollidingCenter - DrawOrigin).RotatedBy(Rotation) + projCenter;
            if (factor > 0.75f)
            {
                for (int n = 0; n < max; n++)
                {
                    Dust.NewDustPerfect(vec, UpgradeValue(MyDustId.YellowHallowFx, MyDustId.GreenFXPowder, MyDustId.PinkBubble), (MathHelper.TwoPi / max * n).ToRotationVector2() * Main.rand.NextFloat(2, 8)).noGravity = true;
                }
            }
            //if (factor == 1)
            //{
            //    Projectile.NewProjectile(projectile.GetSource_FromThis(), vec, default, ModContent.ProjectileType<HolyExp>(), player.GetWeaponDamage(player.HeldItem) * 3, projectile.knockBack, projectile.owner);
            //}
            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            if (controlState == 2)
            {
                target.AddBuff(UpgradeValue(ModContent.BuffType<ToxicⅠ>(), ModContent.BuffType<ToxicⅡ>(), ModContent.BuffType<ToxicⅢ>()), UpgradeValue(600, 1200, 1800));
            }
        }
        public override Rectangle? frame => projTex.Frame(3, 1, UpgradeValue(0, 1, 2));
        public T UpgradeValue<T>(T normal, T extra, T ultra, T defaultValue = default)
        {
            var type = Player.HeldItem.type;
            if (type == ModContent.ItemType<IxodoomClaw>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<IxodoomClawEX>())
            {
                return extra;
            }

            if (type == ModContent.ItemType<IxodoomClawHT>())
            {
                return ultra;
            }

            return defaultValue;
        }
    }
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
    public class ToxicColorNPC : GlobalNPC
    {
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<ToxicⅠ>()))
            {
                return Color.Green * drawColor.R;
            }

            if (npc.HasBuff(ModContent.BuffType<ToxicⅡ>()))
            {
                return Color.Purple * drawColor.R;
            }

            if (npc.HasBuff(ModContent.BuffType<ToxicⅢ>()))
            {
                return Color.Lerp(Color.Green, Color.Purple, 0.5f) * drawColor.R;
            }

            return drawColor;
        }
    }
}