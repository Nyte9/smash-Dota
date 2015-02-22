using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))] //This means that when you add the script to an object it will add a character controller
public class PlayerController : MonoBehaviour {

	//Customisable Settings
	public float speed;
	public float acceleration;
	public float attackSpeed;
	public float attackDamage;

	//PrivteVariables
	private Vector3 velocity;

	//Settings from NetworkManager
	public string hero = "Axe";

	//Resorces
	private Animator anim;
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		hero = this.gameObject.name.ToString(); //Sets the name of the selected hero
		anim = this.GetComponent<Animator> (); //This finds the attached animator
		controller = GetComponent<CharacterController> (); //This finds the attached CharaterController
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
	}

	void Movement (){ 
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")); //Sets the input to a vector3
		if(input.x != 0){ //this stop the character facing forward when no input is given	
			transform.rotation = Quaternion.LookRotation(new Vector3(input.x,0,0)); //this rotates the character to the horizontal input
		}
		velocity = Vector3.MoveTowards (velocity,input*speed,acceleration*Time.deltaTime); //this accelerates the velocity towards the input
		Vector3 motion = velocity; 
		motion += Vector3.up * Physics.gravity.y; //adds gravity

		controller.Move (motion * Time.deltaTime); //tells the controller to move
		anim.SetFloat ("Speed", Mathf.Sqrt(motion.x*motion.x)); //this updates the animator so locomotion is given a value ie, idle to run


	}
}