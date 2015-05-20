using System;
using System.Collections.Generic;
public class MatchingBallVisitor : IBallVisitor
{
	private List<Ball> 		matches;
	private Ball   		ball;
	private BallGrid 	grid;

	public MatchingBallVisitor(List<Ball> matches, Ball ball, BallGrid gd)
	{
		this.matches = matches;
		this.ball = ball;
		this.grid = gd;
	}

	public void visit(BallRow row, int index)
	{
		Ball b = row.GetBall (index);
		if (b != null) {
			if (b.GetBallTypeCode() == ball.GetBallTypeCode()) {
				int mSequenceID = grid.GetSequenceID();
				if (b.GetSequenceID() < mSequenceID) {
					b.SetSequenceID(mSequenceID);
					matches.Add(b);
					grid.VisitNeighbors(this, row, index);
				}
			}
		}
	}
}

