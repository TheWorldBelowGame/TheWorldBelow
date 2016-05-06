using UnityEngine;
using System.Collections;

public class Input_Managment : MonoBehaviour {

    /// <summary>
    /// TRIGGERS AND D-PAD ARE DIFFERENT FOR WINDOWS AND MAC
    /// WILL NEED FUNCTIONS FOR THOSE IF WE USE THEM
    /// </summary>

    static bool initialized = false;
    
    public static string i_Start;
    public static string i_Move;
    public static string i_Jump;
    public static string i_Run;
    public static string i_Attack;
    public static string i_Action;
    public static string i_Speak;

	public static void init() {

        if (initialized) {
            return;
        }

        initialized = true;

        RuntimePlatform platform = Application.platform;

        switch (platform) {

            case RuntimePlatform.WindowsEditor:
                i_Start = "Start_W";
                i_Move = "L_XAxis_W";
                i_Jump = "A_W";
                i_Run = "RB_W";
                i_Attack = "X_W";
                i_Action = "B_W";
                i_Speak = "Y_W";
                break;

            case RuntimePlatform.WindowsPlayer:
                i_Start = "Start_W";
                i_Move = "L_XAxis_W";
                i_Jump = "A_W";
                i_Run = "RB_W";
                i_Attack = "X_W";
                i_Action = "B_W";
                i_Speak = "Y_W";
                break;

            case RuntimePlatform.WindowsWebPlayer:
                i_Start = "Start_W";
                i_Move = "L_XAxis_W";
                i_Jump = "A_W";
                i_Run = "RB_W";
                i_Attack = "X_W";
                i_Action = "B_W";
                i_Speak = "Y_W";
                break;

            case RuntimePlatform.OSXEditor:
                i_Start = "Start_M";
                i_Move = "L_XAxis_M";
                i_Jump = "A_M";
                i_Run = "RB_M";
                i_Attack = "X_M";
                i_Action = "B_M";
                i_Speak = "Y_M";
                break;

            case RuntimePlatform.OSXPlayer:
                i_Start = "Start_M";
                i_Move = "L_XAxis_M";
                i_Jump = "A_M";
                i_Run = "RB_M";
                i_Attack = "X_M";
                i_Action = "B_M";
                i_Speak = "Y_M";
                break;

            case RuntimePlatform.OSXWebPlayer:
                i_Start = "Start_M";
                i_Move = "L_XAxis_M";
                i_Jump = "A_M";
                i_Run = "RB_M";
                i_Attack = "X_M";
                i_Action = "B_M";
                i_Speak = "Y_M";
                break;

            default:
                i_Start = "Start_M";
                i_Move = "L_XAxis_M";
                i_Jump = "A_M";
                i_Run = "RB_M";
                i_Attack = "X_M";
                i_Action = "B_M";
                i_Speak = "Y_M";
                break;
        }
    }
}
