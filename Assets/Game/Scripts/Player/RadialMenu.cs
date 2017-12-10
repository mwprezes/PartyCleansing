using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour {

    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerCricle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    public int menuItems;
    public int currMenuItem;
    private int oldMenuItem;

    private GameObject player;
    private Traps trap;
    public GameObject tooltip;
    public GameObject actiontip;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            return;
        }

        trap = player.GetComponent<Traps>();
        menuItems = buttons.Count;
        foreach(MenuButton button in buttons)
        {
            button.ico.color = button.color;
        }
        currMenuItem = 0;
        oldMenuItem = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            GetCurrMenuItem();
            if (Input.GetButtonDown("Fire1"))
                ButtonAction();
        }
	}

    private void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("Player(Clone)");
            if (player != null)
            {
                trap = player.GetComponent<Traps>();
                menuItems = buttons.Count;
                foreach (MenuButton button in buttons)
                {
                    button.ico.color = button.color;
                }
                currMenuItem = 0;
                oldMenuItem = 0;
            }
        }
    }

    public void GetCurrMenuItem()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        float angle = (Mathf.Atan2(fromVector2M.y - centerCricle.y, fromVector2M.x - centerCricle.x) - Mathf.Atan2(toVector2M.y - centerCricle.y, toVector2M.x - centerCricle.x)) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;

        currMenuItem = (int)(angle / (360.0f / menuItems));

        if (currMenuItem != oldMenuItem)
        {
            buttons[oldMenuItem].ico.color = buttons[oldMenuItem].color;
            oldMenuItem = currMenuItem;
            buttons[currMenuItem].ico.color = buttons[currMenuItem].highlightColor;

            tooltip.SetActive(true);
            actiontip.SetActive(true);
            tooltip.GetComponent<Text>().text = buttons[currMenuItem].desc;
            actiontip.GetComponent<Text>().text = buttons[currMenuItem].actionPoints;
        }
    }

    public void ButtonAction()
    {
        buttons[currMenuItem].ico.color = buttons[currMenuItem].selectColor;
        switch (currMenuItem)
        {
            case 0:
                trap.trapType = "BasicLock"; break;
            case 1:
                trap.trapType = "Cake"; break;
        }

    }
}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image ico;
    public Color color = Color.white;
    public Color highlightColor = Color.gray;
    public Color selectColor = Color.green;

    public string desc;
    public string actionPoints;
}