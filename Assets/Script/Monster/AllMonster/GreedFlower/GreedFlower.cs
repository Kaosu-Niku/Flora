using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class GreedFlower : Monster
{
    [SerializeField] GameObject Col;
    [SerializeField] Transform point;
    PlayerSystem GetPlayer;
    bool Attacking = false;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "AttackOut")
        {
            OnAction();
            return;
        }
        if (e.Data.Name == "DieOut")
        {

            return;
        }
    }
    protected override IEnumerator CustomAction()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
        while (true)
        {
            yield return 0;
        }
    }
    protected override void CustomHurt()
    {
        StartCoroutine(HurtIEnum());
    }
    IEnumerator HurtIEnum()
    {
        Hp = 9999;
        GetPlayer.SetCanJump(true);
        GetPlayer.SetCanFlash(true);
        Col.SetActive(false);
        if (D != null)
            StopCoroutine(D);
        yield return new WaitForSeconds(3);
        Attacking = false;
    }
    void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>();
        Col.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Attacking == false)
            {
                Attacking = true;
                GetPlayer.transform.position = point.position;
                GetPlayer.SetCanJump(false);
                GetPlayer.SetCanFlash(false);
                Col.SetActive(true);
                if (D != null)
                    StopCoroutine(D);
                D = StartCoroutine(DamageIEnum());
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
            }
        }
    }
    Coroutine D;
    IEnumerator DamageIEnum()
    {
        while (true)
        {
            GetPlayer.Hurt(1);
            yield return new WaitForSeconds(3);
        }
    }
}
