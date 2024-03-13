using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pin;
    [SerializeField]
    private GameObject smallBarrier;
    [SerializeField]
    private Transform table;
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;
    [SerializeField]
    private bool barriersSpawned;
    [SerializeField]
    private bool pinsSpawnedA;
    [SerializeField]
    private bool pinsSpawnedB;

    private bool tableRotated;

    private float currPosX = -25f;

    private float currPosY = 5f;

    private float pinsA;

    private float[] posPinsB;

    public Vector3[] barriersPos;

    public int[] barrierScore;

    private void Start()
    {
        barriersPos = new Vector3[columns];
        barrierScore = new int[columns+1];
        for (int i = 0; i < barrierScore.Length; i++) {
            int rand = Random.Range(10, 150);

            while (rand % 10 != 0) {
                rand = Random.Range(10, 150);
            }

            barrierScore[i] = rand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnBarriers();
        spawnPinsB();
        spawnPinsA();
        if (!tableRotated)
        {
            table.Rotate(new Vector3(25, 0, 0));
            tableRotated = true;
        }
    }

    private void spawnBarriers()
    {
        if (!barriersSpawned) {
            float multiplier = 50 / (columns+1);

            for (int i = 0; i < columns; i++)
            {
                if (columns < 8) {
                    currPosX += multiplier;
                } else {
                    currPosX += multiplier + 0.5f;
                }

                barriersPos[i] = new Vector3(currPosX, 4.4f, 10.75f);

                Vector3 barrierPos = barriersPos[i];
                
                Instantiate(smallBarrier, barrierPos, smallBarrier.transform.rotation, table);
            }

            barriersSpawned = true;
        }
    }

    private void spawnPinsA() {
        if (!pinsSpawnedA)
        {
            for (int i = 0; i < pinsA; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    float distX = (Vector3.Distance(barriersPos[j], barriersPos[j + 1])) / 2;
                    float distY = (posPinsB[i + 1] - posPinsB[i]) / 2;
                    Vector3 pinPos = new Vector3(barriersPos[j].x + distX, posPinsB[i] + distY, 10.5f);

                    Instantiate(pin, pinPos, pin.transform.rotation, table);
                }
            }

            pinsSpawnedA = true;
        }
    }

    private void spawnPinsB()
    {
        if (!pinsSpawnedB)
        {
            int pinsB = 0;
            if (rows % 2 != 0) {
                int tmp = (rows / 2);
                pinsB = tmp + 1;
            } else {
                pinsB = (rows / 2);
            }
            pinsA = rows - pinsB;
            float multiplier = 45 / (pinsB+1);

            posPinsB = new float[pinsB];

            for (int i = 0; i < pinsB; i++)
            {
                currPosY += multiplier;
                posPinsB[i] = currPosY;
                for (int j = 0; j < barriersPos.Length; j++)
                {
                    Vector3 pinPos = new Vector3(barriersPos[j].x, currPosY, 10.5f);

                    Instantiate(pin, pinPos, pin.transform.rotation, table);
                }
            }

            pinsSpawnedB = true;
        }
    }
}
