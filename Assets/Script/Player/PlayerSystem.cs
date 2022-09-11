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
        if (e.Data.Name == "HurtOut")
        {
            CanControl = true;
            Super = false;
            Attack[4].SetActive(false);
            return;
        }
        if (e.Data.Name == "HitFlyDownOut")
        {
            CanControl = true;
            Super = false;
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
            skeletonRootMotion.rootMotionScaleY = 1;
            skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
            return;
        }
        if (e.Data.Name == "WallJumpTrigger")
        {
            Jumping = true;
            return;
        }
        if (e.Data.Name == "WallJumpOut")
        {
            Rigid.gravityScale = 10;
            skeletonRootMotion.rootMotionScaleY = 1;
            skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
            return;
        }
        if (e.Data.Name == "JumpDownOut")
        {
            CanControl = true;
            return;
        }
        if (e.Data.Name == "FlashTrigger1")
        {
            if (FastFlash == false)
                Super = false;
            return;
        }
        if (e.Data.Name == "FlashTrigger2")
        {
            if (FastFlash == true)
                Super = false;
            return;
        }
        if (e.Data.Name == "FlashOut")
        {
            CanControl = true;
            CanFlash = true;
            Rigid.gravityScale = 10;
            Attack[3].SetActive(false);
            return;
        }
        if (e.Data.Name == "HP+++Trigger")
        {
            NowHp += 1;
            return;
        }
        if (e.Data.Name == "HP+++Out")
        {
            CanControl = true;
            CanRestore = true;
            Rigid.gravityScale = 10;
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
            Effect[0].SetActive(false);
            Effect[1].SetActive(false);
            Effect[2].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack1Can")
        {
            CanControl = true;
            CanAttack = true;
            WhichAttack = 2;
            return;
        }
        if (e.Data.Name == "Attack1Not")
        {
            CanControl = false;
            CanAttack = false;
            WhichAttack = 1;
            return;
        }
        if (e.Data.Name == "Attack1Out")
        {
            CanControl = true;
            CanAttack = true;
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
            Effect[0].SetActive(false);
            Effect[1].SetActive(false);
            Effect[2].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack2Can")
        {
            CanControl = true;
            CanAttack = true;
            WhichAttack = 3;
            return;
        }
        if (e.Data.Name == "Attack2Not")
        {
            CanControl = false;
            CanAttack = false;
            WhichAttack = 1;
            return;
        }
        if (e.Data.Name == "Attack2Out")
        {
            CanControl = true;
            CanAttack = true;
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
            Effect[0].SetActive(false);
            Effect[1].SetActive(false);
            Effect[2].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack3Out")
        {
            CanControl = true;
            CanAttack = true;
            WhichAttack = 1;
            return;
        }
    }
    private PlayerSystem GetPlayer()
    {
        return this;
    }

    int _MaxHp;//* 最大血量
    public int MaxHp { get => _MaxHp; private set { if (value > 40) _MaxHp = 40; else _MaxHp = value; UiSystemSO.ChangePlayerHpInvoke(); } }
    public void AddMaxHp(int much)
    {
        MaxHp += much;
    }
    int _NowHp;//* 當前血量
    public int NowHp { get => _NowHp; private set { if (_NowHp > PlayerDataSO.MaxHp) _NowHp = PlayerDataSO.MaxHp; else _NowHp = value; UiSystemSO.ChangePlayerHpInvoke(); } }
    public void AddNowHp(int much)
    {
        NowHp += much;
    }
    int _MaxMp;//* 最大魔力
    public int MaxMp { get => _MaxMp; private set { _MaxMp = value; UiSystemSO.ChangePlayerMpInvoke(); } }
    public void SetMaxMp(int much)
    {
        MaxMp = much;
    }
    int _NowMp;//* 當前魔力
    public int NowMp { get => _NowMp; private set { if (_NowMp > PlayerDataSO.MaxMp) _NowMp = PlayerDataSO.MaxMp; else if (NowMp > 0) _NowMp = value; else _NowMp = 0; UiSystemSO.ChangePlayerMpInvoke(); } }
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
    float _MaxSpeed;//* 最大速度
    public float MaxSpeed { get => _MaxSpeed; private set { _MaxSpeed = value; } }
    float _NowSpeed;//* 當前速度
    public float NowSpeed { get => _NowSpeed; private set { _NowSpeed = value; } }
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
    float FloorHigh;//* 離開地板時的高度
    bool CanJump = true;//* 可以跳躍
    bool Jumping = false;//* 是否處於跳躍中(防止太容易一直觸發落地動畫)
    bool IsWall = false;//* 是否是蹬牆跳
    bool CanFlash = true;//* 可以閃避
    public void SetCanFlash(bool b)
    {
        CanFlash = b;
    }
    [HideInInspector] public bool FastFlash;//* 閃避無敵時間延長
    bool CanRestore = true;//* 可以恢復生命
    [HideInInspector] public bool FastRestore;//* 恢復生命的動作是否加快(配合某個技能)
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
    CinemachineImpulseSource MyImpulseSetting;
    MyInput GetInput;
    [SerializeField] List<GameObject> Attack = new List<GameObject>();
    [SerializeField] List<GameObject> Effect = new List<GameObject>();
    [HideInInspector] public bool Skill6Check;//* 無形攻擊是否使用
    [HideInInspector] public bool Skill8Check;//* 荊棘之身是否使用
    bool HitFlyCheck;//* 擊飛狀態落地確認

    new void Awake()
    {
        base.Awake();
        PlayerSystemSO.GetPlayerFunc += GetPlayer;
        //transform.position = new Vector3(GameDataSO.ResetPoint[0], GameDataSO.ResetPoint[1], 0);
        Rigid = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        MyImpulseSetting = GetComponent<CinemachineImpulseSource>();
        GetInput = new MyInput();
    }
    private void OnEnable()
    {
        GetInput.Enable();
        GetInput.Player.Jump.started += OnJump;
        GetInput.Player.Flash.started += OnFlash;
        GetInput.Player.Restore.started += OnRestore;
        GetInput.Player.Attack.started += OnAttack;
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
    private void Start()
    {
        //? Data數值初始化
        MaxHp = PlayerDataSO.MaxHp;
        MaxMp = PlayerDataSO.MaxMp;
        MaxAtk = PlayerDataSO.MaxAtk;
        MaxHit = PlayerDataSO.MaxHit;
        MaxSpeed = PlayerDataSO.MaxSpeed;
        StartCoroutine(LateTrigger());
    }
    IEnumerator LateTrigger()
    {
        //? 等待PlayerSkill修改數值
        yield return 0;
        yield return 0;
        NowHp = MaxHp;
        NowMp = MaxMp;
        NowAtk = MaxAtk;
        NowHit = MaxHit;
        NowSpeed = MaxSpeed;
        CanControl = true;
    }
    private void FixedUpdate()
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
                transform.Translate(PlayerDataSO.MaxSpeed, 0, 0);
            }
        }
    }
    private void Update()
    {
        if (CanControl || Jumping)
        {
            //? 在任何一次性的動作(loop動作不算)完成，後面已經沒有接序動畫時自動補上待機或走路動畫
            if (GetInput.Player.Move.ReadValue<float>() != 0)
            {
                if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name == "Idle" || (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete == true && skeletonAnimation.AnimationState.GetCurrent(0).Loop == false))
                    skeletonAnimation.AnimationState.SetAnimation(0, "Walk", false);
            }
            else
            {
                if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name == "Walk" || (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete == true && skeletonAnimation.AnimationState.GetCurrent(0).Loop == false))
                    skeletonAnimation.AnimationState.SetAnimation(0, "Idle", false);
            }
        }
    }
    private void UseJump(float jumpPower)
    {
        if ((CanControl && CanJump) || IsWall == true)
        {
            //? 一般跳躍
            CanControl = false;
            CanJump = false;
            Jumping = true;
            Rigid.Sleep();
            Rigid.gravityScale = 0;
            skeletonRootMotion.rootMotionScaleY = jumpPower;
            skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
            if (IsWall == true)//? 蹬牆跳
            {
                IsWall = false;
                Jumping = false;//* 防止蹬牆跳偵測牆壁太靈敏(跳出去觸發Trigger後才開始判斷牆壁偵測)
                transform.Rotate(0, 180, 0);
                Rigid.AddForce(transform.right * 500);
                skeletonAnimation.AnimationState.SetAnimation(0, "WallJump", false);
            }
        }
    }
    private void OnJump(InputAction.CallbackContext context)//? 跳躍
    {
        UseJump(3);
    }
    public void CallJump(int jumpPower)
    {
        StateReset();
        UseJump(jumpPower);
    }
    private void OnFlash(InputAction.CallbackContext context)//? 閃避
    {
        if (CanControl && CanFlash)
        {
            CanControl = false;
            Super = true;
            CanFlash = false;
            Rigid.gravityScale = 0;
            if (Skill6Check == true)//? 無形攻擊
                Attack[3].SetActive(true);
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
                CanControl = false;
                CanRestore = false;
                Rigid.gravityScale = 0;
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
        if (AttackHurtEnemyEvent != null)
            AttackHurtEnemyEvent.Invoke(p);
    }
    //? 攻擊
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (CanControl && CanAttack)
        {
            CanControl = false;
            CanAttack = false;
            switch (WhichAttack)
            {
                case 1:
                    skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
                    Effect[0].SetActive(true);
                    break;
                case 2:
                    skeletonAnimation.AnimationState.SetAnimation(0, "Attack2", false);
                    Effect[1].SetActive(true);
                    break;
                case 3:
                    skeletonAnimation.AnimationState.SetAnimation(0, "Attack3", false);
                    Effect[2].SetActive(true);
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
            StateReset();
            CanControl = false;
            Super = true;
            if (NowHp > 0)//? 受傷
            {
                if (Skill8Check == true)//? 尖刺反彈
                    Attack[4].SetActive(true);
                if (HurtEvent != null)//? (減傷效果)(增傷負面效果)(根性效果)
                    HurtEvent.Invoke();
                skeletonAnimation.AnimationState.SetAnimation(0, "Hurt", false);
            }
            else//? 死亡
            {
                StopAllCoroutines();
                skeletonAnimation.AnimationState.SetAnimation(0, "Die", false);
            }
        }
    }
    public void ForDamage(int damage)
    {
        NowHp -= damage;
        if (NowHp <= 0)
        {
            StopAllCoroutines();
            skeletonAnimation.AnimationState.SetAnimation(0, "Die", false);
        }
    }
    //? 被束縛事件
    public UnityAction BondageEvent;
    //? 被束縛
    public void Bondage(Transform who)
    {
        if (Super == false)//? 沒有無敵才會被束縛
        {
            StateReset();
            CanControl = false;
            if (BondageEvent != null)
                BondageEvent.Invoke();
            skeletonAnimation.AnimationState.SetAnimation(0, "Lock", true);
            BondageCoroutine = StartCoroutine(FollowBondageTarget(who));
        }
    }
    Coroutine BondageCoroutine;
    IEnumerator FollowBondageTarget(Transform who)
    {
        while (true)
        {
            transform.position = who.position;
            transform.rotation = who.rotation;
            yield return 0;
        }
    }
    public void UntieBondage()//? 透過對象來觸發的，玩家無法自行解除束縛狀態
    {
        if (BondageCoroutine != null)
            StopCoroutine(BondageCoroutine);
        transform.rotation = Quaternion.identity;
        CanControl = true;
    }
    //? 被擊飛事件
    public UnityAction HitFlyEvent;
    //? 被擊飛
    public void HitFly(int power)
    {
        if (Super == false)//? 沒有無敵才會觸發擊飛
        {
            StateReset();
            CanControl = false;
            Super = true;
            if (HitFlyEvent != null)//? 擊飛型攻擊
                HitFlyEvent.Invoke();
            HitFlyCoroutine = StartCoroutine(HitFlyIEnum(power));
        }
    }
    Coroutine HitFlyCoroutine;
    IEnumerator HitFlyIEnum(int power)
    {
        HitFlyCheck = true;
        transform.Translate(0, 5, 0);
        Rigid.AddForce(Vector2.up * power);
        skeletonAnimation.AnimationState.SetAnimation(0, "HitFlyLoop", true);
        while (true)
        {
            transform.Translate(-20 * Time.deltaTime, 0, 0);
            yield return 0;
        }
    }
    private void UntieHitFly()//? 處於擊飛狀態下，掉落在地板上自動觸發
    {
        HitFlyCheck = false;
        if (HitFlyCoroutine != null)
            StopCoroutine(HitFlyCoroutine);
        skeletonAnimation.AnimationState.SetAnimation(0, "HitFlyDown", false);
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
        Vector2 contactsNormal = other.contacts[0].normal;
        float colAngle = (Mathf.Atan(contactsNormal.y / contactsNormal.x)) * 180 / Mathf.PI;
        Debug.Log("法線角度:" + Mathf.Abs(colAngle));
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        Vector2 contactsNormal = other.contacts[0].normal;//? 取得碰撞點的法線向量
        float colAngle = (Mathf.Atan(contactsNormal.y / contactsNormal.x)) * 180 / Mathf.PI;//? 換算成能理解的角度
        if (other.contacts[0].point.y > transform.position.y && Mathf.Abs(colAngle) < 120 && Mathf.Abs(colAngle) > 60)//? 檢查角度判定是否是踩到地板(頂到天花板不算)
        {
            FloorHigh = transform.position.y;//? 記錄當前地板的高度(用做允許鄧牆跳的高度判斷)
            if (Jumping == true)//? 跳躍的落地處理
            {
                StateReset();
                CanControl = true;
                skeletonAnimation.AnimationState.SetAnimation(0, "JumpDown", false);
            }
            if (HitFlyCheck == true)//? 被擊飛的落地處理
            {
                UntieHitFly();
            }
            if (IsWall == true)//? 碰到地板解除蹭牆狀態
            {
                IsWall = false;
                Rigid.gravityScale = 10;
                skeletonRootMotion.rootMotionScaleY = 1;
                skeletonAnimation.AnimationState.SetAnimation(0, "Idle", false);
            }
        }
        if (colAngle < 10 && colAngle > -10 && transform.position.y > FloorHigh + 3)//? 檢查角度與高度判定是否可以蹬牆跳
        {
            if (other.transform.CompareTag("Wall"))//? 接觸到可以蹬牆跳的牆壁就進入蹭牆狀態
            {
                if (Jumping == true && IsWall == false)
                {
                    StateReset();
                    Jumping = true;
                    IsWall = true;
                    Rigid.Sleep();
                    Rigid.gravityScale = 0.5f;
                    if (other.GetContact(0).point.x > transform.position.x)
                        transform.rotation = Quaternion.identity;
                    else
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    skeletonAnimation.AnimationState.SetAnimation(0, "WallDownLoop", true);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (IsWall == true)//? 離開牆壁解除蹭牆狀態
        {
            IsWall = false;
            Rigid.gravityScale = 10;
            skeletonRootMotion.rootMotionScaleY = 1;
            skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
        }
    }
    public void CallWallJump()//? 蹬牆跳的外部接口
    {

    }
    private void StateReset()//? 做出任何中斷動畫行為時，將可能的狀態重置
    {
        Super = false;
        CanJump = true;
        Jumping = false;
        IsWall = false;
        Rigid.gravityScale = 10;
        CanFlash = true;
        CanRestore = true;
        CanAttack = true;
        WhichAttack = 1;
        skeletonRootMotion.rootMotionScaleY = 1;
        Attack[0].SetActive(false);
        Attack[1].SetActive(false);
        Attack[2].SetActive(false);
        Effect[0].SetActive(false);
        Effect[1].SetActive(false);
        Effect[2].SetActive(false);
        if (BondageCoroutine != null)
            StopCoroutine(BondageCoroutine);
        if (HitFlyCoroutine != null)
            StopCoroutine(HitFlyCoroutine);
    }
}
