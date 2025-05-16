using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        
        SceneType = Define.Scene.Login;

        List<GameObject> list = new();
        for (int i = 0; i < 5; i++)
            list.Add(Managers.Resource.Instantiate("UnityChan"));

        foreach (GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }
    }

    public override void Clear()
    {
        Debug.Log("Clear");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
}