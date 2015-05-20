using System;
public class LooseBallVisitor : IBallVisitor
{
	private BallGrid mGrid = null;

	public LooseBallVisitor(BallGrid grid)
	{
		mGrid = grid;
	}

	public void visit(BallRow row, int index)
	{
		Ball b = row.GetBall (index);
		int mSequenceID = mGrid.GetSequenceID ();
		if ((b != null)&&(b.GetSequenceID() < mSequenceID)) {
			b.SetSequenceID(mSequenceID);
			mGrid.VisitNeighbors(this, row, index);
		}
	}
}

