using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTextCtrler : MonoBehaviour
{
    [SerializeField] TEXTSO textPara;
    [SerializeField] TextMeshProUGUI tmPro;
    [SerializeField] FaceCollider faceCol;
    [SerializeField] TEXTSO[] candidateText;

    public void ParagraphUpdate(string parag)
    {
        tmPro.text = string.Empty;
        if(textPara != null && textPara.paragraphs.Length > 0) //may laman yung SO tsaka di blanko ang paragraph
        {
            string randomText = textPara.paragraphs[Random.Range(0, textPara.paragraphs.Length)];
            tmPro.text = randomText;
        }
    }

    /*private TEXTSO GetTextSOValue(FaceCollider.NormalVector face)
    {
        return faceCol.faceTex.TryGetValue(face, out TEXTSO textDisplay) ? textDisplay : null;
    }*/
}
