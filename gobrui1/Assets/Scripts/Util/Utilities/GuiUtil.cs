using UnityEngine;
using System.Collections;

public class GuiUtil
{
    private static Rect _guiRect = new Rect();
    static Rect GetGUIRect()
    {
        return _guiRect;
    }
    private static GUIStyle _guiStyle = null;
    static GUIStyle GetGUIStyle()
    {
        return _guiStyle ?? (_guiStyle = new GUIStyle());
    }
    /// フォントサイズを設定.
    public static void SetFontSize(int size)
    {
        GetGUIStyle().fontSize = size;
    }
    /// フォントカラーを設定.
    public static void SetFontColor(Color color)
    {
        GetGUIStyle().normal.textColor = color;
    }
    /// フォント位置設定
    public static void SetFontAlignment(TextAnchor align)
    {
        GetGUIStyle().alignment = align;
    }
    /// ラベルの描画.
    public static void GUILabel(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        GUI.Label(rect, text, GetGUIStyle());
    }
    /// ボタンの配置.
    public static bool GUIButton(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        return GUI.Button(rect, text, GetGUIStyle());
    }
}