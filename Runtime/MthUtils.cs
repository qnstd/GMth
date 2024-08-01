using System;
using System.Text;

namespace cngraphi.gmth
{
    /// <summary>
    /// ��ѧ����
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public class MthUtils
    {
        /// <summary>
        /// PI
        /// </summary>
        static public double PI { get { return 3.14159274F; } }


        /// <summary>
        /// �Ƕ�ת����
        /// </summary>
        static public double Deg2Rad { get { return PI / 180; } }


        /// <summary>
        /// ����ת�Ƕ�
        /// </summary>
        static public double Rad2Deg { get { return 57.29578F; } }


        /// <summary>
        /// ���ز���ֵ�ķ���
        /// </summary>
        /// <param name="f">ֵ</param>
        /// <returns>��ֵ��-1����ֵ��1</returns>
        static public int Sign(float f)
        {
            return (Step(0, f) == 0) ? (-1) : 1;
        }
        static public int Sign(int f)
        {
            return Sign((float)f);
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="x">ֵ1</param>
        /// <param name="y">ֵ2</param>
        /// <returns>ȡ����1��ֵ��ȡ����2�ķ���</returns>
        static public float CopySign(float x, float y)
        {
            return Math.Abs(x) * Sign(y);
        }
        static public int CopySign(int x, int y)
        {
            return CopySign(x, y);
        }


        /// <summary>
        /// Step ����
        /// </summary>
        /// <param name="x">�ο�ֵ</param>
        /// <param name="a">ֵ</param>
        /// <returns>ֵС�ڲο�ֵ���򷵻�0�����򷵻�1</returns>
        static public int Step(float x, float a)
        {
            return (a < x) ? 0 : 1;
        }


        /// <summary>
        /// ��ƽ��
        /// </summary>
        /// <param name="x">ֵ</param>
        /// <returns></returns>
        static public float Sqrt(float x) { return (float)Math.Sqrt(x); }


        /// <summary>
        /// ��ƽ���ĵ���
        /// </summary>
        /// <param name="x">ֵ</param>
        /// <returns></returns>
        static public float RSqrt(float x) { return 1.0f / Sqrt(x); }



        /// <summary>
        /// �������
        /// </summary>
        /// <param name="length">���������</param>
        /// <returns></returns>
        public static int RandomInt(int length)
        {
            StringBuilder result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return int.Parse(result.ToString());
        }



        /// <summary>
        /// �ظ��˶�
        /// <para>���ݲ���t��ʱ�䣩���� 0-len ��Χ���ظ��˶�</para>
        /// </summary>
        /// <param name="t"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        static public float Repeat(float t, float len)
        {
            return Math.Clamp(t - MathF.Floor(t / len) * len, 0, len);
        }


        /// <summary>
        /// PingPong
        /// <para>���ݲ���t��ʱ�䣩���� 0-len ��Χ�������˶�</para>
        /// </summary>
        /// <param name="t"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        static public float PingPong(float t, float len)
        {
            t = Repeat(t, len * 2);
            return len - Math.Abs(t - len);
        }

    }
}