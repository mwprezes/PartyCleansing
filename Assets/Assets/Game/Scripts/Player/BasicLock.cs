using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLock : MonoBehaviour {
    Lockpicking lockpick;

    public void Activate(GameObject who, StoringItems store)
    {
        lockpick.SendMessage("bound", store);
        lockpick.SendMessage("lockpick_result", 4);
    }

}
