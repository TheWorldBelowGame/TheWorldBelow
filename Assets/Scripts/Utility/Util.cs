using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Util
{
	public static int ToInt(this Player.AnimState state)
	{
		switch (state) {
			case Player.AnimState.Idle:
				return 0;
			case Player.AnimState.Running:
				return 1;
			case Player.AnimState.Jumping:
				return 2;
			case Player.AnimState.Attack:
				return 3;
			case Player.AnimState.Death:
				return 4;
			case Player.AnimState.Falling:
				return 5;
			default:
				throw new System.Exception("Invalid AnimState used!");
		}
	}

	public static int ToInt(this Dialogue.AnimState state)
	{
		switch (state) {
			case Dialogue.AnimState.Idle:
				return 0;
			case Dialogue.AnimState.Talking:
				return 1;
			default:
				throw new System.Exception("Invalid AnimState used!");
		}
	}
}
