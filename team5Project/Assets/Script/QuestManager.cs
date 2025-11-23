using System.Collections;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText; //임무 표시 텍스트
    public static QuestManager instance;
    public enum QuestStep { DishWash, GiftFound, PickPhone } //임무 종류
    public QuestStep currentQuest = QuestStep.DishWash; //임무 순서 제어

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable() //임무 이벤트 받기
    {
        EventManager.OnEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventManager.OnEvent -= HandleEvent;
    }
    void Start() //첫 시작시 설거지부터
    {
        SetQuest("Do the dishes");
        TimerManager.instance.StartTimer();
    }

    public void SetQuest(string message) //화면에 임무 띄우기
    {
        questText.text = message;
    }

    private void HandleEvent(EventManager.EventType e) //오브젝트 종류 구분
    {
        switch (e) 
        {
            case EventManager.EventType.Plate:
                OnDishWash();
                break;

            case EventManager.EventType.Gift:
                OnGiftFound();
                break;

            case EventManager.EventType.Phone:
                OnPhone();
                break;

        }
    }


    // 각 임무 단계
    public void OnDishWash() //설거지
    {
        if(currentQuest != QuestStep.DishWash) return;
        VoiceManager.instance.PlayVoice(0);
        SubtitleManager.instance.ShowSubtitle("Where did I put my son's birthday present...", 2f);
        SetQuest("Find a birthday present");
        currentQuest = QuestStep.GiftFound;
    }
    public void OnGiftFound() //생일 선물 찾기
    {
        if(currentQuest != QuestStep.GiftFound) return;
        ScoreManager.instance.AddScore(10);
        VoiceManager.instance.PlayRing();
        currentQuest = QuestStep.PickPhone;
        SetQuest("Answer the phone.");
    }

    public void OnPhone() //전화 받기
    {
        if(currentQuest != QuestStep.PickPhone) return;
        ScoreManager.instance.AddScore(10);
    }
}
