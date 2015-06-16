using UnityEngine;

namespace Game.Logic
{
	public class Layers
	{
		public static int Ball{	get	{return LayerMask.NameToLayer("Ball");	}}
		public static int StaticBall{	get	{return LayerMask.NameToLayer("StaticBall");	}}
		public static int Border{	get	{return LayerMask.NameToLayer("Border");	}}
		public static int FallingBall{	get	{return LayerMask.NameToLayer("FallingBall");	}}
	}
}