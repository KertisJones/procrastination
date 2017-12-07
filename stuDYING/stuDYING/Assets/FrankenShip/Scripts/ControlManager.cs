 using UnityEngine;
using System.Collections;
using System.Linq;

public class ControlManager : MonoBehaviour {

    //PLAYER ONE
    /// <summary>
    /// Player one, left stick horizontal axis.
    /// </summary>
    public static float P1LHriz; //left joystick (l/r)
    /// <summary>
    /// Player one, left stick vertical axis.
    /// </summary>
    public static float P1LVert; //left joystick (u/d)
    /// <summary>
    /// Player one, right stick horizontal axis.
    /// </summary>
    public static float P1RHriz; //right joystick (l/r)
    /// <summary>
    /// Player one, right stick vertical axis.
    /// </summary>
    public static float P1RVert; //right joystick (u/d)
    /// <summary>
    /// Player one, A button (Xbox) or Cross button (PS4)
    /// </summary>
    public static bool P1A; //A or CROSS
    /// <summary>
    /// Player one, B button (Xbox) or Circle button (PS4)
    /// </summary>
    public static bool P1B; //B OR CIRCLE
    /// <summary>
    /// Player one, X button (Xbox) or Square button (PS4)
    /// </summary>
    public static bool P1X; //X OR SQUARE
    /// <summary>
    /// Player one, Y button (Xbox) or Triangle button (PS4)
    /// </summary>
    public static bool P1Y; //Y OR TRIANGLE
    /// <summary>
    /// Player one, left bumper
    /// </summary>
    public static bool P1LBump; //left bumper
    /// <summary>
    /// Player one, right bumper
    /// </summary>
    public static bool P1RBump; //right bumper
    /// <summary>
    /// Player one, left trigger
    /// </summary>
    public static bool P1LTrig; //left trigger
    /// <summary>
    /// Player one, right trigger
    /// </summary>
    public static bool P1RTrig; //right trigger
    /// <summary>
    /// Player one, menu/start button (Xbox) or Options button (PS4)
    /// </summary>
	public static bool P1RTrigDown; //right trigger
	/// <summary>
	/// Player one, menu/start button (Xbox) or Options button (PS4)
	/// </summary>
	public static bool P1RTrigUp; //right trigger
	/// <summary>
	/// Player one, menu/start button (Xbox) or Options button (PS4)
	/// </summary>
    public static bool P1Start; //Menu or Options button
	/// <summary>
	/// Player one, right stick click (Xbox) or Options button (PS4)
	/// </summary>
	public static bool P1RClick; //Activate Skillshot button

    //PLAYER TWO
    /// <summary>
    /// Player two, left stick horizontal axis
    /// </summary>
    public static float P2LHriz; //left joystick (l/r)
    /// <summary>
    /// Player two, left stick vertical axis
    /// </summary>
    public static float P2LVert; //left joystick (u/d)
    /// <summary>
    /// Player two, right stick horizontal axis
    /// </summary>
    public static float P2RHriz; //right joystick (l/r)
    /// <summary>
    /// Player two, right stick vertical axis
    /// </summary>
    public static float P2RVert; //right joystick (u/d)
    /// <summary>
    /// Player two, A button (Xbox) or Cross button (PS4)
    /// </summary>
    public static bool P2A; //A or CROSS
    /// <summary>
    /// Player two, B button (Xbox) or Circle button (PS4)
    /// </summary>
    public static bool P2B; //B OR CIRCLE
    /// <summary>
    /// Player two, X button (Xbox) or Square button (PS4)
    /// </summary>
    public static bool P2X; //X OR SQUARE
    /// <summary>
    /// Player two, Y button (Xbox) or Triangle button (PS4)
    /// </summary>
    public static bool P2Y; //Y OR TRIANGLE
    /// <summary>
    /// Player two, left bumper
    /// </summary>
    public static bool P2LBump; //left bumper
    /// <summary>
    /// Player two, right bumper
    /// </summary>
    public static bool P2RBump; //right bumper
    /// <summary>
    /// Player two, left trigger
    /// </summary>
    public static bool P2LTrig; //left trigger
    /// <summary>
    /// Player two, right trigger
    /// </summary>
    public static bool P2RTrig; //right trigger
    /// <summary>
    /// Player two, menu/start button (Xbox) or Options button (PS4)
    /// </summary>
    public static bool P2Start; //Menu or Options button
	/// <summary>
	/// Player two, right stick click (Xbox)
	/// </summary>
	public static bool P2RClick; //Activate Skillshot button

    //CONTROL BOOLEANS
	public static bool P1Xbox = false;
	public static bool P2Xbox = false;
	public static bool P1Key = false;
	public static bool P2Key = false;

