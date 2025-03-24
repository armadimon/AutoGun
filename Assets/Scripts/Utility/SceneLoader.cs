using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static StageData SelectedStage; // 선택한 스테이지 정보를 저장하는 변수

    public void LoadStage(StageData stageData)
    {
        SelectedStage = stageData;
        SceneManager.LoadScene("StageScene"); // 스테이지 씬으로 이동
    }
}
