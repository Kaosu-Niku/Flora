using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] protected float Hp;
    protected float MaxHp;
    [SerializeField] float HitRecover;//* 硬直累積量
    float MaxHitRecover;
    [SerializeField] float MaxDis;//* 最大範圍限制
    protected float LeftDis;//* 最左邊
    protected float RightDis;//* 最右邊
    [SerializeField] protected float HurtTime;//* 受傷的硬直時間，配合受傷動畫
    bool Super;//* 是否無敵
    protected bool IsDeath = false;//* 是否死亡
    [SerializeField] protected float DropMoney;//* 死亡掉落多少錢
    [SerializeField] protected float DropMp;//* 死亡掉落多少魔力   
    [SerializeField] protected float DesTime;//* 死亡幾秒後刪除

    protected Rigidbody2D Rigid;
    [SerializeField] protected Animator Anima;
    //* 其他組件
    [SerializeField] GameObjectPoolSO GetGameObjectPool;
    [HideInInspector] protected GameObject HpObject;//* 血條物件(存放在物件池MyGameObjectPool)
    [HideInInspector] protected Transform HpSlider;//* 真正的血條
    [SerializeField] Vector2 HpSliderMove;//* 血條初始化移動
    protected GameObject GetPlayer;

    protected void OnAction()//? 重置行動
    {
        if (PlayerDataSO.CantFindPlayer == false)
            StartCoroutine(OnActionIEnum());
        else
            StartCoroutine(DontActionIEnum());
    }
    public void Hurt(float damage, float hitValue)//? 受傷
    {
        StartCoroutine(HitIEnum(hitValue));
        StartCoroutine(HurtIEnum(damage));
        StartCoroutine(CustomHurtIEnum());
    }
    protected void Hit()//? 硬直
    {
        StartCoroutine(ResetAction(HurtTime));
        StartCoroutine(CustomHitRecoverIEnum());
    }
    protected void Die()//? 死亡
    {
        StartCoroutine(DieIEnum());
        StartCoroutine(CustomDieIEnum());
    }
    protected virtual IEnumerator DontActionIEnum()//? 無行為行動
    {
        yield return new WaitForSeconds(1);
        OnAction();
        yield break;
    }
    protected virtual IEnumerator OnActionIEnum()//? 自定義行動邏輯
    {
        yield break;
    }
    protected virtual IEnumerator CustomHurtIEnum()//? 自定義受傷行為
    {
        yield break;
    }
    protected virtual IEnumerator CustomHitRecoverIEnum()//? 自定義硬直行為
    {
        yield break;
    }
    protected virtual IEnumerator CustomDieIEnum()//? 自定義死亡行為
    {
        yield break;
    }
    private IEnumerator HitIEnum(float hitValue)//? 達到最大硬直就中斷所有執行中協程
    {
        if (Super == false)
        {
            HitRecover -= hitValue;
            if (HitRecover <= 0)
            {
                HitRecover = MaxHitRecover;
                StopAllCoroutines();
                Hit();
                yield break;
            }
        }
        else
            yield break;
    }
    private IEnumerator ResetAction(float time)//? 硬直後的行動重置
    {
        yield return 0;
        Super = true;
        yield return new WaitForSeconds(time);
        Super = false;
        OnAction();
        yield break;
    }
    private IEnumerator HurtIEnum(float damage)
    {
        if (Super == false)
        {
            Hp -= damage;
            HpSlider.localScale = new Vector3(Hp / MaxHp, 1, 0);
            yield return 0;
            if (Hp <= 0)
                Die();
            yield break;
        }
        else
            yield break;

    }
    private IEnumerator DieIEnum()
    {
        IsDeath = true;
        Super = true;
        HpObject.transform.localScale = Vector3.zero;
        Rigid.gravityScale = 0;
        while (DropMoney > 1000)
        {
            Instantiate(GetGameObjectPool.GetGameObject(5, transform.position, Quaternion.identity));
            DropMoney -= 1000;
        }
        while (DropMoney > 100)
        {
            Instantiate(GetGameObjectPool.GetGameObject(4, transform.position, Quaternion.identity));
            DropMoney -= 100;
        }
        while (DropMoney > 10)
        {
            Instantiate(GetGameObjectPool.GetGameObject(3, transform.position, Quaternion.identity));
            DropMoney -= 10;
        }
        Destroy(this.gameObject, DesTime);
        StopAllCoroutines();
        yield break;
    }
    private void Awake()
    {
        LeftDis = transform.position.x - MaxDis / 2;
        RightDis = transform.position.x + MaxDis / 2;
        MaxHp = Hp;
        MaxHitRecover = HitRecover;
        Rigid = GetComponent<Rigidbody2D>();
        HpObject = GetGameObjectPool.GetGameObject(2, new Vector3(transform.position.x + HpSliderMove.x, transform.position.y + HpSliderMove.y, 0), Quaternion.identity);
        HpObject.transform.parent = transform;
        HpSlider = HpObject.transform.GetChild(1).transform;
        HpObject.SetActive(false);
        GetPlayer = GameObject.FindGameObjectWithTag("Player");
        Invoke("OnAction", Time.deltaTime);
    }
}
