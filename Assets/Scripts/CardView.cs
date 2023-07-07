using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardNumberText;

    public void SetCardNumber(int cardNumber)
    {
        cardNumberText.text = cardNumber.ToString();
    }

    public void SetColor(Color imageColor , Color textColor)
    {
        cardImage.color = imageColor;
        cardNumberText.color = textColor;
    }
}
