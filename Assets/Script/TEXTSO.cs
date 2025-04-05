using UnityEngine;

[CreateAssetMenu(menuName = "TextContainer/Result Text")]
public class TEXTSO : ScriptableObject
{
    public string Title;
    [TextArea(5, 10)]
    public string[] paragraphs;
}
