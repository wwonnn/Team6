using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Professor : MonoBehaviour
{
    public GameObject demand;
    public GameObject answer1;
    public GameObject answer2;

    public int tryCount;    //조합 시도 횟수

    // Start is called before the first frame update
    void Start()
    {
        NewProfessor();
    }

    //새로운 의뢰 받기
    public void NewProfessor()
    {
        tryCount = 0;
        StartCoroutine("ShowDemandText");
        demand.GetComponent<Demands>().NewDemand();
    }

    //처음에 말풍선 생성
    IEnumerator ShowDemandText()
    {
        yield return new WaitForSeconds(1.0f);
        demand.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        answer1.SetActive(true);
        answer2.SetActive(true);
    }

    //요구 수락 했을 시 말풍선 제거(버튼)
    public void InactiveDemandUI()
    {
        //demand.SetActive(false);
        answer1.SetActive(false);
        answer2.SetActive(false);
    }

    //약 조합 버튼 클릭 시 정답 비교(버튼)
    public void CompareMedi()
    {
        MediManager mediManager = GameObject.Find("MediManager").GetComponent<MediManager>();
        Demands demandMedi = GameObject.Find("Demand").GetComponent<Demands>();

        if(mediManager.makedMedi.name == demandMedi.mediNum.ToString())
        {
            Debug.Log("조합 성공!");
            GameManager.Instance.AddScore(demandMedi.demandCount);
            StopCoroutine("ShowDemandText");
            demand.SetActive(false);
            NewProfessor();
        }
        else
        {
            Debug.Log("조합 실패!");
            tryCount++;
            if(tryCount >= 2)
            {
                StopCoroutine("ShowDemandText");
                demand.SetActive(false);
                NewProfessor();
            }
        }
    }

}
