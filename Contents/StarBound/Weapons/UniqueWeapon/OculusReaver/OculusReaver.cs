using System;
using Terraria.DataStructures;
using Terraria.ID;

using VirtualDream.Utils.BaseClasses;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.OculusReaver
{
    public class OculusReaver : GlowItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("当你凝视着它的时候，它也许也凝视着你。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("目珠掠夺者");
        }
        public Item item => Item;

        public override void SetDefaults()
        {
            item.damage = 150;
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
            item.shoot = ModContent.ProjectileType<OculusReaverProj>();
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
            //Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient(ItemID.SpiderFang, 50);
            //recipe1.AddIngredient(ItemID.Bone, 25);
            //recipe1.AddIngredient(ItemID.VialofVenom, 50);
            //recipe1.AddIngredient(ItemID.Stinger, 30);
            //recipe1.AddIngredient(ItemID.StyngerBolt, 30);
            //recipe1.AddIngredient(ItemID.Ectoplasm, 15);
            //recipe1.AddTile(TileID.MythrilAnvil);
            //recipe1.SetResult(this);
            //recipe1.AddRecipe();
        }
    }
    public class OculusReaverEX : OculusReaver
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("当你凝视着它的时候，它也许也凝视着你。\n当你一脸嫌弃看向你的敌人的时候，它也会一脸嫌弃地看着你的猎物。(x\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("目珠掠夺者EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 250;
            item.crit = 32;
            item.rare = MyRareID.Tier2;
        }
    }
    public class OculusReaverProj : VertexHammerProj
    {
        public override string HammerName => base.HammerName;
        public override float MaxTime => (controlState == 2 ? 2f : 1f) * UpgradeValue(12, 9);
        public override float factor => base.factor;
        public override Vector2 CollidingSize => base.CollidingSize * 2;
        //public override Vector2 projCenter => base.projCenter + new Vector2(Player.direction * 16, -16);
        public override Vector2 CollidingCenter => base.CollidingCenter;//new Vector2(projTex.Size().X / 3 - 16, 16)
        public override Vector2 DrawOrigin => base.DrawOrigin + new Vector2(-12, 12);
        public override Color color => base.color;
        public override Color VertexColor(float time) => default;
        public override float MaxTimeLeft => (controlState == 2 ? 0.75f : 1f) * UpgradeValue(8, 7);
        public override float Rotation => base.Rotation;
        public override bool UseRight => true;
        public override (int X, int Y) FrameMax => (2, 1);

        public override void Kill(int timeLeft)
        {

            //Lighting.add

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
        }
        public override void OnRelease(bool charged, bool left)
        {
            if (Charged && left)
            {
                int max = UpgradeValue(2, 5);
                for (int n = 0; n < max; n++) 
                {
                    Vector2 pointPoisition2 = Player.Center + new Vector2(128 * Player.direction, 0) * ((projectile.ai[1] + (float)n / max) / MaxTimeLeft) * max;
                    Player.LimitPointToPlayerReachableArea(ref pointPoisition2);
                    Vector2 vector22 = pointPoisition2 + Main.rand.NextVector2Circular(8f, 8f);
                    Vector2 value7 = Player.FindSharpTearsSpot(vector22).ToWorldCoordinates(Main.rand.Next(17), Main.rand.Next(17));
                    Vector2 vector23 = (vector22 - value7).SafeNormalize(-Vector2.UnitY) * 16f;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), value7.X, value7.Y, vector23.X, vector23.Y, 756, projectile.damage / 3, projectile.knockBack, Player.whoAmI, 0f, Main.rand.NextFloat() * 0.5f + 0.5f);
                }

            }
            base.OnRelease(charged, left);
        }
        public override Rectangle? frame => projTex.Frame(2, 1, UpgradeValue(0, 1));
        public Item sourceItem;
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo itemSource)
            {
                sourceItem = itemSource.Item;
            }
        }
        public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        {
            //var type = Player.HeldItem.type;
            var type = sourceItem.type;

            if (type == ModContent.ItemType<OculusReaver>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<OculusReaverEX>())
            {
                return extra;
            }

            return defaultValue;
        }
    }
}