using Terraria.ID;
using Terraria.DataStructures;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.FlamingDemon
{
    public class FlamingDemonSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("无穷的烈焰自它的剑锋中喷涌而出。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("炎岚之怒");
        }
        public Item item => Item;

        public override void SetDefaults()
        {
            item.DamageType = DamageClass.Melee;
            item.crit = 6;
            item.width = 71;
            item.width = 71;
            item.useTime = 60;
            item.useAnimation = 60;
            item.knockBack = 10;
            item.useStyle = ItemUseStyleID.Swing;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 10);
            item.rare = MyRareID.Tier2;
            item.shoot = ModContent.ProjectileType<FlamingDemonSwordProj>();
            item.noMelee = true;
            item.noUseGraphic = true;
            item.shootSpeed = 1;
            item.damage = 250;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = ModContent.ProjectileType<FlamingDemonSwordProj>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.shootSpeed = 1;
                item.mana = 5;
                item.channel = true;
            }
            else
            {
                item.mana = 0;
                item.channel = false;
                item.shoot = ProjectileID.None;
                item.noMelee = false;
                item.noUseGraphic = false;
                item.shootSpeed = 0;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<FlamingDemonSwordProj>()] < 1;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, MyDustId.Fire, 0, 0, 100, Color.White, 1.0f);
        }
        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient<Materials.ScorchedCore>(50);
            recipe1.AddIngredient<Materials.AncientEssence>(2000);
            recipe1.AddIngredient(ItemID.FragmentSolar, 15);
            recipe1.AddIngredient(ItemID.LunarBar, 20);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
    public class FlamingDemonSwordEX : FlamingDemonSword
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("无穷的烈焰自它的剑锋中喷涌而出。\n火焰更加猛烈，一切都将化为灰烬吧。\n此物品来自[c/cccccc:STARB][c/cccc00:O][c/cccccc:UND]");
            DisplayName.SetDefault("炎岚之怒EX");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useTime = 48;
            item.useAnimation = 48;
            item.knockBack = 10;
            item.value = Item.sellPrice(0, 50);
            item.rare = MyRareID.Tier3;
            item.damage = 400;
        }
        public override void AddRecipes()
        {
        }
    }
    public class FlamingDemonSwordProj : RangedHeldProjectile
    {
        public override (int X, int Y) FrameMax => (6, 2);
        public override void OnCharging(bool left, bool right)
        {
            if (Charged && (int)Projectile.ai[0] % 5 == 0)
            {
                if (Player.CheckMana(sourceItem, -1, true))
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), ShootCenter, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.Pi / 48, MathHelper.Pi / 48)) * 32, ModContent.ProjectileType<OtherProjectiles.FlameCloud>(), Projectile.damage / 8, Projectile.knockBack, Projectile.owner, 0.975f);
                }
                else Projectile.Kill();
            }
        }
        public override bool UseLeft => false;
        public override bool UseRight => true;
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            origin = new Vector2(18, 22);
            frame = texture.Frame(FrameMax.X, FrameMax.Y, (int)Projectile.ai[0] / 2 % 6, UpgradeValue(0, 1));
        }
        public override bool Charged => base.Charged;
        public override Vector2 ShootCenter => base.ShootCenter + Projectile.velocity * 76;
        public override float Factor
        {
            get
            {
                return MathHelper.Clamp(Projectile.ai[0] / UpgradeValue(30f, 24f), 0, 1);
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo itemSource)
            {
                sourceItem = itemSource.Item;
            }
        }
        public Item sourceItem;
        public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
        {
            var type = sourceItem.type;//Player.HeldItem.type
            if (type == ModContent.ItemType<FlamingDemonSword>())
            {
                return normal;
            }

            if (type == ModContent.ItemType<FlamingDemonSwordEX>())
            {
                return extra;
            }

            return defaultValue;
        }
    }
}