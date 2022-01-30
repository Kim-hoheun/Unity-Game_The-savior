using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    
    public int activePoint;//플레이어의 행동력을 담을 int 변수
    public Player player;//플레이어 script를 가져올 Player 변수
    public int[] stageActivePoint; // 각 스테이지 별 행동력 포인트
    public Text talkText; // 처음 나오는 대사
    //public GameObject scanObject; 
    public GameObject talkPanel; // 대사창
    public GameObject activePointUi; // 게임화면 행동력과 몇스테이지 인지를 보여주는 부분
    public GameObject subMenu; // 서브메뉴창
    public bool isAction; // 대사창을 띄우기 위한 bool값
    public TalkManager talkManager; // 토크매니저
    public int talkIndex; // 토크 대사를 하나씩 가져오는 index
    public Image portraitImg1; // 대화창에서 왼쪽 캐릭터
    public Image portraitImg2; // 대화창에서 오른쪽 캐릭터
    public LevelLoader leveLoader; // 컷넘어갈 때 효과를 구현해놈
    public Color portrait1; // 대화창에서 캐릭터의 투명도를 조절하기 위해 가져옴
    public Color portrait2; // 대화창에서 캐릭터의 투명도를 조절하기 위해 가져옴
    public GameObject stage; // 몇스테이지 인지를 알기위해 가져와서 stage.name을 사용함
    public GameObject EndingScen; // 마지막 장면에서 끝 이라고 뜨는 장면을 활성화 하기위해 씀
    public Text ActivePoint; // 행동력
    public Text Stage; // 몇스테인지를 표시
    public AudioEvent AudioSound;

    private void Awake()
    {
        talkText.text = "";
        if(stage.name!= "Last")
        {
            ActivePoint.text = activePoint.ToString();
            Stage.text = stage.name;
        }
        portrait1 = portraitImg1.GetComponent<Image>().color;
        portrait2 = portraitImg2.GetComponent<Image>().color;

    }
    private void Start()
    {
        AudioSound.PlaySound("GamePlay", gameObject.GetComponent<AudioSource>());
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (subMenu.activeSelf)
            {
                subMenu.SetActive(false);
            }
            else
            {
                subMenu.SetActive(true);
            }
            
        }
    }

    public void Action(int talkKey, bool nextIs, bool TalkWithIs)
    {

        if (TalkWithIs) // 두명이상이 대화를 하는 것을 연출할 때 씀
        {
            TalkWith(talkKey, nextIs);
            
            talkPanel.SetActive(isAction);
            
        }
        else
        {
            Talk(talkKey, nextIs); // 독백 연출하기 위한 함수
            talkPanel.SetActive(isAction);
            
        }
        

    }
    void Talk(int id, bool nextIs) // 독백 연출하기 위한 함수
    {                                                          
        
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        if(talkData == null) // 더이상 불러올 대사가 없다면 null을 반환하게 된다
        {
            if (nextIs) //true 일 경우
            {
                
                isAction = false; // 불러올 대사가 없다면 false 줘서 대화창을 끄고 캐릭터 이동도 가능하게 된다
                talkIndex = 0;   // 대화가 끝났으면  index를 다시 0부터 시작해야 한다
                leveLoader.LoadNextLevel();
                return;         // 대화 시스템을 끝내기 위해 리턴
            }
            else //false 일 경우
            {
                isAction = false; // 불러올 대사가 없다면 false 줘서 대화창을 끄고 캐릭터 이동도 가능하게 된다
                talkIndex = 0;   // 대화가 끝났으면  index를 다시 0부터 시작해야 한다
                
                return;         // 대화 시스템을 끝내기 위해 리턴
            }
            
            
        }
        //1번 캐릭터
        // Text를 가져옴 [0]
        talkText.text = talkData.Split(':')[0];
        // 초상화를 가져옴 [1]
        portraitImg1.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        // 불투명도를 가져옴
        isAction = true;
        talkIndex++;    
    }
    //--------------------
    void TalkWith(int id, bool nextIs)// 두명이상이 대화를 하는 것을 연출할 때 씀
    {

        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null) // 더이상 불러올 대사가 없다면 null을 반환하게 된다
        {

            if (!nextIs)//false 일 경우
            {
                if (stage.name == "Last")
                {
                    isAction = false; // 불러올 대사가 없다면 false 줘서 대화창을 끄고 캐릭터 이동도 가능하게 된다
                    
                    talkIndex = 0;   // 대화가 끝났으면  index를 다시 0부터 시작해야 한다
                    EndingScen.SetActive(true);
                    return;         // 대화 시스템을 끝내기 위해 리턴
                }
                else
                {
                    isAction = false; // 불러올 대사가 없다면 false 줘서 대화창을 끄고 캐릭터 이동도 가능하게 된다
                    
                    talkIndex = 0;   // 대화가 끝났으면  index를 다시 0부터 시작해야 한다
                    
                    return;         // 대화 시스템을 끝내기 위해 리턴
                }
                
            }
            else if (stage.name == "Stage8")
            {
                isAction = false; // 불러올 대사가 없다면 false 줘서 대화창을 끄고 캐릭터 이동도 가능하게 된다
                
                talkIndex = 0;   // 대화가 끝났으면  index를 다시 0부터 시작해야 한다
                leveLoader.LastNextLevel(); // 보스 죽는씬 연출후 마지막 씬으로 넘어감
                return;         // 대화 시스템을 끝내기 위해 리턴
            }
            else //true 일 경우
            {
                
                isAction = false; // 불러올 대사가 없다면 false 줘서 대화창을 끄고 캐릭터 이동도 가능하게 된다
                
                talkIndex = 0;   // 대화가 끝났으면  index를 다시 0부터 시작해야 한다
                leveLoader.LoadNextLevel();
                return;         // 대화 시스템을 끝내기 위해 리턴
            }
            



        }
        
        
        // Text를 가져옴 [0]
        talkText.text = talkData.Split(':')[0];

        
        //1번 캐릭터
        // 초상화를 가져옴 [1]
        portraitImg1.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        // 불투명도를 가져옴
        portrait1.a = float.Parse(talkData.Split(':')[2]);
        portraitImg1.GetComponent<Image>().color = portrait1;

        //2번 캐릭터
        // 초상화를 가져옴 [1]
        
        portraitImg2.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[3]));
        // 불투명도를 가져옴
        portrait2.a = float.Parse(talkData.Split(':')[4]);
        portraitImg2.GetComponent<Image>().color = portrait2;


        isAction = true;
        talkIndex++;

    }

    
    public void HealthDown()
    {
        if (activePoint > 0)
        {
            ActivePoint.text = activePoint.ToString(); 
            // 플레이어가 한칸 이동할 때 1--;
            // 플레이어가 가시에 걸렸을 때 1--;
            activePoint--;
            //Debug.Log(activePoint);
        }
        else
        {
            isAction = true;
            ActivePoint.text = activePoint.ToString();
            
            // 플레이어 죽는 이펙트 실행
            leveLoader.DieActive();
            // 결과창 UI

            // 재도전 UI

        }
    }

    



    //->행동력이 0이 됐을 때 
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //행동력이 0이 된다면 
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

}

