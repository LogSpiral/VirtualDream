//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using IllusionBoundMod.Utils;
//using IllusionBoundMod.Tiles.StormZone;
//using static Terraria.ModLoader.ModContent;
//using Terraria.DataStructures;

//namespace IllusionBoundMod.Items.Electrical
//{

//    //melee
//    public class StormItem : ModItem
//    {
//        public Item item => Item;
//        public Mod mod => Mod;
//    }
//    public class StormProjectile : ModProjectile
//    {
//        public virtual bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            return false;
//        }
//        public override bool PreDraw(ref Color lightColor)
//        {
//            return PreDraw(Main.spriteBatch, lightColor);
//        }
//        public Projectile projectile => Projectile;
//        public Mod mod => Mod;
//    }
//    public class ElectricalSwordUnCharged : StormItem
//    {
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemType<StormBrick>(), 30);
//            recipe.AddIngredient(ItemType<StormSandBrick>(), 10);
//            recipe.AddIngredient(ItemType<StormGlass>(), 10);
//            recipe.AddIngredient(ItemType<StormWood>(), 25);
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("闪电剑");
//            Tooltip.SetDefault("[未充能]");
//        }
//        public override void SetDefaults()
//        {
//            item.rare = ItemRarityID.Green;
//            item.UseSound = SoundID.Item1;
//            item.useStyle = ItemUseStyleID.Swing;
//            item.damage = 21;
//            item.useAnimation = 16;
//            item.useTime = 16;
//            item.width = 64;
//            item.height = 64;
//            item.scale = 1f;
//            item.knockBack = 1.2f;
//            item.DamageType = DamageClass.Melee;
//            item.value = Item.sellPrice(0, 0, 20, 0);
//            item.autoReuse = true;
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalSword : StormItem
//    {
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemType<StormBar>(), 25);
//            recipe.AddIngredient(ItemType<ElectricalSwordUnCharged>());
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("闪电剑");
//            Tooltip.SetDefault("万钧雷霆之下的水蓝弧光\n每五刀降下一束闪电。");
//        }
//        public override void SetDefaults()
//        {
//            item.rare = ItemRarityID.Orange;
//            item.UseSound = SoundID.Item1;
//            item.useStyle = ItemUseStyleID.Swing;
//            item.damage = 25;
//            item.useAnimation = 16;
//            item.useTime = 80;
//            item.width = 64;
//            item.height = 64;
//            item.shoot = ProjectileType<ElectricalCloud>();
//            item.scale = 1f;
//            item.shootSpeed = 12f;
//            item.knockBack = 1.5f;
//            item.DamageType = DamageClass.Melee;
//            item.value = Item.sellPrice(0, 0, 20, 0);
//            item.autoReuse = true;
//        }
//        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//        {
//            position = new Vector2(Main.MouseWorld.X, player.Center.Y - 512);
//            damage = (int)((float)damage * 2 / 3);
//            Projectile.NewProjectile(source, position, default, type, damage, knockback, player.whoAmI);
//            //Projectile.NewProjectile(position, new Vector2(0, 16), ProjectileType<MeleeLight>(), damage, knockBack, player.whoAmI, 0, 1);
//            return false;
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalCloud : StormProjectile 
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("雷云");
//        }
//        public override void SetDefaults()
//        {
//            projectile.tileCollide = false;
//            projectile.ignoreWater = true;
//            projectile.width = 54;
//            projectile.height = 28;
//            projectile.aiStyle = -1;
//            projectile.penetrate = -1;
//            projectile.timeLeft = 60;
//        }
//        public override void AI()
//        {
//			Main.projFrames[projectile.type] = 6;
//			projectile.frame += ((int)Main.time % 8 == 0) ? 1 : 0;
//            if (projectile.timeLeft < 30)
//            {
//                projectile.alpha += 8;
//            }
//		}
//    }
//    public class MeleeLight : StormProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("电弧");
//        }
//        public override void SetDefaults()
//        {
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.aiStyle = -1;
//            projectile.friendly = true;
//            projectile.light = 0.1f;
//            projectile.timeLeft = 30;
//            projectile.ignoreWater = true;
//            projectile.tileCollide = false;
//            projectile.penetrate = -1;
//        }

//        public override bool ShouldUpdatePosition()
//        {
//            return false;
//        }
//        private LightTree tree;
//        public override void AI()
//        {
//            if (projectile.ai[0] % 3 == 0)
//            {
//                tree = new LightTree(Main.rand);
//                Vector2 pos = projectile.Center + projectile.velocity * 100f;
//                tree.Generate(projectile.Center, projectile.velocity, pos);
//            }
//            projectile.ai[0]++;
//        }
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            return tree.Check(targetHitbox);
//        }


