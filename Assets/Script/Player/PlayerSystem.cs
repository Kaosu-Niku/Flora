using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;
using Spine;
using Spine.Unity;

public class PlayerSystem : SkeletonAnimationSystem
{
    [SerializeField] SkeletonRootMotion skeletonRootMotion;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "JumpWallTrigger")
        {
            Jumping = true;
            return;
        }
        if (e.Data.Name == "JumpH1")
        {
            if (GetInput.Player.Jump.ReadValue<float>() == 0)
            {
                Rigid.gravityScale = 10;
                skeletonRootMotion.rootMotionScaleY = 1;
                skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
                return;
            }
        }
        if (e.Data.Name == "JumpH2")
        {
            if (GetInput.Player.Jump.ReadValue<float>() == 0)
            {
                Rigid.gravityScale = 10;
                skeletonRootMotion.rootMotionScaleY = 1;
                skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
                return;
            }
        }
        if (e.Data.Name == "JumpOut")
        {
            Rigid.gravityScale = 10;
            skeletonRootMotion.rootMotionScaleY = 1;//? 跳躍高度倍率
            skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
            return;
        }
        if (e.Data.Name == "JumpDownIn")
        {
            Super = false;
            CanControl = false;
            CanJump = true;
            Jumping = false;
            Rigid.gravityScale = 10;
            CanFlash = true;
            CanRestore = true;
            CanAttack = true;
            WhichAttack = 1;
            Attack[0].SetActive(false);
            Attack[1].SetActive(false);
            Attack[2].SetActive(false);
            return;
        }
        if (e.Data.Name == "JumpDownOut")
        {
            CanControl = true;
            return;
        }
        if (e.Data.Name == "FlashTrigger")//! 延長或縮短欠
        {
            Super = false;
            return;
        }
        if (e.Data.Name == "FlashOut")
        {
            CanFlash = true;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "HP+++Trigger")//! 快速欠
        {
            NowHp += 1;
            return;
        }
        if (e.Data.Name == "HP+++Out")
        {
            CanRestore = true;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "HurtOut")
        {
            Super = false;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "Attack1Open")
        {
            Attack[0].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack1Close")
        {
            Attack[0].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack1Can")
        {
            WhichAttack = 2;
            CanAttack = true;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "Attack1Not")
        {
            WhichAttack = 1;
            CanAttack = false;
            CanControl = false;
            return;
        }
        if (e.Data.Name == "Attack1Out")
        {
            CanAttack = true;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "Attack2Open")
        {
            Attack[1].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack2Close")
        {
            Attack[1].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack2Can")
        {
            WhichAttack = 3;
            CanAttack = true;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "Attack2Not")
        {
            WhichAttack = 1;
            CanAttack = false;
            CanControl = false;
            return;
        }
        if (e.Data.Name == "Attack2Out")
        {
            CanAttack = true;
            CanControl = true;
            return;
        }
        if (e.Data.Name == "Attack3Open")
        {
            Attack[2].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack3Close")
        {
            Attack[2].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack3Out")
        {
            WhichAttack = 1;
            CanAttack = true;
            CanControl = true;
            return;
        }
    }

    private PlayerSystem GetPlayer()
    {
        return this;
    }

    int _MaxHp;//* 最大血量
    public int MaxHp { get => _MaxHp; private set { if (value > 40) _MaxHp = 40; else _MaxHp = value; } }
    int _NowHp;//* 當前血量
    public int NowHp { get => _NowHp; private set { if (_NowHp > PlayerDataSO.MaxHp) _NowHp = PlayerDataSO.MaxHp; else _NowHp = value; UiSystem.ChangePlayerHpInvoke(); } }
    public void AddNowHp(int much)
    {
        NowHp += much;
    }
    int _MaxMp;//* 最大魔力
    public int MaxMp { get => _MaxMp; private set { _MaxMp = value; } }
    public void AddMaxMp(int much)
    {
        MaxMp += much;
    }
    int _NowMp;//* 當前魔力
    public int NowMp { get => _NowMp; private set { if (_NowMp > PlayerDataSO.MaxMp) _NowMp = PlayerDataSO.MaxMp; else if (NowMp > 0) _NowMp = value; else _NowMp = 0; UiSystem.ChangePlayerMpInvoke(); } }
    public void AddNowMp(int much)
    {
        NowMp += much;
        if (AddMpEvent != null)
            AddMpEvent.Invoke(much);
    }
    public UnityAction<int> AddMpEvent;//? 增加魔力事件 (魔力吸取技能訂閱)
    int _MaxAtk;//* 最大攻擊力
    public int MaxAtk { get => _MaxAtk; private set { _MaxAtk = value; } }
    int _NowAtk;//* 當前攻擊力
    public int NowAtk { get => _NowAtk; private set { if (_NowAtk > PlayerDataSO.MaxAtk) _NowAtk = PlayerDataSO.MaxAtk; _NowAtk = value; } }
    public void AddNowAtk(int much)
    {
        NowAtk += much;
    }
    int _MaxHit;//* 最大硬直力
    public int MaxHit { get => _MaxHit; private set { _MaxHit = value; } }
    int _NowHit;//* 當前硬直力
    public int NowHit { get => _NowHit; private set { if (_NowHit > PlayerDataSO.MaxHit) _NowHit = PlayerDataSO.MaxHit; _NowHit = value; } }
    public void AddNowHit(int much)
    {
        NowHit += much;
    }
    int _MaxSpeed;//* 最大速度
    public int MaxSpeed { get => _MaxSpeed; private set { _MaxSpeed = value; } }
    int _NowSpeed;//* 當前速度
    public int NowSpeed { get => _NowSpeed; private set { _NowSpeed = value; } }
    public void AddNowSpeed(int much)
    {
        NowSpeed += much;
    }
    public UnityAction<int> AddMoneyEvent;//? 增加金錢事件 (拜金技能訂閱)
    public void AddMoney(int much)
    {
        PlayerDataSO.Money += much;
        if (AddMoneyEvent != null)
            AddMoneyEvent.Invoke(much);
    }
    bool _CanControl;//* 玩家是否能控制
    public bool CanControl { get => _CanControl; private set { _CanControl = value; } }
    public void SetCanControl(bool value)
    {
        CanControl = value;
    }
    bool Super = false;//* 無敵狀態
    bool CanJump = true;//* 可以跳躍
    bool Jumping = false;//* 是否處於跳躍中(防止太容易一直觸發落地動畫)
    bool CanFlash = true;//* 可以閃避
    public void SetCanFlash(bool b)
    {
        CanFlash = b;
    }
    int _FlashTime;//* 閃避時間延長或結束
    public void SetFlashTime(bool b)
    {
        if (b == false)
            _FlashTime--;
        else
            _FlashTime++;
    }
    bool CanRestore = true;//* 可以恢復生命
    [HideInInspector] public bool FastRestore = true;//* 恢復生命的動作是否加快(配合某個技能)
    int WhichAttack = 1;
    bool CanAttack = true;//* 可以攻擊
    bool _CanFind = true;//* 怪物是否能找到玩家
    public bool CanFind { get => _CanFind; private set { _CanFind = value; } }
    public void SetCanFind(bool value)
    {
        CanFind = value;
    }
    Rigidbody2D Rigid;
    Collider2D Col;
    public CircleCollider2D SuckAwardCol;//* 吸取道具用的圓形碰撞體
    [SerializeField] GameObject PlayerHint;
    CinemachineImpulseSource MyImpulseSetting;
    MyInput GetInput;
    [SerializeField] List<GameObject> Attack = new List<GameObject>();

    new void Awake()
    {
        base.Awake();
        PlayerSystemSO.GetPlayerFunc += GetPlayer;
        //transform.position = new Vector3(GameDataSO.ResetPoint[0], GameDataSO.ResetPoint[1], 0);
        Rigid = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        PlayerHint.SetActive(false);
        MyImpulseSetting = GetComponent<CinemachineImpulseSource>();
        GetInput = new MyInput();
    }
    private void OnEnable()
    {
        //? Data數值初始化
        MaxHp = PlayerDataSO.MaxHp;
        MaxMp = PlayerDataSO.MaxMp;
        MaxAtk = PlayerDataSO.MaxAtk;
        MaxHit = PlayerDataSO.MaxHit;
        MaxSpeed = PlayerDataSO.MaxSpeed;
        GetInput.Enable();
        GetInput.Player.Jump.started += OnJump;
        GetInput.Player.Flash.started += OnFlash;
        GetInput.Player.Restore.started += OnRestore;
        GetInput.Player.Attack.started += OnAttack;
        StartCoroutine(LateTrigger());
    }
    private void OnDisable()
    {
        CanControl = false;
        PlayerSystemSO.GetPlayerFunc -= GetPlayer;
        GetInput.Disable();
        GetInput.Player.Jump.started -= OnJump;
        GetInput.Player.Flash.started -= OnFlash;
        GetInput.Player.Restore.started -= OnRestore;
        GetInput.Player.Attack.started -= OnAttack;
    }
    IEnumerator LateTrigger()
    {
        yield return 0;
        yield return 0;
        MaxHp = PlayerDataSO.MaxHp;
        NowHp = MaxHp;
        MaxMp = PlayerDataSO.MaxMp;
        NowMp = MaxMp;
        MaxAtk = PlayerDataSO.MaxAtk;
        NowAtk = MaxAtk;
        MaxHit = PlayerDataSO.MaxHit;
        NowHit = MaxHit;
        MaxSpeed = PlayerDataSO.MaxSpeed;
        NowSpeed = MaxSpeed;
        CanControl = true;
    }
    private void Update()
    {
        if (CanControl || Jumping)
        {
            if (GetInput.Player.Move.ReadValue<float>() != 0)
            {
                if (GetInput.Player.Move.ReadValue<float>() > 0)
                {
                    if (transform.eulerAngles.y != 0)
                        transform.rotation = Quaternion.identity;
                }
                else
                {
                    if (transform.eulerAngles.y != 180)
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                transform.Translate(PlayerDataSO.MaxSpeed * Time.deltaTime, 0, 0);
                if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name == "Idle" || skeletonAnimation.AnimationState.GetCurrent(0).IsComplete == true)
                    skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
            }
            else
            {
                if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name == "Walk" || skeletonAnimation.AnimationState.GetCurrent(0).IsComplete == true)
                    skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            }
        }
    }
    private void OnJump(InputAction.CallbackContext context)//? 跳躍
    {
        if (CanControl && CanJump)
        {
            CanJump = false;
            CanControl = false;
            Rigid.Sleep();
            Rigid.gravityScale = 0;
            if (PlayerHint.activeInHierarchy == false)//? 一般跳躍
            {
                Jumping = true;
                skeletonRootMotion.rootMotionScaleY = 3;
                skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
            }
            else//? 蹬牆跳
            {
                PlayerHint.gameObject.SetActive(false);
                transform.Rotate(0, 180, 0);
                Jumping = false;
                skeletonRootMotion.rootMotionScaleY = 3;
                Rigid.AddForce(transform.right * 500);
                skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
            }
        }
    }
    public void CallJump(int jumpPower)
    {
        CanJump = false;
        Jumping = true;
        CanControl = false;
        Rigid.Sleep();
        Rigid.gravityScale = 0;
        if (PlayerHint.activeInHierarchy == false)//? 一般跳躍
        {
            skeletonRootMotion.rootMotionScaleY = jumpPower;
            skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
        }
        else//? 蹬牆跳
        {
            PlayerHint.gameObject.SetActive(false);
            transform.Rotate(0, 180, 0);
            skeletonRootMotion.rootMotionScaleY = jumpPower;
            Rigid.AddForce(transform.right * 500);
            skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
        }
    }
    public UnityAction FlashEvent;//? 閃避事件
    private void OnFlash(InputAction.CallbackContext context)//? 閃避
    {
        if (CanControl && CanFlash)
        {
            //? (無形攻擊效果)
            if (FlashEvent != null)
                FlashEvent.Invoke();
            CanFlash = false;
            CanControl = false;
            Super = true;
            skeletonAnimation.AnimationState.SetAnimation(0, "Flash", false);
        }
    }
    //? 恢復生命(消耗固定10魔力回復血量)
    private void OnRestore(InputAction.CallbackContext context)
    {
        if (CanControl && CanRestore)
        {
            if (true)//NowMp > 9
            {
                NowMp -= 10;
                CanRestore = false;
                CanControl = false;
                Rigid.gravityScale = 10;
                if (FastRestore == false)
                    skeletonAnimation.AnimationState.SetAnimation(0, "HP+++", false);
                else
                    skeletonAnimation.AnimationState.SetAnimation(0, "FastHP+++", false);
            }
        }
    }
    //? 攻擊事件
    public UnityAction<PlayerAttack> AttackEvent;
    //? 攻擊事件觸發
    public void AttackTrigger(PlayerAttack p)
    {
        if (AttackEvent != null)
            AttackEvent.Invoke(p);
    }
    //? 攻擊成功擊中敵人事件
    public UnityAction<PlayerAttack> AttackHurtEnemyEvent;
    //? 攻擊成功擊中敵人事件觸發
    public void AttackHurtEnemyTrigger(PlayerAttack p)
    {
        if (AttackEvent != null)
            AttackEvent.Invoke(p);
    }
    //? 攻擊
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (CanControl && CanAttack)
        {
            CanAttack = false;
            CanControl = false;
            Rigid.gravityScale = 10;
            switch (WhichAttack)
            {
                case 1:
                    skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
                    break;
                case 2:
                    skeletonAnimation.AnimationState.SetAnimation(0, "Attack2", false);
                    break;
                case 3:
                    skeletonAnimation.AnimationState.SetAnimation(0, "Attack3", false);
                    break;
            }
        }
    }
    //? 受傷事件
    public UnityAction HurtEvent;
    //? 受傷
    public void Hurt(int damage)
    {
        if (Super == false)
        {
            NowHp -= damage;
            if (NowHp > 0)
            {
                Super = true;
                CanControl = false;
                CanJump = true;
                Jumping = false;
                Rigid.gravityScale = 10;
                CanFlash = true;
                CanRestore = true;
                CanAttack = true;
                WhichAttack = 1;
                Attack[0].SetActive(false);
                Attack[1].SetActive(false);
                Attack[2].SetActive(false);
                //? (減傷效果)(增傷負面效果)(傷害反彈效果)(根性效果)
                if (HurtEvent != null)
                    HurtEvent.Invoke();
                skeletonAnimation.AnimationState.SetAnimation(0, "Hurt", false);
                //MyImpulseSetting.GenerateImpulse();//? 鏡頭震動            
            }
            else
            {
                Die();
            }
        }
    }
    //? 死亡
    private void Die()
    {
        Super = true;
        CanControl = false;
        Jumping = false;
        Rigid.gravityScale = 10;
        Attack[0].SetActive(false);
        Attack[1].SetActive(false);
        Attack[2].SetActive(false);
        skeletonAnimation.AnimationState.SetAnimation(0, "Die", false);
        StopAllCoroutines();
    }
    private IEnumerator DontControl(float stopTime)
    {
        CanControl = false;
        yield return new WaitForSeconds(stopTime);
        CanControl = true;
    }
    public void CallDontControl(float stopTime)//? 停止控制玩家的外部接口
    {
        CanJump = true;
        StartCoroutine(DontControl(stopTime));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 contactsNormal = other.contacts[0].normal;//? 取得碰撞點的法線向量
        float colAngle = (Mathf.Atan(contactsNormal.y / contactsNormal.x)) * 180 / Mathf.PI;//? 換算成能理解的角度
        if (colAngle < 120 && colAngle > 60 || colAngle < -120 && colAngle > -60)//? 檢查角度判定是否是踩到地板
        {
            if (Jumping == true)
                skeletonAnimation.AnimationState.SetAnimation(0, "JumpDown", false);
        }
        if (colAngle < 10 && colAngle > -10)//? 檢查角度判定是否可以蹬牆跳
        {
            if (other.transform.CompareTag("Wall"))//? 接觸特定牆壁才可以蹬牆跳
            {
                if (other.contacts[0].point.x < other.transform.position.x)
                    transform.rotation = Quaternion.identity;
                else
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine(WallJumpIEnum());
            }
        }
    }
    private IEnumerator WallJumpIEnum()//? 吸附到可蹬牆跳的牆上，此時只能按跳躍鍵
    {
        CanJump = true;
        Jumping = false;
        CanControl = false;
        Rigid.Sleep();
        Rigid.gravityScale = 0.5f;
        PlayerHint.gameObject.SetActive(true);
        skeletonAnimation.AnimationState.SetAnimation(0, "WallHanging", true);
        while (true)
        {
            if (GetInput.Player.Jump.triggered)//? 蹬牆跳
            {
                if (CanControl == false)
                {
                    CanControl = true;
                    CallJump(3);
                }
                yield break;
            }
            yield return 0;
        }
    }
    public void CallWallJump()//? 蹬牆跳的外部接口
    {
        StartCoroutine(WallJumpIEnum());
    }
}
