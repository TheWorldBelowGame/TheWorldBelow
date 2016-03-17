using UnityEngine;
using System.Collections;

public class Global {
    public float killed;
    public float collected;
    private static Global _s = null;
    public static Global S
    {
        get
        {
            if (_s == null)
                _s = new Global();
            return _s;
        }
    }

    private Global() {
        killed = 0;
        collected = 0;
    }
    
}
