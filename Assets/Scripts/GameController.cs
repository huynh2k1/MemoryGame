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
        OnClick();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        Debug.Log(gameGuesses);
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

            firstPickPuzzle = gamePuzzles[firstPickIndex].name;

            buttons[firstPickIndex].image.sprite = gamePuzzles[firstPickIndex];
        }
        else if(!secondPick)
        {
            secondPick = true;
            secondPickIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondPickPuzzle = gamePuzzles[secondPickIndex].name;

            buttons[secondPickIndex].image.sprite = gamePuzzles[secondPickIndex];

            countGusses++;

            StartCoroutine(CheckIsCouple());

        }
    }
    //Kiểm tra cặp được chọn có giống nhau không
    IEnumerator CheckIsCouple()
    {
        yield return new WaitForSeconds(1f);
        if(firstPickPuzzle == secondPickPuzzle)
        {
            yield return new WaitForSeconds(0.5f);
            buttons[firstPickIndex].interactable = false;
            buttons[secondPickIndex].interactable = false;

            CheckGameOver();
        }
        else
        {
            buttons[firstPickIndex].image.sprite = btnImage;
            buttons[secondPickIndex].image.sprite = btnImage;

        }
        yield return new WaitForSeconds(0.5f);

        firstPick = secondPick = false;
    }
    //Kiểm tra game đã kết thúc hay chưa
    private void CheckGameOver()
    {
        countCorrectGuesses++;
        if(countCorrectGuesses == gameGuesses)//Nếu như số match = số cặp của trò chơi
        {
            Debug.Log("End Game!!!");
            Debug.Log("It took you " + countGusses + "many guess to finish game");
        }
    }

    private void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
