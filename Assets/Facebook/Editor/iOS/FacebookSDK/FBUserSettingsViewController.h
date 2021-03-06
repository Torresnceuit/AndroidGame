nal page, in case the pages are in motion.
                    mPageListeningToSlider = mKeyguardWidgetPager.getNextPage();
                }
            }

            // View is on the move.  Pause the security view until it completes.
            mKeyguardSecurityContainer.onPause();
        }
        mLastScrollState = scrollState;
    }

    @Override
    public void onScrollPositionChanged(float scrollPosition, int challengeTop) {
        mChallengeTop = challengeTop;
        KeyguardWidgetFrame frame = mKeyguardWidgetPager.getWidgetPageAt(mPageListeningToSlider);
        if (frame != null && mLastScrollState != SlidingChallengeLayout.SCROLL_STATE_FADING) {
            frame.adjustFrame(getChallengeTopRelativeToFrame(frame, mChallengeTop));
        }
    }

    private Runnable mHideHintsRunnable = new Runnable() {
        @Override
        public void run() {
            if (mKeyguardWidgetPager != null) {
                mKeyguardWidgetPager.hideOutlinesAndSidePages();
            }
        }
    };

    public void showUsabilityHints() {
        mMainQueue.postDelayed( new Runnable() {
            @Override
            public void run() {
                mKeyguardSecurityContainer.showUsabilityHint();
            }
        } , SCREEN_ON_RING_HINT_DELAY);
        if (SHOW_INITIAL_PAGE_HINTS) {
            mKeyguardWidgetPager.showInitialPageHints();
        }
        if (mHideHintsRunnable != null) {
            mMainQueue.postDelayed(mHideHintsRunnable, SCREEN_ON_HINT_DURATION);
        }
    }

    // ChallengeLayout.OnBouncerStateChangedListener
    @Override
    public void onBouncerStateChanged(boolean bouncerActive) {
        if (bouncerActive) {
            mKeyguardWidgetPager.zoomOutToBouncer();
        } else {
            mKeyguardWidgetPager.zoomInFromBouncer();
            if (mKeyguardHostView != null) {
                mKeyguardHostView.setOnDismissAction(null);
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  /*
 * 