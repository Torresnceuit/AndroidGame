using System;
using System.Collections;
using System.Collections.Generic;

public class NativeXRewardData : IEnumerable {

	private ArrayList balances; // Using a generic might be okay here, but we've encountered problems in Unity with generics
	
	public NativeXRewardData()
	{
		balances = new ArrayList();
	}
	
	IEnumerator IEnumerable.GetEnumerator()
	{
		return (IEnumerator)GetEnumerator();
	}
	
	public NativeXRewardList GetEnumerator()
	{
		var enumeratedBalances = new NativeXRewardList(( NativeXReward[]) balances.ToArray(typeof(NativeXReward)));
		balances.Clear();
		return enumeratedBalances;
	}
	
	pub