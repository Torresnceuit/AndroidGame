using System;
using System.Collections;
//using System.Collections.Generic;

[Obsolete("use NativeXRewardList instead", false)]
public class NativeXBalances : IEnumerator
{
    private NativeXBalance[] balances;
    int position = -1;

    public NativeXBalances( NativeXBalance[] list )
    {
        balances = list;
    }

    public bool MoveNext()
    {
        position++;
        return (position < balances.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public NativeXBalance Current
    {
        get
        {
            try
            {
                return balances[position];
            }
            catch ( IndexOutOfRangeException )
            {
                throw new InvalidOperationException();
            }
        }
    }
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                