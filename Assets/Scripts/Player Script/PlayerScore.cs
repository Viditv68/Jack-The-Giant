using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinClip, lifeClip;

    private CameraScripts cameraScript;

    private Vector2 previousPosition;
    private bool countScore;

    public static int scoreCount, lifeCount, coinCount;

    private void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraScripts>();
    }
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        countScore = true;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void CountScore()
    {
        if(countScore)
        {
            if (transform.position.y < previousPosition.y)
                scoreCount++;

            previousPosition = transform.position;
            GameplayController.instance.SetScore(scoreCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {
            coinCount++;
            scoreCount += 200;
            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetCoinScore(coinCount);

            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if(target.tag == "Life")
        {
            lifeCount++;
            scoreCount += 300;
            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.setLifeScore(lifeCount);

            AudioSource.PlayClipAtPoint(lifeClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if(target.tag == "Bounds")
        {
            cameraScript.moveCamera = false;
            countScore = false;

            GameplayController.instance.GameOverShowPanel(scoreCount, coinCount);
            transform.position = new Vector3(500, 500, 0);
            lifeCount--;
            
        }

        if(target.tag == "Deadly")
        {
            cameraScript.moveCamera = false;
            countScore = false;
            GameplayController.instance.GameOverShowPanel(scoreCount, coinCount);
            transform.position = new Vector3(500, 500, 0);
            lifeCount--;
            
        }
    }
}
