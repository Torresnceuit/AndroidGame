using System.Collections;

public class NativeXReward {

	public NativeXReward() {
		Amount = 0;
		RewardName = "";
		RewardId = "";
		ReceiptId = "";
	}
	
	public double Amount { get; set; }
	public string RewardName { get; set; }
	public string RewardId { get; set; }
	public string ReceiptId { get; set; }
		
	public override string ToString ()
	{
		return "RewardName: " + RewardName + ", RewardId: " + RewardId + ", Amount: " + Amount.ToString();
	}

}
