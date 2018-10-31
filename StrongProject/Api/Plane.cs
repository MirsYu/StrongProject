using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongProject
{
    public class Plane
    {
        //设平面方程为：Ax+By+Cz+D=0;
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        /// <summary>
        /// 根据给定的点拟合出此平面
        /// </summary>
        /// <param name="points"></param>
        public Plane(float[,] points)
        {
            double Xi = 0, Yi = 0, Zi = 0, Xi2 = 0, Yi2 = 0, XiYi = 0, XiZi = 0, YiZi = 0, n = points.GetLength(0);
            for (int i = 0; i < points.GetLength(0); i++)
            {
                Xi += points[i, 0];
                Yi += points[i, 1];
                Zi += points[i, 2];
                Xi2 += (points[i, 0] * points[i, 0]);
                Yi2 += (points[i, 1] * points[i, 1]);
                XiYi += (points[i, 0] * points[i, 1]);
                XiZi += (points[i, 0] * points[i, 2]);
                YiZi += (points[i, 1] * points[i, 2]);
            }
            double sign1, sign2;//sign1=0时平面平行于z轴，sign2=0时平面平行于y轴
            sign1 = Xi2 * (Yi2 * n - Yi * Yi) + XiYi * (Yi * Xi - n * XiYi) + Xi * (XiYi * Yi - Xi * Yi2);
            sign2 = n * Xi2 - Xi * Xi;
            if (sign1 != 0)//此算法适用于平面不平行与z轴的情况
            {
                double a1, a2, a3, b1, b2, b3, c1, c2, c3;
                sign1 = 1 / sign1;
                a1 = (Yi2 * n - Yi * Yi) * sign1;
                a2 = (Yi * Xi - n * XiYi) * sign1;
                a3 = (XiYi * Yi - Xi * Yi2) * sign1;
                b1 = (Yi * Xi - XiYi * n) * sign1;
                b2 = (Xi2 * n - Xi * Xi) * sign1;
                b3 = (XiYi * Xi - Xi2 * Yi) * sign1;
                c1 = (XiYi * Yi - Yi2 * Xi) * sign1;
                c2 = (Xi * XiYi - Xi2 * Yi) * sign1;
                c3 = (Xi2 * Yi2 - XiYi * XiYi) * sign1;
                A = a1 * XiZi + b1 * YiZi + c1 * Zi;
                B = a2 * XiZi + b2 * YiZi + c2 * Zi;
                D = a3 * XiZi + b3 * YiZi + c3 * Zi;
                C = -1;
            }
            if (sign1 == 0 && sign2 != 0)//此算法适用于平面平行于z轴但不平行于y轴的情况
            {
                A = (n * XiYi - Xi * Yi) / sign2;
                D = (Yi - A * Xi) / n;
                B = -1;
                C = 0;
            }
            if (sign1 == 0 && sign2 == 0)//此算法适用于平面平行于z轴且平行于y轴的情况
            {
                D = -(Xi / n);
                A = 1;
                B = 0;
                C = 0;
            }
            //保留三位小数
            A = Math.Round(A, 3);
            B = Math.Round(B, 3);
            C = Math.Round(C, 3);
            D = Math.Round(D, 3);
        }
        /// <summary>
        /// 计算点（x，y，z）到此平面的位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public double DistancePointPlane(float x, float y, float z)
        {
            return Math.Round(Math.Abs(A * x + B * y + C * z + D) / Math.Sqrt(A * A + B * B + C * C), 3);
        }
        /// <summary>
        /// 计算平面p和此平面的夹角
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public double AngleTwoPlane(Plane p)
        {
            double cosAngle = (A * p.A + B * p.B + C * p.C) / (Math.Sqrt(A * A + B * B + C * C) * Math.Sqrt(p.A * p.A + p.B * p.B + p.C * p.C));
            return Math.Round(Math.Acos(cosAngle), 3);
        }
        /// <summary>
        /// 计算平面（pAx+pBy+pCz+pD=0）和此平面的夹角
        /// </summary>
        /// <param name="pA"></param>
        /// <param name="pB"></param>
        /// <param name="pC"></param>
        /// <param name="pD"></param>
        /// <returns></returns>
        public double AngleTwoPlane(double pA, double pB, double pC, double pD)
        {
            double cosAngle = (A * pA + B * pB + C * pC) / (Math.Sqrt(A * A + B * B + C * C) * Math.Sqrt(pA * pA + pB * pB + pC * pC));
            return Math.Round(Math.Acos(cosAngle), 3);
        }
    }
}
