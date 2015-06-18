
using System;

namespace Game.Logic.Clip
{
/*
typedef struct
{
float x; 
float y;
} Point2D; 
 cp 在此是四个元素的数组: 
cp[0] 为起点，或上图中的 P0 
cp[1] 为第一控制点，或上图中的 P1 
cp[2] 为第二控制点，或上图中的 P2 
cp[3] 为结束点，或上图中的 P3 
t 为参数值，0 <= t <= 1 
Point2D PointOnCubicBezier( Point2D* cp, float t ) 
{ 
float ax, bx, cx; float ay, by, cy; 
float tSquared, tCubed; Point2D result; 
 计算多项式系数 
cx = 3.0 * (cp[1].x - cp[0].x); 
bx = 3.0 * (cp[2].x - cp[1].x) - cx; 
ax = cp[3].x - cp[0].x - cx - bx; 
cy = 3.0 * (cp[1].y - cp[0].y); 
by = 3.0 * (cp[2].y - cp[1].y) - cy; 
ay = cp[3].y - cp[0].y - cy - by; 
计算t位置的点值 
tSquared = t * t; 
tCubed = tSquared * t; 
result.x = (ax * tCubed) + (bx * tSquared) + (cx * t) + cp[0].x; 
result.y = (ay * tCubed) + (by * tSquared) + (cy * t) + cp[0].y; 
return result; 
} 
 ComputeBezier 以控制点 cp 所产生的曲线点，填入 Point2D 结构数组。 
调用方必须分配足够的空间以供输出，<sizeof(Point2D) numberOfPoints> 
void ComputeBezier( Point2D* cp, int numberOfPoints, Point2D* curve )
{
float dt; int i; 
dt = 1.0 / ( numberOfPoints - 1 ); 
for( i = 0; i < numberOfPoints; i++) 
    curve[i] = PointOnCubicBezier( cp, i*dt ); 
}*/
	public class BallShakeClip : ActClip
	{
		protected float mfHorizontalOffset 	= 0f;
		protected float mfVerticalOffset 	= 0f;
		protected UnityEngine.Transform mtTransform = null;
		protected UnityEngine.Vector3 mvBasePos = default(UnityEngine.Vector3);

		protected const string BallShake_Key 		= "ballshake";
		protected const string Vertical_Offset_Key 	= "vo";
		protected const string Horizontal_Offset_Key= "ho";
		protected const string During_Key = "during";

		public BallShakeClip ()
		{
			mfHorizontalOffset 	= Property.Ins.GetFloat(BallShake_Key + "." + Horizontal_Offset_Key);
			mfVerticalOffset 	= Property.Ins.GetFloat(BallShake_Key + "." + Vertical_Offset_Key);
			mfDuring 			= Property.Ins.GetFloat (BallShake_Key + "." + During_Key);
		}

		public override void Process (float curTime)
		{
			float _h_offset = CalculateValue (PlayPercent) * mfHorizontalOffset;
			float _v_offset = CalculateValue (PlayPercent) * mfVerticalOffset;

			UnityEngine.Vector3 updatePos = mvBasePos;
			updatePos.x += _h_offset;
			updatePos.y += _v_offset;

			mtTransform.localPosition = updatePos;
		}

		public override void End ()
		{
			// handle the position first
			mtTransform.localPosition = mvBasePos;
			// call the baseclass end function last
			base.End ();
		}

		public override void Play (UnityEngine.Transform trans, Action cb)
		{
			mtTransform = trans;
			mvBasePos 	= mtTransform.localPosition;
			maCallBack	= cb;
			base.Play ();
		}

		private float CalculateValue(float percent)
		{
			return UnityEngine.Mathf.Sin (UnityEngine.Mathf.PI * 2f * percent);
		}
	}
}

