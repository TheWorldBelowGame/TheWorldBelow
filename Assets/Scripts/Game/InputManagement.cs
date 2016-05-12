using UnityEngine;
using System.Collections;

public class InputManagement : MonoBehaviour
{
    // TRIGGERS AND D-PAD ARE DIFFERENT FOR WINDOWS AND MAC
    // WILL NEED FUNCTIONS FOR THOSE IF WE USE THEM

    static bool initialized = false;
    static string platform = "";
    
    // Input strings
    static string i_Start;
    static string i_Move;
    static string i_Jump;
    static string i_Run;
    static string i_Attack;
	static string i_Action;
	static string i_Speak;

	public static bool Start()
	{
		return Input.GetButtonDown(i_Start);
	}

	public static float Move()
	{
		return Input.GetAxis(i_Move);
	}

	public static bool Jump()
	{
		return Input.GetButtonDown(i_Jump);
	}

	public static bool Run()
	{
		return Input.GetButton(i_Run);
	}

	public static bool Attack()
	{
		return Input.GetButtonDown(i_Attack);
	}

	public static bool Action()
	{
		return Input.GetButtonDown(i_Action);
	}

	public static bool Speak()
	{
		return Input.GetButtonDown(i_Speak);
	}

	// Initialization - to be called when game starts.
	public static void init() {
		Debug.Log("Initializing input manager");
        if (initialized) {
            return;
        }
        set_key_bindings();
		initialized = true;
	}
	
    // Maps the actions to the correct keys and platform.
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
	
    // Detects which platform is being run.
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
