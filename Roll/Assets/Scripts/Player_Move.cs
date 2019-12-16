using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{

	public float playerSpeedJoypad;
	// speed for rotation with joypad
	public float playerSpeedMouse;
	public Rigidbody playerRigid;
	// speed of rotation with mouse
	// rigid body of the player
	public float moveHorizontal;
	// keep the value for horizonatl axis joypad
	public float moveVertical;
	// keep the value for vertical axisjoypad
	public float moveHorizontalMouse;
	// keep the value for horizonatl axis mouse
	public float moveVerticalMouse;
    // keep the value for vertical axis mouse
	public  Vector3 forceUP;
	// force applied to player
	public Mine_Cart_Motion mot;
	// getting variables from another script

	void Start ()
	{
		playerRigid = GetComponent<Rigidbody> (); // get rigidbody 
		mot = GameObject.Find ("mineCart").GetComponent<Mine_Cart_Motion> (); // getting the Collision script attached to player 
	}



	void FixedUpdate ()
	{
		moveHorizontal = Input.GetAxis ("Horizontal"); // geeeting the mouse x axis 
		moveVertical = Input.GetAxis ("Vertical"); // geeeting the mouse y axis 
		moveHorizontalMouse = Input.GetAxis ("Mouse X"); // geeeting the mouse x axis 
		moveVerticalMouse = Input.GetAxis ("Mouse Y"); // geeeting the mouse y axis 
		Vector3 movementJoypad = new Vector3 (-moveHorizontal, 0.0f, -moveVertical); // store in movement a new vector with the mouse values 
		Vector3 movementMouse = new Vector3 (moveHorizontalMouse, 0.0f, moveVerticalMouse); // store in movement a new vector with the mouse v
		forceUP = new Vector3 (0f, 0.50f, 0.50f); // setting froce for player 
		playerRigid.AddForce (movementJoypad * playerSpeedJoypad); // add force to the player rigidbody + the speed we want 
		playerRigid.AddForce (movementMouse * playerSpeedMouse); // add force to the player rigidbody + the speed we want 
		if (mot.cart_arrived) { // if mine cart has arrived 
			Debug.Log ("player lunched");
			playerRigid.AddForce (forceUP, ForceMode.Impulse); // add force to the player rigidbody + the speed we want 
			mot.cart_arrived = false; // mine_cart not longer active 
		}
	}


}

