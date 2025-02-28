using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTextCtrler : MonoBehaviour
{
    [SerializeField] TEXTSO textPara;
    [SerializeField] TextMeshProUGUI tmPro;
    [SerializeField] FaceCollider faceCol;
    [SerializeField] TEXTSO[] candidateText;

    string ParagraphUpdate(TEXTSO textSO)
    {
        tmPro.text = string.Empty;
        if (textPara != null && textPara.paragraphs.Length > 0) //may laman yung SO tsaka di blanko ang paragraph
        {
            int randomText = Random.Range(0, textPara.paragraphs.Length);
            return textSO.paragraphs[randomText];
        }
        return "No text";
    }
    string ParaValue()
    {
        switch (faceCol.normVec)
        {
            case FaceCollider.NormalVector.right:
                return ParagraphUpdate(candidateText[0]);
            case FaceCollider.NormalVector.left:
                return ParagraphUpdate(candidateText[1]);
            case FaceCollider.NormalVector.top:
                return ParagraphUpdate(candidateText[2]);
            case FaceCollider.NormalVector.bottom:
                return ParagraphUpdate(candidateText[3]);
            case FaceCollider.NormalVector.front:
                return ParagraphUpdate(candidateText[4]);
            case FaceCollider.NormalVector.back:
                return ParagraphUpdate(candidateText[5]);
             default:
                return null;
        }
    }
    

    /*private TEXTSO GetTextSOValue(FaceCollider.NormalVector face)
    {
        return faceCol.faceTex.TryGetValue(face, out TEXTSO textDisplay) ? textDisplay : null;
    }*/
}