//        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
//        {
//            target.immune[projectile.owner] = (int)projectile.ai[1];
//            for (int i = 0; i < 2; i++)
//            {
//                var dust = Dust.NewDustDirect(target.position, target.width, target.height, MyDustId.ElectricCyan, 0, 0, 100, Color.White, 0.3f);
//                dust.noGravity = true;
//                dust.velocity *= 1.5f;
//            }
//        }
//        public override void PostDraw(Color lightColor)
//        {
//            tree.Draw(Main.spriteBatch, projectile.Center - Main.screenPosition, projectile.velocity);
//        }
//    }
//    //magic
//    public class ElectricalStaffUnCharged : StormItem
//    {
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemType<StormBrick>(), 30);
//            recipe.AddIngredient(ItemType<StormSandBrick>(), 10);
//            recipe.AddIngredient(ItemType<StormGlass>(), 10);
//            recipe.AddIngredient(ItemType<StormWood>(), 25);
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("闪电束法杖");
//            Tooltip.SetDefault("未充能");
//            Item.staff[item.type] = true;
//        }
//        public override void SetDefaults()
//        {
//            item.rare = ItemRarityID.Green;
//            item.UseSound = SoundID.Item43;
//            item.useStyle = ItemUseStyleID.Shoot;
//            item.damage = 25;
//            item.useAnimation = 26;
//            item.useTime = 26;
//            item.width = 54;
//            item.height = 54;
//            item.shoot = ProjectileType<ElectricalBolt>();
//            item.scale = 1f;
//            item.shootSpeed = 12f;
//            item.knockBack = 4f;
//            item.mana = 9;
//            item.DamageType = DamageClass.Magic;
//            item.value = 40000;
//            item.autoReuse = true;
//            item.noMelee = true;
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalStaff : StormItem
//    {
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemType<StormBar>(), 25);
//            recipe.AddIngredient(ItemType<ElectricalStaffUnCharged>());
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("闪电束法杖");
//            Tooltip.SetDefault("电弧在杖端跃动又凝聚\n发射闪电团，1/8的概率发射强化闪电团。");
//            Item.staff[item.type] = true;
//        }
//        public override void SetDefaults()
//        {
//            item.rare = ItemRarityID.Orange;
//            item.UseSound = SoundID.Item43;
//            item.useStyle = ItemUseStyleID.Shoot;
//            item.damage = 30;
//            item.useAnimation = 24;
//            item.useTime = 24;
//            item.width = 54;
//            item.height = 54;
//            item.shoot = ProjectileType<ElectricalBolt>();
//            item.scale = 1f;
//            item.shootSpeed = 12f;
//            item.knockBack = 6f;
//            item.mana = 8;
//            item.DamageType = DamageClass.Magic;
//            item.value = 50000;
//            item.autoReuse = true;
//            item.noMelee = true;
//        }
//        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
//        {
//            if (Main.rand.NextBool(8))
//            {
//                velocity *= 1.5f;
//                damage = (int)(damage * 1.5f);
//                knockback *= 1.5f;
//                type = ProjectileType<ElectricalBolt_Large>();
//            }
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalBolt : StormProjectile 
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("闪电团");
//        }
//        public override void SetDefaults()
//        {
//            projectile.width = 10;
//            projectile.height = 10;
//            projectile.timeLeft = 300;
//            projectile.aiStyle = -1;
//            projectile.alpha = 255;
//            projectile.DamageType = DamageClass.Magic;
//            projectile.penetrate = 5;
//            projectile.friendly = true;
//        }
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            return new Rectangle((int)projectile.Center.X - 16, (int)projectile.Center.Y - 16, 32, 32).Intersects(targetHitbox);
//        }
//        public override void AI()
//        {
//            for (int num344 = 0; num344 < 2; num344++)
//            {
//                Dust d = Dust.NewDustPerfect(projectile.Center, MyDustId.ElectricCyan, projectile.oldVelocity, 50, Color.White, 1.2f);
//                d.noGravity = true;
//                d.velocity *= 0.3f;
//            }
//            if (projectile.ai[1] == 0f)
//            {
//                projectile.ai[1] = 1f;
//                SoundEngine.PlaySound(SoundID.Item8, projectile.position);
//            }
//            float maxDis = 48f;
//            NPC target = null;
//            foreach (var npc in Main.npc)
//            {
//                if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy && !npc.dontTakeDamage)
//                {
//                    float dis = Vector2.Distance(npc.Center, projectile.Center);
//                    if (dis < maxDis + Math.Sqrt(npc.width * npc.width + npc.height * npc.height))
//                    {
//                        maxDis = dis;
//                        target = npc;
//                    }
//                }
//            }
//            if (target != null) 
//            {
//                Vector2 targetVec = target.Center - projectile.Center;
//                targetVec.Normalize();
//                targetVec *= 20f;
//                projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
//            }
//            if (projectile.velocity.Length() < 12) 
//            {
//                projectile.velocity.Normalize();
//                projectile.velocity *= 12f;
//            }
//        }
//        public override void Kill(int timeLeft)
//        {
//            SoundEngine.PlaySound(SoundID.Dig, projectile.Center);
//            for (int num344 = 0; num344 < 2; num344++)
//            {
//                Dust d = Dust.NewDustPerfect(projectile.Center, MyDustId.ElectricCyan, projectile.oldVelocity, 50, Color.White, 1.2f);
//                d.noGravity = true;
//                d.velocity *= 0.5f;
//                d.scale *= 1.2f;
//            }
//        }
//    }
//    public class ElectricalBolt_Large : StormProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("大型闪电团");
//        }
//        public override void SetDefaults()
//        {
//            projectile.width = 10;
//            projectile.height = 10;
//            projectile.aiStyle = -1;
//            projectile.alpha = 255;
//            projectile.timeLeft = 300;
//            projectile.DamageType = DamageClass.Magic;
//            projectile.penetrate = 1;
//            projectile.friendly = true;
//        }
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            return new Rectangle((int)projectile.Center.X - 32, (int)projectile.Center.Y - 32, 64, 64).Intersects(targetHitbox);
//        }
//        public override bool OnTileCollide(Vector2 oldVelocity)
//        {
//            if (projectile.velocity.X != oldVelocity.X)
//            {
//                projectile.velocity.X = -oldVelocity.X;
//            }
//            if (projectile.velocity.Y != oldVelocity.Y)
//            {
//                projectile.velocity.Y = -oldVelocity.Y;
//            }
//            return false;
//        }
//        public override void AI()
//        {
//            projectile.ai[0] += MathHelper.TwoPi / 60;
//            for (int num344 = 0; num344 < 2; num344++)
//            {
//                Dust d = Dust.NewDustPerfect(projectile.Center, MyDustId.ElectricCyan, projectile.oldVelocity, 50, Color.White, 1.2f);
//                d.noGravity = true;
//                d.velocity *= 0.3f;
//                Dust d1 = Dust.NewDustPerfect(projectile.Center + new Vector2(32,0).RotatedBy(projectile.ai[0]), MyDustId.PinkBubble, projectile.oldVelocity, 50, Color.White, 1.2f);
//                d1.noGravity = true;
//                d1.velocity *= 0.3f;
//                Dust d2 = Dust.NewDustPerfect(projectile.Center - new Vector2(32, 0).RotatedBy(projectile.ai[0]), MyDustId.PinkBubble, projectile.oldVelocity, 50, Color.White, 1.2f);
//                d2.noGravity = true;
//                d2.velocity *= 0.3f;
//            }
//            if (projectile.ai[1] == 0f)
//            {
//                projectile.ai[1] = 1f;
//                SoundEngine.PlaySound(SoundID.Item8, projectile.position);
//            }
//            float maxDis = 96f;
//            NPC target = null;
//            foreach (var npc in Main.npc)
//            {
//                if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy && !npc.dontTakeDamage)
//                {
//                    float dis = Vector2.Distance(npc.Center, projectile.Center);
//                    if (dis < maxDis + Math.Sqrt(npc.width * npc.width + npc.height * npc.height))
//                    {
//                        maxDis = dis;
//                        target = npc;
//                    }
//                }
//            }
//            if (target != null)
//            {
//                Vector2 targetVec = target.Center - projectile.Center;
//                targetVec.Normalize();
//                targetVec *= 40f;
//                projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
//            }
//            if (projectile.velocity.Length() < 18)
//            {
//                projectile.velocity.Normalize();
//                projectile.velocity *= 18f;
//            }
//        }
//        public override void Kill(int timeLeft)
//        {
//            SoundEngine.PlaySound(SoundID.Dig, projectile.position);
//            for (int num344 = 0; num344 < 10; num344++)
//            {
//                Dust d = Dust.NewDustPerfect(projectile.Center, (Main.rand.NextBool(2)) ? MyDustId.ElectricCyan : MyDustId.PinkBubble, projectile.oldVelocity, 50, Color.White, 1.2f);
//                d.noGravity = true;
//                d.velocity *= 0.5f;
//                d.scale *= 1.2f;
//            }
//            for (int n = 0; n < 4; n++)
//            {
//                Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center, new Vector2(0, -16).RotatedBy(MathHelper.TwoPi / 4 * n), ProjectileType<ElectricalSphere>(), projectile.damage / 4, projectile.knockBack, projectile.owner);
//            }
//        }
//    }
//    public class ElectricalSphere : StormProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("闪电球");
//        }
//        public override void SetDefaults()
//        {
//            projectile.width = 38;
//            projectile.height = 44;
//            projectile.scale = 1f;
//            projectile.friendly = false;
//            projectile.DamageType = DamageClass.Ranged;
//            projectile.ignoreWater = true;
//            projectile.timeLeft = 60;
//            projectile.tileCollide = false;
//            projectile.penetrate = -1;
//            projectile.aiStyle = -1;
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
//        }
//        public override void AI()
//        {
//            Main.projFrames[projectile.type] = 5;
//            projectile.alpha = 255 - 4 * projectile.timeLeft;
//            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.Pi / 30);
//            projectile.frame += ((int)Main.time % 6 == 0) ? 1 : 0;
//            projectile.frame %= 5;
//            Dust d = Dust.NewDustPerfect(projectile.Center, MyDustId.ElectricCyan, projectile.oldVelocity, 50, Color.White, 1.2f);
//            d.noGravity = true;
//            d.velocity *= 0.3f;
//            NPC target = null;
//            float distanceMax = 400f;
//            foreach (NPC npc in Main.npc)
//            {
//                if (npc.active && !npc.friendly)
//                {
//                    float currentDistance = Vector2.Distance(npc.Center, projectile.Center);
//                    if (currentDistance < distanceMax)
//                    {
//                        distanceMax = currentDistance;
//                        target = npc;
//                    }
//                }
//            }
//            if (target != null && projectile.timeLeft % 4 < 1)
//            {
//                Vector2 toTarget = target.Center - projectile.Center;
//                toTarget.Normalize();
//                toTarget *= 6f;
//                toTarget = toTarget.RotatedBy(Main.rand.NextFloatDirection() * 0.3f);
//                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + projectile.velocity * 4f, toTarget, ProjectileType<ElectricalLight>(), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI, 2);
//            }
//        }
//    }
//    public class ElectricalLight : StormProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("电流");
//        }
//        public override void SetDefaults()
//        {
//            projectile.width = 4;
//            projectile.height = 4;
//            projectile.aiStyle = -1;
//            projectile.friendly = true;
//            projectile.timeLeft = 100;
//            projectile.ignoreWater = true;
//            projectile.tileCollide = false;
//            projectile.extraUpdates = 100;
//        }
//        public override void AI()
//        {
//            // 发出红光
//            Lighting.AddLight(projectile.position, 0.5f, 0.0f, 0.5f);

