using System.Numerics;

namespace cngraphi.gmth
{
    /// <summary>
    /// Matrix4x4 ���󹤾߰�
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public class M44Utils
    {
        /// <summary>
        /// ����ƫ�ƾ���
        /// </summary>
        /// <param name="v">ƫ������</param>
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
        /// �������ž���
        /// </summary>
        /// <param name="v">��������</param>
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
        /// ������ת����
        /// </summary>
        /// <param name="q">��Ԫ��</param>
        /// <returns></returns>
        static public Matrix4x4 CreateRotate(Quaternion q)
        {
            return Quaternion.RotateMatrix(q);
        }
        /// <summary>
        /// ������ת����
        /// </summary>
        /// <param name="eular">ŷ���ǣ��Ƕ�ֵ��</param>
        /// <returns></returns>
        static public Matrix4x4 CreateRotate(Vector3 eular)
        {
            return Quaternion.RotateMatrix(Quaternion.EularTo(eular));
        }


        /// <summary>
        /// TRS �任����
        /// </summary>
        /// <param name="t">ƫ��</param>
        /// <param name="r">��ת�Ƕȣ�ŷ���ǣ�</param>
        /// <param name="s">����</param>
        /// <returns></returns>
        static public Matrix4x4 TRS(Vector3 t, Vector3 r, Vector3 s)
        {
            return trs(t, Quaternion.EularTo(r), s); 
        }
        /// <summary>
        /// TRS �任����
        /// </summary>
        /// <param name="t">ƫ��</param>
        /// <param name="r">��ת</param>
        /// <param name="s">����</param>
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
        /// �������������
        /// </summary>
        /// <param name="m">4x4����</param>
        /// <param name="vec">����</param>
        /// <returns>�µ�����</returns>
        static public Vector3 MultiVector(Matrix4x4 m, Vector3 vec)
        {
            Vector3 o = default(Vector3);

            o.X = m.M11 * vec.X + m.M12 * vec.Y + m.M13 * vec.Z;
            o.Y = m.M21 * vec.X + m.M22 * vec.Y + m.M23 * vec.Z;
            o.Z = m.M31 * vec.X + m.M32 * vec.Y + m.M33 * vec.Z;

            return o;
        }


        /// <summary>
        /// ���������ˣ�ֻ�Ǽ���ƫ�ƣ�δ�Ծ������һ�в���������������Ӱ����д���
        /// </summary>
        /// <param name="m">4x4����</param>
        /// <param name="point">��</param>
        /// <returns>�µ�����</returns>
        static public Vector3 MultiPoint3x4(Matrix4x4 m, Vector3 point)
        {
            Vector3 o = default(Vector3);

            // ������ƫ�Ƶģ���Ҫ���Ͼ���� tx��ty��tz ƫ����
            o.X = m.M11 * point.X + m.M12 * point.Y + m.M13 * point.Z + m.M14;
            o.Y = m.M21 * point.X + m.M22 * point.Y + m.M23 * point.Z + m.M24;
            o.Z = m.M31 * point.X + m.M32 * point.Y + m.M33 * point.Z + m.M34;

            return o;
        }

        /// <summary>
        /// ���������ˣ�ͨ�ã��Ѵ����������ش�����Ӱ�죩
        /// </summary>
        /// <param name="m">4x4����</param>
        /// <param name="point">��</param>
        /// <returns>�µ�����</returns>
        static public Vector3 MultiPoint(Matrix4x4 m, Vector3 point)
        {
            Vector3 o = default(Vector3);

            // ������ƫ�Ƶģ���Ҫ���Ͼ���� tx��ty��tz ƫ����
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