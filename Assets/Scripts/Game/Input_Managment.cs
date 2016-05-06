using UnityEngine;
using System.Collections;

public class Input_Managment : MonoBehaviour {

    /// <summary>
    /// TRIGGERS AND D-PAD ARE DIFFERENT FOR WINDOWS AND MAC
    /// WILL NEED FUNCTIONS FOR THOSE IF WE USE THEM
    /// </summary>


    //CLASS VARIABLES
    static bool initialized = false;
    static string platform = "";
    
    //INPUTS
    public static string i_Start;
    public static string i_Move;
    public static string i_Jump;
    public static string i_Run;
    public static string i_Attack;
    public static string i_Action;
    public static string i_Speak;

    // INIT
    // to be called when game starts to detect platform and set key bindings
	public static void init() {
        if (initialized) {
            return;
        }
        initialized = true;
        set_key_bindings();
    }

    // SET_KEY_BINDINGS
    // maps the actions to the correct keys and platform
    static void set_key_bindings() {

        platform = detect_platform();

        i_Start = "Start" + platform;
        i_Move = "L_XAxis" + platform;
        i_Jump = "A" + platform;
        i_Run = "RB" + platform;
        i_Attack = "X" + platform;
        i_Action = "B" + platform;
        i_Speak = "Y" + platform;
        
    }

    // DETECT_PLATFORM
    // detects which platform is being run
    // returns _W for windows and _M for mac
    static string detect_platform() {

        RuntimePlatform run_platform = Application.platform;

        switch (run_platform) {
            case RuntimePlatform.WindowsEditor:
                return "_W";
            case RuntimePlatform.WindowsPlayer:
                return "_W";
            case RuntimePlatform.WindowsWebPlayer:
                return "_W";
            case RuntimePlatform.OSXEditor:
                return "_M";
            case RuntimePlatform.OSXPlayer:
                return "_M";
            case RuntimePlatform.OSXWebPlayer:
                return "_M";
            default:
                return "_M";
        }
    }
}
