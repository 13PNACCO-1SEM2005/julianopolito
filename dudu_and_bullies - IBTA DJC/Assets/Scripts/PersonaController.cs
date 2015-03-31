using UnityEngine;
using System.Collections;

public class PersonaController : GameObjectBase {


	Animator animHadouken;

	public GameObject Hadouken;
	public GameObject Hit;
	public GameObject ParticleExplosion;
	public Transform GroundCheck;
	public LayerMask whatIsGround;

	public float maxSpeed = 3f;
	float jumpForce = 550f;
	float kickForwardForce = 650f;
	Vector2 startPosition;
	
	bool isGrounded = false;
	bool isRecovering = false;
	float groundRadius = 0.2f;

	//USed to simulate touch action
	public TouchControl.Direction debugAction = TouchControl.Direction.NONE;

	//Health counter
	public int Score = 0;
	public int HadoukenCounter = 3;

	private bool hitCheck = false;

	public static PersonaController instance;


	//Sounds
	public AudioClip jumpSound, punchSound, uppercutSound, roundhouseSound, superSound, pickupSound, hitSound, damageSound;

	public bool IsGrounded {
		get {
			return isGrounded;
		}
	}

	// Use this for initialization
	void Start () {
		instance = this;
		anim = GetComponent<Animator>();
		animHadouken = Hadouken.GetComponent<Animator>();
		Hadouken.SetActive(false);
		startPosition = transform.position;
		mat = GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius,whatIsGround);
		anim.SetBool("Ground", isGrounded);

		float move = Input.GetAxis("Horizontal");
		move = 1;
		anim.SetFloat("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2(move*maxSpeed, rigidbody2D.velocity.y);

		if(move > 0 && !isFacingRight){
			Flip();
		}else if(move < 0 && isFacingRight){
			Flip();
		}
	}

	void Update(){
		//Recebe o input youch ou swipe e reage de acordo
		HandleAction();
	}

	void HandleAction(){
		string state = GetCurrentAnimState();

		//Return se nao estiver correndo - state default
		if(state != "Base.Run")return;

		TouchControl.Direction input = InputControl.instance.GetCurrent();

		//Debug.Log("Input: "+input);

		if(debugAction != TouchControl.Direction.NONE){
			input = debugAction;
		}
		//Debug.Log(jumpSound);
		switch(input){
		case TouchControl.Direction.TOUCH:
			if(isGrounded){
				anim.SetBool("Ground", false);
				anim.SetTrigger("Jump");
				rigidbody2D.AddForce(new Vector2(0,jumpForce));
				isGrounded = false;
				PlaySound(jumpSound);
			}
			break;
		case TouchControl.Direction.SWIPE_UP:
			if(isGrounded){
				anim.SetTrigger("Uppercut");
				//rigidbody2D.AddForce(new Vector2(0,jumpForce/3));
				PlaySound(uppercutSound);
			}
			break;
		case TouchControl.Direction.SWIPE_DOWN:
			if(HadoukenCounter > 0){
				anim.SetTrigger("Super");
				Invoke ("ThrowHadouken",0.3f);
			}
			break;
		case TouchControl.Direction.SWIPE_LEFT:
			if(isGrounded){
				anim.SetTrigger("Roundhouse");
				//rigidbody2D.AddForce(new Vector2(kickForwardForce,jumpForce/2));
			}
			break;
		case TouchControl.Direction.SWIPE_RIGHT:
			if(isGrounded){
				anim.SetTrigger("Punch");
				//rigidbody2D.AddForce(new Vector2(0,0));
			}
			break;
		}
		debugAction = TouchControl.Direction.NONE;
	}

	void OnCollisionStay2D(Collision2D other){

		if(other.gameObject.tag == "Enemy" ){
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Run")
			   && !anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Jump")
			   && !anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Super")
			   && !hitCheck){
				hitCheck = true;
				Invoke("DisableHitCheck",0.2f);
				Debug.Log ("Hit enemy:" + other.gameObject.tag);
				Score += 15;
				other.gameObject.SendMessage("Damage");
				other.gameObject.rigidbody2D.AddForce(new Vector2(340,0));
				GameObject hit = Instantiate(Hit,other.contacts[0].point,Quaternion.identity) as GameObject;
				GameObject ps = Instantiate(ParticleExplosion,other.contacts[0].point,Quaternion.identity) as GameObject;
				ps.SetActive(true);
				//hit.SetActive(true);
				Destroy(ps,0.6f);
				Destroy(hit,0.6f);
				PlaySound(hitSound);
				if(!isRecovering){
					//Debug.Log ("Hit by:" + other.gameObject.tag);
					Animator a = other.gameObject.GetComponent<Animator>();
					//a.SetTrigger("Hit");
					//Damage();
					if(HP == 0){
						GameOver();
					}
				}
			}
		}
	}

	void DisableHitCheck(){
		hitCheck = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(!isRecovering){
			if(other.gameObject.tag == "Enemy" ){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Run")
				   && !anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Jump")){
					Debug.Log ("Kicking ass:" + other.gameObject.tag);
					Score += 15;
					other.SendMessage("Die");
					//Vector3 hitPoint = new Vector3(other.contac.x,other.OverlapPoint().y);
					//Instantiate(Hit,other.collider2D.transform.position,Quaternion.identity);
					PlaySound(hitSound);
				}else{
					Debug.Log ("Hit by:" + other.gameObject.tag);
					Animator a = other.gameObject.GetComponent<Animator>();
					a.SetTrigger("Hit");
					Damage();
					if(HP == 0){
						GameOver();
					}

				}
			}
		}
	}

	void ThrowHadouken(){
		Vector3 newPos = new Vector3(transform.position.x+0.5f,transform.position.y);
		GameObject had = (GameObject)Instantiate(Hadouken,newPos, Quaternion.identity);
		had.SetActive(true);
		Vector2 vec = new Vector2(6.5f,0f);
		had.rigidbody2D.velocity = vec;
		HadoukenCounter--;
		if(HadoukenCounter == 0){
			StartCoroutine(HadoukenTimer());
		}
	}

	IEnumerator HadoukenTimer(){
		yield return new WaitForSeconds(10f);
		HadoukenCounter+=3;
	}

	public override void Damage ()
	{
		isRecovering = true;
		HP--;
		StartCoroutine(Flash(3f, 0.2f));
	}

	void GameOver(){
		PlayerPrefs.SetInt("score",Score);
		Application.LoadLevel(3);
	}

	public void Flip ()
	{
		isFacingRight = !isFacingRight;
		Vector3 curScale = transform.localScale;
		curScale.x *= -1;
		transform.localScale = curScale;
	}

	IEnumerator Flash(float time, float intervalTime)
	{
		Color c = new Color(1f,1f,1f,0.2f);
		Color[] colors = {Color.white,c};
		float elapsedTime = 0f;
		int index = 0;
		while(elapsedTime < time/10 )
		{
			//Debug.Log(elapsedTime);
			mat.color = colors[index % 2];
			elapsedTime += Time.deltaTime;
			index++;
			yield return new WaitForSeconds(intervalTime);
		}
		mat.color = Color.white;
		isRecovering = false;
	}

	string GetCurrentAnimState(){
		string[] states = {"Base.Run", "Base.Jump","Base.Roundhouse", "Base.Punch","Base.Uppercut", "Base.Super", "Base.Die"};

		foreach (string item in states) {
			bool s = anim.GetCurrentAnimatorStateInfo(0).IsName(item);
			if(s)return item;
		}
		return null;
	}
}
