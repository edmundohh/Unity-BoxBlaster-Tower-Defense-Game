using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    public GridPoint GridPosition { get; private set; }

    private SpriteRenderer tileColour;

    private Color32 redColour = new Color32(255, 118, 118, 255);

    private Color32 blueColour = new Color32(0, 0, 205, 255);

    private Tower myTower;


    public bool IsEmpty
    {
        get;
        private set;
    }

    // Use this for initialization
	void Start () {
        tileColour = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUp(GridPoint pos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = pos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.TileList.Add(pos, this);
    }

    private void OnMouseOver()
    {
        
       if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.Clicked != null)
        {
            if (IsEmpty)
            {
                ColourTile(blueColour);
            }
            if (!IsEmpty)
            {
                ColourTile(redColour);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.Clicked == null
                 && Input.GetMouseButtonDown(0)){
            if (myTower != null){
                GameManager.Instance.SelectTower(myTower);
            }
            else {
                GameManager.Instance.DeselectTower();
            }
        }
        
    }

    private void OnMouseExit()
    {
        ColourTile(Color.white);
    }

    private void PlaceTower() 
    {
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.Clicked.Tower, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();



        tower.transform.SetParent(transform);
        MouseHover.Instance.Deactivate();

        GameManager.Instance.Clicked = null;

        IsEmpty = false;
        ColourTile(Color.white);
    }

    private void ColourTile(Color newColour){
        tileColour.color = newColour;
    }

}
