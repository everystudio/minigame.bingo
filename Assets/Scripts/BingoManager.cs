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

    private List<int> bingoNumberBuffer = new List<int>();
    public static System.Action<int> OnBingoNumber;

    private void GenerateBingoCard(int maxNumber)
    {
        bingoSquareList.Clear();
        bingoNumberBuffer.Clear();
        maxNumber = Mathf.Min(Mathf.Max(maxNumber, SQUARE_COUNT), 99);
        List<BingoSquare> tempList = new List<BingoSquare>();
        for (int i = 1; i <= maxNumber; i++)
        {
            BingoSquare bingoSquare = new BingoSquare();
            bingoSquare.number = i;
            bingoSquare.isOpen = false;
            tempList.Add(bingoSquare);
            bingoNumberBuffer.Add(i);
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
        GenerateBingoCard(25);
        for (int i = 0; i < SQUARE_COUNT; i++)
        {
            cardAreaView.SetCardNumber(i, bingoSquareList[i].number);
            cardAreaView.SetCardClose(i);
        }
        //DebugShowRowLine(11);
        //DebugShowColLine(11);
    }

    public void Next()
    {
        // まだ空いていないSquareを探す
        int number = GetRandomNumber();
        if (number == -1)
        {
            return;
        }

        // イベント発信
        OnBingoNumber?.Invoke(number);

        // 数字から何番目のSquareのIndexかを探す
        int squareIndex = bingoSquareList.FindIndex(x => x.number == number);

        // 要素内に存在しない場合はエラー
        if (squareIndex == -1)
        {
            Debug.Log($"number:{number} squareIndex:{squareIndex}");
            return;
        }
        // 表示更新(ここもイベント化しても良さそう)
        cardAreaView.SetCardOpen(squareIndex);
        // 変数の更新
        bingoSquareList[squareIndex].isOpen = true;

        if (IsBingo(squareIndex))
        {
            Debug.Log("Bingo!(縦横の列が)");
        }
    }

    private int GetRandomNumber()
    {
        if (bingoNumberBuffer.Count == 0)
        {
            return -1;
        }
        int randomIndex = Random.Range(0, bingoNumberBuffer.Count);
        int bingoNumber = bingoNumberBuffer[randomIndex];
        bingoNumberBuffer.RemoveAt(randomIndex);
        return bingoNumber;
    }

    private void DebugShowRowLine(int index)
    {
        int row = index / 5;
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"row:{row} col:{i} {bingoSquareList[row * 5 + i].number}");
        }
    }
    private void DebugShowColLine(int index)
    {
        int col = index % 5;
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"row:{i * 5} col:{col} {bingoSquareList[i * 5 + col].number}");
        }
    }
    // 特定のsquareでBingoになっているかを判定する
    public bool IsBingo(int squareIndex)
    {
        // まずはsquareIndexが有効かを確認する
        if (squareIndex < 0 || squareIndex >= SQUARE_COUNT)
        {
            return false;
        }
        // そのsquareが開いているかを確認する
        if (!bingoSquareList[squareIndex].isOpen)
        {
            return false;
        }
        // そのsquareがBingoになっているかを確認する
        int row = squareIndex / 5;
        int col = squareIndex % 5;
        // 横の判定
        bool isBingo = true;
        for (int i = 0; i < 5; i++)
        {
            if (!bingoSquareList[row * 5 + i].isOpen)
            {
                isBingo = false;
                break;
            }
        }
        if (isBingo)
        {
            return true;
        }

        // 縦の判定
        isBingo = true;
        for (int i = 0; i < 5; i++)
        {
            if (!bingoSquareList[col + i * 5].isOpen)
            {
                isBingo = false;
                break;
            }
        }
        if (isBingo)
        {
            return true;
        }
        return false;
    }

    /*




        // 斜めの判定
        if (squareIndex % 6 == 0)
        {
            isBingo = true;
            for (int i = 0; i < 5; i++)
            {
                if (!bingoSquareList[i * 6].isOpen)
                {
                    isBingo = false;
                    break;
                }
            }
            if (isBingo)
            {
                return true;
            }
        }
        if (squareIndex % 4 == 0)
        {
            isBingo = true;
            for (int i = 0; i < 5; i++)
            {
                if (!bingoSquareList[i * 4 + 4].isOpen)
                {
                    isBingo = false;
                    break;
                }
            }
            if (isBingo)
            {
                return true;
            }
        }
    */



}
