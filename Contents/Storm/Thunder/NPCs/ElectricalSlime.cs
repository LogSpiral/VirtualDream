using System;

using Terraria.ID;

using static Terraria.ModLoader.ModContent;

namespace VirtualDream.Contents.Storm.Thunder.NPCs
{
    // This ModNPC serves as an example of a complete AI example.
    public class PositiveElectricalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("正电史莱姆");
            Main.npcFrameCount[npc.type] = 10;
        }

        private NPC npc => NPC;

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 26;
            npc.aiStyle = -1;
            npc.damage = 35;
            npc.defense = 18;
            npc.noGravity = true;
            npc.lifeMax = 150;
            npc.HitSound = SoundID.NPCHit1;
            npc.knockBackResist = 1f;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 300f;
            //NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
        //    return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.8f : 0;
        //}
        private int counter;
        private int counter2;
        private bool theFarthest;
        public override void AI()
        {
            npc.TargetClosest(false);
            float sign = npc.velocity.X / Math.Abs(npc.velocity.X);
            float k = 16 / (16 + Math.Abs(npc.velocity.X));
            npc.rotation = MathHelper.Pi - MathHelper.PiOver2 * sign * (k - 1);
            float sgn = float.IsNaN(npc.velocity.X) ? 1 : npc.velocity.X;
            if (Math.Sign(sgn) != 0)
            {
                npc.spriteDirection = -Math.Sign(sgn);
            }
            if (npc.rotation < -1.57079637f)
            {
                npc.rotation += 3.14159274f;
            }
            if (npc.rotation > 1.57079637f)
            {
                npc.rotation -= 3.14159274f;
            }
            npc.spriteDirection = Math.Sign(sgn);
            int num1011 = (npc.ai[0] >= 0) ? 2 : 1;
            int num1012 = (npc.ai[0] >= 0) ? 30 : 20;
            for (int num1013 = 0; num1013 < 2; num1013++)
            {
                if (Main.rand.Next(3) < num1011)
                {
                    int num1014 = Dust.NewDust(npc.Center - new Vector2(num1012), num1012 * 2, num1012 * 2, MyDustId.ElectricCyan, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 90, Color.White, 0.75f);
                    Main.dust[num1014].noGravity = true;
                    Dust dust3 = Main.dust[num1014];
                    dust3.velocity *= 0.2f;
                    Main.dust[num1014].fadeIn = 1f;
                }
            }
            if (npc.ai[0] >= 0)
            {
                npc.ai[0]--;
                //Vector2 center6 = npc.Center;
                //Vector2 center7 = Main.player[npc.target].Center;
                //Vector2 vec2 = center7 - center6;
                //vec2.Normalize();
                //if (vec2.HasNaNs())
                //{
                //	vec2 = new Vector2((float)npc.direction, 0f);
                //}
                //npc.velocity = (npc.velocity * 59 + vec2 * (npc.velocity.Length() * 0.3f + 18)) / 60;
                if ((int)npc.ai[0] == 0)
                {
                    if (counter == 3)
                    {
                        counter = 0;
                    }
                    if ((npc.Center - Main.player[npc.target].Center).Length() <= 64)
                    {
                        counter++;
                    }
                }
                if (counter == 3)
                {
                    Vector2 vec = npc.Center - Main.player[npc.target].Center;
                    vec.Normalize();
                    npc.velocity = vec * 4;
                }
                else
                {
                    npc.velocity = (npc.velocity * 59 + new Vector2(npc.ai[2], npc.ai[3]) * (npc.velocity.Length() * 0.3f + 18)) / 60;
                }
            }
            else
            {
                if ((int)npc.ai[1] == -15 && (npc.Center - Main.player[npc.target].Center).Length() >= 128 && !matched)
                {
                    Vector2 center6 = npc.Center;
                    Vector2 center7 = Main.player[npc.target].Center;
                    Vector2 vec2 = center7 - center6;
                    vec2.Normalize();
                    if (vec2.HasNaNs())
                    {
                        vec2 = new Vector2(npc.direction, 0f);
                    }
                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vec2 * 4f, ProjectileType<TouhouProject.NPCs.Fairy.CrystalBullet>(), 15, 1, Main.myPlayer, 6);
                }
                npc.ai[1]++;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                npc.velocity *= 0.975f;
            }
            if (npc.ai[1] >= 0)
            {
                npc.ai[1] = -30;
                npc.ai[0] = 15;
                Vector2 center6 = npc.Center;
                Vector2 center7 = Main.player[npc.target].Center;
                Vector2 vec2 = center7 - center6;
                vec2.Normalize();
                if (vec2.HasNaNs())
                {
                    vec2 = new Vector2(npc.direction, 0f);
                }
                npc.ai[2] = vec2.X;
                npc.ai[3] = vec2.Y;
            }
            if ((int)Main.time % 240 == 0)
            {
                int index = -1;
                float distanceMin = 0;
                Player player = Main.player[npc.target];
                if (player != null)
                {
                    foreach (var tar in Main.npc)
                    {
                        if ((tar.Center - player.Center).Length() > distanceMin && (tar.Center - player.Center).Length() < 1024 && tar.type == NPCType<PositiveElectricalSlime>())
                        {

                            index = distanceMin == 0 ? -1 : tar.whoAmI;
                            distanceMin = (tar.Center - player.Center).Length();
                        }
                    }
                    theFarthest = index == npc.whoAmI;
                }
            }
            if (theFarthest)
            {
                counter2++;
                if (counter2 < 61)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        float rand = Main.rand.NextFloat(0, MathHelper.TwoPi);
                        Dust dust = Dust.NewDustPerfect(npc.Center + new Vector2(32, 0).RotatedBy(rand), MyDustId.ElectricCyan, new Vector2(-2, 0).RotatedBy(rand), newColor: Color.White);
                        //Dust dust = Dust.NewDustPerfect(npc.Center + new Vector2(32, 0).RotatedBy(MathHelper.TwoPi / 10 * n), MyDustId.ElectricCyan, new Vector2(-2, 0).RotatedBy(MathHelper.TwoPi / 10 * n), newColor: Color.White);
                        dust.noGravity = true;
                    }
                }
            }
            else
            {
                counter2 = 0;
                //counter2 -= ((int)Main.time % 3 == 0 && counter2 > 0) ? 1 : 0;
            }
            if (counter2 == 60)
            {
                foreach (var tar in Main.npc)
                {
                    if (tar.whoAmI != npc.whoAmI && tar.type == NPCType<PositiveElectricalSlime>())
                    {
                        tar.velocity += Vector2.Normalize(tar.Center - npc.Center) * MathHelper.Clamp(512f / (tar.Center - npc.Center).Length(), 0.25f, 16);
                        //tar.velocity += Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * MathHelper.Clamp(256f / (tar.Center - npc.Center).Length(), 0.25f, 16);
                    }
                }
            }
            for (int num31 = npc.oldPos.Length - 1; num31 > 0; num31--)
            {
                npc.oldPos[num31] = npc.oldPos[num31 - 1];
            }
            npc.oldPos[0] = npc.Center;
            if ((int)Main.time % 6 == 0)
            {
                float distanceMax = 48;
                int index = -1;
                foreach (var target in Main.npc)
                {
                    float distance = (target.Center - npc.Center).Length();
                    if (target.type == NPCType<NegativeElectricalSlime>() && distance < distanceMax)
                    {
                        distanceMax = distance;
                        index = target.whoAmI;
                    }
                }
                if (index != -1)
                {
                    Vector2 vec = (npc.Center + MatchedNPC.Center) / 2;
                    for (int r = 0; r < 36; r++)
                    {
                        Dust dust1 = Dust.NewDustPerfect(vec - new Vector2(32, 0).RotatedBy(MathHelper.TwoPi / 36 * r), MyDustId.BlackMaterial, new Vector2(4, 0).RotatedBy(MathHelper.TwoPi / 36 * k), newColor: Color.White);
                        dust1.noGravity = true;
                    }
                    int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)vec.X, (int)vec.Y, NPCType<NeutralSlime>(), Target: npc.target);
                    Main.npc[n].life = (int)(Main.npc[n].lifeMax * (((float)Main.npc[index].life / Main.npc[index].lifeMax) + ((float)npc.life / npc.lifeMax)) / 2f);
                    Main.npc[index].life -= 2147483647;
                    npc.life -= 2147483647;
                }
            }

            MatchNegativeSlime();
        }
        public bool matched;
        private int matchIndex = -1;
        private NPC MatchedNPC
        {
            get
            {
                return matchIndex != -1 ? Main.npc[matchIndex] : null;
            }
        }
        private NegativeElectricalSlime MatchedNegativeSlime
        {
            get
            {
                return MatchedNPC != null ? MatchedNPC.ModNPC as NegativeElectricalSlime : null;
            }
        }
        public override bool PreKill()
        {
            if (MatchedNPC != null)
            {
                MatchedNegativeSlime.matched = false;
            }
            return true;
        }
        private void MatchNegativeSlime()
        {
            if (!matched && (int)Main.time % 60 == 0)
            {
                float distanceMax = 512f;
                int index = -1;
                foreach (var target in Main.npc)
                {
                    float distance = (npc.Center - target.Center).Length();
                    if (target.type == NPCType<NegativeElectricalSlime>() && !(target.ModNPC as NegativeElectricalSlime).matched && distance < distanceMax)
                    {
                        distanceMax = distance;
                        index = target.whoAmI;
                    }
                }
                if (index != -1)
                {
                    matchIndex = index;
                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, default, ProjectileType<ElectricalChain>(), npc.damage / 3, 5, Main.myPlayer, npc.whoAmI, MatchedNPC.whoAmI);
                }
            }
            if (matchIndex == -1)
            {
                return;
            }
            //LinerDust(npc.Center, MatchedNPC.Center);
            if (!MatchedNPC.active || (MatchedNPC.Center - npc.Center).Length() > 512f || MatchedNPC.type != NPCType<NegativeElectricalSlime>())
            {
                MatchedNegativeSlime.matched = false;
                matched = false;
                matchIndex = -1;
            }
            else
            {
                matched = true;
                MatchedNegativeSlime.matched = true;
            }

        }

        //public override void AI()
        //{
        //	Main.NewText(npc.ai[0]);
        //	npc.TargetClosest(false);
        //	npc.rotation = npc.velocity.ToRotation();
        //	if (Math.Sign(npc.velocity.X) != 0)
        //	{
        //		npc.spriteDirection = -Math.Sign(npc.velocity.X);
        //	}
        //	if (npc.rotation < -1.57079637f)
        //	{
        //		npc.rotation += 3.14159274f;
        //	}
        //	if (npc.rotation > 1.57079637f)
        //	{
        //		npc.rotation -= 3.14159274f;
        //	}
        //	npc.spriteDirection = Math.Sign(npc.velocity.X);
        //	#region 参数设置
        //	float knockBackReistConst = 0.3f;//0.3f的击退抗性
        //	float scaler = 4f;//设置部分速度向量模长 原8
        //	float height = 300f;//玩家中心纵向偏移量
        //	float distanceMax = 800f;//最大距离
        //	float value = 60f;//加权平均权重
        //	float countMax = 5f;//最大计数
        //	float slowerScale = 0.8f;//减速标量
        //	int randArea = 0;//随机值范围大小
        //	float VelScaler = 5f;//设置速度向量模长 原10
        //	float countMax2 = 5f;
        //	float distanceMax2 = 150f;
        //	float value2 = 120f;//加权平均权重2 原60
        //	float num1008 = 0.1f;//控制冲刺部分速度 原1/3f
        //	float VelMax = 4f;//判断当前速度模长是否大于8所用的标量 原8
        //					  //bool flag63 = false;
        //	num1008 *= value2;
        //	if (Main.expertMode)
        //	{
        //		knockBackReistConst *= Main.expertKnockBack;
        //	}
        //	#endregion
        //	#region 粒子效果
        //	int num1011 = (npc.ai[0] == 2f) ? 2 : 1;
        //	int num1012 = (npc.ai[0] == 2f) ? 30 : 20;
        //	for (int num1013 = 0; num1013 < 2; num1013++)
        //	{
        //		if (Main.rand.Next(3) < num1011)
        //		{
        //			int num1014 = Dust.NewDust(npc.Center - new Vector2((float)num1012), num1012 * 2, num1012 * 2, MyDustId.ElectricCyan, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 90, Color.White, 1f);
        //			Main.dust[num1014].noGravity = true;
        //			Dust dust3 = Main.dust[num1014];
        //			dust3.velocity *= 0.2f;
        //			Main.dust[num1014].fadeIn = 1f;
        //		}
        //	}
        //	#endregion
        //	#region 攻击
        //	if (npc.ai[0] == 0f)//状态一:寻找目标
        //	{
        //		npc.knockBackResist = knockBackReistConst;//0.3f的击退抗性
        //		float scaleFactor6 = scaler;//模长为8
        //		Vector2 center4 = npc.Center;//NPC中心
        //		Vector2 center5 = Main.player[npc.target].Center;//玩家中心
        //		Vector2 vector131 = center5 - center4;//朝向玩家的向量
        //		Vector2 vector132 = vector131 - Vector2.UnitY * height;//朝向玩家上方300像素的向量
        //		float num1015 = vector131.Length();//NPC和玩家的距离
        //		vector131 = Vector2.Normalize(vector131) * scaleFactor6;//模长改为8
        //		vector132 = Vector2.Normalize(vector132) * scaleFactor6;//模长改为8
        //		bool flag64 = Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1);//判断是否可攻击？
        //		if (npc.ai[3] >= 120f)
        //		{
        //			flag64 = true;
        //		}
        //		float num1016 = 8f;
        //		flag64 = (flag64 && vector131.ToRotation() > 3.14159274f / num1016 && vector131.ToRotation() < 3.14159274f - 3.14159274f / num1016);
        //		if (num1015 > distanceMax || !flag64)//如果玩家和npc的距离超过800像素或着ai[3]的计数未达到120
        //		{
        //			//npc.velocity.X = (npc.velocity.X * (num1002 - 1f) + vector132.X) / num1002;
        //			//npc.velocity.Y = (npc.velocity.Y * (num1002 - 1f) + vector132.Y) / num1002;
        //			npc.velocity = (npc.velocity * (value - 1) + vector132) / value;//速度向量加权平均
        //			if (!flag64) //如果ai[3]的计数未达到120
        //			{
        //				npc.ai[3] += 1f;//ai[3]增加
        //				if (npc.ai[3] == 120f)
        //				{
        //					npc.netUpdate = true;//重要更新
        //				}
        //			}
        //			else
        //			{
        //				npc.ai[3] = 0f;//重置ai[3]
        //			}
        //		}
        //		else//计数未达到120并且距离小于800
        //		{
        //			npc.ai[0] = 1f;//切换至状态二
        //			npc.ai[2] = vector131.X;//用ai[2] 、 ai[2]存储朝向玩家模长为8的向量
        //			npc.ai[3] = vector131.Y;//
        //			npc.netUpdate = true;
        //		}
        //	}
        //	else if (npc.ai[0] == 1f)//状态二:找到目标后准备冲刺
        //	{
        //		npc.knockBackResist = 0f;//击退抗性拉满
        //		npc.velocity *= slowerScale;//速度每帧乘0.8f(减速，准备冲刺
        //		npc.ai[1] += 1f;//计数增加
        //		if (npc.ai[1] >= countMax)//计数大于等于5时
        //		{
        //			npc.ai[0] = 2f;//切换状态至三
        //			npc.ai[1] = 0f;//计数归零
        //			npc.netUpdate = true;//重要更新  5帧前获取的向量                                         横纵都取0到0.04的随机数作为偏移量（意义不大？
        //			Vector2 velocity = new Vector2(npc.ai[2], npc.ai[3]) + new Vector2((float)Main.rand.Next(-randArea, randArea + 1), (float)Main.rand.Next(-randArea, randArea + 1)) * 0.04f;
        //			velocity.Normalize();
        //			velocity *= VelScaler;//模长设置为10
        //			npc.velocity = velocity;//作为NPC的速度向量
        //		}
        //	}
        //	else if (npc.ai[0] == 2f)//状态三:冲！
        //	{
        //		npc.knockBackResist = 0f;
        //		float num1018 = countMax2;//ai[1]计数大于30时
        //		npc.ai[1] += 1f;
        //		bool flag65 = Vector2.Distance(npc.Center, Main.player[npc.target].Center) > distanceMax2 && npc.Center.Y > Main.player[npc.target].Center.Y;//NPC到玩家的距离大于150像素并且NPC在玩家下方
        //		if ((npc.ai[1] >= num1018 && flag65) || npc.velocity.Length() < VelMax)//计数大于30和flag65同时成立或者速度向量模长小于8成立
        //		{
        //			//npc.ai[0] = 0f;
        //			//npc.ai[1] = 0f;
        //			npc.ai[0] = 4f;//切换至状态四
        //			npc.ai[1] = 45f;//ai[1]计数设置为45
        //			npc.ai[2] = 0f;
        //			npc.ai[3] = 0f;
        //			npc.velocity /= 2f;//速度减半
        //			npc.netUpdate = true;
        //		}
        //		else //计数大于30和flag65有一个不成立并且速度向量模长大于于8
        //		{
        //			Vector2 center6 = npc.Center;
        //			Vector2 center7 = Main.player[npc.target].Center;
        //			Vector2 vec2 = center7 - center6;//朝向玩家的向量
        //			vec2.Normalize();
        //			if (vec2.HasNaNs())
        //			{
        //				vec2 = new Vector2((float)npc.direction, 0f);//如果失去目标(? 就将这个向量设置为npc朝向
        //			}
        //			//                             59的权重               20加上速度向量模长作为权重         除去60
        //			npc.velocity = (npc.velocity * (value2 - 1f) + vec2 * (npc.velocity.Length() + num1008)) / value2;//速度向量越大越容易改变为目标向量
        //		}
        //	}
        //	else if (npc.ai[0] == 4f)//状态四;减速
        //	{
        //		npc.ai[1] -= 3f;//计数每帧减去3
        //		if (npc.ai[1] <= 0f)//15帧后
        //		{
        //			if (counter >= 2)
        //			{
        //				counter = 0;
        //				npc.ai[0] = 0;
        //				npc.ai[1] = 0f;
        //			}
        //			else 
        //			{
        //				counter++;
        //				npc.ai[0] = 1;
        //				npc.ai[1] = 0f;
        //				Vector2 vec = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 4;
        //				npc.ai[2] = vec.X;
        //				npc.ai[3] = vec.Y;
        //			}
        //			npc.netUpdate = true;
        //		}
        //		npc.velocity *= 0.95f;//速度每帧乘0.95f
        //	}
        //	#endregion
        //}
        //public override void AI()
        //{
        //	npc.TargetClosest(false);
        //	npc.rotation = npc.velocity.ToRotation();
        //	if (Math.Sign(npc.velocity.X) != 0)
        //	{
        //		npc.spriteDirection = -Math.Sign(npc.velocity.X);
        //	}
        //	if (npc.rotation < -1.57079637f)
        //	{
        //		npc.rotation += 3.14159274f;
        //	}
        //	if (npc.rotation > 1.57079637f)
        //	{
        //		npc.rotation -= 3.14159274f;
        //	}
        //	npc.spriteDirection = Math.Sign(npc.velocity.X);
        //          #region 参数设置
        //          float knockBackReistConst = 0.3f;//0.3f的击退抗性
        //	float scaler = 4f;//设置部分速度向量模长 原8
        //	float height = 300f;//玩家中心纵向偏移量
        //	float distanceMax = 800f;//最大距离
        //	float value = 60f;//加权平均权重
        //	float countMax = 5f;//最大计数
        //	float slowerScale = 0.8f;//减速标量
        //	int randArea = 0;//随机值范围大小
        //	float VelScaler = 5f;//设置速度向量模长 原10
        //	float countMax2 = 30f;
        //	float distanceMax2 = 150f;
        //	float value2 = 120f;//加权平均权重2 原60
        //	float num1008 = 0.1f;//控制冲刺部分速度 原1/3f
        //	float VelMax = 4f;//判断当前速度模长是否大于8所用的标量 原8
        //	//bool flag63 = false;
        //	num1008 *= value2;
        //	if (Main.expertMode)
        //	{
        //		knockBackReistConst *= Main.expertKnockBack;
        //	}
        //          #endregion
        //          #region 粒子效果
        //          int num1011 = (npc.ai[0] == 2f) ? 2 : 1;
        //	int num1012 = (npc.ai[0] == 2f) ? 30 : 20;
        //	for (int num1013 = 0; num1013 < 2; num1013++)
        //	{
        //		if (Main.rand.Next(3) < num1011)
        //		{
        //			int num1014 = Dust.NewDust(npc.Center - new Vector2((float)num1012), num1012 * 2, num1012 * 2, MyDustId.ElectricCyan, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 90, Color.White, 1f);
        //			Main.dust[num1014].noGravity = true;
        //			Dust dust3 = Main.dust[num1014];
        //			dust3.velocity *= 0.2f;
        //			Main.dust[num1014].fadeIn = 1f;
        //		}
        //	}
        //          #endregion
        //          #region 攻击
        //          if (npc.ai[0] == 0f)//状态一:寻找目标
        //	{
        //		npc.knockBackResist = knockBackReistConst;//0.3f的击退抗性
        //		float scaleFactor6 = scaler;//模长为8
        //		Vector2 center4 = npc.Center;//NPC中心
        //		Vector2 center5 = Main.player[npc.target].Center;//玩家中心
        //		Vector2 vector131 = center5 - center4;//朝向玩家的向量
        //		Vector2 vector132 = vector131 - Vector2.UnitY * height;//朝向玩家上方300像素的向量
        //		float num1015 = vector131.Length();//NPC和玩家的距离
        //		vector131 = Vector2.Normalize(vector131) * scaleFactor6;//模长改为8
        //		vector132 = Vector2.Normalize(vector132) * scaleFactor6;//模长改为8
        //		bool flag64 = Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1);//判断是否可攻击？
        //		if (npc.ai[3] >= 120f)
        //		{
        //			flag64 = true;
        //		}
        //		float num1016 = 8f;
        //		flag64 = (flag64 && vector131.ToRotation() > 3.14159274f / num1016 && vector131.ToRotation() < 3.14159274f - 3.14159274f / num1016);
        //		if (num1015 > distanceMax || !flag64)//如果玩家和npc的距离超过800像素或着ai[3]的计数未达到120
        //		{
        //			//npc.velocity.X = (npc.velocity.X * (num1002 - 1f) + vector132.X) / num1002;
        //			//npc.velocity.Y = (npc.velocity.Y * (num1002 - 1f) + vector132.Y) / num1002;
        //			npc.velocity = (npc.velocity * (value - 1) + vector132) / value;//速度向量加权平均
        //			if (!flag64) //如果ai[3]的计数未达到120
        //			{
        //				npc.ai[3] += 1f;//ai[3]增加
        //				if (npc.ai[3] == 120f)
        //				{
        //					npc.netUpdate = true;//重要更新
        //				}
        //			}
        //			else
        //			{
        //				npc.ai[3] = 0f;//重置ai[3]
        //			}
        //		}
        //		else//计数未达到120并且距离小于800
        //		{
        //			npc.ai[0] = 1f;//切换至状态二
        //			npc.ai[2] = vector131.X;//用ai[2] 、 ai[2]存储朝向玩家模长为8的向量
        //			npc.ai[3] = vector131.Y;//
        //			npc.netUpdate = true;
        //		}
        //	}
        //	else if (npc.ai[0] == 1f)//状态二:找到目标后准备冲刺
        //	{
        //		npc.knockBackResist = 0f;//击退抗性拉满
        //		npc.velocity *= slowerScale;//速度每帧乘0.8f(减速，准备冲刺
        //		npc.ai[1] += 1f;//计数增加
        //		if (npc.ai[1] >= countMax)//计数大于等于5时
        //		{
        //			npc.ai[0] = 2f;//切换状态至三
        //			npc.ai[1] = 0f;//计数归零
        //			npc.netUpdate = true;//重要更新  5帧前获取的向量                                         横纵都取0到0.04的随机数作为偏移量（意义不大？
        //			Vector2 velocity = new Vector2(npc.ai[2], npc.ai[3]) + new Vector2((float)Main.rand.Next(-randArea, randArea + 1), (float)Main.rand.Next(-randArea, randArea + 1)) * 0.04f;
        //			velocity.Normalize();
        //			velocity *= VelScaler;//模长设置为10
        //			npc.velocity = velocity;//作为NPC的速度向量
        //		}
        //	}
        //	else if (npc.ai[0] == 2f)//状态三:冲！
        //	{
        //		npc.knockBackResist = 0f;
        //		float num1018 = countMax2;//ai[1]计数大于30时
        //		npc.ai[1] += 1f;
        //		bool flag65 = Vector2.Distance(npc.Center, Main.player[npc.target].Center) > distanceMax2 && npc.Center.Y > Main.player[npc.target].Center.Y;//NPC到玩家的距离大于150像素并且NPC在玩家下方
        //		if ((npc.ai[1] >= num1018 && flag65) || npc.velocity.Length() < VelMax)//计数大于30和flag65同时成立或者速度向量模长小于8成立
        //		{
        //			//npc.ai[0] = 0f;
        //			//npc.ai[1] = 0f;
        //			npc.ai[0] = 4f;//切换至状态四
        //			npc.ai[1] = 45f;//ai[1]计数设置为45
        //			npc.ai[2] = 0f;
        //			npc.ai[3] = 0f;
        //			npc.velocity /= 2f;//速度减半
        //			npc.netUpdate = true;
        //		}
        //		else //计数大于30和flag65有一个不成立并且速度向量模长大于于8
        //		{
        //			Vector2 center6 = npc.Center;
        //			Vector2 center7 = Main.player[npc.target].Center;
        //			Vector2 vec2 = center7 - center6;//朝向玩家的向量
        //			vec2.Normalize();
        //			if (vec2.HasNaNs())
        //			{
        //				vec2 = new Vector2((float)npc.direction, 0f);//如果失去目标(? 就将这个向量设置为npc朝向
        //			}
        //			//                             59的权重               20加上速度向量模长作为权重         除去60
        //			npc.velocity = (npc.velocity * (value2 - 1f) + vec2 * (npc.velocity.Length() + num1008)) / value2;//速度向量越大越容易改变为目标向量
        //		}
        //		//if (flag63 && Collision.SolidCollision(npc.position, npc.width, npc.height))
        //		//{
        //		//	npc.ai[0] = 3f;
        //		//	npc.ai[1] = 0f;
        //		//	npc.ai[2] = 0f;
        //		//	npc.ai[3] = 0f;
        //		//	npc.netUpdate = true;
        //		//}
        //	}
        //	else if (npc.ai[0] == 4f)//状态四;减速
        //	{
        //		npc.ai[1] -= 3f;//计数每帧减去3
        //		if (npc.ai[1] <= 0f)//15帧后
        //		{
        //			npc.ai[0] = 0f;//回到状态一
        //			npc.ai[1] = 0f;//计数归零
        //			npc.netUpdate = true;
        //		}
        //		npc.velocity *= 0.95f;//速度每帧乘0.95f
        //	}
        //          #endregion
        //	//if (flag63 && npc.ai[0] != 3f && Vector2.Distance(npc.Center, Main.player[npc.target].Center) < 64f)
        //	//{
        //	//	npc.ai[0] = 3f;
        //	//	npc.ai[1] = 0f;
        //	//	npc.ai[2] = 0f;
        //	//	npc.ai[3] = 0f;
        //	//	npc.netUpdate = true;
        //	//}
        //	//if (npc.ai[0] == 3f)//这个状态不会被这个NPC对应的分支ai执行
        //	//{
        //	//	npc.position = npc.Center;
        //	//	npc.position.X = npc.position.X - (float)(npc.width / 2);
        //	//	npc.position.Y = npc.position.Y - (float)(npc.height / 2);
        //	//	npc.velocity = Vector2.Zero;
        //	//	npc.damage = (int)(80f * Main.damageMultiplier);
        //	//	npc.alpha = 255;
        //	//	Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.2f, 0.7f, 1.1f);
        //	//	int num;
        //	//	for (int num1019 = 0; num1019 < 10; num1019 = num + 1)
        //	//	{
        //	//		int num1020 = Dust.NewDust(npc.position, npc.width, npc.height, MyDustId.CyanBubble, 0f, 0f, 100, Color.White, 1.5f);
        //	//		Dust dust3 = Main.dust[num1020];
        //	//		dust3.velocity *= 1.4f;
        //	//		Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
        //	//		num = num1019;
        //	//	}
        //	//	for (int num1021 = 0; num1021 < 40; num1021 = num + 1)
        //	//	{
        //	//		int num1022 = Dust.NewDust(npc.position, npc.width, npc.height, MyDustId.ElectricCyan, 0f, 0f, 100, Color.White, 0.5f);
        //	//		Main.dust[num1022].noGravity = true;
        //	//		Dust dust3 = Main.dust[num1022];
        //	//		dust3.velocity *= 2f;
        //	//		Main.dust[num1022].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
        //	//		Main.dust[num1022].velocity = Main.dust[num1022].velocity / 2f + Vector2.Normalize(Main.dust[num1022].position - npc.Center);
        //	//		if (Main.rand.NextBool(2))
        //	//		{
        //	//			num1022 = Dust.NewDust(npc.position, npc.width, npc.height, MyDustId.ElectricCyan, 0f, 0f, 100, Color.White, 0.9f);
        //	//			Main.dust[num1022].noGravity = true;
        //	//			dust3 = Main.dust[num1022];
        //	//			dust3.velocity *= 1.2f;
        //	//			Main.dust[num1022].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
        //	//			Main.dust[num1022].velocity = Main.dust[num1022].velocity / 2f + Vector2.Normalize(Main.dust[num1022].position - npc.Center);
        //	//		}
        //	//		if (Main.rand.NextBool(4))
        //	//		{
        //	//			num1022 = Dust.NewDust(npc.position, npc.width, npc.height, MyDustId.ElectricCyan, 0f, 0f, 100, Color.White, 0.7f);
        //	//			dust3 = Main.dust[num1022];
        //	//			dust3.velocity *= 1.2f;
        //	//			Main.dust[num1022].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
        //	//			Main.dust[num1022].velocity = Main.dust[num1022].velocity / 2f + Vector2.Normalize(Main.dust[num1022].position - npc.Center);
        //	//		}
        //	//		num = num1021;
        //	//	}
        //	//	npc.ai[1] += 1f;
        //	//	if (npc.ai[1] >= 3f)
        //	//	{
        //	//		SoundEngine.PlaySound(SoundID.Item14, npc.position);
        //	//		npc.life = 0;
        //	//		npc.HitEffect(0, 10.0);
        //	//		npc.active = false;
        //	//		return;
        //	//	}
        //	//}
        //}
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/ElectricalCloud"), npc.oldPos[k] - Main.screenPosition + new Vector2(0, 20) + new Vector2(Main.rand.NextFloat(0, 16), 0).RotatedBy(npc.rotation + Main.rand.NextFloat(-MathHelper.Pi / 24, MathHelper.Pi / 24)), new Rectangle(0, (k + startFrame) % 6 * 28, 54, 28), new Color(1, 1, 1, 1) * (102 - 8 * k), 0, new Vector2(27, 14), (npc.scale * 0.8f - 0.05f * k) * Main.rand.NextFloat(0.9f, 1.2f), SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/PositiveElectricalSlime_Glow1"), npc.Center + new Vector2(0, 4) - Main.screenPosition, new Rectangle(0, npc.frame.Y, 36, 26), Color.White * (1 - IllusionBoundMod.GlowLight), npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/PositiveElectricalSlime_Glow2"), npc.Center + new Vector2(0, 4) - Main.screenPosition, null, Color.White * IllusionBoundMod.GlowLight, npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
        }
        private int startFrame;
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y += (int)Main.time % 2 == 0 ? frameHeight : 0;
            npc.frame.Y %= frameHeight * 10;
            startFrame += (int)Main.time % 2 == 0 ? 1 : 0;
            startFrame %= 6;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int num5;
            if (npc.life > 0)
            {
                int num64 = 0;
                while (num64 < damage / npc.lifeMax * 20.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 241, hitDirection, -1f, 0, default, 1f);
                    if (Main.rand.NextBool(2))
                    {
                        Dust dust14 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 0, default, 1f)];
                        dust14.noGravity = true;
                        dust14.scale = 1.5f;
                        dust14.fadeIn = 1f;
                        Dust dust = dust14;
                        dust.velocity *= 3f;
                    }
                    num5 = num64;
                    num64 = num5 + 1;
                }
            }
            else
            {
                for (int num65 = 0; num65 < 20; num65 = num5 + 1)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 241, hitDirection, -1f, 0, default, 1f);
                    Dust dust15 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 0, default, 1f)];
                    dust15.noGravity = true;
                    dust15.scale = 1.5f;
                    dust15.fadeIn = 1f;
                    Dust dust = dust15;
                    dust.velocity *= 3f;
                    num5 = num65;
                }
                //Gore.NewGore(npc.Center, npc.velocity * 0.8f, 841, 1f);
                //Gore.NewGore(npc.Center, npc.velocity * 0.8f, 842, 1f);
                //Gore.NewGore(npc.Center, npc.velocity * 0.8f, 842, 1f);
                //Gore.NewGore(npc.Center, npc.velocity * 0.9f, 843, 1f);
                //Gore.NewGore(npc.Center, npc.velocity * 0.9f, 843, 1f);
            }
        }
    }
    public class NegativeElectricalSlime : ModNPC
    {
        public bool matched;

        private NPC npc => NPC;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("负电史莱姆");
            Main.npcFrameCount[npc.type] = 10;
        }
        public override void SetDefaults()
        {
            //npc.width = 36;
            //npc.height = 26;
            //npc.aiStyle = -1;
            //npc.damage = 45;
            //npc.defense = 18;
            //npc.noGravity = true;
            //npc.behindTiles = true;
            //npc.lifeMax = 150;
            //npc.HitSound = SoundID.NPCHit1;
            //npc.knockBackResist = 1f;
            //npc.DeathSound = SoundID.NPCDeath1;
            //npc.value = 300f;
            npc.npcSlots = 1f;
            npc.width = 36;
            npc.height = 26;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 18;
            npc.lifeMax = 150;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 150f;
            npc.knockBackResist = 1f;
        }
        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
        //    return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.8f : 0;
        //}
        public Player player { get { return Main.player[npc.target]; } }
        public override void AI()
        {
            for (int num31 = npc.oldPos.Length - 1; num31 > 0; num31--)
            {
                npc.oldPos[num31] = npc.oldPos[num31 - 1];
                oldFrameY[num31] = oldFrameY[num31 - 1];
            }
            npc.oldPos[0] = npc.Center;
            oldFrameY[0] = npc.frame.Y;
            bool flag = false;
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
                        npc.velocity.Y = -12f;
                        npc.velocity.X = npc.velocity.X + (float)(4.5f * npc.direction);
                        npc.ai[0] = -200f;
                        npc.ai[3] = npc.position.X;
                    }
                    else
                    {
                        npc.velocity.Y = -9f;
                        npc.velocity.X = npc.velocity.X + (float)(3f * npc.direction);
                        npc.ai[0] = -120f;
                        if (num19 == 1)
                        {
                            npc.ai[0] -= 1000f;
                        }
                        else
                        {
                            npc.ai[0] -= 2000f;
                        }
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
            if (matched)
            {
                return;
            }

            if ((player.Center - npc.Center).Length() < 64f)
            {
                player.velocity = new Vector2(player.velocity.X * 0.925f, player.velocity.Y);
            }
            else if ((player.Center - npc.Center).Length() < 128f)
            {
                player.velocity = new Vector2(player.velocity.X * 0.95f, player.velocity.Y);
            }
            else if ((player.Center - npc.Center).Length() <= 1024f && (player.Center - npc.Center).Length() >= 512f && (int)IllusionBoundMod.ModTime2 % 60 == 0 && Main.rand.NextBool(5) && flag)
            {
                Vector2 vec = player.Center + 160 * Main.rand.NextVector2Unit();
                bool flag2 = false;
                while (!flag2)
                {
                    flag2 = true;
                    for (int n = -1; n <= 1; n++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            flag2 &= !Main.tile[(int)vec.X / 16 + n, (int)vec.Y / 16 + i].HasTile;
                        }
                    }
                    if (!flag2)
                    {
                        vec = player.Center + 160 * Main.rand.NextVector2Unit();
                    }
                }
                OtherMethods.LinerDust(vec, npc.Center, MyDustId.RedBubble);
                npc.Center = vec;
                for (int n = 0; n < 36; n++)
                {
                    Dust.NewDustPerfect(npc.Center + (MathHelper.TwoPi / 36 * n).ToRotationVector2() * 32, MyDustId.RedBubble, -2 * (MathHelper.TwoPi / 36 * n).ToRotationVector2(), 0, Color.White);
                }
            }
        }
        private readonly int[] oldFrameY = new int[10];
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 vector2, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, new Rectangle(0, npc.frame.Y, 36, 26), npc.GetAlpha(drawColor), npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 vector2, Color drawColor)
        {
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.oldPos[k] + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition + new Vector2(Main.rand.NextFloat(0, 16), 0).RotatedBy(npc.rotation + Main.rand.NextFloat(-MathHelper.Pi / 24, MathHelper.Pi / 24)), new Rectangle(0, oldFrameY[k], 36, 26), new Color((102 - 8 * k), 0, 0, (102 - 8 * k)), npc.rotation, new Vector2(18, 13), (npc.scale * 0.8f - 0.05f * k) * Main.rand.NextFloat(0.9f, 1.2f), SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/NegativeElectricalSlime_Glow1"), npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, new Rectangle(0, npc.frame.Y, 36, 26), Color.White * (1 - IllusionBoundMod.GlowLight), npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/NegativeElectricalSlime_Glow2"), npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, null, Color.White * IllusionBoundMod.GlowLight, npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            if (matched)
            {
                return;
            }

            for (int n = 0; n < 3; n++)
            {
                float k = 1 - (float)(Main.time / 60 + 1 / 3f * n) + (int)(Main.time / 60 + 1 / 3f * n);
                spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/SlowDownField"), npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, null, Color.White with { A = 0 } * (1 - k), npc.rotation, new Vector2(32, 32), k * 4, SpriteEffects.None, 0);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y += (int)Main.time % 3 == 0 ? frameHeight : 0;
            npc.frame.Y %= frameHeight * 10;
        }
    }
    //public class NegativeElectricalSlime : ModNPC
    //{
    //	public bool matched;
    //	public override void SetStaticDefaults()
    //	{
    //		DisplayName.SetDefault("负电史莱姆");
    //		Main.npcFrameCount[npc.type] = 10;
    //	}
    //	public override void SetDefaults()
    //	{
    //		npc.width = 36;
    //		npc.height = 26;
    //		npc.aiStyle = -1;
    //		npc.damage = 45;
    //		npc.defense = 18;
    //		npc.noGravity = true;
    //		npc.behindTiles = true;
    //		npc.lifeMax = 150;
    //		npc.HitSound = SoundID.NPCHit1;
    //		npc.knockBackResist = 1f;
    //		npc.DeathSound = SoundID.NPCDeath1;
    //		npc.value = 300f;
    //	}
    //	//private Vector2 targetVec;
    //	//private Vector2 targetPlayerVec;
    //	//public NegativeElectricalSlime()
    //	//{
    //	//	targetVec = npc.Center;
    //	//}
    //	public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //	{
    //		return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.8f : 0;
    //	}
    //	public Player player { get { return Main.player[npc.target]; } }
    //	//private int counter2;
    //	//private int outOfControlTimer;
    //	//private void Collision_MoveBlazingWheel()
    //	//{

    //	//}
    //	/*private void Collision_WalkDownSlopes()
    //	{
    //		//float y = npc.velocity.Y;
    //		Vector4 vector = Collision.WalkDownSlope(npc.position, npc.velocity, npc.width, npc.height, 0.3f);
    //		npc.position.X = vector.X;
    //		npc.position.Y = vector.Y;
    //		npc.velocity.X = vector.Z;
    //		npc.velocity.Y = vector.W;
    //	}
    //	//private bool Collision_LavaCollision()
    //	//{
    //	//	bool flag = Collision.LavaCollision(npc.position, npc.width, npc.height);
    //	//	if (flag)
    //	//	{
    //	//		npc.lavaWet = true;
    //	//		if (!npc.lavaImmune && !npc.dontTakeDamage && Main.netMode != NetmodeID.MultiplayerClient && npc.immune[255] == 0)
    //	//		{
    //	//			npc.AddBuff(24, 420, false);
    //	//			npc.immune[255] = 30;
    //	//			npc.StrikeNPCNoInteraction(50, 0f, 0, false, false, false);
    //	//			if (Main.netMode == 2 && Main.netMode != 0)
    //	//			{
    //	//				NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 50f, 0f, 0f, 0, 0, 0);
    //	//			}
    //	//		}
    //	//	}
    //	//	return flag;
    //	//}
    //	private bool Collision_WaterCollision()
    //	{
    //		npc.wetCount = 0;
    //		if (npc.wet)
    //		{
    //			npc.velocity.X = npc.velocity.X * 0.5f;
    //			npc.wet = false;
    //			if (npc.wetCount == 0)
    //			{
    //				npc.wetCount = 10;
    //				if (!npc.lavaWet)
    //				{
    //					if (npc.honeyWet)
    //					{
    //						for (int m = 0; m < 10; m++)
    //						{
    //							int num4 = Dust.NewDust(new Vector2(npc.position.X - 6f, npc.position.Y + (float)(npc.height / 2) - 8f), npc.width + 12, 24, 152, 0f, 0f, 0, default, 1f);
    //							Dust dust7 = Main.dust[num4];
    //							dust7.velocity.Y = dust7.velocity.Y - 1f;
    //							Dust dust8 = Main.dust[num4];
    //							dust8.velocity.X = dust8.velocity.X * 2.5f;
    //							Main.dust[num4].scale = 1.3f;
    //							Main.dust[num4].alpha = 100;
    //							Main.dust[num4].noGravity = true;
    //						}
    //						SoundEngine.PlaySound(19, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
    //					}
    //					else
    //					{
    //						for (int n = 0; n < 30; n++)
    //						{
    //							int num5 = Dust.NewDust(new Vector2(npc.position.X - 6f, npc.position.Y + (float)(npc.height / 2) - 8f), npc.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default, 1f);
    //							Dust dust9 = Main.dust[num5];
    //							dust9.velocity.Y = dust9.velocity.Y - 4f;
    //							Dust dust10 = Main.dust[num5];
    //							dust10.velocity.X = dust10.velocity.X * 2.5f;
    //							Main.dust[num5].scale *= 0.8f;
    //							Main.dust[num5].alpha = 100;
    //							Main.dust[num5].noGravity = true;
    //						}
    //						SoundEngine.PlaySound(19, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
    //					}
    //				}
    //				else
    //				{
    //					for (int num6 = 0; num6 < 10; num6++)
    //					{
    //						int num7 = Dust.NewDust(new Vector2(npc.position.X - 6f, npc.position.Y + (float)(npc.height / 2) - 8f), npc.width + 12, 24, 35, 0f, 0f, 0, default, 1f);
    //						Dust dust11 = Main.dust[num7];
    //						dust11.velocity.Y = dust11.velocity.Y - 1.5f;
    //						Dust dust12 = Main.dust[num7];
    //						dust12.velocity.X = dust12.velocity.X * 2.5f;
    //						Main.dust[num7].scale = 1.3f;
    //						Main.dust[num7].alpha = 100;
    //						Main.dust[num7].noGravity = true;
    //					}
    //					SoundEngine.PlaySound(19, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
    //				}
    //			}
    //		}
    //		return false;
    //	}
    //	private bool Collision_DecideFallThroughPlatforms()
    //	{
    //		bool result = false;
    //		if (npc.type == 2 || npc.type == -43 || npc.type == 317 || npc.type == 318 || npc.type == 133)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 10)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 40)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 467)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 477)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 14)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 173)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 469 && npc.ai[2] == 1f)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 3 && npc.directionY == 1)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 210 || npc.type == 211)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 50 && npc.target >= 0 && Main.player[npc.target].position.Y > npc.position.Y + (float)npc.height)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 247 || npc.type == 248)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 245 && npc.target >= 0 && Main.player[npc.target].position.Y > npc.position.Y + (float)npc.height)
    //		{
    //			result = true;
    //		}
    //		if (npc.type >= 542 && npc.type <= 545)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 107 && npc.directionY == 1)
    //		{
    //			result = true;
    //		}
    //		if (npc.type == 418)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 87 && Main.player[npc.target].position.Y > npc.position.Y + (float)npc.height)
    //		{
    //			result = true;
    //		}
    //		if (npc.aiStyle == 7)
    //		{
    //			int num = 16;
    //			bool flag = false;
    //			if (!Main.dayTime || Main.invasionType > 0 || Main.eclipse)
    //			{
    //				flag = true;
    //			}
    //			else
    //			{
    //				int num2 = (int)(npc.position.Y + (float)npc.height) / 16;
    //				if (npc.homeTileY - num2 > num)
    //				{
    //					result = true;
    //				}
    //			}
    //			if (flag && (npc.position.Y + (float)npc.height - 8f) / 16f < (float)(npc.homeTileY - 1))
    //			{
    //				result = true;
    //			}
    //		}
    //		return result;
    //	}
    //	private void FishTransformationDuringRain()
    //	{
    //		if (Main.netMode != NetmodeID.MultiplayerClient)
    //		{
    //			if (npc.type == 230 && npc.wet)
    //			{
    //				int direction = npc.direction;
    //				Vector2 velocity = npc.velocity;
    //				npc.Transform(55);
    //				npc.direction = direction;
    //				npc.velocity = velocity;
    //				npc.wet = true;
    //				if (npc.velocity.Y < 0f)
    //				{
    //					npc.velocity.Y = 0f;
    //					return;
    //				}
    //			}
    //			else if (npc.type == 55 && !npc.wet && Main.raining)
    //			{
    //				int direction2 = npc.direction;
    //				Vector2 velocity2 = npc.velocity;
    //				npc.Transform(230);
    //				npc.direction = direction2;
    //				npc.velocity = velocity2;
    //				npc.homeTileX = (int)(npc.position.X / 16f) + 10 * npc.direction;
    //			}
    //		}
    //	}
    //	private void GetTileCollisionParameters(out Vector2 cPosition, out int cWidth, out int cHeight)
    //	{
    //		cPosition = npc.position;
    //		cWidth = npc.width;
    //		cHeight = npc.height;
    //		if (npc.type == 243)
    //		{
    //			cHeight = 90;
    //		}
    //		if (npc.type == 290)
    //		{
    //			cHeight = 40;
    //		}
    //		if (npc.type == 351)
    //		{
    //			cHeight = 40;
    //		}
    //		if (npc.type == 482)
    //		{
    //			cHeight = 40;
    //		}
    //		if (npc.type == 351 || npc.type == 343 || npc.type == 348 || npc.type == 349)
    //		{
    //			cHeight = 40;
    //		}
    //		if (npc.type == 391)
    //		{
    //			for (int i = 0; i < 200; i++)
    //			{
    //				if (Main.npc[i].active && Main.npc[i].type == 390 && Main.npc[i].ai[0] == (float)npc.whoAmI)
    //				{
    //					cHeight = 62;
    //					break;
    //				}
    //			}
    //		}
    //		if (npc.type == 415)
    //		{
    //			for (int j = 0; j < 200; j++)
    //			{
    //				if (Main.npc[j].active && Main.npc[j].type == 416 && Main.npc[j].ai[0] == (float)npc.whoAmI)
    //				{
    //					cHeight = 62;
    //					break;
    //				}
    //			}
    //		}
    //		if (npc.type == 576 || npc.type == 577)
    //		{
    //			cPosition.X += 32f;
    //			cWidth -= 64;
    //		}
    //		if (cHeight != npc.height)
    //		{
    //			cPosition.Y += (float)(npc.height - cHeight);
    //		}
    //	}
    //	private void Collision_MoveWhileDry()
    //	{
    //		if (Collision.up)
    //		{
    //			npc.velocity.Y = 0.01f;
    //		}
    //		if (npc.oldVelocity.X != npc.velocity.X)
    //		{
    //			npc.collideX = true;
    //		}
    //		if (npc.oldVelocity.Y != npc.velocity.Y)
    //		{
    //			npc.collideY = true;
    //		}
    //		npc.oldPosition = npc.position;
    //		npc.oldDirection = npc.direction;
    //		npc.position += npc.velocity;
    //	}
    //	private void Collision_MoveWhileWet(Vector2 oldDryVelocity, float Slowdown = 0.5f)
    //	{
    //		if (Collision.up)
    //		{
    //			npc.velocity.Y = 0.01f;
    //		}
    //		Vector2 value = npc.velocity * Slowdown;
    //		if (npc.velocity.X != oldDryVelocity.X)
    //		{
    //			value.X = npc.velocity.X;
    //			npc.collideX = true;
    //		}
    //		if (npc.velocity.Y != oldDryVelocity.Y)
    //		{
    //			value.Y = npc.velocity.Y;
    //			npc.collideY = true;
    //		}
    //		npc.oldPosition = npc.position;
    //		npc.oldDirection = npc.direction;
    //		npc.position += value;
    //	}
    //	private void UpdateCollision()
    //	{
    //		Collision_WalkDownSlopes();
    //		//bool lava = Collision_LavaCollision();
    //		//lava = Collision_WaterCollision(lava);
    //		if (!npc.wet)
    //		{
    //			npc.lavaWet = false;
    //			npc.honeyWet = false;
    //		}
    //		if (npc.wetCount > 0)
    //		{
    //			npc.wetCount -= 1;
    //		}
    //		//bool fall = Collision_DecideFallThroughPlatforms();
    //		npc.oldVelocity = npc.velocity;
    //		npc.collideX = false;
    //		npc.collideY = false;
    //		//FishTransformationDuringRain();
    //		//Vector2 cPosition;
    //		//int cWidth;
    //		//int cHeight;
    //		//GetTileCollisionParameters(out cPosition, out cWidth, out cHeight);
    //		Vector2 velocity = npc.velocity;
    //		Collision_MoveBlazingWheel();
    //		if (npc.wet)
    //		{
    //			if (npc.honeyWet)
    //			{
    //				Collision_MoveWhileWet(velocity, 0.5f);
    //			}
    //			else if (npc.lavaWet)
    //			{
    //				Collision_MoveWhileWet(velocity, 0.5f);
    //			}
    //			else
    //			{
    //				Collision_MoveWhileWet(velocity, 0.25f);
    //			}
    //		}
    //		else
    //		{
    //			Collision_MoveWhileDry();
    //		}
    //	} */
    //	private float PiOver2Rad(float rad)
    //	{
    //		rad += MathHelper.Pi * 1.25f;
    //		int r = (int)(rad * 2 / MathHelper.Pi);
    //		rad %= 4;
    //		while (rad < 0)
    //		{
    //			rad += 4;
    //		}
    //		return r * MathHelper.PiOver2 - MathHelper.Pi;
    //	}
    //	public override void AI()
    //	{
    //		//if (Main.player[npc.target] != null) 
    //		//{
    //		//	float value2 = 60f;
    //		//	Vector2 vec2 = Main.player[npc.target].Center - npc.Center;
    //		//	vec2.Normalize();
    //		//	npc.velocity = (npc.velocity * (value2 - 1f) + vec2 * 20f) / value2;
    //		//}
    //		//npc.noGravity = outOfControlTimer < 0;
    //		//outOfControlTimer -= outOfControlTimer > -1 ? 1 : 0;
    //		for (int num31 = npc.oldPos.Length - 1; num31 > 0; num31--)
    //		{
    //			npc.oldPos[num31] = npc.oldPos[num31 - 1];
    //			oldFrameY[num31] = oldFrameY[num31 - 1];
    //		}
    //		npc.oldPos[0] = npc.Center;
    //		oldFrameY[0] = npc.frame.Y;
    //		//if (!Main.tile[(int)(targetVec.X / 16), (int)(targetVec.Y / 16)].active()) 
    //		//{
    //		//	targetVec = FindTargetTile(npc, player);
    //		//}
    //		//if (!Main.tile[(int)(targetPlayerVec.X / 16), (int)(targetPlayerVec.Y / 16)].active())
    //		//{
    //		//	targetPlayerVec = FindTargetTile(player, npc);
    //		//}
    //		//Collision_MoveBlazingWheel();
    //		if (npc.ai[0] == 0f)
    //		{
    //			npc.TargetClosest(true);
    //			npc.directionY = 1;
    //			npc.ai[0] = 1f;
    //		}
    //		npc.ai[2] += 0.05f;
    //		npc.ai[2] = MathHelper.Clamp(npc.ai[2], 0, 6);
    //		Vector2 vec = player.Center;
    //		//if ((int)Main.time % 10 == 0) 
    //		//{
    //		//	ToolPlayer toolPlayer = player.GetModPlayer<ToolPlayer>();
    //		//	toolPlayer.vectors.Clear();
    //		//	for (int n = 1; n <= 60; n++)
    //		//	{
    //		//		for (int i = 0; i < 2 * n; i++)
    //		//		{
    //		//			for (int k = 0; k < 4; k++)
    //		//			{
    //		//				Point point = (player.Center / 16 + new Vector2(n, (i + n - n) % (2 * n) - n + 1).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //		//				if (Main.tile[point.X, point.Y].active())
    //		//				{
    //		//					toolPlayer.vectors.Add(point.ToVector2() * 16);
    //		//				}
    //		//			}
    //		//			if (toolPlayer.vectors.Count > 0)
    //		//			{
    //		//				break;
    //		//			}
    //		//		}
    //		//		if (toolPlayer.vectors.Count > 0)
    //		//		{
    //		//			break;
    //		//		}
    //		//	}
    //		//	if (toolPlayer.vectors.Count > 0)
    //		//	{
    //		//		float distanceMax = float.MaxValue;
    //		//		int index = -1;
    //		//		for (int n = 0; n < toolPlayer.vectors.Count; n++)
    //		//		{
    //		//			float distance = (npc.Center - toolPlayer.vectors[n]).Length();
    //		//			if (distance < distanceMax)
    //		//			{
    //		//				distanceMax = distance;
    //		//				index = n;
    //		//			}
    //		//		}
    //		//		if (index != -1)
    //		//		{
    //		//			vec = toolPlayer.vectors[index];
    //		//		}
    //		//	}
    //		//}
    //		//for (int n = -2; n < 3; n++) 
    //		//{
    //		//	int k = -4;
    //		//	for (int i = -3; n < 4; i++)
    //		//	{
    //		//		if (Main.tile[(int)player.Center.X / 16 + n, (int)player.Center.Y / 16 + i].active())
    //		//		{
    //		//			vec += new Vector2(0, i) * 16;
    //		//			k = i;
    //		//			break;
    //		//		}
    //		//	}
    //		//	if (k != -1 && Main.tile[(int)player.Center.X / 16 + n, (int)player.Center.Y / 16 + k].active()) 
    //		//	{
    //		//		vec += new Vector2(n, 0) * 16;
    //		//		break;
    //		//	}
    //		//}
    //		//bool f1 = Main.tile[(int)(player.Center.X / 16), (int)(player.Top.Y / 16 + 0.5f)].active();
    //		//bool f2 = Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16 - 0.5f)].active();
    //		//bool f3 = Main.tile[(int)(player.Left.X / 16 - 0.5f), (int)(player.Center.Y / 16)].active();
    //		//bool f4 = Main.tile[(int)(player.Right.X / 16 + 0.5f), (int)(player.Center.Y / 16)].active();
    //		//if (f1 && !f2) 
    //		//{
    //		//	vec.Y = player.Top.Y + 8f;
    //		//}
    //		//if (f2 && !f1)
    //		//{
    //		//	vec.Y = player.Bottom.Y - 8f;
    //		//}
    //		//if (f3 && !f4)
    //		//{
    //		//	vec.X = player.Left.X - 8f;
    //		//}
    //		//if (f4 && !f3)
    //		//{
    //		//	vec.X = player.Right.X + 8f;
    //		//}
    //		bool flag3 = false;
    //		npc.ai[3]++;
    //		if (npc.ai[3] >= (vec - npc.position).Length() / 4 + 10)
    //		{
    //			npc.ai[3] = 0;
    //			flag3 = true;
    //		}
    //		//bool flag = ((int)Main.time % ((int)(vec - npc.position).Length() / 4) >= 1 ? ((int)(vec - npc.position).Length() / 4) : 1) == 0 && (vec - npc.position).Length() >= (vec - npc.oldPosition).Length();
    //		bool flag = flag3 && (vec - npc.position).Length() >= (vec - npc.oldPosition).Length();
    //		bool flag2 = false;
    //		for (int n = 1; n <= 4; n++)
    //		{
    //			for (int i = 0; i < 2 * n; i++)
    //			{
    //				for (int k = 0; k < 4; k++)
    //				{
    //					Point point = (npc.Center / 16 + new Vector2(n, (i + n - n) % (2 * n) - n + 1).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //					flag2 |= Main.tile[point.X, point.Y].active();
    //				}
    //			}
    //		}
    //		Vector2 vector2 = FindTargetTile(npc, player);
    //		npc.rotation = PiOver2Rad((vector2 - npc.Center).ToRotation()) - MathHelper.PiOver2;
    //		//if (!flag2 && outOfControlTimer < 0)
    //		//{
    //		//	npc.velocity = FindTargetTile(npc, player) - npc.Center;
    //		//}
    //		if (!flag2)
    //		{
    //			LinerDust(vector2, npc.Center, MyDustId.RedBubble);
    //			npc.velocity = vector2 - npc.Center;
    //		}
    //		else
    //		{
    //			if (npc.ai[1] == 0f)//水平移动
    //			{
    //				if (npc.collideY)
    //				{
    //					npc.ai[0] = 2f;
    //				}
    //				if (flag)
    //				{
    //					npc.direction = -npc.direction;
    //					npc.ai[2] = 0;
    //				}
    //				if (!npc.collideY && npc.ai[0] == 2f)
    //				{
    //					npc.direction = -npc.direction;
    //					npc.ai[1] = 1f;
    //					npc.ai[0] = 1f;
    //				}
    //				if (npc.collideX)
    //				{
    //					npc.directionY = -npc.directionY;
    //					npc.ai[1] = 1f;
    //				}
    //			}
    //			else//垂直移动
    //			{
    //				if (npc.collideX)
    //				{
    //					npc.ai[0] = 2f;
    //				}
    //				if (flag)
    //				{
    //					npc.directionY = -npc.directionY;
    //					npc.ai[2] = 0;
    //				}
    //				if (!npc.collideX && npc.ai[0] == 2f)
    //				{
    //					npc.directionY = -npc.directionY;
    //					npc.ai[1] = 0f;
    //					npc.ai[0] = 1f;
    //				}
    //				if (npc.collideY)
    //				{
    //					npc.direction = -npc.direction;
    //					npc.ai[1] = 0f;
    //				}
    //			}
    //			//if (outOfControlTimer < 0) 
    //			//{
    //			npc.velocity.X = npc.ai[2] * npc.direction * (npc.frame.Y / 260f / 2f + 0.75f);
    //			npc.velocity.Y = npc.ai[2] * npc.directionY * (npc.frame.Y / 260f / 2f + 0.75f);
    //			//}
    //		}
    //		if (matched)
    //			return;
    //		if ((player.Center - npc.Center).Length() < 64f)
    //		{
    //			player.velocity = new Vector2(player.velocity.X * 0.925f, player.velocity.Y);
    //		}
    //		else if ((player.Center - npc.Center).Length() < 128f)
    //		{
    //			player.velocity = new Vector2(player.velocity.X * 0.95f, player.velocity.Y);
    //		}
    //		//counter2++;
    //		//if(counter2 > 6)
    //		//{
    //		//	counter2 = 0;
    //		//	foreach (var tar in Main.npc)
    //		//	{
    //		//		if (tar.whoAmI != npc.whoAmI && tar.type == NPCType<NegativeElectricalSlime>())
    //		//		{
    //		//			int vel = (int)MathHelper.Clamp(128f / (tar.Center - npc.Center).Length(), 0.25f, 8);
    //		//			tar.velocity += Vector2.Normalize(tar.Center - npc.Center) * MathHelper.Clamp(128f / (tar.Center - npc.Center).Length(), 0.25f, 8);
    //		//			((NegativeElectricalSlime)tar.modNPC).outOfControlTimer = vel;
    //		//		}
    //		//	}
    //		//}
    //	}
    //	private Vector2 FindTargetTile(Entity myself, Entity target)
    //	{
    //		List<Vector2> vectors = new List<Vector2>();
    //		for (int n = 1; n <= 60; n++)
    //		{
    //			for (int i = 0; i < 2 * n; i++)
    //			{
    //				for (int k = 0; k < 4; k++)
    //				{
    //					Point point = (myself.Center / 16 + new Vector2(n, (i + n - 1) % (2 * n) - n + 1).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //					if (Main.tile[point.X, point.Y].active() && Main.tileSolid[Main.tile[point.X, point.Y].type])
    //					{
    //						vectors.Add(point.ToVector2() * 16 + new Vector2(8, 8));
    //					}
    //				}
    //				if (vectors.Count > 0)
    //				{
    //					break;
    //				}
    //			}
    //			if (vectors.Count > 0)
    //			{
    //				break;
    //			}
    //		}
    //		if (vectors.Count > 0)
    //		{
    //			float distanceMax = float.MaxValue;
    //			int index = -1;
    //			for (int n = 0; n < vectors.Count; n++)
    //			{
    //				float distance = (target.Center - vectors[n]).Length();
    //				if (distance < distanceMax)
    //				{
    //					distanceMax = distance;
    //					index = n;
    //				}
    //			}
    //			if (index != -1)
    //			{
    //				return vectors[index];
    //			}
    //		}
    //		return default;
    //	}
    //	private readonly int[] oldFrameY = new int[10];
    //	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
    //	{
    //		spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, new Rectangle(0, npc.frame.Y, 36, 26), npc.GetAlpha(drawColor), npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
    //		return false;
    //	}
    //	public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
    //	{
    //		for (int k = 0; k < npc.oldPos.Length; k++)
    //		{
    //			spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.oldPos[k] + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition + new Vector2(Main.rand.NextFloat(0, 16), 0).RotatedBy(npc.rotation + Main.rand.NextFloat(-MathHelper.Pi / 24, MathHelper.Pi / 24)), new Rectangle(0, oldFrameY[k], 36, 26), new Color((102 - 8 * k), 0, 0, (102 - 8 * k)), npc.rotation, new Vector2(18, 13), (npc.scale * 0.8f - 0.05f * k) * Main.rand.NextFloat(0.9f, 1.2f), SpriteEffects.None, 0f);
    //		}
    //		spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/NegativeElectricalSlime_Glow1"), npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, new Rectangle(0, npc.frame.Y, 36, 26), Color.White * (1 - IllusionBoundMod.StormTileGrowLight), npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
    //		spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/NegativeElectricalSlime_Glow2"), npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, null, Color.White * IllusionBoundMod.StormTileGrowLight, npc.rotation, new Vector2(18, 13), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
    //		if (matched)
    //			return;
    //		Main.spriteBatch.End();
    //		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
    //		for (int n = 0; n < 3; n++)
    //		{
    //			float k = 1 - (float)(Main.time / 60 + 1 / 3f * n) + (int)(Main.time / 60 + 1 / 3f * n);
    //			//if(n == 0)
    //			//Main.NewText(k);
    //			spriteBatch.Draw(IllusionBoundMod.GetTexture("Contents/Storm/Thunder/NPCs/SlowDownField"), npc.Center + new Vector2(0, 4) + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, null, Color.White * (1 - k), npc.rotation, new Vector2(32, 32), k * 4, SpriteEffects.None, 0);
    //		}
    //		Main.spriteBatch.End();
    //		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
    //	}
    //	public override void FindFrame(int frameHeight)
    //	{
    //		npc.frame.Y += (int)Main.time % 3 == 0 ? frameHeight : 0;
    //		npc.frame.Y %= frameHeight * 10;
    //	}
    //}
    public class ElectricalChain : ModProjectile
    {
        private Projectile projectile => Projectile;
        public NPC Owner_2
        {
            get
            {
                return Main.npc[(int)projectile.ai[1]];
            }
        }
        public NPC Owner
        {
            get
            {
                return Main.npc[(int)projectile.ai[0]];
            }
        }
        public float Alpha
        {
            get
            {
                return 1 - (Owner.Center - Owner_2.Center).Length() / 512f;
            }
        }
        public PositiveElectricalSlime ModOwner
        {
            get
            {
                return Owner != null ? Owner.ModNPC as PositiveElectricalSlime : null;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner_2.Center, 16, ref point);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.DamageType = DamageClass.Magic;
            projectile.timeLeft = 31;
            projectile.penetrate = -1;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.immune = true;
            target.immuneTime = 15;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("闪电链");
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            CustomVertexInfo[] Points = new CustomVertexInfo[6];
            float r = (Owner_2.Center - Owner.Center).ToRotation();
            Points[0] = new CustomVertexInfo(new Vector2(16, 0).RotatedBy(r + MathHelper.PiOver2) + Owner.Center, new Vector3(0, 0, Alpha / 2));
            Points[1] = new CustomVertexInfo(new Vector2(16, 0).RotatedBy(r - MathHelper.PiOver2) + Owner.Center, new Vector3(0, 1, Alpha / 2));
            Points[2] = new CustomVertexInfo(new Vector2(16, 0).RotatedBy(r + MathHelper.PiOver2) + Owner_2.Center, new Vector3(1, 0, Alpha / 2));
            Points[3] = new CustomVertexInfo(new Vector2(16, 0).RotatedBy(r + MathHelper.PiOver2) + Owner_2.Center, new Vector3(1, 0, Alpha / 2));
            Points[4] = new CustomVertexInfo(new Vector2(16, 0).RotatedBy(r - MathHelper.PiOver2) + Owner_2.Center, new Vector3(1, 1, Alpha / 2));
            Points[5] = new CustomVertexInfo(new Vector2(16, 0).RotatedBy(r - MathHelper.PiOver2) + Owner.Center, new Vector3(0, 1, Alpha / 2));
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            IllusionBoundMod.ColorfulEffect.Parameters["uTransform"].SetValue(model * Main.GameViewMatrix.TransformationMatrix * projection);
            IllusionBoundMod.ColorfulEffect.Parameters["uTime"].SetValue(0);
            IllusionBoundMod.ColorfulEffect.Parameters["defaultColor"].SetValue(Color.Purple.ToVector4());
            Main.graphics.GraphicsDevice.Textures[0] = IllusionBoundMod.LaserTex[(int)IllusionBoundMod.ModTime / 2 % 4];
            Main.graphics.GraphicsDevice.Textures[1] = IllusionBoundMod.LaserTex[(int)IllusionBoundMod.ModTime / 2 % 4];
            Main.graphics.GraphicsDevice.Textures[2] = IllusionBoundMod.AniTexes[6];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            IllusionBoundMod.ColorfulEffect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Points, 0, 2);
            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override void AI()
        {
            if (!Main.npc[(int)projectile.ai[0]].active || !Main.npc[(int)projectile.ai[1]].active || Main.npc[(int)projectile.ai[0]].type != NPCType<PositiveElectricalSlime>() || Main.npc[(int)projectile.ai[1]].type != NPCType<NegativeElectricalSlime>() || !((PositiveElectricalSlime)Main.npc[(int)projectile.ai[0]].ModNPC).matched)
            {
                projectile.Kill();
            }
            else
            {
                projectile.timeLeft = 2;
                projectile.Center = Owner.Center;
            }
        }
    }
    public class NeutralSlime : ModNPC
    {
        private NPC npc => NPC;
        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
        //    return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.8f : 0;
        //}
        private bool StartRecordH;
        private float[] VY = new float[2];
        public override void SetDefaults()
        {
            npc.npcSlots = 1f;
            npc.width = 72;
            npc.height = 52;
            npc.aiStyle = -1;
            npc.damage = 55;
            npc.defense = 20;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.alpha = 60;
            npc.knockBackResist = 0.1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("中性史莱姆");
            Main.npcFrameCount[npc.type] = 10;
        }
        private float startHeight;
        public override void AI()
        {
            if (!StartRecordH)
            {
                StartRecordH = true;
                startHeight = npc.height;
            }
            VY[1] = VY[0];
            VY[0] = npc.velocity.Y;
            bool flag = false;
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
                if (Math.Abs(npc.Center.Y - startHeight) > 32)
                {
                    if (npc.HasPlayerTarget)
                    {
                        Player player = Main.player[npc.target];
                        float dis = (player.Center - npc.Center).Length();
                        player.velocity.Y = (player.velocity.Y == 0 && dis <= 512) ? (float)Math.Pow(0.85, dis / 12) * -12 : 0;
                    }
                    for (int n = -16; n <= 16; n++)
                    {
                        Dust.NewDustPerfect(npc.Center + new Vector2(4 * n, 26), MyDustId.BlackMaterial, new Vector2(n / 4, -(float)Math.Sqrt(Math.Abs(n))), 0, Color.White);
                    }
                }
                startHeight = npc.Center.Y;
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
        public override void OnKill()
        {
            float r = Main.rand.NextFloat(0, MathHelper.TwoPi);
            Point point1 = (npc.Center + new Vector2(40, 0).RotatedBy(r)).ToPoint();
            Point point2 = (npc.Center - new Vector2(40, 0).RotatedBy(r)).ToPoint();
            int n = NPC.NewNPC(npc.GetSource_FromAI(), point1.X, point1.Y, NPCType<PositiveElectricalSlime>(), Target: npc.target);
            int i = NPC.NewNPC(npc.GetSource_FromAI(), point2.X, point2.Y, NPCType<NegativeElectricalSlime>(), Target: npc.target);
            for (int k = 0; k < 36; k++)
            {
                Dust dust1 = Dust.NewDustPerfect(point1.ToVector2() - new Vector2(16, 0).RotatedBy(MathHelper.TwoPi / 36 * k), MyDustId.ElectricCyan, new Vector2(2, 0).RotatedBy(MathHelper.TwoPi / 36 * k), newColor: Color.White);
                Dust dust2 = Dust.NewDustPerfect(point2.ToVector2() - new Vector2(16, 0).RotatedBy(MathHelper.TwoPi / 36 * k), MyDustId.RedBubble, new Vector2(2, 0).RotatedBy(MathHelper.TwoPi / 36 * k), newColor: Color.White);
                dust1.noGravity = true;
                dust2.noGravity = true;
            }
            Main.npc[n].life = Main.npc[n].lifeMax / 2;
            Main.npc[i].life = Main.npc[i].lifeMax / 2;
        }
    }
    //public class PlayerFinder : ModNPC
    //{
    //	public override void SetStaticDefaults()
    //	{
    //		DisplayName.SetDefault("玩家寻找者（？");
    //	}
    //	public override void SetDefaults()
    //	{
    //		npc.width = 32;
    //		npc.height = 2;
    //		npc.aiStyle = -1;
    //		npc.damage = 45;
    //		npc.defense = 18;
    //		npc.noGravity = true;
    //		npc.behindTiles = true;
    //		npc.lifeMax = 150;
    //		npc.HitSound = SoundID.NPCHit1;
    //		npc.knockBackResist = 1f;
    //		npc.DeathSound = SoundID.NPCDeath1;
    //		npc.value = 300f;
    //	}
    //	public Player player { get { return Main.player[npc.target]; } }
    //	private float PiOver2Rad(float rad)
    //	{
    //		rad += MathHelper.Pi * 1.25f;
    //		int r = (int)(rad * 2 / MathHelper.Pi);
    //		rad %= 4;
    //		while (rad < 0)
    //		{
    //			rad += 4;
    //		}
    //		return r * MathHelper.PiOver2 - MathHelper.Pi;
    //	}
    //	public override void AI()
    //	{
    //		for (int num31 = npc.oldPos.Length - 1; num31 > 0; num31--)
    //		{
    //			npc.oldPos[num31] = npc.oldPos[num31 - 1];
    //			oldFrameY[num31] = oldFrameY[num31 - 1];
    //		}
    //		npc.oldPos[0] = npc.Center;
    //		oldFrameY[0] = npc.frame.Y;
    //		if (npc.ai[0] == 0f)
    //		{
    //			npc.TargetClosest(true);
    //			npc.directionY = 1;
    //			npc.ai[0] = 1f;
    //		}
    //		npc.ai[2] += 0.05f;
    //		npc.ai[2] = MathHelper.Clamp(npc.ai[2], 0, 6);
    //		Vector2 vec = player.Center;
    //		bool flag3 = false;
    //		npc.ai[3]++;
    //		if (npc.ai[3] >= (vec - npc.position).Length() / 4 + 10)
    //		{
    //			npc.ai[3] = 0;
    //			flag3 = true;
    //		}
    //		bool flag = flag3 && (vec - npc.position).Length() >= (vec - npc.oldPosition).Length();
    //		bool flag2 = false;
    //		for (int n = 1; n <= 4; n++)
    //		{
    //			for (int i = 0; i < 2 * n; i++)
    //			{
    //				for (int k = 0; k < 4; k++)
    //				{
    //					Point point = (npc.Center / 16 + new Vector2(n, (i + n - n) % (2 * n) - n + 1).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //					flag2 |= Main.tile[point.X, point.Y].active();
    //				}
    //			}
    //		}
    //		Vector2 vector2 = FindTargetTile(npc, player);
    //		npc.rotation = PiOver2Rad((vector2 - npc.Center).ToRotation()) - MathHelper.PiOver2;
    //		if (!flag2)
    //		{
    //			LinerDust(vector2, npc.Center, MyDustId.RedBubble);
    //			npc.velocity = vector2 - npc.Center;
    //		}
    //		else
    //		{
    //			if (npc.ai[1] == 0f)//水平移动
    //			{
    //				if (npc.collideY)
    //				{
    //					npc.ai[0] = 2f;
    //				}
    //				if (flag)
    //				{
    //					npc.direction = -npc.direction;
    //					npc.ai[2] = 0;
    //				}
    //				if (!npc.collideY && npc.ai[0] == 2f)
    //				{
    //					npc.direction = -npc.direction;
    //					npc.ai[1] = 1f;
    //					npc.ai[0] = 1f;
    //				}
    //				if (npc.collideX)
    //				{
    //					npc.directionY = -npc.directionY;
    //					npc.ai[1] = 1f;
    //				}
    //			}
    //			else//垂直移动
    //			{
    //				if (npc.collideX)
    //				{
    //					npc.ai[0] = 2f;
    //				}
    //				if (flag)
    //				{
    //					npc.directionY = -npc.directionY;
    //					npc.ai[2] = 0;
    //				}
    //				if (!npc.collideX && npc.ai[0] == 2f)
    //				{
    //					npc.directionY = -npc.directionY;
    //					npc.ai[1] = 0f;
    //					npc.ai[0] = 1f;
    //				}
    //				if (npc.collideY)
    //				{
    //					npc.direction = -npc.direction;
    //					npc.ai[1] = 0f;
    //				}
    //			}
    //			npc.velocity.X = npc.ai[2] * npc.direction * (npc.frame.Y / 260f / 2f + 0.75f);
    //			npc.velocity.Y = npc.ai[2] * npc.directionY * (npc.frame.Y / 260f / 2f + 0.75f);
    //		}
    //	}
    //	private Vector2 FindTargetTile(Entity myself, Entity target)
    //	{
    //		List<Vector2> vectors = new List<Vector2>();
    //		for (int n = 1; n <= 60; n++)
    //		{
    //			for (int i = 0; i < 2 * n; i++)
    //			{
    //				for (int k = 0; k < 4; k++)
    //				{
    //					Point point = (myself.Center / 16 + new Vector2(n, (i + n - 1) % (2 * n) - n + 1).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //					if (Main.tile[point.X, point.Y].active() && Main.tileSolid[Main.tile[point.X, point.Y].type])
    //					{
    //						vectors.Add(point.ToVector2() * 16 + new Vector2(8, 8));
    //					}
    //				}
    //				if (vectors.Count > 0)
    //				{
    //					break;
    //				}
    //			}
    //			if (vectors.Count > 0)
    //			{
    //				break;
    //			}
    //		}
    //		if (vectors.Count > 0)
    //		{
    //			float distanceMax = float.MaxValue;
    //			int index = -1;
    //			for (int n = 0; n < vectors.Count; n++)
    //			{
    //				float distance = (target.Center - vectors[n]).Length();
    //				if (distance < distanceMax)
    //				{
    //					distanceMax = distance;
    //					index = n;
    //				}
    //			}
    //			if (index != -1)
    //			{
    //				return vectors[index];
    //			}
    //		}
    //		return default;
    //	}
    //	private readonly int[] oldFrameY = new int[10];
    //	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
    //	{
    //		spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, npc.Center + new Vector2(6 - (npc.rotation == 0 ? 2 : 0), 0).RotatedBy(npc.rotation + MathHelper.PiOver2) - Main.screenPosition, new Rectangle(0, 0, 32, 32), npc.GetAlpha(drawColor), npc.rotation, new Vector2(16, 16), 1f, npc.velocity.X < 0f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
    //		return false;
    //	}
    //}

    //public class ToolPlayer : ModPlayer
    //{
    //	public List<Vector2> vectors = new List<Vector2>();
    //	//public override void ModifyDrawLayers(List<PlayerLayer> layers)
    //	//{
    //	//	if (vectors.Count == 0)
    //	//	{
    //	//		return;
    //	//	}
    //	//	//for (int n = 0; n < vectors.Count; n++) 
    //	//	//{
    //	//	//	DrawData drawData = new DrawData(IllusionBoundMod.GetTexture("Tiles/StormZone/StormSandBrickTile_Glowa"), vectors[n], null, Color.White, 0, default, 1, SpriteEffects.None, 0);
    //	//	//}
    //	//	foreach (var vec in vectors)
    //	//	{
    //	//		DrawData drawData = new DrawData(IllusionBoundMod.GetTexture("Tiles/StormZone/StormSandBrickTile_Glowa"), vec - Main.screenPosition, null, Color.White, 0, default, 1, SpriteEffects.None, 0);
    //	//		Main.playerDrawData.Add(drawData);
    //	//	}
    //	//}
    //}
    //public class PlayerFinder : ModNPC
    //{
    //	public override void SetStaticDefaults()
    //	{
    //		DisplayName.SetDefault("爬行侦测器(测试用npc)");
    //	}
    //	public override void SetDefaults()
    //	{
    //		npc.width = 32;
    //		npc.height = 32;
    //		npc.aiStyle = -1;
    //		npc.damage = 25;
    //		npc.defense = 18;
    //		npc.noGravity = true;
    //		npc.lifeMax = 150;
    //		npc.HitSound = SoundID.NPCHit1;
    //		npc.knockBackResist = 1f;
    //		npc.DeathSound = SoundID.NPCDeath1;
    //		npc.value = 300f;
    //	}
    //	private Vector2 targetVec;
    //	private Vector2 targetPlayerVec;
    //	public PlayerFinder()
    //	{
    //		targetVec = npc.Center;
    //	}
    //	public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //	{
    //		return Main.LocalPlayer.GetModPlayer<IllusionBoundPlayer>().ZoneStorm ? 0.8f : 0;
    //	}
    //	private float PiOver2Rad(float rad)
    //	{
    //		rad += MathHelper.Pi * 1.25f;
    //		int r = (int)(rad * 2 / MathHelper.Pi);
    //		rad %= 4;
    //		while (rad < 0)
    //		{
    //			rad += 4;
    //		}
    //		return r * MathHelper.PiOver2 - MathHelper.Pi;
    //	}
    //	public Player player { get { return Main.player[npc.target]; } }
    //	public override void AI()
    //	{
    //		if (!Main.tile[(int)(targetVec.X / 16), (int)(targetVec.Y / 16)].active() && player != null)
    //		{
    //			targetVec = FindTargetTile(npc, player);
    //		}
    //		else
    //		{
    //			npc.velocity = targetVec - npc.Center;
    //			//Vector2 vel = new Vector2(0, -1).RotatedBy(PiOver2Rad((targetVec - npc.Center).ToRotation()));
    //			//Vector2 vec = targetVec;
    //			//for (int n = 0; n < 300; n++)
    //			//{

    //			//}
    //		}
    //		if (!Main.tile[(int)(targetPlayerVec.X / 16), (int)(targetPlayerVec.Y / 16)].active() && player != null)
    //		{
    //			targetPlayerVec = FindTargetTile(player, npc);
    //		}
    //	}
    //	private Vector2 FindTargetTile(NPC myself, Player target)
    //	{
    //		List<Vector2> vectors = new List<Vector2>();
    //		for (int n = 1; n <= 60; n++)
    //		{
    //			for (int i = 0; i < 2 * n; i++)
    //			{
    //				for (int k = 0; k < 4; k++)
    //				{
    //					Point point = (myself.Center / 16 + new Vector2(n, n - 1 + i).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //					if (Main.tile[point.X, point.Y].active())
    //					{
    //						vectors.Add(point.ToVector2() * 16 + new Vector2(8, 8));
    //					}
    //				}
    //				if (vectors.Count > 0)
    //				{
    //					break;
    //				}
    //			}
    //			if (vectors.Count > 0)
    //			{
    //				break;
    //			}
    //		}
    //		if (vectors.Count > 0)
    //		{
    //			float distanceMax = float.MaxValue;
    //			int index = -1;
    //			for (int n = 0; n < vectors.Count; n++)
    //			{
    //				float distance = (target.Center - vectors[n]).Length();
    //				if (distance < distanceMax)
    //				{
    //					distanceMax = distance;
    //					index = n;
    //				}
    //			}
    //			if (index != -1)
    //			{
    //				return vectors[index];
    //			}
    //		}
    //		return default;
    //	}
    //	private Vector2 FindTargetTile(Player myself, NPC target)
    //	{
    //		List<Vector2> vectors = new List<Vector2>();
    //		for (int n = 1; n <= 60; n++)
    //		{
    //			for (int i = 0; i < 2 * n; i++)
    //			{
    //				for (int k = 0; k < 4; k++)
    //				{
    //					Point point = (myself.Center / 16 + new Vector2(n, n - 1 + i).RotatedBy(MathHelper.PiOver2 * k)).ToPoint();
    //					if (Main.tile[point.X, point.Y].active())
    //					{
    //						vectors.Add(point.ToVector2() * 16 + new Vector2(8, 8));
    //					}
    //				}
    //				if (vectors.Count > 0)
    //				{
    //					break;
    //				}
    //			}
    //			if (vectors.Count > 0)
    //			{
    //				break;
    //			}
    //		}
    //		if (vectors.Count > 0)
    //		{
    //			float distanceMax = float.MaxValue;
    //			int index = -1;
    //			for (int n = 0; n < vectors.Count; n++)
    //			{
    //				float distance = (target.Center - vectors[n]).Length();
    //				if (distance < distanceMax)
    //				{
    //					distanceMax = distance;
    //					index = n;
    //				}
    //			}
    //			if (index != -1)
    //			{
    //				return vectors[index];
    //			}
    //		}
    //		return default;
    //	}
    //}
}