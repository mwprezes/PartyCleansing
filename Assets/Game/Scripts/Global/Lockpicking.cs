using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpicking : MonoBehaviour
{
    public int[] password;
    public int[] inserted_password;

    StoringItems store;
    public string message_on_text;
    private bool text_after;

    public bool activ;
    public int iter;
    public int max_liczb;
    public bool result_good;
    public bool end_this;

    void generate_passwod()
    {
        for (int j = 0; j < max_liczb; j++)
        {
            password[j] = Random.Range(1, 9);
            Debug.Log(password[j]);
        }
    }

    int pressed_button(int j, int k)
    {
        inserted_password[j] = k;
        if (text_after == true)
        {
            message_on_text = "";
            text_after = false;
        }
        message_on_text += k + " ";
        j++;
        return j;
    }

    bool compare_passwords()
    {
        int compare = 0;

        for (int j = 0; j < max_liczb; j++)
            if (password[j] == inserted_password[j]) compare++;
        if (compare == max_liczb)
        {
            message_on_text = "CONGRATS";
            activ = false;
            iter = 0;
            result_good = true;
            Destroy(store.GetComponent<BasicLock>());
            return true;
        }
        else
        {
            text_after = true;
            iter = 0;
            message_on_text = "Result is: " + compare + " of " + max_liczb + ". ( ";
            for (int j = 0; j < max_liczb; j++)
                if (j < max_liczb - 1) message_on_text += inserted_password[j] + "-";
                else message_on_text += inserted_password[j] + ")";
            return false;
        }
    }

    bool Exit_from_locpick()
    {
        activ = false;
        result_good = false;
        return false;
    }

    void Start()
    {
        activ = false;
        password = new int[7];
        inserted_password = new int[7];
    }

    public void bound (StoringItems receive)
    {
        store = receive;
    }

    public void lockpick_result(int k)
    {
        activ = true;
        max_liczb = k;
        iter = 0;
        generate_passwod();

    }

    void OnGUI()
    {
        if (activ)
        {
            GUI.Box(new Rect(200, 0, 500, 600), new GUIContent());
            if (GUI.Button(new Rect(260, 60, 100, 80), "1"))  iter = pressed_button(iter, 1);
            if (GUI.Button(new Rect(380, 60, 100, 80), "2"))  iter = pressed_button(iter, 2);
            if (GUI.Button(new Rect(500, 60, 100, 80), "3"))  iter = pressed_button(iter, 3);

            if (GUI.Button(new Rect(260, 160, 100, 80), "4")) iter = pressed_button(iter, 4);
            if (GUI.Button(new Rect(380, 160, 100, 80), "5")) iter = pressed_button(iter, 5);
            if (GUI.Button(new Rect(500, 160, 100, 80), "6")) iter = pressed_button(iter, 6);

            if (GUI.Button(new Rect(260, 260, 100, 80), "7")) iter = pressed_button(iter, 7);
            if (GUI.Button(new Rect(380, 260, 100, 80), "8")) iter = pressed_button(iter, 8);
            if (GUI.Button(new Rect(500, 260, 100, 80), "9")) iter = pressed_button(iter, 9);

            if (iter >= max_liczb) compare_passwords();

            if (GUI.Button(new Rect(620, 120, 80, 150), "EXIT")) Exit_from_locpick();

            GUI.Label(new Rect(440, 350, 300, 40), message_on_text);
        }
    }
}