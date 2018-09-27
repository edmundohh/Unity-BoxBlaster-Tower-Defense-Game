using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {
    

    private static float TileSize = LevelManager.Instance.TileSize;
    private static Vector3[] finalPath = new Vector3[19];
    static Vector3 startPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

    public static Vector3[] FindPath() {
        int z = 0;

        for (int y = 0; y < LevelManager.Instance.tileTypeList.Length; y++)
        {
            char[] newTiles = LevelManager.Instance.tileTypeList[y].ToCharArray();
            for (int x = 0; x < LevelManager.Instance.tileTypeList[y].ToCharArray().Length; x++){
                if (newTiles[x] == '1'||newTiles[x] == '5'){
                    finalPath[z] = new Vector3((startPos.x + (TileSize*x)+0.7f), ((startPos.y - (TileSize*y)-0.7f)));
                    z++;
                }
            }

        }


        return finalPath;
    }

    //public static Vector3[] SortPath(Vector3[] arr) {
    //    int i, j;
    //    int n = arr.Length;

    //    for (j = 0; j < n - 1; j++){
            
    //        int min = 0;
    //        for (i = j + 1; i < n; i++){
    //            min = i;
    //            Vector3 jVal = arr[j];
    //            Vector3 iVal = arr[i];
    //            Vector3 minVal = arr[min];

    //            if (Mathf.Sqrt(Mathf.Pow(jVal.x-iVal.x,2)+Mathf.Pow(jVal.y-iVal.y,2))
    //                < Mathf.Sqrt(Mathf.Pow(jVal.x - minVal.x, 2) + Mathf.Pow(jVal.y - minVal.y, 2))) {
    //                min = i;
    //            }
    //        }
    //        if (min != j){
    //            Vector3 temp = arr[min];
    //            arr[min] = arr[j];
    //            arr[j] = temp;
    //        }
                    

               
    //    }
    //    for (int y = 0; y < arr.Length; y++)
    //    {
    //        Debug.Log(arr[y]);
    //    }
    //    return arr;
    //}

       


   
}
