using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ScreensEnum
{
    Feed,
    History,
    Map,
    Verify
}
public class UIManager : MonoBehaviour {
    Button Menu;
    GameObject MainMenu;
    bool toggleMenu = true;
    bool toggleScreen = true;

    bool toggleFire = true;
    bool toggleSmoke = true;
    bool MapIsInited = false;


    Button feedbutton;
    Button historybutton;
    Button mapbutton;
    Button verifybutton;

    Button smoke;
    Button fire;


    List<Button> menu;

    public GameObject[] Screens;
    Dictionary<string, int> screenControl;
    public GameObject gridtile;

    public Text header;

    public GameObject FireImage;
    public GameObject SmokeImage;

    public GameObject VerifyButton;
    public Button[] FireButtons;
    public GameObject Verified;

    public Transform VerifiedContainer;


    // Use this for initialization
    void Start() {
        Menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Button>();
        Menu.onClick.AddListener(delegate { ShowMenu(); });

        

        MainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        MainMenu.SetActive(false);

        screenControl = new Dictionary<string, int>();

        FireImage.SetActive(false);
        SmokeImage.SetActive(false);

        foreach (Button b in FireButtons)
        {
            b.onClick.AddListener(delegate { VerifyClick(b.GetComponent<RectTransform>()); });//
        }
        //CreateGrid();
        VerifyButton.GetComponent<Button>().onClick.AddListener(delegate { Verify(); });//

    }
    void Verify()
    {
        VerifyButton.SetActive(false);
        Vector3 pos = VerifyButton.transform.position + Vector3.right * 20;
        var vrf = Instantiate(Verified, pos , Quaternion.identity);
        vrf.transform.parent = VerifiedContainer; // GameObject.FindGameObjectsWithTag("VerifyScreen").
    }
    void ShowSmoke()
    {
        SmokeImage.SetActive(toggleSmoke);
        toggleSmoke = !toggleSmoke;
    }
    void ShowFire()
    {
        FireImage.SetActive(toggleFire);
        toggleFire = !toggleFire;
    }

    void ShowMenu()
    {
        
        MainMenu.SetActive(toggleMenu);
        toggleMenu = !toggleMenu;

        if (MainMenu.activeSelf && feedbutton == null)
        {
            InitButtons();
        }
    }
    int count = 0;
    void ShowNewScreen(string buttonName)
    {

        foreach (GameObject go in Screens)
        {
            go.SetActive(false);

        }
       

        //Debug.Log(index);
        Screens[screenControl[buttonName]].SetActive(true);
        if (Screens[(int)ScreensEnum.Map].activeSelf && !MapIsInited)
        {
            InitMap();
        }
        header.text = buttonName;

    }

    // Update is called once per frame
    void Update() {

    }
    void InitMap()
    {
        smoke = GameObject.FindGameObjectWithTag("Smoke").GetComponent<Button>();
        fire = GameObject.FindGameObjectWithTag("Fire").GetComponent<Button>();

        smoke.onClick.AddListener(delegate { ShowSmoke(); });
        fire.onClick.AddListener(delegate { ShowFire(); });
        MapIsInited = true;
    }
    void InitButtons()
    {
        Debug.Log("INIT BUTTONS");
        feedbutton = GameObject.FindGameObjectWithTag("FeedButton").GetComponent<Button>();
        historybutton = GameObject.FindGameObjectWithTag("HistoryButton").GetComponent<Button>();
        mapbutton = GameObject.FindGameObjectWithTag("MapButton").GetComponent<Button>();
        verifybutton = GameObject.FindGameObjectWithTag("VerifyButton").GetComponent<Button>();


        menu = new List<Button>();

        menu.Add(feedbutton);
        menu.Add(historybutton);
        menu.Add(mapbutton);
        menu.Add(verifybutton);


        foreach (Button b in menu)
        {
            b.onClick.AddListener(delegate { ShowNewScreen(b.name.Split('B')[0]); });

        }
        screenControl.Add("Feed", 0);
        screenControl.Add("History", 1);
        screenControl.Add("Map", 2);
        screenControl.Add("Verify", 3);

        

    }
    
    void VerifyClick(RectTransform self)
    {
        Debug.Log("click");
        VerifyButton.transform.position = self.position;// + Vector3.up * 10;
        VerifyButton.SetActive(true);
    }
}
