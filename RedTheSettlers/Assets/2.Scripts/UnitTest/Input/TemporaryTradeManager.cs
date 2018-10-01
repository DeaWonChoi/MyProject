using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryTradeManager : Singleton<TemporaryTradeManager>
{
    private static TemporaryTradeManager tempTradeManager;

    private void Awake()
    {
        tempTradeManager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Trade(Vector3 position)
    {

    }
}
