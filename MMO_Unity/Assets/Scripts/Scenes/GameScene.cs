using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    // 코루틴
    // 1. 함수의 상태를 저장/복원 가능
    //      엄청 오래 걸리는 작업을 잠시 끊거나
    //      원하는 타이밍에 함수를 잠시 스탑했다가 복원하는 경우
    // 2. return 우리가 원하는 타입으로 가능 -> class도 가능

    protected override void Init()
    {
        base.Init();
        
        SceneType = Define.Scene.Game;
        
        //Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    public override void Clear()
    {
    }
}
