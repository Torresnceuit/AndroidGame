sults = serverresult.Split('#');
		if (results [0] == "undefined") 
		{
			opponentname = "No Name";
		}
		else
		{
			opponentname = results [0].ToString();
		}
		if (results [1] == "undefined")
			opponentscore = "0.00";
		else
			opponentscore = results [1].ToString ();
		if (results [2] == "undefined") 
		{
			realgold=0;
		}
		else
		{
			realgold=Int32.Parse(results[2]);
		}
		requestserver_ok=true;
	}

	public void LoadItem()
	{
		isuseditem_ok=true;
		string[] results = datafromserver.Split('#');
		if (results [0] == "undefined") 
		{
			realgold=0;
		}
		else
		{
			realgold=Int32.Parse(results[0]);
			//gold = Int32.Parse(results[0]);
		}
		if (results [1] == "undefined")
			numShieldItem = 0;
		else
			numShieldItem = Int32.Parse(results[1]);
		if (results [2] == "undefined")
			numJumpItem = 0;
		else
			numJumpItem = Int32.Parse(results[2]);
		if (results [3] == "undefined")
			numLifeItem = 0;
		else
			numLifeItem = Int32.Parse(results[3]);
		if (results [4] == "undefined")
			besttime = 0;
		else
			besttime = float.Parse(results[4]);
		if (results [5] == "undefined")
			challengelog = "";
		else 
		{
			challengelog = results [5].ToString ();
			if(challengelog.Contains("Done")||challengelog.Contains("New")
			   ||challengelog.Contains("Win")||challengelog.Contains("Close")
			   ||challengelog.Contains("Rejected"))
			{
				challengemode = true;
			}
		}
	}

	public void Reset()
	{
		gold=0;
		realgold=0;
		numShieldItem=0;
		numJumpItem=0;
		numLifeItem=0;
	}

	public void ChangeMode(bool isOfflineMode)
	{
		offlinemode = isOfflineMode;
	}

	public bool HasConnection()
	{
		try
		{
			using (var client = new WebClient())
				using (var stream = new WebClient().OpenRead("http://www.google.com"))
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
	}

	public void ChangeGold(int gold)
	{
		realgold -= gold;
	}
	
	/*public void SaveGame()
	{
		StreamWriter w;
		if(!f.Exists)
		{
			w = f.CreateText();    
		}
		else
		{
			f.Delete();
			w = f.CreateText();
		}
		w.WriteLine(realgold.ToString());
		w.WriteLine(realgold.ToString());
		w.WriteLine(numShieldItem.ToString());
		w.WriteLine(numJumpItem.ToString());
		w.WriteLine(numLifeItem.ToString());
		w.Close();
	}

	public void LoadGame()
	{
		//StreamReader r = File.OpenText(Application.dataPath + "\\" + "savedata.txt");
		StreamReader r = File.OpenText(Application.persistentDataPath + "\\" + "savedata.txt");
		realgold = IntParseFast (r.ReadLine ());
		gold = IntParseFast (r.ReadLine ());
		numShieldItem = IntParseFast (r.ReadLine ());
		numJumpItem = IntParseFast (r.ReadLine ());
		numLifeItem = IntParseFast (r.ReadLine ());
		//realgold = int.Parse(r.ReadLine ());
		//gold = int.Parse(r.ReadLine ());
		//numShieldItem = int.Parse(r.ReadLine ());
		//numJumpItem = int.Parse(r.ReadLine ());
		//numLifeItem = int.Parse(r.ReadLine ());
		r.Close();

	}

	public static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}*/
	
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         using UnityEngine;
using System.Collections;
using Facebook;

public class LoadingScreen : MonoBehaviour
{

	public GhostRunnerResources resources; //Use to check loading
	public GameState gamestate;
	public int percent =0;
	private bool checkingadsid_ok = false;
	private bool checkingloginfacebook_ok = false;
	private bool loadingdata_ok = false;
	public TextMesh percentText;
	public bool thereIsConnection = false;
	public bool checkinginternetisdone=false;
	public bool gamestarted = false;
	private float delay = 30f;
	private float timeForNextEvent=0f;
	// Use this for initialization
	void Start ()
	{
		percentText.text = "Checking internet connection....";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timeForNextEvent 