using UnityEngine;

[CreateAssetMenu(menuName = "TextContainer/")]
public class TEXTSO : ScriptableObject
{
    [TextArea(5, 10)]
    public string[] paragraphs;
}
