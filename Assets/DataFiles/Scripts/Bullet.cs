using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject particleEffect;
	float timer=2f;

		void Update () {
		timer-= Time.deltaTime;
		if(timer<=0){
			createParticle();
			Destroy(gameObject);
		}
	}

// création de particule quand la bullet finit sa course 
	public void createParticle(){
		Instantiate(particleEffect,transform.position,transform.rotation);
	}
}
