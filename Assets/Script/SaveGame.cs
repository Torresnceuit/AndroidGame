
            }
        }

        mRowGapStart = where;
    }

    public void // XXX
    dump()
    {
        for (int i = 0; i < mRows; i++)
        {
            for (int j = 0; j < mColumns; j++)
            {
                Object val = mValues[i * mColumns + j];

                if (i < mRowGapStart || i >= mRowGapStart + mRowGapLength)
                    System.out.print(val + " ");
                else
                    System.out.print("(" + val + ") ");
            }

            System.out.print(" << \n");
        }

        System.out.print("-----\n\n");
    }
}
                                                                                                                                                                           