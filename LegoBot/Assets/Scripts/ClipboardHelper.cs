using UnityEngine;
using System.Runtime.InteropServices;

public class ClipboardHelper : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string str);

    public static void Copy(string text)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        CopyToClipboard(text);
#else
        GUIUtility.systemCopyBuffer = text;
#endif
    }
}