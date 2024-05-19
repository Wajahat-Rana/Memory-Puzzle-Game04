using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPuzzleButtons : MonoBehaviour
{
    public List<Button> puzzleButtons = new List<Button>();

    [SerializeField]
    private Sprite puzzleButtonImage;
    public Sprite[] puzzleSprites;
    public List<Sprite> gamePuzzleSprites = new List<Sprite>();

    private bool firstClick, secondClick;
    private int firstClickIndex, secondClickIndex;
    private string firstClickName, secondClickName;
    void Awake()
    {
        puzzleSprites = Resources.LoadAll<Sprite>("Sprites/PuzzleSprites");
    }
    void Start()
    {
        getButtons();
        AddListener();
        AddGamePuzzleSprites();
    }
    void getButtons()
    {
        GameObject[] btns = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < 8; i++)
        {
            puzzleButtons.Add(btns[i].GetComponent<Button>());
            puzzleButtons[i].image.sprite = puzzleButtonImage;
        }
    }

    void AddGamePuzzleSprites()
    {
        int looper = puzzleButtons.Count;
        int index = 0;
        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzleSprites.Add(puzzleSprites[index]);
            index++;
        }
    }
    void AddListener()
    {
        foreach (Button btn in puzzleButtons)
        {
            btn.onClick.AddListener(PuzzleButtonClicked);
        }
    }
    void PuzzleButtonClicked()
    {

        if (!firstClick)
        {
            firstClick = true;
            firstClickIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstClickName = gamePuzzleSprites[firstClickIndex].name;
            puzzleButtons[firstClickIndex].image.sprite = gamePuzzleSprites[firstClickIndex];
        }
        else if (!secondClick){
            secondClick = true;
            secondClickIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondClickName = gamePuzzleSprites[secondClickIndex].name;
            puzzleButtons[secondClickIndex].image.sprite = gamePuzzleSprites[secondClickIndex];
            if (firstClickName == secondClickName)
            {
                Debug.Log("Matched");
            }
            else{
                Debug.Log("Not Matched");
            }
        }

    }


}
