using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public StageData stageData;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SelectStage()
    {
        sceneLoader.LoadStage(stageData);
    }

    public void ReturnToStageSelectScene()
    {
        sceneLoader.ReturnToStageSelectScene();
    }
}