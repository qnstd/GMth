using System;
using System.Text;

namespace cngraphi.gmth
{
    /// <summary>
    /// 数学计算
    /// <para>作者：强辰</para>
    /// </summary>
    public class MthUtils
    {
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