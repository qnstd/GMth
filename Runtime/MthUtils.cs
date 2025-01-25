using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace cngraphi.gmth
{
    /// <summary>
    /// 数学工具包
    /// <para>作者：强辰</para>
    /// </summary>
    public class MthUtils
    {
        #region 数据类型 强制转换
        // 创建int，float类型的强制转换
        [StructLayout(LayoutKind.Explicit)]
        internal struct IntFloatUnion
        {
            [FieldOffset(0)]// 偏移位置
            public int intValue;
            [FieldOffset(0)]
            public float floatValue;
        }
        /// <summary>
        /// 强制将 float 转 int 
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
        /// 强制将 int 转 float
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
        /// 强制将 uint 转 float
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float AS_float(uint x) { return AS_float((int)x); }
        /// <summary>
        /// 强制将 float 转 uint 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public uint AS_uint(float x) { return (uint)AS_int(x); }
        /// <summary>
        /// 强制将 Uint4 数据结构转为 Vector4 数据结构
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        static public Vector4 AS_float(Uint4 u) { return new Vector4(AS_float(u.x), AS_float(u.y), AS_float(u.z), AS_float(u.w)); }
        /// <summary>
        /// 强制将 Vector4 转 Uint4
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
        /// 角度转弧度
        /// </summary>
        static public double Deg2Rad { get { return PI / 180; } }


        /// <summary>
        /// 弧度转角度
        /// </summary>
        static public double Rad2Deg { get { return 57.29578F; } }


        /// <summary>
        /// float 值的 abs 绝对值转换
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float Abs(float x) { return AS_float(AS_uint(x) & 0x7FFFFFFF); }


        /// <summary>
        /// 返回参数值的符号
        /// </summary>
        /// <param name="f">值</param>
        /// <returns>负值：-1，正值：1</returns>
        static public int Sign(float f)
        {
            return (Step(0, f) == 0) ? (-1) : 1;
        }
        static public int Sign(int f)
        {
            return Sign((float)f);
        }


        /// <summary>
        /// 拷贝符号
        /// </summary>
        /// <param name="x">值1</param>
        /// <param name="y">值2</param>
        /// <returns>取参数1的值，取参数2的符号</returns>
        static public float CopySign(float x, float y)
        {
            return Math.Abs(x) * Sign(y);
        }
        static public int CopySign(int x, int y)
        {
            return CopySign(x, y);
        }


        /// <summary>
        /// Step 操作
        /// </summary>
        /// <param name="x">参考值</param>
        /// <param name="a">值</param>
        /// <returns>值小于参考值，则返回0；否则返回1</returns>
        static public int Step(float x, float a)
        {
            return (a < x) ? 0 : 1;
        }


        /// <summary>
        /// 开平方
        /// </summary>
        /// <param name="x">值</param>
        /// <returns></returns>
        static public float Sqrt(float x) { return (float)Math.Sqrt(x); }


        /// <summary>
        /// 开平方的倒数
        /// </summary>
        /// <param name="x">值</param>
        /// <returns></returns>
        static public float RSqrt(float x) { return 1.0f / Sqrt(x); }



        /// <summary>
        /// 随机整数
        /// </summary>
        /// <param name="length">随机数长度</param>
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
        /// 重复运动
        /// <para>根据参数t（时间），在 0-len 范围内重复运动</para>
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
        /// <para>根据参数t（时间），在 0-len 范围内往返运动</para>
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