//            // 线性粒子效果
//            for (int i = 0; i < 3; i++)
//            {
//                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.ElectricCyan, 0, 0, 100, Color.White, 1f);
//                d.position = projectile.Center - projectile.velocity * i / 3f;
//                d.velocity *= 0.2f;
//                d.noGravity = true;
//            }

//            // 获取目标NPC
//            NPC target = Main.npc[(int)projectile.ai[0]];
//            // 如果敌对npc是活着的
//            if (target.active)
//            {
//                // 计算朝向目标的向量
//                Vector2 targetVec = target.Center - projectile.Center;
//                targetVec.Normalize();
//                // 目标向量是朝向目标的大小为20的向量
//                targetVec *= 6f;
//                // 朝向npc的单位向量*20 + 3.33%偏移量
//                float n = 0f;
//                projectile.velocity = (projectile.velocity * n + targetVec) / (n + 1);
//            }

//        }
//    }
//    //ranged
//    public class ElectricalBowUnCharged : StormItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("雷鸣");
//            Tooltip.SetDefault("[未充能]");
//        }
//        public override void SetDefaults()
//        {
//            item.rare = ItemRarityID.Green;
//            item.UseSound = SoundID.Item5;
//            item.useStyle = ItemUseStyleID.Shoot;
//            item.damage = 28;
//            item.useAnimation = 24;
//            item.useTime = 24;
//            item.width = 26;
//            item.height = 82;
//            item.scale = 1f;
//            item.knockBack = 1.2f;
//            item.DamageType = DamageClass.Ranged;
//            item.value = Item.sellPrice(0, 0, 20, 0);
//            item.autoReuse = true;
//            item.useAmmo = AmmoID.Arrow;
//            item.shoot = 1;
//            item.shootSpeed = 10;
//        }
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemType<StormBrick>(), 30);
//            recipe.AddIngredient(ItemType<StormSandBrick>(), 10);
//            recipe.AddIngredient(ItemType<StormGlass>(), 10);
//            recipe.AddIngredient(ItemType<StormWood>(), 25);
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalBow : StormItem
//    {
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemType<StormBar>(), 25);
//            recipe.AddIngredient(ItemType<ElectricalBowUnCharged>());
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("雷鸣");
//            Tooltip.SetDefault("离弦之矢势如雷霆");
//        }
//        public override void SetDefaults()
//        {
//            item.rare = ItemRarityID.Orange;
//            item.UseSound = SoundID.Item5;
//            item.useStyle = ItemUseStyleID.Shoot;
//            item.damage = 32;
//            item.useAnimation = 22;
//            item.useTime = 22;
//            item.width = 26;
//            item.height = 82;
//            item.scale = 1f;
//            item.knockBack = 1.5f;
//            item.DamageType = DamageClass.Ranged;
//            item.value = Item.sellPrice(0, 0, 20, 0);
//            item.autoReuse = true;
//            item.useAmmo = AmmoID.Arrow;
//            item.shoot = 1;
//            item.shootSpeed = 12;
//        }
//        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
//        {
//            switch (Main.rand.Next(5))
//            {
//                case 0:
//                    damage += 2;
//                    type = ProjectileType<ElectricalBowArrow1>();
//                    break;
//                case 1:
//                    damage += 4;
//                    type = ProjectileType<ElectricalBowArrow2>();
//                    break;
//            }
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalBowArrow1 : StormProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
//            DisplayName.SetDefault("雷鸣");
//        }

