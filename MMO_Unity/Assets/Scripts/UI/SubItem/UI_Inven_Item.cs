using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText
    }

    private string _name;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        
        Get<GameObject>((int)GameObjects.ItemNameText)
            .GetComponent<Text>().text = _name;
        Get<GameObject>((int)GameObjects.ItemIcon)
            .BindEvent((data => { Debug.Log($"아이템 클릭 {_name}"); }));
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