    // Use this for initialization
    void Awake () {
        if (Input.GetJoystickNames().Any())
        {
            string[] joysticks = Input.GetJoystickNames();
            //if there is one controller, it is player one.
            if (joysticks.Length == 1)
            {
                if (joysticks[0].Equals("Wireless Controller")) //this is the Ps controller catch. Xbox controllers are the default
                {
					
                }
                else //xbox controller
                {
                    P1Xbox = true;
                }
				//player two - uses keyboard
				P2Key = true;
            }
            else //there are two joysticks. Joystick 0 will always be Player ONE.
            {
                if (joysticks[0].Equals("Wireless Controller")) //ps4
                {
					
                }
                else //xbox
                {
                    P1Xbox = true;
                }
                if (joysticks[1].Equals("Wireless Controller")) //ps4
                {
					
                }
                else //xbox
                {
                    P2Xbox = true;
                }
            }
        }
        else //there are no joysticks, use keyboard keybinds.
        {
            //player one
            P1Key = true;
            //player two
            P2Key = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (P1Xbox)
        {
            P1LHriz = Input.GetAxis("GamePadLHoP1");
            P1LVert = Input.GetAxis("GamePadLVertP1");
            P1RHriz = Input.GetAxis("GamePadRHoP1");
            P1RVert = Input.GetAxis("GamePadRVertP1");
            P1A = Input.GetKeyDown(KeyCode.Joystick1Button0);
            P1B = Input.GetKeyDown(KeyCode.Joystick1Button1);
            P1X = Input.GetKeyDown(KeyCode.Joystick1Button2);
            P1Y = Input.GetKeyDown(KeyCode.Joystick1Button3);
            P1LBump = Input.GetKeyDown(KeyCode.Joystick1Button4);
            P1RBump = Input.GetKeyDown(KeyCode.Joystick1Button5);
			if (Input.GetAxis ("LTriggerP1") != 0) {
				P1LTrig = true;
			} else {
				P1LTrig = false;
			}
			if (Input.GetAxis ("RTriggerP1") != 0) {
				P1RTrig = true;
			} else {
				P1RTrig = false;
			}
			if (Input.GetButtonDown ("RTriggerP1")) {
				P1RTrigDown = true;
				print ("SADSA");
			} else {
				P1RTrigDown = false;
			}
			if (Input.GetButtonUp ("RTriggerP1")) {
				P1RTrigUp = true;
			} else {
				P1RTrigUp = false;
			}
            P1Start = Input.GetKeyDown(KeyCode.Joystick1Button7);
			P1RClick = Input.GetKeyDown (KeyCode.Joystick1Button9);
        }
        if (P2Xbox)
        {
            P2LHriz = Input.GetAxis("GamePadLHoP2");
            P2LVert = Input.GetAxis("GamePadLVertP2");
            P2RHriz = Input.GetAxis("GamePadRHoP2");
            P2RVert = Input.GetAxis("GamePadRVertP2");
            P2A = Input.GetKeyDown(KeyCode.Joystick2Button0);
            P2B = Input.GetKeyDown(KeyCode.Joystick2Button1);
            P2X = Input.GetKeyDown(KeyCode.Joystick2Button2);
            P2Y = Input.GetKeyDown(KeyCode.Joystick2Button3);
            P2LBump = Input.GetKeyDown(KeyCode.Joystick2Button4);
            P2RBump = Input.GetKeyDown(KeyCode.Joystick2Button5);
			if (Input.GetAxis ("LTriggerP2") != 0) {
				P2LTrig = true;
			} else {
				P2LTrig = false;
			}
			if (Input.GetAxis ("RTriggerP2") != 0) {
				P2RTrig = true;
			} else {
				P2RTrig = false;
			}
            P2Start = Input.GetKeyDown(KeyCode.Joystick2Button7);
			P2RClick = Input.GetKeyDown (KeyCode.Joystick2Button9);
        }
        if (P1Key)
        {
            P1LVert = Input.GetAxis("KeyboardVertP1");
			P1LHriz = Input.GetAxis("KeyboardHoP1");
			P1RHriz = Input.GetAxis("KeyboardHoP1");
			P1A = Input.GetKeyDown (KeyCode.Space);
			P1B = Input.GetKeyDown (KeyCode.LeftShift);
			P1X = Input.GetKeyDown (KeyCode.C);
			P1Y = Input.GetKeyDown (KeyCode.Space);
            P1LBump = Input.GetKeyDown(KeyCode.Q);
			P1RBump = Input.GetKeyDown(KeyCode.E);
			P1LTrig = Input.GetKeyDown(KeyCode.F);
			P1RTrig = Input.GetKey(KeyCode.G);
			P1Start = Input.GetKeyDown(KeyCode.P);
			P1RClick = Input.GetKeyDown (KeyCode.LeftShift);

        }
        if (P2Key)
        {
            //P2LVert = Input.GetAxis("KeyboardVertP2");
			//P2LHriz = Input.GetAxis("KeyboardHoP2");
			//P2RHriz = Input.GetAxis("KeyboardHoP2");
			//P2A = Input.GetKeyDown (KeyCode.RightShift);
			//P2B = Input.GetKeyDown (KeyCode.Comma);
			//P2X = Input.GetKeyDown (KeyCode.Y);
			//P2Y = Input.GetKeyDown (KeyCode.L);
			//P2LBump = Input.GetKeyDown(KeyCode.Period);
			//P2RBump = Input.GetKeyDown(KeyCode.Slash); 
			//P2LTrig = Input.GetKeyDown(KeyCode.Period);
			//P2RTrig = Input.GetKey(KeyCode.Slash);
			//P2Start = Input.GetKeyDown(KeyCode.P);
			//P2RClick = Input.GetKeyDown (KeyCode.Slash);
        }
    }
}
