using System;
using System.Collections;

[Obsolete("use NativeXReward instead")]
public class NativeXBalance {

	private bool isRedeemed;

	public NativeXBalance()
	{
		DisplayName = "";
		RewardType = "";
		ReceiptId = "";
		Message = "";
	}

	public string Message { get; set; }
	public double Amount { get; set; }
	public string DisplayName { get; set; }
	public string RewardType { get; set; }
	public string ReceiptId { get; set; }

	public override string ToString()
	{
		return  "DisplayName: "     + DisplayName + 
                ", RewardType: "    + RewardType + 
                ", Amount: "        + Amount;
	}

 
}
                                                                                                                                                                                                                                                                        