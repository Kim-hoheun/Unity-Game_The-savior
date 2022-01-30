using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint; // �̸� �̵���ų ��ü
    public Animator anim;
    public LayerMask whatStopMovement; // �� ���̾�δ� �̵����� ����
    public LayerMask Key;               // Ű ���̾ ���� box�� �浹���� �ʰ� ĳ���Ͷ��� �浹��
    public LayerMask spineArea;
    public bool keyHave; // �÷��̾ Ű�� ������ �ִ����� �Ǻ�
    SpriteRenderer spriteRenderer;
    public int moveScore;
    public GameManager gameManager;
    public int stageTalkIdStart; // ���� �� �� �ʿ��� ��ȭ ���̵�
    public int stageTalkIdEnd; // ��Ż�� �������� �� �ʿ��� ��ȭ ���̵�
    public bool endTalk; // �ʿ� �������� �� ������� ���� Ŭ�������� �� ��������� ����
    public bool talkWith;
    public AudioEvent AudioSound;
    AudioSource PlayerAudio;
    private void Awake()
    {
        movePoint.parent = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerAudio = GetComponent<AudioSource>();

    }
    public void Start() // ȭ�鿡 ó�� ��ȭ���� ��� �ֱ� ����. �̸� ���� ���̵� �ƿ����� ���� �����ż� ������
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
        
        //-> ĳ���� �̵�, �̵��ִϸ��̼��� ������ �κ� / �Լ������� �� �κп� ���� ������ ������
        //   layer�� 
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------11111111111111111111
        // ��ü�� ���� �̵���Ų �� Player�� ���� ���� ���
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // ��ü�� Player��ü�� �Ÿ��� ���� ���� �������� �̵��� ����(���� �־����� ������ �̻�����)
        if (Vector3.Distance(transform.position,movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow)) && !gameManager.isAction) // �¿� ����Ű �Է�
            {
                // ĳ���� ������ �ִ� �ڵ�
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
                // ĳ���� �̵� �ڵ�
                else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .4f,whatStopMovement))// �÷��̾ �̵��� ������ 0.2fũ���� ���� �׸��� whatStopMovement ��ü�� �ִٸ� true�� �����ؼ� ����ȉ�
                {
                    AudioSound.PlaySound("Move", PlayerAudio);
                    PlayerAudio.Play();
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);                                               // �̵��� ������ �ƹ��͵� ���ٸ� �̵��� �������� ��ǥ�� ���� ���� �ش�.
                    gameManager.HealthDown();
                    
                }
                // ĳ���Ͱ� �̵� �� �� ���� �� ��ȣ�ۿ�
                else 
                {
                    //�Է¹��� �������� �������� ���
                    // �÷��̾ whatStopMovement�� ������ �Ǹ� �װ��� � ��ü���� Ȯ�� �� �� �� ��ü���� if���� �����Ѵ�
                    Debug.DrawRay(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * 1.0f, new Color(0, 1, 0));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 1.0f, LayerMask.GetMask("StopMovement"));
                    // Box, Devil, Keybox, key ����� ��ȣ�ۿ����ִ�
                    // �ڽ��� ��� �Է��� ����Ű ������ �� �� �ִ�
                        if (hit.collider.tag == "Box")
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Box", PlayerAudio);
                            PlayerAudio.Play();
                        BoxObject targetBox = hit.collider.gameObject.GetComponent<BoxObject>();
                            targetBox.BoxMoveIs();
                            gameManager.HealthDown();
                        }
                        // �Ǹ��� ��� �� �� �ְ� �Ǹ��� �̵��Ұ����� �������� �и� �μ�����
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
                        // ��������� ��� �÷��̾ Ű�� ������ �ִ��� Ȯ�� �� �� ���踦 ������ �ִٸ� ������ڸ� �ı��Ѵ�
                        else if (hit.collider.tag == "KeyBox")
                        {
                            AudioSound.PlaySound("Key", PlayerAudio);
                            PlayerAudio.Play();
                        KeyBox targetKeyBox = hit.collider.gameObject.GetComponent<KeyBox>();
                            // ���踦 ȹ�� �ߴٸ�
                            if (keyHave)
                            {
                                // ���ڸ� ����
                                targetKeyBox.KeyBoxOpen();
                                gameManager.HealthDown();
                            }
                        }
                        // ������ ��� ������� ����ȹ�� bool ������ true�� �ٲ��ش�.
                        
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
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && !gameManager.isAction) // ���� ����Ű �Է�
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
                // �̵� �ϴ� �ڵ�
                else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .4f, whatStopMovement))// �÷��̾ �̵��� ������ 0.2fũ���� ���� �׸��� �浿�� ��ü�� �ִٸ� true�� �����ؼ� ����ȉ�
                {
                    AudioSound.PlaySound("Move", PlayerAudio);
                    PlayerAudio.Play();
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);                                                // �̵��� ������ �ƹ��͵� ���ٸ� �̵��� �������� ��ǥ�� ���� ���� �ش�.
                    gameManager.HealthDown();
                    
                }
                 
                else
                {
                    //�Է¹��� �������� �������� ���
                    // �÷��̾ whatStopMovement�� ������ �Ǹ� �װ��� � ��ü���� Ȯ�� �� �� �� ��ü���� if���� �����Ѵ�
                    Debug.DrawRay(transform.position, new Vector3(0, Input.GetAxisRaw("Vertical"), 0) * 1.0f, new Color(0, 1, 0));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 1.0f, LayerMask.GetMask("StopMovement"));
                    // Box, Devil, Keybox, key ����� ��ȣ�ۿ����ִ�
                        if (hit.collider.tag == "Box") // �ڽ��� ��� �Է��� ����Ű ������ �� �� �ִ�
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Box", PlayerAudio);
                            PlayerAudio.Play();
                            BoxObject targetBox = hit.collider.gameObject.GetComponent<BoxObject>();
                            targetBox.BoxMoveIs();
                            gameManager.HealthDown();
                        }
                        else if (hit.collider.tag == "Devil") // �Ǹ��� ��� �� �� �ְ� �Ǹ��� �̵��Ұ����� �������� �и� �μ�����
                        {
                            anim.SetBool("attacking", true);
                            AudioSound.PlaySound("Devil", PlayerAudio);
                            PlayerAudio.Play();
                        DevilObject targetDevil = hit.collider.gameObject.GetComponent<DevilObject>();
                            targetDevil.damagedAnimation();
                            targetDevil.DevilMoveIs();
                            gameManager.HealthDown();
                        }
                        else if (hit.collider.tag == "KeyBox") // ��������� ��� �÷��̾ Ű�� ������ �ִ��� Ȯ�� �� �� ���踦 ������ �ִٸ� ������ڸ� �ı��Ѵ�
                        {
                            AudioSound.PlaySound("Key", PlayerAudio);
                            PlayerAudio.Play();
                            KeyBox targetKeyBox = hit.collider.gameObject.GetComponent<KeyBox>();
                            if (keyHave) // ���踦 ȹ�� �ߴٸ�
                            {
                                targetKeyBox.KeyBoxOpen(); // ���ڸ� ����
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
                if (endTalk)//�����ο��� ���� �� ��
                {
                    gameManager.Action(stageTalkIdEnd, true, true);
                }
            }
            else
            {
                if (endTalk)
                {// ��Ż���� ���� �� ��
                    gameManager.Action(stageTalkIdEnd, true,false);
                }
                else
                {
                    if (stageTalkIdStart == 987 || stageTalkIdStart == 985)// 8�������� ���� ������ ���� ��ȭ�� ��� �����̶� ���� ó������
                    {
                        gameManager.Action(stageTalkIdStart, false, true);
                    }
                    else // �ʿ� �������� �� ������ ���
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
        



        

        

        // -> �ൿ�� ���� �Ǵ� ��Ȳ
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // ��ĭ �̵��� �� ���� �ൿ���� �پ� ��ϴ�.
        // ������ �� ������ �ൿ���� �پ� ��ϴ�.
        // �ൿ���� ���� �����ϸ�..UI�� �����ؼ� ������ ����� �� ������ ���� ���ϴ�.
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
