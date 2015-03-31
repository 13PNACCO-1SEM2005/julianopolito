using UnityEngine;
using System.Collections;

public class GameObjectBase : MonoBehaviour {

	public int HP = 1;
	public bool isFacingRight = true;
	public string[] animationStates;
	protected Animator anim;
	protected Material mat;

	void Start(){
		mat = GetComponent<SpriteRenderer>().material;
		anim.GetComponent<Animator>();
	}

	public virtual void PlaySound(AudioClip sound){
		AudioSource.PlayClipAtPoint(sound, transform.position);
	}

	public virtual void Damage(){

	}

	public virtual void Die(){

	}

	public virtual void SetActive(bool fl){
		this.gameObject.SetActive(fl);
	}

	public virtual void Flip ()
	{
		isFacingRight = !isFacingRight;
		Vector3 curScale = transform.localScale;
		curScale.x *= -1;
		transform.localScale = curScale;
	}
	
	public virtual IEnumerator Flash(float time, float intervalTime)
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
	}
	
	public virtual string GetCurrentAnimState(){
		if(!anim) return "";

		string[] states = animationStates;
		
		foreach (string item in states) {
			bool s = anim.GetCurrentAnimatorStateInfo(0).IsName(item);
			if(s)return item;
		}
		return null;
	}
}