//        public override void SetDefaults()
//        {
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.scale = 1f;
//            projectile.friendly = true;
//            projectile.DamageType = DamageClass.Melee;
//            projectile.ignoreWater = true;
//            projectile.timeLeft = 150;
//            projectile.tileCollide = true;
//            projectile.penetrate = 1;
//            projectile.light = 0.5f;
//            projectile.aiStyle = -1;
//        }
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            return targetHitbox.Intersects(new Rectangle((int)projectile.Center.X - 8, (int)projectile.Center.Y - 8, 16, 16));
//        }
//        public override void AI()
//        {
//            projectile.velocity.Y += 0.1f;
//            projectile.rotation = projectile.velocity.ToRotation();
//        }
//        public override void Kill(int timeLeft)
//        {
//            for (int i = 0; i < 30; i++)
//            {
//                base.Kill(timeLeft);
//                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.ElectricCyan, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, Color.White, 1.5f);
//                d.noGravity = true;
//            }
//            foreach (var npc in Main.npc)
//            {
//                if (npc.active && npc.CanBeChasedBy() && npc.type != NPCID.TargetDummy && (npc.Center - projectile.Center).Length() < 256f)
//                {
//                    npc.velocity = Vector2.Normalize(projectile.Center - npc.Center) * 16;
//                }
//            }
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            Utils.IllusionBoundExtensionMethods.DrawShaderTail(spriteBatch, projectile, ShaderTailTexture.Frozen, ShaderTailStyle.Light, 8);
//            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(20, 11), 1f, SpriteEffects.None, 0);
//            spriteBatch.Draw(IllusionBoundMod.GetTexture("Items/Electrical/ElectricalBowArrow1_g"), projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(20, 11), 1f, SpriteEffects.None, 0);
//            return false;
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    public class ElectricalBowArrow2 : StormProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
//            DisplayName.SetDefault("雷鸣");
//        }

