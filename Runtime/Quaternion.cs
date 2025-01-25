using System;
using System.Numerics;

namespace cngraphi.gmth
{
    /// <summary>
    /// ��Ԫ��
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public class Quaternion
    {
        #region ��̬
        static private readonly Quaternion identity = new Quaternion(0, 0, 0, 1);
        static public Quaternion Identity { get { return identity; } }


        /// <summary>
        /// ŷ����ת��Ԫ��
        /// </summary>
        /// <param name="eular">ŷ���ǣ��Ƕ�ֵ��</param>
        /// <returns>��Ԫ��</returns>
        static public Quaternion EularTo(Vector3 eular)
        {
            Vector3 r = eular * (float)(Math.PI / 180);
            r *= 0.5f;

            // ǰ׺c = cos�� ǰ׺s = sin
            float cyaw = (float)Math.Cos(r.Z); // ��z
            float syaw = (float)Math.Sin(r.Z);
            float cpitch = (float)Math.Cos(r.Y); // ��y
            float spitch = (float)Math.Sin(r.Y);
            float croll = (float)Math.Cos(r.X); // ��x
            float sroll = (float)Math.Sin(r.X);

            Quaternion q = new Quaternion
                (
                // ��ʽ
                /* x */    cyaw * cpitch * sroll - syaw * spitch * croll,
                /* y */    syaw * cpitch * sroll + cyaw * spitch * croll,
                /* z */    syaw * cpitch * croll - cyaw * spitch * sroll,
                /* w */    cyaw * cpitch * croll + syaw * spitch * sroll
                );

            return q;
        }


        /// <summary>
        /// ��Ԫ��תŷ����
        /// </summary>
        /// <param name="q">��Ԫ��</param>
        /// <returns>ŷ���ǵĽǶ�ֵ</returns>
        static public Vector3 ToEular(Quaternion q)
        {
            Vector3 v;

            // roll �� x ����ת�ĽǶ� -��ʽ
            double sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
            double cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
            v.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch �� y ����ת�ĽǶ� -��ʽ
            double sinp = 2 * (q.w * q.y - q.z * q.x);
            if (Math.Abs(sinp) >= 1)
                v.Y = MthUtils.CopySign((float)(Math.PI * 0.5f), (float)sinp); // sinp �ķ��Ÿ��� Math.PI*0.5f
            else
                v.Y = (float)Math.Asin(sinp);

            // yaw �� z ����ת�ĽǶ� -��ʽ
            double siny_cosp = 2 * (q.w * q.z + q.x * q.y);
            double cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
            v.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);


            // ����ת�Ƕ�
            v *= (float)(180 / Math.PI);
            return v;
        }


        /// <summary>
        /// ��Ԫ��ת��ת����
        /// </summary>
        /// <param name="q">��Ԫ��</param>
        /// <returns>4x4����ת����</returns>
        static public Matrix4x4 RotateMatrix(Quaternion q)
        {
            /*
                ��ʽ

                   [-1-2(z^2 + w^2)     2(yz - xw)          2(xz + yw)          0]
                   [2(yz + xw)          1-2(y^2 + w^2)      2(zw - xy)          0]
                   [2(yw - xz)          2(xy + zw)          1-2(y^2 + z^2)      0]
                   [0                   0                   0                   1]   

                    ^ �൱�� Pow ָ��
             */

            float num = q.x * 2f;
            float num2 = q.y * 2f;
            float num3 = q.z * 2f;
            float num4 = q.x * num;
            float num5 = q.y * num2;
            float num6 = q.z * num3;
            float num7 = q.x * num2;
            float num8 = q.x * num3;
            float num9 = q.y * num3;
            float num10 = q.w * num;
            float num11 = q.w * num2;
            float num12 = q.w * num3;

            Matrix4x4 m = default;
            m.M11 = 1f - (num5 + num6);
            m.M21 = num7 + num12;
            m.M31 = num8 - num11;
            m.M41 = 0f;

            m.M12 = num7 - num12;
            m.M22 = 1f - (num4 + num6);
            m.M32 = num9 + num10;
            m.M42 = 0f;

            m.M13 = num8 + num11;
            m.M23 = num9 - num10;
            m.M33 = 1f - (num4 + num5);
            m.M43 = 0f;

            m.M14 = 0f;
            m.M24 = 0f;
            m.M34 = 0f;
            m.M44 = 1f;
            return m;
        }



