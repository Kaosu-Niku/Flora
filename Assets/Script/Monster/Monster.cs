using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public abstract class Monster : SkeletonAnimationSystem
{
    protected MeshRenderer MonsterRenderer;
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
            StartCoroutine(DieDroplIEnum());
            StartCoroutine(DieMaterialIEnum());
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
    protected IPoolObject EnemyHpSlider;//* 血條物件
    [SerializeField] Vector3 HpSliderMove;//* 血條初始化移動
    [HideInInspector] protected Transform HpSlider;//* 真正的血條
    new void Awake()
    {
        base.Awake();
        tag = "Monster";
        Hp = MaxHp;
        HitRecover = MaxHitRecover;
        Super = false;
        MonsterRenderer = GetComponent<MeshRenderer>();
        skeletonRootMotion = GetComponent<SkeletonRootMotion>();
        for (int x = 0; x < Attack.Count; x++)
        {
            Attack[x].SetActive(false);
        }
        StartCoroutine(Late());
    }
    IEnumerator Late()
    {
        yield return 0;
        EnemyHpSlider = GameManagerSO.GetPoolInvoke().GetObject("EnemyHpSlider", transform.position + HpSliderMove, Quaternion.identity);
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
            StopCoroutine(C);
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
            if (Hp > 0)
            {
                if (HitRecover <= 0)//? 造成硬直
                {
                    if (C != null)
                        StopCoroutine(C);
                    HitRecover = MaxHitRecover;
                    Super = true;
                    CustomHurt();
                    skeletonAnimation.AnimationState.SetAnimation(0, "Hit", false);
                    StartCoroutine(HitMaterialIEnum());
                }
            }
            else
            {
                if (C != null)
                    StopCoroutine(C);
                Super = true;
                CustomHurt();
                skeletonAnimation.AnimationState.SetAnimation(0, "Die", false);
            }
        }
    }
    IEnumerator HitMaterialIEnum()
    {
        GameManagerSO.GetPoolInvoke().GetObject("EnemyInjuriedPartical", transform.position, Quaternion.identity);
        for (int x = 0; x < 1; x++)
        {
            for (float t = 0; t < 0.2f; t += Time.deltaTime)
            {
                MonsterRenderer.material.SetFloat("Injuried", t * 5);
                yield return 0;
            }
            for (float t = 0.2f; t > 0; t -= Time.deltaTime)
            {
                MonsterRenderer.material.SetFloat("Injuried", t * 5);
                yield return 0;
            }
        }
        MonsterRenderer.material.SetFloat("Injuried", 0);
        yield break;
    }
    IEnumerator DieDroplIEnum()
    {
        while (DropMoney > 100)
        {
            GameManagerSO.GetPoolInvoke().GetObject("BigMoney", transform.position, Quaternion.identity);
            DropMoney -= 100;
            yield return new WaitForSeconds(0.05f);
        }
        while (DropMoney > 10)
        {
            GameManagerSO.GetPoolInvoke().GetObject("MiddleMoney", transform.position, Quaternion.identity);
            DropMoney -= 10;
            yield return new WaitForSeconds(0.05f);
        }
        while (DropMoney > 1)
        {
            GameManagerSO.GetPoolInvoke().GetObject("SmallMoney", transform.position, Quaternion.identity);
            DropMoney -= 1;
            yield return new WaitForSeconds(0.05f);
        }
        while (DropMp > 100)
        {
            GameManagerSO.GetPoolInvoke().GetObject("BigMp", transform.position, Quaternion.identity);
            DropMp -= 100;
            yield return new WaitForSeconds(0.05f);
        }
        while (DropMp > 10)
        {
            GameManagerSO.GetPoolInvoke().GetObject("MiddleMp", transform.position, Quaternion.identity);
            DropMp -= 10;
            yield return new WaitForSeconds(0.05f);
        }
        while (DropMp > 1)
        {
            GameManagerSO.GetPoolInvoke().GetObject("SmallMp", transform.position, Quaternion.identity);
            DropMp -= 1;
            yield return new WaitForSeconds(0.05f);
        }
        yield break;
    }
    IEnumerator DieMaterialIEnum()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            MonsterRenderer.material.SetFloat("Dissolve", t);
            yield return 0;
        }
        yield break;
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
