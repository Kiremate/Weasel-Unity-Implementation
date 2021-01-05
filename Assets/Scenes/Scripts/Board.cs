using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    public int MAX_COLS = 5;
    public int MAX_ROWS = 5;

    public int MAX_ROWS_COLS = 5;

    public List<GameObject> board;
    public Transform thisTransform;
    public GameObject fichaPrefab;
    public GridLayoutGroup layout;


    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<GridLayoutGroup>(out layout);
        TryGetComponent<Transform>(out thisTransform);

        SpawnBoard();
    }

    // Update is called once per frame
    void Update()
    {
        layout.constraintCount = MAX_ROWS_COLS;

    }


    public void SpawnBoard()
    {
        for(int i = 0; i < MAX_ROWS_COLS; ++i)
        {
            GameObject ficha = Instantiate(fichaPrefab, thisTransform);
            TextMeshPro textFicha = ficha.GetComponentInChildren<TextMeshPro>();
            textFicha.text = i.ToString();
        }

        //for (int i = 0; i < MAX_ROWS; ++i)
        //{
        //    for(int j = 0; j < MAX_COLS; ++j)
        //    {

        //    }
        //}
    }



}
