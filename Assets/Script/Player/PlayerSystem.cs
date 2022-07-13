using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;
using Spine.Unity;

public class PlayerSystem : MonoBehaviour
{
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
    public int NowMp { get => _NowMp; private set { if (_NowMp > PlayerDataSO.MaxMp) _NowMp = PlayerDataSO.MaxMp; else _NowMp = value; UiSystem.ChangePlayerMpInvoke(); } }
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
    bool _super = false;//* 無敵狀態
    public bool Super { get => _super; private set { _super = value; } }
    public void SetSuper(bool value)
    {
        Super = value;
    }
    bool _CanFind = true;//* 怪物是否能找到玩家
    public bool CanFind { get => _CanFind; private set { _CanFind = value; } }
    public void SetCanFind(bool value)
    {
        CanFind = value;
    }
    bool CanJump = true;//* 可以跳躍
    float JumpHigh;//* 跳躍高度檢查
    bool CanWallJump = false;//* 可以蹬牆跳
    Vector3 WallPos;//* 吸附牆上的位置固定    
    bool WallJumping;//* 是否正處於蹬牆跳的動作(配合玩家控制開關)
    public float DashTime = 0.75f;//* 閃避的無敵時間
    [HideInInspector] public bool CanDash = true;//* 可以閃避
    [SerializeField] float DashPower;//* 閃避力道
    bool CanRestore = true;//* 可以恢復生命
    [HideInInspector] public bool FastRestore = true;//* 恢復生命的動作是否加快(配合某個技能)
    bool CanAttack = true;//* 可以攻擊  
    Rigidbody2D Rigid;
    Collider2D Col;
    public CircleCollider2D SuckAwardCol;//* 吸取道具用的圓形碰撞體
    [SerializeField] GameObject PlayerHint;
    [SerializeField] DefaultObject PlayerAttack01;
    [SerializeField] Animator Anima;
    CinemachineImpulseSource MyImpulseSetting;
    MyInput GetInput;

