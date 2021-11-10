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
        sb.Append("�����: ");
        sb.Append(GameManager.Instance.PlayerAbility.HP);
        sb.Append("/");
        sb.Append(GameManager.Instance.PlayerAbility[Ability.Stat.MaxHP]);
        sb.Append("</color>\n");
        sb.Append("������� ��� �������� �׽��ϴ�.");

        return sb.ToString();
    }

    string MPstring()
    {
        sb.Clear();
        sb.Append("<color=#FF7F50>");
        sb.Append("û����: ");
        sb.Append(GameManager.Instance.PlayerAbility.MP);
        sb.Append("/");
        sb.Append(GameManager.Instance.PlayerAbility[Ability.Stat.MaxMP]);
        sb.Append("</color>\n");
        sb.Append("��簡 û���� ���� ����� ����ϰų� ���ظ� ������ û������ �����˴ϴ�.\n");
        sb.Append("û������ �����Ͽ� �� ������ û�� ����� ����� �� �ֽ��ϴ�.\n");

        return sb.ToString();
    }
}
