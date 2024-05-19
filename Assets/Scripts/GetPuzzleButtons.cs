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

    private int RequiredCorrectGuesses;
    private int currentCorrectGuesses;
    private int countGuesses;
    void Awake()
    {
        puzzleSprites = Resources.LoadAll<Sprite>("Sprites/PuzzleSprites");
    }
    void Start()
    {
        getButtons();
        AddListener();
        AddGamePuzzleSprites();
        RequiredCorrectGuesses = puzzleButtons.Count / 2;
        shufflePuzzle(gamePuzzleSprites);
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
        else if (!secondClick)
        {
            secondClick = true;
            secondClickIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondClickName = gamePuzzleSprites[secondClickIndex].name;
            puzzleButtons[secondClickIndex].image.sprite = gamePuzzleSprites[secondClickIndex];

            StartCoroutine(checkIfPuzzlesMatch());
            countGuesses++;
        }

    }
    IEnumerator checkIfPuzzlesMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstClickName == secondClickName)
        {
            yield return new WaitForSeconds(0.5f);

            puzzleButtons[firstClickIndex].interactable = false;
            puzzleButtons[secondClickIndex].interactable = false;

            puzzleButtons[firstClickIndex].image.color = new Color (0,0,0,0);
            puzzleButtons[secondClickIndex].image.color = new Color (0,0,0,0);

            checkIfGameIsFinished();
        }
        else{
        puzzleButtons[firstClickIndex].image.sprite = puzzleButtonImage;
        puzzleButtons[secondClickIndex].image.sprite = puzzleButtonImage;
        }
        yield return new WaitForSeconds(0.5f);

        firstClick = secondClick = false;

    }

    void checkIfGameIsFinished()
    {
        currentCorrectGuesses++;
        if(currentCorrectGuesses == RequiredCorrectGuesses){
            Debug.Log("Game is Finished");
            Debug.Log("It took you " + countGuesses + " guesses ('-')");
        }
    }

    void shufflePuzzle(List<Sprite> list){

        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


}