//        public override void SetDefaults()
//        {
//            projectile.width = 1;
//            projectile.height = 1;
//            projectile.scale = 1f;
//            projectile.friendly = true;
//            projectile.DamageType = DamageClass.Melee;
//            projectile.ignoreWater = true;
//            projectile.timeLeft = 150;
//            projectile.tileCollide = true;
//            projectile.penetrate = 1;
//            projectile.light = 0.5f;
//            projectile.aiStyle = -1;
//        }
//        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
//        {
//            return targetHitbox.Intersects(new Rectangle((int)projectile.Center.X - 8, (int)projectile.Center.Y - 8, 16, 16));
//        }
//        public override void AI()
//        {
//            projectile.velocity.Y += 0.1f;
//            projectile.rotation = projectile.velocity.ToRotation();
//        }
//        public override void Kill(int timeLeft)
//        {
//            for (int i = 0; i < 30; i++)
//            {
//                Dust.NewDustPerfect(projectile.position, MyDustId.ElectricCyan, (MathHelper.TwoPi / 30f * i).ToRotationVector2() * 4, 100, Color.White, 1).noGravity = true;
//                Dust.NewDustPerfect(projectile.position + (MathHelper.TwoPi / 30f * i).ToRotationVector2() * 32, MyDustId.ElectricCyan, -4 * (MathHelper.TwoPi / 30f * i).ToRotationVector2(), 100, Color.White, 1).noGravity = true;
//            }
//            foreach (NPC npc in Main.npc)
//            {
//                if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy && (npc.Center - projectile.Center).Length() < 64f)
//                {
//                    Main.player[projectile.owner].ApplyDamageToNPC(npc, projectile.damage / 2, projectile.knockBack, projectile.Center.X - npc.Center.X >= 0 ? 1 : -1, true);
//                }
//            }
//        }
//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            Texture2D projectileTexture = TextureAssets.Projectile[projectile.type].Value;
//            for (int k = 0; k < projectile.oldPos.Length; k++)
//            {
//                spriteBatch.Draw(projectileTexture, projectile.oldPos[k] - Main.screenPosition, null, new Color(0, 255, 255), projectile.rotation, new Vector2(22, 9), projectile.scale - 0.1f * k, SpriteEffects.None, 0f);
//            }
//            spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(22, 9), 1f, SpriteEffects.None, 0);
//            spriteBatch.Draw(IllusionBoundMod.GetTexture("Items/Electrical/ElectricalBowArrow2_g"), projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(22, 9), 1f, SpriteEffects.None, 0);
//            return false;
//        }
//        public override Color? GetAlpha(Color lightColor)
//        {
//            return Color.White;
//        }
//    }
//    //材料
//    public class StormBar : StormItem 
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("风暴锭");
//            Tooltip.SetDefault("浓缩着狂风暴雨与十万伏特");
//        }
//        public override void SetDefaults()
//        {
//            item.width = 30;
//            item.height = 24;
//            item.maxStack = 99;
//            item.value = Item.buyPrice(silver: 50);
//            item.rare = ItemRarityID.Orange;
//        }
//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddRecipeGroup("IllusionBoundMod:GoldBar");
//            recipe.AddIngredient(ItemID.Hellstone, 2);
//            recipe.AddIngredient(ItemType<StormOre>(), 4);
//            recipe.AddTile(TileType<Soviet.SovietHammerStatue>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//    }
//    public class StormWood : StormItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("风暴木");
//            Tooltip.SetDefault("生长在密布的乌云下，任由风吹雨打，你不由得对生命的力量为之一振并肃然起敬。");
//        }
//        public override void SetDefaults()
//        {
//            item.width = 30;
//            item.height = 24;
//            item.maxStack = 999;
//            item.rare = ItemRarityID.Blue;
//        }
//    }
//    //public class SGI : GlobalItem
//    //{
//    //    public override void HoldItem(Item item, Player player)
//    //    {
//    //        Main.NewText(new Vector2(item.useTime, item.useAnimation));
//    //    }
//    //}
//    //Others
//    public class StormSolution : StormItem
//    {
//        // Token: 0x060049AD RID: 18861 RVA: 0x00362670 File Offset: 0x00360870
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("风暴溶液");
//            Tooltip.SetDefault("环境改造枪的子弹\n让暴风雨来得更猛烈些吧（笑");
//        }

