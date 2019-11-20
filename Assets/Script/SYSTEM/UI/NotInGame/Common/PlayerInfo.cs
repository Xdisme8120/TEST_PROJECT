using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;

public class PlayerInfo : BaseUIForm
{


    //血量条
    Image hp_Fill;
    //蓝量条
    Image mp_Fill;
    //经验条
    Image ep_Fill;

    //头像
    Image avatar_Img;

    //血量值比例
    Text hp_Text;
    //蓝量值比例
    Text mp_Text;
    //等级
    Text level_Text;
    //呢称
    Text nikeName_Text;



    private void Awake()
    {

        hp_Fill = UnityHelper.FindTheChildNode(gameObject, "Fill_ProgressBar(Primary)").GetComponent<Image>();
        mp_Fill = UnityHelper.FindTheChildNode(gameObject, "Fill_ProgressBar(Secondary)").GetComponent<Image>();
        ep_Fill = UnityHelper.FindTheChildNode(gameObject, "Fill_ExperienceBar").GetComponent<Image>();
        avatar_Img = UnityHelper.FindTheChildNode(gameObject, "Avatar_IMG").GetComponent<Image>();


        hp_Text = UnityHelper.FindTheChildNode(gameObject, "Text_ProgressBar(Primary)").GetComponent<Text>();
        mp_Text = UnityHelper.FindTheChildNode(gameObject, "Text_ProgressBar(Secondary)").GetComponent<Text>();
        level_Text = UnityHelper.FindTheChildNode(gameObject, "Text_Level").GetComponent<Text>();
        nikeName_Text = UnityHelper.FindTheChildNode(gameObject, "Text_nikeName").GetComponent<Text>();

        UIManager.GetInstance().CloseUIForms("PlayerInfo");
        EventCenter.AddListener<HeroState>(EventDefine.UI_SetHeroInfo, SetPlayerInfo);
        EventCenter.AddListener<HeroState>(EventDefine.UI_SetPlayerInfo2Inven, SetPlayerInfo);
        CurrentUIType.UIForms_Type = UIFormType.Fixed;
    }

    // Use this for initialization
    void Start()
    {

        //maxHp=level*100
        //maxMp=level*100
        //
        //GamingData
        //float hPtemp = 1f / heroState.maxHp;
        //float mPtemp = 1f / heroState.maxMp;

        ////float epTemp = 400f / 1000f;

        //hp_Fill.fillAmount = heroState.hp * hPtemp;
        //mp_Fill.fillAmount = heroState.sp * mPtemp;
        //ep_Fill.fillAmount = epTemp;
        //Debug.Log(hp_Fill.fillAmount);
        //Debug.Log(mp_Fill.fillAmount);

        StartCoroutine(InitPlayerInfo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator InitPlayerInfo()
    {
        while (GamingData.INSTANCE().HeroState == null)
        {
            yield return null;
        }
        GamingData.INSTANCE().HeroState.maxHp = GamingData.INSTANCE().HeroState.level * 100;
        GamingData.INSTANCE().HeroState.maxMp = GamingData.INSTANCE().HeroState.level * 100;
        GamingData.INSTANCE().HeroState.levelUpExp = GamingData.INSTANCE().HeroState.level * 100;

        hp_Fill.fillAmount = GamingData.INSTANCE().HeroState.hp / GamingData.INSTANCE().HeroState.maxHp;
        mp_Fill.fillAmount = GamingData.INSTANCE().HeroState.sp / GamingData.INSTANCE().HeroState.maxMp;
        ep_Fill.fillAmount = GamingData.INSTANCE().HeroState.cueeExp / GamingData.INSTANCE().HeroState.levelUpExp;

        hp_Text.text = ((int)((GamingData.INSTANCE().HeroState.hp / GamingData.INSTANCE().HeroState.maxHp) * 100f)).ToString() + "%";
        mp_Text.text = ((int)((GamingData.INSTANCE().HeroState.sp / GamingData.INSTANCE().HeroState.maxMp) * 100f)).ToString() + "%";

        if (GamingData.heroType == 0)
        {
            avatar_Img.sprite = Resources.Load<Sprite>("Item/00");
        }
        else
        {
            avatar_Img.sprite = Resources.Load<Sprite>("Item/01");
        }

        level_Text.text = GamingData.INSTANCE().HeroState.level.ToString();
        nikeName_Text.text = GamingData.nickname;
    }
    //实时同步玩家信息
    public void SetPlayerInfo(HeroState _heroState)
    {
        ep_Fill.fillAmount = (float)_heroState.cueeExp / _heroState.levelUpExp;
        level_Text.text = _heroState.level.ToString();
        hp_Fill.fillAmount = _heroState.hp / _heroState.maxHp;
        mp_Fill.fillAmount = _heroState.sp / _heroState.maxMp;
        hp_Text.text = ((hp_Fill.fillAmount * 100f)).ToString() + "%";
        mp_Text.text = ((mp_Fill.fillAmount * 100f)).ToString() + "%";
    }

}
