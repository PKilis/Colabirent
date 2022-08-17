using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Leveller")]
    [SerializeField] private GameObject[] levels;
    public bool bolumGec;
    public bool openVib;

    [Header("Canvas")]
    [SerializeField] internal GameObject playPanel;
    [SerializeField] internal GameObject restartPanel;
    [SerializeField] internal GameObject nextLevelPanel;
    [SerializeField] internal Slider vibrationSlider;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject vibrationOnImage;
    [SerializeField] private GameObject vibrationOffImage;

    private int levelIndex;

    GameObject temp;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        levelIndex = 0;

        temp = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
        openVib = true;

        temp.SetActive(true);
        Time.timeScale = 0;
        bolumGec = false;
    }

    void Update()
    {


        if (bolumGec)
        {
            Next_Level();
        }

    }

    public void Start_Level()
    {
        Time.timeScale = 1;
        playPanel.SetActive(false);


    }

    public void Next_Level()
    {
        Destroy(temp);
//        levels[levelIndex].SetActive(false);
        ++levelIndex;

        if (levelIndex > levels.Length - 1 )
        {
            levelIndex = 0;
        }
        temp = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
        bolumGec = false;

    }

    public void Restart_Level()
    {
        Destroy(temp);

        temp = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
        if (!temp.activeSelf)
        {
            temp.SetActive(true);
        }
//        levels[levelIndex].SetActive(false);

        restartPanel.SetActive(false);
    }

    public void Open_Settings()
    {
        playPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Close_Settings()
    {
        settingsPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void Slider_Control()
    {
        if (vibrationSlider.value > 0)
        {
            openVib = false;
            vibrationOffImage.SetActive(true);
            vibrationOnImage.SetActive(false);
        }
        else
        {
            openVib = true;
            vibrationOnImage.SetActive(true);
            vibrationOffImage.SetActive(false);
        }
    }
}