//        // Token: 0x060049AE RID: 18862 RVA: 0x003626C8 File Offset: 0x003608C8
//        public override void SetDefaults()
//        {
//            base.item.ammo = AmmoID.Solution;
//            base.item.shoot = ProjectileType<StormSpray>() - 145;
//            base.item.width = 10;
//            base.item.height = 12;
//            base.item.rare = 3;
//            base.item.maxStack = 999;
//            base.item.consumable = true;
//        }

//        // Token: 0x060049AF RID: 18863 RVA: 0x0001B4E1 File Offset: 0x000196E1
//        public override bool CanBeConsumedAsAmmo(Item weapon, Player player)
//        {
//            return player.itemAnimation >= player.HeldItem.useAnimation - 3;
//        }
//    }
//    public class StormGlobalSpray : GlobalProjectile
//    {
//        public override void PostAI(Projectile projectile)
//        {
//            int num = (int)(projectile.Center.X / 16f);
//            int num2 = (int)(projectile.Center.Y / 16f);
//            for (int i = num - 1; i <= num + 1; i++)
//            {
//                for (int j = num2 - 1; j <= num2 + 1; j++)
//                {
//                    if (projectile.type == 145)
//                    {
//                        Utils.IllusionBoundExtensionMethods.ConvertFromStorm(i, j, ConvertType.Pure);
//                        WorldGen.SquareTileFrame(i, j, true);
//                    }
//                    if (projectile.type == 147)
//                    {
//                        Utils.IllusionBoundExtensionMethods.ConvertFromStorm(i, j, ConvertType.Corrupt);
//                        WorldGen.SquareTileFrame(i, j, true);
//                    }
//                    if (projectile.type == 149)
//                    {
//                        Utils.IllusionBoundExtensionMethods.ConvertFromStorm(i, j, ConvertType.Crimson);
//                        WorldGen.SquareTileFrame(i, j, true);
//                    }
//                    if (projectile.type == 146)
//                    {
//                        Utils.IllusionBoundExtensionMethods.ConvertFromStorm(i, j, ConvertType.Hallow);
//                        WorldGen.SquareTileFrame(i, j, true);
//                    }
//                }
//            }
//        }
//    }
//    public class StormSpray : StormProjectile
//    {
//        // Token: 0x06000B9E RID: 2974 RVA: 0x00087FA4 File Offset: 0x000861A4
//        public override void SetDefaults()
//        {
//            projectile.width = 16;
//            projectile.height = 16;
//            projectile.aiStyle = -1;
//            projectile.friendly = false;
//            projectile.alpha = 255;
//            projectile.penetrate = -1;
//            projectile.extraUpdates = 2;
//            projectile.tileCollide = false;
//            projectile.ignoreWater = true;
//        }

