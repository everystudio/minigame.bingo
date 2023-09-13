using UnityEngine;
using TMPro;

public class AreaInfoView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentNumberText;
    [SerializeField] private TextMeshProUGUI subInfoText;

    public TextMeshProUGUI CurrentNumberText => currentNumberText;
    public TextMeshProUGUI SubInfoText => subInfoText;

    private void Awake()
    {
        BingoManager.OnBingoNumber += (number) =>
        {
            currentNumberText.text = number.ToString();
        };
        subInfoText.text = "";
        BingoManager.OnChangeSubInfoText += (text) =>
        {
            subInfoText.text = text;
        };
    }
}