using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    private SpriteRenderer myRenderer;

    private Monster target;

    public Monster Target{
        get { return target; }
    }

    private float attackFrequency;

    [SerializeField]
    private float attackCoolDown;

    private Queue<Monster> boxes = new Queue<Monster>();

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

    public int Damage{
        get { return damage; }
    }

    public float ProjectileSpeed{
        get { return projectileSpeed; }
    }

    private bool canAttack = true;

   

	// Use this for initialization
	void Start () {
        myRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Attack();

	}

    public void Select() {
        myRenderer.enabled = !myRenderer.enabled;
    }

    public void Attack()
    {
        if (!canAttack)
        {
            attackFrequency += Time.deltaTime;

            if (attackFrequency >= attackCoolDown){
                canAttack = true;
                attackFrequency = 0;
            }
        }
        if (target == null && boxes.Count > 0)
        {
            target = boxes.Dequeue();
        }
        if (target != null && target.IsActive)
        { 
            if (canAttack){
                Shoot();

                canAttack = false;
            }
        }
        else if (boxes.Count > 0){
            target = boxes.Dequeue();
        }
        if (target != null && !target.Alive){
            target = null;
        }

    }


    private void Shoot() {
        Projectile projectile = GameManager.Instance.Pool.GetObject(2).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            boxes.Enqueue(other.GetComponent<Monster>());
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Box"){
            target = null;
        }
    }
}
