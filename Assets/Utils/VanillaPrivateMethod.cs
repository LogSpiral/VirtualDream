using System;
using System.Collections.Generic;

namespace VirtualDream.Utils
{
    public static class VanillaPrivateMethod
    {
		public static void DrawWhip(this Projectile proj)
		{
			List<Vector2> list = new List<Vector2>();
			Projectile.FillWhipControlPoints(proj, list);
			Texture2D value = TextureAssets.FishingLine.Value;
            Rectangle value2 = value.Frame();
			Vector2 origin = new Vector2(value2.Width / 2, 2f);
            Color originalColor = Color.White;
			Vector2 value3 = list[0];
			for (int i = 0; i < list.Count - 1; i++)
			{
				Vector2 vector = list[i];
				Vector2 vector2 = list[i + 1] - vector;
				float rotation = vector2.ToRotation() - (float)Math.PI / 2f;
                Color color = Lighting.GetColor(vector.ToTileCoordinates(), originalColor);
				Vector2 scale = new Vector2(1f, (vector2.Length() + 2f) / (float)value2.Height);
				Main.spriteBatch.Draw(value, value3 - Main.screenPosition, value2, color, rotation, origin, scale, SpriteEffects.None, 0f);
				value3 += vector2;
			}
			DrawWhip_WhipBland(proj, list);
		}
		public static Vector2 DrawWhip_WhipBland(Projectile proj, List<Vector2> controlPoints,Texture2D otherTex = null)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (proj.spriteDirection == 1)
				spriteEffects ^= SpriteEffects.FlipHorizontally;

			Texture2D value = otherTex ?? TextureAssets.Projectile[proj.type].Value;
			Rectangle rectangle = value.Frame(1, 5);
			int height = rectangle.Height;
			rectangle.Height -= 2;
			Vector2 vector = rectangle.Size() / 2f;
			Vector2 vector2 = controlPoints[0];
			for (int i = 0; i < controlPoints.Count - 1; i++)
			{
				bool flag = false;
				Vector2 origin = vector;
				float scale = 1f;
				if (i == 0)
				{
					origin.Y -= 4f;
					flag = true;
				}
				else
				{
					flag = true;
					int num = 1;
					if (i > 10)
						num = 2;

					if (i > 20)
						num = 3;

					rectangle.Y = height * num;
				}

				if (i == controlPoints.Count - 2)
				{
					flag = true;
					rectangle.Y = height * 4;
					scale = 1.3f;
					Projectile.GetWhipSettings(proj, out float timeToFlyOut, out int _, out float _);
					float t = proj.ai[0] / timeToFlyOut;
					float amount = Terraria.Utils.GetLerpValue(0.1f, 0.7f, t, clamped: true) * Terraria.Utils.GetLerpValue(0.9f, 0.7f, t, clamped: true);
					scale = MathHelper.Lerp(0.5f, 1.5f, amount);
				}

				Vector2 vector3 = controlPoints[i];
				Vector2 vector4 = controlPoints[i + 1] - vector3;
				if (flag)
				{
					float rotation = vector4.ToRotation() - (float)Math.PI / 2f;
					Color color = Lighting.GetColor(vector3.ToTileCoordinates());
					Main.spriteBatch.Draw(value, vector2 - Main.screenPosition, rectangle, color, rotation, origin, scale, spriteEffects, 0f);
				}

				vector2 += vector4;
			}

			return vector2;
		}
		public static bool FindSharpTearsOpening(int x, int y, bool acceptLeft, bool acceptRight, bool acceptUp, bool acceptDown)
		{
			if (acceptLeft && !WorldGen.SolidTile(x - 1, y))
				return true;

			if (acceptRight && !WorldGen.SolidTile(x + 1, y))
				return true;

			if (acceptUp && !WorldGen.SolidTile(x, y - 1))
				return true;

			if (acceptDown && !WorldGen.SolidTile(x, y + 1))
				return true;

			return false;
		}
		public static Point FindSharpTearsSpot(this Player player,Vector2 targetSpot)
		{
			Point point = targetSpot.ToTileCoordinates();
			Vector2 center = player.Center;
			Vector2 endPoint = targetSpot;
			int samplesToTake = 3;
			float samplingWidth = 4f;
			Collision.AimingLaserScan(center, endPoint, samplingWidth, samplesToTake, out Vector2 vectorTowardsTarget, out float[] samples);
			float num = float.PositiveInfinity;
			for (int i = 0; i < samples.Length; i++)
			{
				if (samples[i] < num)
					num = samples[i];
			}

			targetSpot = center + vectorTowardsTarget.SafeNormalize(Vector2.Zero) * num;
			point = targetSpot.ToTileCoordinates();
			Rectangle value = new Rectangle(point.X, point.Y, 1, 1);
			value.Inflate(6, 16);
			Rectangle value2 = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
			value2.Inflate(-40, -40);
			value = Rectangle.Intersect(value, value2);
			List<Point> list = new List<Point>();
			List<Point> list2 = new List<Point>();
			for (int j = value.Left; j <= value.Right; j++)
			{
				for (int k = value.Top; k <= value.Bottom; k++)
				{
					if (!WorldGen.SolidTile(j, k))
						continue;

					Vector2 value3 = new Vector2(j * 16 + 8, k * 16 + 8);
					if (!(Vector2.Distance(targetSpot, value3) > 200f))
					{
						if (FindSharpTearsOpening(j, k, j > point.X, j < point.X, k > point.Y, k < point.Y))
							list.Add(new Point(j, k));
						else
							list2.Add(new Point(j, k));
					}
				}
			}

			if (list.Count == 0 && list2.Count == 0)
				list.Add((player.Center.ToTileCoordinates().ToVector2() + Main.rand.NextVector2Square(-2f, 2f)).ToPoint());

			List<Point> list3 = list;
			if (list3.Count == 0)
				list3 = list2;

			int index = Main.rand.Next(list3.Count);
			return list3[index];
		}
	}
}
