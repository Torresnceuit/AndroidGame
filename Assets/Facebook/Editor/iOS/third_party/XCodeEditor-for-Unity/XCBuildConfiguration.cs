 ActionBar and the app content in it.
     */
    protected View getDecorContent() {
        return mDecorContent;
    }

    /** Setup things like the title, subtitle, icon etc. */
    protected void setupActionBar() {
        setTitle();
        setSutTitle();
        setIcon();
        setHomeAsUp(mCallback.getHomeButtonStyle() == HomeButtonStyle.SHOW_HOME_AS_UP);
    }

    protected abstract void setTitle(CharSequence title);
    protected abstract void setSubtitle(CharSequence subtitle);
    protected abstract void setIcon(String icon);
    protected abstract void setHomeAsUp(boolean homeAsUp);

    private void setTitle() {
        RenderResources res = mBridgeContext.getRenderResources();

        String title = mParams.getAppLabel();
        ResourceValue titleValue = res.findResValue(title, false);
        if (titleValue != null && titleValue.getValue() != null) {
            setTitle(titleValue.getValue());
        } else {
            setTitle(title);
        }
    }

    private void setSutTitle() {
        String subTitle = mCallback.getSubTitle();
        if (subTitle != null) {
            setSubtitle(subTitle);
        }
    }

    private void setIcon() {
        String appIcon = mParams.getAppIcon();
        if (appIcon != null) {
            setIcon(appIcon);
        }
    }

    public abstract void createMenuPopup();

    public ActionBarCallback getCallBack() {
        return mCallback;
    }

    protected static void setMatchParent(View view) {
        view.setLayoutParams(new LayoutParams(LayoutParams.MATCH_PARENT,
                LayoutParams.MATCH_PARENT));
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       