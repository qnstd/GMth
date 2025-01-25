using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace cngraphi.gmth
{
    /// <summary>
    /// ��ѧ���߰�
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public class MthUtils
    {
        #region �������� ǿ��ת��
        // ����int��float���͵�ǿ��ת��
        [StructLayout(LayoutKind.Explicit)]
        internal struct IntFloatUnion
        {
            [FieldOffset(0)]// ƫ��λ��
            public int intValue;
            [FieldOffset(0)]
            public float floatValue;
        }
        /// <summary>
        /// ǿ�ƽ� float ת int 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public int AS_int(float x)
        {
            IntFloatUnion u;
            u.intValue = 0;
            u.floatValue = x;
            return u.intValue;
        }
        /// <summary>
        /// ǿ�ƽ� int ת float
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float AS_float(int x)
        {
            IntFloatUnion u;
            u.floatValue = 0;
            u.intValue = x;
            return u.floatValue;
        }
        /// <summary>
        /// ǿ�ƽ� uint ת float
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float AS_float(uint x) { return AS_float((int)x); }
        /// <summary>
        /// ǿ�ƽ� float ת uint 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public uint AS_uint(float x) { return (uint)AS_int(x); }
        /// <summary>
        /// ǿ�ƽ� Uint4 ���ݽṹתΪ Vector4 ���ݽṹ
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        static public Vector4 AS_float(Uint4 u) { return new Vector4(AS_float(u.x), AS_float(u.y), AS_float(u.z), AS_float(u.w)); }
        /// <summary>
        /// ǿ�ƽ� Vector4 ת Uint4
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static public Uint4 AS_uint(Vector4 v) { return new Uint4(AS_uint(v.X), AS_uint(v.Y), AS_uint(v.Z), AS_uint(v.W)); }
        #endregion



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
        /// float ֵ�� abs ����ֵת��
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float Abs(float x) { return AS_float(AS_uint(x) & 0x7FFFFFFF); }


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