//        // Token: 0x06000B9F RID: 2975 RVA: 0x00002DBD File Offset: 0x00000FBD

//        // Token: 0x06000BA0 RID: 2976 RVA: 0x00088024 File Offset: 0x00086224
//        public override bool PreAI()
//        {
//            if (base.projectile.owner == Main.myPlayer)
//            {
//                int num = (int)(base.projectile.Center.X / 16f);
//                int num2 = (int)(base.projectile.Center.Y / 16f);
//                Utils.IllusionBoundExtensionMethods.ConvertToStorm(num - 1, num + 1, num2 - 1, num2 + 1);
//                WorldGen.SquareTileFrame(num, num2, true);
//            }
//            if (base.projectile.timeLeft > 133)
//            {
//                base.projectile.timeLeft = 133;
//            }
//            if (base.projectile.ai[0] > 7f)
//            {
//                float num3 = 1f;
//                if (base.projectile.ai[0] == 8f)
//                {
//                    num3 = 0.2f;
//                }
//                else if (base.projectile.ai[0] == 9f)
//                {
//                    num3 = 0.4f;
//                }
//                else if (base.projectile.ai[0] == 10f)
//                {
//                    num3 = 0.6f;
//                }
//                else if (base.projectile.ai[0] == 11f)
//                {
//                    num3 = 0.8f;
//                }
//                base.projectile.ai[0] += 1f;
//                for (int i = 0; i < 1; i++)
//                {
//                    int num4 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, MyDustId.ElectricCyan, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default, 1f);
//                    Main.dust[num4].noGravity = true;
//                    Main.dust[num4].scale *= 1.75f * num3;
//                    Dust dust = Main.dust[num4];
//                    dust.velocity.X = dust.velocity.X * 2f;
//                    Dust dust2 = Main.dust[num4];
//                    dust2.velocity.Y = dust2.velocity.Y * 2f;
//                }
//            }
//            else
//            {
//                base.projectile.ai[0] += 1f;
//            }
//            base.projectile.rotation += 0.3f * (float)base.projectile.direction;
//            return false;
//        }
//    }
//    //public class ElectricalPlayer : ModPlayer 
//    //{

//    //}
//    //盔甲
//    [AutoloadEquip(EquipType.Head)]
//    public class ElectricalFeatherHelmet : StormItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("雷羽发饰");
//            Tooltip.SetDefault("你觉得比起实用的头盔，有时还是更适合戴上拉风一些的头饰。\n毕竟在风暴之地这么危险的地方，是不是头盔都难存活（");
//            //ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
//            //ArmorIDs.Head.Sets.DrawHead[equipSlotHeadAlt] = false;
//            int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
//            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
//            ArmorIDs.Head.Sets.DrawHatHair[equipSlotHead] = true;
//            //ArmorIDs.Head.Sets.DrawHead[equipSlotHeadAlt] = false;
//        }
//        public override void SetDefaults()
//        {
//            item.width = 18;
//            item.height = 18;
//            item.value = 10000;
//            item.rare = ItemRarityID.Orange;
//            item.defense = 7;
//        }
//        //public override bool DrawHead()
//        //{
//        //    SpriteBatch spriteBatch = Main.spriteBatch;
//        //    var player = Main.LocalPlayer;
//        //    //Main.spriteBatch.End();
//        //    //Main.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//        //    spriteBatch.Draw(GetTexture(Texture + "_Head_Glow"), player.Center - Main.screenPosition /*- new Vector2(20, 31)*/ + new Vector2(0, player.gfxOffY), player.headFrame, Color.White, 0, default, 1f, player.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 1);
//        //    return true;
//        //}
//        public override void UpdateEquip(Player player)
//        {
//            base.UpdateEquip(player);
//        }
//        public override bool IsArmorSet(Item head, Item body, Item legs)
//        {
//            return body.type == ItemType<ElectricalFeatherBreastplate>() && legs.type == ItemType<ElectricalFeatherLeggings>();
//        }

