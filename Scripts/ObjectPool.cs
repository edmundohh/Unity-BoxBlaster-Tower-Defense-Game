using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField]
    private GameObject prefabs;

    [SerializeField]
    private GameObject ball;


    private List<GameObject> pooledObjects = new List<GameObject>();



    public GameObject GetObject(int i)
    {
        if (i == 1)
        {
            foreach (GameObject x in pooledObjects)
            {
                if (!x.activeInHierarchy)
                {
                    x.SetActive(true);
                    return x;
                }
            }


            GameObject newObject = Instantiate(prefabs);
            pooledObjects.Add(newObject);

            return newObject;
        }
        if (i == 2)
        {
            
            GameObject newBall = Instantiate(ball);

            return newBall;
        }
        else
            return null;
    }

        public void ReleaseObject(GameObject gameObject)
        {
        
            gameObject.SetActive(false);

        }
        
}
       



