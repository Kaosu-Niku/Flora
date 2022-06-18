using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] Sprite NullSkill;
    [SerializeField] List<Image> UseSkill = new List<Image>();
    [SerializeField] List<Sprite> HaveSkill = new List<Sprite>();
    int SkillCount = 0;
    public void DesSkill(int num)
    {
        UseSkill[num].sprite = NullSkill;
        SkillCount = 0;
    }

    public void SetSkill(int num)
    {
        UseSkill[SkillCount].sprite = HaveSkill[num];
        SkillCount++;
        if (SkillCount > 4)
            SkillCount = 0;
    }
}
