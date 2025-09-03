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
}