using UnityEngine;
using System.Collections;

public interface IBallVisitor
{
	void visit(BallRow row, int index);
}
