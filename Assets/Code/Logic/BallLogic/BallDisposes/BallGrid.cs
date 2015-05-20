using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class BallGrid
{
	private List<BallRow> mRows = new List<BallRow>();
	private int miSequenceID = 0;

	public void push(BallRow row)
	{
		mRows.Add (row);
		renumber ();
	}

	public void insert(BallRow row)
	{
		mRows.Insert (0, row);
		renumber ();
	}

	void renumber ()
	{
		for (int i = 0; i < mRows.Count; i++) 
		{
			if (mRows[i] != null) 
			{
				mRows[i].SetNumber(i);
				mRows[i].name = "Row_" + i;
			}
		}
	}

	public BallRow Remove(int index)
	{
		BallRow row = mRows [index];
		mRows.RemoveAt (index);
		mRows.Add (mRows[mRows.Count - 1]);
		renumber();
		return row;
	}

	public BallRow GetRow(int index)
	{
		if (index < 0 || index >= mRows.Count) {
			return null;
		}
		return mRows [index];
	}

	public void AdvanceRows(float yDelta)
	{
		for (int i = 0; i < mRows.Count; i++) {
			BallRow row = mRows[i];
			if (row != null) {
				row.Move(0,yDelta);
			}
		}
	}

	public Ball[] GetLooseBalls()
	{
		IBallVisitor visitor = new LooseBallVisitor (this);
		getNextSequenceID ();
		if (mRows[0] != null) {
			for (int i = 0; i < Configs.ROW_MAX_BALLS; i++) {
				visitor.visit(GetRow(0), i);
			}
		}

		List<Ball> loose = new List<Ball> ();
		for (int i = 0; i < mRows.Count; i++) {
			BallRow row = GetRow(i);
			if (row != null) {
				for (int j = 0; j < Configs.ROW_MAX_BALLS; j++) {
					Ball b = row.GetBall(j);
					if ((b!=null) && (b.GetSequenceID() < miSequenceID)) {
						loose.Add(b);
					}
				}
			}
		}

		return loose.ToArray ();
	}

	public int GetSequenceID ()
	{
		return miSequenceID;
	}

	public void VisitNeighbors (IBallVisitor visitor, BallRow row, int index)
	{
		if (row == null) {
			return;
		}
		if (row.IsOffset ()) {
			// get the row number in the total list
			int num1 = row.GetNumber ();
			// if there is a number
			if (num1 > 0) {
				// visit pre row
				BallRow prev1 = GetRow (num1 - 1);
				visitor.visit (prev1, index);
				if (index < Configs.ROW_MAX_BALLS - 1) {
					// visit the ball on
					visitor.visit (prev1, index + 1);
				}
			}
			if (index > 0) {
				visitor.visit (row, index - 1);
			}
			if (index < Configs.ROW_MAX_BALLS - 1) {
				visitor.visit (row, index + 1);
			}
			if (++num1 < mRows.Count) {
				BallRow next1 = GetRow (num1);
				if (next1 != null) {
					visitor.visit (next1, index);
					if (index < Configs.ROW_MAX_BALLS - 1) {
						visitor.visit (next1, index + 1);
					}
				}
			}
		} else {
			int num2 = row.GetNumber();
			if (num2 > 0) {
				BallRow prev2 = GetRow(num2 - 1);
				if (index > 0) {
					visitor.visit(prev2, index - 1);
				}
				visitor.visit(prev2, index);
			}
			if (index > 0) {
				visitor.visit(row, index - 1);
			}
			if (index < Configs.ROW_MAX_BALLS - 1) {
				visitor.visit(row, index + 1);
			}
			if (++num2 < mRows.Count) {
				BallRow next2 = GetRow(num2);
				if (next2 != null) {
					if (index > 0) {
						visitor.visit (next2, index - 1);
					}
					visitor.visit(next2, index);
				}
			}
		}
	}

	public BallRow GetNearestRow(Vector3 worldPos, Transform ballLayer)
	{
		Vector3 localPos = ballLayer.InverseTransformPoint (worldPos);
		for (int i =  0; i < mRows.Count ; i++) {
			BallRow row = mRows[i];
			if (row == null) {
				break;
			}
			if ((localPos.y > row.transform.localPosition.y - Configs.BALL_HALF_SIZE.y)&&
			    (localPos.y < row.transform.localPosition.y + Configs.BALL_HALF_SIZE.y)) {

				return row;
			}
		}
		return null;
	}

	public Ball[] GetMatchingBalls(Ball ball)
	{
		ball.SetSequenceID (getNextSequenceID ());
		List<Ball> matches = new List<Ball> ();
		matches.Add (ball);
		IBallVisitor visitor = new MatchingBallVisitor (matches, ball, this);
		VisitNeighbors (visitor, ball.GetBallRow(), ball.GetIndex());
		return matches.ToArray ();
	}

	int getNextSequenceID ()
	{
		return ++miSequenceID;
	}

	public void DestroyAllBalls ()
	{
		for (int i = 0; i < mRows.Count; i++) {
			BallRow row = mRows[i];
			if (row != null) {
				row.DestroyAllBalls();
			}
		}
	}

	public void FillRow (LevelConfig conf, int i, bool b, int iFrom, int iTo, bool bHasPowerups)
	{
		BallRow row = GetRow (i);
		if (row != null) {
			row.FillRow(GetRow(i + 1), conf, b, iFrom, iTo, bHasPowerups);
		}
	}

	public BallRow GetBottomRow ()
	{
		return GetRow (mRows.Count - 1);
	}

	public BallRow GetTopRow ()
	{
		return GetRow (0);
	}

	public IEnumerator DisposeAll ()
	{
		for (int i = 0; i < mRows.Count; i++) {
			BallRow row = mRows[i];
			if (row != null) {
				row.DisposeAll();
				GameObject.Destroy(row.gameObject);
				yield return null;
			}
		}
		mRows.Clear ();
	}
}
