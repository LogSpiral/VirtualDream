using Microsoft.Xna.Framework.Graphics;
namespace VirtualDream.Utils
{
    public delegate T FactorFunction<T>(float fac);
    //public struct Quaternion
    //{
    //	public float r;
    //	public float i;
    //	public float j;
    //	public float k;
    //	public Matrix Transform 
    //	=> new Matrix
    //		(
    //		    r,-i,-j,-k,
    //			i, r, k, j,
    //			j,-k, r,-i,
    //			k,-j, i, r
    //		);
    //	public float LengthSquare() => r * r + i * i + j * j + k * k;
    //	public float Length => (float)Math.Sqrt(LengthSquare());
    //	public Quaternion Conjugate() 
    //	{
    //		var (s,v) = ToScalerAndVector();
    //		return new Quaternion(s, -v);
    //	}
    //	public Quaternion Inverse() 
    //	{
    //		return Conjugate() / LengthSquare();
    //	}
    //	public Vector4 ToVector4() 
    //	{
    //		return new Vector4(r, i, j, k);
    //	}
    //	public (float scaler, Vector3 vector) ToScalerAndVector() 
    //	{
    //		return (r, new Vector3(i, j, k));
    //	}
    //	public static Quaternion operator +(Quaternion q1,Quaternion q2)
    //	{
    //		return new Quaternion(q1.ToVector4() + q2.ToVector4());
    //	}
    //	public static Quaternion operator -(Quaternion q1, Quaternion q2)
    //	{
    //		return new Quaternion(q1.ToVector4() - q2.ToVector4());
    //	}
    //	public static Quaternion operator *(Quaternion q1, Quaternion q2)
    //	{
    //		return new Quaternion(q2.ToVector4().ApplyMatrix(q1.Transform));
    //	}
    //	public static Quaternion operator *(Quaternion q1, float scaler)
    //	{
    //		return new Quaternion(q1.ToVector4() * scaler);
    //	}
    //	public static Quaternion operator /(Quaternion q1, float scaler)
    //	{
    //		return new Quaternion(q1.ToVector4() / scaler);
    //	}
    //	public override string ToString()
    //       {
    //		return "scaler:" + r + " vector:(" + i + "," + j + "," + k + ")";
    //       }
    //       public Quaternion(float _r, float _i, float _j, float _k)
    //	{
    //		r = _r;
    //		i = _i;
    //		j = _j;
    //		k = _k;
    //       }
    //       public Quaternion(Vector4 vector)
    //       {
    //		r = vector.X;
    //		i = vector.Y;
    //		j = vector.Z;
    //		k = vector.W;
    //	}
    //       public Quaternion(float scaler,Vector3 vector)
    //       {
    //		r = scaler;
    //		i = vector.X;
    //		j = vector.Y;
    //		k = vector.Z;
    //	}
    //	public Quaternion((float scaler, Vector3 vector) q)
    //	{
    //		r = q.scaler;
    //		i = q.vector.X;
    //		j = q.vector.Y;
    //		k = q.vector.Z;
    //	}
    //}
    //public struct VertexTriangle3_RigidList
    //{
    //	//ListOfTriangleIn3DSpace
    //	public int Length => tris.Length;
    //	public float height;
    //	public VertexTriangle3_Rigid[] tris;
    //	public void Update() { foreach (var t in tris) t.Update(); }
    //	public void Update(Matrix matrix) { foreach (var t in tris) t.Update(matrix); }
    //	public VertexTriangle3_Rigid this[int i] => tris[i];
    //	public VertexTriangle3_RigidList(float _height, params VertexTriangle3_Rigid[] _tris)
    //	{
    //		height = _height;
    //		tris = _tris;
    //	}
    //	public void ApplyMatrix() 
    //	{

