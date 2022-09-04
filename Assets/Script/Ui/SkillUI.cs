using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [System.Serializable]
    public class skillData
    {
        public string SkillName;
        public Sprite SKillSprite;
        public int UseCount;
        [Multiline(15)]
        public string SkillCaption;
    }
    [SerializeField] List<Button> SkillButton = new List<Button>();
    [SerializeField] List<skillData> SKillData = new List<skillData>();
    [SerializeField] TMPro.TextMeshProUGUI SkillNameText;
    [SerializeField] Image SkillImage;
    [SerializeField] List<Image> CostCountImage = new List<Image>();
    [SerializeField] TMPro.TextMeshProUGUI SkillCaptionText;
    [SerializeField] List<Image> CanCountImage = new List<Image>();
    [SerializeField] List<Image> UseCountImage = new List<Image>();
    bool CanUse;
    public void OnClick_ShowSkillData(int which)
    {
        CanUse = SkillButton[which].interactable;
        if (CanUse == true)
        {
            SkillNameText.text = SKillData[which].SkillName;
            SkillImage.sprite = SKillData[which].SKillSprite;
            for (int x = 0; x < CostCountImage.Count; x++)
                CostCountImage[x].enabled = false;
            for (int x = 0; x < SKillData[which].UseCount; x++)
                CostCountImage[x].enabled = true;
            SkillCaptionText.text = SKillData[which].SkillCaption;
        }
        else
        {
            SkillNameText.text = "? ? ?";
            SkillImage.sprite = null;
            for (int x = 0; x < CostCountImage.Count; x++)
                CostCountImage[x].enabled = false;
            SkillCaptionText.text = null;
        }
    }
    public void OnClick_UseSkill(int which)
    {
        CanUse = SkillButton[which].interactable;
        if (CanUse == true)
        {
            PlayerSystemSO.UseSkill(which);
            for (int x = 0; x < UseCountImage.Count; x++)
                UseCountImage[x].enabled = false;
            for (int x = 0; x < PlayerSystemSO.SkillNowPoint; x++)
                UseCountImage[x].enabled = true;
        }
    }
    private void OnDisable()
    {
        SkillNameText.text = null;
        SkillImage.sprite = null;
        for (int x = 0; x < CostCountImage.Count; x++)
            CostCountImage[x].enabled = false;
        SkillCaptionText.text = null;
        for (int x = 0; x < CanCountImage.Count; x++)
            CanCountImage[x].enabled = false;
        for (int x = 0; x < PlayerSystemSO.SkillMaxPoint; x++)
            CanCountImage[x].enabled = true;
    }
    private void Start()
    {
        SkillNameText.text = null;
        SkillImage.sprite = null;
        for (int x = 0; x < CostCountImage.Count; x++)
            CostCountImage[x].enabled = false;
        SkillCaptionText.text = null;
        for (int x = 0; x < CanCountImage.Count; x++)
            CanCountImage[x].enabled = false;
        for (int x = 0; x < PlayerSystemSO.SkillMaxPoint; x++)
            CanCountImage[x].enabled = true;
    }
}
