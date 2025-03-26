using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static StageData SelectedStage;

    public void LoadStage(StageData stageData)
    {
        SelectedStage = stageData;
        SceneManager.LoadScene("StageScene");
    }

    public void ReturnToStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