    //	}
    //	public Vector2 Projectile(Vector3 vector) => height / (height - vector.Z) * (new Vector2(vector.X, vector.Y) - Main.screenPosition - new Vector2(960, 560)) + Main.screenPosition + new Vector2(960, 560);
    //	public CustomVertexInfo[] ToVertexInfo()
    //	{
    //		var vis = new CustomVertexInfo[tris.Length * 3];
    //		for (int i = 0; i < tris.Length; i++)
    //		{
    //			for (int n = 0; n < 3; n++)
    //			{
    //				var t = tris[i];
    //				vis[i * 3 + n] = new CustomVertexInfo(Projectile((t.positions[n] + t.center).ApplyMatrix(t.transform)), t.colors[n], t.vertexs[n]);
    //			}
    //		}
    //		return vis;
    //	}
    //}
    //public struct VertexTriangle3_Rigid : IVertexTriangle
    //{
    //	public Vector3 center;
    //	public float rotation;
    //	public float velocity;
    //	public static float height;
    //	public Vector3 director;
    //	public readonly Vector3[] positions;
    //	public readonly Vector3[] vertexs;
    //	public readonly Color[] colors;
    //	public static Vector2 Projectile(Vector3 vector) => height / (height - vector.Z) * (new Vector2(vector.X, vector.Y) - Main.screenPosition - new Vector2(960, 560)) + Main.screenPosition + new Vector2(960, 560);
    //	//public Matrix Transform 
    //	//{
    //	//	get
    //	//	{
    //	//		var (s, c) = MathF.SinCos(rotation);
    //	//		var x = director.X;
    //	//		var y = director.Y;
    //	//		var z = director.Z;
    //	//		return new Matrix
    //	//		(
    //	//			x * x * (1 - c) + c    , x * y * (1 - c) - z * s, x * z * (1 - c) + y * s, 0,
    //	//			x * y * (1 - c) + z * s, y * y * (1 - c) + c    , y * z * (1 - c) - x * s, 0,
    //	//			x * z * (1 - c) - y * s, y * z * (1 - c) + x * s, z * z * (1 - c) + c    , 0,
    //	//			0                      , 0                      , 0                      , 1
    //	//		);
    //	//	}
    //	//}
    //	//public Func<Quaternion, Quaternion> transform;
    //	public Matrix transform;
    //	public void Update(params Vector3[] newPositions) 
    //	{
    //		rotation += velocity;
    //		transform = director.CreateRotationTransform(rotation);
    //		if (newPositions.Length == 0) return;
    //		for (int n = 0; n < Math.Min(newPositions.Length, 3); n++)
    //		{
    //			positions[n] = newPositions[n];
    //		}
    //	}
    //	public void Update(float scaler)
    //	{
    //		rotation += velocity;
    //		transform = director.CreateRotationTransform(rotation);
    //		director = Vector3.Normalize(director) * scaler;
    //	}
    //	public void Update(float scaler, params Vector3[] newPositions)
    //	{
    //		rotation += velocity;
    //		transform = director.CreateRotationTransform(rotation);
    //		director = Vector3.Normalize(director) * scaler;
    //		for (int n = 0; n < Math.Min(newPositions.Length, 3); n++) 
    //		{
    //			positions[n] = newPositions[n];
    //		}
    //	}
    //	public CustomVertexInfo this[int index] 
    //	{
    //		get => index > 3 || index < 0 ? default : new CustomVertexInfo(Projectile(positions[index].ApplyMatrix(transform) + center), colors[index], vertexs[index]);
    //		set 
    //		{
    //			//positon不能写入
    //			colors[index] = value.Color;
    //			vertexs[index] = value.TexCoord;
    //		}
    //	}
    //	public CustomVertexInfo A => new CustomVertexInfo(Projectile(positions[0].ApplyMatrix(transform) + center), colors[0], vertexs[0]);
    //	public CustomVertexInfo B => new CustomVertexInfo(Projectile(positions[1].ApplyMatrix(transform) + center), colors[1], vertexs[1]);
    //	public CustomVertexInfo C => new CustomVertexInfo(Projectile(positions[2].ApplyMatrix(transform) + center), colors[2], vertexs[2]);
    //	public static CustomVertexInfo[] ToVertexInfo(VertexTriangle3_Rigid[] tris)
    //	{
    //		var vis = new CustomVertexInfo[tris.Length * 3];
    //		for (int i = 0; i < tris.Length; i++)
    //		{
    //			for (int n = 0; n < 3; n++)
    //			{
    //				var t = tris[i];
    //				vis[i * 3 + n] = t[n];
    //			}
    //		}
    //		return vis;
    //	}
    //       public VertexTriangle3_Rigid(Vector3 _center,Vector3 _director,Vector3 _A, Vector3 _B,Vector3 _C, Vector3[] _vertexs, Color[] _colors, float scaler, float _velocity = MathHelper.Pi / 60, float _rotation = 0)
    //       {
    //		positions = new Vector3[3];
    //		vertexs = new Vector3[3];
    //		colors = new Color[3];
    //		center = _center;
    //		rotation = _rotation;
    //		velocity = _velocity;
    //		director = Vector3.Normalize(_director) * scaler;
    //		transformer = default;
    //		positions[0] = _A;
    //		positions[1] = _B;
    //		positions[2] = _C;
    //		transform = director.CreateRotationTransform(rotation);
    //	}
    //	public VertexTriangle3_Rigid(Vector3 _center, Vector3 _director, (Vector3 _A, Vector3 _B, Vector3 _C) vs, Vector3[] _vertexs, Color[] _colors, float scaler, float _velocity = MathHelper.Pi / 60, float _rotation = 0)
    //	{
    //		positions = new Vector3[3];
    //		vertexs = new Vector3[3];
    //		colors = new Color[3];
    //		center = _center;
    //		rotation = _rotation;
    //		velocity = _velocity;
    //		director = Vector3.Normalize(_director) * scaler;
    //		transformer = default;
    //		positions[0] = vs._A;
    //		positions[1] = vs._B;
    //		positions[2] = vs._C;
    //		transform = director.CreateRotationTransform(rotation);
    //	}
    //	public VertexTriangle3_Rigid(Vector3 _center, Vector3 _director, Vector3[] _pisitions, Vector3[] _vertexs, Color[] _colors, float scaler, float _velocity = MathHelper.Pi / 60, float _rotation = 0)
    //	{
    //		positions = new Vector3[3];
    //		vertexs = new Vector3[3];
    //		colors = new Color[3];
    //		center = _center;
    //		rotation = _rotation;
    //		velocity = _velocity;
    //		director = Vector3.Normalize(_director) * scaler;
    //		transformer = default;
    //		for (int n = 0; n < 3; n++) 
    //		{
    //			positions[n] = _pisitions[n];
    //		}
    //		for (int n = 0; n < 3; n++)
    //		{
    //			vertexs[n] = _vertexs[n];
    //		}
    //		for (int n = 0; n < 3; n++)
    //		{
    //			colors[n] = _colors[n];
    //		}
    //		transform = director.CreateRotationTransform(rotation);
    //	}
    //}
    public class LoopArray<T>
    {
        public T[] array;
        public virtual T this[int index]
        {
            get
            {
                while (index < 0)
                {
                    index += array.Length;
                }

                while (index >= array.Length)
                {
                    index -= array.Length;
                }

                return array[index];
            }
            set
            {
                while (index < 0)
                {
                    index += array.Length;
                }

                while (index >= array.Length)
                {
                    index -= array.Length;
                }

                array[index] = value;
            }
        }
        public int Length => array.Length;
        public LoopArray(T[] _array)
        {
            array = _array;
        }
        public static implicit operator T[](LoopArray<T> myArray)
        {
            return myArray.array;
        }
        //public static explicit operator T[](LoopArray<T> myArray) 
        //{
        //	return myArray.array;
        //}
        //public static explicit operator T[](LoopArray<T> myArray)
        //{
        //	return myArray.array;
        //}
    }
    public struct VertexTriangle3List
    {
        //ListOfTriangleIn3DSpace
        public int Length => tris.Length;
        public float height;
        public Vector2 offset;
        public VertexTriangle3[] tris;
        public VertexTriangle3 this[int i] => tris[i];