    private void OnMove(InputAction.CallbackContext context)//? 左右移動
    {
        if (CanControl)
            StartCoroutine(MoveIEnum(context));
    }
    private void OnJump(InputAction.CallbackContext context)//? 跳躍
    {
        if (CanControl && CanJump)
            StartCoroutine(JumpIEnum());
    }
    private void OnDash(InputAction.CallbackContext context)//? 閃避
    {
        if (CanControl && CanDash)
            StartCoroutine(DashIEnum());
    }
    private void OnRestore(InputAction.CallbackContext context)//? 恢復生命
    {
        if (CanControl && CanRestore)
            StartCoroutine(RestoreIEnum());
    }
    private void OnOneAttack(InputAction.CallbackContext context)//? 攻擊
    {
        if (CanControl && CanAttack)
            StartCoroutine(OneAttackIEnum());
    }
    public void Hurt(int damage)//? 受傷
    {
        StartCoroutine(HurtIEnum(damage));
    }
    private void Die()//? 死亡
    {
        StartCoroutine(DieIEnum());
    }
    private IEnumerator MoveIEnum(InputAction.CallbackContext context)
    {
        bool dir;
        if (context.ReadValue<float>() < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            dir = false;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            dir = true;
        }
        Anima.SetInteger("speed", 1);
        if (dir == true)
        {
            while (context.ReadValue<float>() > 0)
            {
                transform.Translate(PlayerDataSO.MaxSpeed * Time.deltaTime, 0, 0);
                yield return 0;
            }
            Anima.SetInteger("speed", 0);
            yield break;
        }
        else
        {
            while (context.ReadValue<float>() < 0)
            {
                transform.Translate(PlayerDataSO.MaxSpeed * Time.deltaTime, 0, 0);
                yield return 0;
            }
            Anima.SetInteger("speed", 0);
            yield break;
        }
    }
    private IEnumerator JumpIEnum()
    {
        CanJump = false;
        if (CanWallJump == false)//? 一般跳躍
        {
            float pressTime = 0;
            Rigid.Sleep();
            Anima.SetInteger("jump", 1);
            while (GetInput.Player.Jump.ReadValue<float>() == 1)
            {
                pressTime += Time.deltaTime;
                if (pressTime > 0.2f)
                {
                    Rigid.AddForce(Vector2.up * 2800);
                    yield return 0;
                    while (transform.position.y >= JumpHigh)
                    {
                        JumpHigh = transform.position.y;
                        yield return 0;
                    }
                    JumpHigh = -1000;
                    Anima.SetInteger("jump", -1);
                    yield break;
                }
                yield return 0;
            }
            if (pressTime > 0.2f)
                Rigid.AddForce(Vector2.up * 2800);
            else if (pressTime > 0.1f)
                Rigid.AddForce(Vector2.up * 2200);
            else
                Rigid.AddForce(Vector2.up * 1500);
            yield return 0;
            while (transform.position.y >= JumpHigh)
            {
                JumpHigh = transform.position.y;
                yield return 0;
            }
            JumpHigh = -1000;
            Anima.SetInteger("jump", -1);
            yield break;
        }
        else//? 蹬牆跳
        {
            CanWallJump = false;
            WallJumping = true;
            Rigid.Sleep();
            yield return 0;
            if (PlayerHint.transform.eulerAngles.z > 90 && PlayerHint.transform.eulerAngles.z < 270)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.identity;
            Rigid.AddForce(PlayerHint.transform.rotation * Vector2.right * 2800);//? 根據指示箭頭方向決定彈出去的方向
            Anima.SetInteger("jump", 1);
            PlayerHint.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            CanControl = true;
            CanJump = true;
            while (transform.position.y >= JumpHigh)
            {
                JumpHigh = transform.position.y;
                yield return 0;
            }
            JumpHigh = -1000;
            Anima.SetInteger("jump", -1);
            yield break;
        }
    }
    private IEnumerator WallJumpIEnum(bool canAll)//? 吸附到可蹬牆跳的牆上，此時只能按跳躍鍵(需指定角色彈出方向，false為向左彈出，反之向右)
    {
        bool dir = false;
        CanWallJump = true;
        CanControl = false;

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
        Anima.SetTrigger("wall");
        StopCoroutine(JumpIEnum());
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
            if (GetInput.Player.Jump.triggered)//? 跳躍
            {
                StartCoroutine(JumpIEnum());
                yield break;
            }
            yield return 0;
        }
    }
    private IEnumerator DashIEnum()//? 閃避
    {
        DashTrigger();//? (無形攻擊效果)
        CanDash = false;
        Super = true;
        if (transform.eulerAngles.y == 0)
            Rigid.AddForce(Vector2.right * DashPower, ForceMode2D.Impulse);
        else
            Rigid.AddForce(Vector2.left * DashPower, ForceMode2D.Impulse);
        Anima.SetTrigger("dash");
        yield return new WaitForSeconds(DashTime);
        Super = false;
        yield return new WaitForSeconds(1.5f - DashTime);
        CanDash = true;
        yield return 0;
    }
    private IEnumerator RestoreIEnum()//? 消耗固定10魔力恢復生命
    {

        if (NowMp > 9)
        {
            NowMp -= 10;
            CanRestore = false;
            CanControl = false;
            if (FastRestore == false)
            {
                Anima.SetTrigger("restore");
                yield return new WaitForSeconds(1);
            }
            else
            {
                Anima.SetTrigger("fastRestore");
                yield return new WaitForSeconds(0.5f);
            }
            CanRestore = true;
            CanControl = true;
            NowHp += 1;
            yield break;

        }
        else
            yield break;
    }
    private IEnumerator OneAttackIEnum()//? 一般攻擊
    {
        CanAttack = false;
        CanControl = false;
        PlayerAttack01.gameObject.SetActive(true);
        PlayerAttack01.transform.position = transform.position + transform.right * 1.5f + Vector3.up * 2;
        Anima.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);//? 0.5秒可以再攻擊
        CanAttack = true;
        CanControl = true;
    }
    // IEnumerator ThreeAttackIEnum()//? 三連擊
    // {
    //     //? 第一擊
    //     CanAttack = false;
    //     CanControl = false;
    //     Instantiate(OneAttack, transform.position, transform.rotation);
    //     Anima.SetInteger("whichAttack", 1);
    //     Anima.SetTrigger("attack");
    //     yield return new WaitForSeconds(0.5f);//? 0.5秒後在1秒內可以使出第二擊
    //     CanControl = true;
    //     for (float x = 0; x < 1; x += Time.deltaTime)
    //     {
    //         if (GetInput.Player.Attack.triggered)//? 第二次攻擊
    //         {
    //             CanControl = false;
    //             Instantiate(OneAttack, transform.position, transform.rotation);
    //             Anima.SetInteger("whichAttack", 2);
    //             Anima.SetTrigger("attack");
    //             yield return new WaitForSeconds(0.5f);//? 0.5秒後在1秒內可以使出第三擊
    //             CanControl = true;
    //             for (float y = 0; y < 1; y += Time.deltaTime)
    //             {
    //                 if (GetInput.Player.Attack.triggered)//? 第三次攻擊
    //                 {
    //                     CanControl = false;
    //                     Instantiate(OneAttack, transform.position, transform.rotation);
    //                     Anima.SetInteger("whichAttack", 3);
    //                     Anima.SetTrigger("attack");
    //                     yield return new WaitForSeconds(1);//? 1秒後可以重新開始3連擊
    //                     CanControl = true;
    //                     CanAttack = true;
    //                     yield break;
    //                 }
    //                 yield return 0;
    //             }
    //             CanAttack = true;
    //             yield break;
    //         }
    //         yield return 0;
    //     }
    //     CanAttack = true;
    //     yield break;
    // }
    private IEnumerator HurtIEnum(int damage)
    {
        if (Super == false)
        {
            NowHp -= damage;
            //? (減傷效果)(增傷負面效果)(傷害反彈效果)(根性效果)
            if (HurtEvent != null)
                HurtEvent.Invoke();
            Super = true;
            CanControl = false;
            Anima.SetTrigger("hit");
            //MyImpulseSetting.GenerateImpulse();//? 鏡頭震動            
            yield return 0;
            if (NowHp < 1)
                Die();
            yield return new WaitForSeconds(0.5f);
            Super = false;
            CanControl = true;
        }
        else
            yield break;
    }
    private IEnumerator DieIEnum()
    {
        CanControl = false;
        Destroy(Col);
        Rigid.gravityScale = 0;
        Anima.SetTrigger("die");
        StopAllCoroutines();
        yield return 0;
    }
    private IEnumerator DontControl(float stopTime)
    {
        CanControl = false;
        yield return new WaitForSeconds(stopTime);
        CanControl = true;
    }
    public void PublicWallJump(bool canAll)//? 蹬牆跳的外部接口
    {
        CanJump = true;
        Anima.SetInteger("jump", 0);
        StartCoroutine(WallJumpIEnum(canAll));
    }
    public void PublicDontControl(float stopTime)//? 停止控制玩家的外部接口
    {
        CanJump = true;
        StartCoroutine(DontControl(stopTime));
    }
    public UnityAction DashEvent;//? 閃避事件
    private void DashTrigger()//? 閃避事件觸發
    {
        if (DashEvent != null)
            DashEvent.Invoke();
    }
    public UnityAction<PlayerAttack> AttackEvent;//? 攻擊事件
    public void AttackTrigger(PlayerAttack p)//? 攻擊事件觸發
    {
        if (AttackEvent != null)
            AttackEvent.Invoke(p);
    }
    public UnityAction<PlayerAttack> AttackHurtEnemyEvent;//? 攻擊成功擊中敵人事件
    public void AttackHurtEnemyTrigger(PlayerAttack p)//? 攻擊成功擊中敵人事件觸發
    {
        if (AttackEvent != null)
            AttackEvent.Invoke(p);
    }
    public UnityAction HurtEvent;//? 受傷事件
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
        GetInput.Player.Move.started += OnMove;
        GetInput.Player.Jump.started += OnJump;
        GetInput.Player.Dash.started += OnDash;
        GetInput.Player.Restore.started += OnRestore;
        GetInput.Player.Attack.started += OnOneAttack;
        StartCoroutine(LateTrigger());
    }
    private void OnDisable()
    {
        CanControl = false;
        PlayerSystemSO.GetPlayerFunc -= GetPlayer;
        GetInput.Disable();
        GetInput.Player.Move.started -= OnMove;
        GetInput.Player.Jump.started -= OnJump;
        GetInput.Player.Dash.started -= OnDash;
        GetInput.Player.Restore.started -= OnRestore;
        GetInput.Player.Attack.started -= OnOneAttack;

    }
    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 contactsNormal = other.contacts[0].normal;//? 取得碰撞點的法線向量
        float colAngle = (Mathf.Atan(contactsNormal.y / contactsNormal.x)) * 180 / Mathf.PI;//? 換算成能理解的角度
        if (WallJumping == true)//? 處於蹬牆跳時，接觸到任何牆壁或地板時解除
            WallJumping = false;
        Anima.SetInteger("jump", 0);
        if (colAngle < 105 && colAngle > 75)//? 檢查角度判定是否是踩到地板
        {
            CanJump = true;
        }
        if (colAngle < 10 && colAngle > -10)//? 檢查角度判定是否可以蹬牆跳
        {
            if (other.transform.CompareTag("Wall"))//? 接觸特定牆壁才可以蹬牆跳
            {
                CanJump = true;
                if (other.contacts[0].point.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                else
                    transform.rotation = Quaternion.identity;
                StartCoroutine(WallJumpIEnum(false));
            }
        }
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
}
