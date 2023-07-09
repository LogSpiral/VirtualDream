using Terraria.ID;
namespace VirtualDream
{
    public class TeleportStaff : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.RubyStaff;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("传送法杖");
            // Tooltip.SetDefault("走你\n右键进入子世界_时之风");
        }
        Item item => Item;
        public override void SetDefaults()
        {
            //item.CloneDefaults(ItemID.Zenith);
            item.rare = MyRareID.Tier1;
            item.width = 46;
            item.height = 84;
            item.mana = 25;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 6;
            item.useStyle = 5;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<TeleportStaffProj>();
            item.noUseGraphic = true;
            item.noMelee = true;
            item.channel = true;
            item.shootSpeed = 1f;
            item.UseSound = SoundID.Item13;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        //public int myValue { get; private set; }
        //private bool GetZenithTarget(Vector2 searchCenter, float maxDistance, out int npcTargetIndex)
        //{
        //    npcTargetIndex = 0;
        //    int? num = null;
        //    float num2 = maxDistance;
        //    for (int i = 0; i < 200; i++)
        //    {
        //        NPC npc = Main.npc[i];
        //        if (npc.CanBeChasedBy(this, false))
        //        {
        //            float num3 = Vector2.Distance(searchCenter, npc.Center);
        //            if (num2 > num3)
        //            {
        //                num = new int?(i);
        //                num2 = num3;
        //            }
        //        }
        //    }
        //    if (num == null)
        //    {
        //        return false;
        //    }
        //    npcTargetIndex = num.Value;
        //    return true;
        //}

        //public override void UseAnimation(Player player)
        //{
        //    player.GetModPlayer<drawFlameModplayer>().创造火焰(player.Center);
        //    base.UseAnimation(player);
        //}

        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        //{
        //    Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true, true);
        //    float num6 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
        //    float num7 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
        //    int num166 = (player.itemAnimationMax - player.itemAnimation) / player.itemTime;
        //    Vector2 _velocity = new Vector2(num6, num7);
        //    int num167 = FinalFractalHelper.GetRandomProfileIndex();
        //    if (num166 == 0)
        //    {
        //        num167 = 24;
        //    }
        //    Vector2 value7 = Main.MouseWorld - player.MountedCenter;
        //    if (num166 == 1 || num166 == 2)
        //    {
        //        int num168;
        //        bool zenithTarget = this.GetZenithTarget(Main.MouseWorld, 400f, out num168);
        //        if (zenithTarget)
        //        {
        //            value7 = Main.npc[num168].Center - player.MountedCenter;
        //        }
        //        bool flag8 = num166 == 2;
        //        if (num166 == 1 && !zenithTarget)
        //        {
        //            flag8 = true;
        //        }
        //        if (flag8)
        //        {
        //            value7 += Main.rand.NextVector2Circular(150f, 150f);
        //        }
        //    }
        //    _velocity = value7 / 2f;
        //    float ai5 = (float)Main.rand.Next(-100, 101);
        //    Projectile.NewProjectile(source,player.Center, _velocity, type, damage, knockback, player.whoAmI, ai5, (float)num167);
        //    return false;
        //}

    }
    public class TeleportStaffProj : RangedHeldProjectile
    {
        public override float Factor => MathHelper.Clamp(Projectile.ai[0] / 30, 0, 1);
        public override void OnCharging(bool left, bool right)
        {
            for (int n = 0; n < Factor * 30; n++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Dust.NewDustPerfect(Player.Center + ((n / 30f + k) * MathHelper.PiOver2).ToRotationVector2() * 64, MyDustId.RedBubble, Main.rand.NextVector2Unit()).noGravity = true;
                }
            }
        }
        public override string Texture => "VirtualDream/Contents/StarBound/Weapons/BossDrop/KluexStaff/KluexStaffPH";//"Terraria/Images/Item_" + ItemID.RubyStaff
        public override void GetDrawInfos(ref Texture2D texture, ref Vector2 center, ref Rectangle? frame, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects spriteEffects)
        {
            texture = TextureAssets.Item[ItemID.RubyStaff].Value;
            base.GetDrawInfos(ref texture, ref center, ref frame, ref color, ref rotation, ref origin, ref scale, ref spriteEffects);
        }
        public override bool UseRight => true;
        public override void OnRelease(bool charged, bool left)
        {
            if (charged) 
            {
                if (left)
                {
                    Player.Teleport(Main.MouseWorld, 1);
                    //Main.NewText((Main.maxTilesX, Main.maxTilesY));
                }
                else 
                {
                    if (SubworldLibrary.SubworldSystem.Current != null)
                    {
                        SubworldLibrary.SubworldSystem.Exit();
                    }
                    else 
                    {
                        SubworldLibrary.SubworldSystem.Enter<Contents.StarBound.TimeBackTracking.TimeBackTrackingWorld>();
                    }
                }
            }
            base.OnRelease(charged, left);
        }
    }
}
