using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace ManualImageObjectSelector
{
    public class OrientedRect
    {
        public OrientedRect(PointF center, PointF direction, float w, float h)
        {
            this.center = center;
            float len2 = direction.X * direction.X + direction.Y * direction.Y;
            float ilen = (float)(1.0 / Math.Sqrt(len2));
            this.dir = new PointF(direction.X * ilen, direction.Y * ilen);
            this.w = w;
            this.h = h;
        }
        public OrientedRect(PointF direction, PointF p0, PointF p1)
        {
            float len2 = direction.X * direction.X + direction.Y * direction.Y;
            float ilen = (float)(1.0 / Math.Sqrt(len2));
            this.dir = new PointF(direction.X * ilen, direction.Y * ilen);

            this.center = new PointF(0.5f*(p0.X + p1.X), 0.5f * (p0.Y + p1.Y));
            this.w = Math.Abs(this.dir.Y * (p1.X - p0.X) - this.dir.X * (p1.Y - p0.Y)) * 0.5f;
            this.h = Math.Abs(this.dir.X * (p1.X - p0.X) + this.dir.Y * (p1.Y - p0.Y)) * 0.5f;
        }
        public PointF dir, center;
        public float w, h;

        public PointF getNormal()
        {
            return new PointF(dir.Y, -dir.X);
        }
        public PointF getPoint(float x, float y)
        {
            //r = c + x * normal + y * dir
            //rx = cx + x * nx + y * dx
            //ry = cy + x * ny + y * dy
            //rx = cx + x * dy + y * dx
            //ry = cy - x * dx + y * dy
            return new PointF(center.X + w * x * dir.Y + h * y * dir.X, center.Y - w * x * dir.X + h * y * dir.Y);
        }
        public PointF[] getAbsPoints(float xCoef, float yCoef, float xOff, float yOff)
        {
            PointF[] res = new PointF[4];

            PointF tmp = this.getPoint(-1.0f, -1.0f);
            res[0] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            tmp = this.getPoint(1.0f, -1.0f);
            res[1] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            tmp = this.getPoint(1.0f, 1.0f);
            res[2] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            tmp = this.getPoint(-1.0f, 1.0f);
            res[3] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            return res;
        }

        public void draw(PaintEventArgs e, Pen pen, float xCoef, float yCoef, float xOff, float yOff)
        {
            PointF[] pts = this.getAbsPoints(xCoef, yCoef, xOff, yOff);

            for (int i = 0; i < pts.Length; i++)
            {
                int j = i + 1;
                if (j >= pts.Length)
                    j = 0;
                e.Graphics.DrawLine(pen, pts[i], pts[j]);
            }
        }
    }
}
