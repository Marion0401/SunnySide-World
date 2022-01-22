//-----------------------------------------------------------------------------------------------------------------
// 
//	Mushroom Example
//	Created by : Luis Filipe (filipe@seines.pt)
//	Dec 2010
//
//	Source code in this example is in the public domain.
//  The naruto character model in this demo is copyrighted by Ben Mathis.
//  See Assets/Models/naruto.txt for more details
//
//-----------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;



public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	public static int nbMainPlayer;
	
	public List<GameObject> listPlayerPrefab;
	public List<GameObject> listOtherPlayerPrefab;
	
	public static GameObject[] arrayOtherPlayerGameObject = new GameObject[4];
	public static Vector3[] arrayOtherPlayerDestination = new Vector3[4];

	public List<GameObject> listOtherPlayerGameObject;
	public List<Vector3> listOtherPlayerDestination = new List<Vector3>();



	private GameObject mainPlayer;
	private Connection pioconnection;
	private List<Message> msgList = new List<Message>(); //  Messsage queue implementation
	private bool joinedroom = false;

	
	
	private string infomsg = "";

    private void Awake()
    {
		instance = this;
    }
    void Start()
	{
		Application.runInBackground = true;

		// Create a random userid 
		System.Random random = new System.Random();
		string userid = "Guest" + random.Next(0, 10000);

		Debug.Log("Starting");

		PlayerIO.Authenticate(
			"sunnyside-world-k8i01wxeo0i6btm9ugnrq",            //Your game id
			"public",                               //Your connection id
			new Dictionary<string, string> {        //Authentication arguments
				{ "userId", userid },
			},
			null,                                   //PlayerInsight segments
			delegate (Client client) {
				Debug.Log("Successfully connected to Player.IO");
				infomsg = "Successfully connected to Player.IO";


				Debug.Log("Create ServerEndpoint");
				// Comment out the line below to use the live servers instead of your development server
				client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);

				Debug.Log("CreateJoinRoom");
				//Create or join the room 
				client.Multiplayer.CreateJoinRoom(
					"UnityDemoRoom",                    //Room id. If set to null a random roomid is used
					"UnityMushrooms",                   //The room type started on the server
					true,                               //Should the room be visible in the lobby?
					null,
					null,
					delegate (Connection connection) {
						Debug.Log("Joined Room.");
						infomsg = "Joined Room.";
						// We successfully joined a room so set up the message handler
						pioconnection = connection;
						pioconnection.OnMessage += handlemessage;
						joinedroom = true;

					},
					delegate (PlayerIOError error) {
						Debug.Log("Error Joining Room: " + error.ToString());
						infomsg = error.ToString();
					}
				);
			},
			delegate (PlayerIOError error) {
				Debug.Log("Error connecting: " + error.ToString());
				infomsg = error.ToString();
			}
		);

	}

	void handlemessage(object sender, Message m)
	{
		msgList.Add(m);
	}

	void FixedUpdate()
	{
		// process message queue
		foreach (Message m in msgList)
		{
			switch (m.Type)
			{
                case "InstantiateOtherPlayer":


					
                    GameObject newPlayer=Instantiate(listOtherPlayerPrefab[m.GetInt(0)], new Vector3(m.GetFloat(1), m.GetFloat(2)), Quaternion.identity);
					//listOtherPlayerGameObject.Add(newPlayer);
					arrayOtherPlayerGameObject[m.GetInt(0)] = newPlayer;
					//listOtherPlayerDestination.Add(newPlayer.transform.position);
					arrayOtherPlayerDestination[m.GetInt(0)] = newPlayer.transform.position;
					break;
                    


                case "InitializePlayer":
					

					mainPlayer = Instantiate(listPlayerPrefab[m.GetInt(0)], new Vector3(0, 0, 0), Quaternion.identity);
					nbMainPlayer = m.GetInt(0);
					pioconnection.Send("OtherPlayer",m.GetString(1));
					

					break;

				case "PlayerJoined":
					GameObject newOtherPlayer=Instantiate(listOtherPlayerPrefab[m.GetInt(0)], new Vector3(m.GetFloat(1), m.GetFloat(2)), Quaternion.identity);
					//listOtherPlayerGameObject.Add(newPlayer);
					arrayOtherPlayerGameObject[m.GetInt(0)] = newOtherPlayer;
					//listOtherPlayerDestination.Add(newPlayer.transform.position);
					arrayOtherPlayerDestination[m.GetInt(0)] = newOtherPlayer.transform.position;
					break;


				case "TreeCut":
					Map.instance.cutTreeWithIndex(m.GetInt(0));
					break;

				case "PlayerIsCutting":
					Map.instance.playerCutTreeWithIndex(m.GetInt(0), m.GetInt(1));

					break;

				case "PlayerMoved":
					
					OtherPlayer.instance.ChangePositionOtherPlayer(m.GetInt(0), m.GetFloat(1), m.GetFloat(2));
					
					break;
				case "PlayerIsDigging":
					Map.instance.playerDigCarrotWithIndex(m.GetInt(0), m.GetInt(1));

					break;
				case "PlayerIsAttacking":
					Map.instance.playerAttackingSkeletonWithIndex(m.GetInt(0), m.GetInt(1));
					break;

				case "PlayerLeft":
					Debug.Log("playerleft");
					Destroy(listOtherPlayerGameObject[m.GetInt(0)]);
					break;
			}

		}

		// clear message queue after it's been processed
		msgList.Clear();
	}

	public void SendMessageTreeCut(int numberTree)
    {
		pioconnection.Send("IsCutting", numberTree);
    }

	public void SendMessageCarrot(int numberCarrotHole)
	{
		pioconnection.Send("IsDigging", numberCarrotHole);
	}

	public void SendMessageSkeletonAttacked(int numberSkeleton)
    {
		pioconnection.Send("IsAttacking", numberSkeleton);

	}

	public void SendNewPosition(float posX, float posY)
    {
		pioconnection.Send("NewPosition", posX, posY);
    }
	
	


}
