using UnityEngine;

namespace cngraphi.gmth
{
    /// <summary>
    /// 向量工具包
    /// <para>作者：强辰</para>
    /// </summary>
    public class VectorUtils
    {
        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="a">向量1</param>
        /// <param name="b">向量2</param>
        static public float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }



        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="a">向量1</param>
        /// <param name="b">向量2</param>
        static public Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3
                (
                    a.y * b.z - a.z * b.y,
                    a.z * b.x - a.x * b.z,
                    a.x * b.y - a.y * b.x
                );
        }



        /// <summary>
        /// 对参数进行归一化处理
        /// </summary>
        /// <param name="x">向量</param>
        /// <returns></returns>
        static public Vector3 Normalize(Vector3 x) { return MthUtils.RSqrt(Dot(x, x)) * x; }
    }
}
