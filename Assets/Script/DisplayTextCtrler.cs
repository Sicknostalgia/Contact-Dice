using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTextCtrler : MonoBehaviour
{
    [SerializeField] TEXTSO textPara;
    [SerializeField] TextMeshProUGUI tmPro;
    [SerializeField] FaceCollider faceCol;
    [SerializeField] TEXTSO[] candidateText;

    public void UpdatePara(FaceCollider.NormalVector nVecFaceCol)
    {
        tmPro.text = ParaValue(nVecFaceCol);
    }
    string ReturnRandomPara(TEXTSO textSO)
    {
        tmPro.text = string.Empty;
        if (textPara != null && textPara.paragraphs.Length > 0) //may laman yung SO tsaka di blanko ang paragraph
        {
            int randomText = Random.Range(0, textPara.paragraphs.Length);
            return textSO.paragraphs[randomText];
        }
        return "No text";
    }
    string ParaValue(FaceCollider.NormalVector normVec)
    {
        switch (normVec)
        {
            case FaceCollider.NormalVector.right:
                return ReturnRandomPara(candidateText[0]);
            case FaceCollider.NormalVector.left:
                return ReturnRandomPara(candidateText[1]);
            case FaceCollider.NormalVector.top:
                return ReturnRandomPara(candidateText[2]);
            case FaceCollider.NormalVector.bottom:
                return ReturnRandomPara(candidateText[3]);
            case FaceCollider.NormalVector.front:
                return ReturnRandomPara(candidateText[4]);
            case FaceCollider.NormalVector.back:
                return ReturnRandomPara(candidateText[5]);
            default:
                return null;
        }
    }


    /*private TEXTSO GetTextSOValue(FaceCollider.NormalVector face)
    {
        return faceCol.faceTex.TryGetValue(face, out TEXTSO textDisplay) ? textDisplay : null;
    }*/
}
