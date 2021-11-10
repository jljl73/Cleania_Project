using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class IconData : MonoBehaviour
{
    public enum TYPE { Icon, HP, MP, Exp, }

    public TYPE type;
    [SerializeField]
    string value;

    StringBuilder sb = new StringBuilder();

    public string GetString()
    {
        switch (type)
        {
            case TYPE.Icon:
                return value;
            case TYPE.HP:
                return HPstring();
            case TYPE.MP:
                return MPstring();
            case TYPE.Exp:
                break;
        }

        return "";
    }

    string HPstring()
    {
        sb.Clear();
        sb.Append("<color=#FF7F50>");
        sb.Append("생명력: ");
        sb.Append(GameManager.Instance.PlayerAbility.HP);
        sb.Append("/");
        sb.Append(GameManager.Instance.PlayerAbility[Ability.Stat.MaxHP]);
        sb.Append("</color>\n");
        sb.Append("생명력이 모두 떨어지면 죽습니다.");

        return sb.ToString();
    }

    string MPstring()
    {
        sb.Clear();
        sb.Append("<color=#FF7F50>");
        sb.Append("청량감: ");
        sb.Append(GameManager.Instance.PlayerAbility.MP);
        sb.Append("/");
        sb.Append(GameManager.Instance.PlayerAbility[Ability.Stat.MaxMP]);
        sb.Append("</color>\n");
        sb.Append("블루가 청량감 생성 기술을 사용하거나 피해를 받으면 청량감이 생성됩니다.\n");
        sb.Append("청량감을 생성하여 더 강력한 청소 기술을 사용할 수 있습니다.\n");

        return sb.ToString();
    }
}
