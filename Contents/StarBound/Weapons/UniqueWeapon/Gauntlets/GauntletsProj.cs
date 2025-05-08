using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;
using System;
using System.Collections.Generic;
using Terraria.ID;
using static Terraria.Utils;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Gauntlets
{
    public abstract class Gauntlets : StarboundWeaponBase
    {
        public Item item => Item;
        public override void SetDefaults()
        {
            item.damage = 50;
            item.DamageType = DamageClass.Melee;
            item.width = 20;
            item.height = 14;
            item.useTime = 12;
            item.useAnimation = 12;
            item.knockBack = 8;
            item.useStyle = ItemUseStyleID.Swing;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.rare = 10;
            //item.rare = ItemRarityID.Purple;
            //item.UseSound = SoundID.Item1;
            //item.useStyle = ItemUseStyleID.Swing;
            //item.damage = 190;
            //item.useAnimation = 16;
            //item.useTime = 16;
            //item.width = 30;
            //item.height = 30;
            //item.shoot = mod.ProjectileType("ForbiddenSpeechBeam");
            //item.scale = 1.1f;
            //item.shootSpeed = 12f;
            //item.knockBack = 6.5f;
            //item.DamageType = DamageClass.Melee;
            //item.value = Item.sellPrice(0, 20, 0, 0);
            //item.autoReuse = true;
        }
        public virtual int projType => Mod.Find<ModProjectile>(GetType().Name + "Proj").Type;
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[projType] < 1)
                {
                    Projectile.NewProjectile(GetSource_StarboundWeapon(), player.Center, new Vector2(0, -1), projType, player.GetWeaponDamage(item), 0f, player.whoAmI);//player.GetWeaponKnockback(item, item.knockBack)
                }
            }
        }
    }
    public abstract class GauntletsProj : ModProjectile, IStarboundWeaponProjectile
    {
        public Projectile projectile => Projectile;
        public virtual float swooshAlpha => WhenSA ? 0.75f : 0.3f;
        public Player owner => Main.player[projectile.owner];
        public Texture2D projTex => TextureAssets.Projectile[projectile.type].Value;
        public int LeftTimer => (int)projectile.ai[0];
        public int RightTimer => (int)projectile.ai[1];
        public int Counter
        {
            get => projectile.frameCounter;
            set { projectile.frameCounter = value; }
        }
        public int LastState
        {
            get => projectile.frame;
            set { projectile.frame = value; }
        }
        public virtual int itemType => Mod.Find<ModItem>(GetType().Name.Replace("Proj", "")).Type;
        public virtual float GetFactor(int index)
        {
            float factor = MathHelper.Clamp(owner.itemAnimationMax + 3 - projectile.ai[index], 0, owner.itemAnimationMax);
            factor /= owner.itemAnimationMax;
            factor = (float)Math.Pow(factor, 3);
            if (factor < 0.75f)
            {
                owner.bodyFrame.Y = 1008 - (int)(factor * 4f) * 56;
            }
            else
            {
                owner.bodyFrame.Y = 392;
            }

            return factor;
        }
        public virtual void SpecialAttack()
        {

        }
        public virtual bool WhenSA => Counter == 3 && (int)projectile.ai[(LastState + Counter) % 2] > 3;
        public Vector2 ProjCenter(int index)
        {
            if (index < 0 || index > 1) throw new Exception("超范围辣我囸你先人");
            Vector2 Offset = default;
            if (index == 1) Offset += new Vector2(10, 2);
            if ((int)projectile.ai[index] == 0)//< 3
            {
                Offset += new Vector2(4, 4);
                owner.bodyFrame.Y = 1008;
            }
            else
            {
                Offset += Vector2.Lerp(new Vector2(-8, 0), new Vector2(8, 0), GetFactor(index));
            }
            return owner.Center + Offset * new Vector2(owner.direction, 1);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[projectile.whoAmI] = owner.itemAnimationMax / 4;//3
            if (target.CanBeChasedBy())
                target.velocity += (target.Center - owner.Center).SafeNormalize(default) * (float)Math.Sqrt(KnockBackValue / 4f) * 4f;//4f
            base.OnHitNPC(target, hit, damageDone);
        }
        public virtual float KnockBackValue => owner.GetWeaponKnockback(owner.HeldItem, owner.HeldItem.knockBack);
        public override void AI()
        {
            if (owner.HeldItem.type != itemType) projectile.Kill();
            else projectile.timeLeft = 2;
            if (projectile.ai[0] < 3 && projectile.ai[1] < 3)
            {
                if (owner.controlUseItem ^ owner.controlUseTile)//Main.mouseLeft ^ Main.mouseRight
                {
                    int last = LastState;
                    LastState = owner.controlUseItem ? 0 : 1;
                    if (last != LastState)
                    {
                        Counter = 0;
                    }
                    else
                    {
                        Counter++;
                        Counter %= 4;
                    }
                    projectile.ai[(LastState + Counter) % 2] = owner.itemAnimationMax + 3;
                    owner.direction = Math.Sign(Main.MouseWorld.X - owner.Center.X);
                }
            }
            if (WhenSA)
            {
                SpecialAttack();
            }
            if ((int)projectile.ai[0] == 0 && (int)projectile.ai[1] == 0)
            {
                LastState = 2;
            }
            projectile.ai[0] -= projectile.ai[0] > 0 ? 1 : 0;
            projectile.ai[1] -= projectile.ai[1] > 0 ? 1 : 0;
            projectile.Center = owner.Center;
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.DamageType = DamageClass.Melee;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("打拳辣");
        }
        public virtual Vector2 hitBox => new(32);
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int n = 0; n < 2; n++)
            {
                if (targetHitbox.Intersects(CenteredRectangle(ProjCenter(n), hitBox)) && projectile.ai[n] >= 3) return true;
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            for (int n = 1; n >= 0; n--)
            {
                //(int)projectile.ai[n] < 3
                var projCen = ProjCenter(n);
                spriteBatch.Draw(projTex, projCen - Main.screenPosition, projTex.Frame(2, 1, n, 0), owner.GetColor(), (int)projectile.ai[n] == 0 ? -MathHelper.PiOver4 * owner.direction : 0, projTex.Frame(2).Size() * .5f, 2f, owner.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
                if ((int)projectile.ai[n] >= 3)
                    DrawSwoosh(spriteBatch, projCen, GetFactor(n), n);
            }
            return false;
        }
        public const string GaunletsPath = "Contents/StarBound/Weapons/UniqueWeapon/Gauntlets/";
        public virtual (Texture2D tex, int frames) SwooshTex => (VirtualDreamMod.GetTexture(GaunletsPath + "physicalswoosh"), 3);
        public virtual float swooshSize => 3f;
        public virtual void DrawSwoosh(SpriteBatch spriteBatch, Vector2 projCen, float factor, int index)
        {
            //Main.NewText((int)(factor * 3f));
            spriteBatch.Draw(SwooshTex.tex, projCen - Main.screenPosition + new Vector2(4f * owner.direction, 0), SwooshTex.tex.Frame(SwooshTex.frames, 1, (int)MathHelper.Clamp(factor * SwooshTex.frames, 0, SwooshTex.frames - 1), 0), owner.GetColor() * swooshAlpha, 0, SwooshTex.tex.Frame(SwooshTex.frames).Size() * .5f, swooshSize, (owner.direction == 1 ? 0 : SpriteEffects.FlipHorizontally) ^ (index == 0 ? 0 : SpriteEffects.FlipVertically), 0);//.75f * (float)(0.5f - 0.5f * Math.Cos(factor * factor))
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Main.instance.DrawCacheProjsOverWiresUI.Add(index);
            //drawCacheProjsBehindNPCsAndTiles.Remove(index);
            //drawCacheProjsBehindNPCsAndTiles.Remove(index);
            //drawCacheProjsBehindNPCsAndTiles.Remove(index);
        }
    }
    public class BoxingGlove : Gauntlets
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useTime = item.useAnimation = 8;

        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("拳击手套");
            // Tooltip.SetDefault("现在你可以成为一个竞争者\n终结技：崩拳");
        }
    }
    public class Gauntlet : Gauntlets
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 180;
            item.knockBack += 3f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("决斗拳套");
            // Tooltip.SetDefault("按Q发起一场决斗\n终结技：升龙拳");
        }
    }
    public class StunGlove : Gauntlets
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("电击拳套");
            // Tooltip.SetDefault("非常有效。\n终结技：雷光爪");
        }
    }
    public class VineFist : Gauntlets
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("藤蔓拳套");
            // Tooltip.SetDefault("释放自然的力量。\n终结技：鞭藤");
        }
    }
    public class ClawGlove : Gauntlets
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 220;
            item.knockBack += 2f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("狼爪");
            // Tooltip.SetDefault("不够坚硬，但依然很酷。\n终结技：疾冲裂伤");
        }
    }
    public class SupernovaGauntlet : Gauntlets
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useTime = item.useAnimation = 20;
            item.damage = 250;
            item.knockBack += 8f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("超新星护手");
            // Tooltip.SetDefault("具有一颗超新星力量的手套。\n终结技：突进");
        }
    }
    public class BoxingGloveProj : GauntletsProj
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            CombatText.NewText(target.Hitbox, Color.Lerp(Color.Red, Color.Yellow, Main.rand.NextFloat(0, 1)), "欧拉！", true, true);
            //base.OnHitNPC(target, damage, knockback, crit);
            if (WhenSA)
            {
                target.immune[projectile.whoAmI] = owner.itemAnimationMax / 4;//3
                if (target.CanBeChasedBy())
                    target.velocity += (target.Center - owner.Center).SafeNormalize(default) * (float)Math.Sqrt(KnockBackValue / 2f) * 4f;//4f
                //if (((IStarboundWeaponProjectile)this).weapon != null)
                //{
                //    if (target.type != NPCID.TargetDummy)
                //    {
                //        if (target.life - damage <= 0)
                //        {
                //            ((IStarboundWeaponProjectile)this).weapon.killCount++;
                //        }
                //        ((IStarboundWeaponProjectile)this).weapon.hurtCount += damage;
                //    }
                //}
            }
            else
            {
                base.OnHitNPC(target, hit, damageDone);
            }
            target.immune[projectile.whoAmI] = 1;
        }
        public override (Texture2D tex, int frames) SwooshTex => WhenSA ? (VirtualDreamMod.GetTexture(GaunletsPath + "powerpunchswoosh"), 4) : base.SwooshTex;
        public override Vector2 hitBox => WhenSA ? new Vector2(64) : base.hitBox;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("拳击手套");
        }
    }
    public class GauntletProj : GauntletsProj
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("决斗拳套");
        }
        public override (Texture2D tex, int frames) SwooshTex => WhenSA ? (VirtualDreamMod.GetTexture(GaunletsPath + "uppercutswoosh"), 4) : base.SwooshTex;
        public override Vector2 hitBox => WhenSA ? new Vector2(80) : base.hitBox;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (WhenSA && target.CanBeChasedBy()) target.velocity.Y -= 16;
        }
        public override void DrawSwoosh(SpriteBatch spriteBatch, Vector2 projCen, float factor, int index)
        {
            spriteBatch.Draw(SwooshTex.tex, projCen - Main.screenPosition + new Vector2(4f * owner.direction, 0), SwooshTex.tex.Frame(SwooshTex.frames, 1, (int)MathHelper.Clamp(factor * SwooshTex.frames, 0, SwooshTex.frames - 1), 0), owner.GetColor() * swooshAlpha, 0, SwooshTex.tex.Frame(SwooshTex.frames).Size() * .5f, swooshSize, owner.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
        }
    }
    public class StunGloveProj : GauntletsProj
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("电击拳套");
        }
        public override (Texture2D tex, int frames) SwooshTex => WhenSA ? (VirtualDreamMod.GetTexture(GaunletsPath + "thunderpunchswoosh"), 4) : (VirtualDreamMod.GetTexture(GaunletsPath + "electricswoosh"), 4);
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (!WhenSA)
                return base.Colliding(projHitbox, targetHitbox);
            for (int n = 0; n < 2; n++)
            {
                var rec = CenteredRectangle(ProjCenter(n), hitBox);
                if (owner.direction == 1)
                {
                    rec.Width += (int)(GetFactor(n) * 40);
                }
                else
                {
                    rec.X -= (int)(GetFactor(n) * 40);
                }
                if (targetHitbox.Intersects(rec) && projectile.ai[n] >= 3) return true;
            }
            return false;
        }
        public override float swooshSize => WhenSA ? 2f : base.swooshSize;
    }
    public class VineFistProj : GauntletsProj
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("藤蔓拳套");
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //int index = -1;
            SpriteBatch spriteBatch = Main.spriteBatch;
            for (int n = 1; n >= 0; n--)
            {
                //(int)projectile.ai[n] < 3
                if (!WhenSA || projectile.ai[n] < 3)
                {
                    //index = n;
                    var projCen = ProjCenter(n);
                    spriteBatch.Draw(projTex, projCen - Main.screenPosition, new Rectangle(0, 16 * n, 16, 16), owner.GetColor(), (int)projectile.ai[n] == 0 ? -MathHelper.PiOver4 * owner.direction : 0, new Vector2(8), 2f, owner.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
                    if ((int)projectile.ai[n] >= 3)
                        DrawSwoosh(spriteBatch, projCen, GetFactor(n), n);
                }
                else if (WhenSA)
                {
                    //index++;
                    //index %= 2;
                    var projCen = ProjCenter(n);
                    if (owner.direction == -1) projCen.X -= 64;
                    spriteBatch.Draw(projTex, projCen - Main.screenPosition, new Rectangle((int)MathHelper.Clamp(GetFactor(n) * 4, 0, 3) * 48, 16 * n, 48, 16), owner.GetColor(), 0, new Vector2(8), 2f, owner.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
                }
            }
            //if (WhenSA) 
            //{
            //    index++;
            //    index %= 2;
            //    var projCen = ProjCenter(index);
            //    if (owner.direction == -1) projCen.X -= 64;
            //    spriteBatch.Draw(projTex, projCen  - Main.screenPosition, new Rectangle((int)MathHelper.Clamp(GetFactor(index) * 4, 0, 3) * 48, 16 * index, 48, 16), owner.GetColor(), 0, new Vector2(8), 2f, owner.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 0);
            //}
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (!WhenSA)
                return base.Colliding(projHitbox, targetHitbox);
            for (int n = 0; n < 2; n++)
            {
                var rec = CenteredRectangle(ProjCenter(n), hitBox);
                if (owner.direction == 1)
                {
                    rec.Width += (int)(GetFactor(n) * 80);
                }
                else
                {
                    rec.X -= (int)(GetFactor(n) * 80);
                }
                if (targetHitbox.Intersects(rec) && projectile.ai[n] >= 3) return true;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.ToxicⅠ>(), 30);
            target.AddBuff(BuffID.Poisoned, 30);

            base.OnHitNPC(target, hit, damageDone);
        }
    }
    public class ClawGloveProj : GauntletsProj
    {
        public override Vector2 hitBox => new(64, 32);
        public override float swooshSize => 2.5f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("狼爪");
        }
        public override void SpecialAttack()
        {
            owner.immune = true;
            if (Math.Abs(owner.velocity.X) < 32)
                owner.velocity += new Vector2(owner.direction, 0);
        }
        public override (Texture2D tex, int frames) SwooshTex => WhenSA ? (VirtualDreamMod.GetTexture(GaunletsPath + "dashswoosh"), 4) : (VirtualDreamMod.GetTexture(GaunletsPath + "clawswoosh"), 3);
    }
    public class SupernovaGauntletProj : GauntletsProj
    {
        public override Vector2 hitBox => new(64);
        //public override float swooshSize => base.swooshSize;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("超新星护手");
        }
        public override void SpecialAttack()
        {
            owner.immune = true;
            if (Math.Abs(owner.velocity.X) < 48)
                owner.velocity += new Vector2(owner.direction * 1.5f, 0);
        }
        public override (Texture2D tex, int frames) SwooshTex => WhenSA ? (VirtualDreamMod.GetTexture(GaunletsPath + "novapunch"), 1) : (VirtualDreamMod.GetTexture(GaunletsPath + "bigphysicalswoosh"), 3);

    }
}
