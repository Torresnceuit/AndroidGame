outSurface)
            throws RemoteException {
        // pass for now
        return null;
    }

    @Override
    public boolean performDrag(IWindow window, IBinder dragToken,
            float touchX, float touchY, float thumbCenterX, float thumbCenterY,
            ClipData data)
            throws RemoteException {
        // pass for now
        return false;
    }

    @Override
    public void reportDropResult(IWindow window, boolean consumed) throws RemoteException {
        // pass for now
    }

    @Override
    public void dragRecipientEntered(IWindow window) throws RemoteException {
        // pass for now
    }

    @Override
    public void dragRecipientExited(IWindow window) throws RemoteException {
        // pass for now
    }

    @Override
    public void setWallpaperPosition(IBinder window, float x, float y,
        float xStep, float yStep) {
        // pass for now.
    }

    @Override
    public void wallpaperOffsetsComplete(IBinder window) {
        // pass for now.
    }

    @Override
    public void setWallpaperDisplayOffset(IBinder windowToken, int x, int y) {
        // pass for now.
    }

    @Override
    public Bundle sendWallpaperCommand(IBinder window, String action, int x, int y,
            int z, Bundle extras, boolean sync) {
        // pass for now.
        return null;
    }

    @Override
    public void wallpaperCommandComplete(IBinder window, Bundle result) {
        // pass for now.
    }

    @Override
    public void setUniverseTransform(IBinder window, float alpha, float offx, float offy,
            float dsdx, float dtdx, float dsdy, float dtdy) {
        // pass for now.
    }

    @Override
    public IBinder asBinder() {
        // pass for now.
        return null;
    }

    @Override
    public void onRectangleOnScreenRequested(IBinder window, Rect rectangle) {
        // pass for now.
    }

    @Override
    public IWindowId getWindowId(IBinder window) throws RemoteException {
        // pass for now.
        return null;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   