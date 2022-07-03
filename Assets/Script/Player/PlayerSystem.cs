using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;
using Spine.Unity;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] float 遊戲運行速度調整;
    [SerializeField] GameObjectPoolSO GetGameObjectPool;
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
    CinemachineImpulseSource MyImpulseSetting;
    [SerializeField] Animator Anima;
    [SerializeField] GameObject OneAttack;
    GameObject WallJumpHint;
    MyInput GetInput;
    private void OnMove(InputAction.CallbackContext context)//? 左右移動
    {
        if (PlayerDataSO.CanControl)
            StartCoroutine(MoveIEnum(context));
    }
    private void OnJump(InputAction.CallbackContext context)//? 跳躍
    {
        if (PlayerDataSO.CanControl && CanJump)
            StartCoroutine(JumpIEnum());
    }
    private void OnDash(InputAction.CallbackContext context)//? 閃避
    {
        if (PlayerDataSO.CanControl && CanDash)
            StartCoroutine(DashIEnum());
    }
    private void OnRestore(InputAction.CallbackContext context)//? 恢復生命
    {
        if (PlayerDataSO.CanControl && CanRestore)
            StartCoroutine(RestoreIEnum());
    }
    private void OnOneAttack(InputAction.CallbackContext context)//? 攻擊
    {
        if (PlayerDataSO.CanControl && CanAttack)
            StartCoroutine(OneAttackIEnum());
    }
    private void Hurt(int damage)//? 受傷
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
                transform.Translate(PlayerDataSO.PlayerMaxSpeed * Time.deltaTime, 0, 0);
                yield return 0;
            }
            Anima.SetInteger("speed", 0);
            yield break;
        }
        else
        {
            while (context.ReadValue<float>() < 0)
            {
                transform.Translate(PlayerDataSO.PlayerMaxSpeed * Time.deltaTime, 0, 0);
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
            if (WallJumpHint.transform.eulerAngles.z > 90 && WallJumpHint.transform.eulerAngles.z < 270)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.identity;
            Rigid.AddForce(WallJumpHint.transform.rotation * Vector2.right * 2800);//? 根據指示箭頭方向決定彈出去的方向
            Anima.SetInteger("jump", 1);
            WallJumpHint.transform.Translate(0, 1000, 0);
            yield return new WaitForSeconds(0.25f);
            PlayerDataSO.CanControl = true;
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
        PlayerDataSO.CanControl = false;
        WallPos = transform.position;
        WallJumpHint.transform.position = transform.position + Vector3.up * 2.1f;
        if (canAll == false)
        {
            if (transform.eulerAngles.y > 90)
            {
                WallJumpHint.transform.rotation = Quaternion.Euler(0, 0, 45);
                dir = true;
            }
            else
            {
                WallJumpHint.transform.rotation = Quaternion.Euler(0, 0, 135);
                dir = false;
            }
        }
        Anima.SetTrigger("wall");
        StopCoroutine(JumpIEnum());
        while (true)//? 按右鍵一律順時針旋轉，左鍵則為逆時針旋轉
        {
            transform.position = WallPos;
            if (GetInput.Player.Move.ReadValue<float>() > 0)
                WallJumpHint.transform.Rotate(0, 0, -2);
            if (GetInput.Player.Move.ReadValue<float>() < 0)
                WallJumpHint.transform.Rotate(0, 0, 2);
            if (canAll == false)
            {
                if (dir == false)
                {
                    if (WallJumpHint.transform.eulerAngles.z < 100)
                        WallJumpHint.transform.rotation = Quaternion.Euler(0, 0, 100);
                    else if (WallJumpHint.transform.eulerAngles.z > 260)
                        WallJumpHint.transform.rotation = Quaternion.Euler(0, 0, 260);
                }
                else
                {
                    if (WallJumpHint.transform.eulerAngles.z > 80 && WallJumpHint.transform.eulerAngles.z < 180)
                        WallJumpHint.transform.rotation = Quaternion.Euler(0, 0, 80);
                    else if (WallJumpHint.transform.eulerAngles.z < 280 && WallJumpHint.transform.eulerAngles.z > 180)
                        WallJumpHint.transform.rotation = Quaternion.Euler(0, 0, 280);
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
        PlayerDataSO.Super = true;
        if (transform.eulerAngles.y == 0)
            Rigid.AddForce(Vector2.right * DashPower, ForceMode2D.Impulse);
        else
            Rigid.AddForce(Vector2.left * DashPower, ForceMode2D.Impulse);
        Anima.SetTrigger("dash");
        yield return new WaitForSeconds(DashTime);
        PlayerDataSO.Super = false;
        yield return new WaitForSeconds(1.5f - DashTime);
        CanDash = true;
        yield return 0;
    }
    private IEnumerator RestoreIEnum()//? 消耗固定10魔力恢復生命
    {

        if (PlayerDataSO.PlayerNowMp > 9)
        {
            PlayerDataSO.PlayerNowMp -= 10;
            CanRestore = false;
            PlayerDataSO.CanControl = false;
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
            PlayerDataSO.CanControl = true;
            PlayerDataSO.PlayerNowHp += 1;
            yield break;

        }
        else
            yield break;
    }
    private IEnumerator OneAttackIEnum()//? 一般攻擊
    {
        CanAttack = false;
        PlayerDataSO.CanControl = false;
        Instantiate(OneAttack, transform.position, transform.rotation);
        Anima.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);//? 0.5秒可以再攻擊
        CanAttack = true;
        PlayerDataSO.CanControl = true;
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
        if (PlayerDataSO.Super == false)
        {
            PlayerDataSO.PlayerNowHp -= damage;
            HurtTrigger();//? (減傷效果)(增傷負面效果)(傷害反彈效果)(根性效果)
            PlayerDataSO.Super = true;
            PlayerDataSO.CanControl = false;
            Anima.SetTrigger("hit");
            //MyImpulseSetting.GenerateImpulse();//? 鏡頭震動            
            yield return 0;
            if (PlayerDataSO.PlayerNowHp < 1)
                Die();
            yield return new WaitForSeconds(0.5f);
            PlayerDataSO.Super = false;
            PlayerDataSO.CanControl = true;
        }
        else
            yield break;
    }
    private IEnumerator DieIEnum()
    {
        PlayerDataSO.CanControl = false;
        Destroy(Col);
        Rigid.gravityScale = 0;
        Anima.SetTrigger("die");
        StopAllCoroutines();
        yield return 0;
    }
    private IEnumerator DontControl(float stopTime)
    {
        PlayerDataSO.CanControl = false;
        yield return new WaitForSeconds(stopTime);
        PlayerDataSO.CanControl = true;
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
    public static UnityAction DashEvent;//? 閃避事件
    private void DashTrigger()//? 閃避事件觸發
    {
        if (DashEvent != null)
            DashEvent.Invoke();
    }
    public static UnityAction<PlayerAttack> AttackEvent;//? 攻擊事件
    public static void AttackTrigger(PlayerAttack p)//? 攻擊事件觸發
    {
        if (AttackEvent != null)
            AttackEvent.Invoke(p);
    }
    public static UnityAction<PlayerAttack> AttackHurtEnemyEvent;//? 攻擊成功擊中敵人事件
    public static void AttackHurtEnemyTrigger(PlayerAttack p)//? 攻擊成功擊中敵人事件觸發
    {
        if (AttackEvent != null)
            AttackEvent.Invoke(p);
    }
    public static UnityAction HurtEvent;//? 受傷事件
    public void HurtTrigger()//? 受傷事件觸發
    {
        if (HurtEvent != null)
            HurtEvent.Invoke();
    }
    private void Awake()
    {
        //? Data數值初始化
        PlayerDataSO.PlayerMaxHp = 20;
        PlayerDataSO.PlayerMaxMp = 100;
        PlayerDataSO.PlayerMaxAtk = 10;
        PlayerDataSO.PlayerMaxHit = 10;
        PlayerDataSO.PlayerNowHp = PlayerDataSO.PlayerMaxHp;
        PlayerDataSO.PlayerNowMp = PlayerDataSO.PlayerMaxMp;
        PlayerDataSO.PlayerNowAtk = PlayerDataSO.PlayerMaxAtk;
        PlayerDataSO.PlayerNowHit = PlayerDataSO.PlayerMaxHit;
        PlayerDataSO.PlayerTrans = transform;
        //transform.position = new Vector3(GameDataSO.ResetPoint[0], GameDataSO.ResetPoint[1], 0);
        PlayerDataSO.CanControl = true;
        Rigid = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        MyImpulseSetting = GetComponent<CinemachineImpulseSource>();
        GetInput = new MyInput();
        WallJumpHint = GetGameObjectPool.GetGameObject(0, transform.position, Quaternion.identity);
        WallJumpHint.transform.Translate(0, 1000, 0);
        Time.timeScale = 遊戲運行速度調整;
    }
    private void OnEnable()
    {
        GetInput.Enable();
        GetInput.Player.Move.started += OnMove;
        GetInput.Player.Jump.started += OnJump;
        GetInput.Player.Dash.started += OnDash;
        GetInput.Player.Restore.started += OnRestore;
        GetInput.Player.Attack.started += OnOneAttack;
        GameRunSO.PlayerHurtEvent += Hurt;
    }
    private void OnDisable()
    {
        GetInput.Disable();
        GetInput.Player.Move.started -= OnMove;
        GetInput.Player.Jump.started -= OnJump;
        GetInput.Player.Dash.started -= OnDash;
        GetInput.Player.Restore.started -= OnRestore;
        GetInput.Player.Attack.started -= OnOneAttack;
        GameRunSO.PlayerHurtEvent -= Hurt;
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
}
