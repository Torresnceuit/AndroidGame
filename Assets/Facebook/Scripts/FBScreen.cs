        // TODO Auto-generated method stub
    }

    @Override
    public void setInputMethod(IBinder arg0, String arg1) throws RemoteException {
        // TODO Auto-generated method stub

    }

    @Override
    public void setInputMethodAndSubtype(IBinder arg0, String arg1, InputMethodSubtype arg2)
            throws RemoteException {
        // TODO Auto-generated method stub

    }

    @Override
    public boolean setInputMethodEnabled(String arg0, boolean arg1) throws RemoteException {
        // TODO Auto-generated method stub
        return false;
    }

    @Override
    public void showInputMethodAndSubtypeEnablerFromClient(IInputMethodClient arg0, String arg1)
            throws RemoteException {
        // TODO Auto-generated method stub

    }

    @Override
    public void showInputMethodPickerFromClient(IInputMethodClient arg0) throws RemoteException {
        // TODO Auto-generated method stub

    }

    @Override
    public void showMySoftInput(IBinder arg0, int arg1) throws RemoteException {
        // TODO Auto-generated method stub

    }

    @Override
    public boolean showSoftInput(IInputMethodClient arg0, int arg1, ResultReceiver arg2)
            throws RemoteException {
        // TODO Auto-generated method stub
        return false;
    }

    @Override
    public InputBindResult startInput(IInputMethodClient client, IInputContext inputContext,
            EditorInfo attribute, int controlFlags) throws RemoteException {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public boolean switchToLastInputMethod(IBinder arg0) throws RemoteException {
        // TODO Auto-generated method stub
        return false;
    }

    @Override
    public boolean switchToNextInputMethod(IBinder arg0, boolean arg1) throws RemoteException {
        // TODO Auto-generated method stub
        return false;
    }

    @Override
    public boolean shouldOfferSwitchingToNextInputMethod(IBinder arg0) throws RemoteException {
        // TODO Auto-generated method stub
        return false;
    }

    @Override
     public int getInputMethodWindowVisibleHeight() throws RemoteException {
        // TODO Auto-generated method stub
        return 0;
    }

    @Override
    public void notifyUserAction(int sequenceNumber) throws RemoteException {
        // TODO Auto-generated method stub
    }

    @Override
    public void updateStatusIcon(IBinder arg0, String arg1, int arg2) throws RemoteException {
        // TODO Auto-generated method stub

    }

    @Override
    public InputBindResult windowGainedFocus(IInputMethodClient client, IBinder windowToken,
            int controlFlags, int softInputMode, int windowFlags, EditorInfo attribute,
            IInputContext inputContext) throws RemoteException {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public IBinder asBinder() {
        // TODO Auto-generated method stub
        return null;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/**
 * Basic wrapper around UnityEngine.Screen
 * Allows for window resizing within Facebook Canvas
 */
public class FBScreen {

    private static bool resizable = false;

    public static bool FullScreen {
        get { return Screen.fullScreen; }
        set { Screen.fullScreen = value; }
    }

    // Is the game resizable by the user?
    // (ex. By growing or shrinking the browser window)
    public static bool Resizable
    {
        get { return resizable; }
    }

    public static int Width
    {
        get { return Screen.width; }
    }

    public static int Height
    {
        get { return Screen.height; }
    }

    public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params Layout[] layoutParams)
    {
#if !UNITY_WEBPLAYER
        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
#else
        if (fullscreen)
        {
            Screen.SetResolution(width, height, true, preferredRefreshRate);
        }
        else
        {
            resizable = false;
            Application.ExternalCall("IntegratedPluginCanvas.setResolution", width, height);
            SetLayout(layoutParams);
        }
#endif
    }

    public static void SetAspectRatio(int width, int height, params Layout[] layoutParams)
    {
#if !UNITY_WEBPLAYER
        var newWidth = Screen.height / height * width;
        Screen.SetResolution(newWidth, Screen.height, Screen.fullScreen);
#else
        resizable = true;
        Application.ExternalCall("IntegratedPluginCanvas.setAspectRatio", width, height);
        SetLayout(layoutParams);
#endif
    }

    public static void SetUn