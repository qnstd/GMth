using System;
using System.Numerics;

namespace cngraphi.gmth
{
    /// <summary>
    /// 视椎体工具包
    /// <para>作者：强辰</para>
    /// </summary>
    public class FrustumUtils
    {
        /// <summary>
        /// 获取摄像机视椎远裁面的 4 个端点
        /// </summary>
        /// <param name="upDir">上方向</param>
        /// <param name="rightDir">右方向</param>
        /// <param name="position">位置</param>
        /// <param name="forward">朝向</param>
        /// <param name="far">远裁剪面</param>
        /// <param name="fov">视野（角度）</param>
        /// <param name="aspect">宽高比</param>
        /// <returns>端点数组。顺序依次是：左下、右下、左上、右上</returns>
        static public Vector3[] GetCameraFarPlane4Points(Vector3 upDir, Vector3 rightDir, Vector3 position, Vector3 forward, float far, float fov, float aspect)
        {
            float upDistance = far * MathF.Tan((float)(MthUtils.Deg2Rad * fov * 0.5f));
            float rightDistance = upDistance * aspect;

            Vector3 up = upDir * upDistance;
            Vector3 right = rightDir * rightDistance;
            Vector3 farPlaneCenterPoint = position + forward * far;

            return new Vector3[4]
            {
                farPlaneCenterPoint - up - right, // 左下
                farPlaneCenterPoint - up + right, // 右下
                farPlaneCenterPoint + up - right, // 左上
                farPlaneCenterPoint + up + right  // 右上
            };
        }


        /// <summary>
        /// 获取摄像机视椎近裁面的 4 个端点
        /// </summary>
        /// <param name="upDir">上方向</param>
        /// <param name="rightDir">右方向</param>
        /// <param name="position">位置</param>
        /// <param name="forward">朝向</param>
        /// <param name="far">近裁剪面</param>
        /// <param name="fov">视野（角度）</param>
        /// <param name="aspect">宽高比</param>
        /// <returns>端点数组。顺序依次是：左下、右下、左上、右上</returns>
        static public Vector3[] GetCameraNearPlane4Points(Vector3 upDir, Vector3 rightDir, Vector3 position, Vector3 forward, float near, float fov, float aspect)
        {
            float upDistance = near * MathF.Tan((float)(MthUtils.Deg2Rad * fov * 0.5f));
            float rightDistance = upDistance * aspect;

            Vector3 up = upDir * upDistance;
            Vector3 right = rightDir * rightDistance;
            Vector3 nearPlaneCenterPoint = position + forward * near;

            return new Vector3[4]
            {
                nearPlaneCenterPoint - up - right, // 左下
                nearPlaneCenterPoint - up + right, // 右下
                nearPlaneCenterPoint + up - right, // 左上
                nearPlaneCenterPoint + up + right  // 右上
            };
        }



        /// <summary>
        /// 获取摄像机视椎体的 6 个平面
        /// </summary>
        /// <param name="upDir">上方向</param>
        /// <param name="rightDir">右方向</param>
        /// <param name="position">摄像机位置</param>
        /// <param name="forward">摄像机朝向</param>
        /// <param name="nearClipPlane">近裁剪面</param>
        /// <param name="farClipPlane">远裁剪面</param>
        /// <param name="fov">视野（角度）</param>
        /// <param name="aspect">宽高比</param>
        /// <returns>平面数组。顺序依次是：左、右、下、上、近裁、远裁</returns>
        static public Vector4[] GetCameraFrustumPlanes(Vector3 upDir, Vector3 rightDir, Vector3 position, Vector3 forward, float near, float far, float fov, float aspect)
        {
            Vector3 camPos = position;
            Vector3[] points = GetCameraFarPlane4Points(upDir, rightDir, position, forward, far, fov, aspect);
            Vector3 camForward = forward;


            return new Vector4[6]
            {
        #region 顺序针填入位置，unity 内部定义以顺时针为正面
                PlaneUtils.BuildPlane(camPos, points[0], points[2]), // 左
                PlaneUtils.BuildPlane(camPos, points[3], points[1]), // 右
                PlaneUtils.BuildPlane(camPos, points[1], points[0]), // 下
                PlaneUtils.BuildPlane(camPos, points[2], points[3]), // 上
        #endregion
                PlaneUtils.BuildPlane(-camForward, camPos + camForward * near), // 近裁面
                PlaneUtils.BuildPlane(camForward, camPos + camForward * far)  // 远裁面
            };
        }



        /// <summary>
        /// 包围盒是否在视椎内
        /// </summary>
        /// <param name="planes">视椎体6面</param>
        /// <param name="box">包围盒的顶点</param>
        /// <returns>true：在；false：不在</returns>
        static public bool BoxInFrustum(Vector4[] planes, Vector3[] box)
        {
            // 视椎体必须是 6面 的横截面
            if (planes.Length != 6) { return false; }

            foreach (var plane in planes)
            {
                for (int i = 0; i < box.Length; i++)
                {
                    if (!(PlaneUtils.PointAzimuthWithPlane(box[i], plane) > 0))
                    {// 当前视椎的横截面包含包围盒的某一顶点。
                        break;
                    }
                    if (i == box.Length - 1)
                    {// 包围盒所有顶点都在当前视椎检测面（同一个平面）之外，剔除
                        return false;
                    }
                }
            }
            return true;
        }
    }
}