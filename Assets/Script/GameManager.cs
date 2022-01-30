using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    
    public int activePoint;//�÷��̾��� �ൿ���� ���� int ����
    public Player player;//�÷��̾� script�� ������ Player ����
    public int[] stageActivePoint; // �� �������� �� �ൿ�� ����Ʈ
    public Text talkText; // ó�� ������ ���
    //public GameObject scanObject; 
    public GameObject talkPanel; // ���â
    public GameObject activePointUi; // ����ȭ�� �ൿ�°� ������� ������ �����ִ� �κ�
    public GameObject subMenu; // ����޴�â
    public bool isAction; // ���â�� ���� ���� bool��
    public TalkManager talkManager; // ��ũ�Ŵ���
    public int talkIndex; // ��ũ ��縦 �ϳ��� �������� index
    public Image portraitImg1; // ��ȭâ���� ���� ĳ����
    public Image portraitImg2; // ��ȭâ���� ������ ĳ����
    public LevelLoader leveLoader; // �ƳѾ �� ȿ���� �����س�
    public Color portrait1; // ��ȭâ���� ĳ������ ������ �����ϱ� ���� ������
    public Color portrait2; // ��ȭâ���� ĳ������ ������ �����ϱ� ���� ������
    public GameObject stage; // ������� ������ �˱����� �����ͼ� stage.name�� �����
    public GameObject EndingScen; // ������ ��鿡�� �� �̶�� �ߴ� ����� Ȱ��ȭ �ϱ����� ��
    public Text ActivePoint; // �ൿ��
    public Text Stage; // ��������� ǥ��
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

        if (TalkWithIs) // �θ��̻��� ��ȭ�� �ϴ� ���� ������ �� ��
        {
            TalkWith(talkKey, nextIs);
            
            talkPanel.SetActive(isAction);
            
        }
        else
        {
            Talk(talkKey, nextIs); // ���� �����ϱ� ���� �Լ�
            talkPanel.SetActive(isAction);
            
        }
        

    }
    void Talk(int id, bool nextIs) // ���� �����ϱ� ���� �Լ�
    {                                                          
        
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        if(talkData == null) // ���̻� �ҷ��� ��簡 ���ٸ� null�� ��ȯ�ϰ� �ȴ�
        {
            if (nextIs) //true �� ���
            {
                
                isAction = false; // �ҷ��� ��簡 ���ٸ� false �༭ ��ȭâ�� ���� ĳ���� �̵��� �����ϰ� �ȴ�
                talkIndex = 0;   // ��ȭ�� ��������  index�� �ٽ� 0���� �����ؾ� �Ѵ�
                leveLoader.LoadNextLevel();
                return;         // ��ȭ �ý����� ������ ���� ����
            }
            else //false �� ���
            {
                isAction = false; // �ҷ��� ��簡 ���ٸ� false �༭ ��ȭâ�� ���� ĳ���� �̵��� �����ϰ� �ȴ�
                talkIndex = 0;   // ��ȭ�� ��������  index�� �ٽ� 0���� �����ؾ� �Ѵ�
                
                return;         // ��ȭ �ý����� ������ ���� ����
            }
            
            
        }
        //1�� ĳ����
        // Text�� ������ [0]
        talkText.text = talkData.Split(':')[0];
        // �ʻ�ȭ�� ������ [1]
        portraitImg1.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        // �������� ������
        isAction = true;
        talkIndex++;    
    }
    //--------------------
    void TalkWith(int id, bool nextIs)// �θ��̻��� ��ȭ�� �ϴ� ���� ������ �� ��
    {

        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null) // ���̻� �ҷ��� ��簡 ���ٸ� null�� ��ȯ�ϰ� �ȴ�
        {

            if (!nextIs)//false �� ���
            {
                if (stage.name == "Last")
                {
                    isAction = false; // �ҷ��� ��簡 ���ٸ� false �༭ ��ȭâ�� ���� ĳ���� �̵��� �����ϰ� �ȴ�
                    
                    talkIndex = 0;   // ��ȭ�� ��������  index�� �ٽ� 0���� �����ؾ� �Ѵ�
                    EndingScen.SetActive(true);
                    return;         // ��ȭ �ý����� ������ ���� ����
                }
                else
                {
                    isAction = false; // �ҷ��� ��簡 ���ٸ� false �༭ ��ȭâ�� ���� ĳ���� �̵��� �����ϰ� �ȴ�
                    
                    talkIndex = 0;   // ��ȭ�� ��������  index�� �ٽ� 0���� �����ؾ� �Ѵ�
                    
                    return;         // ��ȭ �ý����� ������ ���� ����
                }
                
            }
            else if (stage.name == "Stage8")
            {
                isAction = false; // �ҷ��� ��簡 ���ٸ� false �༭ ��ȭâ�� ���� ĳ���� �̵��� �����ϰ� �ȴ�
                
                talkIndex = 0;   // ��ȭ�� ��������  index�� �ٽ� 0���� �����ؾ� �Ѵ�
                leveLoader.LastNextLevel(); // ���� �״¾� ������ ������ ������ �Ѿ
                return;         // ��ȭ �ý����� ������ ���� ����
            }
            else //true �� ���
            {
                
                isAction = false; // �ҷ��� ��簡 ���ٸ� false �༭ ��ȭâ�� ���� ĳ���� �̵��� �����ϰ� �ȴ�
                
                talkIndex = 0;   // ��ȭ�� ��������  index�� �ٽ� 0���� �����ؾ� �Ѵ�
                leveLoader.LoadNextLevel();
                return;         // ��ȭ �ý����� ������ ���� ����
            }
            



        }
        
        
        // Text�� ������ [0]
        talkText.text = talkData.Split(':')[0];

        
        //1�� ĳ����
        // �ʻ�ȭ�� ������ [1]
        portraitImg1.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        // �������� ������
        portrait1.a = float.Parse(talkData.Split(':')[2]);
        portraitImg1.GetComponent<Image>().color = portrait1;

        //2�� ĳ����
        // �ʻ�ȭ�� ������ [1]
        
        portraitImg2.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[3]));
        // �������� ������
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
            // �÷��̾ ��ĭ �̵��� �� 1--;
            // �÷��̾ ���ÿ� �ɷ��� �� 1--;
            activePoint--;
            //Debug.Log(activePoint);
        }
        else
        {
            isAction = true;
            ActivePoint.text = activePoint.ToString();
            
            // �÷��̾� �״� ����Ʈ ����
            leveLoader.DieActive();
            // ���â UI

            // �絵�� UI

        }
    }

    



    //->�ൿ���� 0�� ���� �� 
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //�ൿ���� 0�� �ȴٸ� 
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

}

