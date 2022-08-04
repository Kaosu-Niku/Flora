using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public abstract class Monster : SkeletonAnimationSystem
{
    protected SkeletonRootMotion skeletonRootMotion;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        //? 敵人通用動畫事件
        if (e.Data.Name == "HitOut")
        {
            Super = false;
            OnAction();
            return;
        }
        if (e.Data.Name == "DieOut")
        {
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
            return;
        }
        //? 自定義動畫事件
        CustomAnimationEventCallBack(trackEntry, e);
    }
    protected abstract void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e);
    [SerializeField] protected float MaxHp;
    protected float Hp;
    [SerializeField] protected float MaxHitRecover;
    protected float HitRecover;//* 硬直累積量
    protected bool Super;//* 是否無敵
    [SerializeField] protected float DropMoney;//* 死亡掉落多少錢
    [SerializeField] protected float DropMp;//* 死亡掉落多少魔力 
    [SerializeField] protected List<GameObject> Attack = new List<GameObject>();
    //* 其他組件
    protected PoolObject EnemyHpSlider;//* 血條物件
    [SerializeField] Vector3 HpSliderMove;//* 血條初始化移動
    [HideInInspector] protected Transform HpSlider;//* 真正的血條
    private void Start()
    {
        Hp = MaxHp;
        HitRecover = MaxHitRecover;
        Super = false;
        skeletonRootMotion = GetComponent<SkeletonRootMotion>();
        EnemyHpSlider = GameObjectPoolSO.GetObject("EnemyHpSlider", transform.position + HpSliderMove, Quaternion.identity);
        EnemyHpSlider.transform.parent = transform;
        HpSlider = EnemyHpSlider.transform.GetChild(1).transform;
        EnemyHpSlider.gameObject.SetActive(false);
        OnAction();
    }
    Coroutine C;
    protected abstract IEnumerator CustomAction();//? 自定義行動
    protected void OnAction()//? 重置行動
    {
        if (C != null)
        {
            StopCoroutine(C);
        }
        C = StartCoroutine(CustomAction());
    }
    protected abstract void CustomHurt();//? 自定義受傷行為
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
                Super = true;
                CustomHurt();
                skeletonAnimation.AnimationState.SetAnimation(0, "Die", false);
                return;
            }
            if (HitRecover <= 0)//? 造成硬直
            {
                HitRecover = MaxHitRecover;
                Super = true;
                CustomHurt();
                skeletonAnimation.AnimationState.SetAnimation(0, "Hit", false);
            }
        }
    }

    protected float GetPlayerDistance()//? 立即取得與玩家的距離
    {
        return Mathf.Abs(Vector2.Distance(transform.position, PlayerSystemSO.GetPlayerInvoke().transform.position));
    }
    protected void LookPlayer()//? 立即看向玩家位置
    {
        if (PlayerSystemSO.GetPlayerInvoke().transform.position.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.identity;
    }
}
