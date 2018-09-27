using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : Singleton<GameManager>
{
    private int level = 0;

    [SerializeField]
    private Text Level;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private GameObject levelButton;

    [SerializeField]
    private GameObject gameOverMenu;

    private int health = 2;

    private List<Monster> activeBoxes = new List<Monster>();

    private int lives;

    private bool gameOver = false;

    private Tower selectedTower;

    public ObjectPool Pool
    {
        get;
        set;
    }

    public int Lives{
        get { return lives; }
        set
        {
            this.lives = value;

            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }

            livesText.text = string.Format("HP: <color=red>{0}</color>", lives);

        }

    }

    public bool LevelActive{
        get { return activeBoxes.Count > 0; }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public TowerButton Clicked
    {
        get;
        set;
    }


    // Use this for initialization
    void Start () {
        Lives = 4;
	}
	
	// Update is called once per frame
	void Update () {
        DropTower();
	}

    public void PickTower(TowerButton towerButton){
        if(!LevelActive){
            this.Clicked = towerButton;
            MouseHover.Instance.Activate(towerButton.Sprite);
        }
       
    }

    private void DropTower(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            MouseHover.Instance.Deactivate();
        }
    }

    public void SelectTower(Tower tower){
        if (selectedTower != null){
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower(){
        if (selectedTower != null){
            selectedTower.Select();
        }
        selectedTower = null;
    }

    public void Play()
    {

        level++;

        Level.text = string.Format("Level: <color=blue>{0}</color>", level);
        StartCoroutine(Spawn());
        levelButton.SetActive(false);

    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < level; i++){
            Monster box = Pool.GetObject(1).GetComponent<Monster>();
            box.Spawn(health);

            //if (level % 3 == 0){
            //    health += 2;
            //}

            activeBoxes.Add(box);

            yield return new WaitForSeconds(1);
        }
       
    }


    public void removeBox(Monster box){
        activeBoxes.Remove(box);

        if (!LevelActive && !gameOver){
            levelButton.SetActive(true);
        }
    }

    public void GameOver(){
        if (!gameOver){
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
