using System.Numerics;

namespace cngraphi.gmth
{
    /// <summary>
    /// �������߰�
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public static class VectorUtils
    {
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="a">����1</param>
        /// <param name="b">����2</param>
        static public float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="v1">����1</param>
        /// <param name="v2">����2</param>
        static public float Dot(Vector4 v1, Vector4 v2)
        {
            return v1.X * v2.X + v1.Y + v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }


        /// <summary>
        /// ���
        /// </summary>
        /// <param name="a">����1</param>
        /// <param name="b">����2</param>
        static public Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3
                (
                    a.Y * b.Z - a.Z * b.Y,
                    a.Z * b.X - a.X * b.Z,
                    a.X * b.Y - a.Y * b.X
                );
        }



        /// <summary>
        /// �Բ������й�һ������
        /// </summary>
        /// <param name="v">����</param>
        static public Vector3 Normalize(Vector3 v) { return MthUtils.RSqrt(Dot(v, v)) * v; }
        /// <summary>
        /// �Բ������й�һ������
        /// </summary>
        /// <param name="v">����</param>
        static public Vector4 Normalize(Vector4 v){ return MthUtils.RSqrt(Dot(v, v)) * v; }


        #region ��չ
        static public Vector4 ZWXY(this Vector4 v)
        {
            return new Vector4(v.Z, v.W, v.X, v.Y);
        }
        static public Vector4 WZYX(this Vector4 v)
        {
            return new Vector4(v.W, v.Z, v.Y, v.X);
        }
        #endregion
    }
}
