using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoSquare
{
    public int number;
    public bool isOpen;
}

public class BingoManager : MonoBehaviour
{
    public const int SQUARE_COUNT = 25;
    private List<BingoSquare> bingoSquareList = new List<BingoSquare>();
    [SerializeField] private CardAreaView cardAreaView;

    private void GenerateBingoCard(int maxNumber)
    {
        bingoSquareList.Clear();
        maxNumber = Mathf.Min(Mathf.Max(maxNumber, SQUARE_COUNT), 99);
        List<BingoSquare> tempList = new List<BingoSquare>();
        for (int i = 1; i <= maxNumber; i++)
        {
            BingoSquare bingoSquare = new BingoSquare();
            bingoSquare.number = i;
            bingoSquare.isOpen = false;
            tempList.Add(bingoSquare);
        }
        for (int i = 0; i < SQUARE_COUNT; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            bingoSquareList.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        GenerateBingoCard(9);
        for (int i = 0; i < SQUARE_COUNT; i++)
        {
            cardAreaView.SetCardNumber(i, bingoSquareList[i].number);
        }
    }
}
