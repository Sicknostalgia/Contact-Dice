using UnityEngine;

[CreateAssetMenu(menuName = "TextContainer/Result Text")]
public class TEXTSO : ScriptableObject
{
    [TextArea(5, 10)]
    public string[] paragraphs;
}
