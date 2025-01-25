using System;
using System.Numerics;

namespace cngraphi.gmth
{
    /// <summary>
    /// ��׵�幤�߰�
    /// <para>���ߣ�ǿ��</para>
    /// </summary>
    public class FrustumUtils
    {
        /// <summary>
        /// ��ȡ�������׵Զ����� 4 ���˵�
        /// </summary>
        /// <param name="upDir">�Ϸ���</param>
        /// <param name="rightDir">�ҷ���</param>
        /// <param name="position">λ��</param>
        /// <param name="forward">����</param>
        /// <param name="far">Զ�ü���</param>
        /// <param name="fov">��Ұ���Ƕȣ�</param>
        /// <param name="aspect">��߱�</param>
        /// <returns>�˵����顣˳�������ǣ����¡����¡����ϡ�����</returns>
        static public Vector3[] GetCameraFarPlane4Points(Vector3 upDir, Vector3 rightDir, Vector3 position, Vector3 forward, float far, float fov, float aspect)
        {
            float upDistance = far * MathF.Tan((float)(MthUtils.Deg2Rad * fov * 0.5f));
            float rightDistance = upDistance * aspect;

            Vector3 up = upDir * upDistance;
            Vector3 right = rightDir * rightDistance;
            Vector3 farPlaneCenterPoint = position + forward * far;

            return new Vector3[4]
            {
                farPlaneCenterPoint - up - right, // ����
                farPlaneCenterPoint - up + right, // ����
                farPlaneCenterPoint + up - right, // ����
                farPlaneCenterPoint + up + right  // ����
            };
        }


        /// <summary>
        /// ��ȡ�������׵������� 4 ���˵�
        /// </summary>
        /// <param name="upDir">�Ϸ���</param>
        /// <param name="rightDir">�ҷ���</param>
        /// <param name="position">λ��</param>
        /// <param name="forward">����</param>
        /// <param name="far">���ü���</param>
        /// <param name="fov">��Ұ���Ƕȣ�</param>
        /// <param name="aspect">��߱�</param>
        /// <returns>�˵����顣˳�������ǣ����¡����¡����ϡ�����</returns>
        static public Vector3[] GetCameraNearPlane4Points(Vector3 upDir, Vector3 rightDir, Vector3 position, Vector3 forward, float near, float fov, float aspect)
        {
            float upDistance = near * MathF.Tan((float)(MthUtils.Deg2Rad * fov * 0.5f));
            float rightDistance = upDistance * aspect;

            Vector3 up = upDir * upDistance;
            Vector3 right = rightDir * rightDistance;
            Vector3 nearPlaneCenterPoint = position + forward * near;

            return new Vector3[4]
            {
                nearPlaneCenterPoint - up - right, // ����
                nearPlaneCenterPoint - up + right, // ����
                nearPlaneCenterPoint + up - right, // ����
                nearPlaneCenterPoint + up + right  // ����
            };
        }



        /// <summary>
        /// ��ȡ�������׵��� 6 ��ƽ��
        /// </summary>
        /// <param name="upDir">�Ϸ���</param>
        /// <param name="rightDir">�ҷ���</param>
        /// <param name="position">�����λ��</param>
        /// <param name="forward">���������</param>
        /// <param name="nearClipPlane">���ü���</param>
        /// <param name="farClipPlane">Զ�ü���</param>
        /// <param name="fov">��Ұ���Ƕȣ�</param>
        /// <param name="aspect">��߱�</param>
        /// <returns>ƽ�����顣˳�������ǣ����ҡ��¡��ϡ����á�Զ��</returns>
        static public Vector4[] GetCameraFrustumPlanes(Vector3 upDir, Vector3 rightDir, Vector3 position, Vector3 forward, float near, float far, float fov, float aspect)
        {
            Vector3 camPos = position;
            Vector3[] points = GetCameraFarPlane4Points(upDir, rightDir, position, forward, far, fov, aspect);
            Vector3 camForward = forward;


            return new Vector4[6]
            {
        #region ˳��������λ�ã�unity �ڲ�������˳ʱ��Ϊ����
                PlaneUtils.BuildPlane(camPos, points[0], points[2]), // ��
                PlaneUtils.BuildPlane(camPos, points[3], points[1]), // ��
                PlaneUtils.BuildPlane(camPos, points[1], points[0]), // ��
                PlaneUtils.BuildPlane(camPos, points[2], points[3]), // ��
        #endregion
                PlaneUtils.BuildPlane(-camForward, camPos + camForward * near), // ������
                PlaneUtils.BuildPlane(camForward, camPos + camForward * far)  // Զ����
            };
        }



        /// <summary>
        /// ��Χ���Ƿ�����׵��
        /// </summary>
        /// <param name="planes">��׵��6��</param>
        /// <param name="box">��Χ�еĶ���</param>
        /// <returns>true���ڣ�false������</returns>
        static public bool BoxInFrustum(Vector4[] planes, Vector3[] box)
        {
            // ��׵������� 6�� �ĺ����
            if (planes.Length != 6) { return false; }

            foreach (var plane in planes)
            {
                for (int i = 0; i < box.Length; i++)
                {
                    if (!(PlaneUtils.PointAzimuthWithPlane(box[i], plane) > 0))
                    {// ��ǰ��׵�ĺ���������Χ�е�ĳһ���㡣
                        break;
                    }
                    if (i == box.Length - 1)
                    {// ��Χ�����ж��㶼�ڵ�ǰ��׵����棨ͬһ��ƽ�棩֮�⣬�޳�
                        return false;
                    }
                }
            }
            return true;
        }
    }
}