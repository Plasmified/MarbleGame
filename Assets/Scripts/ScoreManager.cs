using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private bool hasScored;
    [SerializeField]
    private int score;
    [SerializeField]
    private Transform marblePos;
    [SerializeField]
    private PinManager pinManager;
    [SerializeField]
    private GameObject marble;
    [SerializeField]
    private GameObject marblePrefab;
    [SerializeField]
    private bool hasSpawned = true;
    [SerializeField]
    private Text scoreText;

    // Update is called once per frame
    void Update()
    {
        checkMarblePos();
    }

    void checkMarblePos() {
        if (!hasScored)
        {
            if (marblePos.position.y < 0.15f)
            {
                bool scoreDetected = false;
                for (int i = 1; i < pinManager.barriersPos.Length; i++)
                {
                    if (marblePos.position.x < pinManager.barriersPos[i].x && marblePos.position.x > pinManager.barriersPos[i-1].x) {
                        scoreDetected = true;
                        score += pinManager.barrierScore[i];
                        scoreText.text = "Score : " + score;
                        hasScored = true;
                        break;
                    }
                }

                if (!scoreDetected) {
                    if (marblePos.position.x < pinManager.barriersPos[0].x)
                    {
                        scoreDetected = true;
                        score += pinManager.barrierScore[0];
                        scoreText.text = "Score : " + score;
                        hasScored = true;
                    }
                    else
                    {
                        score += pinManager.barrierScore[pinManager.barriersPos.Length];
                        scoreText.text = "Score : " + score;
                        hasScored = true;
                    }
                }
            }
        } else {
            Destroy(marble);
            hasSpawned = false;
            if (!hasSpawned)
            {
                marble = Instantiate(marblePrefab, new Vector3(Random.Range(-23, 23), 37.15f, 29), Quaternion.identity);
                marblePos = marble.transform;
                hasSpawned = true;
                hasScored = false;
            }
        }
    }
}
