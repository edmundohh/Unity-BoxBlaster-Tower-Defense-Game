﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Monster target;

    private Tower parent;

    private Animator myAnimator;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
        MoveToTarget();
	}

    public void Initialize(Tower parent) {
        this.target = parent.Target;
        this.parent = parent;
    }

    private void MoveToTarget() {
        if (target != null && target.IsActive) {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime*parent.ProjectileSpeed);
        }
        else if (!target.IsActive) {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Box") {
            if (target.gameObject == other.gameObject){
                target.TakeDamage(parent.Damage);

                myAnimator.SetTrigger("Impact");
               
            }


        }
    }

}
