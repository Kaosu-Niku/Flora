using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{

    [SerializeField] protected float MaxHp;
    protected float Hp;
    [SerializeField] protected float MaxHitRecover;
    protected float HitRecover;//* 硬直累積量

    [SerializeField] float MaxDis;//* 最大範圍限制
    protected float LeftDis;//* 最左邊
    protected float RightDis;//* 最右邊
    protected float GetPlayerDistance()//? 立即取得與玩家的距離
    {
        return Mathf.Abs(transform.position.x - PlayerSystemSO.GetPlayerInvoke().transform.position.x);
    }
    protected bool Super;//* 是否無敵
    private bool _IsFight;//* 是否進入戰鬥
    protected bool IsFight { get => _IsFight; set { _IsFight = value; } }
    [SerializeField] protected float DropMoney;//* 死亡掉落多少錢
    [SerializeField] protected float DropMp;//* 死亡掉落多少魔力  
    protected Rigidbody2D Rigid;
    [SerializeField] protected Animator Anima;
    //* 其他組件
    protected PoolObject EnemyHpSlider;//* 血條物件
    [SerializeField] Vector3 HpSliderMove;//* 血條初始化移動
    [HideInInspector] protected Transform HpSlider;//* 真正的血條

    Coroutine CurrentAction;//? 敵人正在執行的當前行動
    private void ChangeAction(IEnumerator nextAction)
    {
        //? 要切換每一個動作行動時只能透過調用該方法來達成切換行動
        //? 會立即停止正在執行的行動並執行參數指定的行動
        if (CurrentAction != null)
            StopCoroutine(CurrentAction);
        CurrentAction = StartCoroutine(nextAction);
    }
    private void OnAction()//? 重置行動
    {
        if (IsFight == true && PlayerSystemSO.GetPlayerInvoke().CanFind == true)
            ChangeAction(DefaultAction());
        else
            ChangeAction(IdleAction());
    }
    protected abstract IEnumerator CustomIdle();//? 自定義閒置行動
    private IEnumerator IdleAction()//? 沒有與玩家互動就迴圈該行為
    {
        if (EnemyHpSlider != null)
        {
            EnemyHpSlider.gameObject.SetActive(false);
        }
        yield return StartCoroutine(CustomIdle());
        OnAction();
    }
    protected abstract IEnumerator CustomDefault();//? 自定義起始行動
    private IEnumerator DefaultAction()//? 與玩家互動就迴圈該行為
    {
        if (EnemyHpSlider != null)
        {
            EnemyHpSlider.gameObject.SetActive(true);
        }
        yield return StartCoroutine(CustomDefault());
        OnAction();
    }

    public void Hurt(float damage, float hitValue)//? 受傷
    {
        //? 先確認是否有無敵
        if (Super == false)
        {
            Hp -= damage;
            HpSlider.localScale = new Vector3(Hp / MaxHp, 1, 0);
            HitRecover -= hitValue;
            if (Hp <= 0)
            {
                ChangeAction(DieAction());
                return;
            }
            if (HitRecover > 0)//? 無造成硬直
            {
                //? 受傷行為並不會中斷行動，所以不調用 ChangeAction()
                StartCoroutine(HurtAction());
            }
            else
            {
                HitRecover = MaxHitRecover;
                ChangeAction(HitRecoverAction());
            }
        }
    }
    protected abstract IEnumerator CustomHurt();//? 自定義受傷行為
    private IEnumerator HurtAction()
    {
        Super = true;
        yield return StartCoroutine(CustomHurt());//? 等待自定義受傷行動完畢
        Super = false;
    }

    protected abstract IEnumerator CustomHitRecover();//? 自定義硬直行為
    private IEnumerator HitRecoverAction()
    {
        Super = true;
        yield return StartCoroutine(CustomHitRecover());//? 等待自定義硬直行動完畢
        Super = false;
        OnAction();//? 重新開始執行行動
    }
    protected abstract IEnumerator CustomDie();//? 自定義死亡行為
    private IEnumerator DieAction()
    {
        Super = true;
        EnemyHpSlider.transform.localScale = Vector3.zero;
        while (DropMoney > 1000)
        {
            GameObjectPoolSO.GetObject("BigMoney", transform.position, Quaternion.identity);
            DropMoney -= 1000;
        }
        while (DropMoney > 100)
        {
            GameObjectPoolSO.GetObject("MiddleMoney", transform.position, Quaternion.identity);
            DropMoney -= 100;
        }
        while (DropMoney > 10)
        {
            GameObjectPoolSO.GetObject("SmallMoney", transform.position, Quaternion.identity);
            DropMoney -= 10;
        }
        yield return StartCoroutine(CustomDie());//? 等待自定義死亡行動完畢
    }
    private void Awake()
    {
        LeftDis = transform.position.x - MaxDis / 2;
        RightDis = transform.position.x + MaxDis / 2;
        Hp = MaxHp;
        HitRecover = MaxHitRecover;
        Super = true;
        Rigid = GetComponent<Rigidbody2D>();
    }
    protected void Start()
    {
        EnemyHpSlider = GameObjectPoolSO.GetObject("EnemyHpSlider", transform.position + HpSliderMove, Quaternion.identity);
        EnemyHpSlider.transform.parent = transform;
        HpSlider = EnemyHpSlider.transform.GetChild(1).transform;
        StartCoroutine(Late());
    }
    IEnumerator Late()
    {
        yield return 0;
        Super = false;
        OnAction();
    }
}