//        public override void UpdateArmorSet(Player player)
//        {
//            player.setBonus = "适应「雷电」程度的能力\n你将如闪电般归来。";
//            player.runAcceleration += 0.2f;
//            player.GetDamage(DamageClass.Generic) += 0.05f;
//        }
//    }
//    [AutoloadEquip(EquipType.Body)]//, EquipType.Wings
//    public class ElectricalFeatherBreastplate : StormItem
//    {

//        //public bool Down => player.controlDown && player.controlJump && player.wingTime > 0f;
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("雷羽胸甲");
//            Tooltip.SetDefault("轻盈坚硬的羽甲和雷霆之翼更配哦");
//            int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
//            ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = false;
//            ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = false;
//        }
//        //public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
//        //{
//        //    if (Down)
//        //    {
//        //        ascentWhenFalling = 0f;
//        //        ascentWhenRising = 0f;
//        //        maxCanAscendMultiplier = 0f;
//        //        maxAscentMultiplier = 0f;
//        //        constantAscend = 0f;
//        //    }
//        //    else
//        //    {
//        //        ascentWhenFalling = 0.85f;
//        //        ascentWhenRising = 0.15f;
//        //        maxCanAscendMultiplier = 1f;
//        //        maxAscentMultiplier = 3f;
//        //        constantAscend = 0.135f;
//        //    }
//        //}

//        //public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
//        //{
//        //    if (Down)
//        //    {
//        //        speed = 108f;
//        //        acceleration *= 30f;
//        //    }
//        //    else
//        //    {
//        //        speed = 36f;
//        //        acceleration *= 10f;
//        //    }
//        //}
//        public override void SetDefaults()
//        {
//            item.width = 18;
//            item.height = 18;
//            item.value = 10000;
//            item.rare = ItemRarityID.Orange;
//            item.defense = 8;
//        }
//        //public override bool DrawBody()
//        //{
//        //    SpriteBatch spriteBatch = Main.spriteBatch;
//        //    var player = Main.LocalPlayer;
//        //    //Main.spriteBatch.End();
//        //    //Main.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//        //    spriteBatch.Draw(GetTexture(Texture + (player.Male ? "_Body" : "_Female") + "_Glow"), player.Center - Main.screenPosition /*- new Vector2(20, 31)*/ + new Vector2(0, player.gfxOffY), player.bodyFrame, Color.White, 0, default, 1f, player.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 1);
//        //    return true;
//        //}
//        //public override void DrawHands(ref bool drawHands, ref bool drawArms)
//        //{
//        //    drawHands = true;
//        //}
//        //Player player => Main.player[item.owner];
//        public override void UpdateEquip(Player player)
//        {
//            //player.wingTimeMax = 1800;
//            //player.wingTime = 1800;
//            //if (Down && !player.merman)
//            //{
//            //    player.velocity.Y *= 0.9f;
//            //    if (player.velocity.Y > -2f && player.velocity.Y < 1f)
//            //    {
//            //        player.velocity.Y = 1E-05f;
//            //    }
//            //}
//        }
//    }
//    [AutoloadEquip(EquipType.Legs)]
//    public class ElectricalFeatherLeggings : StormItem
//    {
//        //public override bool DrawLegs()
//        //{
//        //    SpriteBatch spriteBatch = Main.spriteBatch;
//        //    var player = Main.LocalPlayer;
//        //    //Main.spriteBatch.End();
//        //    //Main.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
//        //    spriteBatch.Draw(GetTexture(Texture + "_Legs_Glow"), player.Center - Main.screenPosition /*- new Vector2(20, 31)*/ + new Vector2(0, player.gfxOffY), player.legFrame, Color.White, 0, default, 1f, player.direction == 1 ? 0 : SpriteEffects.FlipHorizontally, 1);
//        //    return true;
//        //}
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("雷羽护胫");
//            Tooltip.SetDefault("你看，这个闪电靴，就是逊啦");
//            int equipSlotLegs = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
//            ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = false;
//        }
//        public override void SetDefaults()
//        {
//            item.width = 22;
//            item.height = 18;
//            item.value = 10000;
//            item.rare = ItemRarityID.Orange;
//            item.defense = 7;
//        }
//        public override void UpdateEquip(Player player)
//        {
//            player.moveSpeed += 0.9f;
//        }

//    }

//}
