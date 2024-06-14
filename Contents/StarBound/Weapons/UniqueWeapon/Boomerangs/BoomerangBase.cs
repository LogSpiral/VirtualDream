using System;

namespace VirtualDream.Contents.StarBound.Weapons.UniqueWeapon.Boomerangs
{
    public abstract class BoomerangBaseProj : ModProjectile, IStarboundWeaponProjectile
    {
		public Projectile projectile => Projectile;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("飞镖");
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.DamageType = DamageClass.Melee;
			projectile.timeLeft = 180;
			projectile.penetrate = -1;
        }
		public override bool PreDraw(ref Color lightColor)
		{
			float length = projectile.oldPos.Length;
			var tex = TextureAssets.Projectile[Type].Value;
			var rectangle = tex.Frame(2, 1, (int)Projectile.ai[1]);
			var origin = tex.Size() * new Vector2(.25f, .5f);
			for (int n = 0; n < length; n++)
			{
				var factor = (length - n) / length;
				Main.EntitySpriteDraw(tex, projectile.oldPos[n] - Main.screenPosition, rectangle, lightColor * factor, projectile.oldRot[n], origin, (float)Math.Sqrt(factor), 0, 0);
			}
			return false;
		}
		public override void AI()
		{
			float v = 1.5f;
			if (projectile.localAI[0] == 0f)
			{
				projectile.localAI[1] += 1f;
				if (projectile.localAI[1] >= 45f)
				{
					projectile.localAI[0] = 1f;
					projectile.localAI[1] = 0f;
					projectile.netUpdate = true;
				}
				else if (projectile.localAI[1] >= 30f)
				{
					projectile.localAI[0] = 1f;
					projectile.localAI[1] = 0f;
					projectile.netUpdate = true;
				}
			}
			else
			{
				projectile.tileCollide = false;
				float num42 = 16f;
				float num43 = 1.2f;
				Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
				float num44 = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - vector2.X;
				float num45 = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - vector2.Y;
				float num46 = (float)Math.Sqrt((double)(num44 * num44 + num45 * num45));
				if (num46 > 3000f)
				{
					projectile.Kill();
				}
				num46 = num42 / num46;
				num44 *= num46;
				num45 *= num46;
				if (projectile.velocity.X < num44 * v)
				{
					projectile.velocity.X = projectile.velocity.X + num43;
					if (projectile.velocity.X < 0f && num44 * v > 0f)
					{
						projectile.velocity.X = projectile.velocity.X + num43;
					}
				}
				else if (projectile.velocity.X > num44 * v)
				{
					projectile.velocity.X = projectile.velocity.X - num43;
					if (projectile.velocity.X > 0f && num44 * v < 0f)
					{
						projectile.velocity.X = projectile.velocity.X - num43;
					}
				}
				if (projectile.velocity.Y < num45 * v)
				{
					projectile.velocity.Y = projectile.velocity.Y + num43;
					if (projectile.velocity.Y < 0f && num45 * v > 0f)
					{
						projectile.velocity.Y = projectile.velocity.Y + num43;
					}
				}
				else if (projectile.velocity.Y > num45 * v)
				{
					projectile.velocity.Y = projectile.velocity.Y - num43;
					if (projectile.velocity.Y > 0f && num45 * v < 0f)
					{
						projectile.velocity.Y = projectile.velocity.Y - num43;
					}
				}
				if (Main.myPlayer == projectile.owner)
				{
					Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
					Rectangle value2 = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
					if (rectangle.Intersects(value2))
					{
						projectile.Kill();
					}
				}
			}
			Vector2 vector4 = projectile.ai[0] == 0f ? projectile.velocity : (projectile.Center - Main.player[projectile.owner].Center);
			vector4.Normalize();
			projectile.rotation = (float)Math.Atan2(vector4.Y, vector4.X) + 1.57f + (float)VirtualDreamSystem.ModTime2 * MathHelper.Pi / 15;
			for (int n = projectile.oldPos.Length - 1; n > 0; n--) 
			{
				projectile.oldPos[n] = projectile.oldPos[n - 1];
				projectile.oldRot[n] = projectile.oldRot[n - 1];
			}
			projectile.oldPos[0] = projectile.Center;
			projectile.oldRot[0] = projectile.rotation;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.velocity.X = -projectile.velocity.X;
			projectile.velocity.Y = -projectile.velocity.Y;
			return false;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[projectile.owner] = 3;
			base.OnHitNPC(target, hit, damageDone);
		}
	}
}
