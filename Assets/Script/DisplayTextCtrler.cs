using UnityEngine;
using TMPro;

public class DisplayTextCtrler : MonoBehaviour
{
    [SerializeField] TEXTSO textPara;
    [SerializeField] TextMeshProUGUI tmPro;


    void ParagraphUpdate(string parag)
    {
        tmPro.text = string.Empty;
        tmPro.text = parag;
    }
}
