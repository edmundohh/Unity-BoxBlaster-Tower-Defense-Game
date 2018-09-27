using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Vector3[] path;

    public Vector3 GridPosition { get; set; }

    private Vector3 destination;

    private Animator myAnimator;

    public bool IsActive { get; set; }

    [SerializeField]
    private int health;

    public bool Alive {
        get { return health > 0; }
    }

    int count = 0;

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove){


        
        float progress = 0;
        while (progress <= 1){
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }
        transform.localScale = to;
        IsActive = true;

        if (remove){
            Release();
        }
    }

    public void Spawn(int health) {
        myAnimator = GetComponent<Animator>();
        transform.position = new Vector2(-8.5f, 7);

        this.health = health;
       
        path = Path.FindPath();
        SetPath();

        StartCoroutine(Scale(new Vector3(0.01f, 0.01f), new Vector3(1, 1), false));

    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (IsActive){
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);


            if (transform.position == destination)
            {
                if (count < path.Length)
                {
                    if (path != null)
                    {
                        destination = path[count];
                        count++;
                    }


                }
        }

        }
    }

    private void SetPath()
    {
        if (count != 0){
            count = 0;
        }


        destination = path[count];
        count++;

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LastTile"){
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.01f, 0.01f), true));

            GameManager.Instance.Lives--;
        }
    }

    public void Release(){
        IsActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.removeBox(this);
    }

    public void TakeDamage(int damage)  {
        if (IsActive){
            health -= damage;

            if (health <= 0){
                myAnimator.SetTrigger("Die");

                GetComponent<SpriteRenderer>().sortingOrder--;
                IsActive = false;

            }
        }
    }

}
