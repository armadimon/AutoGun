using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public StageData stageData; // 이 버튼이 연결된 스테이지 데이터
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectStage);
    }

    private void SelectStage()
    {
        sceneLoader.LoadStage(stageData);
    }
}