using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    private GameObject[] backgrounds;
    private float lastY;

    // Start is called before the first frame update
    void Start()
    {
        GetBacgroundsAndLastY();
    }

    void GetBacgroundsAndLastY()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        lastY = backgrounds[0].transform.position.y;

        for(int i = 1; i < backgrounds.Length; i++)
        {
            lastY = backgrounds[i].transform.position.y;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Background")
        {
            if(target.transform.position.y == lastY)
            {
                Vector3 tmp = target.transform.position;
                float height = ((BoxCollider2D)target).size.y;

                for(int i = 0;i < backgrounds.Length; i++)
                { 
                    if(!backgrounds[i].activeInHierarchy)
                    {
                        tmp.y -= height;
                        lastY = tmp.y;

                        backgrounds[i].transform.position = tmp;
                        backgrounds[i].SetActive(true);
                    }
                }

            }
        }

    }
}
