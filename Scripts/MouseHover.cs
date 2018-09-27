using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : Singleton<MouseHover> {


    private SpriteRenderer hoverSprite;

    private SpriteRenderer rangeSprite;

	// Use this for initialization
	void Start () {
        this.hoverSprite = GetComponent<SpriteRenderer>();
        this.rangeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Follow();
	}

    private void Follow() {

        if (hoverSprite.enabled)
        {

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Activate(Sprite sprite){
        this.hoverSprite.sprite = sprite;
        hoverSprite.enabled = true;
        rangeSprite.enabled = true;
    }

    public void Deactivate(){
        hoverSprite.enabled = false;
        rangeSprite.enabled = false;
        GameManager.Instance.Clicked = null;

    }
}
