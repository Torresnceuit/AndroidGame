 super(c, attrs);

            final TypedArray a = c.obtainStyledAttributes(attrs,
                    R.styleable.SlidingChallengeLayout_Layout);
            childType = a.getInt(R.styleable.SlidingChallengeLayout_Layout_layout_childType,
                    CHILD_TYPE_NONE);
            maxHeight = a.getDimensionPixelSize(
                    R.styleable.SlidingChallengeLayout_Layout_layout_maxHeight, 0);
            a.recycle();
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      