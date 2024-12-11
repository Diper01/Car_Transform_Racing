using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneButtonsController : MonoBehaviour {

    public static SceneButtonsController Instance;
    public static bool _keyboard = false;
    public ButtonEntities[] playerUISkills;
    List<ButtonEntities> buttonEntities = new List<ButtonEntities>();
    List<ButtonEntities> currentButtons = new List<ButtonEntities>();
    ButtonEntities currentButtonEntitiesSelecting;

    int playerUISkillsIndex;
    int currentButtonsIndex;

    private bool isReceiveAndroidBackKeyEvent;
    public bool IsReceiveAndroidBackKeyEvent
    {
        get
        {
            return isReceiveAndroidBackKeyEvent;
        }
        set
        {
            isReceiveAndroidBackKeyEvent = value;
            //print(value);
        }
    }

    public delegate void AndroidBackKeyEventHandler();
    public event AndroidBackKeyEventHandler AndroidBackKeyEvent;

    public void Init()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        QualitySetting.Init();

        Application.targetFrameRate = 30;
    }
    public void ClearAndroidBackKeyEvent()
    {
        AndroidBackKeyEvent = null;
    }
    public void RegisterAllButton(List<ButtonEntities> btnList)
    {
        for (int i = 0; i < btnList.Count; i++)
        {
            if(!IsSkillsExist(btnList[i]) &&!buttonEntities.Contains(btnList[i]))//check Skills buttons
            {
                buttonEntities.Add(btnList[i]);
            }
        }        
    }

    public void SetActiveGuideButton(ButtonEntities buttonEntitie)
    {
        ClearActiveButtons(); 

        currentButtons.Add(buttonEntitie);
        currentButtonEntitiesSelecting = buttonEntitie;
        currentButtonEntitiesSelecting.Select();
        //Debug.Log(currentButtonEntitiesSelecting.name);
        
    }
    public void SetActiveButtons(List<ButtonEntities> btns)
    {
        currentButtonsIndex = 0;
        currentButtons.Clear();
        for (int i = 0; i < btns.Count; i++)
        {
            if(!IsSkillsExist(btns[i]))//check Skills buttons
            {
                currentButtons.Add(btns[i]);
            }
        }
        //Debug.Log("SetActiveButtons");
    }
    public void ClearActiveButtons()
    {
        currentButtons.Clear();
        //currentButtonEntitiesSelecting.Deselect();
        currentButtonEntitiesSelecting = null;
        currentButtonsIndex = 0;
    }
    private void Awake()
    {
       Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsReceiveAndroidBackKeyEvent)
                return;

            if (AndroidBackKeyEvent != null)
            {
                if (currentButtonEntitiesSelecting != null && currentButtonEntitiesSelecting.IsSelectble())//тут что то работает неправельно. Если изначально не выбрана никакая кнопка, диселект не происходит и кнопка в меню паузе не уменьшает размер.
                    currentButtonEntitiesSelecting.Deselect();

                AndroidBackKeyEvent();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            KeyBoardHandle(KeyOrder.Enter);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KeyBoardHandle(KeyOrder.LeftDown);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            KeyBoardHandle(KeyOrder.LeftUp);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            KeyBoardHandle(KeyOrder.RightDown);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            KeyBoardHandle(KeyOrder.RightUp);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            KeyBoardHandle(KeyOrder.Up);
            Input.ResetInputAxes();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KeyBoardHandle(KeyOrder.Down);
            Input.ResetInputAxes();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            KeyBoardHandle(KeyOrder.Jump);
        }


        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown((KeyCode)10)) //中间按键
        {
            KeyBoardHandle(KeyOrder.Enter);
        }
        
    }
    void KeyBoardHandle(KeyOrder key)
    {
        //Debug.Log(key);

        switch (key)
        {
            case KeyOrder.Enter:
                _keyboard = true;
                OnButtonEntityClick();
                //Debug.Log(IsPlayingGame());
                break;
            case KeyOrder.Up:

                _keyboard = true;
                if (UIGuideControllor_GameClone.Instance.gameObject.activeInHierarchy && UIGuideControllor_GameClone.Instance.goUseCurPropBtn.activeInHierarchy)
                {
                    OnButtonEntityClick();
                }

                if (IsPlayingGame())
                {
                    GamePlayerUIControllor.Instance.UsePropOnClick();
                }
                else
                {
                    SelectNextBtnEntity(true);
                }
                break;

            case KeyOrder.Down:
                _keyboard = true;
                if (UIGuideControllor_GameClone.Instance.gameObject.activeInHierarchy &&( UIGuideControllor_GameClone.Instance.goUseFlyBmobBtn.activeInHierarchy|| UIGuideControllor_GameClone.Instance.goUseSpeedupBtn.activeInHierarchy))
                {
                    OnButtonEntityClick();
                }
                if (IsPlayingGame())
                {
                    SelectNextPlayerUISkills();
                }
                else
                {
                    SelectNextBtnEntity(false);
                }
                break;

            case KeyOrder.LeftDown:
                _keyboard = true;
                if (UIGuideControllor_GameClone.Instance.gameObject.activeInHierarchy && UIGuideControllor_GameClone.Instance.goGamePlayerUILeftGuideBtn.activeInHierarchy)
                {
                    OnButtonEntityClick();
                }
                if (IsPlayingGame())
                {
                    GamePlayerUIControllor.Instance.LeftDown();
                }
                else
                {
                    SelectNextBtnEntity(true);
                }
                break;

            case KeyOrder.LeftUp:
                _keyboard = true;
                if (IsPlayingGame())
                {
                    GamePlayerUIControllor.Instance.LeftUp();
                }
                break;

            case KeyOrder.RightDown:
                _keyboard = true;
                if (UIGuideControllor_GameClone.Instance.gameObject.activeInHierarchy && UIGuideControllor_GameClone.Instance.goGamePlayerUIRightGuideBtn.activeInHierarchy)
                {
                    OnButtonEntityClick();
                }
                if (IsPlayingGame())
                {
                    GamePlayerUIControllor.Instance.RightDown();
                }
                else
                {
                    SelectNextBtnEntity(false);
                }
                break;

            case KeyOrder.RightUp:
                _keyboard = true;
                if (IsPlayingGame())
                {
                    GamePlayerUIControllor.Instance.RightUp();
                }
                break;
            default:
                break;
        }

    }

    private bool IsSkillsExist(ButtonEntities b)//do not add Skills buttons to other lists
    {
        for (int i = 0; i < playerUISkills.Length; i++)
        {
            if(playerUISkills[i].GetInstanceID().Equals(b.GetInstanceID()))
            {
                return true;
            }
        }
        return false;
    }

    private void SelectNextPlayerUISkills()
    {
        if (playerUISkills[playerUISkillsIndex].IsSelectble())
        {
            currentButtonEntitiesSelecting = playerUISkills[playerUISkillsIndex];
            currentButtonEntitiesSelecting.Select();

            playerUISkillsIndex = playerUISkillsIndex >= playerUISkills.Length - 1 ? 0 : playerUISkillsIndex + 1;
            //Debug.Log(currentButtonEntitiesSelecting.name);
        }
        else
        {
            if (!IsAnySelectble(playerUISkills))//out of recursion
                return;

            playerUISkillsIndex = playerUISkillsIndex >= playerUISkills.Length - 1 ? 0 : playerUISkillsIndex + 1;
            SelectNextPlayerUISkills();
        }

    }

    void SelectNextBtnEntity(bool isUp)
    {
        if (currentButtons.Count > 0)
        {
            if (isUp)
            {
                currentButtonsIndex = currentButtonsIndex <= 0 ? currentButtons.Count - 1 : currentButtonsIndex - 1;
            }
            else
            {
                currentButtonsIndex = currentButtonsIndex >= currentButtons.Count - 1 ? 0 : currentButtonsIndex + 1;
            }

            if (currentButtons[currentButtonsIndex].IsSelectble())
            {
                //Debug.Log(currentButtons[currentButtonsIndex]);
                currentButtonEntitiesSelecting = currentButtons[currentButtonsIndex];
                currentButtonEntitiesSelecting.Select();                
            }
            else
            {
                if (!IsAnySelectble(currentButtons.ToArray()))//out of recursion
                    return;

                SelectNextBtnEntity(isUp);
            }
            
        }
        else//может быть ненужно.
        {
            for (int i = 0; i < buttonEntities.Count; i++)
            {
                if (!currentButtons.Contains(buttonEntities[i]))
                {
                    if (buttonEntities[i].IsSelectble())
                    {
                        currentButtons.Add(buttonEntities[i]);
                    }
                }
            }
        }
    }
    bool IsAnySelectble(ButtonEntities[] buttons)
    {
        int a=0;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].IsSelectble())
            {
                a++;
            }
        }
        if (a >= buttons.Length)
        {
            return false;
        }

        return true;
    }
    void OnButtonEntityClick()
    {
        if (currentButtonEntitiesSelecting != null)
        {
            //Debug.Log(currentButtonEntitiesSelecting.name);
            currentButtonEntitiesSelecting.OnClick();
            ClearActiveButtons();
            playerUISkillsIndex = 0;
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Application.targetFrameRate = 0;
            PlayerData.Instance.SaveData();
            ExchangeActivityData.Instance.SaveData();
        }
        else
        {
            Application.targetFrameRate = 30;
        }
    }
    bool IsPlayingGame()
    {
        if (GlobalConst.SceneName == SceneType.GameScene && !GameData.Instance.IsPause && !GameData.Instance.IsWin)
            return true;
        return false;
    }
    void OnDisable()
    {
        PlayerData.Instance.SaveData();
    }
    enum KeyOrder
    {
        Up,
        Down,
        LeftDown,
        LeftUp,
        RightDown,
        RightUp,
        Enter,
        Back,
        Attack,
        Jump,
    }
}
