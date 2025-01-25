using System.Numerics;

namespace cngraphi.gmth
{
    /// <summary>
    /// Matrix4x4 矩阵工具包
    /// <para>作者：强辰</para>
    /// </summary>
    public class M44Utils
    {
        /// <summary>
        /// 创建偏移矩阵
        /// </summary>
        /// <param name="v">偏移向量</param>
        /// <returns></returns>
        static public Matrix4x4 CreateTranslation(Vector3 v)
        {
            return new Matrix4x4
                        (
                            1, 0, 0, v.X,
                            0, 1, 0, v.Y,
                            0, 0, 1, v.Z,
                            0, 0, 0, 1
                        );
        }

        /// <summary>
        /// 创建缩放矩阵
        /// </summary>
        /// <param name="v">缩放向量</param>
        /// <returns></returns>
        static public Matrix4x4 CreateScale(Vector3 v)
        {
            return new Matrix4x4
                    (
                        v.X, 0, 0, 0,
                        0, v.Y, 0, 0,
                        0, 0, v.Z, 0,
                        0, 0, 0, 1
                    );
        }


        /// <summary>
        /// 创建旋转矩阵
        /// </summary>
        /// <param name="q">四元数</param>
        /// <returns></returns>
        static public Matrix4x4 CreateRotate(Quaternion q)
        {
            return Quaternion.RotateMatrix(q);
        }
        /// <summary>
        /// 创建旋转矩阵
        /// </summary>
        /// <param name="eular">欧拉角（角度值）</param>
        /// <returns></returns>
        static public Matrix4x4 CreateRotate(Vector3 eular)
        {
            return Quaternion.RotateMatrix(Quaternion.EularTo(eular));
        }


        /// <summary>
        /// TRS 变换矩阵
        /// </summary>
        /// <param name="t">偏移</param>
        /// <param name="r">旋转角度（欧拉角）</param>
        /// <param name="s">缩放</param>
        /// <returns></returns>
        static public Matrix4x4 TRS(Vector3 t, Vector3 r, Vector3 s)
        {
            return trs(t, Quaternion.EularTo(r), s); 
        }
        /// <summary>
        /// TRS 变换矩阵
        /// </summary>
        /// <param name="t">偏移</param>
        /// <param name="r">旋转</param>
        /// <param name="s">缩放</param>
        /// <returns></returns>
        static public Matrix4x4 TRS(Vector3 t, Quaternion r, Vector3 s)
        {
            return trs(t, r, s);
        }
        static private Matrix4x4 trs(Vector3 t, Quaternion r, Vector3 s)
        {
            return CreateTranslation(t) * CreateRotate(r) * CreateScale(s);
        }


        /// <summary>
        /// 矩阵与向量相乘
        /// </summary>
        /// <param name="m">4x4矩阵</param>
        /// <param name="vec">向量</param>
        /// <returns>新的向量</returns>
        static public Vector3 MultiVector(Matrix4x4 m, Vector3 vec)
        {
            Vector3 o = default(Vector3);

            o.X = m.M11 * vec.X + m.M12 * vec.Y + m.M13 * vec.Z;
            o.Y = m.M21 * vec.X + m.M22 * vec.Y + m.M23 * vec.Z;
            o.Z = m.M31 * vec.X + m.M32 * vec.Y + m.M33 * vec.Z;

            return o;
        }


        /// <summary>
        /// 矩阵与点相乘（只是加了偏移，未对矩阵最后一行参数所带来的缩放影响进行处理）
        /// </summary>
        /// <param name="m">4x4矩阵</param>
        /// <param name="point">点</param>
        /// <returns>新的向量</returns>
        static public Vector3 MultiPoint3x4(Matrix4x4 m, Vector3 point)
        {
            Vector3 o = default(Vector3);

            // 点是有偏移的，需要加上矩阵的 tx，ty，tz 偏移项
            o.X = m.M11 * point.X + m.M12 * point.Y + m.M13 * point.Z + m.M14;
            o.Y = m.M21 * point.X + m.M22 * point.Y + m.M23 * point.Z + m.M24;
            o.Z = m.M31 * point.X + m.M32 * point.Y + m.M33 * point.Z + m.M34;

            return o;
        }

        /// <summary>
        /// 矩阵与点相乘（通用，已处理缩放因素带来的影响）
        /// </summary>
        /// <param name="m">4x4矩阵</param>
        /// <param name="point">点</param>
        /// <returns>新的向量</returns>
        static public Vector3 MultiPoint(Matrix4x4 m, Vector3 point)
        {
            Vector3 o = default(Vector3);

            // 点是有偏移的，需要加上矩阵的 tx，ty，tz 偏移项
            o.X = m.M11 * point.X + m.M12 * point.Y + m.M13 * point.Z + m.M14;
            o.Y = m.M21 * point.X + m.M22 * point.Y + m.M23 * point.Z + m.M24;
            o.Z = m.M31 * point.X + m.M32 * point.Y + m.M33 * point.Z + m.M34;

            float num = m.M41 * point.X + m.M42 * point.Y + m.M43 * point.Z + m.M44;
            num = 1f / num;
            o.X *= num;
            o.Y *= num;
            o.Z *= num;

            return o;
        }
    }

}