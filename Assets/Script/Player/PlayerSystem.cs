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
    [SerializeField] SkeletonAnimation SetSkeletonAnimation;
    protected override SkeletonAnimation skeletonAnimation { get => SetSkeletonAnimation; }
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "JumpUpOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "JumpLoop", true);
            return;
        }
        if (e.Data.Name == "JumpIn")
        {
            CanControl = false;
            return;
        }
        if (e.Data.Name == "JumpOut")
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
    public bool CanControl { get => _CanControl; private set { _CanControl = value; Debug.Log(_CanControl); } }
    public void SetCanControl(bool value)
    {
        CanControl = value;
    }
    bool Super = false;//* 無敵狀態
    bool CanJump = true;//* 可以跳躍
    Vector3 WallPos;//* 吸附牆上的位置固定
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

    private void Awake()
    {
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
        PlayerSystemSO.GetPlayerFunc += GetPlayer;
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
        if (CanControl == true)
        {
            float f = GetInput.Player.Move.ReadValue<float>();
            if (f != 0)
            {
                if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name != "Walk")
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
                }
                if (f > 0)
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
            }
            else
            {
                if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name != "Idle")
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
                }
            }
        }
    }
    private void OnJump(InputAction.CallbackContext context)//? 跳躍
    {
        if (CanControl && CanJump)
            StartCoroutine(JumpIEnum());
    }
    private IEnumerator JumpIEnum()
    {
        CanJump = false;
        if (PlayerHint.activeInHierarchy == false)//? 一般跳躍
        {
            float jumpTIme = 0;
            while (GetInput.Player.Jump.ReadValue<float>() > 0)
            {
                jumpTIme += Time.fixedDeltaTime;
                if (jumpTIme > 0.2f)
                    break;
                yield return new WaitForFixedUpdate();
            }
            if (jumpTIme > 0.2f)
                Rigid.AddForce(Vector2.up * 0);
            else if (jumpTIme > 0.1f)
                Rigid.AddForce(Vector2.up * 0);
            else
                Rigid.AddForce(Vector2.up * 0);
            skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
        }
        else//? 蹬牆跳
        {
            if (PlayerHint.transform.eulerAngles.z > 90 && PlayerHint.transform.eulerAngles.z < 270)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.identity;
            Rigid.AddForce(PlayerHint.transform.rotation * Vector2.right * 2800);//? 根據指示箭頭方向決定彈出去的方向
            skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
            PlayerHint.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            CanControl = true;
            CanJump = true;
        }
        yield break;
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
                CanFlash = true;
                CanRestore = true;
                CanAttack = true;
                //? (減傷效果)(增傷負面效果)(傷害反彈效果)(根性效果)
                if (HurtEvent != null)
                    HurtEvent.Invoke();
                Super = true;
                CanControl = false;
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
        CanControl = false;
        Destroy(Col);
        Rigid.gravityScale = 0;
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
        if (colAngle < 105 && colAngle > 75)//? 檢查角度判定是否是踩到地板
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "JumpDown", false);
            CanJump = true;
        }
        if (colAngle < 10 && colAngle > -10)//? 檢查角度判定是否可以蹬牆跳
        {
            if (other.transform.CompareTag("Wall"))//? 接觸特定牆壁才可以蹬牆跳
            {
                if (other.contacts[0].point.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                else
                    transform.rotation = Quaternion.identity;
                StartCoroutine(WallJumpIEnum(false));
            }
        }
    }
    private IEnumerator WallJumpIEnum(bool canAll)//? 吸附到可蹬牆跳的牆上，此時只能按跳躍鍵(需指定角色彈出方向，false為向左彈出，反之向右)
    {
        CanJump = true;
        CanControl = false;
        bool dir = false;
        if (canAll == false)
        {
            PlayerHint.gameObject.SetActive(true);
            if (transform.eulerAngles.y > 90)
            {
                PlayerHint.transform.rotation = Quaternion.Euler(0, 0, 45);
                dir = true;
            }
            else
            {
                PlayerHint.transform.rotation = Quaternion.Euler(0, 0, 135);
                dir = false;
            }
        }
        WallPos = transform.position;
        skeletonAnimation.AnimationState.SetAnimation(0, "WallHanging", true);
        while (true)//? 按右鍵一律順時針旋轉，左鍵則為逆時針旋轉
        {
            transform.position = WallPos;
            if (GetInput.Player.Move.ReadValue<float>() > 0)
                PlayerHint.transform.Rotate(0, 0, -2);
            if (GetInput.Player.Move.ReadValue<float>() < 0)
                PlayerHint.transform.Rotate(0, 0, 2);
            if (canAll == false)
            {
                if (dir == false)
                {
                    if (PlayerHint.transform.eulerAngles.z < 100)
                        PlayerHint.transform.rotation = Quaternion.Euler(0, 0, 100);
                    else if (PlayerHint.transform.eulerAngles.z > 260)
                        PlayerHint.transform.rotation = Quaternion.Euler(0, 0, 260);
                }
                else
                {
                    if (PlayerHint.transform.eulerAngles.z > 80 && PlayerHint.transform.eulerAngles.z < 180)
                        PlayerHint.transform.rotation = Quaternion.Euler(0, 0, 80);
                    else if (PlayerHint.transform.eulerAngles.z < 280 && PlayerHint.transform.eulerAngles.z > 180)
                        PlayerHint.transform.rotation = Quaternion.Euler(0, 0, 280);
                }
            }
            if (GetInput.Player.Jump.triggered)//? 蹬牆跳
            {
                StartCoroutine(JumpIEnum());
                yield break;
            }
            yield return 0;
        }
    }
    public void CallWallJump(bool canAll)//? 蹬牆跳的外部接口
    {
        CanJump = true;
        skeletonAnimation.AnimationState.SetAnimation(0, "JumpDown", false);
        StartCoroutine(WallJumpIEnum(canAll));
    }
}
