
#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public static class iOSCameraPermission
{

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    extern static private void _verifyPermission(string gameObject, string callback);
#endif

    public static void VerifyPermission(string gameObjectName, string callbackName)
    {
#if UNITY_IOS && !UNITY_EDITOR
        _verifyPermission(gameObjectName, callbackName);
#endif
    }
}