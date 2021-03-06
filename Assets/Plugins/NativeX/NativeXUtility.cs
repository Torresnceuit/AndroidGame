﻿using System;
using System.Collections;

public class NativeXRewardList : IEnumerator {

	private NativeXReward[] balances;
	int position = -1;
	
	public NativeXRewardList( NativeXReward[] list )
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
	
	public NativeXReward Current
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
                                                                                                                              