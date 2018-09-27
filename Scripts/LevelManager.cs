using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{

    [SerializeField]
    private GameObject[] tiles;

    [SerializeField]
    private CameraView cameraView;

    public string[] tileTypeList;



    public float TileSize
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    [SerializeField]
    private Transform map;

    public Dictionary<GridPoint, TileScript> TileList { get; set; }

    // Use this for initialization
    void Start()
    {
        ConstructLevel();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }


    private void ConstructLevel()
    {

        TileList = new Dictionary<GridPoint, TileScript>();

        string[] mapData = new string[]
        {
            "010000000000000", 
            "011110020000000", 
            "000010000000000", 
            "000011111110000",
            "000000000010000", 
            "000000030011115",
            "000000000000000"
        };


        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        tileTypeList = mapData;

        Vector3 maxTile = Vector3.zero;

        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();


            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, startPosition);
            }
        }
        maxTile = TileList[new GridPoint(mapX - 1, mapY - 1)].transform.position;

        cameraView.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

    }



    private void PlaceTile(string tileType, int x, int y, Vector3 startPosition) 
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tiles[tileIndex]).GetComponent<TileScript>();

            
        newTile.SetUp(new GridPoint(x, y), new Vector3(startPosition.x + (TileSize * x),
                                                       startPosition.y - (TileSize * y), 0), map);




    }

}

 