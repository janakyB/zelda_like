using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{

    Animator anim;
    public float speed;
    int direction;
    float changeTimer = 0.2f;
    bool shouldChange;
    public Transform rewardPosition;
    public GameObject potion;

    float directionTimer = 0.7f;
    public int health;
    bool canAttack;
    float attackTimer = 2f;
    public GameObject deathParticle;
    public GameObject projectile;
    public float thrustPower;
	float specialAttackTimer = 0.5f;
    public int touta0 = 0;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        canAttack = false;
        shouldChange = false;
    }

    void Update()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= touta0)
        {
            directionTimer = Random.Range(0,2);
            switch (direction)
			{
				case 1: 
					direction=2;
					break;
				case 2:
					direction =0;
					break;
				case 0:
					direction = 3;
					break;
				case 3:
					direction = 1;
					break;
			}
        }
        
        Movement();
        attackTimer -= Time.deltaTime;
        if (attackTimer <= touta0)
        {
            attackTimer = 2f;
            canAttack = true;
        }
        attack();
        if (shouldChange)
        {
            changeTimer -= Time.deltaTime;
            if (changeTimer <= touta0)
            {
                changeTimer = 0.2f;
                shouldChange = false;
            }
        }
		specialAttackTimer -= Time.deltaTime;
		if(specialAttackTimer<=touta0){
			specialAttackTimer = 0.5f;
			specialAttack();
			specialAttack();
		}

    }
    void attack()
    {
        // direction d'attaque de ses projectites
        if (!canAttack) return;
        canAttack = false;

        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        if (direction == 0)
        {
            newProjectile.transform.Rotate(0, 0, 0);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
        }
        else if (direction == 1)
        {
            newProjectile.transform.Rotate(0, 0, -180);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.down * thrustPower);
        }
        else if (direction == 2)
        {
            newProjectile.transform.Rotate(0, 0, 90);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrustPower);
        }
        else if (direction == 3)
        {
            newProjectile.transform.Rotate(0, 0, -90);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
        }
    }
    void Movement()
    {
        // les directions de mouvement pour le boss
        if (direction == 0)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            anim.SetInteger("dir", direction);
        }
        else if (direction == 1)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            anim.SetInteger("dir", direction);
        }
        else if (direction == 2)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", direction);
        }
        else if (direction == 3)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", direction);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // si rentre en contact de l'arme du player il pert de la vie
        if (col.gameObject.tag == "Sword")
        {
            col.gameObject.GetComponent<Sword>().createParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
            Destroy(col.gameObject);
            if (health <= 0)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
                Instantiate(potion,rewardPosition.position,potion.transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // même principe que pour les autres ennemies si rentre en contact avec le player il peut plus toucher le player pendant un certains temps
        if (col.gameObject.tag == "Player")
        {
            health--;
            if (!col.gameObject.GetComponent<Player>().invisibilityFrame)
            {
                col.gameObject.GetComponent<Player>().currentHealth--;
                col.gameObject.GetComponent<Player>().invisibilityFrame = true;
            }
            if (health <= touta0)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "wall")
        {
            if (shouldChange) return;
            if (direction == 0)
            {
                direction = 1;
            }
            else if (direction == 1)
            {
                direction = 0;
            }
            else if (direction == 2)
            {
                direction = 3;
            }
            else if (direction == 3)
            {
                direction = 2;
            }
            shouldChange = true;
        }
    }
   // si il arrive a un certains nombre de pv il passe dans ce mode d'attaque
	void specialAttack(){
		GameObject newProjectile = Instantiate(projectile,transform.position,transform.rotation);
		direction = Random.Range(0,3);
		switch (direction)
		{
			case 0:newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);break;
			case 1:newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.down * thrustPower); break;
			case 2:newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrustPower); break;
			case 3: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower); break;
		}
	}
}
