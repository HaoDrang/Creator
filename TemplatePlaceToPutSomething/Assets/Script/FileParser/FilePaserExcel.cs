using UnityEngine;

public class FilePaserExcel : FileParser
{
	public FilePaserExcel(MonoBehaviour mb):base(mb)
	{
	
	}

	protected override void ParseLine(string str)
	{
		int headPos = str.IndexOf ("\t");

		SetDic (str.Substring(0, headPos), str.Substring(headPos));
	}
}


