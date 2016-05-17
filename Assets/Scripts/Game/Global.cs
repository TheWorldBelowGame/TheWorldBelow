using UnityEngine;
using System.Collections;

// Class to keep track of Global variables. Utilizes lazy initialization.
public class Global
{
	// Singleton
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

	// Public
	public int killed;
	public int collected;

	// Private
	private Global()
	{
        killed = 0;
        collected = 0;
    }
    
}
