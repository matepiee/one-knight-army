using UnityEngine;

public static class UIManager
{
    public static int OpenWindowCount = 0;

    public static bool IsAnyUIOpen => OpenWindowCount > 0;
}
