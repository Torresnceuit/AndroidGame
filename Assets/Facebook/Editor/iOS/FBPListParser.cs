
            if (DEBUG) Log.d(TAG, "onKeyguardBouncerChanged(" + bouncer + ")");
            handleBouncerUserVisibilityChanged();
        }

        @Override
        public void onScreenTurnedOn() {
            if (DEBUG) Log.d(TAG, "onScreenTurnedOn()");
            handleBouncerUserVisibilityChanged();
        }

        @Override
        public void onScreenTurnedOff(int why) {
            if (DEBUG) Log.d(TAG, "onScreenTurnedOff()");
            handleBouncerUserVisibilityChanged();
        }

        @Override
        public void onEmergencyCallAction() {
            if (mBiometricUnlock != null) {
                mBiometricUnlock.stop();
            }
        }
    };

    @Override
    public void showUsabilityHint() {
    }

    @Override
    public void showBouncer(int duration) {
        KeyguardSecurityViewHelper.
                showBouncer(mSecurityMessageDisplay, mEcaView, mBouncerFrame, duration);
    }

    @Override
    public void hideBouncer(int duration) {
        KeyguardSecurityViewHelper.
                hideBouncer(mSecurityMessageDisplay, mEcaView, mBouncerFrame, duration);
    }

    @Override
    public void startAppearAnimation() {
        // TODO.
    }

    @Override
    public boolean startDisappearAnimation(Runnable finishRunnable) {
        return false;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ﻿using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.Facebo