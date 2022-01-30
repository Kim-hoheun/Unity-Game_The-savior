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
        talkData.Add(1000, new string[] { "여긴…?:0", "방금까지만 해도 신선림에 있었는데 어떻게된 일이지?:2", "…일단 저쪽 길로 가봐야겠다.:0" });
        talkData.Add(999, new string[] { "이게 뭐지…?:2", "다른 길은 없으니깐 한번 들어가 보자:1" });
        //Stage 2
        talkData.Add(998, new string[] { "음산한 기운의 마물들…:3", "이곳은 내가 있던 무림과 다른 공간인 것 같군.:4" });
        talkData.Add(997, new string[] { "음…:5", "쭉 가보자.:5" });
        //Stage 3
        talkData.Add(996, new string[] { "저쪽에 사람이 보인다. 가서 말을 걸어봐야겠다.:4" });
        
        // 대사: 남주초상화 : 남주 투명도 : 여주초상화: 여주투명도 
        talkData.Add(995, new string[] { "저기… 하나만 묻겠소.:5:1.0:1005:0.5", "이곳은 무림이 아닌 것 같소만 혹시 돌아갈 방법을 알 수 있겠소?:6:1.0:1005:0.5",
                                        "……:5:0.5:1005:1.0","…혹시 용사님이세요?:5:0.5:1006:1.0","용사라니 무슨 말씀이오?:5:1.0:1005:0.5","일반인이라면 당장 돌아가요!:5:0.5:1007:1.0","나도 돌가고 싶소. 하지만 처음 보는 마물. 기운들… 내가 있던 세상과 완전히 다른 세상에 소환된 것 같소이다.:5:1.0:1005:0.5",
                                            "이 곳 상황을 설명해 줄 수 있겠소?:5:1.0:1005:0.5","여긴 위험하니까 저기로 가서 이야기 해요!:5:0.5:1007:1.0"});
        //talkData.Add(2000, new string[] { ":", ":" });
        //Stage 4
        talkData.Add(993, new string[] { "소녀를 따라가자.:7" });
        talkData.Add(994, new string[] { "설명 해 주시오.:6:1.0:1006:0.5","… 이 세계의 인류는 곧 멸망할 거에요. 마신이 강림하고 수많은 사람들이 죽었어요….:6:0.5:1006:1.0", "그나마 살아남은 저 같은 사람들은 숨어 살기 급급한 세상이 되었어요.:6:0.5:1006:1.0",
                                        "어쩌면 이를 막기 위해 아저씨가 소환된 건 아닐까요?:6:0.5:1006:1.0","아저씨는 강한가요?:6:0.5:1006:1.0","일단은 저쪽에서 '무신'이라는 칭호를 받긴 했는데…, 그 마신이라는 것의 전력을 소인은 모르잖소.:6:1.0:1006:0.5",
                                        "무신이면 최강 아니에요? 드디어, 드디어 용사님이 오신거야! :6:0.5:1007:1.0","부탁드려요, 아저씨 마신을 무찔러 주세요….:6:0.5:1007:1.0",
                                            "어쩌면… 그렇게 해야 돌아갈 방법을 찾을 수 있을지도 모르겠군.:8:1.0:1007:0.5", "그것이 있는 곳까지 안내해주시오.:6:1.0:1007:0.5"});
        //Stage 5
        //talkData.Add(993, new string[] { "소녀와 얘기를 나누기 전에 퍼즐을 먼저 풀자..:7" });
        talkData.Add(991, new string[] { "아저씨 퍼즐 무지 잘 푸시네요!:9:0.5:1010:1.0", "마신이 길목마다 설계해놓은 퍼즐이에요 조심하세요!:9:0.5:1011:1.0", "마신 낯짝이 궁굼해지는구려.:9:1.0:1011:0.5"});
        //Stage 6
        //talkData.Add(993, new string[] { "소녀와 얘기를 나누기 전에 퍼즐을 먼저 풀자..:7" });
        talkData.Add(989, new string[] { "마인들을 이렇게 손쉽게 해치우는 사람은 처음 봐요!:11:0.5:1012:1.0", "이정도 가지고 그러시오.:12:1.0:1012:0.5", "소인의 힘의 1할도 안 쓴 것이오.:12:1.0:1012:0.5",
                                        "마신은 훨씬 강해요!:11:0.5:1012:1.0","…기대되는구려.:12:1.0:1012:0.5"});
        //Stage 7
        //talkData.Add(993, new string[] { "소녀와 얘기를 나누기 전에 퍼즐을 먼저 풀자..:7" });
        talkData.Add(988, new string[] { "거의 다 왔어요!:12:0.5:1012:1.0", "다음 퍼즐만 풀면 마신을 만날 수 있어요.:12:0.5:1012:1.0", "너무 무서워요….:12:0.5:1012:1.0", "걱정마시오.:12:1.0:1012:0.5" });
        //Stage 8
        talkData.Add(987, new string[] { "네놈이..마신!:13:1.0:1016:0.5", "오너라..나의 최고 퍼즐을 뚫고..!:13:0.5:1016:1.0"});
        talkData.Add(986, new string[] { "이곳까지 온 인간은 오랜만이구나.:14:0.5:1017:1.0", "이곳의 인간들은 너무 약해. 재미가 없더군.:14:0.5:1017:1.0", "특이한 차림새를 보아하니 다른 인간들과는 달랐으면 좋겠구나.:14:0.5:1017:1.0", "자, 너는 강한가?:14:0.5:1017:1.0",
                                        "!? 컥.....:14:0.5:1017:1.0","말만 많고 실속은 없군.:14:1.0:1017:0.5","어떻게 그런 힘을 손에 넣---:14:0.5:1017:1.0","죽어라.:14:1.0:1017:0.5"});
        //Last Stage
        talkData.Add(985, new string[] { "아저씨 해낼 줄 알았어요. 감사합니다….:15:0.5:1015:1.0", "정말 감사합니다……. 훌쩍.:15:0.5:1015:1.0","그래서, 소인은 언제 돌아갈 수 있소?:15:1.0:1015:0.5", "…….:15:1.0:1015:1.0",
                                            "지금 가녀린 소녀가 울고 있는데 돌아갈 생각만 하는거에요?!:15:0.5:1017:1.0", "하하, 미안하오 그런데,:16:1.0:1015:0.5", "어떻게 돌아가야 하오?:15:1.0:1015:1.0" });

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
