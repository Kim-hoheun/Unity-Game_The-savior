using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portrait;
    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portrait = new Dictionary<int, Sprite>();

        GenerateData();
    }

    void GenerateData()
    {

        //talkData.Add(, new string[] { ":", ":" });
        //Stage 1
        talkData.Add(1000, new string[] { "���䡦?:0", "��ݱ����� �ص� �ż����� �־��µ� ��Ե� ������?:2", "���ϴ� ���� ��� �����߰ڴ�.:0" });
        talkData.Add(999, new string[] { "�̰� ������?:2", "�ٸ� ���� �����ϱ� �ѹ� �� ����:1" });
        //Stage 2
        talkData.Add(998, new string[] { "������ ����� �����顦:3", "�̰��� ���� �ִ� ������ �ٸ� ������ �� ����.:4" });
        talkData.Add(997, new string[] { "����:5", "�� ������.:5" });
        //Stage 3
        talkData.Add(996, new string[] { "���ʿ� ����� ���δ�. ���� ���� �ɾ���߰ڴ�.:4" });
        
        // ���: �����ʻ�ȭ : ���� ������ : �����ʻ�ȭ: ���������� 
        talkData.Add(995, new string[] { "���⡦ �ϳ��� ���ڼ�.:5:1.0:1005:0.5", "�̰��� ������ �ƴ� �� ���Ҹ� Ȥ�� ���ư� ����� �� �� �ְڼ�?:6:1.0:1005:0.5",
                                        "����:5:0.5:1005:1.0","��Ȥ�� �����̼���?:5:0.5:1006:1.0","����� ���� �����̿�?:5:1.0:1005:0.5","�Ϲ����̶�� ���� ���ư���!:5:0.5:1007:1.0","���� ������ �ͼ�. ������ ó�� ���� ����. ���顦 ���� �ִ� ����� ������ �ٸ� ���� ��ȯ�� �� �����̴�.:5:1.0:1005:0.5",
                                            "�� �� ��Ȳ�� ������ �� �� �ְڼ�?:5:1.0:1005:0.5","���� �����ϴϱ� ����� ���� �̾߱� �ؿ�!:5:0.5:1007:1.0"});
        //talkData.Add(2000, new string[] { ":", ":" });
        //Stage 4
        talkData.Add(993, new string[] { "�ҳฦ ������.:7" });
        talkData.Add(994, new string[] { "���� �� �ֽÿ�.:6:1.0:1006:0.5","�� �� ������ �η��� �� ����� �ſ���. ������ �����ϰ� ������ ������� �׾���䡦.:6:0.5:1006:1.0", "�׳��� ��Ƴ��� �� ���� ������� ���� ��� �ޱ��� ������ �Ǿ����.:6:0.5:1006:1.0",
                                        "��¼�� �̸� ���� ���� �������� ��ȯ�� �� �ƴұ��?:6:0.5:1006:1.0","�������� ���Ѱ���?:6:0.5:1006:1.0","�ϴ��� ���ʿ��� '����'�̶�� Īȣ�� �ޱ� �ߴµ���, �� �����̶�� ���� ������ ������ ���ݼ�.:6:1.0:1006:0.5",
                                        "�����̸� �ְ� �ƴϿ���? ����, ���� ������ ���Űž�! :6:0.5:1007:1.0","��Ź�����, ������ ������ ���� �ּ��䡦.:6:0.5:1007:1.0",
                                            "��¼�顦 �׷��� �ؾ� ���ư� ����� ã�� �� �������� �𸣰ڱ�.:8:1.0:1007:0.5", "�װ��� �ִ� ������ �ȳ����ֽÿ�.:6:1.0:1007:0.5"});
        //Stage 5
        //talkData.Add(993, new string[] { "�ҳ�� ��⸦ ������ ���� ������ ���� Ǯ��..:7" });
        talkData.Add(991, new string[] { "������ ���� ���� �� Ǫ�ó׿�!:9:0.5:1010:1.0", "������ ��񸶴� �����س��� �����̿��� �����ϼ���!:9:0.5:1011:1.0", "���� ��¦�� �ñ������±���.:9:1.0:1011:0.5"});
        //Stage 6
        //talkData.Add(993, new string[] { "�ҳ�� ��⸦ ������ ���� ������ ���� Ǯ��..:7" });
        talkData.Add(989, new string[] { "���ε��� �̷��� �ս��� ��ġ��� ����� ó�� ����!:11:0.5:1012:1.0", "������ ������ �׷��ÿ�.:12:1.0:1012:0.5", "������ ���� 1�ҵ� �� �� ���̿�.:12:1.0:1012:0.5",
                                        "������ �ξ� ���ؿ�!:11:0.5:1012:1.0","�����Ǵ±���.:12:1.0:1012:0.5"});
        //Stage 7
        //talkData.Add(993, new string[] { "�ҳ�� ��⸦ ������ ���� ������ ���� Ǯ��..:7" });
        talkData.Add(988, new string[] { "���� �� �Ծ��!:12:0.5:1012:1.0", "���� ���� Ǯ�� ������ ���� �� �־��.:12:0.5:1012:1.0", "�ʹ� �������䡦.:12:0.5:1012:1.0", "�������ÿ�.:12:1.0:1012:0.5" });
        //Stage 8
        talkData.Add(987, new string[] { "�׳���..����!:13:1.0:1016:0.5", "���ʶ�..���� �ְ� ������ �հ�..!:13:0.5:1016:1.0"});
        talkData.Add(986, new string[] { "�̰����� �� �ΰ��� �������̱���.:14:0.5:1017:1.0", "�̰��� �ΰ����� �ʹ� ����. ��̰� ������.:14:0.5:1017:1.0", "Ư���� �������� �����ϴ� �ٸ� �ΰ������ �޶����� ���ڱ���.:14:0.5:1017:1.0", "��, �ʴ� ���Ѱ�?:14:0.5:1017:1.0",
                                        "!? ��.....:14:0.5:1017:1.0","���� ���� �Ǽ��� ����.:14:1.0:1017:0.5","��� �׷� ���� �տ� ��---:14:0.5:1017:1.0","�׾��.:14:1.0:1017:0.5"});
        //Last Stage
        talkData.Add(985, new string[] { "������ �س� �� �˾Ҿ��. �����մϴ١�.:15:0.5:1015:1.0", "���� �����մϴ١���. ��½.:15:0.5:1015:1.0","�׷���, ������ ���� ���ư� �� �ּ�?:15:1.0:1015:0.5", "����.:15:1.0:1015:1.0",
                                            "���� ���ะ �ҳడ ��� �ִµ� ���ư� ������ �ϴ°ſ���?!:15:0.5:1017:1.0", "����, �̾��Ͽ� �׷���,:16:1.0:1015:0.5", "��� ���ư��� �Ͽ�?:15:1.0:1015:1.0" });

        portrait.Add(1000 + 0, portraitArr[0]);
        portrait.Add(1000 + 1, portraitArr[1]);
        portrait.Add(1000 + 2, portraitArr[2]);
        portrait.Add(2000 + 0, portraitArr[3]);
        portrait.Add(2000 + 1, portraitArr[4]);
        portrait.Add(2000 + 2, portraitArr[5]);
        portrait.Add(2000 + 3, portraitArr[6]);



    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }

    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portrait[id + portraitIndex];
    }

}