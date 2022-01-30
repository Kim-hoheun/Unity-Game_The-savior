using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint; // 미리 이동시킬 객체
    public Animator anim;
    public LayerMask whatStopMovement; // 아 레이어로는 이동하지 않음
    public LayerMask Key;               // 키 레이어를 만들어서 box와 충돌하지 않고 캐릭터랑은 충돌함
    public LayerMask spineArea;
    public bool keyHave; // 플레이어가 키를 가지고 있는지를 판별
    SpriteRenderer spriteRenderer;
    public int moveScore;
    public GameManager gameManager;
    public int stageTalkIdStart; // 시작 할 때 필요한 대화 아이디
    public int stageTalkIdEnd; // 포탈에 도착했을 때 필요한 대화 아이디
    public bool endTalk; // 맵에 진입했을 때 대사인지 맵을 클리어했을 때 대사인지를 구분
    public bool talkWith;
    public AudioEvent AudioSound;
    AudioSource PlayerAudio;
    private void Awake()
    {
        movePoint.parent = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerAudio = GetComponent<AudioSource>();

    }
    public void Start() // 화면에 처음 대화씬을 띄워 주기 위함. 미리 띄우면 페이드 아웃보다 먼저 생성돼서 불편함
    {
        Invoke("PanelCreate", 0.5f);
        
    }
    public void PanelCreate()
    {
        gameManager.talkPanel.SetActive(true);
        if(gameManager.stage.name != "Last")
        {
            gameManager.activePointUi.SetActive(true);
        }
        
    }


    private void Update()
    {
        
        //-> 캐릭터 이동, 이동애니메이션을 구현한 부분 / 함수사용안함 이 부분에 전부 구현이 돼있음
        //   layer가 
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        // 빈객체를 먼저 이동시킨 뒤 Player가 따라 가는 방식
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // 빈객체와 Player객체의 거리가 서로 멀지 않을때만 이동이 가능(둘이 멀어지면 구현시 이상해짐)
        if (Vector3.Distance(transform.position,movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow)) && !gameManager.isAction) // 좌우 방향키 입력
            {
                // 캐릭터 뒤집어 주는 코드
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
                
                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .4f, Key))
                {
                    Debug.DrawRay(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * 1.0f, new Color(0, 1, 0));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 1.0f, LayerMask.GetMask("Key"));
                    KeyObject targetKey = hit.collider.gameObject.GetComponent<KeyObject>();
                    AudioSound.PlaySound("Key", PlayerAudio);
                    PlayerAudio.Play();
                    targetKey.obtainKey();
                    gameManager.HealthDown();


                }
                // 캐릭터 이동 코드
                else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .4f,whatStopMovement))// 플레이어가 이동할 공간에 0.2f크기의 원을 그리고 whatStopMovement 물체가 있다면 true를 리턴해서 실행안됌
                {
                    AudioSound.PlaySound("Move", PlayerAudio);
                    PlayerAudio.Play();
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);                                               // 이동할 공간에 아무것도 없다면 이동할 공간으로 좌표로 값을 더해 준다.
                    gameManager.HealthDown();
                    
                }
                // 캐릭터가 이동 할 수 없을 때 상호작용
                else 
                {
                    //입력받은 방향으로 레이저를 쏜다
                    // 플레이어가 whatStopMovement를 만나게 되면 그것이 어떤 물체인지 확인 한 뒤 각 물체마다 if문을 수행한다
                    Debug.DrawRay(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * 1.0f, new Color(0, 1, 0));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 1.0f, LayerMask.GetMask("StopMovement"));
                    // Box, Devil, Keybox, key 들과의 상호작용이있다
                    // 박스일 경우 입력한 방향키 쪽으로 밀 수 있다
                        if (hit.collider.tag == "Box")
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Box", PlayerAudio);
                            PlayerAudio.Play();
                        BoxObject targetBox = hit.collider.gameObject.GetComponent<BoxObject>();
                            targetBox.BoxMoveIs();
                            gameManager.HealthDown();
                        }
                        // 악마일 경우 밀 수 있고 악마는 이동불가능한 지역으로 밀면 부서진다
                        else if (hit.collider.tag == "Devil")
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Devil", PlayerAudio);
                            PlayerAudio.Play();
                        DevilObject targetDevil = hit.collider.gameObject.GetComponent<DevilObject>();
                            targetDevil.damagedAnimation();
                            targetDevil.DevilMoveIs();
                            gameManager.HealthDown();
                        }
                        // 열쇠상자의 경우 플레이어가 키를 가지고 있는지 확인 한 뒤 열쇠를 가지고 있다면 열쇠상자를 파괴한다
                        else if (hit.collider.tag == "KeyBox")
                        {
                            AudioSound.PlaySound("Key", PlayerAudio);
                            PlayerAudio.Play();
                        KeyBox targetKeyBox = hit.collider.gameObject.GetComponent<KeyBox>();
                            // 열쇠를 획득 했다면
                            if (keyHave)
                            {
                                // 상자를 연다
                                targetKeyBox.KeyBoxOpen();
                                gameManager.HealthDown();
                            }
                        }
                        // 열쇠의 경우 사용자의 열쇠획득 bool 변수를 true로 바꿔준다.
                        
                        else if (hit.collider.tag == "Heroine")
                        {
                            AudioSound.PlaySound("Clear", PlayerAudio);
                            PlayerAudio.Play();
                            gameManager.Action(stageTalkIdEnd, true, true);
                            endTalk = true;
                            talkWith = true;
                        }
                        else if (hit.collider.tag == "Potal")
                        {
                            AudioSound.PlaySound("Clear", PlayerAudio);
                            PlayerAudio.Play();
                        gameManager.Action(stageTalkIdEnd, true, false);
                            endTalk = true;
                        }
                        else if (hit.collider.tag == "Boss")
                        {
                            gameManager.Action(stageTalkIdEnd, false, true);
                            endTalk = true;
                            talkWith = true;
                        }




                }
                
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && !gameManager.isAction) // 상하 방향키 입력
            {
                
                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0f), .4f, Key))
                {

                    Debug.DrawRay(transform.position, new Vector3(0, Input.GetAxisRaw("Vertical"), 0) * 1.0f, new Color(0, 1, 0));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 1.0f, LayerMask.GetMask("Key"));
                    KeyObject targetKey = hit.collider.gameObject.GetComponent<KeyObject>();
                    AudioSound.PlaySound("Key", PlayerAudio);
                    PlayerAudio.Play();
                    targetKey.obtainKey();
                    gameManager.HealthDown();


                }
                // 이동 하는 코드
                else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .4f, whatStopMovement))// 플레이어가 이동할 공간에 0.2f크기의 원을 그리고 충동할 물체가 있다면 true를 리턴해서 실행안됌
                {
                    AudioSound.PlaySound("Move", PlayerAudio);
                    PlayerAudio.Play();
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);                                                // 이동할 공간에 아무것도 없다면 이동할 공간으로 좌표로 값을 더해 준다.
                    gameManager.HealthDown();
                    
                }
                 
                else
                {
                    //입력받은 방향으로 레이저를 쏜다
                    // 플레이어가 whatStopMovement를 만나게 되면 그것이 어떤 물체인지 확인 한 뒤 각 물체마다 if문을 수행한다
                    Debug.DrawRay(transform.position, new Vector3(0, Input.GetAxisRaw("Vertical"), 0) * 1.0f, new Color(0, 1, 0));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 1.0f, LayerMask.GetMask("StopMovement"));
                    // Box, Devil, Keybox, key 들과의 상호작용이있다
                        if (hit.collider.tag == "Box") // 박스일 경우 입력한 방향키 쪽으로 밀 수 있다
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Box", PlayerAudio);
                            PlayerAudio.Play();
                            BoxObject targetBox = hit.collider.gameObject.GetComponent<BoxObject>();
                            targetBox.BoxMoveIs();
                            gameManager.HealthDown();
                        }
                        else if (hit.collider.tag == "Devil") // 악마일 경우 밀 수 있고 악마는 이동불가능한 지역으로 밀면 부서진다
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Devil", PlayerAudio);
                            PlayerAudio.Play();
                        DevilObject targetDevil = hit.collider.gameObject.GetComponent<DevilObject>();
                            targetDevil.damagedAnimation();
                            targetDevil.DevilMoveIs();
                            gameManager.HealthDown();
                        }
                        else if (hit.collider.tag == "KeyBox") // 열쇠상자의 경우 플레이어가 키를 가지고 있는지 확인 한 뒤 열쇠를 가지고 있다면 열쇠상자를 파괴한다
                        {
                            AudioSound.PlaySound("Key", PlayerAudio);
                            PlayerAudio.Play();
                            KeyBox targetKeyBox = hit.collider.gameObject.GetComponent<KeyBox>();
                            if (keyHave) // 열쇠를 획득 했다면
                            {
                                targetKeyBox.KeyBoxOpen(); // 상자를 연다
                                gameManager.HealthDown();
                            }
                        }
                        
                        else if (hit.collider.tag == "Heroine")
                        {
                            AudioSound.PlaySound("Clear", PlayerAudio);
                            PlayerAudio.Play();
                            gameManager.Action(stageTalkIdEnd, true, true);
                            endTalk = true;
                            talkWith = true;
                    }
                        else if (hit.collider.tag == "Potal")
                        {
                            AudioSound.PlaySound("Clear", PlayerAudio);
                            PlayerAudio.Play();
                            gameManager.Action(stageTalkIdEnd, true, false);
                            endTalk = true;
                        }
                        else if (hit.collider.tag == "Boss")
                        {

                            gameManager.Action(stageTalkIdEnd, false, true);
                            endTalk = true;
                            talkWith = true;
                    }


                }
            }
            anim.SetBool("moving", false);
        }
        else
        {
            anim.SetBool("attacking", false);
            anim.SetBool("moving", true);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------22222222222222222222
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------22222222222222222222
        if (Input.GetKeyUp(KeyCode.Space) && gameManager.isAction)
        {
            if (talkWith)
            {
                if (endTalk)//히로인에게 말을 걸 때
                {
                    gameManager.Action(stageTalkIdEnd, true, true);
                }
            }
            else
            {
                if (endTalk)
                {// 포탈에게 말을 걸 때
                    gameManager.Action(stageTalkIdEnd, true,false);
                }
                else
                {
                    if (stageTalkIdStart == 987 || stageTalkIdStart == 985)// 8스테이지 에서 시작할 때만 대화형 대사 연출이라 따로 처리해줌
                    {
                        gameManager.Action(stageTalkIdStart, false, true);
                    }
                    else // 맵에 진입했을 때 나오는 대사
                    {
                        gameManager.Action(stageTalkIdStart, false, false);
                    }
                    
                }
            }
            
             
            
            



        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Spine")
        {
            AudioSound.PlaySound("Spine", PlayerAudio);
            PlayerAudio.Play();
            gameManager.HealthDown();
            
        }
        
    }
    void FixedUpdate()
    {
        

        
    }
        



        

        

        // -> 행동력 감소 되는 상황
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 한칸 이동할 때 마다 행동력이 줄어 듭니다.
        // 공격을 할 때마다 행동력이 줄어 듭니다.
        // 행동력이 전부 감소하면..UI가 출현해서 게임을 재시작 할 것인지 물어 봅니다.
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