        public VertexTriangle3List(float _height, Vector2 _offset, params VertexTriangle3[] _tris)
        {
            height = _height;
            offset = _offset;
            tris = _tris;
        }
        public Vector2 Projectile(Vector3 vector) => height / (height - vector.Z) * (new Vector2(vector.X, vector.Y) + offset - Main.screenPosition - new Vector2(960, 560)) + Main.screenPosition + new Vector2(960, 560);
        public CustomVertexInfo[] ToVertexInfo()
        {
            var vis = new CustomVertexInfo[tris.Length * 3];
            for (int i = 0; i < tris.Length; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    var t = tris[i];
                    vis[i * 3 + n] = new CustomVertexInfo(Projectile(t.positions[n]), t.colors[n], t.vertexs[n]);
                }
            }
            return vis;
        }
    }
    public struct VertexTriangle3 : IVertexTriangle
    {
        //TriangleIn3DSpace
        public VertexTriangle3(Vector3 vA, Vector3 vB, Vector3 vC, Color cA, Color cB, Color cC, Vector3 pA = default, Vector3 pB = default, Vector3 pC = default)
        {
            positions = new Vector3[3];
            vertexs = new Vector3[3];
            colors = new Color[3];
            vertexs[0] = vA;
            vertexs[1] = vB;
            vertexs[2] = vC;
            colors[0] = cA;
            colors[1] = cB;
            colors[2] = cC;
            positions[0] = pA;
            positions[1] = pB;
            positions[2] = pC;
        }
        public static float height = 100;
        public readonly Vector3[] positions;
        public readonly Vector3[] vertexs;
        public readonly Color[] colors;
        public static Vector2 offset = default;
        public static Vector2 Projectile(Vector3 vector) => height / (height - vector.Z) * (new Vector2(vector.X, vector.Y) + offset - Main.screenPosition - new Vector2(960, 560)) + Main.screenPosition + new Vector2(960, 560);
        public CustomVertexInfo this[int index] => new CustomVertexInfo(Projectile(positions[index]), colors[index], vertexs[index]);
        public CustomVertexInfo A => new CustomVertexInfo(Projectile(positions[0]), colors[0], vertexs[0]);
        public CustomVertexInfo B => new CustomVertexInfo(Projectile(positions[1]), colors[1], vertexs[1]);
        public CustomVertexInfo C => new CustomVertexInfo(Projectile(positions[2]), colors[2], vertexs[2]);
        public static CustomVertexInfo[] ToVertexInfo(VertexTriangle3[] tris)
        {
            var vis = new CustomVertexInfo[tris.Length * 3];
            for (int i = 0; i < tris.Length; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    var t = tris[i];
                    vis[i * 3 + n] = new CustomVertexInfo(Projectile(t.positions[n]), t.colors[n], t.vertexs[n]);
                }
            }
            return vis;
        }
    }
    public struct VertexTriangleList
    {
        public int Length => tris.Length;
        public Vector2 offset;
        public VertexTriangle this[int i] => tris[i];

        public VertexTriangle[] tris;
        public Vector2 GetRealPosition(Vector2 vector) => vector + offset;
        public VertexTriangleList(Vector2 _offset, params VertexTriangle[] _tris)
        {
            offset = _offset;
            tris = _tris;
        }
        public CustomVertexInfo[] ToVertexInfo()
        {
            var vis = new CustomVertexInfo[tris.Length * 3];
            for (int i = 0; i < tris.Length; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    var t = tris[i];
                    vis[i * 3 + n] = new CustomVertexInfo(GetRealPosition(t.positions[n]), t.colors[n], t.vertexs[n]);
                }
            }
            return vis;
        }
    }
    public struct VertexTriangle : IVertexTriangle
    {
        public VertexTriangle(Vector3 vA, Vector3 vB, Vector3 vC, Color cA, Color cB, Color cC, Vector2 pA = default, Vector2 pB = default, Vector2 pC = default)
        {
            positions = new Vector2[3];
            vertexs = new Vector3[3];
            colors = new Color[3];
            vertexs[0] = vA;
            vertexs[1] = vB;
            vertexs[2] = vC;
            colors[0] = cA;
            colors[1] = cB;
            colors[2] = cC;
            positions[0] = pA;
            positions[1] = pB;
            positions[2] = pC;
        }
        public readonly Vector2[] positions;
        public readonly Vector3[] vertexs;
        public readonly Color[] colors;
        public static Vector2 offset = default;
        public static Vector2 GetRealPosition(Vector2 vector) => vector + offset;
        public CustomVertexInfo this[int index] => new CustomVertexInfo(GetRealPosition(positions[index]), colors[index], vertexs[index]);
        public CustomVertexInfo A => new CustomVertexInfo(GetRealPosition(positions[0]), colors[0], vertexs[0]);
        public CustomVertexInfo B => new CustomVertexInfo(GetRealPosition(positions[1]), colors[1], vertexs[1]);
        public CustomVertexInfo C => new CustomVertexInfo(GetRealPosition(positions[2]), colors[2], vertexs[2]);
        public static CustomVertexInfo[] ToVertexInfo(VertexTriangle[] tris)
        {
            var vis = new CustomVertexInfo[tris.Length * 3];
            for (int i = 0; i < tris.Length; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    var t = tris[i];
                    vis[i * 3 + n] = t[n];
                }
            }
            return vis;
        }

    }
    public interface IVertexTriangle
    {
        //这个不好用（明明是阿汪你不会用
        CustomVertexInfo A { get; }
        CustomVertexInfo B { get; }
        CustomVertexInfo C { get; }
        CustomVertexInfo this[int index] { get; }
        //CustomVertexInfo[] ToVertexInfo(IVertexTriangle[] tris);
    }
    public enum DirOf3DRotation
    {
        x_Axis_P,
        x_Axis_N,
        y_Axis_P,
        y_Axis_N,
        z_Axis_P,
        z_Axis_N
    }
    public struct CustomVertexInfo : IVertexType
    {
        private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
        {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
        });//这里问别人吧（
        public Vector2 Position;//顶点的位置
        public Color Color;
        public Vector3 TexCoord;
        public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
        {
            this.Position = position;
            this.Color = color;
            this.TexCoord = texCoord;
        }
        public CustomVertexInfo(Vector2 position, Vector3 texCoord)
        {
            this.Position = position;
            this.Color = Color.White;
            this.TexCoord = texCoord;
        }
        public VertexDeclaration VertexDeclaration
        {
            get
            {
                return _vertexDeclaration;
            }
        }
    }
    public enum ShaderTailTexture
    {
        Fire = 0,
        Frozen = 1,
        Yellow = 2,
        White = 3,
        Solar = 4,
        Nebula = 5,
        Vortex = 6,
        StarDust = 7,
        RainBow = 8,
        NegativeRB = 9
    }
    public enum ShaderTailStyle
    {
        Dust = 0,
        Light = 1,
        Bolt = 2,
        Dust2 = 3,
        Zenith = 4,
        PiercingStarlight = 5
    }
    public enum ShaderTailMainStyle
    {
        MiddleLine = 0,
        BottomCurve = 1,
        TopCurve = 2,
        MiddleLine2 = 3,
    }
    public enum ConvertType
    {
        // Token: 0x0400027B RID: 635
        Pure,
        // Token: 0x0400027C RID: 636
        Corrupt,
        // Token: 0x0400027D RID: 637
        Crimson,
        // Token: 0x0400027E RID: 638
        Hallow
    }
}
