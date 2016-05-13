using UnityEngine;
using System.Collections;

public class Global
{
    static Global _s = null;
    public static Global S
    {
        get
        {
            if (_s == null)
                _s = new Global();
            return _s;
        }
    }

	public int killed;
	public int collected;

	private Global()
	{
        killed = 0;
        collected = 0;
    }
    
}