        static private readonly Uint4 _0yzw = new(0x00000000, 0x80000000, 0x80000000, 0x80000000);
        static private readonly Uint4 _0y0w = new(0x00000000, 0x80000000, 0x00000000, 0x80000000);
        static private readonly Uint4 _xyz0 = new(0x80000000, 0x80000000, 0x80000000, 0x00000000);
        static private readonly Vector3 _Up = new Vector3(0, 1, 0);
        //static private readonly Vector3 _Right = new Vector3(1, 0, 0);
        //static private readonly Vector3 _Forward = new Vector3(0, 0, 1);
        /// <summary>
        /// ���ݳ������Ϸ���ȷ����ת�ĽǶ�
        /// </summary>
        /// <param name="forward">����</param>
        /// <param name="up">�Ϸ���Ĭ�ϣ�(0,1,0)��</param>
        /// <returns>һ��������ת�Ƕȵ���Ԫ��</returns>
        static public Quaternion LookRotation(Vector3 forward, Vector3 up)
        {
            Vector4 result;

            // �Գ���Ϊz�ᣬ�������µ�x�ἰy��
            Vector3 xaxis = VectorUtils.Normalize(VectorUtils.Cross(up, forward));
            Vector3 u = xaxis; //x
            Vector3 v = VectorUtils.Cross(forward, xaxis); //y
            Vector3 w = forward; //z

            #region ���� UNITY ���㷨
            uint u_sign = (MthUtils.AS_uint(u.X) & 0x80000000);
            float t = v.Y + MthUtils.AS_float(MthUtils.AS_uint(w.Z) ^ u_sign);
            Uint4 u_mask = new Uint4((int)u_sign >> 31);
            Uint4 t_mask = new Uint4(MthUtils.AS_int(t) >> 31);

            float tr = 1.0f + MthUtils.Abs(u.X);
            Uint4 sign_flips = _0yzw ^ (u_mask & _0y0w) ^ (t_mask & _xyz0);

            Vector4 v1 = new Vector4(tr, u.Y, w.X, v.Z), v2 = new Vector4(t, v.X, u.Z, w.Y);
            result = v1 + MthUtils.AS_float(MthUtils.AS_uint(v2) ^ sign_flips);   // +---, +++-, ++-+, +-++
            result = MthUtils.AS_float((MthUtils.AS_uint(result) & ~u_mask) | (MthUtils.AS_uint(result.ZWXY()) & u_mask));
            result = MthUtils.AS_float((MthUtils.AS_uint(result.WZYX()) & ~t_mask) | (MthUtils.AS_uint(result) & t_mask));
            result = VectorUtils.Normalize(result);
            #endregion

            // ������Ԫ��������
            return new Quaternion(result);
        }
        static public Quaternion LookRotation(Vector3 forward)
        {
            return LookRotation(forward, _Up);
        }
        #endregion // ��̬����


        public float w { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }


        public Quaternion() { Set(0, 0, 0, 0); }
        public Quaternion(Vector4 v) { Set(v); }
        public Quaternion(float x, float y, float z, float w) { Set(x, y, z, w); }
        public Quaternion(Quaternion q) { Set(q.x, q.y, q.z, q.w);  }


        public void Set(Vector4 v)
        {
            w = v.W;
            x = v.X;
            y = v.Y;
            z = v.Z;
        }

        public void Set(float x, float y, float z, float w)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }


        /// <summary>
        /// ���� * �Ų�����
        /// </summary>
        /// <param name="q">�Ƕȶ�Ӧ����Ԫ��</param>
        /// <param name="point">��</param>
        static public Vector3 operator *(Quaternion q, Vector3 point)
        {
            float num = q.x * 2f;
            float num2 = q.y * 2f;
            float num3 = q.z * 2f;
            float num4 = q.x * num;
            float num5 = q.y * num2;
            float num6 = q.z * num3;
            float num7 = q.x * num2;
            float num8 = q.x * num3;
            float num9 = q.y * num3;
            float num10 = q.w * num;
            float num11 = q.w * num2;
            float num12 = q.w * num3;

            Vector3 result = default;
            // ����ת��������һ��
            result.X = (1f - (num5 + num6)) * point.X + (num7 - num12) * point.Y + (num8 + num11) * point.Z;
            result.Y = (num7 + num12) * point.X + (1f - (num4 + num6)) * point.Y + (num9 - num10) * point.Z;
            result.Z = (num8 - num11) * point.X + (num9 + num10) * point.Y + (1f - (num4 + num5)) * point.Z;
            return result;
        }

    }
}