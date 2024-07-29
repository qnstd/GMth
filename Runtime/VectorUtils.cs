using UnityEngine;

namespace cngraphi.gmth
{
    /// <summary>
    /// �������߰�
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public class VectorUtils
    {
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="a">����1</param>
        /// <param name="b">����2</param>
        static public float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
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
                    a.y * b.z - a.z * b.y,
                    a.z * b.x - a.x * b.z,
                    a.x * b.y - a.y * b.x
                );
        }



        /// <summary>
        /// �Բ������й�һ������
        /// </summary>
        /// <param name="x">����</param>
        /// <returns></returns>
        static public Vector3 Normalize(Vector3 x) { return MthUtils.RSqrt(Dot(x, x)) * x; }
    }
}