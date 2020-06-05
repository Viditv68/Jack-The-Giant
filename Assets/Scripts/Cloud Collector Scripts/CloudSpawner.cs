using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] clouds;

    private float distanceBetweenTheClouds = 3f;
    private float minX, maxX;
    private float lastCloudPositionY;
    private float controlX;

    [SerializeField]
    private GameObject[] collectables;

    private GameObject player;

    private void Awake()
    {
        controlX = 0;
        SetMinAndMax();
        CreateClouds();
        player = GameObject.Find("Player");

        for(int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }
    }

    void Start()
    {
        PositionThePlayer();
    }

    void SetMinAndMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        maxX = bounds.x - 0.5f;
        minX = -bounds.x + 0.5f;
    }

    void Shuffle(GameObject[] arrayToShuffle)
    {
        for(int i = 0;i < arrayToShuffle.Length; i++)
        {
            GameObject tmp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = tmp;

        }

    }

    void CreateClouds()
    {
        Shuffle(clouds);

        float positionY = 0f;

        for(int i = 0; i < clouds.Length; i++)
        {
            Vector3 tmp = clouds[i].transform.position;
            tmp.y = positionY;

            if(controlX == 0)
            {
                tmp.x = Random.Range(0.0f, maxX);
                controlX = 1;
            }
            else if(controlX == 1)
            {
                tmp.x = Random.Range(0.0f, minX);
                controlX = 2;
            }
            else if(controlX == 2)
            {
                tmp.x = Random.Range(1.0f, maxX);
                controlX = 3;
            }
            else if(controlX == 3)
            {
                tmp.x = Random.Range(-1.0f, minX);
                controlX = 0;
            }
            lastCloudPositionY = positionY;
            clouds[i].transform.position = tmp;
            positionY -= distanceBetweenTheClouds;
        }
    }

    void PositionThePlayer()
    {
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        for(int i = 0; i < darkClouds.Length; i++)
        {
            if(darkClouds[i].transform.position.y == 0f)
            {
                Vector3 tmp = darkClouds[i].transform.position;
                darkClouds[i].transform.position = new Vector3(cloudsInGame[0].transform.position.x,
                                                               cloudsInGame[0].transform.position.y,
                                                               cloudsInGame[0].transform.position.z);
                cloudsInGame[0].transform.position = tmp;
            }   
        }

        Vector3 temp = cloudsInGame[0].transform.position;
        for(int i = 1; i < cloudsInGame.Length; i++)
        {
            if(temp.y < cloudsInGame[i].transform.position.y)
            {
                temp = cloudsInGame[i].transform.position;
            }
        }
        temp.y += 0.8f;

        player.transform.position = temp;

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Cloud" || target.tag == "Deadly")
        {
            if(target.transform.position.y == lastCloudPositionY)
            {
                Shuffle(clouds);
                Shuffle(collectables);

                Vector3 tmp = target.transform.position;

                for(int i = 0;i < clouds.Length; i++)
                {
                    if(!clouds[i].activeInHierarchy)
                    {
                        if (controlX == 0)
                        {
                            tmp.x = Random.Range(0.0f, maxX);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            tmp.x = Random.Range(0.0f, minX);
                            controlX = 2;
                        }
                        else if (controlX == 2)
                        {
                            tmp.x = Random.Range(1.0f, maxX);
                            controlX = 3;
                        }
                        else if (controlX == 3)
                        {
                            tmp.x = Random.Range(-1.0f, minX);
                            controlX = 0;
                        }
                        tmp.y -= distanceBetweenTheClouds;
                        lastCloudPositionY = tmp.y;

                        clouds[i].transform.position = tmp;
                        clouds[i].SetActive(true);

                        int random = Random.Range(0, collectables.Length);

                        if(clouds[i].tag != "Deadly")
                        {
                            if(!collectables[random].activeInHierarchy)
                            {
                                Vector3 tmp2 = clouds[i].transform.position;
                                tmp2.y += 0.7f;

                                if(collectables[random].tag == "Life")
                                {
                                    if(PlayerScore.lifeCount < 2)
                                    {
                                        collectables[random].transform.position = tmp2;
                                        collectables[random].SetActive(true);
                                    }
                                    
                                }
                                else
                                {
                                    collectables[random].transform.position = tmp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
