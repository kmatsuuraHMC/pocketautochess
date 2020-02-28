using UnityEngine;
using System.Collections;
public class GetSprite
{

    /// スプライトをリソースから取得する.
    /// ※スプライトは「Resources/Sprites」以下に配置していなければなりません.
    /// ※fileNameに空文字（""）を指定するとシングルスプライトから取得します.
    public static Sprite GetSprite1(string fileName, string spriteName)
    {
        spriteName = null;
        if (spriteName == "")
        {
            // シングルスプライト
            return Resources.Load<Sprite>(fileName);
        }
        else
        {
            // マルチスプライト
            Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
            return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
        }
    }
}