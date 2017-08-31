     NSLog(@"JSON(inXCode): %s", [json UTF8String]);
        UnitySendMessage("NativeXHandler", "adInfo", [json UTF8String]);
    }

}

-(void)nativeXAdViewNoAdContent:(NativeXAdView *)adView
{
    NSLog(@"We were unable to load any content for Enhanced Ad.");
    UnitySendMessage("NativeXHandler", "didAdLoad", "NO_AD_LOADED");
    UnitySendMessage("NativeXHandler", "noAdLoaded", [adView.placement UTF8String]);
}

-(void)nativeXAdView:(NativeXAdView *)adView didFailWithError:(NSError *)error
{
    NSLog(@"Enhanced Ad failed to inititialize with Error: %@", error);
    if(adView.placement){
        UnitySendMessage("NativeXHandler", "actionFailed", [adView.placement UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "actionFailed", "NAME_UNDEFINED");
    }
}

-(void)nativeXAdView:(NativeXAdView *)adView willResizeToFrame:(CGRect)newFrame
{
    if(adView.placeme