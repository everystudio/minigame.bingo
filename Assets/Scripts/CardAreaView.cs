using UnityEngine;

public class CardAreaView : MonoBehaviour
{
    [SerializeField] private Color closeImageColor;
    [SerializeField] private Color closeTextColor;
    [SerializeField] private Color openImageColor;
    [SerializeField] private Color openTextColor;

    [SerializeField] private CardView[] cardViews;

    public void SetCardNumber(int cardIndex, int cardNumber)
    {
        cardViews[cardIndex].SetCardNumber(cardNumber);
    }

    public void SetCardOpen(int cardIndex)
    {
        cardViews[cardIndex].SetColor(openImageColor, openTextColor);
    }

    public void SetCardClose(int cardIndex)
    {
        cardViews[cardIndex].SetColor(closeImageColor, closeTextColor);
    }
}
