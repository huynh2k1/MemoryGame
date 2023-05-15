using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Sprite btnImage;//ảnh nền của các card
    public Sprite[] puzzles;//mảng hình ảnh câu đố của game
    public List<Sprite> gamePuzzles = new List<Sprite>();//danh sách ảnh câu đố cho màn chơi
    public List<Button> buttons = new List<Button>();//danh sách các nút(card)

    private bool firstPick, secondPick;

    private int countGusses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstPickIndex, secondPickIndex;

    private string firstPickPuzzle, secondPickPuzzle;

    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/Animals");    
    }

    private void Start()
    {
        GetButton();
        PickACard();
        AddGamePuzzles();
    }

    public void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card"); //Tim tat ca cac button co tag "Card"
        for(int i = 0; i < objects.Length; i++)
        {
            buttons.Add(objects[i].GetComponent<Button>()); //Them vao danh sach buttons
            buttons[i].image.sprite = btnImage; //Set ảnh nền cho các card
        }
    }
    //Lấy số ảnh = số card / 2
    private void AddGamePuzzles()
    {
        int looper = buttons.Count;
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;

        }
    }

    public void OnClick()
    {
        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() => PickACard());
        }
    }

    private void PickACard()
    {
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //nếu ô chưa chọn
        if (!firstPick)
        {
            firstPick = true;
            firstPickIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            buttons[firstPickIndex].image.sprite = gamePuzzles[firstPickIndex];
        }
        else if(!secondPick)
        {
            secondPick = true;
            secondPickIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            buttons[secondPickIndex].image.sprite = gamePuzzles[secondPickIndex];
        }
    }

}
