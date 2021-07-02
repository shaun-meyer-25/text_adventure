using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class IController : MonoBehaviour
{
    public Text displayText;
    public string currentColor;
    public Choice[] actions;
    public LevelLoader levelLoader;
    public InteractableObject[] characters;
    public Image background;
    public ObserveChoice[] observableChoices;
    public AudioSource audio;
    public InteractChoice[] interactableChoices;
    public List<InteractableObject> travelingCompanions;
    public List<Room> allRoomsInGame;
    public Animator backgroundColor;
    public bool isDaytime;
    public float processingDelay;
    public Text northLabel;
    public Text eastLabel;
    public Text westLabel;
    public Text southLabel;
    public VolumeManipulation volumeManipulation;
    public GameObject fifthButton;
    public Animator useButtonAnimator;
    public Light2D light;
    
    [HideInInspector] public List<string> exitNames = new List<string>();
    [HideInInspector] public List<ExitChoice> exitChoices = new List<ExitChoice>();
    [HideInInspector] public bool isInteracting = false;
    [HideInInspector] public bool isObserving = false;
    [HideInInspector] public bool isUsing = false;
    [HideInInspector] public bool isConversing = false;
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public InteractableItems interactableItems;
    [HideInInspector] public List<string> actionLog = new List<string>();
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string> ();
    [HideInInspector] public Fire fire;
    [HideInInspector] public CheckpointManager checkpointManager;
    
    public Choice[] startingActions;

    public virtual void UpdateRoomChoices(Choice[] choices)
    {
        
    }
    
    public virtual List<string> ObservableChoiceNames()
    {
        return null;
    }

    public virtual void DisplayLoggedText()
    {
        
    }

    public virtual void LogStringWithReturn(string stringToAdd)
    {
    }

    public virtual void LoadRoomData()
    {
        
    }

    public virtual void LoadRoomDataAndDisplayRoomText()
    {

    }

    public virtual void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        
    }

    public virtual string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
    {
        return "";
    }

    public Dictionary<string, string> LoadDictionaryFromFile(string fileName)
    {
        TextAsset data = (TextAsset) Resources.Load(fileName);
        string[] lines = data.text.Split('\n');
		
        Dictionary<string, string> dict = new Dictionary<string, string>();

        foreach (string line in lines)
        {
            string[] split = line.Split('=');
            string key = split[0].Trim();
            string value = split[1].Trim();
            dict.Add(key, value);
        }
		
        return dict;
    }
	
    public Dictionary<string, List<string>> LoadDictionaryFromCsvFile(string fileName)
    {
        TextAsset data = (TextAsset) Resources.Load(fileName);
        string[] lines = data.text.Split('\n');
		
        Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        foreach (string line in lines)
        {
            string[] split = line.Split(',');
            string key = split[0].Trim();
            List<string> value = split.Skip(1).ToList();
            dict.Add(key, value);
        }
		
        return dict;
    }
    
    public void SetDaylight()
    {
        backgroundColor.SetTrigger("SetDaytime");
        currentColor = "black";
    }

    public void SetNighttime()
    {
        backgroundColor.SetTrigger("SetNighttime");
        currentColor = "white";
    }
    
    public void BearKillsYou()
    {
        SceneManager.LoadScene("BearKillsYou");
    }

}
